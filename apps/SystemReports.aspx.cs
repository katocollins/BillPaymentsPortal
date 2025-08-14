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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using System.Collections.Generic;
using System.Reflection;

public partial class SystemReports : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();

    //
    string username = "";
    string fullname = "";
    string userBranch = "";
    string userRole = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            username = Session["UserName"] as string;
            fullname = Session["FullName"] as string;
            userBranch = Session["UserBranch"] as string;
            userRole = Session["RoleCode"] as string;

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
        bll.LoadVendors(userBranch, ddVendor);
        bll.LoadUtilities(userBranch, ddUtility);
        bll.LoadReportTypes(userBranch, ddReport, userRole);

        if (userBranch.ToLower().Equals("pegpay"))
        {
            simpleFilterTable.Visible = true;
            advancedFilterTable.Visible = true;
        }
    }

    protected void ddReport_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearPage();

            string reportCode = ddReport.SelectedValue.ToString();

            if (string.IsNullOrEmpty(reportCode))
            {
                bll.LoadUtilities(userBranch, ddUtility);
            }
            else
            {
                SystemReport report = bll.GetReportById(reportCode, userBranch);
                bll.LoadUtilities(ddUtility, report);
                try
                {
                    bll.LoadVendorsDynamic(ddVendor, report);
                }
                catch (Exception ex2)
                {
                    bll.LoadVendors(userBranch, ddVendor);
                }
            }

            //show button field if specific report is selected
            dataGridResults.Columns[0].Visible = reportCode.Equals("FLEXIPAY_MERCHANT_REQUESTS") || reportCode.Equals("272_TRANSACTIONS") ? true : false;
        }
        catch (Exception ex)
        {
            bll.LoadUtilities(userBranch, ddUtility);
            bll.LoadVendors(userBranch, ddVendor);
        }
    }

    private void ClearPage()
    {
        dataGridResults.DataSource = new DataTable();
        dataGridResults.DataBind();
        lblCount.Text = "";
        lblTotal.Text = "";
        lblmsg.Text = "";
    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = SearchDb();
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
                        bll.ExportToExcel(dt, "", Response);
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
            SearchDb();
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    private DataTable SearchDb()
    {
        DataTable dt = new DataTable();
        try
        {
            SystemReport report = bll.GetReportById(ddReport.SelectedValue, userBranch);
            if (report.StatusCode != "0")
            {
                string msg = report.StatusDesc;
                bll.ShowMessage(lblmsg, msg, true, Session);

            }
            else if (txtFromDate.Text.Equals("") && report.IsDateDelimited)
            {
                bll.ShowMessage(lblmsg, "From Date is required", true, Session);
                txtFromDate.Focus();
            }
            else
            {
                string[] searchParams = GetSearchParameters();
                DataSet ds = bll.ExecuteDataAccess(report.Database, report.StoredProcedure, searchParams);
                dt = ds.Tables[0];

                dt = ApplyCustomFilter(dt);

                if (dt.Rows.Count > 0)
                {
                    CalculateTotal(dt);
                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();
                    string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
                    Multiview2.ActiveViewIndex = 0;
                    bll.ShowMessage(lblmsg, msg, false, Session);
                }
                else
                {
                    CalculateTotal(dt);
                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();
                    string msg = "No Records Found Matching Search Criteria";
                    bll.ShowMessage(lblmsg, msg, true, Session);
                }

            }

        }
        catch (Exception ex)
        {
            CalculateTotal(dt);
            dataGridResults.DataSource = dt;
            dataGridResults.DataBind();
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
        return dt;
    }

    private DataTable ApplyCustomFilter(DataTable dt)
    {
        string query = "", queryType = "";
        if (!string.IsNullOrEmpty(simpleFilter.Text) && !string.IsNullOrEmpty(advancedFilter.Text))
        {
            throw new Exception("You can only use one search type: Simple or Advanced");
        }

        if (!string.IsNullOrEmpty(simpleFilter.Text.Trim()))
        {
            query = BuildSimpleQuery(simpleFilter.Text.Trim(), dt);
            queryType = "simpleFilter";
        }
        else
        {
            //person knows what they are doing
            query = advancedFilter.Text.Trim();
            queryType = "advancedFilter";
        }

        if (string.IsNullOrEmpty(query))
        {
            return dt;
        }

        DataRow[] rows = dt.Select(query);

        DataTable dataTable = dt.Clone();
        foreach (DataRow dr in rows)
        {
            dataTable.Rows.Add(dr.ItemArray);
        }

        bll.InsertIntoAuditLog(queryType, "SELECT", ddReport.SelectedValue, userBranch, username, bll.GetCurrentPageName(), fullname + " ran the following query : " + query + "  Results:" + dataTable.Rows.Count.ToString());

        return dataTable;
    }

    private string BuildSimpleQuery(string columnsAndValues, DataTable dt)
    {
        string query = "";

        //first get all filters
        string[] filters = columnsAndValues.Split(',');

        foreach (string filter in filters)
        {
            string[] data = filter.Split(':');
            string column = data[0];
            string value = data[1];

            if (bll.TableContainsColumn(column, dt))
            {
                query = " and " + column + " = '" + value + "'";
            }
        }

        string finalQuery = string.IsNullOrEmpty(query) ? query : query.Remove(0, 4);

        return finalQuery;
    }

    private void CalculateTotal(DataTable Table)
    {
        try
        {
            SetTotal(Table, "TranAmount");
        }
        catch (Exception ex)
        {
            try
            {
                SetTotal(Table, "Total");
            }
            catch (Exception exe)
            {
                lblTotal.Text = "";
            }
        }
    }

    private void SetTotal(DataTable Table, string column)
    {
        double total = 0;
        foreach (DataRow dr in Table.Rows)
        {
            double amount = double.Parse(dr[column].ToString());
            total += amount;
        }

        lblTotal.Text = "Total Amount [" + total.ToString("#,##0") + "]";
    }

    private string[] GetSearchParameters()
    {
        List<string> searchCriteria = new List<string>();
        string Vendor = ddVendor.SelectedValue;
        string utility = ddUtility.SelectedValue;
        string reference = txtReference.Text;
        string FromDate = txtFromDate.Text;
        string ToDate = txtToDate.Text;

        searchCriteria.Add(Vendor);
        searchCriteria.Add(utility);
        searchCriteria.Add(reference);
        searchCriteria.Add(FromDate);
        searchCriteria.Add(ToDate);

        return searchCriteria.ToArray();
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataGridResults.PageIndex = e.NewPageIndex;
        SearchDb();
    }

    protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //make this better later, caters only for merchant requests report
            if (e.CommandName == "Action")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = dataGridResults.Rows[index];

                string vendorTranId = row.Cells[1].Text;
                string report = ddReport.SelectedValue.ToString();
                string tranStatus = row.Cells[10].Text;
                string merchant = row.Cells[6].Text, stroredProcedure = "", DB = "";
                string PaymentChannel = "", MerchantCode = "", VendorCode = "";

                if (report.Equals(("272_TRANSACTIONS")))
                {
                    stroredProcedure = "UpdateMerchantTransactionDelayed";
                    DB = "LiveMerchantDB";
                }
                else
                {
                    stroredProcedure = "MarkGatewayRequestAsSuccessful2";
                    DB = "LiveMerchantCoreDB";
                    merchant = "STANBIC_MERCHANT";
                    PaymentChannel = row.Cells[19].Text;
                    MerchantCode = row.Cells[6].Text;
                    VendorCode = row.Cells[6].Text;
                }

                ProcessRequest(report, merchant, stroredProcedure, DB, vendorTranId, tranStatus, PaymentChannel, MerchantCode, VendorCode);
            }
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }

    protected void ProcessRequest(string report, string merchant, string stroredProcedure, string DB,
        string vendorTranId, string tranStatus, string PaymentChannel, string MerchantCode, string VendorCode)
    {
        try
        {
            string status = "";
            string telecomId = "";
            string pegPayId = "";

            if (PaymentChannel == "CARD")
            {
                InterLinkClass.PegPayGatewayApi.PegPayGateway gatewayApi = new InterLinkClass.PegPayGatewayApi.PegPayGateway();
                InterLinkClass.PegPayGatewayApi.Response gatewayResp = gatewayApi.GetTransactionStatusAtMIGs(MerchantCode, VendorCode, vendorTranId);

                status = gatewayResp.StatusDesc;
                telecomId = gatewayResp.ThirdPartyId;
                pegPayId = gatewayResp.PegPayId;
            }
            else if (PaymentChannel == "MOBILE_MONEY" || PaymentChannel == "USSD_MOBILE_MONEY")
            {
                InterLinkClass.PegPayGatewayApi.PegPayGateway gatewayApi = new InterLinkClass.PegPayGatewayApi.PegPayGateway();
                InterLinkClass.PegPayGatewayApi.MerchantSettings creds = gatewayApi.GetMerchantDetails(MerchantCode, "MOMO",VendorCode, "T3rr1613");

                DataTable dt = bll.ExecuteProcedureInMomo("GetVendorTransByRef", creds.AccessCode, vendorTranId).Tables[0];

                if (dt.Rows.Count == 0)
                {
                    if (MerchantCode.Equals("100427"))
                    {
                        dt = bll.ExecuteProcedureInMomo("GetVendorTransByRef", "STANBIC-MERCHANT-AIRTEL-PULLS", vendorTranId).Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                            throw new Exception("The transaction " + vendorTranId + " does not exist in Mobile Money");
                        }
                    }
                    else
                    {
                        throw new Exception("The transaction " + vendorTranId + " does not exist in Mobile Money");
                    }
                }

                status = dt.Rows[0]["Status"].ToString();
                telecomId = dt.Rows[0]["TelecomId"].ToString();
                pegPayId = dt.Rows[0]["PegPayTranId"].ToString();
            }
            else
            {
                DataTable dt = bll.ExecuteProcedureInMomo("GetVendorTransByRef", merchant, vendorTranId).Tables[0];

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("The transaction " + vendorTranId + " does not exist in Mobile Money");
                }

                status = dt.Rows[0]["Status"].ToString();
                telecomId = dt.Rows[0]["TelecomId"].ToString();
                pegPayId = dt.Rows[0]["PegPayTranId"].ToString();
            }

            if (status != "SUCCESS")
            {
                string msg2 = string.IsNullOrEmpty(PaymentChannel) ? "The transaction " + vendorTranId + " has a status of " + status +
                                    " in Mobile Money" : "The transaction " + vendorTranId + " has a status of " + status +
                                    " in " + PaymentChannel + " System";
                throw new Exception(msg2);
            }

            InterLinkClass.DbApi.DataAccess db = new InterLinkClass.DbApi.DataAccess();
            if (report.Equals(("272_TRANSACTIONS")))
            {
                DataSet ds = bll.ExecuteDataAccess(DB, stroredProcedure, new object[] { vendorTranId, status, status, telecomId, pegPayId });
                bll.ExecuteDataAccess(DB, "InsertIntoAuditTrail", new object[]
                {
                    "Update", "MerchantTransactions", userBranch, username,
                    fullname + " marked the transaction with Id [" + vendorTranId + "] that had a status of " +
                    tranStatus + " as successful with paymentId: " + telecomId + " from IP: " +
                    bll.GetIPAddress()
                });
            }
            else
            {
                DataSet ds = bll.ExecuteDataAccess(DB, stroredProcedure, new object[] { vendorTranId, telecomId,MerchantCode });
                bll.InsertIntoAuditLog(vendorTranId, "UPDATE", "PaymentGatewayRequests", userBranch, username,
                    bll.GetCurrentPageName(),
                    fullname + " marked the gateway request [" + vendorTranId + "] that had a status of " +
                    tranStatus + " as successful with paymentId: " + telecomId + " from IP: " +
                    bll.GetIPAddress());

            }
            bll.ShowMessage(lblmsg,
                "The transaction [" + vendorTranId + "] has been marked successful with the telecom ID " +
                telecomId, false, Session);
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }
}
