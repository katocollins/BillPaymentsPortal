using InterLinkClass.ControlObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class MTNDataReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                DataLogin dl = new DataLogin();
                BusinessLogin bll = new BusinessLogin();
                string sdate = startDate.Text.Trim();
                string edate = endDate.Text.Trim();
                string tel = telno.Text.Trim();
                string status = Status.Text.Trim();
                string[] pars = { "MTN", tel, sdate, edate, "", "" };
                if (status.Equals("1"))
                {
                    dt = dl.GetDataReport(pars);
                }
                else if (status.Equals("2"))
                {
                    dt = dl.GetFailedDataReport(pars);
                }
                else
                {
                    dt = dl.GetCombinedDataReport(pars);
                }
                dataGridResults.DataSource = dt;
                dataGridResults.DataBind();
            }
            catch (Exception ex)
            {
                msg.Text = ex.Message;
            }


        }
        protected void btnCSV_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                DataLogin dl = new DataLogin();
                BusinessLogin bll = new BusinessLogin();
                string sdate = startDate.Text.Trim();
                string edate = endDate.Text.Trim();
                string tel = telno.Text.Trim();
                string status = Status.Text.Trim();
                string[] pars = { "MTN", tel, sdate, edate, "", "" };
                if (status.Equals("1"))
                {
                    dt = dl.GetDataReport(pars);
                }
                else if (status.Equals("2"))
                {
                    dt = dl.GetFailedDataReport(pars);
                }
                else
                {
                    dt = dl.GetCombinedDataReport(pars);
                }

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition", "attachment; filename=DataReport.csv;");

                StringBuilder sb = new StringBuilder();

                string[] columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in dt.Rows)
                {
                    //string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();
                    string rowItem = "";
                    foreach (var item in row.ItemArray)
                    {
                        rowItem += '"' + item.ToString() + '"' + ",";
                    }
                    sb.AppendLine(rowItem);
                }

                // the most easy way as you have type it
                response.Write(sb.ToString());


                response.Flush();
                response.End();
            }
            catch (Exception err)
            {
                lblmg.Text = "Error: " + err.Message;
            }

        }
    }
}