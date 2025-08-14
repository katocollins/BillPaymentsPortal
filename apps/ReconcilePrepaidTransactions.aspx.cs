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
    //DataRow dr = new DataRow();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    DataFileProcess dfile = new DataFileProcess();
    private ArrayList fileContents;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                LoadData();
                
                ToggleVendor();
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
            }
        }
        catch (Exception ex)
        {
            
             ShowMessage(ex.Message, true);
        }

    }

    private void LoadData()
    {
        LoadVendors();
        bll.LoadHours(ddFromHour);
        bll.LoadHours(ddToHour);
    }

    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        ///Button1.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button1, "").ToString());
        //Button2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button2, "").ToString());
    }

    private void LoadVendors()
    {
        dtable = datafile.GetAllPrepaidVendors();
        cboPrepaidVendor.DataSource = dtable;
        cboPrepaidVendor.DataValueField = "PrepaidVendors";
        cboPrepaidVendor.DataTextField = "PrepaidVendors";
        cboPrepaidVendor.DataBind();
    }

    private void ToggleVendor()
    {
        string districtcode = Session["DistrictCode"].ToString();
        string role = Session["RoleCode"].ToString();
        if (role.Equals("005"))
        {
            cboPrepaidVendor.Enabled = false;
            cboPrepaidVendor.SelectedIndex = cboPrepaidVendor.Items.IndexOf(cboPrepaidVendor.Items.FindByValue(districtcode));
        }
        else
        {
            cboPrepaidVendor.Enabled = true;
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            string vendorcode = cboPrepaidVendor.SelectedValue.ToString();
            if (vendorcode.Equals("0"))
            {
                ShowMessage("Please select a vendor for Reconciliation", true);
            }
            else if (FileUpload1.FileName.Trim().Equals(""))
            {
                ShowMessage("Please upload a file to reconcile", true);
            }
            else
            {
                ReadFileToRecon(vendorcode);
            }
        }
        catch (Exception ex)
        {

            ShowMessage(ex.Message,true);
        }
    }

    private void ReadFileToRecon(string vendorcode)
    {
        string fromDate = DateTime.Parse(txtFromDate.Text).ToString();
        string toDate = DateTime.Parse(txtToDate.Text).AddDays(1).AddSeconds(-1).ToString();

        string filename = Path.GetFileName(FileUpload1.FileName);
        string extension = Path.GetExtension(filename);

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


        string filePath = bll.ReconPrepaidFilePath(vendorcode, filename);
        FileUpload1.SaveAs(filePath);

        bool valid = bll.ValidateUploadedFile(filePath);
        if (valid)
        {
            string[] allLines = File.ReadAllLines(filePath);
            int count = 0, fileNumber = 1, interval = 100;
            string Intvalue = datafile.GetSystemParameter(11, 12);
            Int32.TryParse(Intvalue, out interval);

            string sessionEmail = Session["UserEmail"].ToString();
            string name = Session["FullName"].ToString();
            string user = Session["Username"].ToString();
            if (allLines.Length > interval)
            {
                List<string> newDataFile = new List<string>();

                foreach (string str in allLines)
                {
                    newDataFile.Add(str);
                    if (count >= interval)
                    {
                        string named2 = Path.GetFileNameWithoutExtension(filename);
                        string filePath2 = bll.ReconPrepaidFilePath(vendorcode, named2 + "_" + fileNumber + "" + extension);
                        File.WriteAllLines(filePath2, newDataFile.ToArray());
                        datafile.SaveUploadedPrepaidVendorReconFileDetail(filePath2, user, sessionEmail, 0, vendorcode, fromDate, toDate);
                        newDataFile.Clear();
                        count = 0;
                        fileNumber++;
                    }
                    count++;
                }
            }
            else
            {
                datafile.SaveUploadedPrepaidVendorReconFileDetail(filePath, user, sessionEmail, 0, vendorcode, fromDate, toDate);
            }

            ShowMessage("Hello\t" + name + "\t\tPrepaid Transactions File has been Uploaded Successfully Reconciliation has started and the report will be sent to your Email Shortly thank you.", false);

        }
        else
        {
            ShowMessage("CSV FILE UPLOADED HAS INVALID COLUMN(S)...File should contain only 3 columns VendortranId,Amount and payment date", true);
        }
    }
    protected void cboPrepaidVendor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cboPrepaidVendor_DataBound(object sender, EventArgs e)
    {
        cboPrepaidVendor.Items.Insert(0, new ListItem("select", "0"));
        cboPrepaidVendor.Items.Insert(1, new ListItem("All Vendors", "All Vendors"));
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
}
