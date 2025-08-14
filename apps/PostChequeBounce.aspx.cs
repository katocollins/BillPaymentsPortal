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
public partial class PostChequeBounce : System.Web.UI.Page
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
                LoadBanks();        
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

    private void LoadBanks()
    {
        dtable = datafile.GetActiveBanks();
        cboBanks.DataSource = dtable;
        cboBanks.DataValueField = "RecordID";
        cboBanks.DataTextField = "BankName";
        cboBanks.DataBind();
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
    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        btnPost.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnPost, "").ToString());
        
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadCheques();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadCheques()
    {
        if (cboBanks.SelectedValue.ToString().Equals("0"))
        {
            DataGrid1.Visible = false;
            ShowMessage("Please Select Cheque Bank", true);
        }
        else if (txtChequeNo.Text.Equals(""))
        {
            DataGrid1.Visible = false;
            ShowMessage("Cheque Number is required", true);
            txtChequeNo.Focus();
        }
        else if (txtaccountno.Text.Equals(""))
        {
            DataGrid1.Visible = false;
            ShowMessage("Cheque Account Number is required", true);
            txtaccountno.Focus();
        }
        else
        {
            string districtcode = GetDistrictCode();
            string ChequeNo = txtChequeNo.Text.Trim();
            string AccountNo = txtaccountno.Text.Trim();
            string BankName = cboBanks.SelectedItem.ToString();
            dataTable = datapay.GetChequesToBounce(ChequeNo, AccountNo, BankName, districtcode);
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                CalculateTotal(dataTable);
                MultiView1.ActiveViewIndex = 0;
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
        lblTotal.Text = "Cheque Total Amount [" + total.ToString("#,##0") + "]";
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
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string districtcode = GetDistrictCode();
            string ChequeNo = txtChequeNo.Text.Trim();
            string AccountNo = txtaccountno.Text.Trim();
            string BankName = cboBanks.SelectedItem.ToString();
            dataTable = datapay.GetChequesToBounce(ChequeNo, AccountNo, BankName, districtcode);
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }   
    protected void cboBanks_DataBound(object sender, EventArgs e)
    {
        cboBanks.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            string str = GetChequePaymentsStr().TrimEnd(',');
            string naration = "Bounced Cheque";
            string ret = Process.Reversestr(str, naration, true);
            if (ret.Contains("POSTED SUCCESSFULLY"))
            {
                LoadCheques();
                ShowMessage(ret, false);
            }
            else
            {                
                ShowMessage(ret, true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private string GetChequePaymentsStr()
    {
        string ItemArr = "";
        foreach (DataGridItem items in DataGrid1.Items)
        {
            string ItemFound = items.Cells[0].Text;
            ItemArr = ItemArr += ItemFound + ",";
        }
        return ItemArr;
    }
}
