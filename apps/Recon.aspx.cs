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
    public partial class Recon : System.Web.UI.Page
    {
        Datapay dp = new Datapay();
        BusinessLogin bll = new BusinessLogin();
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
                    //Multiview2.ActiveViewIndex = 1;
                }
            }
            catch (Exception ex)
            {
                bll.ShowMessage(lblmsg, ex.Message, true, Session);
            }
        }

        private void LoadData()
        {
            bll.LoadTelecomsNewNew(ddTelecom);
            //bll.LoadBillpaymentsUtilities(userBranch, ddTelecom);
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
                            bll.ExportToExcelNew(dt, "ReconStats", Response);
                        }
                        else if (rdPdf.Checked)
                        {
                            bll.ExportToPdfNew(dt, "ReconStats", Response);
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
        private DataTable SearchDb()
        {
            DataTable dataTable = new DataTable();
            try
            {
                string vendor = ddTelecom.SelectedValue;
                string fromDate = txtfromDate.Text;
                string toDate = txttoDate.Text;

                if (string.IsNullOrEmpty(fromDate))
                {
                    bll.ShowMessage(lblmsg, "Please select the From Date", true, Session);
                    txtfromDate.Focus();
                }

                else
                {
                    if (string.IsNullOrEmpty(vendor))
                    {
                        vendor = "";
                    }

                    dataTable = dp.ExecuteDataSet("GetGroupedReconciliationStatistics", vendor, fromDate, toDate).Tables[0];
                    if (dataTable.Rows.Count > 0)
                    {
                        bll.ShowMessage(lblmsg, dataTable.Rows.Count + " Record(s) found", false, Session);
                        MultiView1.ActiveViewIndex = 0;
                        MultiView2.ActiveViewIndex = 0;
                        dataGridResults.DataSource = dataTable;
                        dataGridResults.DataBind();
                    }
                    else
                    {
                        bll.ShowMessage(lblmsg, "No Records Found", true, Session);
                        MultiView1.ActiveViewIndex = -1;
                        //MultiView2.ActiveViewIndex = -1;
                    }

                }
            }
            catch (Exception ex)
            {
                //bll.ShowMessage(lblmsg, ex.Message, true, Session);
                bll.ShowMessage(lblmsg, "Error Occured", true, Session);

            }
            return dataTable;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                SearchDb();
            }
            catch (Exception ex)
            {
                //bll.ShowMessage(lblmsg, ex.Message, true, Session);
                bll.ShowMessage(lblmsg, "Error Occured", true, Session);

            }
        }
        protected void ddTelecom_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string telecom = ddTelecom.SelectedValue.ToString();

            //bll.LoadTelecomsNewNew(telecom);

        }
        protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //dataGridResults.PageIndex = e.NewPageIndex;
            //SearchDb();
        }



    }
}