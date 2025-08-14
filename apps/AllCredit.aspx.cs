using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class AllCredit : System.Web.UI.Page
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
        }

        private void ClearPage()
        {
            dataGridResults.DataSource = new DataTable();
            dataGridResults.DataBind();
            lblCount.Text = "";
            lblTotal.Text = "";
            lblmsg.Text = "";
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
                if (txtFromDate.Text.Equals(""))
                {
                    bll.ShowMessage(lblmsg, "From Date is required", true, Session);
                    txtFromDate.Focus();
                }
                else
                {
                    string[] searchParams = GetSearchParameters();
                    DataSet ds = bll.ExecuteProcedure("GetCreditsDetails", searchParams);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        CalculateTotal(dt);
                        dataGridResults.DataSource = dt;
                        dataGridResults.DataBind();
                        string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
                        Multiview2.ActiveViewIndex = 0;
                        bll.ShowMessage(lblmsg, msg, false, Session);
                        //show button field
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
            string reference = txtReference.Text;
            string FromDate = txtFromDate.Text;
            string ToDate = txtToDate.Text;

            searchCriteria.Add(Vendor);
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
                    // Retrieve the row that contains the button clicked by the user from the Rows collection.
                    GridViewRow row = dataGridResults.Rows[index];

                    string vendorCode = row.Cells[1].Text;
                    string vendorTranId = row.Cells[2].Text;
                    string amount = row.Cells[3].Text;
                    string status = row.Cells[6].Text;

                    string newTranId = vendorTranId + "R";

                    if (status != "APPROVED")
                    {
                        throw new Exception("You can't reverse a rejected credit.");
                    }

                    //first get status in mobile money
                    bll.ExecuteProcedure("ReversePrepaidVendorCredit", vendorCode, "", amount, newTranId, "REVERSE" + vendorTranId, "DEBIT");
                    bll.ExecuteProcedure("UpdateCreditReason", vendorTranId, "REVERSED");

                    bll.InsertIntoAuditLog(vendorTranId, "INSERT", "ReceivedPrepaidTransactions", userBranch, username, bll.GetCurrentPageName(),
                  fullname + " reversed the prepaid vendor credit to [" + vendorCode + "] of " + amount + " with a transaction Id of : " + vendorTranId + " from IP: " + bll.GetIPAddress());


                    bll.ShowMessage(lblmsg, "The credit [" + vendorTranId + "] has been reversed successful with the transaction ID " + newTranId, false, Session);

                }
            }
            catch (Exception ex)
            {
                bll.ShowMessage(lblmsg, ex.Message, true, Session);
            }
        }


        protected void dataGridResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                if (e.Row.Cells[6].Text == "REJECTED")
                {
                    e.Row.Cells[6].BackColor = Color.Red;
                }
                else
                {
                    e.Row.Cells[6].BackColor = Color.LawnGreen;
                }

                if (e.Row.Cells[9].Text == "REVERSED")
                {
                    e.Row.Cells[9].BackColor = Color.Red;
                }
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
                            //bll.ExportToExcel(dt, "", Response);
                            bll.ExportToExcelNew(dt, "", Response);
                        }
                        else if (rdPdf.Checked)
                        {
                            bll.ExportToPdfNew(dt, "", Response);
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
    }
}