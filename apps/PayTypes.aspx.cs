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

public partial class PayTypes : System.Web.UI.Page
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
                    CallPayTypeList();
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
    private void CallPayTypeList()
    {
        btnCallList.Enabled = false;
        btnAddDistrict.Enabled = true;
        LoadPayTypes();
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
    private void LoadPayTypes()
    {
        dataTable = datafile.GetPaymentTypes();
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
            ShowMessage("No Payment Type found", true);
        }
    }
    protected void btnCallList_Click(object sender, EventArgs e)
    {
        try
        {
            CallPayTypeList();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void btnAddDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            CallPayTypeform();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnEdit")
            {
                string PayTypeCode = e.Item.Cells[1].Text;
                LoadForm(PayTypeCode);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void CallPayTypeform()
    {
        btnCallList.Enabled = true;
        btnAddDistrict.Enabled = false;
        MultiView3.ActiveViewIndex = 1;
        txtUser.Text = Session["FullName"].ToString();
    }
    private void LoadForm(string PayTypeCode)
    {
        dtable = datafile.GetPayTypeByCode(PayTypeCode);
        if (dtable.Rows.Count > 0)
        {
            CallPayTypeform();
            lblcode.Text = dtable.Rows[0]["PaymentCode"].ToString();
            txtcode.Text = dtable.Rows[0]["PaymentCode"].ToString();
            txtshortname.Text = dtable.Rows[0]["ShortName"].ToString(); 
            txtname.Text = dtable.Rows[0]["PaymentType"].ToString();
            double amount = double.Parse(dtable.Rows[0]["Amount"].ToString());
            bool Isactive = bool.Parse(dtable.Rows[0]["Active"].ToString());
            bool IsVat = bool.Parse(dtable.Rows[0]["Vatable"].ToString());
            bool IsRef = bool.Parse(dtable.Rows[0]["Referenced"].ToString());
            txtAmount.Text = amount.ToString("#,##0");
            chkActive.Checked = Isactive;
            chkvat.Checked = IsVat;
            chkRef.Checked = IsRef;

            txtcode.Enabled = false;
            txtshortname.Enabled = false;
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
        string code = txtcode.Text.Trim();
        string shortname = txtshortname.Text.Trim();
        string name = txtname.Text.Trim();
        string amount = txtAmount.Text.Trim();
        bool Isactive = chkActive.Checked;
        bool IsVat = chkvat.Checked;
        bool IsRef = chkRef.Checked;
        if (code.Equals(""))
        {
            ShowMessage("Please Enter Payment Type Code", true);
            txtcode.Focus();
        }
        if (shortname.Equals(""))
        {
            ShowMessage("Please Enter Payment Type Short name", true);
            txtshortname.Focus();
        }
        else if (name.Equals(""))
        {
            ShowMessage("Please Enter Payment Type Name", true);
            txtname.Focus();
        }
        else
        {
            string ret = Process.SavePayType(code,shortname, name, amount, Isactive, IsVat, IsRef);
            ShowMessage(ret, false);
            ClearContrls();
        }
    }
    private void ClearContrls()
    {
        lblcode.Text = "0";
        txtcode.Text = "";
        txtname.Text = "";
        txtAmount.Text = "";
        chkActive.Checked = false;
        chkRef.Checked = false;
        chkvat.Checked = false;
    }
}
