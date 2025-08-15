using InterLinkClass.ControlObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace apps
{
    public partial class InternalPayment : System.Web.UI.Page
    {
        ProcessPay Process = new ProcessPay();
        DataLogin datafile = new DataLogin();
        Datapay datapay = new Datapay();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();
        DataTable dtable = new DataTable();
        private ReportDocument Rptdoc = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    LoadVendors();
                    if (Session["AreaID"].ToString().Equals("3"))
                    {
                        cboVendor.SelectedIndex = cboVendor.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                        cboVendor.Enabled = false;
                    }
                    Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                    Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                    Button MenuReport = (Button)Master.FindControl("btnCalReports");
                    Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                    Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                    Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                    MenuTool.Font.Underline = false;
                    MenuPayment.Font.Underline = false;
                    MenuReport.Font.Underline = false;
                    MenuRecon.Font.Underline = true;
                    MenuAccount.Font.Underline = false;
                    MenuBatching.Font.Underline = false;
                    DisableBtnsOnClick();
                    ToggleToken();
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }
        private void LoadVendors()
        {
            dtable = datafile.GetAllVendors("0");
            cboVendor.DataSource = dtable;
            cboVendor.DataValueField = "VendorCode";
            cboVendor.DataTextField = "Vendor";
            cboVendor.DataBind();
        }
        private void ToggleToken()
        {
            MultiView5.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = 0;
            MultiView3.ActiveViewIndex = 0;
            rdcash.Checked = true;
            Button1.Enabled = false;
            Button1.Visible = true;
            txtCustRef.Focus();
            LoadPayTypes();
        }

        private void LoadPayTypes()
        {
            dtable = datafile.GetPayTypes();
            cboPayType.DataSource = dtable;
            cboPayType.DataValueField = "PaymentCode";
            cboPayType.DataTextField = "PaymentType";
            cboPayType.DataBind();
        }
        private void ShowMessage(string Message, bool Error)
        {
            Label lblmsg = (Label)Master.FindControl("lblmsg");
            if (Error) { lblmsg.ForeColor = System.Drawing.Color.Red; lblmsg.Font.Bold = false; }
            else { lblmsg.ForeColor = System.Drawing.Color.Black; lblmsg.Font.Bold = true; }
            if (Message == ".")
            {
                lblmsg.Text = ".";
            }
            else
            {
                lblmsg.Text = "MESSAGE: " + Message.ToUpper();
            }
        }
        private void DisableBtnsOnClick()
        {
            string strProcessScript = "this.value='Working...';this.disabled=true;";
            Button1.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button1, "").ToString());
            Button2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button2, "").ToString());
            //Button3.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button3, "").ToString());
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                InquireCustomer();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        private void InquireCustomer()
        {
            string custref = txtCustRef.Text.Trim();
            if (custref.Equals(""))
            {
                ShowMessage("Please Enter Customer Reference Number", true);
                Button1.Enabled = false;
                txtCustRef.Focus();
            }
            else
            {
                ShowMessage(".", true);
                MultiView3.ActiveViewIndex = 0;
                Customer cust = new Customer();
                cust = bll.InquireCust(custref);
                if (cust.StatusCode.Equals("0"))
                {
                    txtcode.Text = cust.CustomerRef;
                    txtname.Text = cust.CustomerName;
                    txtbal.Text = cust.Balance.ToString("#,##0");
                    txtCustType.Text = cust.CustomerType.ToString().ToUpper();
                    txtagentRef.Text = "";
                    txtAmount.Text = "";
                    cboVendor.SelectedIndex = cboVendor.Items.IndexOf(cboVendor.Items.FindByValue("0"));
                    cboPayType.SelectedIndex = cboPayType.Items.IndexOf(cboPayType.Items.FindByValue("0"));
                    Button1.Enabled = true;
                }
                else
                {
                    ShowMessage(cust.StatusDescription, true);
                    txtcode.Text = "";
                    txtname.Text = "";
                    txtCustType.Text = "";
                    txtbal.Text = "";
                    txtAmount.Text = "";
                    txtPhone.Text = "";
                    Button1.Enabled = false;
                }

            }
        }
        private string GetPayTypeSelected()
        {
            string ret = "";
            if (rdcash.Checked.Equals(true))
            {
                ret = "CASH";
            }
            else if (rdcheque.Checked.Equals(true))
            {
                ret = "CHEQUE";
            }
            else
            {
                ret = "NONE";
            }
            return ret;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                PostTransaction();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        private void PostTransaction()
        {
            string custref = txtcode.Text.Trim();
            string custname = txtname.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string amount = txtAmount.Text.Trim();
            string bal = txtbal.Text.Trim();
            string custtype = txtCustType.Text.Trim();
            string paymode = GetPayTypeSelected();
            string payType = cboPayType.SelectedValue.ToString();
            string teller = Session["Username"].ToString();
            string vendorcode = cboVendor.SelectedValue.ToString();
            string agentref = txtagentRef.Text.Trim();
            bool sms = chkSendSms.Checked;
            if (custref.Equals(""))
            {
                ShowMessage("Customer Ref is Required", true);
            }
            else if (custname.Equals(""))
            {
                ShowMessage("Customer Name is Required", true);
            }
            else if (vendorcode.Equals("0"))
            {
                ShowMessage("Please Select Agent", true);
            }
            else if (agentref.Equals(""))
            {
                ShowMessage("Agent Ref is Required", true);
                txtagentRef.Focus();
            }
            else if (payType.Equals("0"))
            {
                ShowMessage("Please Select Payment Type", true);
            }
            else if (amount.Equals(""))
            {
                ShowMessage("Please Enter Amount to pay", true);
                txtAmount.Focus();
            }
            else if (amount.Equals("0"))
            {
                ShowMessage("Amount to post cannot be zero", true);
                txtAmount.Focus();
            }
            else if (bal.Equals(""))
            {
                ShowMessage("Customer Balance is Required", true);
            }
            else if (phone.Equals("") && sms)
            {
                ShowMessage("Please Provide Phone Number or Uncheck Send SMS", true);
                txtPhone.Focus();
            }
            else if (paymode.Equals("NONE"))
            {
                ShowMessage("Please Select Payment Mode, Cash or Cheque", true);
            }
            else
            {
                PhoneValidator pv = new PhoneValidator();
                if (pv.PhoneNumbersOk(phone))
                {
                    Responseobj res = new Responseobj();
                    Transaction t = new Transaction();
                    t.TranAmount = amount.Replace(",", "");
                    t.CustomerRef = custref;
                    t.CustomerType = custtype;
                    t.CustomerName = custname;
                    t.TranType = paymode;
                    t.PaymentType = payType;
                    t.CustomerTel = phone;
                    t.Reversal = "0";
                    t.Teller = teller;
                    t.VendorCode = vendorcode;
                    t.VendorTranId = agentref;
                    t.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");
                    res = bll.PostInternalPayment(t, sms);
                    if (res.Errorcode.Equals("0"))
                    {
                        //DisplayReceipt(res);
                        ShowMessage(res.Message, false);
                        ClearContrls();

                    }
                    else
                    {
                        ShowMessage(res.Message, true);
                    }
                }
                else
                {
                    ShowMessage("Please Enter a valid phone number", true);
                    txtPhone.Focus();
                }
            }
        }
        private void ClearContrls()
        {
            txtcode.Text = "";
            txtCustRef.Text = "";
            txtCustType.Text = "";
            txtname.Text = "";
            txtPhone.Text = "";
            txtAmount.Text = "";
            txtbal.Text = "";
            txtagentRef.Text = "";
            cboPayType.SelectedIndex = cboPayType.Items.IndexOf(cboPayType.Items.FindByValue("0"));
            cboVendor.SelectedIndex = cboVendor.Items.IndexOf(cboVendor.Items.FindByValue("0"));
            Button1.Enabled = false;
        }

        private void LoadReceipt(string receiptno, string vendorref)
        {
            Responseobj res = new Responseobj();
            res.VendorRef = vendorref;
            res.Receiptno = receiptno;
            dataTable = datapay.GetPaymentDetails(res);
            dataTable = formatTable(dataTable);
            string appPath, physicalPath, rptName;
            appPath = HttpContext.Current.Request.ApplicationPath;
            physicalPath = HttpContext.Current.Request.MapPath(appPath);

            rptName = physicalPath + "\\Bin\\Reports\\Receipt.rpt";

            Rptdoc.Load(rptName);
            Rptdoc.SetDataSource(dataTable);
            CrystalReportViewer1.ReportSource = Rptdoc;
            ///Rptdoc.PrintOptions.PrinterName("");
            Rptdoc.PrintOptions.PaperSize = PaperSize.PaperEnvelopeDL;
            Rptdoc.PrintToPrinter(1, true, 0, 0);
            Rptdoc.Dispose();
        }
        private DataTable formatTable(DataTable dataTable)
        {
            DataTable formedTable;
            string Header = "REFRESH PAYMENT RECEIPT";
            DataColumn myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "Title";
            dataTable.Columns.Add(myDataColumn);
            // Add data to the new columns

            dataTable.Rows[0]["Title"] = Header;
            formedTable = dataTable;
            return formedTable;
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                MultiView3.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = 0;
                txtcode.Text = "";
                txtCustRef.Text = "";
                txtname.Text = "";
                txtPhone.Text = "";
                txtbal.Text = "";
                txtAmount.Text = "";
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }
        protected void cboPayType_DataBound(object sender, EventArgs e)
        {
            cboPayType.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void rdcash_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }
        protected void rdcheque_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }
        protected void cboVendor_DataBound(object sender, EventArgs e)
        {
            cboVendor.Items.Insert(0, new ListItem("Select Agent", "0"));
        }
    }
}