using InterLinkClass.ControlObjects;
using InterLinkClass.CoreMerchantAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class PrepaidVendorXMLLogs : System.Web.UI.Page
    {
        ProcessPay Process = new ProcessPay();
        DataLogin datafile = new DataLogin();
        Datapay datapay = new Datapay();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();
        DataTable dtable = new DataTable();
        private ReportDocument Rptdoc = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    MultiView1.ActiveViewIndex = -1;
                    //string RegionCode = "0";
                    // LoadBranches();
                    // LoadUtilities();
                    Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                    Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                    Button MenuReport = (Button)Master.FindControl("btnCalReports");
                    Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                    Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                    Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                    Button MenuOtherReport = (Button)Master.FindControl("btnOtherReports");
                    MenuTool.Font.Underline = false;
                    MenuPayment.Font.Underline = false;
                    MenuReport.Font.Underline = false;
                    MenuRecon.Font.Underline = false;
                    MenuAccount.Font.Underline = false;
                    MenuBatching.Font.Underline = false;
                    MenuOtherReport.Font.Underline = true;
                    lblTotal.Visible = false;
                    DisableBtnsOnClick();
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }


        private void Page_Unload(object sender, EventArgs e)
        {
            if (Rptdoc != null)
            {
                Rptdoc.Close();
                Rptdoc.Dispose();
                GC.Collect();
            }
        }
        private void DisableBtnsOnClick()
        {
            string strProcessScript = "this.value='Working...';this.disabled=true;";
            btnOK.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(btnOK, "").ToString());
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                LoadTransactions();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        private void LoadTransactions()
        {

            if (cboTranId.Text.Equals(""))
            {
                DataGrid1.Visible = false;
                ShowMessage("Transaction ID is required", true);
                cboTranId.Focus();
            }
            if (txtfromDate.Text.Equals(""))
            {
                DataGrid1.Visible = false;
                ShowMessage("From Date is required", true);
                txtfromDate.Focus();
            }
            else
            {
                string tranId = cboTranId.Text.ToString();
                string fromdate = txtfromDate.Text.ToString();
                string todate = txttoDate.Text.ToString();
                ArrayList TransactionIDs;
                DataTable resultTable = bll.CreateDataTable("TransactionIDs", new string[] { "PegPayId", "UtilityCode", "Method", "Request", "Response", "RequestDate", "ResponseDate", "RecordDate" });

                TransactionIDs = new ArrayList(tranId.Split(','));


                foreach (string TransactionID in TransactionIDs)
                {
                    dataTable = datapay.GetPrepaidXMLRequestAndResponseLogs(TransactionID, fromdate, todate);
                    resultTable.Merge(dataTable);
                }

                if (dataTable.Rows.Count > 0)
                {
                    MultiView1.ActiveViewIndex = 0;
                    DataGrid1.DataSource = resultTable;
                    DataGrid1.DataBind();

                }
                else
                {
                    lblTotal.Text = ".";
                    DataGrid1.Visible = false;
                    lblTotal.Visible = false;
                    MultiView1.ActiveViewIndex = -1;
                    ShowMessage("No Record found", true);
                }

            }

        }
        //protected void btnConvert_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        DataTable dt = new DataTable();
        //        string[] searchParams = GetSearchParameters();
        //        DataSet ds = bll.ExecuteDataAccess("LiveGenericPegPayApi", "GetPrepaidXMLLogs2", searchParams);
        //        dt = ds.Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            try
        //            {
        //                if (!rdPdf.Checked && !rdExcel.Checked)
        //                {
        //                    ShowMessage("CHECK ONE EXPORT OPTION", true);
        //                }
        //                else if (rdExcel.Checked)
        //                {
        //                    bll.ExportToExcelxx(dt, "", Response);
        //                }
        //                else if (rdPdf.Checked)
        //                {
        //                    bll.ExportToPdf(dt, "", Response);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //        }
        //        else
        //        {
        //            string msg = "No Records Found Matching Search Criteria";
        //            ShowMessage(msg, true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = "FAILED: " + ex.Message;
        //        ShowMessage(msg, true);
        //    }
        //}
        private string[] GetSearchParameters()
        {
            List<string> searchCriteria = new List<string>();
            string Vendor = cboTranId.Text;
            string fromDate = txtfromDate.ToString();
            string todate = txttoDate.ToString();

            searchCriteria.Add(Vendor);
            searchCriteria.Add(fromDate);
            searchCriteria.Add(todate);


            return searchCriteria.ToArray();
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //DataGrid1.PageIndex = e.NewPageIndex;
            //SearchDb();
        }

        private void CalculateTotal(DataTable Table)
        {
            double total = 0;
            foreach (DataRow dr in Table.Rows)
            {
                double amount = double.Parse(dr["Total"].ToString());
                total += amount;
            }
            string rolecode = Session["RoleCode"].ToString();
            if (rolecode.Equals("004"))
            {
                lblTotal.Visible = false;
            }
            else
            {
                lblTotal.Visible = true;
            }
            lblTotal.Text = "Total Amount of Transactions [" + total.ToString("#,##0") + "]";
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

        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
        }
        protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                string traniD = cboTranId.Text.ToString();
                //string vendorcode = cbovendorcode.Text.ToString();
                string fromdate = txtfromDate.Text.ToString();
                string todate = txttoDate.Text.ToString();

                dataTable = datapay.GetPrepaidXMLRequestAndResponseLogs(traniD, fromdate, todate);
                //DataGrid1.CurrentPageIndex = e.NewPageIndex;
                DataGrid1.DataSource = dataTable;
                DataGrid1.DataBind();
                ShowMessage(".", true);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }

        }
        //protected void btnConvert_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ConvertToFile();
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMessage(ex.Message, true);
        //    }
        //}

        //private void ConvertToFile()
        //{
        //    if (rdExcel.Checked.Equals(false) && rdPdf.Checked.Equals(false))
        //    {
        //        ShowMessage("Please Check file format to Convert to", true);
        //    }
        //    else
        //    {
        //        LoadRpt();
        //        if (rdPdf.Checked.Equals(true))
        //        {
        //            Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "TRANSACTIONS");

        //        }
        //        else
        //        {
        //            Rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true, "TRANSACTIONS");

        //        }
        //    }
        //}
        private void LoadRpt()
        {
            //string status = cboStatus.SelectedValue.ToString();
            //string districtcode = cboBranches.SelectedValue.ToString();
            //DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            //DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            //dataTable = datapay.GetDistrictTotalTrans(status, districtcode, fromdate, todate);
            //dataTable = formatTable(dataTable);
            //string appPath, physicalPath, rptName;
            //appPath = HttpContext.Current.Request.ApplicationPath;
            //physicalPath = HttpContext.Current.Request.MapPath(appPath);

            //rptName = physicalPath + "\\Bin\\Reports\\DistrictTotals.rpt";

            //Rptdoc.Load(rptName);
            //Rptdoc.SetDataSource(dataTable);
            //CrystalReportViewer1.ReportSource = Rptdoc;
        }

        private DataTable formatTable(DataTable dataTable)
        {
            DataTable formedTable;

            string Header = GetTitle();
            string Printedby = "Printed By : " + Session["FullName"].ToString();
            DataColumn myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "DateRange";
            dataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "PrintedBy";
            dataTable.Columns.Add(myDataColumn);

            // Add data to the new columns

            dataTable.Rows[0]["DateRange"] = Header;
            dataTable.Rows[0]["PrintedBy"] = Printedby;
            formedTable = dataTable;
            return formedTable;
        }

        private string GetTitle()
        {
            string ret = "";
            //string districtcode = cboBranches.SelectedValue.ToString();
            //string district = cboBranches.SelectedItem.ToString();
            //DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            //DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            //if (districtcode.Equals("0"))
            //{
            //    ret = "ALL DISTRICTS TRANSACTION(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
            //}
            //else
            //{
            //    ret = district + " DISTRICT TRANSACTION(S) BETWEEN [" + fromdate.ToString("dd/MM/yyyy") + " - " + todate.ToString("dd/MM/yyyy") + "]";
            //}
            return ret;
        }
        //protected void cboBranches_DataBound(object sender, EventArgs e)
        //{
        //    cboBranches.Items.Insert(0, new ListItem("All Districts", "0"));
        //}

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                string tranId = cboTranId.Text.ToString();
                string fromdate = txtfromDate.Text.ToString();
                string todate = txttoDate.Text.ToString();
                ArrayList TransactionIDs;
                DataTable resultTable = bll.CreateDataTable("TransactionIDs", new string[] { "PegPayId", "UtilityCode", "Method", "Request", "Response", "RequestDate", "ResponseDate", "RecordDate" });

                TransactionIDs = new ArrayList(tranId.Split(','));


                foreach (string TransactionID in TransactionIDs)
                {
                    dataTable = datapay.GetPrepaidXMLRequestAndResponseLogs(TransactionID, fromdate, todate);
                    resultTable.Merge(dataTable);
                }

                if (dataTable.Rows.Count > 0)
                {
                    try
                    {
                        if (!RadioButton1.Checked && !RadioButton1.Checked)
                        {
                            ShowMessage("CHECK ONE EXPORT OPTION", true);
                        }
                        else if (RadioButton1.Checked)
                        {
                            bll.ExportToExcelxx(resultTable, "", Response);
                            //bll.ExportToExcel(dt, "", Response);
                        }
                        else if (RadioButton1.Checked)
                        {
                            bll.ExportToPdf(resultTable, "", Response);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                else
                {
                    lblTotal.Text = ".";
                    DataGrid1.Visible = false;
                    lblTotal.Visible = false;
                    MultiView1.ActiveViewIndex = -1;
                    ShowMessage("No Record found", true);
                }

            }
            catch (Exception ex)
            {
                string msg = "FAILED: " + ex.Message;
                ShowMessage(msg, true);
            }

        }
    }
}