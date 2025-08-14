using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace apps
{
    public partial class VendorUtilityCredentials : System.Web.UI.Page
    {
        BusinessLogin bll = new BusinessLogin();
        InterLinkClass.CoreMerchantAPI.Service service = new InterLinkClass.CoreMerchantAPI.Service();
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
            SystemReport report = new SystemReport();
            report.Database = "LiveMerchantCoreDB";
            report.ReportType = "RECON_EXCEPTIONS";
            bll.LoadUtilities(ddUtility, report);
            LoadCurrencies();
        }

        private void LoadCurrencies()
        {
            string[] SearchParams = { };
            //dtable = bll.ExecuteDataAccess("LiveMerchantCoreDB", "GetMerchantCommissionAccounts").Tables[0];
            DataTable dtable = service.ExecuteDataSet("GetAllCurrencies", SearchParams).Tables[0];
            ddCurrency.DataSource = dtable;
            ddCurrency.DataValueField = "CurrencyCode";
            ddCurrency.DataTextField = "CurrencyCode";
            ddCurrency.DataBind();
        }
        protected void ddUtility_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SearchDb();
            }
            catch (Exception ex)
            {
                bll.LoadUtilities(userBranch, ddUtility);
            }
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
                string[] searchParams = GetSearchParameters();
                DataSet ds = bll.ExecuteDataAccess("LiveMerchantCoreDB", "SearchReconciliationExceptions", searchParams);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    btnUpdate.Visible = true;
                    txtComment.Visible = true;
                    lblTotal.Text = bll.CalculateTotal(dt, "TranAmount");
                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();
                    string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
                    Multiview2.ActiveViewIndex = 0;
                    bll.ShowMessage(lblmsg, msg, false, Session);
                }
                else
                {
                    btnUpdate.Visible = false;
                    txtComment.Visible = false;
                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();
                    string msg = "No Records Found Matching Search Criteria";
                    bll.ShowMessage(lblmsg, msg, true, Session);
                }
            }
            catch (Exception ex)
            {
                dataGridResults.DataSource = dt;
                dataGridResults.DataBind();
                bll.ShowMessage(lblmsg, ex.Message, true, Session);
            }
            return dt;
        }


        private string[] GetSearchParameters()
        {
            List<string> searchCriteria = new List<string>();
            string State = ddState.SelectedValue;
            string utility = ddUtility.SelectedValue;
            string value = txtReference.Text.Trim();
            string fromDate = txtFromDate.Text.Trim();
            string toDate = txtToDate.Text.Trim();
            string currency = ddCurrency.SelectedValue.ToString();

            searchCriteria.Add(State);
            searchCriteria.Add(utility);
            searchCriteria.Add(value);
            searchCriteria.Add(fromDate);
            searchCriteria.Add(toDate);
            searchCriteria.Add(currency);

            return searchCriteria.ToArray();
        }

        protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            GridViewRow row;
            GridView grid = sender as GridView;

            index = Convert.ToInt32(e.CommandArgument);
            row = grid.Rows[index];
            string tranId = row.Cells[1].Text;

            if (e.CommandName.Equals("EditRow"))
            {
                Server.Transfer("./ExceptionHandling.aspx?transferid=" + tranId + "&utility=", true);
            }
        }

        //RowDataBound Event  
        protected void dataGridResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                if (e.Row.Cells[6].Text == "YES")
                {
                    e.Row.Cells[6].BackColor = Color.LawnGreen;
                }
                else
                {
                    e.Row.Cells[6].BackColor = Color.Red;
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtComment.Text.Trim()))
                {
                    throw new Exception("Please provide a comment");
                }

                foreach (GridViewRow row in dataGridResults.Rows)
                {
                    string tranId = row.Cells[1].Text;
                    InterLinkClass.CoreMerchantAPI.Result res = bll.ExecuteDataQueryMerchant("LiveMerchantCoreDB", "UpdateReconException", tranId, txtComment.Text.Trim(), "", username);
                }

                bll.ShowMessage(lblmsg, "The chosen exceptions have been cleared", true, Session);
            }
            catch (Exception ex)
            {
                string msg = "FAILED: " + ex.Message;
                bll.ShowMessage(lblmsg, msg, true, Session);
            }
        }

        protected void ddCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            // bll.LoadUtilities()
        }
    }
}