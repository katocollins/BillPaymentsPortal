using InterLinkClass.ControlObjects;
using InterLinkClass.EntityObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class AddEmail : System.Web.UI.Page
    {
        //SystemUsers dac = new SystemUsers();
        ProcessUsers Process = new ProcessUsers();
        DataLogin datafile = new DataLogin();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();

        //Vendor vendor = new Vendor();

        Merchant merchant = new Merchant();
        private HttpFileCollection uploads2 = HttpContext.Current.Request.Files;
        string vendor = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                vendor = Session["DistrictID"] as string;
                if (!Session["AreaID"].ToString().Equals("1"))
                {
                    MultiView1.ActiveViewIndex = -1;
                    ShowMessage("YOU DONOT HAVE RIGHTS TO VIEW THIS PAGE", true);
                }

                if (IsPostBack == false)
                {
                    bll.LoadVendors(vendor, ddVendor);
                    bll.LoadNotificationTypes(ddNotificationType);
                }


            }
            catch (Exception ex)
            {
                MultiView1.ActiveViewIndex = 0;
                ShowMessage(ex.Message, true);
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

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInputs();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        private void ValidateInputs()
        {
            string email = txtemail.Text.Trim();
            string vendor = ddVendor.SelectedValue;
            string NotificationType = ddNotificationType.SelectedValue.Trim();
            //if (vendor.Equals(""))
            //{
            //    //ShowMessage("Please Enter Utility Code", true);
            //    //ddVendor.Focus();
            //}
            //else 
            if (email.Equals(""))
            {
                ShowMessage("Please Enter Utility Email", true);
                txtemail.Focus();
            }
            else if (txtconfirmemail.Text.Equals(""))
            {
                ShowMessage("Please Confirm Email", true);
                txtconfirmemail.Focus();
            }
            else if (!email.Equals(txtconfirmemail.Text.Trim()))
            {
                ShowMessage("Please Emails Provided do not match", true);
            }
            else if (!bll.IsValidEmailAddress(email))
            {
                ShowMessage("Please Provide valid Emails ", true);
                txtemail.Focus();
            }
            else
            {
                string UserID = Session["USERID"].ToString();
                UtilityDetails EmailObj = new UtilityDetails();
                EmailObj.Email = email;
                EmailObj.UtilityCode = vendor;
                EmailObj.NotificationType = NotificationType;
                string ret = Process.SaveEmail(EmailObj, UserID);
                ShowMessage(ret, false);
                ClearControls();
            }

        }

        private void ClearControls()
        {
            //ddVendor.Text = "";
            txtemail.Text = "";
            txtconfirmemail.Text = "";
        }
    }
}