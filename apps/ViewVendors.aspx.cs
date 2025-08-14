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
    public partial class ViewVendors : System.Web.UI.Page
    {
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
                    MultiView1.ActiveViewIndex = 0;
                    Multiview2.ActiveViewIndex = 1;
                }
            }
            catch (Exception ex)
            {
                bll.ShowMessage(lblmsg, ex.Message, true, Session);
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
                DataSet ds = bll.Search("SearchVendors", searchParams);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();
                    string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
                    Multiview2.ActiveViewIndex = 0;
                    bll.ShowMessage(lblmsg, msg, false, Session);
                }
                else
                {
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
            string state = ddState.SelectedValue;
            string value = txtReference.Text;

            searchCriteria.Add(value);
            searchCriteria.Add(state);

            return searchCriteria.ToArray();
        }

        protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            GridViewRow row;
            GridView grid = sender as GridView;

            index = Convert.ToInt32(e.CommandArgument);
            row = grid.Rows[index];
            string vendor = row.Cells[1].Text;
            string utility = row.Cells[2].Text;

            if (e.CommandName.Equals("EditRow"))
            {
                Server.Transfer("./AddVendor.aspx?transferid=" + vendor + "&utility=" + utility, true);
            }
        }

        //RowDataBound Event  
        protected void dataGridResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                if (e.Row.Cells[6].Text == "NO")
                {
                    e.Row.Cells[6].BackColor = Color.Red;
                }
                else
                {
                    e.Row.Cells[6].BackColor = Color.LawnGreen;
                }
            }
        }
    }
}