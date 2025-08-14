using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class UmemeDirectPage : System.Web.UI.Page
    {
        ProcessPay Process = new ProcessPay();
        DataLogin datafile = new DataLogin();
        Datapay datapay = new Datapay();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();
        DataTable dtable = new DataTable();
        DataTable activebanks = new DataTable();
        string loggedinUser = "";
        string username = "";
        string fullname = "";
        string userBranch = "";
        private string CorrectCustomerRef;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                username = Session["UserName"] as string;
                fullname = Session["FullName"] as string;
                userBranch = Session["UserBranch"] as string;
                loggedinUser = Session["Username"].ToString();
                if (IsPostBack == false)
                {
                    MultiView2.ActiveViewIndex = 0;
                    LoadUtilities();

                }
            }
            catch (Exception ex)
            {
                Response.Redirect("Default.aspx");
            }

        }

        private void LoadUtilities()
        {
            dtable = datafile.GetUmemeDirect();
            cboUtility.DataSource = dtable;
            cboUtility.DataValueField = "BankCode";
            cboUtility.DataTextField = "BankName";
            cboUtility.DataBind();
            cboUtility.Enabled = false;

            activebanks = datafile.GetUmemeActivity();

            if (activebanks.Rows.Count > 0)
            {
                DataGrid1.DataSource = activebanks;
                DataGrid1.DataBind();
                MultiView2.ActiveViewIndex = 0;

            }
            else
            {
                ShowMessage("UMEME DIRECT NOT ACTIVE.", true);
            }


        }

        protected void Activate_Click(object sender, EventArgs e)
        {
            try
            {

                string bankcode = cboUtility.SelectedItem.Value.ToString();
                //CorrectCustomerRef = txtCustomerRef.Text.Trim();
                if (string.IsNullOrEmpty(bankcode))
                {
                    ShowMessage("Please select bank", true);
                }

                datafile.ActivateUMEME(bankcode);
                Response.Redirect(Request.RawUrl);
                ShowMessage("UMEME DIRECT HAS BEEN ACVTIVATED SUCCESSFULLY.", true);

            }
            catch (Exception ee)
            {
                ShowMessage("ERROR OCCURED WHILE ACTIVATING.", false);
            }
        }
        protected void Deactivate_Click(object sender, EventArgs e)
        {
            try
            {

                string bankcode = cboUtility.SelectedItem.Value.ToString();
                //CorrectCustomerRef = txtCustomerRef.Text.Trim();
                if (string.IsNullOrEmpty(bankcode))
                {
                    ShowMessage("Please select bank", true);
                }

                datafile.DeactivateUMEME(bankcode);
                Response.Redirect(Request.RawUrl);
                ShowMessage("UMEME DIRECT HAS BEEN DEACVTIVATED SUCCESSFULLY.", false);

            }
            catch (Exception ee)
            {
                ShowMessage("ERROR OCCURED WHILE DEACTIVATING.", false);
            }
        }
        private void ShowMessage(string Message, bool Error)
        {
            Label lblmsg = (Label)Master.FindControl("lblmsg");
            if (Error) { lblmsg.ForeColor = System.Drawing.Color.Red; lblmsg.Font.Bold = false; }
            else { lblmsg.ForeColor = System.Drawing.Color.Green; lblmsg.Font.Bold = true; }
            if (Message == ".")
            {
                lblmsg.Text = ".";
            }
            else
            {
                lblmsg.Text = "MESSAGE: " + Message.ToUpper();
            }
        }
        protected void btn_Success_Click(object sender, EventArgs e)
        {
            try
            {
                string utilityRef = txtUtilityRef.Text;// lbl_realId.Text;
                string referenceId = txtReferenceId.Text;
                string UtilityCode = txtServiceName.Text;
                int updated = datafile.UpdateUtilityTransactionReference(referenceId, utilityRef, UtilityCode);
                /////////////////////////////////////////
                if (updated > 0)
                {
                    bll.InsertIntoAuditLog(utilityRef, "UPDATE", "Resubmit DSTV Transaction with New Reference",
                        userBranch, username, bll.GetCurrentPageName(),
                        fullname + " completed Re-submit transaction with id: " + utilityRef + " from IP: " + bll.GetIPAddress());
                    ShowMessage("Customer Reference Updated Successfully", false);

                    txtUtilityRef.Text = "";
                    txtUtilityRefOld.Text = "";
                    txtBenId.Text = "";
                    txt_benName.Text = "";
                    txtTranAmount.Text = "";
                    txtTranDate.Text = "";
                    txtReferenceId.Text = "";
                    txtServiceName.Text = "";
                    lbl_serviceId.Text = "";
                    txt_status.Text = "";
                    MultiView2.ActiveViewIndex = 0;
                }
                else
                {
                    ShowMessage("Update Failed", true);
                }
            }
            catch (Exception ee)
            {

            }
        }

        protected void btn_Proceed_Click(object sender, EventArgs e)
        {

        }
    }
}