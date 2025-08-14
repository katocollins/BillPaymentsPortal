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

public partial class Invoice : System.Web.UI.Page
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
                MultiView2.ActiveViewIndex = 0;
                MultiView3.ActiveViewIndex = 0;
                Button1.Enabled = false;
                txtofficer.Text = Session["FullName"].ToString();
                LoadPayTypes();
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
    private void LoadPayTypes()
    {
        dtable = datafile.GetActivePayTypes();
        cboPayTypes.DataSource = dtable;
        cboPayTypes.DataValueField = "ShortName";
        cboPayTypes.DataTextField = "PaymentType";
        cboPayTypes.DataBind();
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
    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        Button1.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button1, "").ToString());
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
           
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            PostInvoiceDetails();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void PostInvoiceDetails()
    {
        InvoiceTran inv = new InvoiceTran();
        inv.Fname = txtfname.Text.Trim().ToUpper();
        inv.Mname = txtmName.Text.Trim().ToUpper();
        inv.Lname = txtlname.Text.Trim().ToUpper();
        inv.Phone = txtPhone.Text.Trim();
        inv.Email = txtemail.Text.Trim();
        inv.ShortName = cboPayTypes.SelectedValue.ToString();
        inv.PayType = cboPayTypes.SelectedItem.ToString();
        string amount = txtamount.Text.Trim();
        if (inv.Fname.Equals(""))
        {
            ShowMessage("Please Provide Customer First Name", true);
            txtfname.Focus();
        }
        else if (inv.Lname.Equals(""))
        {
            ShowMessage("Please Provide Customer Last Name", true);
            txtlname.Focus();
        }   
        else if (amount.Equals(""))
        {
            ShowMessage("Please Provide Invoice amount", true);
            txtamount.Focus();
        }
        else if (amount.Equals("0"))
        {
            ShowMessage("Invoice amount cannot be Zero", true);
            txtamount.Focus();
        }
        else
        {
            inv.Vatable = GetVatStatus();
            inv.Amount = double.Parse(amount);
            InvoiceTran ret = new InvoiceTran();
            ret = Process.SaveInvoiceDetails(inv);

            if (ret.ErrorCode.Equals("0"))
            {
                DisplayInvoice(ret);
                txtamount.Text = "";
                txtemail.Text = "";
                txtfname.Text = "";
                txtlname.Text = "";
                txtmName.Text = "";
                txtpayingfor.Text = "";
                txtPhone.Text = "";
                cboPayTypes.SelectedIndex = cboPayTypes.Items.IndexOf(cboPayTypes.Items.FindByValue("0"));
            }
            else
            {
                ShowMessage(ret.Error, true);
            }
        }
    }

    private bool GetVatStatus()
    {
        string status = lblvat.Text.Trim();
        if (status.Equals("True"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DisplayInvoice(InvoiceTran ret)
    {
        ShowMessage(ret.Error, false);
        MultiView3.ActiveViewIndex = 1;
        MultiView2.ActiveViewIndex = -1;
        LoadInvoice(ret.InvoiceSerial);
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
    private void LoadInvoice(string InvSerial)
    {
        dataTable = datapay.GetInvoiceDetails(InvSerial);
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\Invoice.rpt";

        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dataTable);
        CrystalReportViewer1.ReportSource = Rptdoc;
        Hidetoolbar();
        lblcode.Text = InvSerial;
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView3.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = 0;
            lblcode.Text = "0";
            
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void cboPayType_DataBound(object sender, EventArgs e)
    {
        cboPayTypes.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void cboPayTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string paytypecode = cboPayTypes.SelectedValue.ToString();
            if (paytypecode.Equals("0"))
            {
                Button1.Enabled = false; 
            }
            else
            {
                dataTable = datafile.GetPayTypesByShortName(paytypecode);
                if (dataTable.Rows.Count > 0)
                {
                    Button1.Enabled = true;
                    string name = dataTable.Rows[0]["PaymentType"].ToString();
                    bool Vatable = bool.Parse(dataTable.Rows[0]["Vatable"].ToString());
                    double amount = double.Parse(dataTable.Rows[0]["Amount"].ToString());

                    txtamount.Text = amount.ToString("#,##0");
                    lblvat.Text = Vatable.ToString();
                    txtpayingfor.Text = name;
                }
                else
                {
                    Button1.Enabled = false; 
                }

            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        try
        {
            string serialno = lblcode.Text.Trim();
            LoadInvoice(serialno);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "INVOICE");
            ShowMessage(".",true);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}
