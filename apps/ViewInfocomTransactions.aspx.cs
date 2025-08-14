using InterLinkClass.DbApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewInfocomTransactions : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();
    public string smsQueuepath = "";
    private MessageQueue smsqueue;
    private Message smsmsg;
    //
    string username = "";
    string fullname = "";
    string vendor = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            username = Session["UserName"] as string;
            fullname = Session["FullName"] as string;
            vendor = Session["DistrictID"] as string;

            Session["IsError"] = null;

            //Session is invalid
            if (username == null)
            {
                Response.Redirect("Default.aspx");
            }
            else if (IsPostBack)
            {

            }
            else
            {
                //LoadData();
                MultiView1.ActiveViewIndex = 0;
                Multiview2.ActiveViewIndex = 1;
            }
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }

    private void LoadData()
    {
        //bll.LoadVendors(vendor, ddVendor);
        //bll.LoadUtilities(vendor, ddUtility);
        //bll.LoadTransactionCategories(vendor, ddTranCategory);
        //bll.LoadTranStatus(vendor, ddStatus);
    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {

            ArrayList TransactionIDs;

            DataTable resultTable = bll.CreateDataTable("TransactionIDs", new string[] { "VendorTranId", "PegPayId", "PhoneNumber", "Amount", "Utility", "Narration", "Status", "SentToUtility", "RecordDate" });

            TransactionIDs = new ArrayList(txtTranIds.Text.Split(','));


            foreach (string TransactionID in TransactionIDs)
            {
                DataTable dtResult = bll.CheckInfocomTransactions(TransactionID, Session);
                resultTable.Merge(dtResult);
            }

            if (resultTable.Rows.Count > 0)
            {
                try
                {
                    if (!rdPdf.Checked && !rdExcel.Checked)
                    {
                        bll.ShowMessage(lblmsg, "CHECK ONE EXPORT OPTION", true, Session);
                    }
                    else if (rdExcel.Checked)
                    {
                        //bll.ExportToExcel(dt, "", Response);
                        bll.ExportToCSV(resultTable, Response);
                    }
                    else if (rdPdf.Checked)
                    {
                        bll.ExportToPdf(resultTable, "", Response);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                string msg = "No Records Found Matching Search Criteria";
                bll.ShowMessage(lblmsg, msg, true, Session);
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SearchDb();
        }

        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    private void SearchDb()
    {


        ArrayList TransactionIDs;
        DataTable resultTable = bll.CreateDataTable("TransactionIDs", new string[] { "VendorTranId", "PegPayId", "PhoneNumber", "Amount", "Utility", "Narration", "Status", "SentToUtility", "RecordDate" });

        TransactionIDs = new ArrayList(txtTranIds.Text.Split(','));


        foreach (string TransactionID in TransactionIDs)
        {
            DataTable dtResult = bll.CheckInfocomTransactions(TransactionID, Session);

            //string RecordDate = dtResult.Rows[0]["RecordDate"].ToString();

            //dtResult.Rows[0]["RecordDate"] = RecordDate;


            resultTable.Merge(dtResult);
        }


        if (resultTable.Rows.Count > 0)
        {
            //CalculateTotal(resultTable);
            dataGridResults.DataSource = resultTable;
            dataGridResults.DataBind();

            Multiview2.ActiveViewIndex = 0;
            string msg = "Found " + resultTable.Rows.Count + " Records Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, false, Session);
        }
        else
        {
            CalculateTotal(resultTable);
            dataGridResults.DataSource = null;
            dataGridResults.DataBind();
            string msg = "No Records Found Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    private void CalculateTotal(DataTable Table)
    {
        double total = 0;
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr["Amount"].ToString());
            total += amount;
        }
        string rolecode = Session["RoleCode"].ToString();
        if (rolecode.Equals("004"))
        {
            lblTotal.Visible = false;
        }
        else
        {
            lblTotal.Visible = true;
        }
        lblTotal.Text = "Total Amount [" + total.ToString("#,##0") + "]";
    }

    private void SetSessionVariables(DataTable dt)
    {
        Session["StatementDataTable"] = dt;
        Session["fromDate"] = "THE START";
        Session["toDate"] = "TO TODAY";

    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataGridResults.PageIndex = e.NewPageIndex;
        SearchDb();
    }
    protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ResendToken")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            // Retrieve the row that contains the button clicked by the user from the Rows collection.

            GridViewRow row = dataGridResults.Rows[index];
            // Create a new ListItem object for the contact in the row.     
            ListItem item = new ListItem();

            string Amount = item.Text = Server.HtmlDecode(row.Cells[6].Text);
            string CustomerTel = item.Text = Server.HtmlDecode(row.Cells[7].Text);
            string utilitycode = item.Text = Server.HtmlDecode(row.Cells[8].Text);
            string UtilityTransferRef = item.Text = Server.HtmlDecode(row.Cells[9].Text);
            string CustomerRef = item.Text = Server.HtmlDecode(row.Cells[3].Text);
            string CustomerName = item.Text = Server.HtmlDecode(row.Cells[4].Text);
            string vendorTranId = item.Text = Server.HtmlDecode(row.Cells[2].Text);
            string vendorCode = Server.HtmlDecode(row.Cells[11].Text);
            string Message = "Dear" + " " + CustomerName + " YAKA purchase of UGX " + Amount + " For a/c" + CustomerRef + " Received by UMEME Token " + UtilityTransferRef;
            if (utilitycode == "UMEME" && vendorCode.Contains("MTN") && UtilityTransferRef.Length > 10)
            {
                SMS sms = new SMS();
                sms.Mask = "8888";
                sms.Message = Message;
                sms.Phone = CustomerTel;
                sms.Sender = "MTNService";
                sms.VendorTranId = vendorTranId;
                sms.Reference = "";
                lblTotal.Text = "Token Has been Sent";
                LogSMS(sms);
            }
            else
            {
                lblTotal.Text = "Token Resend Operation not supported";
            }
            //InterLinkClass.MailApi.Messenger msg = new InterLinkClass.MailApi.Messenger();
            //InterLinkClass.MailApi.SMS mysms = new InterLinkClass.MailApi.SMS();
            //mysms.Mask = "8888";
            //mysms.Message = "Dear" + " " + CustomerName + " YAKA purchase of UGX " + Amount+" For a/c"+CustomerRef+ " Received by UMEME Token " + UtilityTransferRef;
            //mysms.Phone = "256702001346";
            //mysms.Sender = "MTNService";
            //mysms.Reference = "";

            //InterLinkClass.MailApi.Result result = SendSms(mysms.Message, mysms.Phone, mysms.Reference);
            //if (result.StatusCode.Equals("0"))
            //{
            //    ShowMessage("SMS SUCCESSFULLY RESENT", true);
            //}
            //else
            //{2
            //    ShowMessage("UNABLE TO SEND MESSAGE ERROR : " + result.StatusDesc, true);

            //}          

        }
    }
    private void LogSMS(SMS sms)
    {
        try
        {
            DataLogin datafile = new DataLogin();
            smsQueuepath = datafile.SmsQueuePath;
            if (MessageQueue.Exists(smsQueuepath))
            {
                smsqueue = new MessageQueue(smsQueuepath);
            }
            else
            {
                smsqueue = MessageQueue.Create(smsQueuepath);
            }
            smsmsg = new Message(sms);
            smsmsg.Label = sms.VendorTranId;
            smsmsg.Recoverable = true;
            smsqueue.Send(smsmsg);
        }
        catch (Exception ex)
        {
            //donothing
        }
    }
}