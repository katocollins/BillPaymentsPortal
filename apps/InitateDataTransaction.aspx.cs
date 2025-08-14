using Excel;
using InterLinkClass.Epayment;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InitateDataTransaction : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadVendors();
            LoadVendorUtilities();
            ddl_bundleCode.Enabled = false;
        }

    }

    public void LoadDataBundles()
    {

    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        //string targetName = CustName.Text.Trim();
        string targetNo = TelNo.Text.Trim();
        string vendor = ddl_vendor.SelectedValue.Trim();
        string bundle = ddl_bundleCode.SelectedValue.Trim();
        string utility = ddl_utilityCode.SelectedValue.Trim();
        //string amount = bundleAmount.Text.Trim();
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
        dtable = datafile.GetAllVendorsUtilities(vendorCode, "DATA");
        if (dtable.Rows.Count == 0)
        {
            ShowMessage("This Vendor Is Has No Utility Data Credentials", true);
        }
        ddl_utilityCode.DataSource = dtable;
        ddl_utilityCode.DataValueField = "UtilityCode";
        ddl_utilityCode.DataTextField = "UtilityCode";
        ddl_utilityCode.DataBind();
        ddl_utilityCode.Enabled = false;
    }



    protected void btn_uploadClicK(object sender, EventArgs e)
    {
        try
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
                    if (dtable.Columns.Count < 3)
                    {
                        ShowMessage("System Expects the file to have BundleAmount, BundleCode, TelephoneNumber as the columns", true);
                        break;
                    }
                    //string CustName = drow["Column1"].ToString();
                    string tranAmount = drow["Column1"].ToString();
                    string bundleCode = drow["Column2"].ToString();
                    string phoneNumber = drow["Column3"].ToString();
                    string utilityCode = "DATA";
                    if (pv.PhoneNumbersOk(phoneNumber) && isNumeric(tranAmount))
                    {
                        phoneNumber = pv.FormatTelephone(phoneNumber);
                        if (utilityCode == "DATA" || utilityCode == "UMEME")
                        {
                            Customer cust = bll.GetDataBeneficiary(phoneNumber, phoneNumber, utilityCode, vendorCode);
                            if (cust.StatusCode.Equals("0"))
                            {
                                string customerName = cust.CustomerName;
                                string customerType = cust.CustomerType.ToString().ToUpper();
                                string vendorTranId = DateTime.Now.ToString("yyyyddMMHHmmssff");
                                Transaction t = new Transaction();
                                t.TranAmount = tranAmount.Replace(",", "");
                                t.CustomerRef = phoneNumber;
                                t.CustomerType = "";
                                //t.Area = bundleCode;
                                t.CustomerTel = phoneNumber;
                                t.CustomerName = customerName;
                                t.TranType = "";
                                //t.PaymentType = payType;
                                //t.CustomerTel = phone;
                                t.Teller = teller;
                                t.Reversal = "0";
                                t.PaymentType = utilityCode;
                                t.Teller = teller;
                                t.VendorCode = vendorCode;
                                t.StatusDescription = "DATA";
                                t.VendorTranId = vendorTranId;
                                t.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");
                                bool executed = datafile.LogAirtimePayment(phoneNumber, customerName, tranAmount, utilityCode, teller, vendorTranId, vendorCode);
                                if (executed)
                                {
                                    res = bll.PostPrepaidPayment1(t);
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
            else
            {
                ShowMessage("Error: Invalid File Format. (only XLS/XLSX accepted)", true);
            }
        }
        catch (Exception err)
        {
            ShowMessage("Error: " + err.Message, true);
        }

    }

    private string GetBundleAmount(string BundleCode, string Network)
    {
        string amount = "";
        try
        {
            DataLogin dl = new DataLogin();
            object[] param = { BundleCode, Network };
            dtable = dl.ExecuteDataSet("GetBundlesDetails", param).Tables[0];
            amount = dtable.Rows[0]["BundleAmount"].ToString();
            return amount;
        }
        catch (Exception er)
        {
            throw er;
        }
    }

    protected string GetTranId()
    {
        Random rd = new Random();
        int rand_num = rd.Next(100, 999);
        string date = DateTime.Now.ToString("yyMMddHHmmssfff");
        string vendorTran = date + rand_num.ToString();
        return vendorTran;
    }
    protected void ClearText()
    {
        TelNo.Text = "";
        //ddl_bundleCode.SelectedValue = "";
        ddl_bundleCode.SelectedIndex = -1;
    }
    protected void DoSingleActivation(object sender, EventArgs e)
    {
        PhoneValidator pv = new PhoneValidator();
        string bundlecode = ddl_bundleCode.SelectedValue.Trim();
        string telno = TelNo.Text.Trim();
        //string name = CustName.Text.Trim();
        string utility = ddl_utilityCode.SelectedValue.Trim();
        string vendor = ddl_vendor.SelectedValue.Trim();
        string vendorTranId = GetTranId();
        if (telno != "")
        {
            if (bundlecode != "")
            {
                if (pv.PhoneNumbersOk(telno) && isNumeric(telno))
                {
                    telno = pv.FormatTelephone(telno);
                    Customer cust = bll.GetDataBeneficiary(telno, telno, utility, vendor);
                    if (cust.StatusCode.Equals("0"))
                    {
                        Responseobj res = new Responseobj();
                        Transaction t = new Transaction();
                        bool okNumber = pv.PhoneNumbersOk(telno);
                        string network = pv.NetworkCode;
                        string tranAmount = GetBundleAmount(bundlecode, network);
                        t.TranAmount = tranAmount.Replace(",", "");
                        t.CustomerRef = telno;
                        t.CustomerType = "";
                        //t.Area = bundlecode;
                        t.CustomerTel = telno;
                        t.CustomerName = cust.CustomerName;
                        t.TranType = "";
                        //t.PaymentType = payType;
                        //t.CustomerTel = phone;
                        t.Teller = Session["Username"].ToString();
                        t.Reversal = "0";
                        t.PaymentType = utility;
                        t.Teller = Session["Username"].ToString();
                        t.VendorCode = vendor;
                        t.StatusDescription = utility;
                        t.VendorTranId = vendorTranId;
                        t.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");
                        bool executed = datafile.LogAirtimePayment(telno, cust.CustomerName, tranAmount, utility, Session["Username"].ToString(), vendorTranId, vendor);
                        if (executed)
                        {
                            res = bll.PostPrepaidPayment1(t);
                            if (res.Errorcode.Equals("0"))
                            {
                                ShowMessage("Data Activation Successful", false);
                                ClearText();
                            }
                            else if (res.Errorcode.Equals("100"))
                            {
                                ShowMessage("Data Activation Pending", false);
                                ClearText();
                            }
                            else
                            {
                                ShowMessage("Error: " + res.Message, true);
                            }

                        }

                    }
                    else
                    {
                        ShowMessage("Failed: " + cust.StatusDescription, true);
                    }
                }
                else
                {
                    ShowMessage("Error: Invalid Phone number", true);
                }
            }
            else
            {
                ShowMessage("Error: Select A Bundle", true);
            }
        }
        else
        {
            ShowMessage("Error: Provide A phone Number", true);
        }


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

    protected void Selection_Change(object sender, EventArgs e)
    {
        try
        {
            if (TelNo.Text.Trim() != "")
            {
                PhoneValidator pv = new PhoneValidator();
                string telno = TelNo.Text.Trim();
                string okPhoneNumber = pv.FormatTelephone(telno);
                bool okNumber = pv.PhoneNumbersOk(okPhoneNumber);
                string network = pv.NetworkCode;
                string duration = Duration.SelectedValue.Trim();
                getAvailableBundle(duration, network);
                ddl_bundleCode.Enabled = true;
            }
            else
            {
                ShowMessage("Provide Phone Number To get Available Bundles", true);
            }
        }
        catch (Exception err)
        {
            ShowMessage("Error :" + err.Message, true);
        }


    }
    private void getAvailableBundle(string duration, string network)
    {
        DataLogin dl = new DataLogin();
        object[] para = { duration, network };
        dtable = dl.ExecuteDataSet("GetAvailableNetworkBundles", duration, network).Tables[0];
        ddl_bundleCode.DataSource = dtable;
        ddl_bundleCode.DataValueField = "BundleCode";
        ddl_bundleCode.DataTextField = "BundleVolume";
        ddl_bundleCode.DataBind();
    }
}