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
public partial class TransactionByRef : System.Web.UI.Page
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
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
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
    private void DisableBtnsOnClick()
    { 
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
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

    private void LoadTransactions()
    {
        string vendorref = txtpartnerRef.Text.Trim();
        string Receiptno = txtRecieptno.Text.Trim();
        string Account = txtAccount.Text.Trim();
        string mreceiptno = txtmreceiptNo.Text.Trim();
        if (vendorref.Equals("") && Receiptno.Equals("") && Account.Equals("") && mreceiptno.Equals(""))
        {
            ShowMessage("Please Provide a Customer account no / Agent Reference / Reciept Number", true);
            txtAccount.Focus();
        }
        else
        {
            dataTable = datapay.GetTransactionsbyRef(vendorref, Account, mreceiptno, Receiptno);
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                CalculateTotal(dataTable);
                MultiView1.ActiveViewIndex = 0;
                DataGrid1.Visible = true;
                lblTotal.Visible = true;
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
            double amount = double.Parse(dr["TranAmount"].ToString());
            total += amount;
        }
        lblTotal.Text = "Total Amount of Transactions [" + total.ToString("#,##0") + "]";
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
            string vendorref = txtpartnerRef.Text.Trim();
            string Receiptno = txtRecieptno.Text.Trim();
            string Account = txtAccount.Text.Trim();
            string mreceiptno = txtmreceiptNo.Text.Trim();
            if (vendorref.Equals("") && Receiptno.Equals("") && Account.Equals("") && mreceiptno.Equals(""))
            {
                ShowMessage("Please Provide a Customer account no / Agent Reference / Reciept Number", true);
            }
            else
            {
                dataTable = datapay.GetTransactionsbyRef(vendorref, Account, mreceiptno, Receiptno);
                DataGrid1.CurrentPageIndex = e.NewPageIndex;
                DataGrid1.DataSource = dataTable;
                DataGrid1.DataBind();
                ShowMessage(".", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

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
                Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "TRANSACTIONS");

            }
            else
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "TRANSACTIONS");

            }
        }
    }
    private void LoadRpt()
    {
        string vendorref = txtpartnerRef.Text.Trim();
        string Receiptno = txtRecieptno.Text.Trim();
        string Account = txtAccount.Text.Trim();
        string CustName = txtmreceiptNo.Text.Trim();
        dataTable = datapay.GetTransactionsbyRef(vendorref, Account, CustName, Receiptno);
        dataTable = formatTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\TransReport.rpt";

        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
    }

    private DataTable formatTable(DataTable dataTable)
    {
        DataTable formedTable;

        string Header = GetTitle();
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

    private string GetTitle()
    {
        string ret = "";
        string accountno = txtAccount.Text.Trim();
        string receiptno = txtRecieptno.Text.Trim();
        string agentref = txtpartnerRef.Text.Trim();
        string custname = txtmreceiptNo.Text.Trim();
        if (!accountno.Equals("") && !receiptno.Equals("") && !agentref.Equals("") && !custname.Equals(""))
        {
            ret = "TRANSACTION(S) BY REFERENCE";
        }
        else if (!accountno.Equals(""))
        {
            ret = "TRANSACTION(S) BY CUSTOMER ACCOUNT / INVOICE NUMBER [ " + accountno + " ]";
        }
        else if (!receiptno.Equals(""))
        {
            ret = "TRANSACTION(S) BY RECEIPT NUMBER [ " + receiptno + " ]";
        }
        else if (!agentref.Equals(""))
        {
            ret = "TRANSACTION(S) BY AGENT REFERENCE NUMBER [ " + agentref + " ]";
        }
        else if (!custname.Equals(""))
        {
            ret = "TRANSACTION(S) BY CUSTOMER NAME [ " + custname + " ]";
        }
        return ret;
    }
}
