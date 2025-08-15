using InterLinkClass.ControlObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
public partial class ResendTransaction : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    ArrayList List = new ArrayList();
    string VendortranID, Vendorcode, tranID, Fileupload = "";
    public bool OkBtnWasClicked = false;
    public bool YesBtnWasClicked = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = -1;
        Label1.Visible = false;
        try
        {
            if (IsPostBack == false)
            {
                LoadVendors();

            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    protected void btnResendTxn_Click(object sender, EventArgs e)
    {

        string tranID = txnstatus.Text.Trim();
        ViewState["VendortranID"] = tranID;
        string vcode = cboVendor.SelectedValue.ToString().Trim();
        ViewState["Vendorcode"] = vcode;
        if (string.IsNullOrEmpty(tranID))
        {
            ShowMessage("Please Enter Transaction Id of transaction to Resend", true);
        }
        else
        {
            // List.Add(reference);
            DataTable dt = datafile.SelectTransactionsToResend(tranID, vcode);
            if (dt.Rows.Count > 0)
            {
                MultiView1.ActiveViewIndex = 0;
                // View1.Visible = true;
                MultiView1.Visible = true;
                lblTxnMsg.Visible = false;
                Label1.Visible = true;
                OkBtn.Visible = false;
                YesBtn.Visible = true;
                string text = "Are you sure you want to resend  one(1) transaction ?";
                ShowWarningMessage(text, true);
                DataRow row = dt.Rows[0];
                ViewState["tranID"] = row["tranID"].ToString();
                DataGrid1.CurrentPageIndex = 0;
                DataGrid1.DataSource = dt;
                DataGrid1.DataBind();
                DataGrid1.Visible = true;



            }
            else
            {
                ShowMessage("No transaction details exist,wrong vendorcode or transaction ID", false);
            }
        }
        //Label1.Text = ".";
        //Label1.Visible = false;
        //MultiView1.ActiveViewIndex = -1;



    }
    protected void ResendtxnsBtn_Click(object sender, EventArgs e)
    {

        try
        {
            string vcode = cboVendor.SelectedValue.ToString();
            if (string.IsNullOrEmpty(FileUpload1.FileName.Trim()))
            {
                lblTxnMsg.Visible = true;
                string msg = lblTxnMsg.Text.ToString();
                msg = ShowMessage("Please Browse file of Transaction Ids to Upload", true);
                // MultiView1.ActiveViewIndex = -1;

            }
            else
            {
                FileUpload1.SaveAs(@"E:\\PePay\GenericApi\Api\application\ResendTransactions\\" + FileUpload1.FileName.Trim());
                string uploadedFile = @"E:\\PePay\GenericApi\Api\application\ResendTransactions\\ResendTransactions\\" + FileUpload1.FileName.Trim();
                ViewState["UploadedFile"] = uploadedFile;
                DataTable dtable = ReadExcelFileAsDataTable(uploadedFile);
                int count = dtable.Rows.Count;
                MultiView1.ActiveViewIndex = 0;
                YesBtn.Visible = false;
                OkBtn.Visible = true;
                lblTxnMsg.Visible = false;
                Label1.Visible = true;
                string text1 = "Are you sure you want to resend these" + " " + count + " " + "transaction(s) ?";
                ShowWarningMessage(text1, true);
                DataGrid1.CurrentPageIndex = 0;
                DataTable dt = ReturnDataGridTable(uploadedFile, vcode);
                DataGrid1.DataSource = dt;
                DataGrid1.DataBind();
                DataGrid1.Visible = true;
                //ResendTransactionFile(uploadedFile);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }


    }

    private DataTable ReturnDataGridTable(string uploadedFile, string vendorcode)
    {
        ArrayList txnRef = new ArrayList();
        DataTable trandatatable = new DataTable();
        string msg = "";
        string validationMsg = ValidateUploadedFile(uploadedFile);
        if (validationMsg.ToUpper().Equals("OK"))
        {
            DataTable dtable = ReadExcelFileAsDataTable(uploadedFile);
            ArrayList trantable = new ArrayList();
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow dr in dtable.Rows)
                {
                    string tranRef = dr["Column1"].ToString();
                    txnRef.Add(tranRef);
                    //GetTransactionTable(tranRef);
                }
                for (int counter = 0; counter < txnRef.Count; counter++)
                {
                    string transactionID = txnRef[counter].ToString().Trim();
                    DataTable datatable = datafile.SelectTransactionsToResend(transactionID, vendorcode);
                    trantable.Add(datatable);
                }
                trandatatable = ReturnTransactionDataTable(trantable);
            }
            else
            {
                lblTxnMsg.Visible = true;
                msg = ShowMessage("SORRY! NON OF THE UPLOADED TRANSACTION IDs IS FOUND AT PEGASUS", true);

            }
        }
        else
        {
            lblTxnMsg.Visible = true;
            msg = ShowMessage(validationMsg, true);
        }
        return trandatatable;
    }

    public void ResendTransactionFile(string path)
    {
        lblTxnMsg.Visible = true;
        string validationMsg = ValidateUploadedFile(path);
        string msg = lblTxnMsg.Text.ToString();
        string vendorcode = cboVendor.SelectedValue.ToString().Trim();
        ArrayList txnReferences = new ArrayList();
        ArrayList trantable = new ArrayList();
        if (validationMsg.Equals("OK"))
        {
            DataTable dtable = ReadExcelFileAsDataTable(path);
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow dr in dtable.Rows)
                {
                    string tranRef = dr["Column1"].ToString();
                    txnReferences.Add(tranRef);
                    //GetTransactionTable(tranRef);
                }
                for (int counter = 0; counter < txnReferences.Count; counter++)
                {
                    string transactionID = txnReferences[counter].ToString().Trim();
                    DataTable datatable = datafile.SelectTransactionsToResend(transactionID, vendorcode);
                    trantable.Add(datatable);
                }
                DataTable trandatatable = ReturnTransactionDataTable(trantable);
                if (trandatatable.Rows.Count > 0)
                {

                    foreach (DataRow dr in trandatatable.Rows)
                    {
                        datafile.ResendThisTransaction(dr["VendorTranId"].ToString(), vendorcode, dr["TranId"].ToString());
                    }
                    ShowMessage(trandatatable.Rows.Count + " transaction(s) resent successfully", true);
                }
                else
                {
                    ShowMessage("No details of these transactions exist, wrong vendor code or transaction IDs", false);
                }


            }
            else
            {
                lblTxnMsg.Visible = true;
                msg = ShowMessage("SORRY! NON OF THE UPLOADED TRANSACTION IDs IS FOUND AT PEGASUS", true);
                // MultiView1.ActiveViewIndex = -1;

            }
        }
        else
        {
            lblTxnMsg.Visible = true;
            msg = ShowMessage(validationMsg, true);
            //MultiView1.ActiveViewIndex = -1;
        }
    }

    //private void ResendThisTransaction(string  txnReferences, string vendor)
    //{
    //    try
    //    {

    //            string tranRef = txnReferences[counter].ToString().Trim();
    //            DataTable dt = datafile.ResendThisTransaction(tranRef, vendor,"2");
    //            int number = dt.Rows.Count;



    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    public DataTable ReadExcelFileAsDataTable(string filePath)
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

        IExcelDataReader excelReader = null;
        if (Path.GetExtension(filePath).ToUpper().Equals(".XLS"))
        {
            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
        }
        else
        {

            //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        }

        DataTable result = excelReader.AsDataSet().Tables[0];
        return result;
    }

    private string ValidateUploadedFile(string path)
    {
        string filename = Path.GetFileName(path);
        string extension = Path.GetExtension(filename);
        extension = extension.ToUpper();
        string validationMsg = "";
        if (extension.Equals(".XLS") || extension.Equals(".XLSX"))
        {
            validationMsg = "OK";
        }
        else
        {
            validationMsg = "PLEASE UPLOAD AN EXCEL FILE.";
        }

        return validationMsg;
    }
    private void LoadVendors()
    {
        dtable = datafile.GetAllVendors("0");
        cboVendor.DataSource = dtable;
        cboVendor.DataValueField = "VendorCode";
        cboVendor.DataTextField = "Vendor";
        cboVendor.DataBind();
    }
    public void Alert_Load(int count)
    {
        //    MessageBox.Show("Some text", "Some title",
        //MessageBoxButtons.OK, MessageBoxIcon.Error);
        //string message = "Are you sure you want to resend "+count+" Transaction(s)";
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append("<script type = 'text/javascript'>");
        //sb.Append("window.onload=function(){");
        //sb.Append("alert('");
        //sb.Append(message);
        //sb.Append("')};");
        //sb.Append("</script>");
        //if (confirm("Are you sure you want to delete ...?"))
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "confirm('Are you sure you want to resend " + count + " Transaction(s)')", true);
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
    }
    private string ShowMessage(string Message, bool Error)
    {
        //Label lblmsg = (Label)Master.FindControl("lblmsg");
        if (Error) { lblTxnMsg.ForeColor = System.Drawing.Color.Red; lblTxnMsg.Font.Bold = false; }
        else { lblTxnMsg.ForeColor = System.Drawing.Color.Blue; lblTxnMsg.Font.Bold = true; }
        if (Message == ".")
        {
            lblTxnMsg.Text = ".";
        }
        else
        {
            lblTxnMsg.Text = "MESSAGE: " + Message.ToUpper();
        }
        return lblTxnMsg.Text.ToString();
    }
    private string ShowWarningMessage(string Message, bool Error)
    {
        //Label lblmsg = (Label)Master.FindControl("lblmsg");
        if (Error) { Label1.ForeColor = System.Drawing.Color.Red; lblTxnMsg.Font.Bold = false; }
        else { Label1.ForeColor = System.Drawing.Color.Blue; lblTxnMsg.Font.Bold = true; }
        if (Message == ".")
        {
            Label1.Text = ".";
        }
        else
        {
            Label1.Text = "MESSAGE: " + Message.ToUpper();
        }
        return Label1.Text.ToString();
    }
    public DataTable ReturnTransactionDataTable(ArrayList listing)
    {
        DataTable dtable = new DataTable();
        int count = 0;
        dtable.Columns.Add("TranId", typeof(string));
        dtable.Columns.Add("VendorTranId", typeof(string));
        dtable.Columns.Add("CustomerRef", typeof(string));
        dtable.Columns.Add("CustomerName", typeof(string));
        dtable.Columns.Add("TranAmount", typeof(string));
        dtable.Columns.Add("VendorCode", typeof(string));
        dtable.Columns.Add("UtilityCode", typeof(string));
        dtable.Columns.Add("SentToUtility", typeof(string));
        dtable.Columns.Add("SentToVendor", typeof(string));
        dtable.Columns.Add("Status", typeof(string));
        dtable.Columns.Add("RecordDate", typeof(string));
        dtable.Columns.Add("utilitySentDate", typeof(string));




        for (count = 0; count < listing.Count; count++)
        {
            DataTable dt = (DataTable)listing[count];
            foreach (DataRow dr in dt.Rows)
            {
                dtable.Rows.Add(dr["TranId"].ToString(), dr["VendorTranId"].ToString(), dr["CustomerRef"].ToString(), dr["CustomerName"].ToString(),
                    dr["TranAmount"].ToString(), dr["VendorCode"].ToString(), dr["UtilityCode"].ToString(), dr["SentToUtility"].ToString(), dr["SentToVendor"].ToString()
                    , dr["Status"].ToString(), dr["RecordDate"].ToString(), dr["utilitySentDate"].ToString());
            }

        }


        return dtable;
    }
    protected void YesBtn_Click(object sender, EventArgs e)
    {
        Label1.Text = ".";
        Label1.Visible = false;
        lblTxnMsg.Text = ".";
        lblTxnMsg.Visible = true;

        if (string.IsNullOrEmpty(ViewState["Vendorcode"].ToString()))
        {
            Vendorcode = cboVendor.SelectedValue.ToString().Trim();
        }
        else
        {
            Vendorcode = ViewState["Vendorcode"].ToString();
        }
        if (string.IsNullOrEmpty(ViewState["VendortranID"].ToString()))
        {
            VendortranID = txnstatus.Text.ToString().Trim();
        }
        else
        {
            VendortranID = ViewState["VendortranID"].ToString();
        }
        if (string.IsNullOrEmpty(ViewState["tranID"].ToString()))
        {
            tranID = ViewState["tranID"].ToString();
        }
        else
        {
            tranID = ViewState["tranID"].ToString();
        }
        int dt = datafile.ResendThisTransaction(VendortranID, Vendorcode, tranID);
        if (dt > 0)
        {
            ShowMessage("Transaction " + " " + VendortranID + " " + " resent successfully", false);

        }
        else
        {
            ShowMessage("Resending transaction " + " " + VendortranID + " " + " failed!", true);
        }
        MultiView1.ActiveViewIndex = -1;
        // YesBtnWasClicked = true;
        //string ID = txnstatus.Text.ToString().Trim();
        //string vcode = cboVendor.SelectedValue.ToString().Trim();
        //ResendThisTransaction(ID,vcode);
    }
    protected void OkBtn_Click(object sender, EventArgs e)
    {
        Label1.Text = ".";
        Label1.Visible = false;
        lblTxnMsg.Text = ".";
        lblTxnMsg.Visible = true;

        if (string.IsNullOrEmpty(ViewState["UploadedFile"].ToString()))
        {
            FileUpload1.SaveAs(@"E:\\PePay\GenericApi\Api\application\ResendTransactions\\" + FileUpload1.FileName.Trim());
            Fileupload = @"E:\\PePay\GenericApi\Api\application\ResendTransactions\\" + FileUpload1.FileName.Trim();

        }
        else
        {
            Fileupload = ViewState["UploadedFile"].ToString();
        }
        MultiView1.ActiveViewIndex = -1;
        ResendTransactionFile(Fileupload);
    }
    protected void NoBtn_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = -1;
        lblTxnMsg.Text = ".";
        lblTxnMsg.Visible = true;
        ShowMessage("Transaction(s) not resent", true);
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
    }
    protected void DataGrid1_SelectedIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
    }

}
}