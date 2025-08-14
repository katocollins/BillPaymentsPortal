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

namespace apps
{
    public partial class AddUser : System.Web.UI.Page
    {
        //SystemUsers dac = new SystemUsers();
        ProcessUsers Process = new ProcessUsers();
        DataLogin datafile = new DataLogin();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();
        private HttpFileCollection uploads2 = HttpContext.Current.Request.Files;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    LoadRoles();
                    LoadAreas();
                    LoadNotificationTypes();
                    if (!Session["AreaID"].ToString().Equals("1"))
                    {
                        cboAreas.SelectedIndex = cboAreas.Items.IndexOf(new ListItem(Session["AreaCode"].ToString(), Session["AreaID"].ToString()));
                        int AreaID = Convert.ToInt16(cboAreas.SelectedValue.ToString());
                        LoadBranches(AreaID);
                        cboBranches.SelectedIndex = cboBranches.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                        cboAreas.Enabled = false;
                        cboBranches.Enabled = false;
                    }
                    else
                    {
                        int AreaID = Convert.ToInt16(cboAreas.SelectedValue.ToString());
                        LoadBranches(AreaID);
                        cboAreas.Enabled = true;
                        cboBranches.Enabled = true;
                    }
                    chkResetPassword.Enabled = false;
                    chkIsLoggedon.Enabled = false;
                    chkIsLoggedon.Checked = false;
                    chkIsActive.Checked = true;
                    if (Request.QueryString["transferid"] != null)
                    {
                        string UserCode = Request.QueryString["transferid"].ToString();
                        LoadControls(UserCode);
                    }

                    MultiView1.ActiveViewIndex = 0;
                    MultiView2.ActiveViewIndex = -1;
                    Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                    Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                    Button MenuReport = (Button)Master.FindControl("btnCalReports");
                    Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                    Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                    Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                    MenuTool.Font.Underline = true;
                    MenuPayment.Font.Underline = false;
                    MenuReport.Font.Underline = false;
                    MenuRecon.Font.Underline = false;
                    MenuAccount.Font.Underline = false;
                    MenuBatching.Font.Underline = false;
                    string strProcessScript = "this.value='Working...';this.disabled=true;";
                    btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        private void LoadControls(string UserCode)
        {
            int UserID = Convert.ToInt32(UserCode);
            dataTable = datafile.GetUserDetailsByID(UserID);
            lblCode.Text = dataTable.Rows[0]["Userid"].ToString();
            TxtFname.Text = dataTable.Rows[0]["FirstName"].ToString();
            txtLname.Text = dataTable.Rows[0]["SurName"].ToString();
            txtMiddleName.Text = dataTable.Rows[0]["OtherName"].ToString();
            txtemail.Text = dataTable.Rows[0]["UserEmail"].ToString();
            txtDesignation.Text = dataTable.Rows[0]["Title"].ToString();
            txtphone.Text = dataTable.Rows[0]["UserPhone"].ToString();
            lblusername.Text = dataTable.Rows[0]["Username"].ToString();
            string BranchID = dataTable.Rows[0]["UserBranch"].ToString();
            string RoleID = dataTable.Rows[0]["RoleCode"].ToString();
            string AreaID = dataTable.Rows[0]["UserArea"].ToString();
            string OTPDeliveryType = dataTable.Rows[0]["OTPDeliveryType"].ToString();
            bool IsActive = bool.Parse(dataTable.Rows[0]["Active"].ToString());
            bool IsLoggedOn = bool.Parse(dataTable.Rows[0]["LoggedOn"].ToString());
            // ddNotificationType.SelectedValue
            cboAreas.SelectedIndex = cboAreas.Items.IndexOf(cboAreas.Items.FindByValue(AreaID));
            LoadBranches(int.Parse(AreaID));
            cboBranches.SelectedIndex = cboBranches.Items.IndexOf(cboBranches.Items.FindByValue(BranchID));
            cboAccessLevel.SelectedIndex = cboAccessLevel.Items.IndexOf(cboAccessLevel.Items.FindByValue(RoleID));
            ddNotificationType.SelectedIndex = ddNotificationType.Items.IndexOf(ddNotificationType.Items.FindByValue(OTPDeliveryType));
            chkIsLoggedon.Checked = IsLoggedOn;
            chkIsActive.Checked = IsActive;
            chkResetPassword.Enabled = true;
            chkIsLoggedon.Enabled = true;
            //if(
        }

