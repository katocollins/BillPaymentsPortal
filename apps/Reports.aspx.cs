using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FullName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                string FullName = Session["FullName"].ToString();
                string Area = Session["AreaName"].ToString();
                string Branch = Session["DistrictName"].ToString();
                string Role = Session["RoleName"].ToString();
                string WelcomeMessage = "Welcome " + FullName;
                lblWelcome.Text = WelcomeMessage;

                lblUsage.Text = "Use the Buttons on your Left and Links above to Navigation System Activities and System forms respectively";

                Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                Button MenuReport = (Button)Master.FindControl("btnCalReports");
                Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                MenuTool.Font.Underline = false;
                MenuPayment.Font.Underline = false;
                MenuReport.Font.Underline = true;
                MenuRecon.Font.Underline = false;
                MenuAccount.Font.Underline = false;
                MenuBatching.Font.Underline = false;
            }
        }
    }

}