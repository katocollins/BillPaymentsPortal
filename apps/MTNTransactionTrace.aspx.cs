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
using System.Collections.Generic;

public partial class MTNTransactionTrace : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    string loggedinUser = "";
    string username = "";
    string fullname = "";
    string userBranch = "";
    private string CorrectCustomerRef;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            username = Session["UserName"] as string;
            fullname = Session["FullName"] as string;
            userBranch = Session["UserBranch"] as string;
            loggedinUser = Session["Username"].ToString();
            if (IsPostBack == false)
            {
                //MultiView2.ActiveViewIndex = 0;
                LoadUtilities();
                if (Session["AreaID"].ToString().Equals("2"))
                {
                    cboUtility.SelectedIndex = cboUtility.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                    cboUtility.Enabled = false;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }

    }

    private void LoadUtilities()
    {
        dtable = datafile.GetAllUtilities("0");
        cboUtility.DataSource = dtable;
        cboUtility.DataValueField = "UtilityCode";
        cboUtility.DataTextField = "Utility";
        cboUtility.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string VendorTranID = txtVendorTranID.Text;
            string startDate = txtfromDate.Text;
            string endDate = txttoDate.Text;
            string UtilityCode = cboUtility.SelectedItem.Text.ToString();
            //CorrectCustomerRef = txtCustomerRef.Text.Trim();
            if (string.IsNullOrEmpty(startDate))
            {
                ShowMessage("Please enter the start date", true);
            }
            else //if (!string.IsNullOrEmpty(referenceId))
            {
                //DataTable table = datafile.GetPendingVasTransaction(referenceId, phoneNumber, startDate, endDate);
                DataTable table = datapay.GetMTNTrace(VendorTranID, DateTime.Parse(startDate), UtilityCode);
                if (table.Rows.Count > 0)
                {
                   // MultiView2.ActiveViewIndex = 0;
                    DataGrid1.DataSource = table;
                    DataGrid1.DataBind();
                }
                else
                {
                    ShowMessage("Record NOT found", true);
                }
            }
            //else
            //{
            //    MultiView2.ActiveViewIndex = -1;
            //    ShowMessage("Please provide a reference id", true);
            //}
        }
        catch (Exception ee)
        {

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
    protected void btn_Success_Click(object sender, EventArgs e)
    {
    }

    protected void btn_Proceed_Click(object sender, EventArgs e)
    {

    }
}
