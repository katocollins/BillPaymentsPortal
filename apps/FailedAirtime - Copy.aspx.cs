using InterLinkClass.ControlObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class FailedAirtime : System.Web.UI.Page
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
                //MultiView2.ActiveViewIndex = 0;

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
            string VendortranId;

            List<object> searchCriteria = new List<object>();
            VendortranId = VendorTranId.Text.ToString();

            object[] data = { VendortranId };

            searchCriteria.Add(VendortranId);

            return searchCriteria.ToArray();
        }

        protected DataTable SearchDB(object[] data)
        {
            DataTable dt = null;
            try
            {
                dt = bll.GetAirtimeFailedTransaction(data);
            }
            catch (Exception ex)
            {
                MultiView2.ActiveViewIndex = 1;
                InfoTxt.Text = "No Airtime  Log for the Vendor TranID.";
            }
            return dt;

        }

        private void PopulateGridView(DataTable datatable, object[] data)
        {
            if (datatable.Rows.Count > 0)
            {
                MultiView2.ActiveViewIndex = 0;
                AirtimeFailedLogsGridView.DataSource = datatable;
                AirtimeFailedLogsGridView.DataBind();
                AirtimeFailedLogsGridView.UseAccessibleHeader = true;
                AirtimeFailedLogsGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                ClearErrorMessage();
            }
            else
            {
                MultiView2.ActiveViewIndex = 1;
                InfoTxt.Text = "Encountered an exception ";

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
                object[] data = GetSearchParameters();
                string response = UpdateDB(data);
                if (response == "SUCCESS")
                {
                    InfoTxt.Text = "Successfully Inserted for Retrying";
                }

            }
            catch (Exception ex)
            {
                MultiView2.ActiveViewIndex = 1;
                InfoTxt.Text = "Encountered an exception " + ex.Message;
            }
        }

        protected string UpdateDB(object[] data)
        {
            string resp = null;
            try
            {
                resp = bll.RetryAirtimebyVendorTranID(data);
            }
            catch (Exception ex)
            {
                MultiView2.ActiveViewIndex = 1;
                InfoTxt.Text = "Encountered an exception " + ex.Message;
            }
            return resp;

        }
    }
}