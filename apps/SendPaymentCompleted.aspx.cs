using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace apps
{
    public partial class SendPaymentCompleted : System.Web.UI.Page
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
            bll.LoadVendors("MTN", ddVendor);
            bll.LoadUtilities(userBranch, ddUtility);
        }

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = SearchDb();
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
                            bll.ExportToExcel(dt, "", Response);
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string fromDate = txtFromDate.Text;
                if (string.IsNullOrEmpty(fromDate))
                {
                    throw new Exception("Please provide a from date");
                }

                SearchDb();
            }
            catch (Exception ex)
            {
                string msg = "FAILED: " + ex.Message;
                bll.ShowMessage(lblmsg, msg, true, Session);
            }
        }

        private DataTable SearchDb()
        {
            DataTable dt = new DataTable();
            try
            {
                string[] searchParams = GetSearchParameters();
                DataSet ds = bll.Search("GetMTNTransDetails", searchParams);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();
                    string msg = "Found " + dt.Rows.Count + " Records Matching Search Criteria";
                    Multiview2.ActiveViewIndex = 0;
                    bll.ShowMessage(lblmsg, msg, false, Session);
                }
                else
                {
                    dataGridResults.DataSource = dt;
                    dataGridResults.DataBind();
                    string msg = "No Records Found Matching Search Criteria";
                    bll.ShowMessage(lblmsg, msg, true, Session);
                }
            }
            catch (Exception ex)
            {
                dataGridResults.DataSource = dt;
                dataGridResults.DataBind();
                bll.ShowMessage(lblmsg, ex.Message, true, Session);
            }
            return dt;
        }

        private string[] GetSearchParameters()
        {
            List<string> searchCriteria = new List<string>();
            string Vendor = ddVendor.SelectedValue;
            string utility = ddUtility.SelectedValue;
            string value = txtReference.Text;
            string fromDate = txtFromDate.Text;
            string toDate = txtToDate.Text;

            searchCriteria.Add(Vendor);
            searchCriteria.Add(utility);
            searchCriteria.Add(value);
            searchCriteria.Add(fromDate);
            searchCriteria.Add(toDate);
            return searchCriteria.ToArray();
        }

        protected void dataGridResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            GridViewRow row;
            GridView grid = sender as GridView;
            DataLogin dh = new DataLogin();

            index = Convert.ToInt32(e.CommandArgument);
            row = grid.Rows[index];
            string tranId = row.Cells[1].Text;
            string vendorId = row.Cells[2].Text;
            string phone = row.Cells[4].Text;
            string utility = row.Cells[6].Text;

            if (e.CommandName.Equals("Complete"))
            {
                PaymentRequest request = new PaymentRequest();
                request.ReceiversRef = tranId;
                request.TransactionId = vendorId;
                request.SendersRef = phone;
                request.Vendor = ddVendor.SelectedValue;
                request.Utility = utility;

                string flag = row.Cells[7].Text;

                if (flag.Equals("1"))
                {
                    throw new Exception("The flag is already 1 for this transaction");
                }

                CompleteAtMtnAndUpdateFlag(request);

                dh.ExecuteDataSet("UpdateSentToVendor", vendorId, 1);
                bll.InsertIntoAuditLog(vendorId, "UPDATE", "UpdateSentToVendor", userBranch, username, bll.GetCurrentPageName(), fullname + " updated the sentToVendor flag for the " + ddUtility.SelectedValue + " transaction " + vendorId);
                bll.ShowMessage(lblmsg, "Transaction updated succssfully", false, Session);
            }
            else
            {
                //bll.DeleteTransaction(vendorId, "FAILED AT MTN");
                bll.InsertIntoAuditLog(vendorId, "UPDATE", "DeletedTransactions", userBranch, username, bll.GetCurrentPageName(), fullname + " transferred the " + ddUtility.SelectedValue + " transaction [" + vendorId + "] to the deleted table. Reason: FAILED AT MTN");
                bll.ShowMessage(lblmsg, "The transaction " + vendorId + " has been failed", false, Session);
            }
        }

        private void CompleteAtMtnAndUpdateFlag(PaymentRequest trans)
        {

            if (WasSuccessfullAtECW(trans.TransactionId))
            {
                MarkAsComplete(trans.TransactionId);
                bll.ShowMessage(lblmsg, "Transaction completed successfully", false, Session);
                return;
            }
            else
            {
                //try to complete at mtn again
                GoToMtn(trans);
            }
        }

        private void GoToMtn(PaymentRequest trans)
        {
            string status = "COMPLETED";

            //notify MTN using PaymentCompleted Mechanism.
            MTNPaymentCompletedResponse Response = NotifyMTNofStatusAtUtility(trans, status, "");

            //if MTN has been successfully notified
            if (Response.Reason.Contains("RESOURCE_TEMPORARY_LOCKED"))
            {
                Response = NotifyMTNofStatusAtUtility(trans, status, "");
            }

            bool FlagHasBeenUpdatedOk = false;
            if (Response.HasBeenSuccessfullAtMtn())
            {
                MarkAsComplete(trans.TransactionId);
            }
            else
            {
                bll.DeleteTransaction(trans.TransactionId, Response.Reason);
                bll.InsertIntoAuditLog(trans.TransactionId, "UPDATE", "SendPaymentCompleted", userBranch, username, bll.GetCurrentPageName(), fullname + " failed to complete the " + ddUtility.SelectedValue + " transaction [" + trans.TransactionId + " Reason:" + Response.Reason);
            }

            //log the MTN Response Got
            bll.InsertIntoVendorResponseLogs(trans.TransactionId, Response.Reason, Response.xmlResponse, status);
            bll.ShowMessage(lblmsg, Response.Reason, FlagHasBeenUpdatedOk, Session);
        }

        private void MarkAsComplete(string vendorTranId)
        {
            try
            {
                //set sentToVendor=1
                bll.UpdateSentToVendor(vendorTranId, 1);
                bll.InsertIntoAuditLog(vendorTranId, "UPDATE", "SendPaymentCompleted", userBranch, username, bll.GetCurrentPageName(), fullname + " completed the  " + ddUtility.SelectedValue + " transaction [" + vendorTranId);

            }
            catch (Exception ex)
            {
                bll.ShowMessage(lblmsg, ex.Message, true, Session);
            }
        }

        internal bool WasSuccessfullAtECW(string VendorTranId)
        {
            DataLogin dh = new DataLogin();
            DataTable datatable = new DataTable();
            try
            {
                datatable = dh.ExecuteDataSet("CheckIfWasSuccessfullAtECW", VendorTranId).Tables[0];
                if (datatable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //RowDataBound Event  
        protected void dataGridResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                if (e.Row.Cells[8].Text == "0")
                {
                    e.Row.Cells[8].BackColor = Color.Red;
                }
                else
                {
                    e.Row.Cells[8].BackColor = Color.LawnGreen;
                }
            }
        }

        private MTNPaymentCompletedResponse NotifyMTNofStatusAtUtility(PaymentRequest trans, string Status, string failureReason)
        {
            MTNPaymentCompletedResponse mtnResp = new MTNPaymentCompletedResponse();
            string timeIn = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string request = "";
            try
            {
                string mtnNotifyUrl = bll.GetSystemParameter(8, 3);
                request = GetMTNPaymentCompletedRequest(trans, Status, failureReason);
                WebRequest r = WebRequest.Create(mtnNotifyUrl);
                r.Method = "POST";
                r.ContentType = "text/xml";
                byte[] byteArray = Encoding.UTF8.GetBytes(request);
                r.ContentLength = byteArray.Length;
                string nonce = "WScqanjCEAC4mQoBE07sAQ==";
                string created = "" + DateTime.Now;
                string SP_ID = bll.GetPaymentCompletedCreds(trans.Utility, trans.Vendor)["SpId"];
                string password = bll.GetPaymentCompletedCreds(trans.Utility, trans.Vendor)["Password"];
                string MSISDN = trans.SendersRef;
                string Signature = "43AD232FD45FF";
                string Cookies = "sessionid=default8fcee064690b45faa9f8f6c7e21c5e5a";
                string toBeHashed = nonce + created + password;
                string passwordDigest = GetSHA1Hash(toBeHashed);
                r.Timeout = 190000;
                r.Headers["Authorization"] = "WSSE realm=\"SDP\"," +
                                               "profile=\"UsernameToken\"";
                r.Headers["X-WSSE"] = "UsernameToken Username=\"" + SP_ID + "\"," +
                                               "PasswordDigest=\"" + passwordDigest + "\"," +
                                               "Nonce=\"" + nonce + "\"," +
                                               "Created=\"" + created + "\"";
                r.Headers["X-RequestHeader"] = "request ServiceId=\"\"," +
                                               "TransId=\"\"," +
                                               "LinkId=\"\"," +
                                               "FA=\"\"";
                r.Headers["Signature"] = Signature;
                r.Headers["Cookie"] = Cookies;
                r.Headers["Msisdn"] = MSISDN;
                r.Headers["X-HW-Extension"] = "k1=v1;k2=v2";

                Stream dataStream = r.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                WebResponse response = r.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();
                StreamReader rdr = new StreamReader(dataStream);
                string feedback = rdr.ReadToEnd();
                string whatToLog = "PaymentCompletedRequestSent :" +
                                   Environment.NewLine + request +
                                   Environment.NewLine + Environment.NewLine;
                whatToLog = whatToLog + "Vendor Xml Response :" + feedback +
                                   Environment.NewLine + Environment.NewLine;
                whatToLog = whatToLog + "---------------------------------------";

                string timeOut = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                bll.LogRequest(ddUtility.SelectedValue, trans.TransactionId, "[Request Time: " + timeIn + "] " + request, "[Response Out: " + timeOut + "] " + feedback);

                mtnResp = GetMTNResponse(feedback);
                Console.WriteLine(mtnResp.successfullyNotified);
                return mtnResp;
            }
            catch (WebException ex)
            {
                bll.LogErrors("NotifyMTNofStatusAtUtility: " + ex.Message + " = " + ex.InnerException + " - " + trans.TransactionId, ddUtility.SelectedValue);

                if (ex.Message.ToUpper().Contains("TIMED OUT") || ex.Message.ToUpper().Contains("UNABLE TO CONNECT TO THE REMOTE SERVER"))
                {
                    //Do nothing
                    mtnResp.successfullyNotified = "00";
                    mtnResp.isFailureResponse = true;
                    mtnResp.Reason = ex.Message;
                    mtnResp.xmlResponse = "";
                }
                // we reached MTN but there is a problem with the PaymentRequest i.e mayb its no longer active etc
                else
                {
                    using (Stream stream = ex.Response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        try
                        {
                            string feedback = reader.ReadToEnd();
                            mtnResp = GetMTNResponse(feedback);
                            string timeOut = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            return mtnResp;
                        }
                        catch (Exception ee)
                        {
                            mtnResp.successfullyNotified = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper().Contains("TIMED OUT") || ex.Message.Contains("UNABLE TO CONNECT TO REMOTE SERVER"))
                {
                    //Do nothing
                    mtnResp.Reason = ex.Message;
                }
            }
            return mtnResp;

        }

        private string GetSHA1Hash(string toBeHashed)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(toBeHashed);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hash = sha.ComputeHash(bytes);
            string authkey = Convert.ToBase64String(hash);
            return authkey;
        }

        private MTNPaymentCompletedResponse GetMTNResponse(string feedback)
        {
            MTNPaymentCompletedResponse resp = new MTNPaymentCompletedResponse();
            resp.xmlResponse = feedback;
            //PARSE XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(feedback);
            XmlNodeList elemList = xmlDoc.GetElementsByTagName("*");
            bool success = GetTranStatus(feedback);


            if (success)
            {
                resp.Reason = "";
                resp.successfullyNotified = "1";
                resp.isFailureResponse = false;
            }
            else
            {
                //if it is an error at DSTV they return the falure reason in the error code
                for (int i = 0; i < elemList.Count; i++)
                {
                    try
                    {
                        resp.Reason = elemList[i].Attributes["errorcode"].Value;
                        resp.successfullyNotified = "1";//true
                        resp.isFailureResponse = true;
                        return resp;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return resp;
        }

        private bool GetTranStatus(string feedback)
        {
            bool success;
            try
            {
                XmlDocument XmlRequest = new XmlDocument();
                XmlRequest.LoadXml(feedback);
                XmlNodeList successlist = XmlRequest.GetElementsByTagName("ns2:paymentcompletedresponse");
                XmlNodeList failurelist = XmlRequest.GetElementsByTagName("ns2:errorResponse");
                if (successlist.Count > 0)
                {
                    success = true;
                }
                else if (failurelist.Count > 0)
                {
                    success = false;
                }
                else
                {
                    success = false;
                }
                return success;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetMTNPaymentCompletedRequest(PaymentRequest trans, string status, string FailureReason)
        {
            string requestBody = "";
            requestBody = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                            "<ns2:paymentcompletedrequest  xmlns:ns2=\"http://www.ericsson.com/em/emm/sp/backend\" >" +
                            "<transactionid>" + trans.TransactionId + "</transactionid>" +
                            "<providertransactionid>" + trans.ReceiversRef + "</providertransactionid>" +
                            "<status>" + status + "</status>" +
                            "</ns2:paymentcompletedrequest>";
            return requestBody;
        }
    }

    public class MTNPaymentCompletedResponse
    {
        public string successfullyNotified;
        public string Reason;
        public string xmlResponse;
        public bool isFailureResponse;

        public MTNPaymentCompletedResponse()
        {
            isFailureResponse = false;
            successfullyNotified = "0";
            Reason = "";
            xmlResponse = "";
        }

        public bool HasBeenSuccessfullAtMtn()
        {
            if (successfullyNotified.Equals("1") && !isFailureResponse)
            {
                return true;
            }
            return false;
        }
    }

    public class PaymentRequest
    {
        public string TransactionId;
        public string SendersRef;
        public string ReceiversRef;
        public string ReceiversName;
        public string RecievingFri;
        public string Amount;
        public string Currency;
        public string Message;
        public string Vendor;
        public string StatusCode;
        public string StatusDescription;
        public string BankCode;
        public string CustomerTel;
        public string Utility;

    }
}