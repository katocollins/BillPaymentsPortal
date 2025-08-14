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
using System.Collections.Generic;
using InterLinkClass.Epayment;

public partial class AirtimeApproval : System.Web.UI.Page
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
        string vendorref = txtpartnerRef.Text.Trim();

        //DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
        //DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);

        string fromdate = string.IsNullOrEmpty(txtfromDate.Text) ? DateTime.Now.ToString("yyyy-MM-dd") : txtfromDate.Text;
        string todate = string.IsNullOrEmpty(txtfromDate.Text) ? DateTime.Now.ToString("yyyy-MM-dd") : txtfromDate.Text;
        if (vendorcode.Equals("0"))
        {
            ShowMessage("Please Select VendorCode", true);
        }
        else
        {
            dataTable = datapay.GetAirtimeTransactionsToApprove(vendorcode, vendorref, fromdate, todate);

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
    protected void btnApprove_Clicks(object sender, EventArgs e)
    {
        try
        {
            string vendorcode = DropDownList1.SelectedValue.ToString();
            string trans_status = "APPROVED";
            if (!vendorcode.Equals("0"))
            {
                List<Transaction> tranList = GetRecordsToApprove2();
                Responseobj res = new Responseobj();
                int count = 0, failed = 0;

                foreach (Transaction t in tranList)
                {
                    //bool executed = datafile.LogAirtimePayment(t.CustomerRef, t.CustomerName, t.TranAmount, t.TranType, t.Teller, t.VendorTranId, t.VendorCode);
                    //bool done = datafile.UpdateAirtimeSentStatus("APPROVED", "", "", t.VendorTranId, t.VendorCode);
                    //if (done)
                    //{
                    //string status, string pegpayId, string reason, string transactionId, string vendorcode

                    res = bll.PostPrepaidPayment(t);

                    if (res.Errorcode.Equals("0"))
                    {
                        //datafile.UpdateAirtimeSentStatus("SUCCESS", res.Receiptno, "", t.VendorTranId, t.VendorCode);
                        count++;
                    }
                    else
                    {
                        failed++;
                    }
                    //}

                }
                LoadTransactions();
                ShowMessage(count + " Were submited for approval, " + failed + " Failed you can retry them", true);
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
    private List<Transaction> GetRecordsToApprove2()
    {
        List<Transaction> tranList = new List<Transaction>();
        int Count = 0;
        string ItemArr = "";
        foreach (DataGridItem Items in DataGrid1.Items)
        {
            CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
            if (chk.Checked)
            {
                string tranid = Items.Cells[0].Text;
                string custRef = Items.Cells[1].Text;
                string customerName = Items.Cells[2].Text;
                string UtilityCode = Items.Cells[3].Text;
                string custType = "-";

                string custBranch = Session["DistrictCode"].ToString();
                string amount = Items.Cells[4].Text;
                string recorddate = Items.Cells[5].Text;

                DateTime payDate = DateTime.Parse(recorddate);

                Transaction t = new Transaction();
                t.TranAmount = amount.Replace(",", "");
                t.CustomerRef = custRef;
                t.CustomerType = "";
                t.CustomerName = custRef;
                t.TranType = "CASH";
                //t.PaymentType = payType;
                t.CustomerTel = custRef;
                t.Reversal = "0";
                t.PaymentType = UtilityCode;
                t.Teller = custRef;
                t.VendorCode = custBranch;
                t.StatusDescription = "";
                t.VendorTranId = tranid;// DateTime.Now.ToString("yyyyMMddHHmmss");
                t.PaymentDate = payDate.ToString("dd/MM/yyyy");
                tranList.Add(t);
                Count++;

            }
        }
        return tranList;
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


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
