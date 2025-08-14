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
using System.Messaging;

public partial class XtraFloatWhiteList : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();
    public string smsQueuepath = "";
    private MessageQueue smsqueue;
    //private Message smsmsg;
    //
    string username = "";
    string fullname = "";
    string vendor = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            username = Session["UserName"] as string;
            fullname = Session["FullName"] as string;
            vendor = Session["DistrictID"] as string;

            Session["IsError"] = null;

            //Session is invalid
            if (username == null)
            {
                Response.Redirect("Default.aspx");
            }
            else if (IsPostBack)
            {

            }
            else
            {
               // LoadData();
                multiview1.ActiveViewIndex = 0;
            }
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SearchDb();
    }

    public void SearchDb()
    {
        try
        {
            string BankCode = "MTN";
            string CustomerID = txtAccountNumber.Text;
            string[] parameters = { CustomerID, BankCode };
            if (string.IsNullOrEmpty(CustomerID))
            {
                bll.ShowMessage(lblmsg, "Provide a phone number", false, Session);
            }
            else
            {
                DataTable dt = bll.GetData("WhiteListedPhoneNumbers_SelectRow", parameters);
                if (dt.Rows.Count > 0)
                {
                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();
                    // ResultsMultiView.ActiveViewIndex = 0;
                    // dataGridResults.Columns[5].Visible = false;
                    string msg = "SUCCESS:" + dt.Rows.Count + " Customer(s) Found!";
                    bll.ShowMessage(lblmsg, msg, false, Session);
                }
                else
                {
                    dataGridResults.DataSource = null;
                    dataGridResults.DataBind();
                    //ResultsMultiView.ActiveViewIndex = -1;
                    string msg = "FAILED: NO CUSTOMER FOUND BASED ON SEARCH PARAMETERS!";
                    bll.ShowMessage(lblmsg, msg, true, Session);
                }
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}
