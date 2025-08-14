using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace apps
{
    public partial class ViewInvoices : System.Web.UI.Page
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
            dtable = datafile.GetActivePayTypes();
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
                LoadInvoices();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }
        private void PrintInvoice(string serialno)
        {
            LoadInvoice(serialno);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "INVOICE");
            ShowMessage(".", true);
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
        private void LoadInvoices()
        {
            string Receiptno = txtReceiptno.Text.Trim();
            string name = txtname.Text.Trim();
            string PaymentType = GetSelectedType();
            if (Receiptno.Equals("") && name.Equals(""))
            {
                ShowMessage("Invoice Number or Customer Name Required", true);
                txtReceiptno.Focus();
            }
            else
            {
                DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
                DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
                dataTable = datapay.GetInvoice(Receiptno, name, PaymentType, fromdate, todate);
                DataGrid1.DataSource = dataTable;
                DataGrid1.DataBind();
                if (dataTable.Rows.Count > 0)
                {
                    CalculateTotal(dataTable);
                    DataGrid1.Visible = true;
                    lblTotal.Visible = true;
                    ShowMessage(".", true);
                }
                else
                {
                    lblTotal.Text = ".";
                    DataGrid1.Visible = false;
                    lblTotal.Visible = false;
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
            lblTotal.Text = "Total Amount of Payments [" + total.ToString("#,##0") + "]";
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
                    string Serialno = e.Item.Cells[1].Text;
                    PrintInvoice(Serialno);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }
        protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                string districtcode = GetDistrictCode();
                string Receiptno = txtReceiptno.Text.Trim();
                string name = txtname.Text.Trim();
                string PaymentType = GetSelectedType();
                DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
                DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
                dataTable = datapay.GetInvoice(Receiptno, name, PaymentType, fromdate, todate);
                DataGrid1.CurrentPageIndex = e.NewPageIndex;
                DataGrid1.DataSource = dataTable;
                DataGrid1.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }

        }

        private string GetSelectedType()
        {
            string value = cboPaymentType.SelectedValue.ToString();
            string output = cboPaymentType.SelectedItem.ToString();
            if (value.Equals("0"))
            {
                output = "0";
            }
            return output;
        }

        protected void cboPaymentType_DataBound(object sender, EventArgs e)
        {
            cboPaymentType.Items.Insert(0, new ListItem("All Payment Types", "0"));
        }
    }

}