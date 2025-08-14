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
public partial class CustContacts : System.Web.UI.Page
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
            LoadCustomerContacts();           
         }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadCustomerContacts()
    {
        string accountno = txtaccountno.Text.Trim();
        string phone = txtPhone.Text.Trim();
        PhoneValidator phonevalid = new PhoneValidator();
        if (phonevalid.PhoneNumbersOk(phone))
        {
            phone = GetPhoneNumber(phone);
            DateTime fromDate = bll.ReturnDate(txtfromdate.Text.Trim(), 1);
            DateTime toDate = bll.ReturnDate(txttodate.Text.Trim(), 2);
            dataTable = datapay.GetCustomercontacts(accountno, phone, fromDate, toDate);            
            if (dataTable.Rows.Count > 0)
            {
                DataGrid1.CurrentPageIndex = 0;
                DataGrid1.DataSource = dataTable;
                DataGrid1.DataBind();
                DataGrid1.Visible = true;
                MultiView1.ActiveViewIndex = 0;
                ShowMessage(".", true);
            }
            else
            {
                DataGrid1.Visible = false;
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("No Record found", true);
            }
        }
        else
        {
            DataGrid1.Visible = false;
            MultiView1.ActiveViewIndex = -1;
            ShowMessage("Please Enter a valid phone number", true);
            txtPhone.Focus();
        }

    }

    private string GetPhoneNumber(string phone)
    {
        string ret = phone;
        if (phone.Equals(""))
        {
            ret = phone;
        }
        else
        {
            ret = bll.FormatPhoneNumber(phone);
        }
        return ret;
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
            string accountno = txtaccountno.Text.Trim();
            string phone = txtPhone.Text.Trim();
            phone = GetPhoneNumber(phone);
            DateTime fromDate = bll.ReturnDate(txtfromdate.Text.Trim(), 1);
            DateTime toDate = bll.ReturnDate(txttodate.Text.Trim(), 2);
            dataTable = datapay.GetCustomercontacts(accountno, phone, fromDate, toDate);
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            ShowMessage(".", true);
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
        string accountno = txtaccountno.Text.Trim();
        string phone = txtPhone.Text.Trim();
        phone = GetPhoneNumber(phone);
        DateTime fromDate = bll.ReturnDate(txtfromdate.Text.Trim(), 1);
        DateTime toDate = bll.ReturnDate(txttodate.Text.Trim(), 2);
        dataTable = datapay.GetCustomercontacts(accountno, phone, fromDate, toDate);
        dataTable = formatTable(dataTable);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\PhoneContacts.rpt";

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
        string phone = txtPhone.Text.Trim();
        string custref = txtaccountno.Text.Trim();
        DateTime fromdate = bll.ReturnDate(txtfromdate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttodate.Text.Trim(), 2); 
        if (phone.Equals("") && custref.Equals(""))
        {
            ret = "CUSTOMER CONTACTS BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
        }
        else
        {
            if (!phone.Equals("") && !custref.Equals(""))
            {
                ret = "CUSTOMER CONTACTS FOR " + phone + " AND " + custref + " BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
            }
            else
            {
                if (!phone.Equals(""))
                {
                    ret = "CUSTOMER CONTACTS FOR " + phone + " BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
                }
                else
                {
                    ret = "CUSTOMER CONTACTS FOR " + custref + " BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
                }
            }

        }
        return ret;
    }

}
