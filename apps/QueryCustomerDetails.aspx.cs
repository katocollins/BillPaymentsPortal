using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InterLinkClass.EntityObjects;

public partial class QueryCustomerDetails : System.Web.UI.Page
{
    string userBranch = "";
    DataLogin dl = new DataLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            userBranch = Session["UserBranch"] as string;
            if (Session["UserBranch"] == null)
            {
                //Response.Redirect("Default.aspx");

            }
            if (!IsPostBack)
            {
                LoadUtilities();
            }
        }
        catch (Exception ee)
        {

        }

    }
    protected void ok_Click(object sender, EventArgs e)
    {
        MultiView3.ActiveViewIndex = -1;
        txt_custRef.Text = "";// response.ResponseField1;
        txtCustType.Text = "";// response.ResponseField5;
        txtbal.Text = "";//response.ResponseField4;
        txtname.Text =  "";//response.ResponseField2;
        txt_area.Text = "";
        txtCustRef.Text = "";
    }

    private void LoadUtilities()
    {
        try
        {
            BusinessLogin bll = new BusinessLogin ();
            bll.LoadUtilities(userBranch, ddl_utilities);
        }
        catch (Exception ee)
        {

        }
    }
    private void ShowMessage(string Message, bool Error)
    {
        Label lblmsg = (Label)Master.FindControl("lblmsg");
        if (Error) { lblmsg.ForeColor = System.Drawing.Color.Red; lblmsg.Font.Bold = false; }
        else { lblmsg.ForeColor = System.Drawing.Color.Black; lblmsg.Font.Bold = true; }
        if (Message == ".")
        {
            lblmsg.Text = ".";
        }
        else
        {
            lblmsg.Text = "MESSAGE: " + Message.ToUpper();
        }
    } 

    protected void Button2_Click(object sender, EventArgs e)
    {

        try
        {
            //BusinessLogin logic = new BusinessLogin();
            string vendorCode = Session["UserBranch"].ToString();
            string utilityCode = ddl_utilities.SelectedValue.ToString().ToUpper().Trim();
            string reference = txtCustRef.Text.Trim().ToString();

            DataTable table = dl.GetVendorCredentials(vendorCode);
            if (table.Rows.Count > 0)
            {
                //string utilityUserName = table.Rows[0]["UtilityUsername"].ToString();
                string utilityPassword = table.Rows[0]["VendorPassword"].ToString();
                BusinessLogin bll = new BusinessLogin();
                utilityPassword = bll.DecryptString(utilityPassword);
                InterLinkClass.PegPayApi.PegPay service = new InterLinkClass.PegPayApi.PegPay();
                InterLinkClass.PegPayApi.QueryRequest request = new InterLinkClass.PegPayApi.QueryRequest();
                InterLinkClass.PegPayApi.Response response = new InterLinkClass.PegPayApi.Response();

                request.QueryField1 = reference;// "65519"; //"203016921";// "P170000000086";//"2142995";//"01454392760";// "04230081848";
                request.QueryField2 = "";///area //ACSSW4
                request.QueryField4 = utilityCode;// "UMEME";// "STB_SCHOOL";//utilitycode//"URA";
                request.QueryField5 = vendorCode;//vendorcode//"Micropay";//"STN";
                request.QueryField6 = utilityPassword;

                response = service.QueryCustomerDetails(request);

                if (response.ResponseField6 == "0")
                {
                    txt_custRef.Text = response.ResponseField1;
                    txtCustType.Text = response.ResponseField5;
                    txtbal.Text = response.ResponseField4;
                    txtname.Text = response.ResponseField2;
                    MultiView3.ActiveViewIndex = 0;
                    ShowMessage(response.ResponseField7, false);
                }
                else
                {
                    MultiView3.ActiveViewIndex = -1;
                    ShowMessage(response.ResponseField7, true);
                }

            }
            else
            {

                ShowMessage("Could not load vendor credentials", true);
            }
        }
        catch (Exception ee)
        {

        }
    }
    protected void ddl_utilities_SelectedIndexChanged(object sender, EventArgs e)
    {
        string utilityCode = ddl_utilities.SelectedValue.ToString().Trim().ToUpper();
        if (utilityCode == "NWSC")
        {
            txt_area.Text = "";
            txt_area.Enabled = true;
        }
        else
        {
            txt_area.Enabled = false;
            txt_area.Text = "";
        }
    }
}
