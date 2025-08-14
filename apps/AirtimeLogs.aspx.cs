using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AirtimeLogs : System.Web.UI.Page
{
    string username = "";
    string fullname = "";
    string vendor = "";
    BusinessLogin bll = new BusinessLogin();

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

            MultiView1.ActiveViewIndex = 0;
            MultiView3.ActiveViewIndex = 0;
            LoadAllAirtimeLogs();

            if (!IsPostBack)
            {

            }

        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }

    }

    private void PopulateGridView(DataTable datatable, object[] data)
    {
        if (datatable.Rows.Count > 0)
        {
            Multiview2.ActiveViewIndex = 0;
            AirtimeLogsGridView.DataSource = datatable;
            AirtimeLogsGridView.DataBind();
            AirtimeLogsGridView.UseAccessibleHeader = true;
            AirtimeLogsGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            ClearErrorMessage();
        }
        else
        {
            Multiview2.ActiveViewIndex = 1;
            InfoTxt.Text = SearchResultMessage(data[0].ToString(), "PegPay id", data[1].ToString(), data[2].ToString());

        }

    }

    private void LoadAllAirtimeLogs()
    {
        string PegpayId, start_date, end_date;
        try
        {
            PegpayId = "";
            start_date = "";
            end_date = "";
            object[] data = { PegpayId, start_date, end_date };
            DataTable datatable = SearchDB(data);
            PopulateGridView(datatable, data);

        }
        catch (Exception ex)
        {
            Multiview2.ActiveViewIndex = 1;
            InfoTxt.Text = "Encountered an exception " + ex.Message;
        }

    }

    protected void Filter_Click(object sender, EventArgs e)
    {


        try
        {

            object[] data = GetSearchParameters();
            DataTable datatable = SearchDB(data);
            PopulateGridView(datatable, data);
        }
        catch (Exception ex)
        {
            Multiview2.ActiveViewIndex = 1;
            InfoTxt.Text = "Encountered an exception " + ex.Message;
        }

    }

    private object[] GetSearchParameters()
    {
        string start_date, end_date, PegpayId;

        List<object> searchCriteria = new List<object>();
        PegpayId = PegPayId.Text.ToString();
        start_date = startDate.Value;
        end_date = endDate.Value;

        object[] data = { PegpayId, start_date, end_date };

        searchCriteria.Add(PegpayId);
        searchCriteria.Add(start_date);
        searchCriteria.Add(end_date);

        return searchCriteria.ToArray();
    }

    protected DataTable SearchDB(object[] data)
    {
        DataTable dt = new DataTable();
        try
        {
            //dt = bll.GetAirtimeTransactionLogs(data);
            DataSet ds = bll.ExecuteDataAccess("SmsClient", "GetAirtimeLogs2", data);
            dt = ds.Tables[0];
        }
        catch (Exception ex)
        {
            Multiview2.ActiveViewIndex = 1;
            InfoTxt.Text = "Encountered an exception " + ex.Message;
        }
        return dt;

    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {

        try
        {

            object[] data = GetSearchParameters();
            DataTable dt = SearchDB(data);
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

    protected void ClearErrorMessage()
    {
        InfoTxt.Text = "";
    }

    protected string SearchResultMessage(string id, string idName, string sdate, string edate)
    {
        string message = null;
        string resp = null;

        message = String.Format("Currently, there are no airtime logs {0}", resp);
        return message;

    }



}