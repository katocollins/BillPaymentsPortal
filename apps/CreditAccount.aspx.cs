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
using System.IO;
using System.Diagnostics;
using System.Text;
using InterLinkClass.EntityObjects;
//using InterLinkClass.PegpayMMoney;
public partial class CreditAccount : System.Web.UI.Page
{
    private DataLogin dac = new DataLogin();
    private BusinessLogin bll = new BusinessLogin();
    private PhoneValidator pv = new PhoneValidator();
    private DataTable dtable = new DataTable();
    //private Beneficiary beneficiary = new Beneficiary();
    private SendMail mailer = new SendMail();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                if (Session["RoleCode"].ToString().Equals("001") ||  Session["AreaID"].ToString().Equals("1"))
                {

                    LoadPageInitials();
                }
                else
                {
                    Response.Redirect("Default2.aspx");
                }
            }
            

            }
        
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadPageInitials()
    {
        try
        {
            string RoleCode = Session["RoleCode"].ToString();
            string AreaId = Session["AreaID"].ToString();
            //LoadNetworks();
            if ((RoleCode.Equals("001") || RoleCode.Equals("003") || RoleCode.Equals("007")) && AreaId.Equals("1"))
            {
                MultiView1.ActiveViewIndex = 0;
            }
            else
            {

                string CompanyCode = Session["CompanyCode"].ToString();
                string CompanyName = Session["DistrictName"].ToString();
                MultiView2.ActiveViewIndex = 0;
                txtCompanyCode.Text = CompanyCode;
                txtName.Text = CompanyName;
                btnCancel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    

    private void Toggle4Process()
    {
        string strProcessScript = "this.value='Processing...';this.disabled=true;";
        btnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnSave, "").ToString());
    }
    private void ShowMessage(string GetMessage, bool ColorRed)
    {
        Label msg = (Label)Master.FindControl("lblmsg");
        msg.Visible = true;
        if (ColorRed == true) { msg.ForeColor = System.Drawing.Color.Red; msg.Font.Bold = false; }
        else { msg.ForeColor = System.Drawing.Color.Blue; msg.Font.Bold = true; }
        if (GetMessage == ".")
        {
            msg.Text = ".";
        }
        else
        {
            msg.Text = "MESSAGE: " + GetMessage;
        }
    }

    

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ValidateDetails();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ValidateDetails()
    {
        string userId = Session["Username"] as string;
        string fullname = Session["Fullname"] as string;
        string userBranch = Session["UserBranch"] as string;

        string CompanyCode = txtCompanyCode.Text;
        string CompanyName = txtName.Text;
        string AccountNumber = txtAccNumber.Text;
        string stringAmt = txtCreditAmount.Text.Trim().Replace(",", "");
        string bankRef = txtBankRef.Text;
        string bankdate = txtBankDate.Text;
      
        double amt = 0;
        bool validAmount = double.TryParse(stringAmt, out amt);
        string recordId = lblCode.Text;
        if (CompanyCode.Equals(""))
        {
            ShowMessage("Please Select Company", true);
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = -1;
            cboCompanyCode.SelectedValue = "0";
        }
        else if (AccountNumber.Equals(""))
        {
            ShowMessage("Account Number not retrieved", true);
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = -1;
            cboCompanyCode.SelectedValue = "0";
        }
        else if (!validAmount)
        {
            ShowMessage("Please Enter Correct Amount", true);
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = -1;
            cboCompanyCode.SelectedValue = "0";
        }
        //else if (string.IsNullOrEmpty(bankRef))
        //{
        //    ShowMessage("Please Enter The Bank Reference for this credit", true);
        //    MultiView1.ActiveViewIndex = 0;
        //    MultiView2.ActiveViewIndex = -1;
        //    cboCompanyCode.SelectedValue = "0";
        //}
        else if (string.IsNullOrEmpty(bankdate))
        {
            bankdate = DateTime.Now.ToString();
        }

        else
        {
            double Amount = double.Parse(stringAmt);
            string status = bll.CreditAccount(CompanyCode, AccountNumber, Amount, bankRef, bankdate);
            if (status.Equals("OK"))
            {
                string message = "The Account Number " + AccountNumber + " has been Credited with " + Amount.ToString() + " and is Pending Approval. ";
                bll.InsertIntoAuditLog(AccountNumber, "CREATE", "Initiate Credit", userBranch, userId, bll.GetCurrentPageName(),
 fullname + " successfully initiated a credit of [" + Amount.ToString() + "]  for the vendorCode [" + CompanyCode + "], account:" + AccountNumber + " from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());
                 
                ShowMessage(message, false);
                ClearControls();

            }
        }
    }
    private void ValidateDetailsNew()
    {
        try
        {
            DataLogin df = new DataLogin();
            DataTable dt = new DataTable();
            string userId = Session["Username"] as string;
            string fullname = Session["Fullname"] as string;
            string userBranch = Session["UserBranch"] as string;

            string CompanyCode = txtCompanyCode.Text;
            string CompanyName = txtName.Text;
            string AccountNumber = txtAccNumber.Text;
            string stringAmt = txtCreditAmount.Text.Trim().Replace(",", "");
            string bankRef = txtBankRef.Text;
            string bankdate = txtBankDate.Text;

            double amt = 0;
            bool validAmount = double.TryParse(stringAmt, out amt);
            string recordId = lblCode.Text;
            if (CompanyCode.Equals(""))
            {
                ShowMessage("Please Select Company", true);
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = -1;
                cboCompanyCode.SelectedValue = "0";
            }
            else if (AccountNumber.Equals(""))
            {
                ShowMessage("Account Number not retrieved", true);
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = -1;
                cboCompanyCode.SelectedValue = "0";
            }
            else if (!validAmount)
            {
                ShowMessage("Please Enter Correct Amount", true);
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = -1;
                cboCompanyCode.SelectedValue = "0";
            }
            //else if (string.IsNullOrEmpty(bankRef))
            //{
            //    ShowMessage("Please Enter The Bank Reference for this credit", true);
            //    MultiView1.ActiveViewIndex = 0;
            //    MultiView2.ActiveViewIndex = -1;
            //    cboCompanyCode.SelectedValue = "0";
            //}
            else if (string.IsNullOrEmpty(bankdate))
            {
                bankdate = DateTime.Now.ToString();
            }

            else
            {
                double Amount = double.Parse(stringAmt);

                DataTable dt1 = df.GetVendorUtilityMappingToAirtimeOrData(CompanyCode);

                if (dt1.Rows.Count > 0)
                {

                    dt = df.GetAirtimeComission(CompanyCode);

                    if (dt.Rows.Count > 0)
                    {
                        string CommissionPercentage = dt.Rows[0]["CommissionPercentage"].ToString();

                        double commission1 = Double.Parse(CommissionPercentage);

                        double commission2 = (Amount * commission1) / 100;

                        double Amount1 = Amount + commission2;

                        //string status = bll.CreditAccountAirtime(CompanyCode, AccountNumber, Amount1, commission2, bankRef, bankdate);
                        string status = bll.CreditAccount(CompanyCode, AccountNumber, Amount1, bankRef, bankdate);
                        if (status.Equals("OK"))
                        {
                            string message = "The Account Number " + AccountNumber + " has been Credited with " + Amount1.ToString() + " with an Airtime/Data Commission of " + commission2.ToString() + " and is Pending Approval. ";
                            bll.InsertIntoAuditLog(AccountNumber, "CREATE", "Initiate Credit", userBranch, userId, bll.GetCurrentPageName(),
             fullname + " successfully initiated a credit of [" + Amount1.ToString() + "]  for the vendorCode [" + CompanyCode + "], account:" + AccountNumber + " from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());

                            ShowMessage(message, false);
                            ClearControls();

                        }

                    }
                    else
                    {
                        ShowMessage("PLEASE SET A PERCENTAGE COMMISSION FOR THIS AIRTIME OR DATA VENDOR.", false);
                    }
                }

                else
                {
                    string status = bll.CreditAccount(CompanyCode, AccountNumber, Amount, bankRef, bankdate);
                    if (status.Equals("OK"))
                    {
                        string message = "The Account Number " + AccountNumber + " has been Credited with " + Amount.ToString() + " and is Pending Approval. ";
                        bll.InsertIntoAuditLog(AccountNumber, "CREATE", "Initiate Credit", userBranch, userId, bll.GetCurrentPageName(),
         fullname + " successfully initiated a credit of [" + Amount.ToString() + "]  for the vendorCode [" + CompanyCode + "], account:" + AccountNumber + " from the IP:" + bll.GetIPAddress() + " at " + DateTime.Now.ToString());

                        ShowMessage(message, false);
                        ClearControls();

                    }
                }
            }
        }
        catch (Exception ee)
        {
            ShowMessage(ee.Message, true);
        }


        //double Amount = double.Parse(stringAmt)
    }
    private void ClearControls()
    {
        txtCompanyCode.Text = "";
        txtName.Text = "";
        txtCreditAmount.Text = "";
        txtCompanyCode.Text = "";
        txtAccountBalance.Text = "";
        txtAccNumber.Text = "";
        txtSearchCode.Text = "";
        txtsearchName.Text = "";
        txtBankRef.Text = "";
        txtBankDate.Text = "";
        cboCompanyCode.SelectedValue = "0";
        lblCode.Text = "0";
        btnSave.Enabled = false;
        MultiView1.ActiveViewIndex = -1;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string CompanyName = txtsearchName.Text;
            string CompanyCode = txtSearchCode.Text;
            dtable = dac.GetSystemCompanies(CompanyName, CompanyCode);
            if (dtable.Rows.Count > 0)
            {
                ShowMessage(".", false);
                LoadCompanies(dtable);
            }
            else
            {
                ShowMessage("No Companies Found For the Search", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadCompanies(DataTable dtable)
    {
        try
        {
            cboCompanyCode.DataSource = dtable;
            cboCompanyCode.DataValueField = "CompanyCode";
            cboCompanyCode.DataTextField = "Company";
            cboCompanyCode.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void cboCompanyCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string companyCode = cboCompanyCode.SelectedValue.ToString();
            string CompanyName = cboCompanyCode.SelectedItem.Text;
            string AccountNumber = "";
            string AccountBalance = "";
            dtable = dac.GetPegPayAccount(companyCode);
            if (dtable.Rows.Count > 0)
            {
                AccountNumber = dtable.Rows[0]["AccountNumber"].ToString();
                AccountBalance = dtable.Rows[0]["AccountBalance"].ToString();
            }
            txtName.Text = CompanyName;
            txtAccNumber.Text = AccountNumber;
            txtAccountBalance.Text = AccountBalance;
            txtCompanyCode.Text = companyCode;
            MultiView1.ActiveViewIndex = -1;
            MultiView2.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    protected void cboCompanyCode_DataBound(object sender, EventArgs e)
    {
        cboCompanyCode.Items.Insert(0, new ListItem("-- Select Company --", "0"));
    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = -1;
            cboCompanyCode.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}
