using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace apps
{
    public partial class AddUtilityCredentials : System.Web.UI.Page
    {
        ProcessUsers Process = new ProcessUsers();
        DataLogin datafile = new DataLogin();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();

        Vendor vendor = new Vendor();

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

                    if (Request.QueryString["transferid"] != null)
                    {
                        string tranId = Request.QueryString["transferid"].ToString();
                        txtTranId.Text = tranId;
                    }

                    string strProcessScript = "this.value='Working...';this.disabled=true;";
                    btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
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

        protected void btnReturn_Click(object sender, EventArgs e)
        {

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtComment.Text))
                {
                    ShowMessage("Please provide a comment", true);
                    return;
                }

                UpdateException();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        private void UpdateException()
        {
            string tranId = txtTranId.Text.Trim();
            string comment = txtComment.Text.Trim();
            bool clear = chkPrepayment.Checked;

            string[] SearchParams = { tranId, comment, clear.ToString(), username };
            InterLinkClass.CoreMerchantAPI.Result res = bll.ExecuteDataQueryMerchant("LiveMerchantCoreDB", "UpdateReconException", SearchParams);
            //bll.ExecuteDataQuery("LiveMerchantCoreDB", "UpdateReconException", tranId, comment, clear, username);

            bll.InsertIntoAuditLog(tranId, "UPDATE", "Merchant Reconciliation Exceptions", userBranch, username, bll.GetCurrentPageName(), fullname + " updated the exception " + tranId + " with the following; Cleared:" + clear + ", Comment:" + comment);


            ShowMessage("The exception has been updated", false);
        }

        private void ClearControls()
        {
            ddlVendor.SelectedIndex = 0;
            txtComment.Text = "";
            chkPrepayment.Checked = false;
        }
    }
}