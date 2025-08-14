using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class NWSCReversals : System.Web.UI.Page
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
                //LoadData();
                MultiView1.ActiveViewIndex = 0;
            }
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
            InsertReversalRequest();
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
            throw ex;
        }
    }
    private void InsertReversalRequest()
    {
        try
        {
            string transactionID = textbox1.Text.Trim();
            string recordedBy = Session["UserName"].ToString();

            List<string> nonExistingOnes = new List<string>();
            List<string> loggedOnes = new List<string>();


            try
            {

                DataTable dt = new DataTable();

                if (!bll.CheckIfReversalRequestLogged(transactionID))
                {
                    dt = bll.CheckIfReversalNWSCExists(transactionID);
                    if (dt.Rows.Count > 0)
                    {

                        MultiView1.ActiveViewIndex = 0;
                        DataGrid1.DataSource = dt;
                        DataGrid1.DataBind();
                        DataGrid1.UseAccessibleHeader = true;
                        DataGrid1.HeaderRow.TableSection = TableRowSection.TableHeader;
                        //bll.AddReversalRequest(id, reason, recordedBy);
                        //bll.ShowMessage(lblmsg, "Reversal added successfully", true, Session);
                    }
                    else
                    {

                        bll.ShowMessage(lblmsg, "No record found", true, Session);
                    }
                }
                else
                {
                    bll.ShowMessage(lblmsg, "Reversal was already added", true, Session);
                }





            }
            catch (Exception ex)
            {

                //bll.AddReversalRequest(transactionID, reason, recordedBy);
                throw ex;
            }



            //bll.ShowMessage(lblmsg, "Reversal added successfully", true, Session);
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
            throw ex;
        }

    }

    protected void gvOnRowCommand(object source, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "btnComplete")
            {

                foreach (GridViewRow row in DataGrid1.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string tranid = row.Cells[2].Text;
                        //reason = row.Cells [11].Text;

                        TextBox txtedit = (TextBox)row.FindControl("txtedit");
                        string reason = txtedit.Text;

                        //Fail and reverse
                        string recordedBy = Session["UserName"].ToString();
                        bll.AddReversalRequest(tranid, reason, recordedBy);

                        bll.ShowMessage(lblmsg, "Reversal added successfully", true, Session);




                    }
                }
            }
        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }

}