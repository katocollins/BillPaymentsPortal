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
using InterLinkClass.Epayment;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;

public partial class Receipt : System.Web.UI.Page
{
    private ReportDocument Rptdoc = new ReportDocument();
    Datapay datapay = new Datapay();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                if ((Session["FullName"] != null))
                {
                    if (Request.QueryString["transfereid"] != null && Request.QueryString["transferecode"] != null)
                    {
                        lblcode.Text = Request.QueryString["transfereid"].ToString();
                        lblvendorcode.Text = Request.QueryString["transferecode"].ToString();
                        //LoadReceipt();

                        string receiptno = lblcode.Text.Trim();
                        string vendorRef = lblvendorcode.Text.Trim();
                        LoadReceiptRpt(receiptno, vendorRef);
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx?login=1", false);
                }

            }
            else
            {
                //if (Request.QueryString["transfereid"] != null && Request.QueryString["transferecode"] != null)
                //{
                //    lblcode.Text = Request.QueryString["transfereid"].ToString();
                //    lblvendorcode.Text = Request.QueryString["transferecode"].ToString();
                //    //LoadReceipt();

                //    string receiptno = lblcode.Text.Trim();
                //    string vendorRef = lblvendorcode.Text.Trim();
                //    LoadReceiptRpt(receiptno, vendorRef);
                //}
            }
        }
        catch (NullReferenceException exe)
        {
            Response.Redirect("Default.aspx?login=1", false);

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
    private void LoadReceipt()
    {
        Responseobj res = new Responseobj();
        res.VendorRef = lblvendorcode.Text.Trim();
        res.Receiptno = lblcode.Text.Trim();
        dataTable = datapay.GetPaymentDetails(res);
        if (dataTable.Rows.Count > 0)
        {
            MultiView1.ActiveViewIndex = 0;
            lblRecieptno.Text = dataTable.Rows[0]["RecieptNo"].ToString();
            lblAgentRef.Text = dataTable.Rows[0]["VendorRef"].ToString();
            lblCustRef.Text = dataTable.Rows[0]["Custref"].ToString();
            lblCustname.Text = dataTable.Rows[0]["Custname"].ToString();
            lblcashier.Text = dataTable.Rows[0]["Createdby"].ToString();
            lblPaymode.Text = dataTable.Rows[0]["Paymentmode"].ToString();
            lblDistrict.Text = dataTable.Rows[0]["District"].ToString();
            DateTime paydate = DateTime.Parse(dataTable.Rows[0]["PaymentDate"].ToString());
            lblPayDate.Text = paydate.ToString("dd/MM/yyyy : HH:MM:ss");
            double bal = double.Parse(dataTable.Rows[0]["Balance"].ToString());
            double amount = double.Parse(dataTable.Rows[0]["Amount"].ToString());
            double newbal = double.Parse(dataTable.Rows[0]["NewBal"].ToString());

            lblamount.Text = amount.ToString("#,##0");
            lblbal.Text = newbal.ToString("#,##0");
            lbloldbal.Text = bal.ToString("#,##0");
            string pagefrom = Session["frompage"].ToString();
            if (pagefrom.ToUpper().Equals("MAKEPAYMENT.ASPX"))
            {
                lblTitle.Text = "PAYMENT RECEIPT";
            }
            else
            {
                lblTitle.Text = "DUPLICATE PAYMENT RECEIPT";
            }
        }
    }
    private void ShowMessage(string Message, bool Error)
    {       
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
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            string pageto = Session["frompage"].ToString();
            Response.Redirect(pageto,true);
        }
        catch (NullReferenceException exe)
        {
            Response.Redirect("Default.aspx?login=1", false);

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
            //Response.Redirect("Default.aspx?login=1", false);
        }
    }
    private void LoadReceiptRpt(string receiptno, string vendorref)
    {
        Responseobj res = new Responseobj();
        res.VendorRef = vendorref;
        res.Receiptno = receiptno;
        dataTable = datapay.GetPaymentDetails(res);
        if (dataTable.Rows.Count > 0)
        {
            string cust_type = dataTable.Rows[0]["CustType"].ToString();
            if (cust_type.ToUpper().Equals("POSTPAID"))
            {
                dataTable = formatTable(dataTable);

                MultiView1.ActiveViewIndex = 1;
                string appPath, physicalPath, rptName;
                appPath = HttpContext.Current.Request.ApplicationPath;
                physicalPath = HttpContext.Current.Request.MapPath(appPath);

                rptName = physicalPath + "\\Bin\\Reports\\Receipt.rpt";

                Rptdoc.Load(rptName);
                Rptdoc.SetDataSource(dataTable);
                CrystalReportViewer1.ReportSource = Rptdoc;
                ///Rptdoc.PrintOptions.PrinterName("");
                Rptdoc.PrintOptions.PaperSize = PaperSize.PaperEnvelopeDL;
                //Rptdoc.PrintToPrinter(1, true, 0, 0);
                //Rptdoc.Dispose();
                Hidetoolbar();
            }
            else
            {
                dtable = datapay.GetPreReceiptDetails(res);
                if (dtable.Rows.Count > 0)
                {
                    dtable = formatTokenTable(dtable);
                    MultiView1.ActiveViewIndex = 1;
                    string appPath, physicalPath, rptName;
                    appPath = HttpContext.Current.Request.ApplicationPath;
                    physicalPath = HttpContext.Current.Request.MapPath(appPath);

                    rptName = physicalPath + "\\Bin\\Reports\\TokenReceipt.rpt";

                    Rptdoc.Load(rptName);
                    Rptdoc.SetDataSource(dtable);
                    CrystalReportViewer1.ReportSource = Rptdoc;
                    ///Rptdoc.PrintOptions.PrinterName("");
                    Rptdoc.PrintOptions.PaperSize = PaperSize.PaperEnvelopeDL;
                    //Rptdoc.PrintToPrinter(1, true, 0, 0);
                    //Rptdoc.Dispose();
                    Hidetoolbar();
                }
            }
        }
    }
    private void Hidetoolbar()
    {
        CrystalReportViewer1.HasCrystalLogo = false;
        CrystalReportViewer1.HasRefreshButton = false;
        CrystalReportViewer1.HasExportButton = false;
        CrystalReportViewer1.HasPageNavigationButtons = false;
        CrystalReportViewer1.HasSearchButton = false;
        CrystalReportViewer1.HasToggleGroupTreeButton = false;
        CrystalReportViewer1.HasPrintButton = false;
        CrystalReportViewer1.HasViewList = false;
        CrystalReportViewer1.HasZoomFactorList = false;
        CrystalReportViewer1.HasGotoPageButton = false;
        CrystalReportViewer1.HasToggleGroupTreeButton = false;
        CrystalReportViewer1.DisplayGroupTree = false;
    }
    private DataTable formatTable(DataTable dataTable)
    {
        DataTable formedTable;
        string pagefrom = Session["frompage"].ToString();
        string Header = "";
        if (pagefrom.ToUpper().Equals("MAKEPAYMENT.ASPX"))
        {
            Header = "PAYMENT RECEIPT";
        }
        else
        {
            Header = "DUPLICATE PAYMENT RECEIPT";
        }
        
        DataColumn myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "Title";
        dataTable.Columns.Add(myDataColumn);
        // Add data to the new columns

        dataTable.Rows[0]["Title"] = Header;
        formedTable = dataTable;
        return formedTable;
    }
    private DataTable formatTokenTable(DataTable dataTable)
    {
        DataTable formedTable;
        string pagefrom = Session["frompage"].ToString();
        string Header = "";
        if (pagefrom.ToUpper().Equals("MAKEPAYMENT.ASPX"))
        {
            Header = "";
        }
        else
        {
            Header = "DUPLICATE TOKEN";
        }

        DataColumn myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "Str1";
        dataTable.Columns.Add(myDataColumn);
        // Add data to the new columns

        dataTable.Rows[0]["Str1"] = Header;
        formedTable = dataTable;
        return formedTable;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string receiptno = lblcode.Text.Trim();
            string vendorRef = lblvendorcode.Text.Trim();
            LoadReceiptRpt(receiptno, vendorRef);
            //Rptdoc.PrintToPrinter(1, true, 0, 0);
            
            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "RECEIPT(" + receiptno + ")");

        }
        catch (NullReferenceException exe)
        {
            Response.Redirect("Default.aspx?login=1", false);

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}
