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
using InterLinkClass.EntityObjects;

public partial class AccountBatch : System.Web.UI.MasterPage
{
    ProcessUsers Usersdll = new ProcessUsers();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if ((Session["FullName"] == null))
            {
                Response.Redirect("Default.aspx");
            }
            lblUserId.Text = Session["FullName"].ToString();
            string AreaDesc = "";
            string Area = Session["AreaName"].ToString();
            string Branch = Session["DistrictName"].ToString();
            if (Branch.Equals("NONE"))
            {
                AreaDesc = Area;
            }
            else
            {
                AreaDesc = Area + " - " + Branch;
            }
            lblArea.Text = AreaDesc;
            lblRole.Text = Session["RoleName"].ToString();
        }
        catch (NullReferenceException exe)
        {
            Response.Redirect("Default.aspx?login=1", false);

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    private void Logout()
    {
        SystemUser user = new SystemUser();
        user.Action = "Logged-out";
        user.Uname = Session["UserName"].ToString();
        user.Userid = int.Parse(Session["UserID"].ToString());
        user.LoggedOn = false;
        Usersdll.LogActivity(user);
        Usersdll.LoginStatus(user);    
        Session["Accesslevel"] = "";
        Session["UserName"] = "";
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Logout();
    }
    protected void btnCallSystemTool_Click(object sender, EventArgs e)
    {       
        Response.Redirect("Admin.aspx");
    }
    protected void btnCallAccountDetails_Click(object sender, EventArgs e)
    {
        Response.Redirect("SystemPassword.aspx");
    }
    protected void btnCalRecon_Click(object sender, EventArgs e)
    {
        Response.Redirect("Accountant.aspx");
    }
    protected void btnCalReports_Click(object sender, EventArgs e)
    {
        Response.Redirect("Reports.aspx");
    }
    protected void btnCallPayments_Click(object sender, EventArgs e)
    {
        Response.Redirect("Payment.aspx");
    }
    protected void btnCallBatching_Click(object sender, EventArgs e)
    {
        Response.Redirect("Billing.aspx");
    }
}
