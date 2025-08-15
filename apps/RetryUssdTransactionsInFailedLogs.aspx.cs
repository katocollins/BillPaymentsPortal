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
    public partial class RetryUssdTransactionsInFailedLogs : System.Web.UI.Page
    {
        ProcessPay Process = new ProcessPay();
        DataLogin datafile = new DataLogin();
        Datapay datapay = new Datapay();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();
        DataTable dtable = new DataTable();
        private ReportDocument Rptdoc = new ReportDocument();


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
            else
            {
                string tranId = cboTranId.Text.ToString();
                //dataTable = datapay.GetTransactionToRetryUSSD(tranId);
                dataTable = datafile.GetTransactionToRetryUSSD(tranId);
                //DataGrid1.CurrentPageIndex = 0;
                DataGrid1.DataSource = dataTable;
                DataGrid1.DataBind();
                if (dataTable.Rows.Count > 0)
                {
                    MultiView1.ActiveViewIndex = 0;

                }
                else
                {
                    lblTotal.Text = ".";
                    DataGrid1.Visible = false;
                    lblTotal.Visible = false;
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
        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                ////string vendortranId;
                ////string msg;
                ////string name;
                ////string amount;
                ////if (e.CommandName == "btnCompete")
                ////{
                ////    vendortranId = cboTranId.Text;
                ////    name = "something else";//DataGrid1.cell
                ////    amount = "money";


                ////    //check if it exists in received
                ////    DataTable dt = datapay.GetTransactionFromBillPayments(vendortranId);
                ////    if (dt.Rows.Count < 1)
                ////    {



                ////        if (!(name == "STARTIMES"))
                ////        {

                ////            string path = "@D:/PePay/ManualUpdates/UnprocessedManuallyUpdatedPulls/" + DateTime.Now + ".csv";
                ////            string details = vendortranId + "," + telecomID + "," + amount + "," +name+","+"01";
                ////            File.WriteAllText(path, details);        
                ////        }





                ////    }
                ////    else
                ////    {
                ////        //transaction is already successfull in bill payments and there4 can't be retried
                ////        msg = "THE TRANSACTION IS ALREADY SUCCESSFUL IN BILLPAYMENTS,IT CAN'T BE RETRIED!!";
                ////        ShowMessage(msg, true);
                ////    }
                //}
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

                dataTable = datapay.GetTransactionToRetryFromMomo(tranId);
                //DataGrid1.CurrentPageIndex = e.NewPageIndex;
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



        protected void gvOnRowCommand(object source, GridViewCommandEventArgs e)
        {
            try
            {
                string vendortranId;
                string msg;
                string name;
                string amount;
                string telecomID, VendorCode;

                if (e.CommandName == "btnComplete")
                {
                    foreach (GridViewRow row in DataGrid1.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            string tranId = row.Cells[1].Text;

                            dataTable = datapay.GetTransactionToRetryFromMomo(tranId);

                            if (dataTable.Rows.Count > 0)
                            {
                                MultiView1.ActiveViewIndex = 0;
                                foreach (DataRow dr in dataTable.Rows)
                                {
                                    telecomID = dr["TelecomID"].ToString();
                                    vendortranId = dr["VendorTranId"].ToString();
                                    VendorCode = dr["VendorCode"].ToString();
                                    amount = dr["TranAmount"].ToString();

                                    bll.WriteTofile(VendorCode, vendortranId, telecomID, amount);

                                    ShowMessage("record updated", true);
                                }
                            }
                            else
                            {
                                ShowMessage("Record couldn't be updated", true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}