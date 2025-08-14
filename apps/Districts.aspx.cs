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

public partial class Districts : System.Web.UI.Page
{
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
                    MultiView2.ActiveViewIndex = 0;
                    CallDistrictList();
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
            else
            {
                MultiView2.ActiveViewIndex = -1;
                MultiView3.ActiveViewIndex = -1;
                ShowMessage("YOU DO NOT HAVE RIGHTS TO VIEW THIS PAGE", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadAreas()
    {
        dataTable = datafile.GetAreas();
        cboAreas.DataSource = dataTable;
        cboAreas.DataValueField = "AreaID";
        cboAreas.DataTextField = "AreaName";
        cboAreas.DataBind();
    }
    private void LoadRegions()
    {
        dataTable = datafile.GetAreas();
        cboRegion.DataSource = dataTable;
        cboRegion.DataValueField = "AreaID";
        cboRegion.DataTextField = "AreaName";
        cboRegion.DataBind();
    }
    private void DisableBtnsOnClick()
    {        
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        Button1.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button1, "").ToString());
        btnAddDistrict.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnAddDistrict, "").ToString());
        btnCallList.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnCallList, "").ToString());
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
    private void CallDistrictList()
    {
        btnCallList.Enabled = false;
        btnAddDistrict.Enabled = true;
        chkIsactive.Checked = true;
        LoadAreas();
        LoadDistricts();
    }
    private void CallDistrictform()
    {
        btnCallList.Enabled = true;
        btnAddDistrict.Enabled = false;
        MultiView3.ActiveViewIndex = 1;
        LoadRegions();     
    }
    private void LoadDistricts()
    {
        string regioncode = cboAreas.SelectedValue.ToString();
        string name = txtSearch.Text.Trim();
        bool Isactive = chkIsactive.Checked;
        dataTable = datafile.GetDistricts(regioncode, name, Isactive);
        DataGrid1.CurrentPageIndex = 0;
        DataGrid1.DataSource = dataTable;
        DataGrid1.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            MultiView3.ActiveViewIndex = 0;            
            ShowMessage(".", true);
        }
        else
        {
            //MultiView3.ActiveViewIndex = -1;
            //CallDistrictform();
            ShowMessage("No District found", true);
        }
    }
    protected void btnCallCustDetails_Click(object sender, EventArgs e)
    {

    }
    protected void btnAddDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            CallDistrictform();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void cboAreas_DataBound(object sender, EventArgs e)
    {
        cboAreas.Items.Insert(0, new ListItem("      All Regions     ", "0"));
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadDistricts();
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
            ValidateInputs();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ValidateInputs()
    {
        string districtcode = lblcode.Text.Trim();
        string code = txtcode.Text.Trim();
        string name = txtname.Text.Trim();
        string regioncode = cboRegion.SelectedValue.ToString();
        bool Isactive = chkActive.Checked;
        if (code.Equals(""))
        {
            ShowMessage("Please Enter District Code", true);
            txtcode.Focus();
        }
        else if (name.Equals(""))
        {
            ShowMessage("Please Enter District Name", true);
            txtname.Focus();
        }
        else if (regioncode.Equals("0"))
        {
            ShowMessage("Please Select Region", true);
        }
        else
        {
            string ret = Process.SaveDistrictDetails(districtcode, code, name, regioncode, Isactive);
            ShowMessage(ret, false);
            ClearContrls();
        }
    }

    private void ClearContrls()
    {
        lblcode.Text = "0";
        txtcode.Text = "";
        txtname.Text = "";
        chkActive.Checked = true;
        cboRegion.SelectedIndex = cboRegion.Items.IndexOf(cboRegion.Items.FindByValue("0"));
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnEdit")
            {
                string DistrictCode = e.Item.Cells[0].Text;
                LoadForm(DistrictCode);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadForm(string DistrictCode)
    {
        dtable = datafile.GetDistrictDetails(DistrictCode);
        if (dtable.Rows.Count > 0)
        {
            CallDistrictform();
            lblcode.Text = dtable.Rows[0]["DistrictID"].ToString();
            txtcode.Text = dtable.Rows[0]["DistrictCode"].ToString();
            txtname.Text = dtable.Rows[0]["DistrictName"].ToString();
            string RegionCode = dtable.Rows[0]["RegionID"].ToString();
            bool Isactive = bool.Parse(dtable.Rows[0]["Active"].ToString());
            cboRegion.SelectedIndex = cboRegion.Items.IndexOf(cboRegion.Items.FindByValue(RegionCode));
            chkActive.Checked = Isactive;
        }

    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string regioncode = cboAreas.SelectedValue.ToString();
            string name = txtSearch.Text.Trim();
            bool Isactive = chkIsactive.Checked;
            dataTable = datafile.GetDistricts(regioncode, name, Isactive);
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void cboRegion_DataBound(object sender, EventArgs e)
    {
        cboRegion.Items.Insert(0, new ListItem("      Select Regions     ", "0"));
    }
    protected void btnCallList_Click(object sender, EventArgs e)
    {
        try
        {
            CallDistrictList();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}
