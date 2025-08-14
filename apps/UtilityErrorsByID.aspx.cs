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
    public partial class UtilityErrorsByID : System.Web.UI.Page
    {
        ProcessPay Process = new ProcessPay();
        DataLogin datafile = new DataLogin();
        Datapay datapay = new Datapay();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();
        DataTable dtable = new DataTable();
        private ReportDocument Rptdoc = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    LoadVendors();
                    if (Session["AreaID"].ToString().Equals("3") || Session["AreaID"].ToString().Equals("2"))
                    {
                        cboVendor.SelectedIndex = cboVendor.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                        cboVendor.Enabled = false;
                    }

                    MultiView1.ActiveViewIndex = 0;
                    Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                    Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                    Button MenuReport = (Button)Master.FindControl("btnCalReports");
                    Button MenuRecon = (Button)Master.FindControl("btnAccounts");
                    Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                    Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                    MenuTool.Font.Underline = false;
                    MenuPayment.Font.Underline = false;
                    MenuReport.Font.Underline = true;
                    //MenuRecon.Font.Underline = false;
                    //MenuAccount.Font.Underline = false;
                    //MenuBatching.Font.Underline = false;
                    lblTotal.Visible = false;
                    DisableBtnsOnClick();
                    LoadUtilities();
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
        private void LoadUtilities()
        {
            dtable = datafile.GetAllUtilities("0");
            cboUtility.DataSource = dtable;
            cboUtility.DataValueField = "UtilityCode";
            cboUtility.DataTextField = "Utility";
            cboUtility.DataBind();

            cboUtility.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void LoadVendors()
        {
            //dtable = datafile.GetAllVendors("0");
            dtable = datafile.GetSystemCompanies("", "");
            cboVendor.DataSource = dtable;
            cboVendor.DataValueField = "CompanyCode";
            cboVendor.DataTextField = "Company";
            cboVendor.DataBind();
        }
        private void Page_Unload(object sender, EventArgs e)
        {
            if (Rptdoc != null)
            {
                Rptdoc.Close();
                Rptdoc.Clone();
                Rptdoc.Dispose();
                GC.Collect();
            }
        }
        private void DisableBtnsOnClick()
        {
            string strProcessScript = "this.value='Working...';this.disabled=true;";
            btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
            //btnConvert.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnConvert, "").ToString());

        }
        protected void cboVendor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void cboVendor_DataBound(object sender, EventArgs e)
        {

        }
        protected void DataGrid1_ItemCommand(object sender, EventArgs e)
        {

        }
        protected void DataGrid1_PageIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnConvert_Click(object sender, EventArgs e)
        {

        }
        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "viewBalance")
                {

                    txt_utilityCode.Text = e.Item.Cells[5].Text;
                    txt_utilityCode.Enabled = false;
                    txt_customerReff.Text = e.Item.Cells[0].Text;
                    txt_amountPaid.Text = e.Item.Cells[6].Text;
                    MultiView2.ActiveViewIndex = 1;
                    MultiView1.ActiveViewIndex = -1;
                    if (txt_utilityCode.Text != "NWSC")
                    {
                        txt_area.Enabled = false;
                    }
                    else
                    {
                        txt_area.Enabled = true;
                    }
                    // Label1.Text = LoadBalance(custRef);
                }
                if (e.CommandName == "resend")
                {
                    string vendortranid = "";
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }

        }
        public string LoadBalance(string custref)
        {
            string bal = "";
            DataTable dt = datapay.GetMeterBalance(custref);
            bal = dt.Rows[0]["AccountBal"].ToString();
            return bal;
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadData()
        {
            if (txtfromDate.Text.Equals(""))
            {
                DataGrid1.Visible = false;
                ShowMessage("From Date is required", true);
                txtfromDate.Focus();
            }
            else
            {
                string vendorcode = cboVendor.SelectedValue.ToString();
                string utilitycode = cboUtility.SelectedValue.ToString();
                string dataLocation = "";//ddlLocation.SelectedValue.ToString();
                string vendorref = txtpartnerRef.Text.Trim();
                DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
                DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
                string txtSearch = "";//txtSearchString.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string custRef = txtPhone.Text.Trim();

                dataTable = datapay.GetFailedDeletedErrorMsg(vendorcode, utilitycode, fromdate, todate, txtSearch, vendorref, phone, custRef, dataLocation);
                Session["utility_error_table1"] = dataTable;
                DataGrid1.CurrentPageIndex = 0;
                DataGrid1.DataSource = dataTable;
                DataGrid1.DataBind();
                MultiView2.ActiveViewIndex = 0;
            }
        }



        protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            if (Session["utility_error_table1"] != null)
            {
                DataTable dataTable = (DataTable)Session["utility_error_table1"];

                DataGrid1.DataSource = dataTable;
                DataGrid1.CurrentPageIndex = e.NewPageIndex;
                DataGrid1.DataBind();
            }
        }
        protected void ok_Click(object sender, EventArgs e)
        {
            txt_custRef.Text = "";
            txtname.Text = "";
            txtCustType.Text = "";
            txtbal.Text = "";
            txt_amountPaid.Text = "";
            MultiView2.ActiveViewIndex = 0;
            MultiView1.ActiveViewIndex = 0;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                //BusinessLogin logic = new BusinessLogin();
                string vendorCode = Session["UserBranch"].ToString();
                string utilityCode = txt_utilityCode.Text;
                string reference = txt_customerReff.Text.Trim().ToString();

                DataTable table = datafile.GetVendorCredentials(vendorCode);
                if (table.Rows.Count > 0)
                {
                    //string utilityUserName = table.Rows[0]["UtilityUsername"].ToString();
                    string utilityPassword = table.Rows[0]["VendorPassword"].ToString();
                    BusinessLogin bll = new BusinessLogin();
                    utilityPassword = bll.DecryptString(utilityPassword);
                    InterLinkClass.PegPayApi.PegPay service = new InterLinkClass.PegPayApi.PegPay();
                    InterLinkClass.PegPayApi.QueryRequest request = new InterLinkClass.PegPayApi.QueryRequest();
                    InterLinkClass.PegPayApi.Response response = new InterLinkClass.PegPayApi.Response();

                    request.QueryField1 = reference;// "65519"; //"203016921";// "P170000000086";//"2142995";//"01454392760";// "04230081848";
                    request.QueryField2 = "";///area //ACSSW4
                    request.QueryField4 = utilityCode;// "UMEME";// "STB_SCHOOL";//utilitycode//"URA";
                    request.QueryField5 = vendorCode;//vendorcode//"Micropay";//"STN";
                    request.QueryField6 = utilityPassword;

                    response = service.QueryCustomerDetails(request);

                    if (response.ResponseField6 == "0")
                    {
                        txt_custRef.Text = response.ResponseField1;
                        txtCustType.Text = response.ResponseField5;
                        txtbal.Text = response.ResponseField4;
                        txtname.Text = response.ResponseField2;
                        ShowMessage(response.ResponseField7, false);
                    }
                    else
                    {
                        ShowMessage(response.ResponseField7, true);
                    }

                }
                else
                {

                    ShowMessage("Could not load vendor credentials", true);
                }
            }
            catch (Exception ee)
            {

            }
        }
    }

}