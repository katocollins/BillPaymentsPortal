using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class ProcessUnmatched : System.Web.UI.Page
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
                    LoadVendors();
                    ToggleVendor();
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

        private void LoadVendors()
        {
            dtable = datafile.GetAllVendors("0");
            DropDownList1.DataSource = dtable;
            DropDownList1.DataValueField = "VendorCode";
            DropDownList1.DataTextField = "Vendor";
            DropDownList1.DataBind();
        }
        private void DisableBtnsOnClick()
        {
            string strProcessScript = "this.value='Working...';this.disabled=true;";
            btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
            btnApprove.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnApprove, "").ToString());
            btnReject.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnReject, "").ToString());

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
            string vendorcode = DropDownList1.SelectedValue.ToString();
            //string vendorcode = cboVendor.SelectedValue.ToString();
            string vendorref = txtpartnerRef.Text.Trim();
            string tranRef = TextBox1.Text.Trim();
            string Paymentcode = "0";
            string Account = "";
            string CustName = "";
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            string teller = "";
            if (vendorcode.Equals("0"))
            {
                ShowMessage("Please Select VendorCode", true);
            }
            else
            {
                dataTable = datapay.GetTransToApproveNew(vendorcode, vendorref, tranRef, fromdate, todate);
                //dataTable = datapay.GetReconciledTransToBin(vendorcode, vendorref, Account, CustName, Paymentcode, teller, fromdate, todate);        
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
            DropDownList1.Items.Insert(0, new ListItem("Select Vendor", "0"));
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
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                string vendorcode = DropDownList1.SelectedValue.ToString();
                string trans_status = "APPROVED";
                if (!vendorcode.Equals("0"))
                {
                    string str = GetRecordsToApprove().TrimEnd(',');
                    string ret = Process.Approve(str, trans_status);
                    if (ret.Contains("APPROVED") || ret.Contains("REJECTED"))
                    {
                        LoadTransactions();
                        ShowMessage(ret, true);
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
        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                string vendorcode = DropDownList1.SelectedValue.ToString();
                string trans_status = "REJECTED";
                if (!vendorcode.Equals("0"))
                {
                    string str = GetRecordsToApprove().TrimEnd(',');
                    string ret = Process.Approve(str, trans_status);
                    if (ret.Contains("APPROVED") || ret.Contains("REJECTED"))
                    {
                        LoadTransactions();
                        ShowMessage(ret, true);
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


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}