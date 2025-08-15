using InterLinkClass.ControlObjects;
using InterLinkClass.CoreMerchantAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
public partial class Reconciliation : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
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
                MultiView1.ActiveViewIndex = -1;
                ToggleVendor();
                LoadVendors();
                LoadUtilities();
                if (Session["AreaID"].ToString().Equals("2"))
                {
                    cboUtility.SelectedIndex = cboUtility.Items.IndexOf(new ListItem(Session["DistrictName"].ToString(), Session["DistrictID"].ToString()));
                    cboUtility.Enabled = false;
                }
                
                Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                Button MenuReport = (Button)Master.FindControl("btnCalReports");
                Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                MenuTool.Font.Underline = false;
                MenuPayment.Font.Underline = false;
                MenuReport.Font.Underline = false;
                MenuRecon.Font.Underline = true;
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

    private void LoadUtilities()
    {
        dtable = datafile.GetAllUtilities("0");
        cboUtility.DataSource = dtable;
        cboUtility.DataValueField = "UtilityCode";
        cboUtility.DataTextField = "Utility";
        cboUtility.DataBind();
    }

    private void ToggleVendor()
    {
        string districtcode = Session["DistrictCode"].ToString();
        string role = Session["RoleCode"].ToString();
        if (role.Equals("005"))
        {
            cboVendor.Enabled = false;
            cboVendor.SelectedIndex = cboVendor.Items.IndexOf(cboVendor.Items.FindByValue(districtcode));
        }
        else
        {
            cboVendor.Enabled = true;
        }
    }

    private void LoadVendors()
    {
        string UserID = Session["UserID"].ToString();
        string vendor = Session["DistrictCode"].ToString();
        if (Session["DistrictCode"].ToString().ToUpper() == "PEGPAY")
        {
            dtable = datafile.GetAllVendors("0");
            cboVendor.DataSource = dtable;
            cboVendor.DataValueField = "VendorCode";
            cboVendor.DataTextField = "Vendor";
            cboVendor.DataBind();
        }
        else
        {
            cboVendor.Items.Add(new ListItem(Session["DistrictCode"].ToString()));
            cboVendor.Enabled = false;
        }
        //dtable = datafile.GetAllVendors("0");
        //cboVendor.DataSource = dtable;
        //cboVendor.DataValueField = "VendorCode";
        //cboVendor.DataTextField = "Vendor";
        //cboVendor.DataBind();
    }
    private void DisableBtnsOnClick()
    { 
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        btnReconcile.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnReconcile, "").ToString());
        
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {           
            LoadTransactions();           
         }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadTransactions()
    {
        string vendorcode = cboVendor.SelectedValue.ToString();
        string vendorref = txtpartnerRef.Text.Trim();
        string Paymentcode = "0";
        string Account = "";
        string CustName = "";
        DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
        string teller = txtSearch.Text.Trim();
        string utility = cboUtility.SelectedValue.ToString();
        if (vendorcode.Equals("0"))
        {
            ShowMessage("Please Select Collection Partner", true);
        }
        else if (utility.Equals("0"))
        {
            ShowMessage("Please Select a Utility To Reconcile", true);
        }
        else
        {
                dataTable = datapay.GetTransToReconcile(vendorcode, vendorref, Account, CustName, Paymentcode, teller, fromdate, todate, utility);
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                MultiView1.ActiveViewIndex = 0;
                CalculateTotal(dataTable);
                ShowMessage(".", true);
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("No Record found", true);
            }
        }
        chkSelect.Checked = false;
        CheckBox2.Checked = false;
    }

    private void CalculateTotal(DataTable Table)
    {
        double total = 0;
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr["TranAmount"].ToString());
            total += amount;
        }
        lblTotal.Text = "Total Amount of Transactions [" + total.ToString("#,##0") + "]";
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
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
           
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }

    protected void cboVendor_DataBound(object sender, EventArgs e)
    {
        cboVendor.Items.Insert(0, new ListItem("Select Agent", "0"));
    }
    protected void cboUtility_DataBound(object sender, EventArgs e)
    {
        cboUtility.Items.Insert(0, new ListItem("Select Utility", "0"));
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            SelectAllItems();
            if (chkSelect.Checked == true)
            {
                CheckBox2.Checked = true;
            }
            else
            {
                CheckBox2.Checked = false;
            }
        }
        catch (Exception ex)
        {
                ShowMessage(ex.Message, true);
        }
    }
    private void SelectAllItems()
    {
        foreach (DataGridItem Items in DataGrid1.Items)
        {
            CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
            if (chk.Checked)
            {
                chk.Checked = false;
            }
            else
            {
                chk.Checked = true;
            }
        }
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            SelectAllItems();
            if (CheckBox2.Checked == true)
            {
                chkSelect.Checked = true;
            }
            else
            {
                chkSelect.Checked = false;
            }
        }
        catch (Exception ex)
        {
                ShowMessage(ex.Message, true);
        }
    }
    protected void btnReconcile_Click(object sender, EventArgs e)
    {
        try
        {
            string str = GetRecordsToReconcile().TrimEnd(',');
            string ret = Process.Reconcilestr(str);
            if (ret.Contains("reconciled"))
            {
                LoadTransactions();
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
    private string GetRecordsToReconcile()
    {
        int Count = 0;
        string ItemArr = "";
        foreach (DataGridItem Items in DataGrid1.Items)
        {
            CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
            if (chk.Checked)
            {
                Count++;
                string ItemFound = Items.Cells[0].Text;
                ItemArr = ItemArr += ItemFound + ",";
            }
        }
        return ItemArr;
    }
}
}