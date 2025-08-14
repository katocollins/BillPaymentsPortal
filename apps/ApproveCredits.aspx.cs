using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
//using System.Text;
//using System.Data;
using System.Net;
//using NumberText;
using System.Net.Mail;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
public partial class ApproveCredits : System.Web.UI.Page
{
    BusinessLogin bll = new BusinessLogin();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    private ReportDocument Rptdoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void chkTransactions_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            chkAllData.Visible = true;
          
            if (chkAllData.Checked == true)
            {
              
            }
            else
            {
                
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void SelectAllItems()
    {
        foreach (DataGridItem Items in DataGrid1.Items)
        {
            CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
            if (chk.Checked)
            {
                chk.Checked = false;
            }
            else
            {
                chk.Checked = true;
            }
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
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string str = GetRecordsToApprove().TrimEnd(',');
            if (str.Equals(""))
            {
                ShowMessage("Please Select Credits to Approve", true);
            }
            else
            {
                LoadCredits();
                ProcessApprovals(str);
                LoadCredits();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void ProcessApprovals(string str)
    {
        try
        {
            string userId = Session["Username"] as string;
            string fullname = Session["Fullname"] as string;
            string userBranch = Session["UserBranch"] as string;

            int suc = 0;
            int failed = 0;
            int count = 0;
            string[] arr = str.Split(',');
            int i = 0;
            string User = Session["UserName"].ToString();
            for (i = 0; i < arr.Length; i++)
            {
                int RecordId = int.Parse(arr[i].ToString());

                string ss = datafile.ApproveAccountCredit(RecordId);
                if (ss.Equals("SUCCESS"))
                {
                    count++;
                    suc++;

                    //update approved credits
                    datafile.UpdateApprovedCredit(RecordId, User);     
                    CustomerReceiptCreditDetails cust = datafile.GetCustomerReceiptDetails(RecordId);
                    bll.InsertIntoAuditLog(RecordId.ToString(), "UPDATE", "Approve Credit", userBranch, userId, bll.GetCurrentPageName(),
fullname + " successfully approved the credit [" + RecordId + "]  with the vendorCode [" + cust.CustomerCode + "] from the IP:" + bll.GetIPAddress() + "at " + DateTime.Now.ToString());
                   
                    SendReceiptToUsernameEmail(cust);

                }

                else
                {
                    failed++;
                }   
            }
            string msg = suc + " Credits Have Been Approved and " + failed + "Failed";
            //datafile.LogActivity(Session["UserName"].ToString(), "Approved Account Credits Details");
            ShowMessage(msg, false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void SendReceiptToUsernameEmail(CustomerReceiptCreditDetails cust)
    {

        // string custName = txtCustName.Text;
        //string CustAccount = txtCustAccount.Text;
        string AccountNumber = "";
        string AccountBalance = "";
        double AccountBalanceNo = 0;
        double CreditAmount = 0;
        DateTime todaydate = DateTime.Today.Date;
        //string bbd = todaydate.ToShortDateString();  //mm/dd/yy
        string datetoday = todaydate.ToString("dd/MM/yyyy");
        try
        {
            DataTable dtable = datafile.GetPegPayAccount(cust.CustomerCode);
            if (dtable.Rows.Count > 0)
            {
                AccountNumber = dtable.Rows[0]["AccountNumber"].ToString();
                AccountBalance = dtable.Rows[0]["AccountBalance"].ToString();
            }

            AccountBalanceNo = Convert.ToDouble(AccountBalance.Split('.')[0]);
            CreditAmount = Convert.ToDouble(cust.CustomerCreditAmount.Split('.')[0]);
            //string nn = Num2Wrd(CreditAmount);
            //string companyName = "PEGASUS CREIT RECEIPT";

            string CreditAmountInWords = ToWords(CreditAmount);
            string AccountBalanceInWords = ToWords(AccountBalanceNo);

            string AccountBalanceNo_WithCommas = AccountBalanceNo.ToString("#,##0");
            string CreditAmountWithCommas = CreditAmount.ToString("#,##0");



            StringBuilder sb = new StringBuilder();
            sb.Append("<table width='100%' cellspacing='0'  cellpadding='2' frame='box'");
            string imageFile = Server.MapPath(".") + "/Images/Receipt.png";
            sb.Append(imageFile);
            // sb.Append("<img src='E:\\PePay\\MoMo\\Production\\application\\apps\\Images\\Receipt.png' width='125' height='101' hspace='20' vspace='3'>");
            //E:\PePay\MoMo\Production\application\apps\Images\Receipt.png
            sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>PEGASUS CREDIT RECEIPT</b></td></tr>");

            sb.Append("<tr><td colspan = '4' ></td></tr>");


            sb.Append("<tr><td colspan = '5'  align='right'><b>Receipt No.</b> ");
            sb.Append(cust.ReceiptNumber);
            sb.Append("</td></tr>");



            sb.Append("<tr><td colspan = '5'   align='right'><b>Date:</b> ");
            sb.Append(datetoday);
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  height='4'>&nbsp;</td> </tr>");



            sb.Append("<tr><td colspan = '5' ><b>Customer Name:</b>");
            sb.Append(cust.CustomerCode);
            sb.Append("</td></tr>");



            //sb.Append("<tr><td colspan = '4'><b>CustomerAccount :</b> ");
            //sb.Append(cust.CustomerAccount);
            //sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5'><b>Credited Amount :</b> ");
            sb.Append(CreditAmountWithCommas + "/" + "=");
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5' ><b>Credited Amount In Words :</b> ");
            sb.Append(CreditAmountInWords + "\t  Uganda Shillings Only");
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5' ><b>Account Balance :</b> ");
            sb.Append(AccountBalanceNo_WithCommas + "/" + "=");
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td colspan = '5' ><b>Account Balance in Words :</b> ");
            sb.Append(AccountBalanceInWords + "\t  Uganda Shillings Only");
            sb.Append("</td></tr>");

            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");
            sb.Append("<tr> <td colspan='10'  ><br></br;</td> </tr>");

            sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '7'><b>KIND REGARDS <br>PEGASUS TECHNOLOGIES LIMITED</br></b></td></tr>");

            sb.Append("<tr><td colspan = '4' ></td></tr>");
            sb.Append("</table>");



            StringReader sr = new StringReader(sb.ToString());


            Document myDoc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 50, 50);
            HTMLWorker htmlparser = new HTMLWorker(myDoc);


            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(myDoc, memoryStream);
                myDoc.Open();
                //Put the image
                iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(imageFile);

                //Image alignment
                myImage.ScaleToFit(600f, 350f);
                myImage.SpacingBefore = 50f;
                myImage.SpacingAfter = 10f;
                //myImage.IndentationLeft = 9f;
                // myImage.BorderWidthTop = 36f;
                myImage.Alignment = Element.ALIGN_CENTER;
                //myDoc.Add(para);
                myDoc.Add(myImage);


                //Add the content
                htmlparser.Parse(sr);
                myDoc.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                string sendEmailsFrom = "notifications@pegasus.co.ug";//bll.DecryptString(datafile.GetSystemParameter(2, 19));
                DataTable sendEmailToVendor = datafile.GetVendorContactPersonsEmails(cust.CustomerCode);



                MailMessage mm = new MailMessage();
                // MailAddress toEmail = new MailAddress("techsupport@pegasustechnologies.co.ug");
                mm.To.Clear();
                if (sendEmailToVendor.Rows.Count > 0)
                {
                    foreach (DataRow dr in sendEmailToVendor.Rows)
                    {


                        string emailaddresss = dr["EmailAddress"].ToString();
                        MailAddress toEmail = new MailAddress(emailaddresss);
                        mm.To.Add(toEmail);


                    }
                }
                else
                {

                    string emailaddresssTech = "techsupport@pegasus.co.ug";
                    MailAddress toEmail1 = new MailAddress(emailaddresssTech);
                    mm.To.Add(toEmail1);
                }
                //Console.WriteLine(line);

                MailAddress toEmail2 = new MailAddress("techsupport@pegasus.co.ug");

                //mm.To.Add(toEmail);
                //mm.To.Add(toEmail2);
                mm.CC.Add(toEmail2);

                mm.From = new MailAddress(sendEmailsFrom, "Pegasus Technologies");
                mm.Subject = "Pegasus Receipt";


                mm.Body = "Hello\t" + cust.CustomerCode + "<br></br><br></br>" + "Your account has  been credited with\t" + CreditAmountWithCommas + "/" + "="
                + "<br></br><br></br>" + "See Receipt Attached" + "<br></br><br></br><br></br>" + "Thank You" + "<br></br><b></br><b></br>Pegasus Technologies";
                mm.Attachments.Add(new Attachment(new MemoryStream(bytes), cust.CustomerCode + "\t Receipt.pdf"));


                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "173.249.48.156"; //"64.233.167.108";
                smtp.EnableSsl = true;
                smtp.Timeout = 800000;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = "notifications@pegasus.co.ug"; //bll.DecryptString(datafile.GetSystemParameter(2, 19)); //"antheamarthy@gmail.com";
                NetworkCred.Password = "notifications@123"; //bll.DecryptString(datafile.GetSystemParameter(2, 20));//"PasswordForEmail";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;


                smtp.Send(mm);

            }





        }
        catch (Exception ex)
        {
            throw ex;

        }


    }
    public string ToWords(double num)
    {
        // Return a word representation of the whole number value.
        // Remove any fractional part.
        num = Math.Truncate(num);

        // If the number is 0, return zero.
        if (num == 0) return "zero";

        string[] groups = {"", "Thousands", "Million", "Billion",
        "Trillion", "Quadrillion", "?", "??", "???", "????"};

        string result = "";

        // Process the groups, smallest first.
        int group_num = 0;
        while (num > 0)
        {
            // Get the next group of three digits.
            double quotient = Math.Truncate(num / 1000);
            int remainder = (int)Math.Round(num - quotient * 1000);
            num = quotient;

            // Convert the group into words.
            if (remainder != 0)

                result = GroupToWords(remainder) + " " + groups[group_num] + ", " + result;

            // Get ready for the next group.
            group_num++;
        }

        // Remove the trailing ", ".
        if (result.EndsWith(", "))
            result = result.Substring(0, result.Length - 2);

        return result.Trim();
    }
    // Convert a number between 0 and 999 into words.
    private string GroupToWords(int num)
    {
        string[] one_to_nineteen = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen" };
        string[] multiples_of_ten = { "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninty" };

        // If the number is 0, return an empty string.
        if (num == 0) return "";

        // Handle the hundreds digit.
        int digit;
        string result = "";
        if (num > 99)
        {
            digit = (int)(num / 100);
            num = num % 100;
            result = one_to_nineteen[digit] + " " + "Hundred";
        }

        // If num = 0, we have hundreds only.
        if (num == 0) return result.Trim();

        // See if the rest is less than 20.
        if (num < 20)
        {
            // Look up the correct name.
            result += " " + one_to_nineteen[num];
        }
        else
        {
            // Handle the tens digit.
            digit = (int)(num / 10);
            num = num % 10;
            result += " " + multiples_of_ten[digit - 2];

            // Handle the final digit.
            if (num > 0)
                result += " " + one_to_nineteen[num];
        }

        return result.Trim();
    }

    private string GetRecordsToApprove()
    {
        int Count = 0;
        string ItemArr = "";
        foreach (DataGridItem Items in DataGrid1.Items)
        {
            CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
            if (chk.Checked)
            {
                Count++;
                string ItemFound = Items.Cells[0].Text;
                ItemArr = ItemArr += ItemFound + ",";
            }
        }
        return ItemArr;
    }
    //public void SendReceiptToUsernameEmail(CustomerReceiptCreditDetails cust)
    //{


    //    // string custName = txtCustName.Text;
    //    //string CustAccount = txtCustAccount.Text;
    //    string AccountNumber = "";
    //    string AccountBalance = "";
    //    int AccountBalanceNo = 0;
    //    int CreditAmount = 0;
    //    DateTime todaydate = DateTime.Today.Date;
    //    //string bbd = todaydate.ToShortDateString();  //mm/dd/yy
    //    string datetoday = todaydate.ToString("dd/MM/yyyy");
    //    try
    //    {
    //        DataTable dtable = datafile.GetPegPayAccount(cust.CustomerCode);
    //        if (dtable.Rows.Count > 0)
    //        {
    //            AccountNumber = dtable.Rows[0]["AccountNumber"].ToString();
    //            AccountBalance = dtable.Rows[0]["AccountBalance"].ToString();
    //        }

    //        AccountBalanceNo = Convert.ToInt32(AccountBalance.Split('.')[0]);
    //        CreditAmount = Convert.ToInt32(cust.CustomerCreditAmount.Split('.')[0]);
    //        //string nn = Num2Wrd(CreditAmount);
    //        //string companyName = "PEGASUS CREIT RECEIPT";

    //        DataTable ReceiptTable = GetReceiptDataTable();
    //        DataRow row;

    //        // int count = 0;


    //        for (int i = 0; i < 1; i++)
    //        {
    //            row = ReceiptTable.NewRow();
    //            row["Custname"] = cust.CustomerCode;
    //            row["Custref"] = cust.CustomerAccount;
    //            row["Amount"] = CreditAmount + "\t USHS";
    //            row["NewBal"] = AccountBalanceNo;
    //            row["PaymentDate"] = datetoday;
    //            ReceiptTable.Rows.Add(row);
    //            ReceiptTable.AcceptChanges();
    //            // count++;
    //            //dt.Columns.Add("CustName");
    //            //dt.Columns.Add("CustRef");
    //            //dt.Columns.Add("Amount");
    //            //dt.Columns.Add("NewBal");
    //            //dt.Columns.Add("PaymentDate");
    //            //}

    //        }

    //        //ReportDocument crystalReport = new ReportDocument();
    //        CrystalReportViewer1.DisplayGroupTree = false;
    //        string appPath, physicalPath, rptName;
    //        appPath = HttpContext.Current.Request.ApplicationPath;
    //        physicalPath = HttpContext.Current.Request.MapPath(appPath);
    //        rptName = physicalPath + "\\Bin\\reports\\ClientReceipt.rpt";
    //        Rptdoc.Load(rptName);
    //        //Rptdoc.Load(Server.MapPath("~/ClientReceipt.rpt"));
    //        //Customers dsCustomers = GetData(query, crystalReport);
    //        Rptdoc.SetDataSource(ReceiptTable);
    //        CrystalReportViewer1.ReportSource = Rptdoc;
    //        CrystalReportViewer1.DataBind();
    //        //CrystalReportViewer1.Refresh();
    //        //CrystalReport1 objRpt = new CrystalReport1();
    //        //objRpt.SetDataSource(ReceiptTable);
    //        //crystalReportViewer1.ReportSource = objRpt;
    //        //crystalReportViewer1.Refresh();

    //        //string appPath, physicalPath, rptName;
    //        //appPath = HttpContext.Current.Request.ApplicationPath;
    //        //physicalPath = HttpContext.Current.Request.MapPath(appPath);
    //        //rptName = physicalPath + "\\Bin\\reports\\ClientReceipt.rpt";
    //        //Rptdoc.Load(rptName);
    //        //Rptdoc.SetDataSource(ReceiptTable);
    //        //objRpt.SetDataSource(ReceiptTable);
    //        //CrystalReportViewer1.ReportSource = objRpt;
    //        //CrystalReportViewer1.Refresh();
    //        //Rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "PAYMENTS");
    //        //CrystalReportViewer1.ReportSource = Rptdoc;
    //        //CrystalReportViewer1.ReportSource = Rptdoc;
    //        //CrystalReportViewer1.Refresh();
    //        //StringBuilder sb = new StringBuilder();
    //        //sb.Append("<table width='100%' cellspacing='0' cellpadding='2' border = '0'>");
    //        //sb.Append("<img src='E:\\PePay\\MoMo\\Production\\application\\apps\\Images\\Receipt.png' width='125' height='101' hspace='20' vspace='3'>");
    //        ////E:\PePay\MoMo\Production\application\apps\Images\Receipt.png
    //        //sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>PEGASUS CREDIT RECEIPT</b></td></tr>");

    //        //sb.Append("<tr><td colspan = '4'></td></tr>");


    //        //sb.Append("<tr><td colspan = '3'><b>Customer Name:</b>");
    //        //sb.Append(cust.CustomerCode);
    //        //sb.Append("</td></tr>");


    //        //sb.Append("<tr><td colspan = '3'><b>CustomerAccount :</b> ");
    //        //sb.Append(cust.CustomerAccount);
    //        //sb.Append("</td></tr>");



    //        //sb.Append("<tr><td colspan = '3'><b>Customer Credit Amount :</b> ");
    //        //sb.Append(CreditAmount+ "\t USHS");
    //        //sb.Append("</td></tr>");



    //        //sb.Append("<tr><td colspan = '3'><b>Account Balance :</b> ");
    //        //sb.Append(AccountBalanceNo +"\t USHS");
    //        //sb.Append("</td></tr>");


    //        //sb.Append("<tr><td colspan = '3'><b>Date Credited :</b> ");
    //        //sb.Append(datetoday);
    //        //sb.Append("</td></tr>");


    //        //sb.Append("</table>");

    //        //StringReader sr = new StringReader(sb.ToString());
    //        ////

    //        //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
    //        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
    //        //using (MemoryStream memoryStream = new MemoryStream())
    //        //{
    //        //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
    //        //    pdfDoc.Open();
    //        //    htmlparser.Parse(sr);
    //        //    pdfDoc.Close();
    //        //    byte[] bytes = memoryStream.ToArray();
    //        //    memoryStream.Close();

    //        string sendEmailsFrom = bll.DecryptString(datafile.GetSystemParameter(2, 19));
    //        //string sendEmailTo = datafile.GetVendorCodeEmail(cust.CustomerCode);


    //        MailMessage mm = new MailMessage(sendEmailsFrom, "antheamartha@yahoo.com");
    //        mm.Subject = "Pegasus Receipt";
    //        mm.Body = cust.CustomerCode + "Receipt Attachment";
    //        //mm.Attachments.Add(new Attachment(new MemoryStream(bytes), cust.CustomerCode + "\t Receipt.pdf"));
    //        Attachment attachment = new System.Net.Mail.Attachment(Rptdoc.ExportToStream(ExportFormatType.PortableDocFormat), "Report.pdf");
    //        mm.Attachments.Add(attachment);
    //        mm.IsBodyHtml = true;
    //        SmtpClient smtp = new SmtpClient();
    //        smtp.Host = "smtp.gmail.com";
    //        smtp.EnableSsl = true;
    //        NetworkCredential NetworkCred = new NetworkCredential();
    //        NetworkCred.UserName = bll.DecryptString(datafile.GetSystemParameter(2, 19)); //"antheamarthy@gmail.com";
    //        NetworkCred.Password = bll.DecryptString(datafile.GetSystemParameter(2, 20));//"steven186";
    //        smtp.UseDefaultCredentials = true;
    //        smtp.Credentials = NetworkCred;
    //        smtp.Port = 587;
    //        smtp.Send(mm);
    //    }






    //    catch (Exception ex)
    //    {
    //        throw ex;

    //    }



    //}

    private DataTable GetReceiptDataTable()
    {
        DataTable dt = new DataTable("Table2");
        // dt.Columns.Add("No.");
        dt.Columns.Add("Custname");
        dt.Columns.Add("Custref");
        dt.Columns.Add("Amount");
        dt.Columns.Add("NewBal");
        dt.Columns.Add("PaymentDate");
        //dt.Columns.Add("PrintedBy");
        //dt.Columns.Add("Str1");
        //dt.Columns.Add("Str2");
        return dt;
    }
    private void LoadCredits()
    {

        try
        {
            string custName = txtCustName.Text;
            string CustAccount = txtCustAccount.Text;
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            dataTable = datafile.GetCreditsToApprove(custName, CustAccount, fromdate, todate);
            if (dataTable.Rows.Count > 0)
            {
                MultiView1.ActiveViewIndex = 0;
                DataGrid1.Visible = true;
                DataGrid1.DataSource = dataTable;
                DataGrid1.CurrentPageIndex = 0;
                DataGrid1.DataBind();
                //ShowMessage(".", false);
            }
            else
            {
                MultiView1.ActiveViewIndex = -1;
                ShowMessage("No Credits To Approve", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            string str = GetRecordsToApprove().TrimEnd(',');
            if (str.Equals(""))
            {
                ShowMessage("Please Select Credits to Reject", true);
            }
            else
            {
                UpdateRejectedCredits(str);
                LoadCredits();
            }

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }
    private void UpdateRejectedCredits(string str)
    {
        int suc = 0;
        int failed = 0;
        string[] arr = str.Split(',');
        int i = 0;
        string User = Session["UserName"].ToString();
        for (i = 0; i < arr.Length; i++)
        {
            int RecordId = int.Parse(arr[i].ToString());


            //update Rejected credits
            string updatesuccess = datafile.UpdateRejectedCredit(RecordId, User);
            if (updatesuccess.Equals("SUCCESSFUL"))
            {
                //count++;
                suc++;
            }
            else
            {
                failed++;
            }


        }
        string msg = suc + " Credits Have Been Rejected and " + failed + "Failed";
        //datafile.LogActivity(Session["UserName"].ToString(), "Rejected Account Credits Details");
        ShowMessage(msg, false);
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            LoadCredits();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            string custName = txtCustName.Text;
            string CustAccount = txtCustAccount.Text;
            DateTime fromdate = bll.ReturnDate(txtfromDate.Text.Trim(), 1);
            DateTime todate = bll.ReturnDate(txttoDate.Text.Trim(), 2);
            dataTable = datafile.GetCreditsToApprove(custName, CustAccount, fromdate, todate);
            MultiView1.ActiveViewIndex = 0;
            DataGrid1.DataSource = dataTable;
            DataGrid1.CurrentPageIndex = e.NewPageIndex;
            DataGrid1.DataBind();
            ShowMessage(".", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }

    }
    //protected void btnApprove_Click(object sender, EventArgs e)
    //{

    //}
}
