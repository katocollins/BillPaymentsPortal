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

public partial class ManualReceipt : System.Web.UI.Page
{
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                MultiView2.ActiveViewIndex = 0;
                MultiView1.ActiveViewIndex = 0;
                txtDistrictName.Text = Session["DistrictName"].ToString();
                txtOfficer.Text = Session["FullName"].ToString();
                LoadCashiers();
                LoadRanges();
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
    private void LoadCashiers()
    {
        string districtcode = GetDistrictCode();
        dtable = datafile.GetCashiers(districtcode);
        cboCashier.DataSource = dtable;
        cboCashier.DataValueField = "Userid";
        cboCashier.DataTextField = "FullName";
        cboCashier.DataBind();
    }
    private void LoadRanges()
    {
        string DistrictCode = GetDistrictCode();
        dataTable = datapay.GetReceiptRanges(DistrictCode);
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
    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        Button2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button2, "").ToString());
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string code = lblCode.Text.Trim();
            string cashier = cboCashier.SelectedValue.ToString();
            string total_amount = txtTotalAmount.Text.Trim();
            string Start = txtStart.Text.Trim();
            string End = txtEnd.Text.Trim();
            string districtCode = GetDistrictCode();
            if (cashier.Equals("0"))
            {
                ShowMessage("Select Cashier to assign range to", true);
            }
            else if (Start.Equals("0"))
            {
                ShowMessage("Start Point Cannot be Zero", true);
                txtStart.Focus();
            }
            else if (Start.Equals(""))
            {
                ShowMessage("Start Point Required", true);
                txtStart.Focus();
            }
            else if (End.Equals("0"))
            {
                ShowMessage("End Point Cannot be Zero", true);
                txtEnd.Focus();
            }
            else if (End.Equals(""))
            {
                ShowMessage("End Point Required", true);
                txtEnd.Focus();
            }
            else if (total_amount.Equals("")) 
            {
                ShowMessage("Total Amount Required", true);
                txtTotalAmount.Focus(); 
            }
            else
            {
                DateTime date = DateTime.Now;
                string ret = Process.SaveReceiptRange(code, Start, End, districtCode, cashier, total_amount);
                if (!ret.Contains("Successfully"))
                {
                    ShowMessage(ret, true);
                }
                else
                {
                    LoadRanges();
                    ShowMessage(ret, false);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    //private string GetAllCashiers()
    //{
    //    string str_out = "";
    //    foreach (DropDownList list in cboCashier.Items)
    //    {
    //        string id = list.ID;
    //        str_out = str_out += id + ",";
    //    }
    //    return str_out;
    //}
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("btnEdit"))
            {
                string record_Code = e.Item.Cells[1].Text;
                string status = e.Item.Cells[7].Text;
                string levelat = e.Item.Cells[5].Text;
                if (!levelat.Equals("0"))
                {
                    ShowMessage("You cannot edit Range because it is in use", true);
                }
                else if (status.Equals("NO"))
                {
                    ShowMessage("You can only edit active Receipt Range", true);
                }
                else
                {
                    LoadControls(record_Code);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadControls(string record_Code)
    {
        int recordId = int.Parse(record_Code);
        dtable = datapay.GetReceiptRangeByID(recordId);
        if (dtable.Rows.Count > 0)
        {
            lblCode.Text = dtable.Rows[0]["RecordID"].ToString();
            txtStart.Text = dtable.Rows[0]["StartOn"].ToString();
            txtEnd.Text = dtable.Rows[0]["EndAt"].ToString();
            string cashier = dtable.Rows[0]["Cashier"].ToString();
            double amount = double.Parse(dtable.Rows[0]["TotalAmount"].ToString());
            txtTotalAmount.Text = amount.ToString("#,##0");
            cboCashier.SelectedIndex = cboCashier.Items.IndexOf(cboCashier.Items.FindByValue(cashier));
            ShowMessage(".", true);
        }
    }
    protected void cboCashier_DataBound(object sender, EventArgs e)
    {
        cboCashier.Items.Insert(0, new ListItem("Select Cashiers", "0"));
    }
}
