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
    public partial class MonthlyTransactionsReport : System.Web.UI.Page
    {
        BusinessLogin bll = new BusinessLogin();

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
                    PopulateYears();
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
            bll.LoadUtilities(vendor, ddUtility);
        }

        public void PopulateYears()
        {
            int startYear = 2010;
            List<int> years = new List<int>();
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year >= startYear; year--)
            {
                years.Add(year);
                txtYear.Items.Add(year.ToString());
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
            string Utility = ddUtility.SelectedValue;
            string month = txtMonth.SelectedValue;
            string year = txtYear.SelectedValue;
            searchCriteria.Add(Utility);
            searchCriteria.Add(month);
            searchCriteria.Add(year);


            return searchCriteria.ToArray();
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dataGridResults.PageIndex = e.NewPageIndex;
            SearchDb();
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
            string[] searchParams = GetSearchParameters();
            DataTable dt = bll.MonthlySearchVwTransactionsTable(searchParams, Session);
            string vendorCode = Session["UserBranch"].ToString();

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
        protected void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                string[] searchParams = GetSearchParameters();
                DataTable dt = bll.MonthlySearchVwTransactionsTable(searchParams, Session);
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
                            bll.ExportToExcelxx(dt, "", Response);
                            //bll.ExportToExcel(dt, "", Response);
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

    }
}