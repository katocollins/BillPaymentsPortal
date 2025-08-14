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
public partial class General_ViewUsers : System.Web.UI.Page
{
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                LoadAreas();
                if (!Session["AreaID"].ToString().Equals("1"))
                {
                    cboAreas.SelectedIndex = cboAreas.Items.IndexOf(new ListItem(Session["AreaCode"].ToString(), Session["AreaID"].ToString()));
                    int AreaID = Convert.ToInt16(cboAreas.SelectedValue.ToString());
                    LoadBranches(AreaID);
                    cboBranches.SelectedIndex = cboBranches.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                    cboAreas.Enabled = false;
                    cboBranches.Enabled = false;
                }
                else
                {
                    int AreaID = Convert.ToInt16(cboAreas.SelectedValue.ToString());
                    LoadBranches(AreaID);
                    cboAreas.Enabled = true;
                    cboBranches.Enabled = true;
                }
                LoadRoles();
                //LoadUsers();
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
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
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
            LoadUsers();            
         }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadUsers()
    {
        string BranchID = "";
        SystemUser user = new SystemUser();
        user.Name = txtSearch.Text.Trim();
        if (cboBranches.Text == "")
        {
            BranchID = "";
        }
        else
        {
            BranchID = cboBranches.SelectedValue.ToString();
        }
        user.Area = int.Parse(cboAreas.SelectedValue.ToString());
        user.Branch = BranchID;
        user.Role = cboAccessLevel.SelectedValue.ToString();
        dataTable = datafile.GetSystemUsers(user);
        if (dataTable.Rows.Count > 0)
        {
            ShowMessage(".", true);
            MultiView1.ActiveViewIndex = 0;
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        else
        {
            ShowMessage("No User(s) Found", true);
            MultiView1.ActiveViewIndex = -1;
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
    private void LoadAreas()
    {
        dataTable = datafile.GetAreas();
        cboAreas.DataSource = dataTable;
        cboAreas.DataValueField = "TypeId";
        cboAreas.DataTextField = "UserType";
        cboAreas.DataBind();
    }
    private void LoadBranches(int AreaID)
    {
        dataTable = datafile.GetBranches(AreaID);
        cboBranches.DataSource = dataTable;
        cboBranches.DataValueField = "CompanyCode";
        cboBranches.DataTextField = "Company";
        cboBranches.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows.Count == 1)
            {
                cboBranches.Enabled = false;
                string district = dataTable.Rows[0]["CompanyCode"].ToString();
                cboBranches.SelectedIndex = cboBranches.Items.IndexOf(cboBranches.Items.FindByValue(district));
            }
            else
            {
                cboBranches.Enabled = true;
            }

        }
        else
        {
            cboBranches.Enabled = false;
        }
    }
    private void LoadRoles()
    {
        dataTable = datafile.GetSystemRoles();
        cboAccessLevel.DataSource = dataTable;
        cboAccessLevel.DataValueField = "RoleCode";
        cboAccessLevel.DataTextField = "RoleName";
        cboAccessLevel.DataBind();
    }
    protected void cboAreas_DataBound(object sender, EventArgs e)
    {
        cboAreas.Items.Insert(0, new ListItem("      All Company Types     ", "0"));
    }
    protected void cboAreas_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int AreaID = Convert.ToInt16(cboAreas.SelectedValue);
            LoadBranches(AreaID);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message,true);
        }
    }
    protected void cboCostCenter_DataBound(object sender, EventArgs e)
    {
        cboBranches.Items.Insert(0, new ListItem("All Companies ", "0"));
    }
   
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            SystemUser user = new SystemUser();
            if (e.CommandName == "btnEdit")
            {
                string UserID = e.Item.Cells[0].Text;
                //Response.Redirect("./AddUser.aspx?transferid=" + UserID, true);
                Server.Transfer("./AddUser.aspx?transferid=" + UserID, false);
                //ShowMessage(UserID, true);
            }
            else if (e.CommandName == "btnSms")
            {
                string UserID = e.Item.Cells[0].Text;
                user.Userid = int.Parse(UserID);
                user.Uname = e.Item.Cells[5].Text;
                string name = e.Item.Cells[6].Text;
                LoadCreditform(user.Uname, name);

            }
            else if (e.CommandName == "btnChange")
            {
                string UserID = e.Item.Cells[0].Text;
                user.Userid = int.Parse(UserID);
                user.Uname = e.Item.Cells[4].Text;
                user.Status = e.Item.Cells[8].Text;
                string returned = Process.ChangeUserAccess(user);
                LoadUsers();
                ShowMessage(returned,false);
            }

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }

    private void LoadCreditform(string username,string name)
    {
        ShowMessage(".", true);
        MultiView1.ActiveViewIndex = 1;
        lblUserName.Text = username;
        string header = "ADDING CREDIT TO "+name;
        lblHeader.Text = header;
        btnOK.Enabled = false;
        cboAccessLevel.Enabled = false;
        cboAreas.Enabled = false;
        cboBranches.Enabled = false;
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string BranchID ="";
            SystemUser user = new SystemUser();
            user.Name = txtSearch.Text.Trim();
            if (cboBranches.Text == "")
            {
                BranchID ="";
            }
            else
            {
                BranchID = cboBranches.SelectedValue.ToString();
            }
            user.Area = int.Parse(cboAreas.SelectedValue.ToString());
            user.Branch = BranchID;
            user.Role = cboAccessLevel.SelectedValue.ToString();
            dataTable = datafile.GetSystemUsers(user);           
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    protected void cboAccessLevel_DataBound(object sender, EventArgs e)
    {
        cboAccessLevel.Items.Insert(0, new ListItem("All Roles", "0"));
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        btnOK.Enabled = true;
        cboAccessLevel.Enabled = true;
        cboAreas.Enabled = true;
        cboBranches.Enabled = true;
        ShowMessage(".", true);
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string credit = txtCredit.Text.Trim();
            string username = lblUserName.Text.Trim();
            string res = Process.SaveSMSCredit(credit, username);
            if (res.Contains("Successfully"))
            {
                ShowMessage(res, false);
                txtCredit.Text = "";
                lblUserName.Text = "0";
                MultiView1.ActiveViewIndex = 0;
                btnOK.Enabled = true;
                cboAccessLevel.Enabled = true;
                cboAreas.Enabled = true;
                cboBranches.Enabled = true;

            }
            else
            {
                ShowMessage(res, true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}
