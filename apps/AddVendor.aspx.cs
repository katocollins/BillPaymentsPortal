using InterLinkClass.ControlObjects;
using InterLinkClass.CoreMerchantAPI;
using InterLinkClass.EntityObjects;
using InterLinkClass.MailApi;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace apps
{
    public partial class AddVendor : System.Web.UI.Page
    {
        //SystemUsers dac = new SystemUsers();
        ProcessUsers Process = new ProcessUsers();
        DataLogin datafile = new DataLogin();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();

        Vendor vendor = new Vendor();

        Merchant merchant = new Merchant();
        private HttpFileCollection uploads2 = HttpContext.Current.Request.Files;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["AreaID"].ToString().Equals("1"))
                {
                    if (IsPostBack == false)
                    {
                        LoadChargeTypes();
                        chkIsActive.Checked = true;
                        MultiView1.ActiveViewIndex = 0;
                        txtUser.Text = Session["FullName"].ToString();
                        if (Request.QueryString["transferid"] != null)
                        {
                            string vendorCode = Request.QueryString["transferid"].ToString();
                            LoadControls(vendorCode);
                        }
                        else
                        {
                            MultiView2.ActiveViewIndex = -1;
                        }
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
                else
                {
                    MultiView1.ActiveViewIndex = -1;
                    ShowMessage("YOU DO NOT HAVE RIGHTS TO VIEW THIS PAGE", true);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        private void LoadControls(string VendorCode)
        {
            vendor.Vendorid = int.Parse(VendorCode);
            dataTable = datafile.GetVendorById(vendor);
            if (dataTable.Rows.Count > 0)
            {
                lblCode.Text = dataTable.Rows[0]["Vendorid"].ToString();
                txtCode.Text = dataTable.Rows[0]["VendorCode"].ToString();
                txtName.Text = dataTable.Rows[0]["Vendor"].ToString();
                txtBillSystemCode.Text = dataTable.Rows[0]["BillSystemCode"].ToString();
                txtcontact.Text = dataTable.Rows[0]["ContactPerson"].ToString();
                txtemail.Text = dataTable.Rows[0]["VendorEmail"].ToString();
                txtconfirmemail.Text = dataTable.Rows[0]["VendorEmail"].ToString();
                bool isActive = bool.Parse(dataTable.Rows[0]["Active"].ToString());
                bool isMActive = bool.Parse(dataTable.Rows[0]["MActive"].ToString());
                chkIsPrepaidVendor.Checked = dataTable.Rows[0]["VendorType"].ToString().Equals("PREPAID") ? true : false;

                DataTable chargeTable = datafile.ExecuteDataSet("GenerateChargesReport1", txtCode.Text, "", "", "", "").Tables[0];
                cboChargeType.SelectedValue = chargeTable.Rows[0]["TypeId"].ToString();
                txtPegPayCharge.Text = chargeTable.Rows[0]["PegasusCharge"].ToString();
                txtCode.Enabled = false;
                MultiView2.ActiveViewIndex = 0;

            }
        }

        private void LoadChargeTypes()
        {
            try
            {
                dataTable = datafile.GetChargeTypes();
                cboChargeType.DataSource = dataTable;
                cboChargeType.DataValueField = "TypeId";
                cboChargeType.DataTextField = "ChargeName";
                cboChargeType.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
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
            vendor.Vendorid = int.Parse(lblCode.Text.Trim());
            vendor.VendorCode = txtCode.Text.Trim();
            vendor.BillSysCode = txtBillSystemCode.Text.Trim();
            vendor.VendorName = txtName.Text.Trim().ToUpper();
            vendor.Email = txtemail.Text.Trim();
            vendor.Active = chkIsActive.Checked;
            vendor.Sendemail = false;
            vendor.Reset = false;
            vendor.Contract = txtcontact.Text.Trim();
            vendor.ChargeType = int.Parse(cboChargeType.SelectedValue.ToString());
            vendor.IsPrepaidVendor = chkIsPrepaidVendor.Checked;
            double res = 0;

            if (vendor.VendorCode.Equals(""))
            {
                ShowMessage("Please Enter Vendor Code", true);
                txtCode.Focus();
            }
            else if (vendor.BillSysCode.Equals(""))
            {
                ShowMessage("Please Enter Vendor Billing System Code", true);
                txtBillSystemCode.Focus();
            }
            else if (vendor.VendorName.Equals(""))
            {
                ShowMessage("Please Enter Vendor Name", true);
                txtName.Focus();
            }
            else if (vendor.Email.Equals(""))
            {
                ShowMessage("Please Enter Vendor Email", true);
                txtemail.Focus();
            }
            else if (txtconfirmemail.Text.Equals(""))
            {
                ShowMessage("Please Confirm Email", true);
                txtconfirmemail.Focus();
            }
            else if (!vendor.Email.Equals(txtconfirmemail.Text.Trim()))
            {
                ShowMessage("Please Emails Provided do not match", true);
            }
            else if (!bll.IsValidEmailAddress(vendor.Email))
            {
                ShowMessage("Please Provide valid Emails ", true);
                txtemail.Focus();
            }
            else if (cboChargeType.SelectedValue.Equals("0"))
            {
                ShowMessage("Select Pegasus Charge type", true);
                txtconfirmemail.Focus();
            }
            else if (txtPegPayCharge.Text.Equals(""))
            {
                ShowMessage("Please Pegasus Charge is required", true);
                txtconfirmemail.Focus();
            }
            else if (!Double.TryParse(txtPegPayCharge.Text.Trim(), out res))
            {
                ShowMessage("Invalid Pegasus Charge Entered", true);
                txtconfirmemail.Focus();
            }
            else
            {
                vendor.PegpayCharge = Convert.ToDouble(txtPegPayCharge.Text.Trim());
                string ret = Process.SaveVendor(vendor, merchant);
                UploadCert(vendor.VendorCode);
                ShowMessage(ret, false);
                ClearControls();
            }

        }

        private void UploadCert(string VendorCode)
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
                    string strPath = bll.GetFileApplicationPath(VendorCode);
                    DirectoryInfo dic = new DirectoryInfo(strPath);
                    bll.EmptyFolder(dic);
                    string fullPath = strPath + "\\" + c1;
                    FileUpload1.PostedFile.SaveAs(fullPath);
                }
            }
        }

        private void ClearControls()
        {
            lblCode.Text = "0";
            txtName.Text = "";
            txtemail.Text = "";
            txtCode.Text = "";
            txtBillSystemCode.Text = "";
            txtconfirmemail.Text = "";
            txtcontact.Text = "";
            chkIsActive.Checked = false;
            MultiView2.ActiveViewIndex = -1;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            string password = bll.GetPasswordString();
            string hashedPassword = bll.EncryptString(password);
            Vendor vendor = new Vendor();
            vendor.Vendorid = int.Parse(lblCode.Text);
            vendor.Passwd = hashedPassword;
            datafile.ResetVendorPassword(vendor);
            SendCredentials();
        }

        protected void btnResend_Click(object sender, EventArgs e)
        {
            SendCredentials();
        }

        private void SendCredentials()
        {
            Messenger mapi = new Messenger();
            Email email = GetEmail();
            email.Sender = "notifications@pegasustechnologies.co.ug";
            System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidation;
            Result result = mapi.PostEmail(email);
            ShowMessage(result.StatusDesc, false);
        }

        private static bool RemoteCertificateValidation(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private Email GetEmail()
        {
            string hashedPassword = bll.Search("GetVendorById", new string[] { lblCode.Text.Trim() }).Tables[0].Rows[0]["VendorPassword"].ToString();
            string password = bll.DecryptString(hashedPassword);
            InterLinkClass.MailApi.Email email = new InterLinkClass.MailApi.Email();

            InterLinkClass.MailApi.EmailAddress addr = new InterLinkClass.MailApi.EmailAddress();
            addr.Address = txtemail.Text;
            addr.Name = txtcontact.Text;
            addr.AddressType = InterLinkClass.MailApi.EmailAddressType.To;

            EmailAddress[] addresses = { addr };
            email.MailAddresses = addresses;
            email.From = "PEGASUS";
            string Subject = "PEGPAY UTILITIES API";

            string message = "";
            message = "<br>Hello " + txtcontact.Text + ",\n\n" +
                "<br>Your PegPay Access Credentials are:\n" +
                 "<br><strong>VendorCode: " + txtCode.Text + "<strong>\n" +
                 "<br><strong>Password: " + password + "<strong>\n" +
                 "<br><strong>URL: https://pegasus.co.ug:8896/LivePegPayApi/PegPay.asmx?WSDL<strong>\n\n" +
                 "<br><strong>Support: techsupport@pegasustechnologies.co.ug";

            email.Message = message;
            email.Subject = Subject;
            return email;
        }

        protected void cboChargeType_DataBound(object sender, EventArgs e)
        {
            cboChargeType.Items.Insert(0, new ListItem(" -----Select Charge Type----- ", "0"));
        }

        protected void cboAccessLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnViewUtilityCreds_Click(object sender, EventArgs e)
        {
            string vendorCode = txtCode.Text.Trim();
            if (vendorCode.Equals(""))
            {
                ShowMessage("PLEASE SELECT A VENDOR", true);
            }
            else
            {
                Response.Redirect("./AddUtilityCredentials.aspx?transfereid=" + vendorCode, false);
            }
        }
    }
}