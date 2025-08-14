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
using System.IO;
using System.Collections.Generic;

public partial class ReconcilePrepaidTransactions : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    DataFileProcess dfile = new DataFileProcess();
    private ArrayList fileContents;

    InterLinkClass.CoreMerchantAPI.Service Merchant = new InterLinkClass.CoreMerchantAPI.Service();
        

    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (IsPostBack == false)
            {
                LoadData();
            }
        }
        catch (Exception ex)
        {
            
             ShowMessage(ex.Message, true);
        }

    }

    private void LoadData()
    {
        LoadBankCodes();
        LoadVendors();
        LoadCurrencies();
        bll.LoadHours(ddFromHour);
        bll.LoadHours(ddToHour);
    }

    private void LoadVendors()
    {   string []SearchParams = { };
        //dtable = bll.ExecuteDataAccess("LiveMerchantCoreDB", "GetMerchantCommissionAccounts").Tables[0];
        dtable = Merchant.ExecuteDataSet("GetMerchantCommissionAccounts", SearchParams).Tables[0];
        cboPrepaidVendor.DataSource = dtable;
        cboPrepaidVendor.DataValueField = "CommissionAccountCode";
        cboPrepaidVendor.DataTextField = "CommissionAccountName";
        cboPrepaidVendor.DataBind();
    }

    private void LoadVendors(string currency)
    {
        string[] SearchParams = { currency };
        //dtable = bll.ExecuteDataAccess("LiveMerchantCoreDB", "GetMerchantCommissionAccounts").Tables[0];
        dtable = Merchant.ExecuteDataSet("GetMerchantCommissionAccountsMultiCurrency", SearchParams).Tables[0];
        cboPrepaidVendor.DataSource = dtable;
        cboPrepaidVendor.DataValueField = "CommissionAccountCode";
        cboPrepaidVendor.DataTextField = "CommissionAccountName";
        cboPrepaidVendor.DataBind();
    }

    private void LoadCurrencies()
    {
        string[] SearchParams = { };
        //dtable = bll.ExecuteDataAccess("LiveMerchantCoreDB", "GetMerchantCommissionAccounts").Tables[0];
        dtable = Merchant.ExecuteDataSet("GetAllCurrencies", SearchParams).Tables[0];
        ddCurrency.DataSource = dtable;
        ddCurrency.DataValueField = "CurrencyCode";
        ddCurrency.DataTextField = "CurrencyCode";
        ddCurrency.DataBind();
    }

    private void LoadBankCodes()
    {
        string[] SearchParams = { };
        //dtable = bll.ExecuteDataAccess("LiveMerchantCoreDB", "GetMerchantCommissionAccounts").Tables[0];
        dtable = Merchant.ExecuteDataSet("GetAllBanks", SearchParams).Tables[0];
        ddBankCode.DataSource = dtable;
        ddBankCode.DataValueField = "BankCode";
        ddBankCode.DataTextField = "BankName";
        ddBankCode.DataBind();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            string vendorcode = cboPrepaidVendor.SelectedValue.ToString();
            string currency = ddCurrency.SelectedValue.ToString();
            if (vendorcode.Equals("0"))
            {
                ShowMessage("Please select an account Type for Reconciliation", true);
            }
            if (currency.Equals("0"))
            {
                ShowMessage("Please select appropriate currency", true);
            }
            else if (FileUpload1.FileName.Trim().Equals(""))
            {
                ShowMessage("Please upload a file to reconcile", true);
            }
            else
            {
                ReadFileToRecon(vendorcode, currency);
            }
        }
        catch (Exception ex)
        {

            ShowMessage(ex.Message,true);
        }
    }

    private void ReadFileToRecon(string vendorcode, string currency)
    {
        string fromDate = DateTime.Parse(txtFromDate.Text).ToString();
        string toDate = DateTime.Parse(txtToDate.Text).AddDays(1).AddSeconds(-1).ToString();

        string bankCode = ddBankCode.SelectedValue;
        string pegpayaccount = ddBankAccount.SelectedValue;
        string bankaccount = ddBankAccount.SelectedItem.Text;

        string filename = Path.GetFileName(FileUpload1.FileName);
        string extension = Path.GetExtension(filename);

        if (String.IsNullOrEmpty(currency))
        {
            ShowMessage("Please Select a currency", true);
            return;
        }
        if (String.IsNullOrEmpty(bankCode))
        {
            ShowMessage("Please Select a Bank", true);
            return;
        }
        if (String.IsNullOrEmpty(pegpayaccount))
        {
            ShowMessage("Please Select an Account", true);
            return;
        }

        if (String.IsNullOrEmpty(fromDate))
        {
            ShowMessage("Please Select a From Date", true);
            return;
        }

        if (String.IsNullOrEmpty(toDate))
        {
            ShowMessage("Please Select a To Date", true);
            return;
        }

        if (!(extension.ToUpper().Equals(".CSV") || extension.ToUpper().Equals(".TXT")))
        {
            ShowMessage("Please upload a CSV File, " + extension + " file not supported", true);
            return;
        }

        fromDate = bll.GetDate(fromDate, ddFromHour.SelectedValue, false);
        toDate = bll.GetDate(toDate, ddToHour.SelectedValue, true);


        string filePath = bll.MerchantFilePath(vendorcode, filename, currency);
        FileUpload1.SaveAs(filePath);

        bool valid = bll.ValidateUploadedFile(filePath);
        if (valid)
        {

            string sessionEmail = Session["UserEmail"].ToString();
            string name = Session["FullName"].ToString();
            string user = Session["Username"].ToString();

                string[] SearchParams = { filePath, user, sessionEmail, "0", vendorcode, fromDate, toDate, currency,bankCode,pegpayaccount,bankaccount };
                Merchant.ExecuteDataSet("ReconciliationFiles_Insert_2", SearchParams);


            ShowMessage("Hello\t" + name + "\tThe Merchant Transactions File has been Uploaded Successfully; Reconciliation has started and the report will be sent to your Email Shortly. Thank you.", false);

        }
        else
        {
            ShowMessage("FILE UPLOADED HAS INVALID FIRST COLUMN...File should contain only 3 columns VendortranId,Amount and payment date", true);
        }
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

    protected void ddCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadVendors(ddCurrency.SelectedValue.ToString());
    }

    protected void ddType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBankAccounts(ddBankCode.SelectedValue, cboPrepaidVendor.SelectedValue, ddCurrency.SelectedValue);
    }

    private void LoadBankAccounts(string bankCode, string type, string currency)
    {
        string[] SearchParams = { bankCode, type, currency };
        //dtable = bll.ExecuteDataAccess("LiveMerchantCoreDB", "GetMerchantCommissionAccounts").Tables[0];
        dtable = Merchant.ExecuteDataSet("GetBankAccounts", SearchParams).Tables[0];
        ddBankAccount.DataSource = dtable;
        ddBankAccount.DataValueField = "PegPayAccountNumber"; //"BankAccountNumber";
        ddBankAccount.DataTextField = "BankAccountNumber"; //"PegPayAccountNumber";
        ddBankAccount.DataBind();
    }
}
