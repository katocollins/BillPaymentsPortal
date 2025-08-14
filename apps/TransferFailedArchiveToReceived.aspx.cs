using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class TransferFailedArchiveToReceived : System.Web.UI.Page
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

                if (!IsPostBack)
                {

                }

            }
            catch (Exception ex)
            {
                bll.ShowMessage(lblmsg, ex.Message, true, Session);
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
                MultiView2.ActiveViewIndex = 1;
                InfoTxt.Text = "Encountered an exception " + ex.Message;
            }

        }

        private object[] GetSearchParameters()
        {
            string TranID;

            List<object> searchCriteria = new List<object>();
            TranID = TranId.Text.ToString();

            object[] data = { TranID };

            searchCriteria.Add(TranID);

            return searchCriteria.ToArray();
        }

        protected DataTable SearchDB(object[] data)
        {
            DataTable dt = null;
            try
            {
                dt = bll.GetFailedTransactionsArchiveLogs(data);
            }
            catch (Exception ex)
            {
                MultiView2.ActiveViewIndex = 1;
                InfoTxt.Text = "Encountered an exception " + ex.Message;
            }
            return dt;

        }

        private void PopulateGridView(DataTable datatable, object[] data)
        {
            if (datatable.Rows.Count > 0)
            {
                MultiView2.ActiveViewIndex = 0;
                FailedTransactionsArchiveLogsGridView.DataSource = datatable;
                FailedTransactionsArchiveLogsGridView.DataBind();
                FailedTransactionsArchiveLogsGridView.UseAccessibleHeader = true;
                FailedTransactionsArchiveLogsGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                ClearErrorMessage();
            }
            else
            {
                MultiView2.ActiveViewIndex = 1;
                InfoTxt.Text = "Encountered an exception";

            }

        }

        protected void ClearErrorMessage()
        {
            InfoTxt.Text = "";
        }

        protected void Retry_Click(object sender, EventArgs e)
        {
            try
            {
                string VendorTranId, Status;
                object[] data = GetSearchParameters();
                DataTable datatable = SearchDB(data);
                VendorTranId = datatable.Rows[0]["VendorTranId"].ToString();

                data = data.Concat(new object[] { VendorTranId }).ToArray();

                DataTable dt = QueryDB(data);
                Status = dt.Rows[0]["Status"].ToString();
                if (Status == "SUCCESS")
                {
                    InfoTxt.Text = "Successful. Record is in INSERTED.";
                }
                else if (Status == "DUPLICATE")
                {
                    InfoTxt.Text = "Duplicate Record.";
                }
                else
                {
                    InfoTxt.Text = "Encountered an exception ";
                }

            }
            catch (Exception ex)
            {
                MultiView2.ActiveViewIndex = 1;
                InfoTxt.Text = "Encountered an exception " + ex.Message;
            }
        }

        protected DataTable QueryDB(object[] data)
        {
            DataTable dt = null;
            try
            {
                dt = bll.TransferFailedArchive2Duplicate(data);
            }
            catch (Exception ex)
            {
                MultiView2.ActiveViewIndex = 1;
                InfoTxt.Text = "Encountered an exception " + ex.Message;
            }
            return dt;

        }
    }
}