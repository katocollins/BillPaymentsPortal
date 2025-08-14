using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FailTransactionandReverse : System.Web.UI.Page
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

                Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                Button MenuReport = (Button)Master.FindControl("btnCalReports");
                Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                MenuTool.Font.Underline = false;
                MenuPayment.Font.Underline = false;
                MenuReport.Font.Underline = true;
                MenuRecon.Font.Underline = false;
                MenuAccount.Font.Underline = false;
                MenuBatching.Font.Underline = false;
                lblTotal.Visible = false;
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void LoadTransactions()
    {

        if (cboTranId.Text.Equals(""))
        {
            DataGrid1.Visible = false;
            ShowMessage("Transaction ID is required", true);
            cboTranId.Focus();
        }

        if (cboVendorCode.Text.Equals(""))
        {
            DataGrid1.Visible = false;
            ShowMessage("VendorCode is required", true);
            cboVendorCode.Focus();
        }
        else
        {
            string tranId = cboTranId.Text.ToString();
            string vendor_code = cboVendorCode.Text.ToString();

            dataTable = bll.GetTransactionToFailandReverse(tranId, vendor_code);
            if (dataTable.Rows.Count > 0)
            {
                MultiView1.ActiveViewIndex = 0;
                DataGrid1.DataSource = dataTable;
                DataGrid1.DataBind();
                DataGrid1.UseAccessibleHeader = true;
                DataGrid1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                DataGrid1.Visible = false;
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("No Record found", true);

            }

        }

    }

    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
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
    protected void gvOnRowCommand(object source, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnComplete")
            {
                string tranid = "", vendorcode = "", reason = "", bankid = "", msg;
                foreach (GridViewRow row in DataGrid1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        tranid = row.Cells[3].Text;
                        vendorcode = row.Cells[0].Text;
                        bankid = row.Cells[10].Text;
                        if (bankid.Equals("&nbsp;"))
                        {
                            bankid = null;
                        }
                        TextBox txtedit = (TextBox)row.FindControl("txtedit");
                        reason = txtedit.Text;

                        //Fail and reverse
                        DataTable dt = datafile.FailandReverseTransaction(tranid, vendorcode, reason, bankid);
                        if (dt.Rows.Count > 1)
                        {
                            //successfully updated
                            msg = "TRANSACTION HAS BEEN SUCCESSFULLY REVERSED";
                            ShowMessage(msg, false);
                        }
                        else
                        {
                            //an error occurred in db and reversing failed
                            msg = "AN ERROR OCCURRED WHILE REVERSING TRANSACTION,TRY AGAIN LATER";
                            ShowMessage(msg, true);
                        }

                    }
                }
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
            string tranId = cboTranId.Text.ToString();
            string vendor_code = cboVendorCode.Text.ToString();

            dataTable = bll.GetTransactionToFailandReverse(tranId, vendor_code);
            DataGrid1.DataSource = dataTable;
            DataGrid1.DataBind();
            ShowMessage(".", true);

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
}