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
using System.Windows.Forms;
using System.Messaging;

public partial class XtraFloatTransactions : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();
    public string smsQueuepath = "";
    private MessageQueue smsqueue;
    //private Message smsmsg;
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
                multiview1.ActiveViewIndex = 0;
                //Multiview2.ActiveViewIndex = 1;
            }
        }
        catch (Exception ee)
        {
            bll.ShowMessage(lblmsg, ee.Message, true, Session);
        }
    }

    private void LoadData()
    {
        try
        {
            //bll.LoadTransactionCategories(ddTranCategory);
            bll.LoadTransactionTypesIntoDropDownALL("MTN", ddTranCategory);
        }
        catch (Exception ee)
        {

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
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }

    private void SearchDb()
    {
        //searchCriteria.Add(BankCode);
        //searchCriteria.Add(BranchCode);
        //searchCriteria.Add(Teller);
        //searchCriteria.Add(AccountNumber);
        //searchCriteria.Add(CustomerName);
        //searchCriteria.Add(TransCategory);
        //searchCriteria.Add(BankId);
        //searchCriteria.Add(PegPayId);
        //searchCriteria.Add(FromDate);
        //searchCriteria.Add(ToDate);
        //searchCriteria.Add("");
        string BankCode = "MTN";
        string branchCode = "ALL";
        string accountNumber = txtAccountNumber.Text;
        string teller = "";
        string customerName = txtCustomerName.Text;
        string tranCategory = ddTranCategory.SelectedValue;
        string bankId = txtTransactionId.Text;
        string pegpayId = "";
        string fromDate = txtFromDate.Text;
        string toDate = txtToDate.Text;
        string value = "";

        string[] searchParams = {BankCode, branchCode, teller, accountNumber, customerName, tranCategory, bankId, pegpayId, fromDate, toDate};
        bool truncated = false;
       // searchParams[10] = GetRowCount("SearchGeneralLedgerTable2", searchParams, out truncated);
        DataTable dt = bll.GetData("SearchGeneralLedgerTableTxnNew", searchParams);
        if (dt.Rows.Count > 0)
        {
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();
            string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
            dataGridResults.Visible = true;
            Session["AllTransaction_"] = dt;
            string email = Session["UserEmail"].ToString(); 
            if (truncated)
            {
               // msg = "Showing Top " + searchParams[10] + " Rows. Complete Report will be delivered to " + email;
            }
            bll.ShowMessage(lblmsg, msg, false, Session);
            bll.ShowMessage(lblmsg, msg, false, Session);
        }
        else
        {
            dataGridResults.Visible = false;
            dataGridResults.DataSource = null;
            dataGridResults.DataBind();
            string msg = "No Records Found Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    private string GetRowCount(string procedure, string[] parameters, out bool truncated)
    {
        string rowCount = "1000000";
        truncated = false;
        try
        {
            string storedProcedure = procedure + "Count";
            string SystemCount = "5000";
            parameters[10] = "0";
            DataTable tbl = bll.GetData("SearchGeneralLedgerCount2", parameters);
            if (tbl.Rows.Count > 0)
            {
                double count = 0, sysCount = 0;
                string queryCount = tbl.Rows[0]["RowCounts"].ToString();
                if (Double.TryParse(queryCount, out count))
                {
                    parameters[10] = queryCount;//.Split(new string[] { "" }, StringSplitOptions.None)[0];
                }
                if (Double.TryParse(SystemCount, out sysCount))
                {
                    rowCount = "5000";

                }
               

                if (count > sysCount)
                {

                    string paramss = "";
                    foreach (string str in parameters)
                    {
                        paramss += str + ",";

                    }
                    string email = Session["UserEmail"].ToString();
                    bll.ProcessData("ReportGenerationRequests_InsertRow", new string[] { email, procedure, paramss.Trim(','), "PENDING" });

                    //DateTime date = DateTime.Parse(parameters[5]);
                    // toDate = DateTime.ParseExact(toDate, format, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    //DateTime date = DateTime.ParseExact(parameters[4], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    //FromDate = date.AddDays(-30).ToString("yyyy-MM-dd");

                    truncated = true;
                }
            }

        }
        catch (Exception ee)
        {
        }
        return rowCount;
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}
