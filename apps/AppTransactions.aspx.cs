using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AppTransactions : System.Web.UI.Page
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
               // LoadVendors();
                //if (Session["AreaID"].ToString().Equals("3"))
                //{
                //    cboVendor.SelectedIndex = cboVendor.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                //    cboVendor.Enabled = false;
                //}
               // LoadAppUtilities();
                //if (Session["AreaID"].ToString().Equals("2"))
                //{
                //    cboUtility.SelectedIndex = cboUtility.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                //    cboUtility.Enabled = false;
                //}
               // LoadPrepaidVendorStatus();
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
               // lblTotal.Visible = false;
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataGridResults.PageIndex = e.NewPageIndex;
        SearchDb();
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
    //private void LoadVendors()
    //{
    //    dtable = datafile.GetAllVendors("0");
    //    cboVendor.DataSource = dtable;
    //    cboVendor.DataValueField = "VendorCode";
    //    cboVendor.DataTextField = "Vendor";
    //    cboVendor.DataBind();
    //}
    //private void LoadPayTypes()
    //{
    //    dtable = datafile.GetPayTypes();
    //    cboPaymentType.DataSource = dtable;
    //    cboPaymentType.DataValueField = "PaymentCode";
    //    cboPaymentType.DataTextField = "PaymentType";
    //    cboPaymentType.DataBind();
    //}

    //private void LoadPrepaidVendorStatus()
    //{
    //    dtable = datafile.GetPrepaidVendorStatus();
    //    cboStatus.DataSource = dtable;
    //    cboStatus.DataValueField = "StatusDescription";  //Column name of the table.
    //    cboStatus.DataTextField = "StatusDescription";
    //    cboStatus.DataBind();
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
            if (txtfromDate.Text.Equals(""))
            {
                bll.ShowMessage(lblmsg, "From Date is required", true, Session);
                txtfromDate.Focus();

            }
            else
            {
                SearchDb();
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }
    private string[] GetSearchParameters()
    {
        List<string> searchCriteria = new List<string>();
        //string Vendor = ddVendor.SelectedValue;
        //string Utility = ddUtility.SelectedValue;
       // string TranCategory = ddTranCategory.SelectedValue;
       // string Status = ddStatus.SelectedValue;
        string Reference = txtAccount.Text;
       // string Name = txtName.Text;
        //string VendorId = txtVendorTranId.Text;
        string PegPayId = txtPegPayId.Text;
        string Phone = txtusername.Text;
        string FromDate = txtfromDate.Text;
        string ToDate = txttoDate.Text;

       // searchCriteria.Add(Vendor);
        //searchCriteria.Add(Utility);
        //searchCriteria.Add(TranCategory);
        //searchCriteria.Add(Status);
        
      //  searchCriteria.Add(Name);
       // searchCriteria.Add(VendorId);
        searchCriteria.Add(PegPayId);
        searchCriteria.Add(Phone);
        searchCriteria.Add(Reference);
        searchCriteria.Add(FromDate);
        searchCriteria.Add(ToDate);

        return searchCriteria.ToArray();
    }
    private void SearchDb()
    {
        string[] searchParams = GetSearchParameters();
        DataTable dt = bll.ExecuteAppTransactionsDataAccess(searchParams);
       // string vendorCode = Session["UserBranch"].ToString();
       
        if (dt.Rows.Count > 0)
        {
           // CalculateTotal(dt);
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();

            Multiview2.ActiveViewIndex = 0;
            string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, false, Session);
        }
        else
        {
            //CalculateTotal(dt);
            dataGridResults.DataSource = null;
            dataGridResults.DataBind();
            string msg = "No Records Found Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    //    private void LoadTransactions()
    //    {
    //        if (txtfromDate.Text.Equals(""))
    //        {
    //            DataGrid1.Visible = false;
    //            ShowMessage("From Date is required", true);
    //            txtfromDate.Focus();
    //        }
    //        else
    //        {/*string[] TobeDistinct = {"Name","City","State"};
    //DataTable dtDistinct = GetDistinctRecords(DTwithDuplicate, TobeDistinct);

    ////Following function will return Distinct records for Name, City and State column.
    //public static DataTable GetDistinctRecords(DataTable dt, string[] Columns)
    //   {
    //       DataTable dtUniqRecords = new DataTable();
    //       dtUniqRecords = dt.DefaultView.ToTable(true, Columns);
    //       return dtUniqRecords;
    //   }*/
    //            string vendorcode = cboVendor.SelectedValue.ToString();
    //            string vendorref = txtpartnerRef.Text.Trim();
    //            //string Paymentcode = cboPaymentType.SelectedValue.ToString();
    //            string status = cboStatus.SelectedValue.ToString();
    //            string Account = txtAccount.Text.Trim();
    //            string CustName = txtCustName.Text.Trim();
    //            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
    //            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
    //            //string teller = txtSearch.Text.Trim();
    //            string utility = cboUtility.SelectedValue.ToString();
    //            dataTable = datapay.GetTransactionsForPrePaidVendors(vendorcode, vendorref, Account, status, CustName, fromdate, todate, utility, Session);
    //            string[] TobeDistinct = { "VendorTranId", "CustomerRef", "status" };
    //            DataTable dtDistinct = GetDistinctRecords(dataTable, TobeDistinct);
    //            DataGrid1.CurrentPageIndex = 0;
    //            DataGrid1.DataSource = dataTable;// TobeDistinct;
    //            DataGrid1.DataBind();
    //            if (dataTable.Rows.Count > 0)
    //            {
    //                string rolecode = Session["RoleCode"].ToString();
    //                if (rolecode.Equals("004"))
    //                {
    //                    MultiView1.ActiveViewIndex = -1;
    //                }
    //                else
    //                {
    //                    MultiView1.ActiveViewIndex = 0;
    //                    CalculateTotal(dataTable);
    //                }
    //                DataGrid1.Visible = true;
    //                ShowMessage(".", true);
    //            }
    //            else
    //            {
    //                lblTotal.Text = ".";
    //                DataGrid1.Visible = false;
    //                lblTotal.Visible = false;
    //                MultiView1.ActiveViewIndex = -1;
    //                ShowMessage("No Record found", true);
    //            }
    //        }
    //    }

    private DataTable GetDistinctRecords(DataTable dataTable, string[] Columns)
    {
        DataTable dtUniqRecords = new DataTable();
        dtUniqRecords = dataTable.DefaultView.ToTable(true, Columns);
        return dtUniqRecords;
    }

    //private void CalculateTotal(DataTable Table)
    //{
    //    double total = 0;
    //    foreach (DataRow dr in Table.Rows)
    //    {
    //        double amount = double.Parse(dr["TranAmount"].ToString());
    //        total += amount;
    //    }
    //    string rolecode = Session["RoleCode"].ToString();
    //    if (rolecode.Equals("004"))
    //    {
    //        lblTotal.Visible = false;
    //    }
    //    else
    //    {
    //        lblTotal.Visible = true;
    //    }
    //    lblTotal.Text = "Total Amount of Transactions [" + total.ToString("#,##0") + "]";
    //}
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

    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
    }
    //protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    //{
    //    try
    //    {
    //        string vendorcode = cboVendor.SelectedValue.ToString();
    //        string vendorref = txtpartnerRef.Text.Trim();
    //        // string Paymentcode = cboPaymentType.SelectedValue.ToString();
    //        string status = cboStatus.SelectedValue.ToString();
    //        string Account = txtAccount.Text.Trim();
    //        string CustName = txtCustName.Text.Trim();
    //        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
    //        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
    //        //string teller = txtSearch.Text.Trim();
    //        string utility = cboUtility.SelectedValue.ToString();
    //        dataTable = datapay.GetTransactionsForPrePaidVendors(vendorcode, vendorref, Account, status, CustName, fromdate, todate, utility, Session);
    //        DataGrid1.CurrentPageIndex = e.NewPageIndex;
    //        DataGrid1.DataSource = dataTable;
    //        DataGrid1.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(ex.Message, true);
    //    }

    //}

    //protected void cboVendor_DataBound(object sender, EventArgs e)
    //{
    //    cboVendor.Items.Insert(0, new ListItem("All Agents", "0"));
    //}
    //protected void cboUtility_DataBound(object sender, EventArgs e)
    //{
    //    cboUtility.Items.Insert(0, new ListItem("All Utilities", "0"));
    //}
    //protected void cboStatus_DataBound(object sender, EventArgs e)
    //{
    //    cboStatus.Items.Insert(0, new ListItem("All Status Types", "0"));
    //}
    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            string[] searchParams = GetSearchParameters();
            DataTable dt = bll.ExecuteAppTransactionsDataAccess(searchParams);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (!rdPdf.Checked && !rdExcel.Checked)
                    {
                        bll.ShowMessage(lblmsg, "CHECK ONE EXPORT OPTION", true, Session);
                    }
                    else if (rdExcel.Checked)
                    {
                        bll.ExportToExcel(dt, Response);
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
                bll.ShowMessage(lblmsg, msg, true, Session);
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
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
    //            Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "TRANSACTIONS");

    //        }
    //    }
    //}
    //private void LoadRpt()
    //{
    //    string vendorcode = cboVendor.SelectedValue.ToString();
    //    string vendorref = txtpartnerRef.Text.Trim();
    //    // string Paymentcode = cboPaymentType.SelectedValue.ToString();
    //    string status = cboStatus.SelectedValue.ToString();
    //    string Account = txtAccount.Text.Trim();
    //    string CustName = txtCustName.Text.Trim();
    //    DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
    //    DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
    //    //string teller = txtSearch.Text.Trim();
    //    string utility = cboUtility.SelectedValue.ToString();
    //    dataTable = datapay.GetTransactionsForPrePaidVendors(vendorcode, vendorref, Account, status, CustName, fromdate, todate, utility, Session);
    //    // DataGrid1.CurrentPageIndex = e.NewPageIndex;
    //    // DataGrid1.DataSource = dataTable;
    //    //DataGrid1.DataBind();
    //    dataTable = formatTable(dataTable);
    //    string appPath, physicalPath, rptName;
    //    appPath = HttpContext.Current.Request.ApplicationPath;
    //    physicalPath = HttpContext.Current.Request.MapPath(appPath);

    //    rptName = physicalPath + "\\Bin\\Reports\\PrepaidVendorTransactionReport.rpt";

    //    Rptdoc.Load(rptName);
    //    Rptdoc.SetDataSource(dataTable);
    //    CrystalReportViewer1.ReportSource = Rptdoc;
    //    //Rptdoc.PrintToPrinter(1,true, 0,0);
    //}

    //private DataTable formatTable(DataTable dataTable)
    //{
    //    DataTable formedTable;

    //    DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
    //    DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);

    //    string agent_code = cboVendor.SelectedValue.ToString();
    //    string agent_name = cboVendor.SelectedItem.ToString();
    //    string Header = "";
    //    if (agent_code.Equals("0"))
    //    {
    //        Header = "ALL AGENTS TRANSACTION(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
    //    }
    //    else
    //    {
    //        Header = agent_name + " TRANSACTION(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
    //    }
    //    string Printedby = "Printed By : " + Session["FullName"].ToString();
    //    DataColumn myDataColumn = new DataColumn();
    //    myDataColumn.DataType = System.Type.GetType("System.String");
    //    myDataColumn.ColumnName = "DateRange";
    //    dataTable.Columns.Add(myDataColumn);

    //    myDataColumn = new DataColumn();
    //    myDataColumn.DataType = System.Type.GetType("System.String");
    //    myDataColumn.ColumnName = "PrintedBy";
    //    dataTable.Columns.Add(myDataColumn);

    //    // Add data to the new columns

    //    dataTable.Rows[0]["DateRange"] = Header;
    //    dataTable.Rows[0]["PrintedBy"] = Printedby;
    //    formedTable = dataTable;
    //    return formedTable;
    //}
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

    }
}