using InterLinkClass.ControlObjects;
using InterLinkClass.CoreMerchantAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
public partial class ReconciliationTransactionReport : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    //private ReportDocument Rptdoc = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                LoadVendors();
                if (Session["AreaID"].ToString().Equals("3"))
                   {
                        cboVendor.SelectedIndex = cboVendor.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                        cboVendor.Enabled = false;
                  }
                loadstatus();
                LoadUtilities();
                LoadReconStatus();
                MultiView1.ActiveViewIndex = -1;
                lblTotal.Visible = false;
                Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                Button MenuReport = (Button)Master.FindControl("btnCalReports");
                Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                MenuTool.Font.Underline = false;
                MenuPayment.Font.Underline = false;
                MenuReport.Font.Underline = false;
                MenuRecon.Font.Underline = true;
                MenuAccount.Font.Underline = false;
                MenuBatching.Font.Underline = false;
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private bool isRoleAuthorisedToVisitPage(string RoleId)
    {
        bool authorised = false;
        DataTable dt = (DataTable)Session["RolePageMatrix"];
        ArrayList roleIds = new ArrayList();
        string currenePage = GetCurrentPage();
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string page = dr["OtherPages"].ToString();
                if (page.Trim().ToUpper().Equals(currenePage.Trim().ToUpper()))
                {
                    roleIds.Add(dr["RoleId"].ToString().Trim());
                }
            }
            if (roleIds.Contains(RoleId.Trim()))
            {
                authorised = true;
            }
        }
        return authorised;
    }
    private string GetCurrentPage()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        return sRet;
    }
    protected void Gvuploadedreading_RowDataBound(object o, GridViewRowEventArgs e)
    {
        //Assumes the Price column is at index 4
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
    }
    private void loadstatus()
    {
        ddlstatus.Items.Clear();
        ddlstatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("SUCCESS", "SUCCESS"));
        ddlstatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("FAILED", "FAILED"));
        //ddlstatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("INSERTED PUSH", "INSERTED PUSH"));
        ddlstatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("PENDING", "PENDING"));
        ddlstatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("REVERSED", "REVERSED"));
        ddlstatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("ALL", ""));

    }

    private void LoadUtilities()
    {
        ddlOvas.Items.Insert(0, new ListItem("SMS", "SMS"));
        ddlOvas.Items.Insert(0, new ListItem("AIRTIME", "AIRTIME"));
        ddlOvas.Items.Insert(0, new ListItem("NWSC", "NWSC"));
        ddlOvas.Items.Insert(0, new ListItem("UMEME", "UMEME"));
        ddlOvas.Items.Insert(0, new ListItem("STARTIMES", "STARTIMES"));
    }

    private void LoadReconStatus()
    {
              
        ddReconStatus.Items.Insert(0, new ListItem("UNRECONCILED", "NULL"));
        ddReconStatus.Items.Insert(0, new ListItem("RECONCILED", "1"));
        ddReconStatus.Items.Insert(0, new ListItem("ALL", ""));

    }
    //private void LoadTranType()
    //{
    //    dtable = datafile.GetTranType();
    //    cboTranType.DataSource = dtable;
    //    cboTranType.DataValueField = "TypeId";
    //    cboTranType.DataTextField = "TranType";
    //    cboTranType.DataBind();
    //}
    //private void Page_Unload(object sender, EventArgs e)
    //{
    //    if (Rptdoc != null)
    //    {
    //        Rptdoc.Close();
    //        Rptdoc.Dispose();
    //        GC.Collect();
    //    }
    //}
    private void LoadVendors()
    {
        //dtable = datafile.GetAllVendors("0");
        dtable = datafile.GetPegasusActiveVendors();
        cboVendor.DataSource = dtable;
        cboVendor.DataValueField = "VendorCode";
        cboVendor.DataTextField = "Vendor";
        cboVendor.DataBind();
    }
    //private void LoadPayTypes()
    //{
    //    dtable = datafile.GetPayTypes();
    //    cboPaymentType.DataSource = dtable;
    //    cboPaymentType.DataValueField = "PaymentCode";
    //    cboPaymentType.DataTextField = "PaymentType";
    //    cboPaymentType.DataBind();
    //}
    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        //btnConvert.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnConvert, "").ToString());

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            dataTable = LoadTransactions();
            Gvuploadedreading.Visible = true;
            Gvuploadedreading.DataSource = dataTable;
            Gvuploadedreading.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                string rolecode = Session["RoleCode"].ToString();
                if (rolecode.Equals("004"))
                {
                    MultiView1.ActiveViewIndex = -1;
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                    CalculateTotal(dataTable);
                }
                //DataGrid1.Visible = true;
                ShowMessage(".", true);
            }
            else
            {
                lblTotal.Text = ".";
                Gvuploadedreading.Visible = false;
                lblTotal.Visible = false;
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("No Record found", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private DataTable LoadTransactions()
    {
        if (txtfromDate.Text.Equals(""))
        {
            Gvuploadedreading.Visible = false;
            ShowMessage("From Date is required", true);
            txtfromDate.Focus();
        }
        else
        {
            string vendorcode = cboVendor.SelectedValue.ToString();
            string vendortrans = txtvendortransid.Text.Trim();
            string Status = ddlstatus.SelectedValue.ToString();
            string Phonenumber = txtphonenumber.Text.Trim();
            // string CustName = txtCustName.Text.Trim();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string telecomid = txttelecomid.Text.Trim();
            string ova = ddlOvas.SelectedValue.ToString();
            string reconStatus = ddReconStatus.SelectedValue.ToString();
            if (ova.ToUpper().Equals("STARTIMES"))
            {
                dataTable = datapay.GetReconciledTransactionsReportPostpaid(vendorcode, vendortrans, Status, Phonenumber, fromdate, todate, telecomid, ova, reconStatus);
            }
            else
            {
                dataTable = datapay.GetReconciledTransactionsReport(vendorcode, vendortrans, Status, Phonenumber, fromdate, todate, telecomid, ova, reconStatus);
            }

        }
        return dataTable;
    }

    private void CalculateTotal(DataTable Table)
    {
        double total = 0;
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr["TranAmount"].ToString());
            total += amount;
        }
        string rolecode = Session["RoleCode"].ToString();
        if (rolecode.Equals("004"))
        {
            lblTotal.Visible = false;
        }
        else
        {
            lblTotal.Visible = true;
        }
        lblTotal.Text = "Total Amount of Transactions [" + total.ToString("#,##0") + "]";
    }
    //private void LoadUsers()
    //{

    //    DataGrid1.DataSource = dataTable;
    //    DataGrid1.DataBind();
    //}
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

    //protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    //{
    //}
    //protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    //{
    //    try
    //    {
    //        string vendorcode = cboVendor.SelectedValue.ToString();
    //        string vendorref = txtpartnerRef.Text.Trim();
    //        string Paymentcode = cboPaymentType.SelectedValue.ToString();
    //        string Account = txtAccount.Text.Trim();
    //        string CustName = txtCustName.Text.Trim();
    //        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
    //        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
    //        string teller = txtSearch.Text.Trim();
    //        string utility = cboTranType.SelectedValue.ToString();
    //        dataTable = datapay.GetReconciledTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, teller, fromdate, todate, utility);
    //        DataGrid1.CurrentPageIndex = e.NewPageIndex;
    //        DataGrid1.DataSource = dataTable;
    //        DataGrid1.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, true);
    //    }

    //}

    protected void cboVendor_DataBound(object sender, EventArgs e)
    {
        cboVendor.Items.Insert(0, new ListItem("All Vendors", ""));
    }
    protected void ddlOvas_DataBound(object sender, EventArgs e)
    {
        ddlOvas.Items.Insert(0, new ListItem("All Utilities", ""));

    }

    protected void Gvuploadedreading_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gvuploadedreading.PageIndex = e.NewPageIndex;
        DataTable dt = LoadTransactions();
        Gvuploadedreading.DataSource = dt;
        //Gvuploadedreading.DataBind();
        //loadreading();
    }
    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = LoadTransactions();
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (!rdPdf.Checked && !rdExcel.Checked)
                    {
                        ShowMessage("CHECK ONE EXPORT OPTION", true);
                    }
                    else if (rdExcel.Checked)
                    {
                        bll.ExportToExcel(dt, "", Response);
                    }
                    else if (rdPdf.Checked)
                    {
                        bll.ExportToPdf(dt, "", Response);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                string msg = "No Records Found Matching Search Criteria";
                ShowMessage(msg, true);
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            ShowMessage(msg, true);
        }
    }




    private DataTable formatTable(DataTable dataTable)
    {
        DataTable formedTable;

        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);

        string agent_code = cboVendor.SelectedValue.ToString();
        string agent_name = cboVendor.SelectedItem.ToString();
        string Header = "";
        if (agent_code.Equals("0"))
        {
            Header = "ALL AGENTS TRANSACTION(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
        }
        else
        {
            Header = agent_name + " TRANSACTION(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
        }
        string Printedby = "Printed By : " + Session["FullName"].ToString();
        DataColumn myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "DateRange";
        dataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "PrintedBy";
        dataTable.Columns.Add(myDataColumn);

        // Add data to the new columns

        dataTable.Rows[0]["DateRange"] = Header;
        dataTable.Rows[0]["PrintedBy"] = Printedby;
        formedTable = dataTable;
        return formedTable;
    }

}
}