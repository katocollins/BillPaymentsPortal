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

public partial class ErrorSubs : System.Web.UI.Page
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
            if(Session["AreaID"].ToString().Equals("1"))
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
    private void DisableBtnsOnClick()
    {        
        string strProcessScript = "this.value='Working...';this.disabled=true;";
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
        LoadErrorSubs();
    }
    private void CallDistrictform()
    {
        btnCallList.Enabled = true;
        btnAddDistrict.Enabled = false;
        MultiView3.ActiveViewIndex = 1;
        DateTime now = DateTime.Now;
        txtRecordDate.Text = now.ToString("dd, MMMM yyyy");
    }
    private void LoadErrorSubs()
    {
        dataTable = datafile.GetErrorSubs();
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
            ShowMessage("No Subscriber found", true);
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
        string name = txtName.Text.Trim();
        string phone = txtphone.Text.Trim();
        string email = txtemail.Text.Trim();
        if (name.Equals(""))
        {
            ShowMessage("Please Enter Subscriber name", true);
            txtName.Focus();
        }
        else if (phone.Equals(""))
        {
            ShowMessage("Please Enter Subscriber Phone", true);
            txtphone.Focus();
        }
        else if (email.Equals(""))
        {
            ShowMessage("Please Enter Subscriber Email", true);
            txtemail.Focus();
        }
        else
        {
            string ret = Process.SaveErrorSub(name,phone,email);
            if (ret.Contains("Successfully"))
            {
                ShowMessage(ret, false);   
                ClearContrls();
            }
            else
            {
                ShowMessage(ret, true);                
            }
        }
    }

    private void ClearContrls()
    {
        lblcode.Text = "0";
        txtemail.Text = "";
        txtphone.Text = "";
        txtName.Text = "";
        
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnEdit")
            {
                string code = e.Item.Cells[0].Text;
                string status = e.Item.Cells[6].Text;
                Process.ChangeSubStatus(code, status);
                LoadErrorSubs();
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
            dataTable = datafile.GetErrorSubs();
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
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
