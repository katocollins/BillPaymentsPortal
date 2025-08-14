using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class UpdateUmemeConnection : System.Web.UI.Page
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
            dtable = datafile.GetAllUmemeConnections();
            cboUtility.DataSource = dtable;
            cboUtility.DataValueField = "ConnectionType";
            cboUtility.DataTextField = "Name";
            cboUtility.DataBind();

            activebanks = datafile.GetActiveUmemeConnection();
            if (activebanks.Rows.Count > 0)
            {
                DataGrid1.DataSource = activebanks;
                DataGrid1.DataBind();
                MultiView2.ActiveViewIndex = 0;

            }
            else
            {
                ShowMessage("No Active Bank Found", true);
            }


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {

                string connectioncode = cboUtility.SelectedItem.Value.ToString();
                //CorrectCustomerRef = txtCustomerRef.Text.Trim();
                if (string.IsNullOrEmpty(connectioncode))
                {
                    ShowMessage("Please select connection", true);
                }

                datafile.UpdateUmemeConnection(connectioncode);
                Response.Redirect(Request.RawUrl);
                ShowMessage("Active connection set Successfully to " + connectioncode, false);

            }
            catch (Exception ee)
            {
                ShowMessage("Active connection set Successfully", false);
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

        protected void btn_Proceed_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Success_Click(object sender, EventArgs e)
        {

        }
    }
}