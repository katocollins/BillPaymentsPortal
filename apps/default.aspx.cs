using System;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InterLinkClass.EntityObjects;
using InterLinkClass.ControlObjects;
using System.Linq;

namespace apps
{
    public partial class default1 : System.Web.UI.Page
    {
        DataLogin datafile = new DataLogin();
        ProcessUsers Usersdll = new ProcessUsers();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();
        DataTable dTable = new DataTable();
        DataTable dtLevels = new DataTable();
        DataSet dataSet = new DataSet();
        HttpCookie userCookie;
        SystemUser user = new SystemUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PageLoadMethod();
                    DisableBtnsOnClick();
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        private void DisableBtnsOnClick()
        {
            string strProcessScript = "this.value='Working...';this.disabled=true;";
            Btnlogin.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Btnlogin, "").ToString());
            BtnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(BtnSave, "").ToString());
        }

        private void PageLoadMethod()
        {
            lblmsg.Text = ".";
            txtUsername.Focus();
            int item = Request.QueryString.Count;
            if (item != 0)
            {
                HttpCookie useridCookie = Request.Cookies["UserID"];
                HttpCookie usernameCookie = Request.Cookies["UserName"];
                if (useridCookie != null)
                {
                    user = new SystemUser();
                    user.Action = "Logged-out";
                    user.Uname = usernameCookie.Value;
                    user.Userid = int.Parse(useridCookie.Value);
                    user.LoggedOn = false;
                    Usersdll.LogActivity(user);
                    Usersdll.LoginStatus(user);
                }
            }
        }

        private void ShowMessage(string Message, bool Error)
        {
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

        private void ShowMessage1(string Message, bool Error)
        {
            if (Error) { lblmsg.ForeColor = System.Drawing.Color.Green; lblmsg.Font.Bold = false; }
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

        protected void Btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                string pword = bll.DecryptString("C8okMTL3IM6tbhJ1yjVBHA==");
                Login();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        protected void BtnForgotPassword_Click(object sender, EventArgs e)
        {
            try
            {
                MultiView1.ActiveViewIndex = 2;
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SavePassword();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        private void SavePassword()
        {
            if (txtNewPassword.Text.Trim() == Label2.Text.Trim())
            {
                ShowMessage("You can not use the same Username as the Password. Please re-enter the Password.", true);
            }
            else if (CheckStrength(txtNewPassword.Text.Trim()))
            {
                user = new SystemUser();
                user.Userid = int.Parse(Label1.Text.Trim());
                user.Passwd = txtNewPassword.Text.Trim();
                user.Cpasswd = txtConfirmPassword.Text.Trim();
                string returned = Usersdll.ChangeUserPassword(user);

                datafile.SetLastPasswordChangeTime(user.Userid.ToString());

                if (returned.Contains("Successfully"))
                {
                    user.Action = "Password Change";
                    user.Uname = Label2.Text.Trim();
                    Usersdll.LogActivity(user);
                    MultiView1.ActiveViewIndex = 0;
                    txtpassword.Focus();
                    ShowMessage(returned, false);
                }
                else
                {
                    ShowMessage(returned, true);
                }
            }
        }

        public bool CheckStrength(string password)
        {
            if (password.Length < 1)
            {
                ShowMessage("PASSWORD IS EMPTY", true);
                return false;
            }

            if (password.Length <= 7)
            {
                ShowMessage("PASSWORD LENGTH CAN'T BE LESS THAN 8 CHARACTERS", true);
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                ShowMessage("PASSWORD SHOULD INCLUDE A NUMBER/DIGIT", true);
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                ShowMessage("PASSWORD SHOULD INCLUDE ATLEAST ONE UPPER CASE LETTER", true);
                return false;
            }

            if (!password.Any(char.IsLower))
            {
                ShowMessage("PASSWORD SHOULD INCLUDE ATLEAST ONE LOWER CASE LETTER", true);
                return false;
            }

            if (!password.Any(ch => !Char.IsLetterOrDigit(ch)))
            {
                ShowMessage("PASSWORD SHOULD INCLUDE ATLEAST ONE SPECIAL CHARACTER", true);
                return false;
            }

            return true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            txtpassword.Focus();
            ShowMessage(".", true);
        }

        private void Login()
        {
            string UserName = txtUsername.Text.Trim();
            string Passwd = txtpassword.Text.Trim();
            user = new SystemUser();
            user.Uname = UserName;
            user.Passwd = Passwd;

            if (user.Uname.Equals(""))
            {
                ShowMessage("Please Enter your System Username", true);
                txtUsername.Focus();
            }
            else if (user.Passwd.Equals(""))
            {
                ShowMessage("Please Enter Your System Password", true);
                txtpassword.Focus();
            }
            else
            {
                user.Uname = UserName;
                user.Passwd = Passwd;
                Access(user);
            }
        }

        private void Access(SystemUser user)
        {
            user.Passwd = bll.EncryptString(user.Passwd);
            dataTable = datafile.GetUserAccessibility(user);
            if (dataTable.Rows.Count > 0)
            {
                datafile.ResetUserLoginCount(user.Uname);

                int UserID = int.Parse(dataTable.Rows[0]["UserID"].ToString());
                string RoleCode = dataTable.Rows[0]["RoleCode"].ToString();
                string VendorCode = dataTable.Rows[0]["UserBranch"].ToString();
                bool IsActiveRole = bool.Parse(dataTable.Rows[0]["IsRoleActive"].ToString());
                bool IsActiveArea = bool.Parse(dataTable.Rows[0]["IsAreaActive"].ToString());
                bool IsActiveBranch = bool.Parse(dataTable.Rows[0]["IsBranchActive"].ToString());
                bool IsActiveUser = bool.Parse(dataTable.Rows[0]["Active"].ToString());
                bool IsLoggedIn = bool.Parse(dataTable.Rows[0]["LoggedOn"].ToString());
                bool reset = bool.Parse(dataTable.Rows[0]["Reset"].ToString());
                if (IsActiveRole)
                {
                    if (IsActiveArea)
                    {
                        if (IsActiveBranch)
                        {
                            if (IsActiveUser)
                            {
                                string Message = "";
                                if (reset)
                                {
                                    Message = "Please you need to change your password to continue";
                                    RequestToChangePassword(Message);
                                    Label1.Text = UserID.ToString();
                                    Label2.Text = user.Uname;
                                }
                                else
                                {
                                    DateTime DateOfChange = AssignSessions(dataTable);
                                    if (bll.PasswordExpired(DateOfChange))
                                    {
                                        Message = "Your Password expired and needs to be changed";
                                        bll.InsertIntoAuditLog(user.Uname, "LOGIN", "SystemUsers", VendorCode, user.Uname, bll.GetCurrentPageName(),
                                            user.Name + " failed to log in from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString() + " .Reason: " + Message);
                                        RequestToChangePassword(Message);
                                        Label1.Text = UserID.ToString();
                                        txtNewPassword.Focus();
                                        ShowMessage(".", true);
                                    }
                                    else
                                    {
                                        double RemainingDays = bll.IsRemainingDays(DateOfChange);
                                        if (RemainingDays < 5)
                                        {
                                            WarnAboutExpiry(RemainingDays);
                                            Label1.Text = UserID.ToString();
                                            Label2.Text = user.Uname;
                                            txtNewPassword.Focus();
                                            ShowMessage(".", true);
                                        }
                                        else
                                        {
                                            DataLogin dl = new DataLogin();
                                            dl.SaveLoginOTP(user.Uname);
                                            MultiView1.ActiveViewIndex = 4;
                                            Session["UserName"] = user.Uname;
                                            Session["Password"] = user.Passwd;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string msg = "Your Account is disabled, Please Contact System Administrators";
                                ShowMessage(msg, true);
                                bll.InsertIntoAuditLog(user.Uname, "LOGIN", "SystemUsers", VendorCode, user.Uname, bll.GetCurrentPageName(),
                                    user.Name + " failed to log in from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString() + " .Reason: " + msg);
                            }
                        }
                        else
                        {
                            ShowMessage("Your Company is disabled, Please Contact System Administrators", true);
                        }
                    }
                    else
                    {
                        ShowMessage("Your Operating Region is disabled, Please Contact System Administrators", true);
                    }
                }
                else
                {
                    ShowMessage("Your System Role is disabled, Please Contact System Administrators", true);
                }
            }
            else
            {
                DataTable dt = datafile.GetUserLoginCount(user.Uname);
                if (!dt.Rows[0]["Message"].ToString().ToUpper().Contains("invalid"))
                {
                    string loginCount = dt.Rows[0]["LoginCount"].ToString();
                    if (int.Parse(loginCount) >= 3)
                    {
                        ShowMessage("Your account has been deactivated because of many failed login attempts. Please contact system administrator to have your account reactivated!", true);
                    }

                    else
                    {
                        ShowMessage("The Username or Password is wrong. Please try again!", true);
                    }
                }
            }
        }

        private void WarnAboutExpiry(double Remaining)
        {
            string Message = "";
            string[] arra = Remaining.ToString().Split('.');
            int days = Convert.ToInt16(arra[0]);
            if (days < 1)
            {
                Message = "Your Password expired and needs to be changed";
                RequestToChangePassword(Message);
            }
            else
            {
                MultiView1.ActiveViewIndex = 3;
                Message = "Your Password will Expire in " + arra[0] + " day(s), Do you want to change it now ";
                lblQn.Text = Message;
            }
        }

        private void RequestToChangePassword(string Message)
        {
            MultiView1.ActiveViewIndex = 1;
            ShowMessage(Message, false);
        }

        private void Redirection(string StartPage)
        {
            SystemUser user = new SystemUser();
            user.Action = "Logged-in";
            user.Uname = Session["UserName"].ToString();
            user.Userid = int.Parse(Session["UserID"].ToString());
            user.LoggedOn = true;
            Usersdll.LogActivity(user);
            Usersdll.LoginStatus(user);
            Server.Transfer(StartPage);
        }

        private void AssignUserCookie()
        {
            userCookie = Request.Cookies["UserID"];
            if (userCookie != null)
            {
                userCookie.Expires = DateTime.Now.AddYears(-30);
            }

            userCookie = new HttpCookie("UserID", Session["UserID"].ToString());
            userCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(userCookie);
        }

        private DateTime AssignSessions(DataTable dataTable)
        {
            Session["UserBranch"] = dataTable.Rows[0]["UserBranch"].ToString();
            Session["UserID"] = dataTable.Rows[0]["UserID"].ToString();
            Session["FullName"] = dataTable.Rows[0]["FullName"].ToString();
            Session["UserName"] = dataTable.Rows[0]["UserName"].ToString();
            Session["RoleName"] = dataTable.Rows[0]["RoleName"].ToString();
            Session["RoleCode"] = dataTable.Rows[0]["RoleCode"].ToString();
            Session["AreaID"] = dataTable.Rows[0]["TypeId"].ToString();
            Session["AreaCode"] = dataTable.Rows[0]["UserType"].ToString();
            Session["AreaName"] = dataTable.Rows[0]["UserType"].ToString();
            Session["DistrictID"] = dataTable.Rows[0]["CompanyCode"].ToString();
            Session["DistrictCode"] = dataTable.Rows[0]["CompanyCode"].ToString();
            Session["DistrictName"] = dataTable.Rows[0]["DistrictName"].ToString();
            Session["Page"] = dataTable.Rows[0]["Page"].ToString();
            Session["VendorType"] = dataTable.Rows[0]["VendorType"].ToString();
            Session["CustomerPegasusAccount"] = dataTable.Rows[0]["AccountNumber"].ToString();
            Session["LoggedAt"] = DateTime.Now.ToString();
            Session["UserEmail"] = dataTable.Rows[0]["UserEmail"].ToString();
            DateTime LastPasswordChange = Convert.ToDateTime(dataTable.Rows[0]["LastPasswdChange"].ToString());
            return LastPasswordChange;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                string Message = "Enter your New Password and Confirm it below and then Save";
                RequestToChangePassword(Message);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            try
            {
                string StartPage = Session["Page"].ToString();
                Redirection(StartPage);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        protected void BtnResetUserPassword_Click(object sender, EventArgs e)
        {
            string UserName2 = TextBoxUsername.Text.Trim();
            string EmailEntered = TextBoxEmail.Text.Trim();

            if (UserName2.Equals(""))
            {
                ShowMessage("Please Enter your System Username", true);
                TextBoxUsername.Focus();
            }
            else if (EmailEntered.Equals(""))
            {
                ShowMessage("Please Enter Your System Email", true);
                TextBoxEmail.Focus();
            }
            else
            {
                dataTable = datafile.GetUserPassword(UserName2, EmailEntered);
                SystemUser user = new SystemUser();
                if (dataTable.Rows.Count > 0)
                {
                    user.Userid = int.Parse(dataTable.Rows[0]["Userid"].ToString());
                    user.Email = dataTable.Rows[0]["UserEmail"].ToString();
                    user.Fname = dataTable.Rows[0]["FirstName"].ToString();
                    user.Uname = dataTable.Rows[0]["UserName"].ToString();
                    user.Sname = dataTable.Rows[0]["SurName"].ToString();
                    user.Oname = dataTable.Rows[0]["OtherName"].ToString();
                    string passwd = bll.PasswdString(8);
                    user.Passwd = bll.EncryptString(passwd);
                    datafile.ResetPassword(user);

                    string Subject = "New PegPay Portal Credentials";
                    string Body = "Hello\t" + user.Fname + "\t" + user.Sname + "\t" + user.Oname + "<br></br><br></br>" +
                        "Please Find Below your New Portal Credentials" +
                        "<br></br><br></br>" + "Username:\t" + user.Uname +
                        "<br></br><br></br>" + "Password:\t" + passwd +
                        "<br></br><br></br><br></br>Thank You" +
                        "<br></br><b></br><b></br>Pegasus Technologies";

                    SendMail mm = new SendMail();
                    mm.SendUserEmail(user.Email, Subject, Body);
                    ShowMessage1("Hello\t" + user.Fname + "\t" + user.Sname + "\t" + user.Oname + "\t your new credentials have been sent to your Email", true);
                    MultiView1.ActiveViewIndex = 0;
                }
                else
                {
                    ShowMessage("Please Enter a correct Username or Email", true);
                    ClearControls();
                    MultiView1.ActiveViewIndex = 2;
                }
            }
        }

        private void ClearControls()
        {
            TextBoxUsername.Text = "";
            TextBoxEmail.Text = "";
        }

        protected void BtnPinPrompt_Click(object sender, EventArgs e)
        {
            DataLogin datL = new DataLogin();
            user.Uname = Session["UserName"].ToString();
            user.Passwd = Session["Password"].ToString();
            dataTable = datafile.GetUserAccessibility(user);
            user.Name = dataTable.Rows[0]["FullName"].ToString();
            int UserID = int.Parse(dataTable.Rows[0]["UserID"].ToString());
            string RoleCode = dataTable.Rows[0]["RoleCode"].ToString();
            string VendorCode = dataTable.Rows[0]["UserBranch"].ToString();
            bool IsActiveRole = bool.Parse(dataTable.Rows[0]["IsRoleActive"].ToString());
            bool IsActiveArea = bool.Parse(dataTable.Rows[0]["IsAreaActive"].ToString());
            bool IsActiveBranch = bool.Parse(dataTable.Rows[0]["IsBranchActive"].ToString());
            bool IsActiveUser = bool.Parse(dataTable.Rows[0]["Active"].ToString());
            bool IsLoggedIn = bool.Parse(dataTable.Rows[0]["LoggedOn"].ToString());
            bool reset = bool.Parse(dataTable.Rows[0]["Reset"].ToString());
            int OTPRetries = int.Parse(dataTable.Rows[0]["OTPRetries"].ToString());
            if (OTPRetries > 2)
            {
                datL.DeactivateUserAccount(user.Uname);
                bll.InsertIntoAuditLog(user.Uname, "LOGIN", "SystemUsers", VendorCode, user.Uname, bll.GetCurrentPageName(),
                    user.Name + " account deactivated due to failed OTP attempts from IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());
                MultiView1.ActiveViewIndex = 0;
                ShowMessage("Invalid Pin Code Entered more than three times. Your account has been deactivated. Contact administrator for help", true);
            }
            else
            {
                DataTable daTB = new DataTable();
                string pin = PinPrompt.Text.Trim();
                if (!string.IsNullOrEmpty(pin))
                {
                    user = new SystemUser();
                    user.Uname = Session["UserName"].ToString();
                    user.Passwd = Session["Password"].ToString();

                    daTB = datL.PickLoginOTP(user.Uname, pin);
                    if (daTB.Rows.Count > 0)
                    {
                        DateTime date = DateTime.Parse(daTB.Rows[0]["RecordDate"].ToString());
                        TimeSpan time = DateTime.Now.Subtract(date);
                        if (time.Minutes > 5)
                        {
                            ShowMessage("Sorry your one time pin has expired", true);
                            datL.UpdateLoginOtp(user.Uname, pin);
                            datL.ResetOTPCount(user.Uname);
                        }
                        else
                        {
                            datL.UpdateLoginOtp(user.Uname, pin);
                            datL.ResetOTPCount(user.Uname);
                            bll.InsertIntoAuditLog(user.Uname, "LOGIN", "SystemUsers", VendorCode, user.Uname, bll.GetCurrentPageName(),
                                user.Name + " successfully logged in from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());
                            AssignSessions(dataTable);
                            string StartPage = Session["Page"].ToString();
                            Redirection(StartPage);
                            AssignUserCookie();
                        }
                    }
                    else
                    {
                        datL.UpdateOTPRetries(user.Uname);
                        ShowMessage("InValid Pin Code Entered.", true);
                    }
                }
                else
                {
                    ShowMessage("InValid Pin Code Entered.", true);
                }
            }
        }

        protected void BtnPinPromptCancel_Click(object sender, EventArgs e)
        {
            Logout();
            MultiView1.ActiveViewIndex = 0;
            string Message = "Please enter your user details to login";
            ShowMessage(Message, false);
        }

        protected void BtnPromptResend_Click(object sender, EventArgs e)
        {
            try
            {
                DataLogin dll = new DataLogin();
                user.Uname = Session["UserName"].ToString();
                dll.DeactivateLogInOTPs(user.Uname);
                dll.SaveLoginOTP(user.Uname);
            }
            catch (Exception ex)
            {
                Response.Redirect("default1.aspx");
                ShowMessage("Failed to Login", true);
            }
        }

        private void Logout()
        {
            try
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
                Response.Redirect("default1.aspx");
            }
            catch (Exception ex)
            {
                Session.Clear();
                Session.Abandon();
                Session["FullName"] = null;
                Response.Redirect("default1.aspx");
            }
        }
    }
}