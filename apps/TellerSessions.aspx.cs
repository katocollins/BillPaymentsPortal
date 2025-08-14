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

public partial class TellerSessions : System.Web.UI.Page
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
                LoadCashiers();
                LoadTokens();
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

    private void LoadTokens()
    {
        string DistrictCode = GetDistrictCode();
        dataTable = datafile.GetTellerSessions(DistrictCode);
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
    private void LoadCashiers()
    {
        string districtcode = GetDistrictCode();
        dtable = datafile.GetCashiers(districtcode);
        cboCashier.DataSource = dtable;
        cboCashier.DataValueField = "Userid";
        cboCashier.DataTextField = "FullName";
        cboCashier.DataBind();
    }
    protected void cboCashier_DataBound(object sender, EventArgs e)
    {
        cboCashier.Items.Insert(0, new ListItem("Select Cashiers", "0"));
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string TellerID = cboCashier.SelectedValue.ToString();
            string TellerName = cboCashier.SelectedItem.ToString();
            if (TellerID.Equals("0"))
            {
                ShowMessage("Please Select A Cashier to Open Session for", true);
            }
            else
            {
                DateTime date = DateTime.Now;
                string ret = Process.SaveTellerSessionToken(TellerID,TellerName, date);
                LoadTokens();
                if (!ret.Contains("Successfully"))
                {
                    ShowMessage(ret, true);
                }
                else
                {
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
                string tokencode = e.Item.Cells[1].Text;
                string name = e.Item.Cells[3].Text;
                string status = e.Item.Cells[5].Text;
                string datestr = e.Item.Cells[4].Text;
                DateTime date = DateTime.Parse(datestr);
                string res = Process.ChangeTokenStatus(tokencode, name, status, date);
                if (res.Contains("Successfully"))
                {
                    ShowMessage(res, false);
                }
                else
                {
                    ShowMessage(res, true);
                }
                LoadTokens();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}
