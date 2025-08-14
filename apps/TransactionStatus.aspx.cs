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
using InterLinkClass.VASReference;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using Excel;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class BankErrors : System.Web.UI.Page
{
    private ReportDocument Rptdoc = new ReportDocument();
    string filename = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = -1;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {

            if (string.IsNullOrEmpty(FileUpload1.FileName.Trim()))
            {
                lblTxnMsg.Visible = true;
                string msg = lblTxnMsg.Text.ToString();
                msg = ShowMessage("Please Browse Customer file to Upload", true);
                MultiView1.ActiveViewIndex = -1;

            }
            else
            {
                //string reference = txnstatus.Text.ToString().Trim();
                FileUpload1.SaveAs(@"E:\\PePay\\GenericApi\\Api\\application\\URAFiles\\" + FileUpload1.FileName.Trim());
                string uploadedFile = @"E:\\PePay\\GenericApi\\Api\\application\\URAFiles\\" + FileUpload1.FileName.Trim();
               // filename = FileUpload1.FileName.
                GetFileTranStatus(uploadedFile);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    public DataTable GenerateTransactionStatusReport()
    {
        DataTable dtable = new DataTable();
        dtable.Columns.Add("BankVendorTranRef", typeof(string));
        dtable.Columns.Add("MtnId", typeof(string));
        dtable.Columns.Add("CustomerReference", typeof(string));
        dtable.Columns.Add("CustomerName", typeof(string));
        dtable.Columns.Add("Tin", typeof(string));
        dtable.Columns.Add("TranAmount", typeof(string));
        dtable.Columns.Add("SentToCoreStatus", typeof(string));
        dtable.Columns.Add("SentToUtilityStatus", typeof(string));
        dtable.Columns.Add("RecordDate", typeof(string));
        dtable.Columns.Add("utilitySentDate", typeof(string));
        DataRow datarow = null;
        
        foreach (DataGridItem dr in DataGrid1.Items)
        {
            //dr.Cells[0].ToString
            datarow = dtable.NewRow();
            datarow["BankVendorTranRef"] = dr.Cells[0].Text.ToString();
            datarow["MtnId"] = dr.Cells[1].Text.ToString();
            datarow["CustomerReference"] = dr.Cells[2].Text.ToString();
            datarow["CustomerName"] = dr.Cells[3].Text.ToString();
            datarow["Tin"] = dr.Cells[4].Text.ToString();
            datarow["TranAmount"] = dr.Cells[5].Text.ToString();
            datarow["SentToCoreStatus"] = dr.Cells[6].Text.ToString();
            datarow["SentToUtilityStatus"] = dr.Cells[7].Text.ToString();
            datarow["utilitySentDate"] = dr.Cells[8].Text.ToString();

            dtable.Rows.Add(datarow);
            dtable.AcceptChanges();
        
        }
        //dtable.Rows.Add(resp.ResponseField10.ToString().Trim(), resp.ResponseField3.ToString().Trim(),
        //                resp.ResponseField1.ToString().Trim(), resp.ResponseField2.ToString().Trim(),
        //                resp.ResponseField13.ToString().Trim(), resp.ResponseField12.ToString().Trim(),
        //                resp.ResponseField5.ToString().Trim(), resp.ResponseField4.ToString().Trim(),
        //                resp.ResponseField9.ToString().Trim(), resp.ResponseField11.ToString().Trim());
      
        //string validationMsg = ValidateUploadedFile(path);
        //string msg = lblTxnMsg.Text.ToString();
        //ArrayList txnReferences = new ArrayList();
        //DataTable txnTable = new DataTable();
        //if (validationMsg.Equals("OK"))
        //{
        //    DataTable dtable = ReadExcelFileAsDataTable(path);
        //    if (dtable.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dtable.Rows)
        //        {
        //            string tranRef = dr["Column1"].ToString();
        //            txnReferences.Add(tranRef);
        //            //GetTransactionTable(tranRef);
        //        }
        //       txnTable = GetTransactionTable(txnReferences);

        //    }
        //    else
        //    {
        //        lblTxnMsg.Visible = true;
        //        msg = ShowMessage("SORRY! NON OF THE UPLOADED REFERENCES IS FOUND AT BANK", true);
        //        MultiView1.ActiveViewIndex = -1;

        //    }
        //}
        //else
        //{
        //    lblTxnMsg.Visible = true;
        //    msg = ShowMessage(validationMsg, true);
        //    MultiView1.ActiveViewIndex = -1;
        //}
        return dtable;
    }
    public void GetFileTranStatus(string path)
    {
        string validationMsg = ValidateUploadedFile(path);
        string msg = lblTxnMsg.Text.ToString();
        ArrayList txnReferences = new ArrayList();
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
                GetTransactionTable(txnReferences);

            }
            else
            {
                lblTxnMsg.Visible = true;
                msg = ShowMessage("SORRY! NON OF THE UPLOADED REFERENCES IS FOUND AT BANK", true);
                MultiView1.ActiveViewIndex = -1;

            }
        }
        else
        {
            lblTxnMsg.Visible = true;
            msg = ShowMessage(validationMsg, true);
            MultiView1.ActiveViewIndex = -1;
        }
    }

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
        string filename = Path.GetFileName(FileUpload1.FileName);
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
   
    protected void btnTxnStatus_Click(object sender, EventArgs e) 
    {
        ArrayList aList = new ArrayList();
        string Ref = txnstatus.Text.ToString().Trim();
        try
        {
            if (string.IsNullOrEmpty(Ref))
            {
                lblTxnMsg.Visible = true;
                string msg = lblTxnMsg.Text.ToString();
                msg = ShowMessage("Transaction Reference is required.", true);
                MultiView1.ActiveViewIndex = -1;
            }
            else
            {
                aList.Add(Ref);
                GetTransactionTable(aList);

            }
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    
    }
    private DataTable GetTransactionTable(ArrayList transref)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidation;
        System.Net.ServicePointManager.Expect100Continue = false;
        PegPay pegpay = new PegPay();
        Response resp = new Response();
        QueryRequest req = new QueryRequest();
        req.QueryField5 = "URA";//vendorcode
        req.QueryField6 = "C1bn@t5#739";//password
        DataTable dtable = new DataTable();
        ArrayList list = new ArrayList();
        int counter = 0;
        try
        {

            for (counter = 0; counter < transref.Count; counter++)
            {
                req.QueryField10 = transref[counter].ToString();
                resp = pegpay.GetTransactionDetails(req);
                if (resp.ResponseField6.ToString().Equals("0"))
                {
                    list.Add(resp);

                }
                else
                {
                    //Basically do nothing just skip the reference.

                }

            }
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataSource = ReturnTransactionStatusGrid(list);
            dtable = ReturnTransactionStatusGrid(list);
            DataGrid1.DataBind();
            DataGrid1.Visible = true;
            lblTxnMsg.Visible = false;
            MultiView1.ActiveViewIndex = 0;


        }
        catch (Exception ex)
        {

            throw ex;
        }
        return dtable;
    }

    public DataTable ReturnTransactionStatusGrid(ArrayList listing)
    {
        DataTable dtable = new DataTable();
        Response resp = new Response();
        int count = 0;
        dtable.Columns.Add("BankVendorTranRef", typeof(string));
        dtable.Columns.Add("MtnId", typeof(string));
        dtable.Columns.Add("CustomerReference", typeof(string));
        dtable.Columns.Add("CustomerName", typeof(string));
        dtable.Columns.Add("Tin", typeof(string));
        dtable.Columns.Add("TranAmount", typeof(string));
        dtable.Columns.Add("SentToCoreStatus", typeof(string));
        dtable.Columns.Add("SentToUtilityStatus", typeof(string));
        dtable.Columns.Add("RecordDate", typeof(string));
        dtable.Columns.Add("utilitySentDate", typeof(string));
        for (count = 0; count < listing.Count; count++)
        {
            resp = (Response)listing[count];
            dtable.Rows.Add(resp.ResponseField10.ToString().Trim(), resp.ResponseField3.ToString().Trim(),
                      resp.ResponseField1.ToString().Trim(), resp.ResponseField2.ToString().Trim(),
                      resp.ResponseField13.ToString().Trim(), resp.ResponseField12.ToString().Trim(),
                      resp.ResponseField5.ToString().Trim(), resp.ResponseField4.ToString().Trim(),
                      resp.ResponseField9.ToString().Trim(), resp.ResponseField11.ToString().Trim());
        }


        return dtable;
    }
    private void GetTransactionStatus()
    {
        try
        {
            string transRef = txnstatus.Text.ToString().Trim(); 
            if (string.IsNullOrEmpty(transRef))
            {
                ShowMessage("Transaction Reference is required", true);
            }
            else
            {
                QueryRequest req = new QueryRequest();
                req.QueryField10 = transRef;
                req.QueryField5 = "URA";//vendorcode
                req.QueryField6 = "C1bn@t5#739";//password

                System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidation;
                System.Net.ServicePointManager.Expect100Continue = false;
                PegPay pegpay = new PegPay();
                Response resp = new Response();
                resp = pegpay.GetTransactionDetails(req);
                if (resp.ResponseField6.ToString().Equals("0"))
                {
                    DataGrid1.CurrentPageIndex = 0;
                    DataGrid1.DataSource = getTransactionStatusGrid(resp);
                    DataGrid1.DataBind();
                    DataGrid1.Visible = true;
                    lblTxnMsg.Visible = false;
                    MultiView1.ActiveViewIndex = 0;
                    
                   
                }
                else
                {
                    DataGrid1.Visible = false;
                    lblTxnMsg.Visible = true;
                    string msg = lblTxnMsg.Text.ToString();
                    msg = ShowMessage("SORRY! NO DETAILS OF THIS TRANSACTION REFERENCE EXIST AT THE BANK.", true);
                    MultiView1.ActiveViewIndex = -1; 

                    
                }
                

            }

        }
        catch (Exception ex)
        {
            
            throw ex;
        }

        
       
    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
       MultiView1.ActiveViewIndex = 0;
        try
        {
            GenerateReport();
        }
        catch (Exception ex)
        {
            lblTxnMsg.Visible = true;
            string msg = lblTxnMsg.Text.ToString();
            msg = ShowMessage(ex.Message, true);
            MultiView1.ActiveViewIndex = 0;
            //ShowMessage(ex.Message, true);
        }

    }

    private void GenerateReport()
    {
        if (rdExcel.Checked.Equals(false) && rdPdf.Checked.Equals(false))
        {
            lblTxnMsg.Visible = true;
            string msg = lblTxnMsg.Text.ToString();
            msg = ShowMessage("Please Check file format to Convert to", true);
            MultiView1.ActiveViewIndex = 0;
        }
        else
        {
            LoadRpt();
            if (rdPdf.Checked.Equals(true))
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "TRANSACTIONS DETAILS");

            }
            else
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "TRANSACTIONS DETAILS");

            }
        }
    }

    private void LoadRpt()
    {
        string value = "";
       // DataGrid1 dataGrid = new DataGrid1(); 
        //FileUpload1.SaveAs(@"E:\\PePay\\GenericApi\\Api\\application\\URAFiles\\" + FileUpload1.FileName.Trim());
        //string uploadedFile = @"E:\\PePay\\GenericApi\\Api\\application\\URAFiles\\" + FileUpload1.FileName.Trim();
        ////FileUpload1.FileName
        ////string filename = Path.GetFileName(uploadedFile);
        ////string filepath = uploadedFile + filename;
        ////GetFileTranStatus(uploadedFile);
        DataTable dt = GenerateTransactionStatusReport();
        //ArrayList arraylist = new ArrayList();
        //string text = txnstatus.Text.ToString().Trim();
        //if (!string.IsNullOrEmpty(text))
        //{
        //    arraylist.Add(text);
        //    DataTable dt1= GetTransactionTable(arraylist);
        //}
        //foreach (DataGridViewRow row in DataGrid1.Items)
        //{
        //    foreach (DataGridViewCell cell in row.Cells)
        //    {
        //         value = cell.Value.ToString();

        //    }
        //    DataTable dtable = (DataTable)value;
        //}
        string appPath, physicalPath, rptName;
        appPath = HttpContext.Current.Request.ApplicationPath;
        physicalPath = HttpContext.Current.Request.MapPath(appPath);

        rptName = physicalPath + "\\Bin\\Reports\\TransactionStatusReport.rpt";
        Rptdoc.Load(rptName);
        Rptdoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = Rptdoc;
    }

    public DataTable getTransactionStatusGrid(Response resp)
    {
        DataTable dtable= new DataTable();
        dtable.Columns.Add("BankVendorTranRef", typeof(string));
        dtable.Columns.Add("MtnId", typeof(string));
        dtable.Columns.Add("CustomerReference", typeof(string));
        dtable.Columns.Add("CustomerName", typeof(string));
        dtable.Columns.Add("Tin", typeof(string));
        dtable.Columns.Add("TranAmount", typeof(string));
        dtable.Columns.Add("SentToCoreStatus", typeof(string));
        dtable.Columns.Add("SentToUtilityStatus", typeof(string));
        dtable.Columns.Add("RecordDate", typeof(string));
        dtable.Columns.Add("utilitySentDate", typeof(string));
        dtable.Rows.Add(resp.ResponseField10.ToString().Trim(),resp.ResponseField3.ToString().Trim(),
                        resp.ResponseField1.ToString().Trim(),resp.ResponseField2.ToString().Trim(),
                        resp.ResponseField13.ToString().Trim(), resp.ResponseField12.ToString().Trim(),
                        resp.ResponseField5.ToString().Trim(),resp.ResponseField4.ToString().Trim(),
                        resp.ResponseField9.ToString().Trim(), resp.ResponseField11.ToString().Trim());
      

        return dtable;
    }
    private string ShowMessage(string Message, bool Error)
    {
        //Label lblmsg = (Label)Master.FindControl("lblmsg");
        if (Error) { lblTxnMsg.ForeColor = System.Drawing.Color.Red; lblTxnMsg.Font.Bold = false; }
        else { lblTxnMsg.ForeColor = System.Drawing.Color.Black; lblTxnMsg.Font.Bold = true; }
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
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {

        try
        {
            string transRef = txnstatus.Text.ToString().Trim();
            if (string.IsNullOrEmpty(transRef))
            {
                ShowMessage("Transaction Reference is required", true);
            }
            else
            {
                QueryRequest req = new QueryRequest();
                req.QueryField10 = txnstatus.Text.Trim();
                req.QueryField5 = "URA";//vendorcode
                req.QueryField6 = "C1bn@t5#739";//password

                System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidation;
                System.Net.ServicePointManager.Expect100Continue = false;
                PegPay pegpay = new PegPay();
                Response resp = new Response();

                resp = pegpay.GetTransactionDetails(req);

            }
          
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    protected void DataGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private static bool RemoteCertificateValidation(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
 
}
