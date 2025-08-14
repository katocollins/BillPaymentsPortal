using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InterLinkClass.Epayment;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
public partial class MakePayment : System.Web.UI.Page
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
                
                Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                Button MenuReport = (Button)Master.FindControl("btnCalReports");
                Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                MenuTool.Font.Underline = false;
                MenuPayment.Font.Underline = true;
                MenuReport.Font.Underline = false;
                MenuRecon.Font.Underline = false;
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

    private void ToggleToken()
    {
        if (bll.IsvalidToken())
        {
            MultiView5.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = 0;
            MultiView3.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = -1;
            rdcash.Checked = true;
            Button1.Enabled = false;
            Button1.Visible = true;
            txtofficer.Text = Session["FullName"].ToString();
            txtCustRef.Focus();
            LoadPayTypes();
        }
        else
        {
            MultiView5.ActiveViewIndex = 1;
            DateTime now = DateTime.Now;
            Label1.Text = "No Active Transaction Token in the System for " + now.ToString("dd-MM-yyyy");
            Button1.Visible = false;
        }
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
                txtAmount.Text = "";              
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
        string accountno = txtaccountno.Text.Trim();
        string chequeno = txtchequeNo.Text.Trim();        
        string chequeDate = txtDate.Text.Trim();
        string paymode = GetPayTypeSelected();
        string bank = GetBank(paymode);
        string payType = cboPayType.SelectedValue.ToString();
        string teller = Session["Username"].ToString();
        bool sms = chkSendSms.Checked;
        if (custref.Equals(""))
        {
            ShowMessage("Customer Ref is Required", true);
        }
        else if (custname.Equals(""))
        {
            ShowMessage("Customer Name is Required", true);
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
        else if (paymode.Equals("CHEQUE") && chequeno.Equals(""))
        {
            ShowMessage("Please Enter Cheque Number", true);
            txtchequeNo.Focus();
        }
        else if (paymode.Equals("CHEQUE") && accountno.Equals("")) 
        {
            ShowMessage("Please Enter Cheque Account Number", true);
            txtaccountno.Focus();
        }
        else if (paymode.Equals("CHEQUE") && bank.Equals("")) 
        {
            ShowMessage("Please Select Cheque Bank Name", true);            
        }
        else if (paymode.Equals("CHEQUE") && chequeDate.Equals(""))
        {
            ShowMessage("Please Enter Cheque Payment Date", true);
            txtDate.Focus(); 
        }
        else
        {
            ShowMessage(".", true);
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
            t.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");
            t.Offline = "0";
            res = bll.PostPayment(t, bal.ToString(), "", false, sms);
            if (res.Errorcode.Equals("0"))
            {
                //DisplayReceipt(res);     
                string receiptno = res.Receiptno;
                string vendorref = res.VendorRef;
                // Save Cheque Details
                if (paymode.Equals("CHEQUE"))
                {
                    string res_cheque = Process.SaveChequeDetails(vendorref, chequeno, accountno, bank, chequeDate);
                }
                //LoadReceipt(receiptno, vendorref);
                ClearContrls();
                //ShowMessage("Payment Posted Successfully. Receipt Number " + receiptno + " Sent to Printer", false);
                Session["frompage"] = "MakePayment.aspx";
                Response.Redirect("./Receipt.aspx?transfereid=" + receiptno + "&transferecode=" + vendorref, false);
            }
            else
            {
                ShowMessage(res.Message, true);
            }
        }
    }

    private string GetBank(string paymeode)
    {
        string ret = "";
        if (!paymeode.Equals("CASH"))
        {
            ret = cboBanks.SelectedItem.ToString();
        }
        return ret;
    }

    private void ClearContrls()
    {
        txtaccountno.Text = "";
        txtcode.Text = "";
        txtCustRef.Text = "";
        txtCustType.Text = "";
        txtDate.Text = "";
        txtname.Text = "";
        txtPhone.Text = "";
        txtAmount.Text = "";
        txtbal.Text = "";
        txtchequeNo.Text = "";
        cboBanks.SelectedIndex = cboBanks.Items.IndexOf(cboBanks.Items.FindByValue("0"));
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
        Rptdoc.PrintToPrinter(1,true, 0,0);
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
            MultiView1.ActiveViewIndex = -1;

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
            MultiView1.ActiveViewIndex = 0;
            LoadBanks();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadBanks()
    {
        dtable = datafile.GetActiveBanks();
        cboBanks.DataSource = dtable;
        cboBanks.DataValueField = "RecordID";
        cboBanks.DataTextField = "BankName";
        cboBanks.DataBind();
    }
    protected void cboBanks_DataBound(object sender, EventArgs e)
    {
        cboBanks.Items.Insert(0, new ListItem("Select", "0"));
    }
}
