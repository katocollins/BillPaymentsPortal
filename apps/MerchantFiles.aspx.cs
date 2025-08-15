using InterLinkClass.ControlObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class ReconcilePrepaidTransactions : System.Web.UI.Page
    {
        BusinessLogin bll = new BusinessLogin();

        string sessionEmail;
        string name;
        string user;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                sessionEmail = Session["UserEmail"].ToString();
                name = Session["FullName"].ToString();
                user = Session["Username"].ToString();

                if (IsPostBack == false)
                {

                }
            }
            catch (Exception ex)
            {

                ShowMessage(ex.Message, true);
            }

        }

        protected void ddFileType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Label lblmsg = (Label)Master.FindControl("lblmsg");
                lblmsg.Text = "";
                txtYear.Enabled = ddFileType.SelectedValue.Equals("SETTLEMENT") ? true : false;
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.FileName.Trim().Equals(""))
                {
                    ShowMessage("Please upload a file", true);
                }
                else
                {
                    ReadFile(ddFileType.SelectedValue);
                }
            }
            catch (Exception ex)
            {

                ShowMessage(ex.Message, true);
            }
        }

        private void ReadFile(string fileType)
        {
            string filename = Path.GetFileName(FileUpload1.FileName);
            string extension = Path.GetExtension(filename);
            Dictionary<string, int> fileLengths = GetLengthList();
            string filePath = bll.MerchantFilePath(fileType, filename, "");
            FileUpload1.SaveAs(filePath);

            if (!(extension.ToUpper().Equals(".XLSX") || extension.ToUpper().Equals(".CSV")))
            {
                ShowMessage("Please upload a .xlsx of .csv File, " + extension + " file not supported", true);
                return;
            }
            if ((extension.ToUpper().Equals(".XLSX")) && !fileType.Equals("DEBIT"))
            {
                ExcelPackage ep = new ExcelPackage(new FileInfo(filePath));
                ExcelWorksheet ws = ep.Workbook.Worksheets[1];

                if (ws.Dimension.Columns != fileLengths[fileType])
                {
                    ShowMessage("The file should contain " + fileLengths[fileType] + " columns", true);
                    return;
                }

            }
            string[] SearchParams = { filePath, user, sessionEmail, fileType };
            bll.ExecuteDataQueryMerchant("LiveMerchantCoreDB", "MerchantFiles_Insert", SearchParams);
            //bll.ExecuteDataQuery("LiveMerchantCoreDB", "MerchantFiles_Insert", filePath, user, sessionEmail, fileType);

            ShowMessage("The file has been successfully uploaded", false);
        }


        private Dictionary<string, int> GetLengthList()
        {
            Dictionary<string, int> fileLengths = new Dictionary<string, int>();
            fileLengths.Add("CHARGEBACK", 13);
            fileLengths.Add("SETTLEMENT", 6);
            fileLengths.Add("DEBIT", 13);
            return fileLengths;
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
    }
}