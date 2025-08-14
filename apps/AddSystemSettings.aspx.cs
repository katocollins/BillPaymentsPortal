using InterLinkClass.ControlObjects;
using InterLinkClass.EntityObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class AddSystemSettings : System.Web.UI.Page
    {
        ProcessUsers Process = new ProcessUsers();
        DataLogin datafile = new DataLogin();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();

        //Vendor vendor = new Vendor();

        private HttpFileCollection uploads2 = HttpContext.Current.Request.Files;

        string username = "";
        string fullname = "";
        string userBranch = "";
        string userRole = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                username = Session["UserName"] as string;
                fullname = Session["FullName"] as string;
                userBranch = Session["UserBranch"] as string;
                userRole = Session["RoleCode"] as string;

                if (!Session["AreaID"].ToString().Equals("1"))
                {
                    MultiView1.ActiveViewIndex = -1;
                    ShowMessage("YOU DO NOT HAVE RIGHTS TO VIEW THIS PAGE", true);
                }

                if (IsPostBack == false)
                {
                    MultiView1.ActiveViewIndex = 0;

                    LoadData();
                    if (Request.QueryString["transferid"] != null)
                    {
                        string vendorCode = Request.QueryString["transferid"].ToString();
                        vendor.Text = vendorCode;
                    }
                    if (Request.QueryString["utility"] != null)
                    {
                        string utility = Request.QueryString["utility"].ToString();
                        ddlUtility.SelectedValue = utility;
                    }

                    //string strProcessScript = "this.value='Working...';this.disabled=true;";
                    //btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }
        private void LoadData()
        {
            ///bll.LoadVendors(userBranch, ddlVendor);
            bll.LoadPegpayUtilities(userBranch, ddlUtility);
        }

        protected void ddlUtility_DataBound(object sender, EventArgs e)
        {
            ddlUtility.Items.Insert(0, new ListItem("Select Utility", "0"));
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

        protected void ddlUtility_SelectedIndexChanged(object sender, EventArgs e)
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
            if (vendor.Text.Equals(""))
            {
                ShowMessage("PLEASE SELECT A VENDOR", true);
            }
            else if (ddlUtility.SelectedIndex == 0)
            {
                ShowMessage("PLEASE SELECT A UTILITY", true);
            }
            else if (BankName.Text.Trim().Equals(""))
            {
                ShowMessage("PLEASE ENTER A BANK TO USE", true);
            }
            else if (BankUName.Text.Trim().Equals(""))
            {
                ShowMessage("PLEASE ENTER A BANK USERNAME TO USE", true);
            }
            else if (utilPswd.Text.Trim().Equals(""))
            {
                ShowMessage("PLEASE ENTER A BANK PASSWORD TO SAVE", true);
            }
            else
            {
                try
                {
                    string encryptPassword = ""; string encryptSecretKey = ""; string encryptCenterVal = ""; string encryptUsername = "";
                    string vendorCode = vendor.Text;
                    string utilityCode = ddlUtility.SelectedValue;
                    string username = BankUName.Text.Trim();
                    string password = utilPswd.Text.Trim();
                    string BankCode = BankName.Text.Trim();
                    string SecretKey = secretkey.Text.Trim();
                    string CertKy = certKey.Text.Trim();
                    string urlIp = url.Text.Trim();
                    string CenterVal = centerVal.Text.Trim();
                    string createdBy = Session["FullName"].ToString();
                    if (!password.Equals(""))
                    {
                        encryptPassword = bll.EncryptString(password);
                    }

                    if (!SecretKey.Equals(""))
                    {
                        encryptSecretKey = bll.EncryptString(SecretKey);
                    }
                    if (!CenterVal.Equals(""))
                    {
                        encryptCenterVal = bll.EncryptString(CenterVal);
                    }
                    if (!username.Equals(""))
                    {
                        encryptUsername = bll.EncryptString(username);
                    }
                    if (!urlIp.Equals(""))
                    {
                        urlIp = bll.EncryptString(urlIp);
                    }
                    if (!CertKy.Equals(""))
                    {
                        CertKy = bll.EncryptString(CertKy);
                    }
                    //datafile.SaveUtilityCredentials(vendorCode, utilityCode, username, password, bankCode, createdBy, DateTime.Now);
                    datafile.SaveUtilitySystemSetting(BankCode, encryptUsername, utilityCode, encryptPassword, vendorCode, encryptSecretKey, encryptCenterVal, urlIp, CertKy);
                    utilityCode = utilityCode + "_" + BankCode;
                    CreateDirectory(vendorCode, utilityCode);
                    if (!Cert.FileName.Trim().Equals(""))
                    {
                        UploadCert(vendorCode, utilityCode);
                    }
                    ShowMessage("DETAILS SAVED SUCCESSFULLY", false);
                }
                catch (Exception ex)
                {
                    ShowMessage(ex.Message, true);
                }
            }

        }

        private void CreateDirectory(string vendorCode, string utilityCode)
        {
            try
            {
                string rootDirectory = bll.DecryptString(datafile.GetSystemParameter(6, 3));
                string path = Path.Combine(rootDirectory, vendorCode);
                if (Directory.Exists(path))
                {
                    path = Path.Combine(path, utilityCode);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
                else
                {
                    Directory.CreateDirectory(path);
                    path = Path.Combine(path, utilityCode);
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UploadCert(string VendorCode, string UtilityCode)
        {
            HttpFileCollection uploads;
            uploads = HttpContext.Current.Request.Files;
            int countfiles = 0;
            for (int i = 0; i <= (uploads.Count - 1); i++)
            {
                if (uploads[i].ContentLength > 0)
                {
                    string c = Path.GetFileName(uploads[i].FileName);
                    string cNoSpace = c.Replace(" ", "-");
                    string c1 = cNoSpace;
                    string strPath = bll.DecryptString(datafile.GetSystemParameter(6, 3));//bll.GetFileApplicationPath(VendorCode);
                    strPath = Path.Combine(strPath, VendorCode);
                    strPath = Path.Combine(strPath, UtilityCode);
                    DirectoryInfo dic = new DirectoryInfo(strPath);
                    bll.EmptyFolder(dic);
                    string fullPath = Path.Combine(strPath, c1);//strPath + "\\" + c1;
                    Cert.PostedFile.SaveAs(fullPath);
                }
            }
        }
        protected void btnGetCredentials_Click(object sender, EventArgs e)
        {
            ShowMessage("", true);
            if (vendor.Text == "")
            {
                ShowMessage("PLEASE SELECT A VENDOR:", true);
            }
            else if (ddlUtility.SelectedIndex == 0)
            {
                ShowMessage("PLEASE SELECT A UTILITY:", true);
            }
            else
            {
                string vendorCode = "Pegasus";
                string utilityCode = ddlUtility.SelectedValue;
                DataTable dt = datafile.GetUtilityCredentials(vendorCode, utilityCode);
                if (dt.Rows.Count > 0)
                {
                    string username = dt.Rows[0]["UtilityUsername"].ToString();
                    string password = dt.Rows[0]["UtilityPassword"].ToString();
                    string bankCode = dt.Rows[0]["BankCode"].ToString();
                    //utility.Text = username;
                    //utilPswd.Text = password;
                    BankName.Text = bankCode;
                }
                else
                {
                    ShowMessage(vendor.Text + " HAS NO CREDENTIALS FOR " + ddlUtility.SelectedItem.Text, true);
                }
            }
        }
    }
}