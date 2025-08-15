using InterLinkClass.ControlObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class PrepaidAutoReconciliation : System.Web.UI.Page
    {
        ProcessPay Process = new ProcessPay();
        DataLogin datafile = new DataLogin();
        Datapay datapay = new Datapay();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();
        DataTable dtable = new DataTable();
        string telecomreconfile = "";
        private ReportDocument Rptdoc = new ReportDocument();

        DataFileProcess dfile = new DataFileProcess();
        private ArrayList fileContents;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    LoadData();
                    ToggleVendor();
                    MultiView1.ActiveViewIndex = 0;
                    Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                    Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                    Button MenuReport = (Button)Master.FindControl("btnCalReports");
                    Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                    Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                    Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                    MenuTool.Font.Underline = false;
                    MenuPayment.Font.Underline = false;
                    MenuReport.Font.Underline = false;
                    MenuRecon.Font.Underline = true;
                    MenuAccount.Font.Underline = false;
                    MenuBatching.Font.Underline = false;
                    DisableBtnsOnClick();
                }
                //string RoleId = Session["RoleCode"].ToString();
                //if (isRoleAuthorisedToVisitPage(RoleId))
                //{
                //    if (IsPostBack == false)
                //    {
                //        LoadData();
                //        ToggleVendor();
                //        MultiView1.ActiveViewIndex = 0;
                //        DisableBtnsOnClick();
                //    }
                //}
                //else
                //{
                //    Response.Redirect("UnauthorisedAccess.aspx");
                //}
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }
        private bool isRoleAuthorisedToVisitPage(string RoleId)
        {
            bool authorised = false;
            DataTable dt = (DataTable)Session["RolePageMatrix"];
            ArrayList roleIds = new ArrayList();
            string currenePage = GetCurrentPage();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string page = dr["OtherPages"].ToString();
                    if (page.Trim().ToUpper().Equals(currenePage.Trim().ToUpper()))
                    {
                        roleIds.Add(dr["RoleId"].ToString().Trim());
                    }
                }
                if (roleIds.Contains(RoleId.Trim()))
                {
                    authorised = true;
                }
            }
            return authorised;
        }
        private string GetCurrentPage()
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }
        private void LoadData()
        {
            //bll.LoadTelecoms(cboTelecom);
            bll.LoadReconUtilities(cboType);
            bll.LoadHours(ddFromHour);
            bll.LoadHours(ddToHour);
        }

        private void ToggleVendor()
        {
            string districtcode = Session["DistrictCode"].ToString();
            string role = Session["RoleCode"].ToString();
            if (role.Equals("005"))
            {
                cboTelecom.Enabled = false;
                cboTelecom.SelectedIndex = cboTelecom.Items.IndexOf(cboTelecom.Items.FindByValue(districtcode));
            }
            else
            {
                cboTelecom.Enabled = true;
            }
        }

        private void ShowMessage(string Message, bool Error)
        {
            Label lblmsg = (Label)Master.FindControl("lblmsg");
            if (Error) { lblmsg.ForeColor = System.Drawing.Color.Red; lblmsg.Font.Bold = true; }
            else { lblmsg.ForeColor = System.Drawing.Color.Green; lblmsg.Font.Bold = true; }
            if (Message == ".")
            {
                lblmsg.Text = ".";
            }
            else
            {
                lblmsg.Text = "MESSAGE: " + Message.ToUpper();
            }
        }
        private void DisableBtnsOnClick()
        {
            string strProcessScript = "this.value='Working...';this.disabled=true;";
            //btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
            Button1.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button1, "").ToString());
            Button2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button2, "").ToString());
        }
        protected void cboTelecom_DataBound(object sender, EventArgs e)
        {
            cboTelecom.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string vendorcode = cboTelecom.SelectedValue.ToString();
                string utility = cboType.SelectedValue.ToString();
                if (vendorcode.Equals("0"))
                {
                    ShowMessage("Please Select Vendor to Reconcile".ToUpper(), true);
                }
                else if (FileUpload1.FileName.Trim().Equals(""))
                {
                    ShowMessage("Please Browse for File to Reconcile".ToUpper(), true);
                }
                else
                {
                    string account = ddOva.SelectedValue.ToString();
                    ReadFileToRecon(utility, vendorcode, account);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }
        private int CreateReconCode()
        {
            int output = 0;
            string createdby = Session["Username"].ToString();
            output = datapay.SaveReconBatch(0, 0, 0, 0, createdby);
            return output;
        }
        private void ReadFileToRecon(string utility, string vendorcode, string account)
        {
            string ova = account;//ddOva.SelectedValue;
            string fromDate = DateTime.Parse(txtFromDate.Text).ToString();
            string toDate = DateTime.Parse(txtToDate.Text).AddDays(1).AddSeconds(-1).ToString();
            bool ReconFromArch = chkboxArchieves.Checked;
            string filename = Path.GetFileName(FileUpload1.FileName);
            string extension = Path.GetExtension(filename);

            if (String.IsNullOrEmpty(ova))
            {
                ShowMessage("Please Select an Account".ToUpper(), true);
                return;
            }

            if (String.IsNullOrEmpty(fromDate))
            {
                ShowMessage("Please Select a From Date".ToUpper(), true);
                return;
            }

            if (String.IsNullOrEmpty(toDate))
            {
                ShowMessage("Please Select a To Date".ToUpper(), true);
                return;
            }

            if (!(extension.ToUpper().Equals(".CSV") || extension.ToUpper().Equals(".TXT")))
            {
                ShowMessage("Please upload a CSV File, " + extension + " file not supported", true);
                return;
            }

            fromDate = GetDate(fromDate, ddFromHour.SelectedValue, false);
            toDate = GetDate(toDate, ddToHour.SelectedValue, true);


            string filePath = bll.ReconPrepaidFilePath_1(utility, vendorcode, filename);
            FileUpload1.SaveAs(filePath);
            string resp = bll.ValidateUploadedFile_1(filePath);
            string name = Session["FullName"].ToString();
            if (resp.ToUpper().Trim().Equals("SUCCESS"))
            {

                string sessionEmail = Session["UserEmail"].ToString();
                name = Session["FullName"].ToString();
                string user = Session["Username"].ToString();

                if (ReconFromArch)
                {
                    //Incase they ever archive this db, here is where you search the archives. Now Isnt life a bitch!!! i ddnt implement it. Marc
                    //datafile.SaveUploadedPrepaidVendorReconFileDetail_1(vendorcode, ova, telecomreconfile,fromDate, toDate, user, sessionEmail);
                    //ShowMessage("Hello\t" + name + "\t\tPrepaid File has been Uploaded Successfully Reconciliation has will start and the report will be sent to your Email Shortly", false);
                    //return;
                }

                //("ReconFilePathDetails_Insert_1", utility, account, filePath, userName, sessionEmail, ReconcileFlag, vendorcode, fromdate, todate
                datafile.SaveUploadedPrepaidVendorReconFileDetail_1(utility, ova, filePath, user, sessionEmail, 0, vendorcode, fromDate, toDate);

                ShowMessage("Hello\t" + name + "\t\tPrepaid Transactions File has been Uploaded Successfully Reconciliation has started and the report will be sent to your Email Shortly thank you.", false);

            }

            else
            {
                ShowMessage("Hello\t" + name + "\t\t" + resp, true);
            }
        }

        private string FileFormatIsOk(string telecomreconfile, string account)
        {
            string resp = "ERROR";
            string error = "THE UPLOADED FILE IS NOT IN THE CORRECT FORMAT. ENSURE CORRECT FORMAT. ALSO ENSURE FILE HAS NO BLANK ROWS, ESPECIALLY AT THE BEGINNING OR THE END OF THE FILE.";
            try
            {
                //String[] languages = new String[] { "english", "french", "german" };
                string firstLine;
                using (StreamReader reader = new StreamReader(telecomreconfile))
                {
                    firstLine = reader.ReadLine() ?? "";
                    if (firstLine.Contains(","))
                    {
                        string[] values = firstLine.Split(',');
                        if (values.Length == 8)
                        {
                            string fromAcc = values[3].ToString();
                            string toAcc = values[4].ToString();
                            if (fromAcc.Trim().ToUpper().Equals(account.Trim().ToUpper()) || toAcc.Trim().ToUpper().Equals(account.Trim().ToUpper()))
                            {
                                resp = "SUCCESS";
                            }
                            else
                            {
                                resp = "THE UPLOADED FILE DOESN'T BELONG TO THE SELECTED OVA. THE FILE BELONGS TO ACCOUNT: " + account + ". PLEASE SELECT THE CORRECT OVA";
                            }
                        }
                        else
                        {
                            resp = error;
                        }
                    }
                    else
                    {
                        resp = error;
                    }
                }
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            return resp;
        }

        private string GetDate(string dateString, string hour, bool end)
        {
            DateTime date = DateTime.Parse(dateString);
            int dateHour = Int32.Parse(hour);

            int minutes = end ? 59 : 0;
            int seconds = end ? 59 : 0;

            DateTime returnDate = new DateTime(date.Year, date.Month, date.Day, dateHour, minutes, seconds);
            return returnDate.ToString();
        }

        private string GetPayDate(string pay_date)
        {
            string res = "";
            try
            {
                string[] str_date = pay_date.Split('/');
                if (str_date.Length.Equals(3))
                {
                    string dd = str_date[0].ToString();
                    string mm = str_date[1].ToString();
                    string yy = str_date[2].ToString();
                    int dd_len = dd.Length;
                    int mm_len = mm.Length;
                    int yy_len = yy.Length;
                    if (dd_len.Equals(1))
                    {
                        dd = "0" + dd;
                        dd_len = dd.Length;
                    }
                    if (mm_len.Equals(1))
                    {
                        mm = "0" + mm;
                        mm_len = mm.Length;
                    }

                    if (dd_len.Equals(2) && mm_len.Equals(2) && yy_len.Equals(4))
                    {
                        res = dd + "/" + mm + "/" + yy;
                    }
                    else
                    {
                        throw new Exception("Wrong Payment Date");
                    }
                }
                else
                {
                    throw new Exception("Wrong Payment Date");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Wrong Payment Date");
            }
            return res;
        }
        private void DisplayFailed(ArrayList failedTransactions)
        {
            try
            {
                DataTable dt = bll.GetFailedReconTable();
                Recontran recontran = new Recontran();
                for (int i = 0; i < failedTransactions.Count; i++)
                {
                    recontran = (Recontran)failedTransactions[i];
                    DataRow dr = dt.NewRow();
                    dr["No."] = i + 1;
                    dr["VendorRef"] = recontran.VendorRef;
                    dr["PayDate"] = recontran.PayDate;
                    dr["TransactionAmount"] = recontran.TransAmount.ToString("#,##0");
                    dr["Reason"] = recontran.Reason;
                    dt.Rows.Add(dr);

                    dt.AcceptChanges();
                }
                LoadFailedGrid(dt);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }
        private void LoadFailedGrid(DataTable dt)
        {
            MultiView1.ActiveViewIndex = 1;
            DataGrid1.DataSource = dt;
            DataGrid1.DataBind();
            //string filePath = CallFilling(dt);
            //foreach (DataRow dr in dac.GetEmailSubscribers().Rows)
            //{
            //    string Email = dr["Email"].ToString();
            //    string FName = dr["FullName"].ToString();
            //    string Msg = "Hello " + " " + FName + "\nPlease find attached, \n Failed Reconciliations for Vendor : " + cboBank.SelectedItem.ToString();
            //    string Subject = "Failed Reconciliations for Vendor: " + cboBank.SelectedItem.ToString();
            //    string Sender = Session["UserName"].ToString();
            //    bll.SendMail(Email, Msg, Sender, Subject, filePath);
            //}     
        }

        private void CancelRecnBatch(int Reconcode)
        {
            datapay.CancelReconBatch(Reconcode);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            ShowMessage(".", true);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                string fileFormat = GetFileformat();
                if (fileFormat.Equals("NONE"))
                {
                    ShowMessage("Please Select File Format", true);
                }
                else
                {
                    ShowMessage(".", true);
                    ConvertToPdf(fileFormat);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
            finally
            {
                Rptdoc.Dispose();
            }
        }

        private string GetFileformat()
        {
            string res = "NONE";
            if (rdPdf.Checked == false && rdExcel.Checked == false)
            {
                res = "NONE";
            }
            else
            {
                if (rdPdf.Checked == true)
                {
                    res = "1";
                }
                else
                {
                    res = "2";
                }
            }
            return res;
        }

        private void ConvertToPdf(string fileformat)
        {
            dataTable = GetGridRptTable();
            LoadReport(dataTable, fileformat);
        }
        private DataTable GetGridRptTable()
        {
            DataTable dtble = GetFailedRptDataTable();
            string vendor = cboTelecom.SelectedItem.ToString();
            foreach (DataGridItem Items in DataGrid1.Items)
            {
                DataRow dr = dtble.NewRow();
                string No = Items.Cells[0].Text;
                string VendorRef = Items.Cells[1].Text;
                string PayDate = Items.Cells[2].Text;
                string Amount = Items.Cells[3].Text;
                string Reason = Items.Cells[4].Text;
                string bank = vendor;
                string PrintedBy = Session["FullName"].ToString();
                ///////
                dr["No."] = No;
                dr["VendorRef"] = VendorRef;
                dr["PayDate"] = PayDate;
                dr["Amount"] = Amount;
                dr["Reason"] = Reason;
                dr["Bank"] = bank;
                dr["PrintedBy"] = PrintedBy;
                dtble.Rows.Add(dr);
                dtble.AcceptChanges();
            }
            return dtble;
        }

        private DataTable GetFailedRptDataTable()
        {
            DataTable dt = new DataTable("Table2");
            dt.Columns.Add("No.");
            dt.Columns.Add("VendorRef");
            dt.Columns.Add("PayDate");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Reason");
            dt.Columns.Add("Bank");
            dt.Columns.Add("PrintedBy");
            return dt;
        }

        private void LoadReport(DataTable dataTable, string fileformat)
        {
            string appPath, physicalPath, rptName;
            appPath = HttpContext.Current.Request.ApplicationPath;
            physicalPath = HttpContext.Current.Request.MapPath(appPath);
            rptName = physicalPath + "\\Bin\\reports\\FailedRecon.rpt";
            Rptdoc.Load(rptName);
            Rptdoc.SetDataSource(dataTable);
            CrystalReportViewer1.ReportSource = Rptdoc;
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            if (fileformat.Equals("1"))
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "FAILED RECONCILIATIONS");
            }
            else
            {
                Rptdoc.ExportToHttpResponse(ExportFormatType.Excel, Response, true, "FAILED RECONCILIATIONS");
            }
            ShowMessage(".", true);
        }
        protected void cboTelecom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vendorcode = cboTelecom.SelectedValue.ToString();
            string type = cboType.SelectedValue.ToString();
            bll.LoadAccountsToReconcile(vendorcode, type, ddOva);
        }

        protected void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string vendorcode = cboTelecom.SelectedValue.ToString();
            string type = cboType.SelectedValue.ToString();
            bll.LoadTelecomsPrepad(type, cboTelecom);
        }
    }
}