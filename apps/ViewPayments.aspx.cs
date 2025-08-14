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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
public partial class ViewPayments : System.Web.UI.Page
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
                LoadCashiers();
                LoadPayTypes();
                MultiView1.ActiveViewIndex = -1;
                Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                Button MenuReport = (Button)Master.FindControl("btnCalReports");
                Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                MenuTool.Font.Underline = false;
                MenuPayment.Font.Underline = true;
                MenuReport.Font.Underline = false;
                MenuRecon.Font.Underline = false;
                MenuAccount.Font.Underline = false;
                MenuBatching.Font.Underline = false;
                lblTotal.Visible = false;
                DisableBtnsOnClick();
                ToggleCashier();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ToggleCashier()
    {
        string rolecode = Session["RoleCode"].ToString();
        string cashier = Session["Username"].ToString();
        if (rolecode.Equals("004"))
        {
            cboCashier.Enabled = false;
            cboCashier.SelectedIndex = cboCashier.Items.IndexOf(cboCashier.Items.FindByValue(cashier));
        }
        else
        {
            cboCashier.Enabled = true;
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
    private void LoadCashiers()
    {
        string districtcode = GetDistrictCode();
        dtable = datafile.GetCashiers(districtcode);
        cboCashier.DataSource = dtable;
        cboCashier.DataValueField = "Username";
        cboCashier.DataTextField = "FullName";
        cboCashier.DataBind();
    }

    private string GetDistrictCode()
    {
        string ret = "0";
        string role = Session["RoleCode"].ToString();
        if (role.Equals("004") || role.Equals("005"))
        {
            ret = Session["DistrictCode"].ToString();
        }
        return ret;
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
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadPayments();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadPayments()
    {
        if (txtfromDate.Text.Equals("") && txtReceiptno.Text.Equals(""))
        {
            DataGrid1.Visible = false;
            ShowMessage("From Date is required", true);
            txtfromDate.Focus();
        }
        else
        {
            string districtcode = GetDistrictCode();
            string Receiptno = txtReceiptno.Text.Trim();
            string Paymentcode = cboPaymentType.SelectedValue.ToString();
            string Paymode = cboPaymode.SelectedValue.ToString();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string teller = cboCashier.SelectedValue.ToString();
            dataTable = datapay.GetPayments(districtcode, Receiptno, Paymentcode, Paymode, teller, fromdate, todate);
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
                    CalculateTotal(dataTable);
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
        lblTotal.Text = "Total Amount of Payments [" + total.ToString("#,##0") + "]";
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
        try
        {
            if (e.CommandName.Equals("btnPrint"))
            {
                string receiptno = e.Item.Cells[2].Text;
                string vendorRef = e.Item.Cells[3].Text;
                //LoadReceipt(receiptno, vendorRef);
                Session["frompage"] = "ViewPayments.aspx";
                Response.Redirect("./Receipt.aspx?transfereid=" + receiptno + "&transferecode=" + vendorRef, false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadReceipt(string receiptno, string vendorref)
    {
        Responseobj res = new Responseobj();
        res.VendorRef = vendorref;
        res.Receiptno = receiptno;
        dataTable = datapay.GetPaymentDetails(res);
        dataTable = formatReceiptTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\Receipt.rpt";

        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
        Rptdoc.PrintOptions.PaperSize = PaperSize.PaperEnvelopeDL;
        Rptdoc.PrintToPrinter(1, true, 0, 0);
        Rptdoc.Dispose();
    }
    private DataTable formatReceiptTable(DataTable dataTable)
    {
        DataTable formedTable;
        string Header = "RE-PRINTED PAYMENT RECEIPT";
        DataColumn myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "Title";
        dataTable.Columns.Add(myDataColumn);
        // Add data to the new columns

        dataTable.Rows[0]["Title"] = Header;
        formedTable = dataTable;
        return formedTable;
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string districtcode = GetDistrictCode();
            string Receiptno = txtReceiptno.Text.Trim();
            string Paymentcode = cboPaymentType.SelectedValue.ToString();
            string Paymode = cboPaymode.SelectedValue.ToString();
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string teller = cboCashier.SelectedValue.ToString();
            dataTable = datapay.GetPayments(districtcode, Receiptno, Paymentcode, Paymode, teller, fromdate, todate);
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
        cboCashier.Items.Insert(0, new ListItem("All Cashiers", "0"));
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

    private void ConvertToFile()
    {
        if (rdExcel.Checked.Equals(false) && rdPdf.Checked.Equals(false))
        {
            ShowMessage("Please Check file format to Convert to", true);
        }
        else
        {
            LoadRpt();
        }
    }
    private void LoadRpt()
    {
        string districtcode = GetDistrictCode();
        string Receiptno = txtReceiptno.Text.Trim();
        string Paymentcode = cboPaymentType.SelectedValue.ToString();
        string Paymode = cboPaymode.SelectedValue.ToString();
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        string teller = cboCashier.SelectedValue.ToString();
        dataTable = datapay.GetPayments(districtcode, Receiptno, Paymentcode, Paymode, teller, fromdate, todate);
        dataTable = formatTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);
        rptName = physicalPath + "\\bin\\reports\\Payments.rpt"; 
        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;

        Response.Buffer = false;
        Response.ClearContent();
        Response.ClearHeaders();
        if (rdPdf.Checked.Equals(true))
        {
            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "PAYMENTS");

        }
        else
        {
            Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "PAYMENTS");

        }
    }

    private DataTable formatTable(DataTable dataTable)
    {
        DataTable formedTable;
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);

        string Header = "PAYMENT(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
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
    protected void cboCashier_DataBound(object sender, EventArgs e)
    {
        cboCashier.Items.Insert(0, new ListItem("All Cashiers", "0"));
    }
}
