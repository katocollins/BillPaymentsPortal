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
using System.IO;
using InterLinkClass.EntityObjects;
using System.Threading;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class VasTransactions : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    DataTable combinedTable = new DataTable();
    private ReportDocument Rptdoc = new ReportDocument();
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
                lblTotal.Visible = false;
                Label1.Visible = false;
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
        string va = cboVendor.Text.Trim();
        if (va.Contains("CELL"))
        {
            cboUtility.Items.Clear();
            dtable = datafile.GetAllStanbicVasUtilities();
            cboUtility.DataSource = dtable;
            int foundRows = dtable.Rows.Count;
            if (foundRows > 0)
            {
                cboUtility.DataValueField = "Utility";
                cboUtility.DataTextField = "Utility";
                cboUtility.DataBind();
            }

        }
        else
        {
            dtable = datafile.GetAllUtilities("0");
            cboUtility.DataSource = dtable;
            cboUtility.DataValueField = "UtilityCode";
            cboUtility.DataTextField = "Utility";
            cboUtility.DataBind();
        }
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
        // Session["VendorCode"] = dtable.Rows[0]["VendorCode"].ToString();
        cboVendor.DataSource = dtable;
        cboVendor.DataValueField = "VendorCode";
        cboVendor.DataTextField = "Vendor";
        cboVendor.DataBind();

    }

    private void LoadPayTypes()
    {

        string va = cboVendor.Text.Trim();
        string status = cboUtility.Text.Trim();
        if (va == "CBA")
        {

            cboPaymentType.Items.Clear();
            cboPaymentType.Items.Add(new ListItem("SUCCESS"));
            cboPaymentType.Items.Add(new ListItem("PROCESSING"));
            cboPaymentType.Items.Add(new ListItem("PENDING"));
            cboPaymentType.Items.Add(new ListItem("FAILED"));
            cboPaymentType.Items.Add(new ListItem("All...."));
            cboPaymentType.SelectedValue.Equals("SUCCESS");
            cboPaymentType.Enabled = true;
        }
        else if (va.Contains("CELL")) 
        { 
             if (status.Contains("Wallet2Bank"))
             {
                 cboPaymentType.Items.Clear();
                 cboPaymentType.Items.Add(new ListItem("SUCCESS"));
                 cboPaymentType.Enabled = true;
             }
             else
             {
                 cboPaymentType.Items.Clear();
                 cboPaymentType.Items.Add(new ListItem("SUCCESS"));
                 cboPaymentType.Items.Add(new ListItem("PROCESSING"));
                 cboPaymentType.Items.Add(new ListItem("PENDING"));
                 cboPaymentType.Items.Add(new ListItem("FAILED"));
                 cboPaymentType.Items.Add(new ListItem("All...."));
                 cboPaymentType.SelectedValue.Equals("SUCCESS");
                 cboPaymentType.Enabled = true;
                 //cboPaymentType.Items.Clear();
                 //cboPaymentType.Items.Add(new ListItem("All Payment Types"));
                 //cboPaymentType.SelectedValue.Equals("All Payment Types");
                 //cboPaymentType.Enabled = false;
             }
        }
        else
        {
            dtable = datafile.GetPayTypes();
            cboPaymentType.DataSource = dtable;
            cboPaymentType.DataValueField = "PaymentCode";
            cboPaymentType.DataTextField = "PaymentType";
            cboPaymentType.DataBind();
        }


    }
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
            SearchForTransactions();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void CalculateTotal(DataTable Table)
    {
        double total = 0;
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr["Amount"].ToString());
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
        lblTotal.Text = "Total Amount [" + total.ToString("#,##0") + "]";
    }

    private void SearchForTransactions()
    {
        string utility = cboUtility.SelectedValue.ToString();
        if (txtfromDate.Text.Equals(""))
        {
            DataGrid1.Visible = false;
            ShowMessage("From Date is required", true);
            txtfromDate.Focus();
        }
        else
        {
            dataTable = GetStanbicVasTransactions(utility);

            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                CalculateTotal(dataTable);
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
                DataGrid1.Visible = true;
                string msg = "Found " + dataTable.Rows.Count + " Records Matching Search Criteria";
                //bll.ShowMessage(lblmsg, msg, false, Session);
                ShowMessage(msg, false);
            }
            else
            {
                CalculateTotal(dataTable);
                lblTotal.Text = ".";
                Label1.Text = ".";
                DataGrid1.Visible = false;
                lblTotal.Visible = false;
                Label1.Visible = false;
                MultiView1.ActiveViewIndex = -1;
                string msg = "Found " + dataTable.Rows.Count + " Records Matching Search Criteria";
                //bll.ShowMessage(lblmsg, msg, false, Session);
                ShowMessage("No Record found", true);
            }
        }
    }

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
            string utility = cboUtility.SelectedValue.ToString();
            dataTable = GetStanbicVasTransactions(utility);
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
        //string vendorcode = cboVendor.SelectedValue.ToString();
        //if (vendorcode == "CELL")
        //{
        //    //Load utilities from vas table
        //}
        //else
        //{
        //    cboUtility.Items.Insert(0, new ListItem("All Utilities", "0"));
        //}
    }
    protected void cboPaymentType_DataBound(object sender, EventArgs e)
    {
        cboPaymentType.Items.Insert(0, new ListItem("SUCCESS", "0"));
    }
    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            ConvertToFile();
        }
        catch (ThreadAbortException ex)
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
                Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "VAS TRANSACTIONS");

            }
            else
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "VAS TRANSACTIONS");

            }
        }
    }
    private void LoadRpt()
    {
        string utility = cboUtility.SelectedValue.ToString();
        dataTable = GetStanbicVasTransactions(utility);
        dataTable = formatTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\VasTransactions.rpt";

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
    public static string GetVasPaymentCode(string utilitycode)
    {
        string Paymentcode = "";
        if (utilitycode.Equals("Wallet2Bank"))
        {
            Paymentcode = "PUSH";
        }
        else
        {
            Paymentcode = "UGX";
        }
        return Paymentcode;
    }
    public static string GetVasVendorCode(string utilitycode)
    {
        string vendorcode = "";
        if (utilitycode.Equals("Wallet2Bank"))
        {
            vendorcode = "MTN";
        }
        else
        {
            vendorcode = "CELL";
        }
        return vendorcode;
    }
    public DataTable GetStanbicVasTransactions(string utilitycode)
    {
        DataTable dataTable = new DataTable();
        DataTable airtel, mtn, allUtilities = new DataTable();

        try
        {
            string vendorcode = cboVendor.SelectedValue.ToString(); //GetVasVendorCode(utilitycode);//cboVendor.SelectedValue.ToString();
            string vendorref = txtpartnerRef.Text.Trim();
            string Paymentcode = GetVasPaymentCode(utilitycode);//"UGX";//cboPaymentType.SelectedValue.ToString();
            string Account = txtAccount.Text.Trim();
            string CustName = txtCustName.Text.Trim();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string transtatus = cboPaymentType.Text.Trim();
            //string transtatus = cboTransactionStatus.Text.Trim();

            string utility = cboUtility.SelectedValue.ToString();
            //dataTable = datapay.GetVasTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, utility);
            //string utilitycode = cboUtility.SelectedValue.ToString();
            //adding the if statment
            if (vendorcode == "CBA")
            {
                if (transtatus == "All....") { transtatus = "0"; }
                dataTable = datapay.GetVasTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus, utility);

            }
            else
            {
                
                switch (utilitycode)
                {
                    case "UMEME":
                        dataTable = datapay.GetVasUMEMETransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus);
                        break;

                    case "NWSC":
                        dataTable = datapay.GetVasNWSCTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus);
                        break;
                    case "URA":
                        dataTable = datapay.GetVasURATransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus);
                        break;
                    case "Bank2Wallet":
                        dataTable = datapay.GetVasB2WTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus);
                        break;
                    case "MtnWallet2Account":
                        dataTable = datapay.GetVasW2BTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus);
                        break;
                    case "AirtelWallet2Account":
                        dataTable = datapay.GetVasAirtelW2BTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus);
                        break;
                    case "Airtime":
                        dataTable = datapay.GetVasAirtimeTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus);
                        break;
                    case "0":
                        allUtilities = datapay.GetVasAllUtilitiesTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus);
                        mtn = datapay.GetVasW2BTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus);
                        airtel = datapay.GetVasAirtelW2BTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus);
                        if (airtel == null)
                            break;
                        else
                        {
                            mtn.Merge(airtel);
                            mtn.AcceptChanges();
                        }

                        //Picked from different DB Servers thus need to combine
                        dataTable = CombineAllUtilitiesAndW2B(allUtilities, mtn);
                        break;
                    case "DSTV":
                        dataTable = datapay.GetVasDSTVUtilitiesTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus);
                        break;
                    case "GOTV":
                        dataTable = datapay.GetVasGOTVUtilitiesTransactions(vendorcode, vendorref, Account, CustName, Paymentcode, fromdate, todate, transtatus);
                        break;
                    default:
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dataTable;
    }

    private DataTable CombineAllUtilitiesAndW2B(DataTable allUtilities, DataTable mtn)
    {
        combinedTable.Columns.Add("No.");
        combinedTable.Columns.Add("ReferenceID");
        combinedTable.Columns.Add("Biller");
        combinedTable.Columns.Add("BeneficiaryID");
        combinedTable.Columns.Add("BeneficiaryName");
        combinedTable.Columns.Add("Amount");
        combinedTable.Columns.Add("PayDate");
        combinedTable.Columns.Add("PostDate");
        combinedTable.Columns.Add("Status");
        combinedTable.Columns.Add("Reason");

        if (mtn == null)
            return allUtilities;
        else if (allUtilities == null)
            return mtn;
        else
        {
            combinedTable = FillTable(combinedTable, allUtilities);
            combinedTable = FillTable(combinedTable, mtn);
        }

        return combinedTable;
    }

    private DataTable FillTable(DataTable combinedTable, DataTable toFillIn)
    {
        foreach (DataRow dr in toFillIn.Rows)
        {
            object[] values = { dr["No."].ToString(), dr["ReferenceID"], dr["Biller"], dr["BeneficiaryID"], dr["BeneficiaryName"], dr["Amount"], dr["PayDate"], dr["PostDate"], dr["Status"], dr["Reason"] };
            combinedTable.Rows.Add(values);
        }
        return combinedTable;
    }
    protected void cboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
