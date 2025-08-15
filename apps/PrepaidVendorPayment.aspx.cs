using InterLinkClass.ControlObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace apps
{
    public partial class PrepaidVendorPayment : System.Web.UI.Page
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
                string companyCode = "";
                if (IsPostBack == false)
                {

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
                    LoadVendorUtilities();
                    LoadVendors();
                    //ToggleToken();
                    MultiView5.ActiveViewIndex = 0;
                    MultiView2.ActiveViewIndex = 0;
                    Button1.Visible = false;
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
            ddl_vendor.DataSource = dtable;
            ddl_vendor.DataValueField = "VendorCode";
            ddl_vendor.DataTextField = "Vendor";
            ddl_vendor.DataBind();
            string vendorcode = Session["DistrictCode"].ToString();// Session["DistrictCode"].ToString();
            ddl_vendor.SelectedValue = vendorcode;
            ddl_vendor.Enabled = false;
        }
        private void LoadVendorUtilities()
        {
            string vendorCode = Session["DistrictCode"].ToString();
            dtable = datafile.GetAllVendorsUtilities(vendorCode);


            ddl_utilityCode.DataSource = dtable;
            ddl_utilityCode.DataValueField = "UtilityCode";
            ddl_utilityCode.DataTextField = "UtilityCode";
            ddl_utilityCode.DataBind();
        }
        //private void ToggleToken()
        //{
        //    MultiView5.ActiveViewIndex = 0;
        //    MultiView2.ActiveViewIndex = 0;
        //    MultiView3.ActiveViewIndex = 0;
        //    rdcash.Checked = true;
        //    Button1.Enabled = false;
        //    Button1.Visible = true;
        //    txtCustRef.Focus();
        //    LoadPayTypes();
        //}

        //private void LoadPayTypes()
        //{
        //    dtable = datafile.GetPayTypes();
        //    cboPayType.DataSource = dtable;
        //    cboPayType.DataValueField = "PaymentCode";
        //    cboPayType.DataTextField = "PaymentType";
        //    cboPayType.DataBind();
        //}
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
            string utilityCode = ddl_utilityCode.SelectedValue.ToString();
            string area = txt_area.Text;
            string vendorCode = Session["DistrictCode"].ToString();
            if (custref.Equals(""))
            {
                ShowMessage("Please Enter Customer Reference Number", true);
                Button1.Enabled = false;
                txtCustRef.Focus();
            }
            else if (string.IsNullOrEmpty(utilityCode) || utilityCode == "0")
            {
                ShowMessage("Please Select A Utility", true);
                ddl_utilityCode.Focus();
                Button1.Enabled = false;
            }
            else
            {
                Customer cust = new Customer();

                if (utilityCode.ToUpper() == "AIRTIME" || utilityCode.ToUpper() == "DATA")
                {
                    PhoneValidator pv = new PhoneValidator();

                    if (pv.PhoneNumbersOk(custref))
                    {
                        custref = pv.FormatTelephone(custref);

                    }
                    area = custref;
                    cust = bll.GetAirtimeBeneficiary(custref, area, utilityCode, vendorCode);
                }
                else
                {
                    cust = bll.GetCustomerDetails(custref, area, utilityCode, vendorCode);
                }


                if (cust.StatusCode.Equals("0"))
                {
                    MultiView3.ActiveViewIndex = 0;
                    txtcode.Text = cust.CustomerRef;
                    txtname.Text = cust.CustomerName;
                    txtbal.Text = cust.Balance.ToString("#,##0");
                    txtCustType.Text = cust.CustomerType.ToString().ToUpper();

                    //txtagentRef.Text = "";
                    lbl_utilityCode.Text = ddl_utilityCode.SelectedValue.ToString();
                    txtAmount.Text = "";
                    //cboVendor.SelectedIndex = cboVendor.Items.IndexOf(cboVendor.Items.FindByValue("0"));
                    //cboPayType.SelectedIndex = cboPayType.Items.IndexOf(cboPayType.Items.FindByValue("0"));
                    Button1.Enabled = true;
                    Button1.Visible = true;
                }
                else
                {
                    ShowMessage(cust.StatusDescription, true);
                    if (cust.StatusCode == "101")
                    {
                        txtcode.Text = cust.CustomerRef;
                        txtname.Text = cust.CustomerName;
                    }
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
            //if (rdcash.Checked.Equals(true)) 
            //{
            //    ret = "CASH";
            //}
            //else if (rdcheque.Checked.Equals(true)) 
            //{
            //    ret = "CHEQUE";
            //}
            //else
            //{
            //    ret = "NONE";
            //}
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
            string utilityCode = lbl_utilityCode.Text;
            string narration = txtreason.Text;
            //string payType = cboPayType.SelectedValue.ToString();
            string teller = Session["Username"].ToString();
            string vendorcode = Session["DistrictCode"].ToString(); //cboVendor.SelectedValue.ToString();
                                                                    //string agentref = txtagentRef.Text.Trim();
                                                                    //bool sms = chkSendSms.Checked;
            if (custref.Equals(""))
            {
                ShowMessage("Customer Ref is Required", true);
            }
            else if (custname.Equals(""))
            {
                ShowMessage("Customer Name is Required", true);
            }
            //else if (vendorcode.Equals("0"))
            //{
            //    ShowMessage("Please Select Agent", true);
            //}
            //else if (agentref.Equals(""))
            //{
            //    ShowMessage("Agent Ref is Required", true);
            //    txtagentRef.Focus();
            //}
            //else if (payType.Equals("0"))
            //{
            //    ShowMessage("Please Select Payment Type", true);
            //}
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
            //else if (phone.Equals("") && sms)
            //{
            //    ShowMessage("Please Provide Phone Number or Uncheck Send SMS", true);
            //    txtPhone.Focus();
            //}
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
                    //t.PaymentType = payType;
                    //t.CustomerTel = phone;
                    t.Teller = teller;
                    t.Reversal = "0";
                    t.PaymentType = utilityCode;
                    t.Teller = teller;
                    t.VendorCode = vendorcode;
                    t.StatusDescription = narration;
                    t.VendorTranId = DateTime.Now.ToString("yyyyMMddHHmmss");
                    t.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");

                    bool executed = datafile.LogAirtimePayment(t.CustomerRef, t.CustomerName, t.TranAmount, utilityCode, teller, t.VendorTranId, t.VendorCode);
                    if (executed)
                    {
                        res = bll.PostPrepaidPayment(t);
                        if (res.Errorcode.Equals("0"))
                        {
                            ShowMessage(res.Message, false);
                            ClearContrls();

                        }
                        else
                        {
                            ShowMessage(res.Message, true);
                        }
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
            //txtagentRef.Text = "";
            //cboPayType.SelectedIndex = cboPayType.Items.IndexOf(cboPayType.Items.FindByValue("0"));
            //cboVendor.SelectedIndex = cboVendor.Items.IndexOf(cboVendor.Items.FindByValue("0"));
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
            //cboPayType.Items.Insert(0, new ListItem("Select", "0"));
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
            //cboVendor.Items.Insert(0, new ListItem("Select Agent", "0"));
        }
        protected void btn_BulkPaymentClicK(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 1;
        }
        protected void btn_uploadClicK(object sender, EventArgs e)
        {
            Responseobj res = new Responseobj();
            string fname = FileUpload1.FileName;
            if (IsExtensionOk(fname))
            {
                string filepath = UploadFile();
                DataTable dtable = ReadXmlData(filepath);
                PhoneValidator pv = new PhoneValidator();
                string vendorCode = ddl_vendor.SelectedValue;
                string teller = Session["Username"].ToString();
                int success = 0, failed = 0;
                foreach (DataRow drow in dtable.Rows)
                {
                    if (dtable.Columns.Count < 2)
                    {
                        ShowMessage("System Expects the file to have TelephoneNumber, Amount, Utility(AIrtime/Data) as the columns", true);
                        break;
                    }
                    string phoneNumber = drow["Column1"].ToString();
                    string tranAmount = drow["Column2"].ToString();
                    string utilityCode = drow["Column3"].ToString();
                    if (pv.PhoneNumbersOk(phoneNumber) && isNumeric(tranAmount))
                    {
                        phoneNumber = pv.FormatTelephone(phoneNumber);
                        if (utilityCode == "AIRTIME" || utilityCode == "UMEME")
                        {
                            Customer cust = bll.GetAirtimeBeneficiary(phoneNumber, phoneNumber, utilityCode, vendorCode);
                            if (cust.StatusCode.Equals("0"))
                            {
                                success++;
                                string customerName = cust.CustomerName;
                                string customerType = cust.CustomerType.ToString().ToUpper();
                                string vendorTranId = DateTime.Now.ToString("yyyyddMMHHmmssff");

                                Transaction t = new Transaction();
                                t.TranAmount = tranAmount.Replace(",", "");
                                t.CustomerRef = phoneNumber;
                                t.CustomerType = "";
                                t.CustomerName = customerName;
                                t.TranType = "";
                                //t.PaymentType = payType;
                                //t.CustomerTel = phone;
                                t.Teller = teller;
                                t.Reversal = "0";
                                t.PaymentType = utilityCode;
                                t.Teller = teller;
                                t.VendorCode = vendorCode;
                                t.StatusDescription = utilityCode;
                                t.VendorTranId = vendorTranId;
                                t.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");

                                bool executed = datafile.LogAirtimePayment(phoneNumber, customerName, tranAmount, utilityCode, teller, vendorTranId, vendorCode);
                                if (executed)
                                {
                                    res = bll.PostPrepaidPayment(t);
                                    if (res.Errorcode.Equals("0"))
                                    {
                                        success++;

                                    }
                                    else
                                    {
                                        failed++;
                                    }
                                }

                            }
                            else
                            {
                                failed++;
                            }
                        }
                        else
                        {
                            failed++;
                            continue;
                        }
                    }
                    else
                    {
                        failed++;
                    }
                }

                ShowMessage(success + " Transactions were sent for approval," + failed + " Transactions failed validation", true);
            }
        }
        public bool IsExtensionOk(string filename)
        {
            string extension = Path.GetExtension(filename);
            extension = extension.ToUpper();
            if (!(extension.Equals(".XLS") || extension.Equals(".XLSX")))
            {
                return false;
            }
            return true;
        }

        private bool isNumeric(string word)
        {
            bool isValid = true;
            char[] characters = word.ToCharArray();
            foreach (char c in characters)
            {
                if (char.IsDigit(c))
                {
                    isValid = false;
                    break;
                }
            }
            return IsValid;
        }
        public DataTable ReadXmlData(string path)
        {

            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);


            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            IExcelDataReader excelReader = null;
            if (Path.GetExtension(path).ToUpper().Equals(".XLS"))
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else
            {
                //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            //...
            //3. DataSet - The result of each spreadsheet will be created in the result.Tables
            DataTable result = excelReader.AsDataSet().Tables[0];
            return result;
        }
        public string UploadFile()
        {
            string FilePath = @"C:\AirtimeUploads\";

            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);
            string filePath = FilePath + DateTime.Now.ToString("dd-mm-yyyy_HH-mm-ss") + "_" + FileUpload1.FileName;
            FileUpload1.SaveAs(filePath);
            return filePath;


        }
        protected void btn_SingleClicK(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 2;
        }
    }
}