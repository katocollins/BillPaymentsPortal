using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using InterLinkClass.EntityObjects;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;

public partial class FailedTransactions : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    private ReportDocument Rptdoc = new ReportDocument();
    public static int go;
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
                LoadUtilities();
                if (Session["AreaID"].ToString().Equals("2"))
                {
                    cboUtility.SelectedIndex = cboUtility.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                    cboUtility.Enabled = false;
                }
                LoadPayTypes();
                MultiView1.ActiveViewIndex = -1;
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
                //lblTotal.Visible = false;
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadUtilities()
    {
        dtable = datafile.GetAllUtilities("0");
        cboUtility.DataSource = dtable;
        cboUtility.DataValueField = "UtilityCode";
        cboUtility.DataTextField = "Utility";
        cboUtility.DataBind();

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
    private void LoadVendors()
    {
        dtable = datafile.GetAllVendors("0");
        cboVendor.DataSource = dtable;
        cboVendor.DataValueField = "VendorCode";
        cboVendor.DataTextField = "Vendor";
        cboVendor.DataBind();
    }
    private void LoadPayTypes()
    {
        dtable = datafile.GetPayTypes();
        cboPaymentType.DataSource = dtable;
        cboPaymentType.DataValueField = "PaymentCode";
        cboPaymentType.DataTextField = "PaymentType";
        cboPaymentType.DataBind();
    }
    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        //btnConvert.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnConvert, "").ToString());

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            LoadFailedTransactions();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    string vendorcode = null;
    string vendorref = null;
    string Paymentcode = null;
    string Account = null;
    string CustName = null;
    DateTime fromdate = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
    //DateTime todate=(DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
    string teller = null;
    string utility = null;

    private void LoadFailedTransactions()
    {
        if (txtfromDate.Text.Equals(""))
        {
            DataGrid1.Visible = false;
            ShowMessage("From Date is required", true);
            txtfromDate.Focus();
        }
        else
        {
            vendorcode = cboVendor.SelectedValue.ToString();
            vendorref = txtpartnerRef.Text.Trim();
            Paymentcode = cboPaymentType.SelectedValue.ToString();
            Account = txtAccount.Text.Trim();
            CustName = txtCustName.Text.Trim();
            fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            //todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            teller = txtSearch.Text.Trim();
            utility = cboUtility.SelectedValue.ToString();
            dataTable = datapay.GetFailedTransactions(vendorcode, vendorref, teller, fromdate, Paymentcode, Account, CustName, utility);
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

                }
                DataGrid1.Visible = true;
                //Button1.Visible = true;
                btnDelete.Visible = true;
                ShowMessage(".", true);
            }
            else
            {

                DataGrid1.Visible = false;
                //Button1.Visible = false;
                btnDelete.Visible = false;
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("No Record found", true);
            }
        }
    }


    protected void btnRemove_Click(object sender, EventArgs e)
    {

        int count = 0;
        GetData();
        //SetData();
        LoadFailedTransactions();
        //DataGrid1.AllowPaging = false;

        //DataGrid1.DataBind();
        DataGrid1.CurrentPageIndex = 0;
        DataGrid1.DataSource = dataTable;
        DataGrid1.DataBind();
        DataGrid1.Visible = true;
        // gvAll.DataBind();

        ArrayList arr = (ArrayList)ViewState["SelectedRecords"];

        count = arr.Count;


        // Iterate through the Items.Rows property
        for (int i = 0; i < DataGrid1.Items.Count; i++)
        {
            DataGridItem item = DataGrid1.Items[i];

            if (arr.Contains(item.Cells[1].Text))
            {

                DeleteRecord(item.Cells[1].Text.ToString());

                arr.Remove(item.Cells[1].Text);


            }


        }
        ViewState["SelectedRecords"] = arr;

        hfCount.Value = "0";

        //DataGrid1.AllowPaging = true;

        //BindGrid();

        ShowDeleteMessage(count);
        LoadFailedTransactions();

    }//End of Remove Button


    //private void SetData()
    //{

    //    int currentCount = 0;

    //    ArrayList arr = (ArrayList)ViewState["SelectedRecords"];

    //    for (int i = 0; i < DataGrid1.Items.Count; i++)
    //    {
    //        DataGridItem item = DataGrid1.Items[i];
    //        CheckBox chkAll = ((CheckBox)(item.FindControl("checkAll")));

    //        if (chkAll != null && chkAll.Checked)
    //        {

    //            chkAll.Checked = true;
    //        }


    //        HtmlInputCheckBox chk = ((HtmlInputCheckBox)(item.FindControl("SelectCheckBox")));

    //        if (chk != null && chk.Checked)
    //        {

    //            chk.Checked = arr.Contains(item.Cells[i].Text);

    //            if (!chk.Checked && chkAll != null)

    //                chkAll.Checked = false;

    //            else

    //                currentCount++;

    //        }

    //    }

    //    hfCount.Value = (arr.Count - currentCount).ToString();

    //}


    //The GetData function simply retrieves the records for which the user has checked the checkbox, adds them to an ArrayList and then saves the ArrayList to ViewState 
    private void GetData()
    {

        ArrayList arr;

        if (ViewState["SelectedRecords"] != null)
        {

            arr = (ArrayList)ViewState["SelectedRecords"];
        }
        else
        {

            arr = new ArrayList();

        }
        go = DataGrid1.Items.Count;
        for (int i = 0; i < DataGrid1.Items.Count; i++)
        {

            DataGridItem item = DataGrid1.Items[i];
            CheckBox chkAll = ((CheckBox)(item.FindControl("checkAll")));
            //CheckBox chkAll = (CheckBox)item.Cells[0].FindControl("chkAll");

            //DataGridItem item = DataGrid1.Items[i];
            //                // Access the CheckBox
            //                CheckBox cb = ((CheckBox)(item.FindControl("SelectCheckBox")));
            if (chkAll != null && chkAll.Checked)
            {
                //tranid = item.Cells[1].Text;
                if (!arr.Contains(item.Cells[1].Text))
                {

                    arr.Add(item.Cells[1].Text);

                }

            }

            else
            {
                //                CheckBox cb = ((CheckBox)(item.FindControl("SelectCheckBox")));
                HtmlInputCheckBox chk = ((HtmlInputCheckBox)(item.FindControl("SelectCheckBox")));

                if (chk != null && chk.Checked)
                {

                    if (!arr.Contains(item.Cells[1].Text))
                    {

                        arr.Add(item.Cells[1].Text);

                    }

                }

                else
                {

                    if (arr.Contains(item.Cells[1].Text))
                    {

                        arr.Remove(item.Cells[1].Text);

                    }

                }

            }

        }

        ViewState["SelectedRecords"] = arr;

    }//End of GetData

    //Delete The Record
    private void DeleteRecord(string TranId)
    {
        int insertsuccesscount = 0;
        try
        {
            insertsuccesscount = datapay.GetFailedTransactionsWhere(TranId);
            if (insertsuccesscount > 0)
            {

                ShowMessage(" Inserted and Deleted", true);

            }
            else
            {
                ShowMessage(" Not Inserted,not Deleted", true);
            }
        }
        catch (Exception ex)
        {

            throw ex;

        }

    }//End of Delete Record

    //Show Delete Message

    private void ShowDeleteMessage(int count)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("alert('");

        sb.Append(count.ToString());

        sb.Append(" record(s) has been Transfered.');");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(),

                        "script", sb.ToString());

    }//End of ShowDeleteMsg 





    private void LoadUsers()
    {

        DataGrid1.DataSource = dataTable;
        DataGrid1.DataBind();
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
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            vendorcode = cboVendor.SelectedValue.ToString();
            vendorref = txtpartnerRef.Text.Trim();
            Paymentcode = cboPaymentType.SelectedValue.ToString();
            Account = txtAccount.Text.Trim();
            CustName = txtCustName.Text.Trim();
            fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            //todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            teller = txtSearch.Text.Trim();
            utility = cboUtility.SelectedValue.ToString();
            dataTable = datapay.GetFailedTransactions(vendorcode, vendorref, teller, fromdate, Paymentcode, Account, CustName, utility);
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();


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
    protected void cboUtility_DataBound(object sender, EventArgs e)
    {
        cboUtility.Items.Insert(0, new ListItem("All Utilities", "0"));
    }
    protected void cboPaymentType_DataBound(object sender, EventArgs e)
    {
        cboPaymentType.Items.Insert(0, new ListItem("All Payment Types", "0"));
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

    //private void ConvertToFile()
    //{
    //    if (rdExcel.Checked.Equals(false) && rdPdf.Checked.Equals(false))
    //    {
    //        ShowMessage("Please Check file format to Convert to", true);
    //    }
    //    else
    //    {
    //        LoadRpt();
    //        if (rdPdf.Checked.Equals(true))
    //        {
    //            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "TRANSACTIONS");

    //        }
    //        else
    //        {
    //            try
    //            {
    //                Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "TRANSACTIONS");
    //            }
    //            catch (System.Threading.ThreadAbortException ex)
    //            {

    //            }

    //        }
    //    }
    //}
    private void ConvertToFile()
    {
        try
        {
            if (rdExcel.Checked.Equals(false) && rdPdf.Checked.Equals(false))
            {
                ShowMessage("Please Check file format to Convert to", true);
            }
            else
            {
                DataTable dt = LoadRpt();

                //try
                //{
                if (rdPdf.Checked.Equals(true))
                {
                    // Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "TRANSACTIONS");
                    bll.ExportToPdfNew(dt, "PENDINGTRANSACTIONS", Response);
                }
                else
                {
                    // Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "TRANSACTIONS");
                    bll.ExportToExcelNew(dt, "PENDINGTRANSACTIONS", Response);
                }
                //}
                //finally
                //{
                //    Rptdoc.Close();
                //    Rptdoc.Clone();
                //    Rptdoc.Dispose();              
                //}
            }
        }
        catch(Exception ex)
        {
            ShowMessage("FAILED TO CONVERT REPORT", true);
        }
    }

    private DataTable LoadRpt()
    {
        vendorcode = cboVendor.SelectedValue.ToString();
        vendorref = txtpartnerRef.Text.Trim();
        Paymentcode = cboPaymentType.SelectedValue.ToString();
        Account = txtAccount.Text.Trim();
        CustName = txtCustName.Text.Trim();
        fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        //todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        teller = txtSearch.Text.Trim();
        utility = cboUtility.SelectedValue.ToString();
        dataTable = datapay.GetFailedTransactionsForReport(vendorcode, vendorref, teller, fromdate, Paymentcode, Account, CustName, utility);
       // dataTable = formatTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\TransReport.rpt";

        return dataTable;
        //Rptdoc.Load(rptName);
        //Rptdoc.SetDataSource(dataTable);
        //CrystalReportViewer1.ReportSource = Rptdoc;
        //Rptdoc.PrintToPrinter(1,true, 0,0);
    }
    //private void LoadRpt()
    //{
    //    vendorcode = cboVendor.SelectedValue.ToString();
    //    vendorref = txtpartnerRef.Text.Trim();
    //    Paymentcode = cboPaymentType.SelectedValue.ToString();
    //    Account = txtAccount.Text.Trim();
    //    CustName = txtCustName.Text.Trim();
    //    fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
    //    //todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
    //    teller = txtSearch.Text.Trim();
    //    utility = cboUtility.SelectedValue.ToString();
    //    dataTable = datapay.GetFailedTransactions(vendorcode, vendorref, teller, fromdate, Paymentcode, Account, CustName, utility);
    //    dataTable = formatTable(dataTable);
    //    string appPath, physicalPath, rptName;
    //    appPath = HttpContext.Current.Request.ApplicationPath;
    //    physicalPath = HttpContext.Current.Request.MapPath(appPath);

    //    rptName = physicalPath + "\\Bin\\Reports\\TransReport.rpt";

    //    Rptdoc.Load(rptName);
    //    Rptdoc.SetDataSource(dataTable);
    //    CrystalReportViewer1.ReportSource = Rptdoc;
    //    //Rptdoc.PrintToPrinter(1,true, 0,0);
    //}

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
    protected void txtpartnerRef_TextChanged(object sender, EventArgs e)
    {

    }
    protected void DataGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {

    }
}