        private void LoadNotificationTypes()
        {
            string category = "OTP";
            dataTable = datafile.GetNotificationTypes(category);
            ddNotificationType.DataSource = dataTable;
            ddNotificationType.DataValueField = "NotificationTypeCode";
            ddNotificationType.DataTextField = "NotificationType";
            ddNotificationType.DataBind();
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
        private void LoadRoles()
        {
            dataTable = datafile.GetSystemRoles();
            cboAccessLevel.DataSource = dataTable;
            cboAccessLevel.DataValueField = "RoleCode";
            cboAccessLevel.DataTextField = "RoleName";
            cboAccessLevel.DataBind();
        }
        private void LoadAreas()
        {
            dataTable = datafile.GetAreas();
            cboAreas.DataSource = dataTable;
            cboAreas.DataValueField = "TypeId";
            cboAreas.DataTextField = "UserType";
            cboAreas.DataBind();
        }
        private void LoadBranches(int AreaID)
        {
            dataTable = datafile.GetBranches(AreaID);
            cboBranches.DataSource = dataTable;
            cboBranches.DataValueField = "CompanyCode";
            cboBranches.DataTextField = "Company";
            cboBranches.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows.Count == 1)
                {
                    cboBranches.Enabled = false;
                    string district = dataTable.Rows[0]["CompanyCode"].ToString();
                    cboBranches.SelectedIndex = cboBranches.Items.IndexOf(cboBranches.Items.FindByValue(district));
                }
                else
                {
                    cboBranches.Enabled = true;
                }

            }
            else
            {
                cboBranches.Enabled = false;
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
            SystemUser user = new SystemUser();
            user.Userid = int.Parse(lblCode.Text.Trim());
            user.Fname = TxtFname.Text.Trim();
            user.Sname = txtLname.Text.Trim();
            user.Oname = txtMiddleName.Text.Trim();
            user.Uname = lblusername.Text.Trim();
            user.Phone = txtphone.Text.Trim();
            user.Email = txtemail.Text.Trim();
            user.Area = int.Parse(cboAreas.SelectedValue.ToString());
            user.Branch = cboBranches.SelectedValue.ToString();
            user.Title = txtDesignation.Text.Trim();
            user.Role = cboAccessLevel.SelectedValue.ToString();
            user.Active = chkIsActive.Checked;
            user.LoggedOn = chkIsLoggedon.Checked;
            user.Reset = chkResetPassword.Checked;
            user.UserName = txtUserName.Text.Trim();
            user.NotificationType = ddNotificationType.SelectedValue.ToString();
            if (user.Fname.Equals(""))
            {
                ShowMessage("Please Enter User First Name", true);
                TxtFname.Focus();
            }
            else if (user.Sname.Equals(""))
            {
                ShowMessage("Please Enter User SurName", true);
                txtLname.Focus();
            }
            else if (!bll.IsValidEmailAddress(user.Email))
            {
                ShowMessage("Please Enter User Valid Email Address", true);
                txtemail.Focus();
            }
            else if (user.Area.Equals(0))
            {
                ShowMessage("Please Select Company Type", true);
            }
            else if (user.Role.Equals("0"))
            {
                ShowMessage("Please Select User System role", true);
            }
            else
            {
                if (user.Branch.Equals(0) && bll.AreaHasBranches(user.Area))
                {
                    ShowMessage("Please Select Company", true);
                }
                else
                {
                    string returned = Process.SaveSystemUser(user);
                    if (returned.Contains("Successfully"))
                    {
                        ShowMessage(returned, false);
                        MultiView2.ActiveViewIndex = -1;
                        ClearControls();
                    }
                    else if (returned.Contains("System generated username"))
                    {
                        ShowMessage(returned, true);
                        MultiView2.ActiveViewIndex = 0;
                        txtUserName.Focus(); // USERNAME PROVIDED ALREADY EXISTS
                    }
                    else if (returned.Contains("UserName Provided already Exists"))
                    {
                        ShowMessage(returned, true);
                        MultiView2.ActiveViewIndex = 0;
                        txtUserName.Focus(); // USERNAME PROVIDED ALREADY EXISTS
                    }
                    else
                    {
                        ShowMessage(returned, true);
                        MultiView2.ActiveViewIndex = -1;
                    }
                }
            }
        }

        private void CallCofirmation(string FirstName, string MiddleName, string LastName)
        {
            MultiView1.ActiveViewIndex = 1;
            string Message = "User with Names( " + FirstName + " " + MiddleName + " " + LastName + " ) is already in the System";
            lblQn.Text = Message;
        }
        protected void cboAccessLevel_DataBound(object sender, EventArgs e)
        {
            cboAccessLevel.Items.Insert(0, new ListItem("Select User Type", "0"));
        }

        protected void cboAreas_DataBound(object sender, EventArgs e)
        {
            cboAreas.Items.Insert(0, new ListItem("Select Company Type", "0"));
        }
        protected void cboAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int AreaID = Convert.ToInt16(cboAreas.SelectedValue.ToString());
                LoadBranches(AreaID);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }
        private void ClearControls()
        {
            lblCode.Text = "0";
            txtDesignation.Text = "";
            txtemail.Text = "";
            TxtFname.Text = "";
            txtLname.Text = "";
            txtMiddleName.Text = "";
            txtphone.Text = "";
            cboAreas.SelectedIndex = cboAreas.Items.IndexOf(cboAreas.Items.FindByValue("0"));
            cboBranches.SelectedIndex = cboBranches.Items.IndexOf(cboBranches.Items.FindByValue("0"));
            cboAccessLevel.SelectedIndex = cboAccessLevel.Items.IndexOf(cboAccessLevel.Items.FindByValue("0"));

        }
        protected void cboBranches_DataBound(object sender, EventArgs e)
        {
            cboBranches.Items.Insert(0, new ListItem("Select Company", "0"));
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {

        }
        protected void btnNo_Click(object sender, EventArgs e)
        {

        }
        protected void cboAccessLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlNotification_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void ddlNotificaction_DataBound(object sender, EventArgs e)
        {



        }
    }
}