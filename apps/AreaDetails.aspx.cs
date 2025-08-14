using System;
using System.Data;
using System.Data.SqlClient;
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

public partial class AreaDetails : System.Web.UI.Page
{
    //SystemUsers dac = new SystemUsers();
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["AreaID"].ToString().Equals("1"))
            {
                if (IsPostBack == false)
                {
                    LoadAreas();
                    MultiView1.ActiveViewIndex = 0;
                    Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                    Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                    Button MenuReport = (Button)Master.FindControl("btnCalReports");
                    Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                    Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                    Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                    MenuTool.Font.Underline = true;
                    MenuPayment.Font.Underline = false;
                    MenuReport.Font.Underline = false;
                    MenuRecon.Font.Underline = false;
                    MenuAccount.Font.Underline = false;
                    MenuBatching.Font.Underline = false;
                    string strProcessScript = "this.value='Working...';this.disabled=true;";
                    btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
                }
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("YOU DO NOT HAVE RIGHTS TO VIEW THIS PAGE", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message,true);            
        }        
    }

    private void LoadAreas()
    {
        dataTable = datafile.GetAreaList();
        DataGrid1.DataSource = dataTable;
        DataGrid1.DataBind();
    }

    private void LoadControls(string code)
    {
        int AreaID = int.Parse(code);
        dtable = datafile.GetAreaDetailsByID(AreaID);
        if (dtable.Rows.Count > 0)
        {
            lblCode.Text = dtable.Rows[0]["AreaID"].ToString();
            txtcode.Text = dtable.Rows[0]["AreaCode"].ToString();
            txtname.Text = dtable.Rows[0]["AreaName"].ToString();
            bool IsActive = bool.Parse(dtable.Rows[0]["Active"].ToString());
            chkIsActive.Checked = IsActive;
            ShowMessage(".", true);
        }
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
    protected void btnReturn_Click(object sender, EventArgs e)
    {

    }
     protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
           ValidateInputs();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);  
        }
    }

    private void ValidateInputs()
    {
        string Serial = lblCode.Text.Trim();
        string code = txtcode.Text.Trim();
        string name = txtname.Text.Trim();
        bool isActive = chkIsActive.Checked;
        if (code.Equals(""))
        {
            ShowMessage("Please Enter Region Code", true);
            txtcode.Focus();
        }
        else if (name.Equals(""))
        {
            ShowMessage("Please Enter Region Name", true);
            txtname.Focus();
        }
        else
        {
            string ret = Process.SaveAreaDetails(Serial, code, name, isActive);
            LoadAreas();
            ShowMessage(ret, false);
            ClearContrls();
        }
    }
    private void ClearContrls()
    {
        lblCode.Text = "0";
        txtcode.Text = "";
        txtname.Text = "";
        chkIsActive.Checked = false;
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

    }
    protected void btnNo_Click(object sender, EventArgs e)
    {

    }
    protected void cboAccessLevel_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("btnEdit"))
            {
                string code = e.Item.Cells[1].Text;
                LoadControls(code);
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
            dataTable = datafile.GetAreaList();
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}