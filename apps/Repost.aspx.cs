using InterLinkClass.ControlObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
public partial class Repost : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();

    //
    string username = "";
    string fullname = "";
    string userBranch = "";
    string userRole = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check If this is an external Request
            string Id = Request.QueryString["Id"];

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
                lblmsg.Text = "";
            }
            //this is an edit request
            else if (Id != null)
            {
                LoadData();
                MultiView1.ActiveViewIndex = 0;
            }
            else
            {
                LoadData();
                MultiView1.ActiveViewIndex = 0;
                Multiview2.ActiveViewIndex = 1;
            }

        }
        catch (Exception ex)
        {
            bll.ShowMessage(lblmsg, ex.Message, true, Session);
        }
    }

    private void LoadData()
    {

    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Session["Results"] as DataTable;
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
                        bll.ExportToExcel(dt, Response);
                    }
                    else if (rdPdf.Checked)
                    {
                        bll.ExportToPdf(dt, "", Response);
                    }
                }
                catch (Exception ex)
                {
                    string msg = "FAILED: " + ex.Message;
                    bll.ShowMessage(lblmsg, msg, true, Session);
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
            ValidateInput();
            ValidateTxns(ddInputType.SelectedValue);
        }
        catch (Exception ex)
        {
            string msg = "FAILED: " + ex.Message;
            bll.ShowMessage(lblmsg, msg, true, Session);
        }
    }

    private void ValidateInput()
    {
        if (ddInputType.SelectedValue != "FILE" && string.IsNullOrEmpty(txtValue.Text))
        {
            throw new Exception("Please provide atleast one transaction id");
        }
        if (ddInputType.SelectedValue == "FILE" && string.IsNullOrEmpty(fileUpload.FileName))
        {
            throw new Exception("Please provide a file");
        }
        if (ddInputType.SelectedValue == "FILE" && !bll.IsValidOption(Path.GetExtension(fileUpload.FileName), ".csv|.txt"))
        {
            throw new Exception("Please provide a .csv or .txt file");
        }
    }

    private void ValidateTxns(string inputType)
    {
        DataTable resultTable = bll.CreateDataTable("Transactions", new string[] { "VendorId", "Utility", "TranAmount", "Phone", "PaymentDate", "PegPayId", "Reason" });

        DataLogin dh = new DataLogin();
        ArrayList txns;
        if (inputType == "FILE")
        {
            DataFile df = new DataFile();
            txns = df.readFile(ReturnPath());
        }
        else
        {
            txns = new ArrayList(txtValue.Text.Split(','));
        }

        string msg = "Operation Complete";
        foreach (string tran in txns)
        {
            DataTable dt = dh.ExecuteDataSet("GetRequestDetails", tran).Tables[0];

            if (dt.Rows.Count > 0)
            {
                object[] data = RepostTxn(dt);
                resultTable.Rows.Add(data);
            }
            else
            {
                InterLinkClass.DbApi.DataAccess db = new InterLinkClass.DbApi.DataAccess();
                dt = db.ExecuteDataSet(@"PGSSSQL4P\PGSSARC", "LiveGenericPegPayApi", "GetRequestDetails", new string[] { tran.Trim() }).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    object[] data = RepostTxn(dt);
                    resultTable.Rows.Add(data);
                }
            }
        }

        Session["Results"] = resultTable;
        dataGridResults.DataSource = resultTable;
        dataGridResults.DataBind();

        Multiview2.ActiveViewIndex = 0;
        bll.ShowMessage(lblmsg, msg, false, Session);

    }

    private object[] RepostTxn(DataTable dt)
    {
        DataLogin dh = new DataLogin();
        BusinessLogin bll = new BusinessLogin();
        string[] tran = { dt.Rows[0]["VendorCode"].ToString(), dt.Rows[0]["VendorTranId"].ToString() };
        dh.ExecuteNonQuery("DeleteFromDuplicateTable", tran);

        System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidation;
        InterLinkClass.PegPayApi.PegPay pegpay = new InterLinkClass.PegPayApi.PegPay();
        InterLinkClass.PegPayApi.QueryRequest query = new InterLinkClass.PegPayApi.QueryRequest();
        InterLinkClass.PegPayApi.TransactionRequest trans = new InterLinkClass.PegPayApi.TransactionRequest();

        string VendorCode = dt.Rows[0]["VendorCode"].ToString();
        string Password = "68R31WJ032";
        string vendorTransRef = dt.Rows[0]["VendorTranId"].ToString();
        string area = "";

        query.QueryField1 = dt.Rows[0]["CustomerRef"].ToString();// Custref;
        query.QueryField4 = dt.Rows[0]["UtilityCode"].ToString();//Utility;
        query.QueryField5 = VendorCode;//VendorCode;
        query.QueryField6 = Password;//Password;
        query.QueryField2 = "";//Area;
        if (query.QueryField4 == "NWSC")
        {
            query.QueryField2 = area = GetArea(query.QueryField1); 
        }
        

        InterLinkClass.PegPayApi.Response result = pegpay.QueryCustomerDetails(query);

        if (!result.ResponseField6.Equals("0"))
        {
            bll.InsertIntoAuditLog(vendorTransRef, "UPDATE", "Repost", userBranch, username, bll.GetCurrentPageName(), fullname + " tried to repost the transaction " + vendorTransRef + " Validation Result: " + result.ResponseField7);

            throw new Exception(result.ResponseField7);
        }

        string Custref = query.QueryField1;
        string customerName = result.ResponseField2;
        string customerType = result.ResponseField5;
        string paymentDate = dt.Rows[0]["PaymentDate"].ToString();
        string paymentType = "2";
        string tranAmount = dt.Rows[0]["TranAmount"].ToString();
        string tranType = "CASH";
        string customerTel = dt.Rows[0]["CustomerTel"].ToString();//"256775117666";
        string reversal = "0";
        string tranIdToReverse = "";
        string teller = dt.Rows[0]["CustomerTel"].ToString();//"256775117666";
        string offline = "0";
        string chequeNumber = "";
        string narration = query.QueryField1; //"REPOSTED";
        string email = "";
        
        string companyCode = dt.Rows[0]["UtilityCode"].ToString();


        string dataToSign = Custref +
                            customerName +
                            customerTel +
                            vendorTransRef +
                            VendorCode +
                            Password +
                            paymentDate +
                            teller +
                            tranAmount +
                            narration +
                            tranType;


        trans.PostField1 = Custref;
        trans.PostField2 = customerName;
        trans.PostField3 = area;
        trans.PostField4 = companyCode;
        trans.PostField21 = customerType;
        trans.PostField5 = paymentDate;
        trans.PostField6 = paymentType;
        trans.PostField7 = tranAmount;
        trans.PostField8 = tranType;
        trans.PostField9 = VendorCode;
        trans.PostField10 = Password;
        trans.PostField11 = customerTel;
        trans.PostField12 = reversal;
        trans.PostField13 = tranIdToReverse;
        trans.PostField14 = teller;
        trans.PostField15 = offline;
        trans.PostField17 = chequeNumber;
        trans.PostField18 = narration;
        trans.PostField19 = email;
        trans.PostField20 = vendorTransRef;
        trans.PostField16 = "1234";//SignCertificate(dataToSign);

        InterLinkClass.PegPayApi.Response resp = pegpay.PostTransaction(trans);

        if (resp.ResponseField6.Equals("0"))
        {
            //DataLogin dh = new DataLogin ();
            dh.ExecuteDataSet("UpdateSentToVendor", vendorTransRef, 1);
        }

        bll.InsertIntoAuditLog(vendorTransRef, "UPDATE", "Repost", userBranch, username, bll.GetCurrentPageName(), fullname + " reposted the transaction " + vendorTransRef + " Result: " + resp.ResponseField7);

        return new object[] { vendorTransRef, companyCode, tranAmount, customerTel, paymentDate, resp.ResponseField8, resp.ResponseField7 };
        
    }

    private string GetArea(string reference)
    {
        DataLogin dh = new DataLogin();
        DataTable dt = dh.ExecuteDataSet("GetCustomerDetails", reference).Tables[0];

        return dt.Rows[0]["Area"].ToString();
    }

    private static bool RemoteCertificateValidation(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    private string ReturnPath()
    {
        string filename = HttpUtility.HtmlEncode(Path.GetFileName(fileUpload.FileName));
        string extension = HttpUtility.HtmlEncode(Path.GetExtension(filename));
        DateTime now = DateTime.Now;
        string dt = now.ToString("hhmmss");
        DataTable returnedPath = new DataTable();
        string folder = @"E:\Logs\UtilitiesApiLogs\RepostLogs\";
        string User = Session["UserName"].ToString().Replace(" ", "-").Replace(".", "");
        filename = User + filename;
        string filepath = folder + dt + filename;
        if (File.Exists(filepath))
        {
            //File.Delete(filepath);
        }
        fileUpload.SaveAs(filepath);
        return filepath;
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataGridResults.PageIndex = e.NewPageIndex;
        DataTable dt = Session["Results"] as DataTable;
        dataGridResults.DataSource = dt;
        dataGridResults.DataBind();
        Multiview2.ActiveViewIndex = 0;
    }

}
}