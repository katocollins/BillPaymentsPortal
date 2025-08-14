using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EODPrepaidBalances : System.Web.UI.Page
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
        DataTable dt = null;
        string[] parameters = { };
        dt= bll.GetPrepaidVendors(parameters);
        txtVendorCode.DataSource = dt;
        txtVendorCode.DataTextField = "VendorCode";
        txtVendorCode.DataValueField = "VendorCode";
        txtVendorCode.DataBind();
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
            //lblTotal.Visible = false;
        }
        else
        {
            //lblTotal.Visible = true;
        }
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string[] searchParams = GetSearchParameters();
        DataTable dt = bll.GetAllPrepaidClosingBalances(searchParams, Session);
        string vendorCode = Session["UserBranch"].ToString();

        if (dt.Rows.Count > 0)
        {
            //CalculateTotal(dt);
            DataGrid1.DataSource = dt;
            this.DataGrid1.Columns[1].HeaderText = "Balances as at (" + txtDate.Text + ")";
            DataGrid1.DataBind();

            MultiView1.ActiveViewIndex = 0;
            //MultiView2.ActiveViewIndex = -1;
            string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, false, Session);
        }
        else
        {
            DataGrid1.DataSource = null;
            DataGrid1.DataBind();
            string msg = "No Records Found Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }
    private string[] GetSearchParameters()
    {
        List<string> searchCriteria = new List<string>();
        string VendorCode = txtVendorCode.SelectedValue;
        string date = txtDate.Text;
        searchCriteria.Add(VendorCode);
        searchCriteria.Add(date);


        return searchCriteria.ToArray();
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //DataGrid1.PageIndex = e.NewPageIndex;
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
        DataTable dt = bll.GetAllPrepaidClosingBalances(searchParams, Session);
        string vendorCode = Session["UserBranch"].ToString();

        if (dt.Rows.Count > 0)
        {
            //CalculateTotal(dt);
            DataGrid1.DataSource = dt;
            //this.DataGrid1.Columns[1].HeaderText = "Balances as at (" + txtDate.Text + ")";
            DataGrid1.DataBind();

            MultiView1.ActiveViewIndex = 0;
            //MultiView2.ActiveViewIndex = -1;
            string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, false, Session);
        }
        else
        {
            DataGrid1.DataSource = null;
            DataGrid1.DataBind();
            string msg = "No Records Found Matching Search Criteria";
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }
    protected void cboVendor_DataBound(object sender, EventArgs e)
    {
        txtVendorCode.Items.Insert(0, new ListItem("ALL", "0"));
    }
    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            string[] searchParams = GetSearchParameters();
            DataTable dt = bll.GetAllPrepaidClosingBalances(searchParams, Session);
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
                        //bll.ExportToExcel(dt, "", Response);ExportToExcelxx
                        bll.ExportToExcelxx(dt, "", Response);
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