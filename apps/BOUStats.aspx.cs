using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                MultiView1.ActiveViewIndex = 0;
                Multiview2.ActiveViewIndex = 1;
            }

        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }

    private string[] GetSearchParameters()
    {
        List<string> searchCriteria = new List<string>();
        //string Utility = ddUtility.SelectedValue;
        string month = txtMonth.SelectedValue;
        string year = txtYear.Text;
        //searchCriteria.Add(Utility);
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
            string yearText = txtYear.Text;
            int year;

            if (int.TryParse(yearText, out year) && year >= 1000 && year <= 9999)
            {
                SearchDb();
            }
            else
            {
                string msg = "Enter the right year e.g 2023";
                bll.ShowMessage(lblmsg, msg, true, Session);
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
        string[] searchParams = GetSearchParameters();
        DataTable dt = bll.BOUStatisticsTable(searchParams, Session);
        DataTable uniqueValuesTable = new DataTable();

        try
        {
            // Replace empty or null values with 0
            foreach (DataRow row in dt.Rows)
            {
                if (row["Count"] == DBNull.Value || string.IsNullOrEmpty(row["Count"].ToString()))
                    row["Count"] = 0;

                if (row["Value"] == DBNull.Value || string.IsNullOrEmpty(row["Value"].ToString()))
                    row["Value"] = 0.0m;
            }

            // Retrieve rows with the same value of Category and Vendor columns
            var matchedRows = from DataRow row in dt.Rows
                              group row by new
                              {
                                  Category = row["Category"],
                                  Vendor = row["Vendor"]
                              } into groupedRows
                              select new
                              {
                                  Category = groupedRows.Key.Category,
                                  Vendor = groupedRows.Key.Vendor,
                                  Count = groupedRows.Sum(r => Convert.ToInt32(r["Count"])),
                                  Value = groupedRows.Sum(r => Convert.ToDecimal(r["Value"]))
                              };

            // Create a new DataTable with unique values
            uniqueValuesTable.Columns.Add("Category", typeof(string));
            uniqueValuesTable.Columns.Add("Vendor", typeof(string));
            uniqueValuesTable.Columns.Add("Count", typeof(int));
            uniqueValuesTable.Columns.Add("Value", typeof(decimal));

            // Add the aggregated rows to the uniqueValuesTable
            foreach (var row in matchedRows)
            {
                uniqueValuesTable.Rows.Add(row.Category, row.Vendor, row.Count, row.Value);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
        if (uniqueValuesTable.Rows.Count > 0)
        {
            //CalculateTotal(dt);
            dataGridResults.DataSource = uniqueValuesTable;
            dataGridResults.DataBind();

            Multiview2.ActiveViewIndex = 0;
            //string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
            //bll.ShowMessage(lblmsg, msg, false, Session);
        }
        else
        {
            //CalculateTotal(dt);
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
            DataTable dt = bll.BOUStatisticsTable(searchParams, Session);

            DataTable uniqueValuesTable = new DataTable();

            try
            {
                // Replace empty or null values with 0
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Count"] == DBNull.Value || string.IsNullOrEmpty(row["Count"].ToString()))
                        row["Count"] = 0;

                    if (row["Value"] == DBNull.Value || string.IsNullOrEmpty(row["Value"].ToString()))
                        row["Value"] = 0.0m;
                }

                // Retrieve rows with the same value of Category and Vendor columns
                var matchedRows = from DataRow row in dt.Rows
                                  group row by new
                                  {
                                      Category = row["Category"],
                                      Vendor = row["Vendor"]
                                  } into groupedRows
                                  select new
                                  {
                                      Category = groupedRows.Key.Category,
                                      Vendor = groupedRows.Key.Vendor,
                                      Count = groupedRows.Sum(r => Convert.ToInt32(r["Count"])),
                                      Value = groupedRows.Sum(r => Convert.ToDecimal(r["Value"]))
                                  };

                // Create a new DataTable with unique values
                uniqueValuesTable.Columns.Add("Category", typeof(string));
                uniqueValuesTable.Columns.Add("Vendor", typeof(string));
                uniqueValuesTable.Columns.Add("Count", typeof(int));
                uniqueValuesTable.Columns.Add("Value", typeof(decimal));

                // Add the aggregated rows to the uniqueValuesTable
                foreach (var row in matchedRows)
                {
                    uniqueValuesTable.Rows.Add(row.Category, row.Vendor, row.Count, row.Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (uniqueValuesTable.Rows.Count > 0)
            {
                try
                {
                    if (!rdPdf.Checked && !rdExcel.Checked)
                    {
                        bll.ShowMessage(lblmsg, "CHECK ONE EXPORT OPTION", true, Session);
                    }
                    else if (rdExcel.Checked)
                    {
                        bll.ExportToExcelxx(uniqueValuesTable, "", Response);
                        //bll.ExportToExcel(dt, "", Response);
                    }
                    else if (rdPdf.Checked)
                    {
                        bll.ExportToPdf(uniqueValuesTable, "", Response);
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