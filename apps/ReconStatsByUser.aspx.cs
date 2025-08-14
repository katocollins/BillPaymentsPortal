using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReconStatsByUser : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();
    Datapay dp = new Datapay();
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
    protected void LoadData()
    {
        bll.LoadTelecomsNewNew(ddTelecom);
        bll.LoadAccountsToReconcileNew(ddTelecom.SelectedValue, ddOva);
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            SearchDb();
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, "Error Occured", true, Session);

            //bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataGridResults.PageIndex = e.NewPageIndex;
        SearchDb();
    }
    protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //int index = 0;
        //GridViewRow row;
        //GridView grid = sender as GridView;

        //if (e.CommandName.Equals("editing"))
        //{
        //    index = Convert.ToInt32(e.CommandArgument);
        //    row = grid.Rows[index];
        //    string id = row.Cells[1].Text;
        //    Server.Transfer("ManageVendor.aspx?id=" + id);
        //}
    }

    private DataTable SearchDb()
    {
        DataTable dataTable = new DataTable();
        try
        {
            string vendor = ddTelecom.SelectedValue;
            string ovaChoice = ddOva.SelectedValue;
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
                    ovaChoice = "";
                }

                dataTable = dp.ExecuteDataSet("GetUserReconciliationStatisticsPrepaid", ovaChoice, fromDate, toDate).Tables[0];
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
                    MultiView2.ActiveViewIndex = -1;
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
    protected void ddTelecom_SelectedIndexChanged(object sender, EventArgs e)
    {
        string telecom = ddTelecom.SelectedValue.ToString();

        bll.LoadAccountsToReconcileNew(telecom, ddOva);

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
                        bll.ExportToExcelNew(dt, "UserReconStats", Response);
                    }
                    else if (rdPdf.Checked)
                    {
                        bll.ExportToPdfNew(dt, "UserReconStats", Response);
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