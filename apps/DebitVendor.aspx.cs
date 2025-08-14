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
using InterLinkClass.Epayment;
using InterLinkClass.EntityObjects;

public partial class DebitVendor : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {                
                MultiView2.ActiveViewIndex = 0;
                MultiView3.ActiveViewIndex = 0;
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
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

    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnCancel.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnCancel, "").ToString());
        btnDebit.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnDebit, "").ToString());
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GetPrepaidVendor(txtVendorCode.Text.Trim());
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void GetPrepaidVendor(string vendorCode)
    {
        DataTable dt = bll.Search("GetPrepaidVendorDetails", vendorCode).Tables[0];

        if (dt.Rows.Count > 0)
        {
            txtAcct.Text = dt.Rows[0]["AccountNumber"].ToString();
            txtcode.Text = dt.Rows[0]["VendorCode"].ToString();
            txtname.Text = dt.Rows[0]["Vendor"].ToString();
            txtpaymode.Text = "DEBIT";
            txtNarration.Text = "";
            lblcode.Text = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            txtBalance.Text = dt.Rows[0]["AccountBalance"].ToString();
            txtUser.Text = Session["FullName"].ToString();
            return;
        }

        ShowMessage("Vendor Details Not Found", true);
    }

    protected void btnInitiate_Click(object sender, EventArgs e)
    {
        try
        {
            ValidateRequest();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ValidateRequest()
    {
        string narration = txtNarration.Text.Trim();
        string amount = txtAmount.Text.Trim();

        if (string.IsNullOrEmpty(narration))
        {
            ShowMessage("Please specify the reason for this debit", true);
            return;
        }
        if (string.IsNullOrEmpty(amount))
        {
            ShowMessage("Please the amount to debit", true);
            return;
        }
        if (!bll.IsValidAmount(amount))
        {
            ShowMessage("Invalid amonunt supplied", true);
            return;
        }

        buttons.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            buttons.Visible = false;
            string msg = ".";
            ShowMessage(msg, true);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    protected void btnDebit_Click(object sender, EventArgs e)
    {
        try
        {
            string vendorCode = txtcode.Text.Trim();
            string account = txtAcct.Text.Trim();
            string amount = txtAmount.Text.Trim();
            string reference = lblcode.Text;
            string narration = txtNarration.Text.Trim();
            string tranType = txtpaymode.Text.Trim();

            string fullname = Session["FullName"].ToString();
            string userId = Session["Username"].ToString();
            string userBranch = Session["UserBranch"].ToString();

            bll.ExecuteProcedure("ReversePrepaidVendorCredit", vendorCode, account, amount, reference, narration, tranType);

            bll.InsertIntoAuditLog(account, "CREATE", "Debit Vendor", userBranch, userId, bll.GetCurrentPageName(),
fullname + " successfully debited [" + bll.PutCommas(amount) + "]  from the vendorCode [" + vendorCode + "]. Reason: " + narration + " IP:" + bll.GetIPAddress() + "at " + DateTime.Now.ToString());

            ShowMessage("Debit Successful", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ClearContrls2()
    {  
        txtAmount.Text = "";   
        txtcode.Text = "";  
        txtname.Text = "";
        txtpaymode.Text = "";
        lblcode.Text = "0";
    }

    private void ClearContrls()
    {
        txtAmount.Text = "";
        txtcode.Text = "";
        txtname.Text = "";
        txtpaymode.Text = "";
        lblcode.Text = "0";
    }
}
