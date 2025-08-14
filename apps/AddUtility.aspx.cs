using InterLinkClass.ControlObjects;
using InterLinkClass.CoreMerchantAPI;
using InterLinkClass.EntityObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace apps
{
    public partial class AddUtility : System.Web.UI.Page
    {
        //SystemUsers dac = new SystemUsers();
        ProcessUsers Process = new ProcessUsers();
        DataLogin datafile = new DataLogin();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();

        Vendor vendor = new Vendor();
        string userBranch;

        Merchant merchant = new Merchant();
        private HttpFileCollection uploads2 = HttpContext.Current.Request.Files;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userBranch = Session["UserBranch"] as string;
                if (!Session["AreaID"].ToString().Equals("1"))
                {
                    MultiView1.ActiveViewIndex = -1;
                    ShowMessage("YOU DONOT HAVE RIGHTS TO VIEW THIS PAGE", true);
                }
                if (IsPostBack == false)
                {
                    SystemReport report = bll.GetReportById("UTILITIES", userBranch);

                    bll.LoadUtilities(ddUtilityType, report);
                    chkIsActive.Checked = true;
                    MultiView1.ActiveViewIndex = 0;
                    txtUser.Text = Session["FullName"].ToString();

                    string strProcessScript = "this.value='Working...';this.disabled=true;";
                    btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
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

        protected void btnReturn_Click(object sender, EventArgs e)
        {

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
            if (txtCode.Text.Equals(""))
            {
                ShowMessage("Please Enter Utility Code", true);
                txtCode.Focus();
            }
            else if (txtName.Text.Equals(""))
            {
                ShowMessage("Please Enter Utility Name", true);
                txtName.Focus();
            }
            else if (txtemail.Equals(""))
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
                UtilityDetails utility = new UtilityDetails();
                utility.Active = chkIsActive.Checked;
                utility.CreationDate = DateTime.Now;
                utility.Email = email;
                utility.Utility = txtName.Text.Trim();
                utility.UtilityCode = txtCode.Text.Trim();
                utility.UtilityContact = txtcontact.Text.Trim();
                utility.UtilityCategory = ddUtilityType.SelectedValue;
                string ret = Process.SaveUtility(utility);
                ShowMessage(ret, false);
                ClearControls();
            }

        }

        private void ClearControls()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtcontact.Text = "";
            txtcontact.Text = "";
            txtemail.Text = "";
            ddUtilityType.SelectedIndex = 0;
            chkIsActive.Checked = false;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {

        }

        protected void btnNo_Click(object sender, EventArgs e)
        {

        }
    }
}