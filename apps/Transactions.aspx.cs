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
using System.Collections.Generic;
using System.Messaging;
using UtilReqSender.EntityObjects;

public partial class Transactions : System.Web.UI.Page
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
                LoadData();
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
        bll.LoadVendors(vendor, ddVendor);
        bll.LoadUtilities(vendor, ddUtility);
        bll.LoadTranStatus(vendor, ddStatus);
    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            string[] searchParams = GetSearchParameters();
            DataTable dt = bll.Search("GetTransactionsModified", searchParams).Tables[0];
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (!rdPdf.Checked && !rdExcel.Checked)
                    {
                        bll.ShowMessage(lblmsg, "CHECK ONE EXPORT OPTION", true, Session);
                    }
                    else if (rdExcel.Checked)
                    {

                        //bll.ExportToExcel(dt, Response);
                        bll.ExportToExcelxx(dt,"", Response);

                    }
                    else if (rdPdf.Checked)
                    {
                        bll.ExportToPdf(dt, "", Response);
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

    private void SetSessionVariables(DataTable dt)
    {
        Session["StatementDataTable"] = dt;
        if (string.IsNullOrEmpty(txtFromDate.Text) || string.IsNullOrEmpty(txtToDate.Text))
        {
            Session["fromDate"] = "THE START";
            Session["toDate"] = "TO TODAY";
        }
        else
        {
            Session["fromDate"] = txtFromDate.Text;
            Session["toDate"] = txtToDate.Text;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text.Equals(""))
            {
                bll.ShowMessage(lblmsg, "From Date is required", true, Session);
                txtFromDate.Focus();

            }
            else
            {
                SearchDb();
            }
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    private void SearchDb()
    {
        string userId = Session["UserName"] as string;
        string fullname = Session["FullName"] as string;
        string userBranch = Session["UserBranch"] as string;
        string page = bll.GetCurrentPageName();
        string queryId = bll.GenerateRandomString(16);

        string[] searchParams = GetSearchParameters();
        string parameterString = bll.GetStringFromArray(searchParams, "|");
        bll.InsertIntoAuditLog(queryId, "QUERY", "GetTransactionsModified", userBranch, userId, page, fullname + " started querying with the fromDate as " + txtFromDate.Text + " and other parameters:" + parameterString);       
        
        DataTable dt = bll.Search("GetTransactionsModified", searchParams).Tables[0];
        bll.InsertIntoAuditLog(queryId, "QUERY", "GetTransactionsModified", userBranch, userId, page, fullname + " finished querying with the fromDate as " + txtFromDate.Text + " and other parameters:" + parameterString);       

        string vendorCode = Session["UserBranch"].ToString();
        if (vendorCode.Contains("MTN"))
        {
            dataGridResults.Columns[0].Visible = true;
        }
        else
        {
            dataGridResults.Columns[0].Visible = false;
        }
        if (dt.Rows.Count > 0)
        {
            CalculateTotal(dt);
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();

            Multiview2.ActiveViewIndex = 0;
            string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, false, Session);
        }
        else
        {
            CalculateTotal(dt);
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

    private string[] GetSearchParameters()
    {
        List<string> searchCriteria = new List<string>();
        string Vendor = ddVendor.SelectedValue;
        string Utility = ddUtility.SelectedValue;
        string Status = ddStatus.SelectedValue;
        string Reference = txtReference.Text;
        string Name = txtName.Text;
        string VendorId = txtVendorTranId.Text;
        string Phone = txtPhone.Text;
        string FromDate = txtFromDate.Text;
        string ToDate = txtToDate.Text;

        searchCriteria.Add(Vendor);
        searchCriteria.Add(VendorId);
        searchCriteria.Add(Phone);
        searchCriteria.Add(Reference);
        searchCriteria.Add(Name);
        searchCriteria.Add(Status);
        searchCriteria.Add(FromDate);
        searchCriteria.Add(ToDate);
        searchCriteria.Add(Utility);

        return searchCriteria.ToArray();
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

            string vendorTranId = Server.HtmlDecode(row.Cells[2].Text);
            string customerRef = Server.HtmlDecode(row.Cells[3].Text);
            string customerName = Server.HtmlDecode(row.Cells[4].Text);
            string customerTel = Server.HtmlDecode(row.Cells[5].Text);
            string utility = Server.HtmlDecode(row.Cells[6].Text);
            string area = Server.HtmlDecode(row.Cells[7].Text);
            string vendorCode = Server.HtmlDecode(row.Cells[8].Text);
            string amount = Server.HtmlDecode(row.Cells[9].Text);
            string utilityRef = Server.HtmlDecode(row.Cells[13].Text);
            string Message = "Dear" + " " + customerName + " YAKA purchase of UGX " + amount + " for a/c "
                + customerRef + " Received by UMEME. Token " + utilityRef;

            if (utility == "UMEME" && vendorCode.Contains("MTN") && utilityRef.Length > 10)
            {
                SMS sms = new SMS();
                sms.Mask = "8888";
                sms.Message = Message;
                sms.Phone = customerTel;
                sms.Sender = "MTNService";
                sms.VendorTranId = vendorTranId;
                sms.Reference = "";
                string msg = "Token Has been Sent";
                bll.ShowMessage(lblmsg, msg, false, Session);
                LogSMS(sms);
            }
            else
            {
                string msg = "Token Resend Operation not supported";
                bll.ShowMessage(lblmsg, msg, true, Session);
            }    

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