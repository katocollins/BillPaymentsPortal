using InterLinkClass.ControlObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class GenerateTestLicense : System.Web.UI.Page
    {
        BusinessLogin bll = new BusinessLogin();
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

            }
            catch (Exception ex)
            {
                bll.ShowMessage(lblmsg, ex.Message, true, Session);
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string vendor = TextBox1.Text.Trim();
                string expiry = txtFromDate.Text.Trim();

                string concat = vendor + "," + expiry;

            }
            catch (Exception ex)
            {
                string msg = "FAILED: " + ex.Message;
                bll.ShowMessage(lblmsg, msg, true, Session);
            }
        }
    }
}