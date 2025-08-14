using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PendingSMS : System.Web.UI.Page
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

            MultiView1.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
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


    private void SearchDb()
    {
        //DataTable dt = new DataTable();
        try
        {
            //if (txtFromDate.Text.Equals(""))
            //{
            //    bll.ShowMessage(lblmsg, "From Date is required", true, Session);
            //    txtFromDate.Focus();
            //}
            //if (txtToDate.Text.Equals(""))
            //{
            //    bll.ShowMessage(lblmsg, "To Date is required", true, Session);
            //    txtFromDate.Focus();
            //}
            
            //{
                //string[] searchParams = GetSearchParameters();

                //string date = txtFromDate.Text.Trim();
                DataLogin dll = new DataLogin();
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();

                //string[] searchParams = { date };

                //DataSet ds = bll.ExecuteDataAccess("LiveSmsPortal", "GetPendingSMS", searchParams);
                dt = dll.GetPendingSMS();



                //dt = ApplyCustomFilter(dt);

                if (dt.Rows.Count > 0)
                {
                    //CalculateTotal(dt);
                   
                    string msg = "Found " + dt.Rows.Count + " pending sms.";



                    //var uniqueCategories = repository.GetAllProducts()
                    //              .Select(p => p.Category)
                    //              .Distinct();

                    //IQueryable<DataRow> dr = from DataRow dr2 in dt.Rows
                    //                         select dr2;

                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();

                    Multiview2.ActiveViewIndex = 0;

                    //count.Text = dt.Rows.Count.ToString();
                    //telecom.Text = dt.Rows[0]["Telecom"].ToString();
                    
                    //bll.ShowMessage(lblmsg, msg, false, Session);
                }else
                {
                    string msg = "NO PENDING SMS";
                    bll.ShowMessage(lblmsg, msg, false, Session);
                }
             

            //}

        }
        catch (Exception ex)
        {
           
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }

    //private IEnumerable getData()
    //{
    //    DataSet ds = bll.ExecuteDataAccess("LiveSmsPortal", "GetPendingSMSCOUNT", searchParams);
    //    dt = ds.Tables[0];

    //}
}