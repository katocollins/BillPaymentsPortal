using InterLinkClass.ControlObjects;
using InterLinkClass.CoreMerchantAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class AgentCredit : System.Web.UI.Page
    {
        ProcessPay Process = new ProcessPay();
        DataLogin datafile = new DataLogin();
        Datapay datapay = new Datapay();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();
        DataTable dtable = new DataTable();
        // private ReportDocument Rptdoc = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    LoadVendors();
                    Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                    Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                    Button MenuReport = (Button)Master.FindControl("btnCalReports");
                    Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                    Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                    Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                    MenuTool.Font.Underline = false;
                    MenuPayment.Font.Underline = false;
                    MenuReport.Font.Underline = true;
                    MenuRecon.Font.Underline = false;
                    MenuAccount.Font.Underline = false;
                    MenuBatching.Font.Underline = false;
                    lblTotal.Visible = false;
                    DisableBtnsOnClick();
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }

        }

        private void LoadVendors()
        {
            dtable = datafile.GetAllVendors("0");
            cboVendor.DataSource = dtable;
            cboVendor.DataValueField = "VendorCode";
            cboVendor.DataTextField = "Vendor";
            cboVendor.DataBind();
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CalculateAgentAccountBalance();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        protected void cboVendor_DataBound(object sender, EventArgs e)
        {
            cboVendor.Items.Insert(0, new ListItem("All Agents", "0"));
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

        private void CalculateAgentAccountBalance()
        {
            double total = 0;
            string vendorcode = cboVendor.SelectedValue.ToString().Trim();
            dataTable = datapay.GetAgentCurrentCredit(vendorcode);
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    double amount = double.Parse(dr["AccountBalance"].ToString());
                    total += amount;
                }
                ShowMessage(".", false);
                lblTotal.Visible = true;
                lblTotal.Text = "Account Balance For " + cboVendor.SelectedItem + " is [" + total.ToString("#,##0") + "]";
            }
            else
            {
                lblTotal.Text = "";
                ShowMessage("No Record found", true);
            }

        }

        private void DisableBtnsOnClick()
        {
            string strProcessScript = "this.value='Working...';this.disabled=true;";
            btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        }

    }
}