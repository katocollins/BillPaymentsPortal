using InterLinkClass.ControlObjects;
using InterLinkClass.CoreMerchantAPI;
using InterLinkClass.EntityObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace apps
{
public partial class ReconciledExceptions : System.Web.UI.Page
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
                //string RoleId = Session["RoleCode"].ToString();
                //if (isRoleAuthorisedToVisitPage(RoleId))
                //{
                    //LoadBanks();
                    if (Session["AreaID"].ToString().Equals("3"))
                    {
                        cboBank.SelectedIndex = cboBank.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                        cboBank.Enabled = false;
                    }
                    //LoadAccountsToReconcile(cboBank.SelectedValue.ToString(), ddOva);
                    MultiView1.ActiveViewIndex = -1;
                    lblTotal.Visible = false;
                    //DisableBtnsOnClick();
                //}
                //else
                //{
                //    Response.Redirect("UnauthorisedAccess.aspx");
                //}
                LoadData();
                //ToggleVendor();
                MultiView1.ActiveViewIndex = 0;
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

    private void LoadData()
    {
        //bll.LoadTelecoms(cboTelecom);
        bll.LoadReconUtilities(cboBank);
        ddReconType.Enabled = false;

    }

    protected void cboTelecom_SelectedIndexChanged(object sender, EventArgs e)
    {
        string vendorcode = ddVendor.SelectedValue.ToString();
        string type = cboBank.SelectedValue.ToString();
        bll.LoadAccountsToReconcile(vendorcode, type, ddOva);
    }
    protected void cboType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string vendorcode = cboTelecom.SelectedValue.ToString();
        string type = cboBank.SelectedValue.ToString();
        bll.LoadTelecomsPrepad(type, ddVendor);
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

    private void Page_Unload(object sender, EventArgs e)
    {
        if (Rptdoc != null)
        {
            Rptdoc.Close();
            Rptdoc.Dispose();
            GC.Collect();
        }
    }
    //private void LoadBanks()
    //{
    //    DataTable datatable = datafile.GetNetworkInMobileMoneyDB();
    //    cboBank.Items.Clear();
    //    cboBank.Items.Add(new ListItem("ALL", ""));
    //    foreach (DataRow dr in datatable.Rows)
    //    {
    //        string telecom = dr["Network"].ToString();
    //        cboBank.Items.Add(new ListItem(telecom, telecom));
    //    }
    //}
    private void LoadOvas()
    {
        dataTable = datapay.ExecuteDataSet("GetAllOvas").Tables[0];
        ddOva.DataSource = dataTable;
        ddOva.DataValueField = "Username";
        ddOva.DataTextField = "Username";
        ddOva.DataBind();
    }

    //private void LoadTranType()
    //{
    //    dtable = datafile.GetTranType();
    //    ddReconType.DataSource = dtable;
    //    ddReconType.DataValueField = "TypeId";
    //    ddReconType.DataTextField = "TranType";
    //    ddReconType.DataBind();
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
            LoadTransactions();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            Comment();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void Comment()
    {
        string id = txtId.Text.Trim();
        string comment = txtComment.Text.Trim();
        string user = Session["Username"].ToString();
        if (comment.Equals(""))
        {
            ShowMessage("Supply a comment", true);
        }
        else
        {
            datafile.ExecuteNonQuery("LogReconComment", id, comment, user);
            MultiView1.ActiveViewIndex = 0;
            SearchDb();
        }
    }

    private void LoadTransactions()
    {
        if (txtfromDate.Text.Equals(""))
        {
            DataGrid1.Visible = false;
            ShowMessage("From Date is required", true);
            txtfromDate.Focus();
        }
        else
        {
            SearchDb();
        }
    }

    private void SearchDb()
    {
        DataGrid1.Visible = true;
        string bank = cboBank.SelectedValue.ToString();
        string bankRef = txtBankRef.Text.Trim();
        string vendor = ddVendor.SelectedValue.ToString().Trim();
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        string account = ddOva.SelectedValue.ToString();
        string reconType = ddReconType.SelectedValue.ToString();


        // GetReconExceptions dataTable = datapay.ExecuteDataSet("SearchReconExceptions", new object[] { vendorcode, vendorref, ova, trantype, fromdate, todate }).Tables[0];
        if (account.ToUpper().Contains("STARTIMES"))
        {
            dataTable = datapay.ExecuteDataSet("GetReconExceptions1", new object[] { bank, vendor, account, reconType, bankRef, fromdate, todate }).Tables[0];
        }
        else
        {
            dataTable = datapay.ExecuteDataSet("GetReconExceptions1", new object[] { bank, vendor, account, reconType, bankRef, fromdate, todate }).Tables[0];

        }
        Session["ExceptionsTable"] = dataTable;
        DataGrid1.CurrentPageIndex = 0;
        DataGrid1.DataSource = dataTable;
        DataGrid1.DataBind();
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
                //CalculateTotal(dataTable);
            }
            DataGrid1.Visible = true;
            ShowMessage(".", true);
        }
        else
        {
            lblTotal.Text = ".";
            DataGrid1.Visible = false;
            lblTotal.Visible = false;
            MultiView1.ActiveViewIndex = -1;
            ShowMessage("No Record found", true);
        }
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

    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName != "comment") return;
        int id = Convert.ToInt32(e.CommandArgument);
        // do something
        txtId.Text = id.ToString();
        txtTranId.Text = GetTranId(id);
        MultiView1.ActiveViewIndex = 1;
        DataGrid1.Visible = false;
    }

    private string GetTranId(int id)
    {
        DataTable dt = datapay.ExecuteDataSet("getReconExceptionById", txtId.Text).Tables[0];
        return dt.Rows[0]["TranId"].ToString();
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string vendorcode = cboBank.SelectedValue.ToString();
            string vendorref = txtBankRef.Text.Trim();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string ova = ddOva.SelectedValue.ToString();
            string trantype = "";

            DataGrid1.Visible = true;
            dataTable = datapay.ExecuteDataSet("SearchReconExceptions", new object[] { vendorcode, vendorref, ova, trantype, fromdate, todate }).Tables[0];
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }

    protected void ddOva_DataBound(object sender, EventArgs e)
    {
        ddOva.Items.Insert(0, new ListItem("All Ovas", ""));
    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            ConvertToFile();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ConvertToFile()
    {
        if (rdExcel.Checked.Equals(false) && rdPdf.Checked.Equals(false))
        {
            ShowMessage("Please Check file format to Convert to", true);
        }
        else
        {
            LoadRpt();
            if (rdPdf.Checked.Equals(true))
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ReconExceptions");

            }
            else
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "ReconExceptions");

            }
        }
    }
    private void LoadRpt()
    {
        string bank = cboBank.SelectedValue.ToString();
        string bankRef = txtBankRef.Text.Trim();
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        string account = ddOva.SelectedValue.ToString();
        string vendor = ddVendor.SelectedValue.ToString().Trim();
        string reconType = ddReconType.SelectedValue.ToString();


        dataTable = Session["ExceptionsTable"] as DataTable;
        //dataTable == null ?
        dataTable = datapay.ExecuteDataSet("GetReconExceptions1", new object[] { bank, vendor, account, reconType, bankRef, fromdate, todate }).Tables[0];
        //: dataTable = Session["ExceptionsTable"] ;


        dataTable = formatTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\ReconExceptionReport.rpt";

        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
        //Rptdoc.PrintToPrinter(1,true, 0,0);
    }

    private DataTable formatTable(DataTable dataTable)
    {
        DataTable formedTable;

        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);

        string agent_code = cboBank.SelectedValue.ToString();
        string agent_name = cboBank.SelectedItem.ToString();
        string Header = "";
        if (agent_code.Equals("0"))
        {
            Header = "RECON EXCEPTIONS [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
        }
        else
        {
            Header = agent_name + " EXCEPTIONS BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
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

    //public void LoadAccountsToReconcile(string telecom, DropDownList ddlst)
    //{
    //    Datapay datapay = new Datapay();
    //    DataTable table = new DataTable();
    //    table = datapay.GetAccountsToReconcile(telecom);

    //    ddlst.Items.Clear();
    //    if (table.Rows.Count > 0)
    //    {
    //        ddlst.Items.Add(new ListItem("ALL", ""));
    //        foreach (DataRow dr in table.Rows)
    //        {
    //            string Ova = dr["AccountCode"].ToString();
    //            string SenderId = dr["AccountNumber"].ToString();
    //            ddlst.Items.Add(new ListItem(Ova + "-" + SenderId, SenderId));
    //        }
    //    }
    //}
    //protected void cboBank_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string vendorcode = cboBank.SelectedValue.ToString(); ;
    //    bll.LoadTelecomsPrepad(vendorcode, ddOva);
    //}
}
}