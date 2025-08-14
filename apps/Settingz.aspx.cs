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
public partial class Settingz : System.Web.UI.Page
{
    ProcessUsers Process = new ProcessUsers();
    DataLogin datafile = new DataLogin();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();

    SystemUser user = new SystemUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["AreaID"].ToString().Equals("1"))
            {
                if (IsPostBack == false)
                {
                    LoadParameters();
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
                MultiView1.ActiveViewIndex = -1;
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
        BtnSave.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(BtnSave, "").ToString());
    }
    private void LoadParameters()
    {
        MultiView1.ActiveViewIndex = 0;
        dataTable = datafile.GetSystemParameters();  
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
            if (e.CommandName.Equals("btnEdit"))
            {
                string valueCode = e.Item.Cells[0].Text;
                string Isnum = e.Item.Cells[1].Text;
                string name = e.Item.Cells[5].Text;
                string value = e.Item.Cells[8].Text;
                Loadform(valueCode, Isnum,name,value);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }

    private void Loadform(string ValueCode, string Isnum,string name,string value)
    {
        MultiView1.ActiveViewIndex = 1;
        lblcode.Text = ValueCode;
        lblIsnum.Text = Isnum;
        string d_value = bll.DecryptString(value);
        txtname.Text = name;
        txtvalue.Text = d_value;
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dataTable = datafile.GetSystemParameters();  
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();          
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string evariable = "";
            string ValueCode = lblcode.Text.Trim();
            string variable = txtvalue.Text.Trim();
            string name = txtname.Text.Trim();
            string Isnum = lblIsnum.Text.Trim();
            int num;
            if (variable.Equals(""))
            {
                ShowMessage("Please Enter variable", true);
                txtvalue.Focus();
            }
            else
            {
                if (Isnum.Equals("True"))
                {
                    if ((Int32.TryParse(variable, out num)))
                    {
                        evariable = bll.EncryptString(variable);
                        Process.UpdateSystemParameter(ValueCode, evariable);
                        ShowMessage(name + " Parameter Updated Successfully", false);
                        LoadParameters();
                    }
                    else
                    {
                        ShowMessage("Numeric Varriable expected", true);
                        txtvalue.Focus();
                    }
                }
                else
                {

                    if (ValueCode.Equals("20"))
                    {
                        string checkstr = "PAN";
                        if (!checkstr.Contains(variable))
                        {
                            ShowMessage("Variable must be A,N or P only", true);
                            txtvalue.Focus();
                        }
                        else
                        {
                            evariable = bll.EncryptString(variable);
                            Process.UpdateSystemParameter(ValueCode, evariable);
                            ShowMessage(name + " Parameter Updated Successfully", false);
                            LoadParameters();
                        }
                    }
                    else
                    {
                        evariable = bll.EncryptString(variable);
                        Process.UpdateSystemParameter(ValueCode, evariable);
                        ShowMessage(name + " Parameter Updated Successfully", false);
                        LoadParameters();
                    }
                }
            }
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
            ShowMessage(".", true);
            LoadParameters();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
}
