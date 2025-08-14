using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ApproveData : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    DataFile dh = new DataFile();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            LoadVendors();
            ToggleVendor();
        }
    }


    private void LoadVendors()
    {
        dtable = datafile.GetAllVendors("0");
        DropDownList1.DataSource = dtable;
        DropDownList1.DataValueField = "VendorCode";
        DropDownList1.DataTextField = "Vendor";
        DropDownList1.DataBind();
    }

    private void ToggleVendor()
    {
        try
        {
            string districtcode = Session["DistrictCode"].ToString();
            string role = Session["RoleCode"].ToString();

            DropDownList1.Enabled = false;
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(districtcode));
        }
        catch (Exception ex)
        {

            throw ex;
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

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            string vendorcode = DropDownList1.SelectedValue.ToString();
            string trans_status = "REJECTED";
            if (!vendorcode.Equals("0"))
            {
                //string vendorcode = Session["DistrictCode"].ToString();
                string str = GetRecordsToApprove().TrimEnd(',');
                string ret = Process.ApproveAirtime(str, trans_status, vendorcode);
                if (ret.Contains("APPROVED") || ret.Contains("REJECTED"))
                {
                    LoadTransactions();
                    //ShowMessage(ret, true);
                }
                else if (str == "")
                {
                    ShowMessage("Please Select Trans To Reject", true);
                }
                else
                {
                    ShowMessage("Rejection failed", true);
                }
            }
            else
            {
                ShowMessage("Please Select Vendor", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private string GetRecordsToApprove()
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
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string vendorcode = DropDownList1.SelectedValue.ToString();
            string trans_status = "APPROVED";
            if (!vendorcode.Equals("0"))
            {
                string str = GetRecordsToApprove().TrimEnd(',');
                string ret = Process.Approve(str, trans_status, vendorcode);

                if (ret.Contains("APPROVED") || ret.Contains("REJECTED"))
                {
                    LoadTransactions();

                }
                else if (str == "")
                {
                    ShowMessage("Please Select Trans to Approve or Reject", true);
                }
                else
                {
                    ShowMessage("approval failed", true);
                }
            }
            else
            {
                ShowMessage("Please Select Vendor", true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void LoadTransactions()
    {
        string vendorcode = DropDownList1.SelectedValue.ToString();
        string vendorref = beneficiary_tel.Text.Trim();

        //DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        //DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);

        string fromdate = string.IsNullOrEmpty(Sdate.Text) ? DateTime.Now.ToString("yyyy-MM-dd") : Sdate.Text;
        string todate = string.IsNullOrEmpty(Edate.Text) ? DateTime.Now.ToString("yyyy-MM-dd") : Edate.Text;
        if (vendorcode.Equals("0"))
        {
            ShowMessage("Please Select VendorCode", true);
        }
        else
        {
            dataTable = datapay.GetDataTransactionsToApprove(vendorcode, vendorref, fromdate, todate);

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
            double amount = double.Parse(dr["Amount"].ToString());
            total += amount;
        }
        lblTotal.Text = "Total Amount of Transactions [" + total.ToString("#,##0") + "]";
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

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}