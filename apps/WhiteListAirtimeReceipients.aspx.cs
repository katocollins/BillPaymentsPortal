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

public partial class WhiteListAirtimeReceipients : System.Web.UI.Page
{
    DataLogin datafile = new DataLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string username = Session["Username"].ToString();
            if (!IsPostBack)
            {
                LoadVendors();
                MultiView2.ActiveViewIndex = 0;
                btn_delete.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Default.aspx", true);
            ShowMessage(ex.Message, true);
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
    protected void btnOK_Click(object sender, EventArgs e)
    {
        MultiView2.ActiveViewIndex = 1;
    }
    private void LoadVendors()
    {
        DataTable dtable = datafile.GetAllVendors("0");
        ddlVendorCode.DataSource = dtable;
        ddlVendorCode.DataValueField = "VendorCode";
        ddlVendorCode.DataTextField = "Vendor";
        ddlVendorCode.DataBind();
        string vendorcode = Session["DistrictCode"].ToString();
        ddlVendorCode.SelectedValue = vendorcode;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string vendorCode = ddlVendorCode.SelectedValue.ToString();
            string customerTel = txt_customerTele.Text.ToString().Trim();
            string name = customerTel;
            PhoneValidator pv = new PhoneValidator();
            if (pv.PhoneNumbersOk(customerTel))
            {
                customerTel = pv.FormatTelephone(customerTel);
            }
            DataTable table = datafile.GetWhitelistedPhoneNumbers(customerTel, name, vendorCode);
            if (table.Rows.Count > 0)
            {
                ShowMessage(table.Rows.Count + " Records Found", true);
                DataGrid1.DataSource = table;
                DataGrid1.DataBind();
            }
            else
            {
                ShowMessage("No records Found", true);
            }

        }
        catch (Exception ee)
        {

        }
    }

    private void LoadData()
    {
        try
        {
            string vendorCode = ddlVendorCode.SelectedValue.ToString();
            string customerTel = txt_customerTele.Text.ToString().Trim();
            string name = customerTel;
            PhoneValidator pv = new PhoneValidator();
            if (pv.PhoneNumbersOk(customerTel))
            {
                customerTel = pv.FormatTelephone(customerTel);
                DataTable table = datafile.GetWhitelistedPhoneNumbers(customerTel,name, vendorCode);
                if (table.Rows.Count > 0)
                {
                    DataGrid1.DataSource = table;
                    DataGrid1.DataBind();
                }
                else
                {

                }
            }

        }
        catch (Exception ee)
        {

        }
    }
    protected void add_beneficiary_Click(object sender, EventArgs e)
    {
        string customerName = txtCustomerName.Text;
        string customerPhone = txtPhone.Text;
        string allowAirtime = (cbx_airtime.Checked) ? "1" : "0";
        string allowData = (cbx_data.Checked) ? "1" : "0";
        string teller = Session["Username"].ToString();
        string vendorcode = Session["DistrictCode"].ToString();
        try
        {
            PhoneValidator pv = new PhoneValidator();
            if (string.IsNullOrEmpty(customerName))
            {
                ShowMessage("Provide the customer name", true);
            }
            else if (string.IsNullOrEmpty(customerPhone))
            {
                ShowMessage("Provide the customer phone number", true);
            }
            else if (pv.PhoneNumbersOk(customerPhone))
            {
                customerPhone = pv.FormatTelephone(customerPhone);

                bool inserted = datafile.LogBeneficiary(customerName, customerPhone, allowAirtime, allowData, teller, vendorcode);
                if (inserted)
                {

                    ShowMessage("Beneficiary has been addedd", false);
                    MultiView2.ActiveViewIndex = 0;
                   
                }
                else
                {
                    ShowMessage("Operation Has failed", true);
                }
            }
            else
            {
                ShowMessage("Invalid phone number", true);
            }
        }
        catch (Exception ee)
        {
            ShowMessage(ee.Message, true);
        }
        txtPhone.Enabled = true;
        LoadData();
    }
    protected void cboVendor_DataBound(object sender, EventArgs e)
    {

    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string customerName = e.Item.Cells[0].Text;
        string customertele = e.Item.Cells[1].Text;
        string allowAirtime = e.Item.Cells[2].Text;
        string allowData = e.Item.Cells[3].Text;
        string vendorcode = e.Item.Cells[4].Text;
        if (e.CommandName == "btnEdit")
        {
            txtCustomerName.Text = customerName;
            txtPhone.Text = customertele;
            string airtime = allowAirtime.ToUpper();
            string data = allowData.ToUpper();
            cbx_airtime.Checked = (airtime == "TRUE") ? true : false;
            cbx_data.Checked = (data == "TRUE") ? true : false;
            //clearFields();
            txtPhone.Enabled = false;
            btn_delete.Visible = true;
            MultiView2.ActiveViewIndex = 1;
        }
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {

    }
    private void clearFields()
    {
        txtCustomerName.Text = "";
        txtPhone.Text = "";
        cbx_data.Checked = false;
        cbx_airtime.Checked = false;
        txtPhone.Enabled = false;
    }
    protected void add_Return_Click(object sender, EventArgs e)
    {
        clearFields();
        MultiView2.ActiveViewIndex = 0;
    }
    protected void add_delete_Click(object sender, EventArgs e)
    {
        try
        {
            string customerName = txtCustomerName.Text;// e.Item.Cells[0].Text;
            string customertele = txtPhone.Text;// e.Item.Cells[1].Text;
            string vendorcode = Session["DistrictCode"].ToString();
            customertele = (customertele == "&nbsp;") ? "" : customertele;
            //datafile.RemoveBeneficiary(customertele, vendorcode);
            bool inserted = datafile.RemoveBeneficiary(customertele, vendorcode);
            if (inserted)
            {

                ShowMessage("Beneficiary has been addedd", false);
                MultiView2.ActiveViewIndex = 0;
                LoadData();
            }
            else
            {
                ShowMessage("Operation Has failed", false);
            }
        }
        catch (Exception ee)
        {
            ShowMessage(ee.Message, true);
        }
    }
}
