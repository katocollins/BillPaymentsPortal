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
using InterLinkClass.EntityObjects;
using System.Text.RegularExpressions;

public partial class PrepaidVendorSettings : System.Web.UI.Page
{
    PrepaidVendor PrepaidVendor = new PrepaidVendor();
    DataLogin dLogin = new DataLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FullName"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            string vendor = Session["UserBranch"].ToString().ToUpper();
            LoadMinBalance(vendor);
        }
    }

    private void LoadMinBalance(string vendor)
    {
        DataTable table = dLogin.GetVendorMinBalance(vendor);
        if (table.Rows.Count > 0)
        {
            string bal = table.Rows[0]["VendorBalance"].ToString();
            double money = Convert.ToDouble(bal);
            curBal.Text = money.ToString();
            curBal.Enabled = false;
        }
        else
        {
            double money = Convert.ToDouble("0");
            curBal.Text = money.ToString();
            curBal.Enabled = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string resp = validateInput();
        string msg = "";
        if (resp.Equals("VALID"))
        {
            msg = "YOUR PREPAID VENDOR SETTINGS HAVE BEEN SAVED.";
            ShowMessage(msg, true);
        }
        else
        {
            msg = "ERROR! YOUR PREPAID VENDOR SETTINGS HAVE NOT BEEN SAVED.";
            ShowMessage(msg, true);
        }
        

    }

    public string validateInput()
    {
        string resp = "";
        PrepaidVendor.VendorBalance = txtminbal.Text.ToString();
        PrepaidVendor.Name = txtcontactperson.Text.ToString();
        PrepaidVendor.EmailAddress = TextBoxEmail.Text.ToString();
        PrepaidVendor.IsActive = true;
        PrepaidVendor.VendorCode = Session["UserBranch"].ToString().ToUpper();
        if (string.IsNullOrEmpty(PrepaidVendor.VendorBalance) || string.IsNullOrEmpty(PrepaidVendor.Name) || string.IsNullOrEmpty(PrepaidVendor.EmailAddress))
        {
            resp = "INVALID";
        }
        else
        {
            PrepaidVendor.VendorBalance = Regex.Replace(PrepaidVendor.VendorBalance, "[^0-9]", "");
            resp = dLogin.InsertPrepaidVendorDetails(PrepaidVendor);
            
        }

        return resp;
    }
    private void ShowMessage(string Message, bool Error)
    {
        Label lblmsg = (Label)Master.FindControl("lblmsg");
        if (Error) { lblmsg.ForeColor = System.Drawing.Color.Red; lblmsg.Font.Bold = false; }
        else { lblmsg.ForeColor = System.Drawing.Color.Black; lblmsg.Font.Bold = true; }
        if (Message == ".")
        {
            lblPrepaidVendor.Text = ".";
        }
        else
        {
            lblPrepaidVendor.Text = "MESSAGE: " + Message.ToUpper();
        }
    }
}
