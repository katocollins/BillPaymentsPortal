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
using System.Collections.Generic;

public partial class CompletePendingVasTransactions : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();
    string loggedinUser = "";
    string username = "";
    string fullname = "";
    string userBranch = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            username = Session["UserName"] as string;
            fullname = Session["FullName"] as string;
            userBranch = Session["UserBranch"] as string;
            loggedinUser = Session["Username"].ToString();

            if (IsPostBack == false)
            {
                MultiView2.ActiveViewIndex = 0;
            }
        }
        catch (Exception ee)
        {
            Response.Redirect("Default.aspx");
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            string referenceId = txtCustRef.Text;
            string phoneNumber = txtPhone.Text;
            string startDate = txtfromDate.Text;
            string endDate = txttoDate.Text;
            if (string.IsNullOrEmpty(startDate))
            {
                ShowMessage("Please enter the start date", true);
            }
            else if (string.IsNullOrEmpty(endDate))
            {
                ShowMessage("Please enter the end date", true);
            }
            else //if (!string.IsNullOrEmpty(referenceId))
            {
                DataTable table = datafile.GetPendingVasTransaction(referenceId, phoneNumber, startDate, endDate);
                if (table.Rows.Count > 0)
                {
                    if (table.Rows.Count == 1)
                    {
                        DataRow drow = table.Rows[0];
                        string beneficiary = drow["BeneficiaryID"].ToString();
                        string benName = drow["BeneficiaryName"].ToString();
                        string amount = drow["Amount"].ToString();
                        string referenceIds = drow["ReferenceId"].ToString();
                        string reason = drow["Reason"].ToString();
                        string status = drow["Status"].ToString();
                        string transDate = drow["RecordDate"].ToString();
                        string serviceName = drow["ServiceName"].ToString();
                        string merchantId = drow["MechantID"].ToString();

                        //string utilityRef = drow[""].ToString();
                        txtBenId.Text = beneficiary;
                        txt_benName.Text = benName;
                        txtTranAmount.Text = amount;
                        txtTranDate.Text = transDate;
                        txtReferenceId.Text = referenceIds;
                        txtServiceName.Text = serviceName;
                        lbl_serviceId.Text = merchantId;
                        txt_status.Text = status;
                        if (status.ToUpper() == "SUCCESS")
                        {
                            string telecomId = drow["Reason"].ToString();
                            btn_Fail.Visible = false;
                            //btn_Success.Enabled = false;
                            txtUtilityRef.Text = telecomId;
                        }

                        MultiView2.ActiveViewIndex = 1;
                    }
                    else
                    {
                        DataGrid1.DataSource = table;
                        DataGrid1.DataBind();
                        MultiView2.ActiveViewIndex = 0;
                    }
                    ShowMessage("", false);
                }
                else
                {
                    MultiView2.ActiveViewIndex = -1;
                    ShowMessage("Reference id NOT found", true);
                }
            }
            //else
            //{
            //    MultiView2.ActiveViewIndex = -1;
            //    ShowMessage("Please provide a reference id", true);
            //}
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

    protected void btn_Success_Click(object sender, EventArgs e)
    {
        try
        {
            string referencid = txtReferenceId.Text;
            string utilityRef = txtUtilityRef.Text;
            string serviceId = lbl_serviceId.Text;
            string serviceName = txtServiceName.Text;
            string status = txt_status.Text;
            string[] requiresReference = { "3", "12", "13" };
            string pegpayId = "";
            BaseObject obj = new BaseObject();
            // if(serviceId == "4" || serviceId == "12"  || serviceId == "3" || serviceId == "3")
            bool proceed = false;
            if (status.ToUpper() == "SUCCESS")
            {
                ShowMessage("Transaction is already successfull", true);
            }
            else if (!serviceName.ToUpper().Contains("NWSC") && string.IsNullOrEmpty(utilityRef))
            {
                ShowMessage("Utility reference/Telecom id is required", true);
                return;
            }
            if (serviceId == "12")
            {
                proceed = false;

                DataTable table = datafile.GetTransactionByVendorCode(referencid, "STANBIC_VAS");
                if (table.Rows.Count > 0)
                {
                    pegpayId = table.Rows[0]["PegpayTranId"].ToString();
                    string status2 = table.Rows[0]["Status"].ToString();
                    string telecomId = table.Rows[0]["TelecomId"].ToString();
                    if (status2.ToUpper() == "SUCCESS")
                    {
                        lbl_message.Text = "Transaction is already successfull with ReferenceId: " + telecomId + ", Hit Proceed if you want to update transactions this with this ReferenceId";//, true);
                        lbl_realId.Text = telecomId;
                        MultiView2.ActiveViewIndex = 2;
                        return;
                    }
                    else
                    {

                        proceed = datafile.UpdateTransactionInMobileMoney("SUCCESS", utilityRef, pegpayId, "0"); //UpdateTransactionInMobileMoney("SUCCESS", utilityRef, referencid, "STANBIC_VAS");
                        bll.InsertIntoAuditLog(referencid, "UPDATE", "Complete VAS Transaction", userBranch, username, bll.GetCurrentPageName(),
               fullname + " completed the VAS transaction ["+ referencid +"] with utilityRef: " + telecomId + " from IP: " + bll.GetIPAddress());
            
                    }
                }
            }
            else if (IsBillPayment(serviceId))
            {
                proceed = CompleteBillpayment(referencid, utilityRef, "STANBIC_VAS", out obj);
                if (!proceed)
                {
                    if (!string.IsNullOrEmpty(obj.Member3))
                    {
                        lbl_message.Text = "Transaction is already successfull with ReferenceId: " + obj.Member3 + ", Hit Proceed if you want to update transactions this with this ReferenceId";//, true);
                        lbl_realId.Text = obj.Member3;
                        MultiView2.ActiveViewIndex = 2;
                        return;
                    }
                }
            }
            else
            {
                proceed = true;
            }

            if (proceed)
            {
                if (obj.Member3 == "CONTINUE")
                {
                    utilityRef = obj.Member3;
                }
                bool updated = false;
                int runCount = 0;
                while (!updated)
                {
                    updated = datafile.UpdateVasTransactionStatus(loggedinUser, "SUCCESS", utilityRef, referencid);
                    if (runCount > 4)
                    {
                        break;
                    }

                    runCount++;
                }
                if (updated)
                {
                    ShowMessage("Transaction Status Updated Successfully", false);
                    txtUtilityRef.Text = "";
                    txtBenId.Text = "";
                    txt_benName.Text = "";
                    txtTranAmount.Text = "";
                    txtTranDate.Text = "";
                    txtReferenceId.Text = "";
                    txtServiceName.Text = "";
                    lbl_serviceId.Text = "";
                    txt_status.Text = "";
                    MultiView2.ActiveViewIndex = -1;
                }
                else
                {
                    //datafile.UpdateTransactionInMobileMoney(obj.Memeber2, utilityRef, pegpayId, "0", out obj);
                    ShowMessage("Update Failed", true);
                }
                //if (IsAirtimeTransaction(serviceName) || IsMobileDataRequest(serviceName) || serviceId == "13")
                //{
                //    // do nothing  
                //    ShowMessage("Transaction Status Updated Successfully", false);
                //}
                //else if (serviceId == "12")
                //{
                //    // update the  transaction in mobile money

                //}
                //else if (isFlexipaySchoolsTransaction(referencid))
                //{
                //    //Update the transaction in flexipay


                //}
                ////
                //else
                //{
                //    // update the transaction in bill payments
                //    datafile.TransferToReceivedFromDeleted("STANBIC_VAS", referencid, utilityRef);
                //}

            }
            else
            {
                ShowMessage("Update Failed", true);
            }
        }
        catch (Exception ee)
        {

        }
    }

    private bool CompleteBillpayment(string referenceId, string utilityRef, string vendorCode, out BaseObject obj)
    {
        bool isValid = false;
        obj = new BaseObject();
        try
        {

            string word = "";
            isValid = datafile.TransferToReceivedTransactions(referenceId, utilityRef, vendorCode, out word);
            if (IsValid)
            {
                obj.Member3 = word;
            }
        }
        catch (Exception ee)
        {

        }
        return isValid;
    }
    private bool IsAirtimeTransaction(string ServiceName)
    {
        if (ServiceName.ToUpper() == "MTU" ||
            ServiceName.ToUpper() == "AIRTIME")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsMobileDataRequest(string ServiceName)
    {
        if (ServiceName.Contains("_DATA"))
        {
            return true;
        }
        return false;
    }

    public bool IsBankToWallet(string ServiceName)
    {
        if (ServiceName.Contains("_B2W"))
        {
            return true;
        }
        return false;
    }
    private bool isFlexipaySchoolsTransaction(string ServiceName)
    {
        if (ServiceName.Contains("FLEXIPAY"))
        {
            return true;
        }
        return false;
    }
    protected void btn_Fail_Click(object sender, EventArgs e)
    {
        try
        {
            string referencid = txtReferenceId.Text;
            string utilityRef = txtUtilityRef.Text;
            string serviceId = lbl_serviceId.Text;
            string tranStatus = txt_status.Text;
            string serviceName = txtServiceName.Text;
            string[] requiresReference = { "3", "12", "13" };
            string pegpayId = "";
            string message = "";
            BaseObject obj = new BaseObject();
            // if(serviceId == "4" || serviceId == "12"  || serviceId == "3" || serviceId == "3")
            bool proceed = false;
            if (tranStatus.ToUpper() == "SUCCESS")
            {
                ShowMessage("Transaction is already successfull", true);
                return;
            }
            else if (string.IsNullOrEmpty(utilityRef))
            {
                ShowMessage("PLEASE ENTER THE REASON OF FAILING", true);
                return; ;
            }
            if (serviceId == "12")
            {
                proceed = false;
                DataTable table = datafile.GetTransactionByVendorCode(referencid, "STANBIC_VAS");

                if (table.Rows.Count > 0)
                {
                    string status = table.Rows[0]["Status"].ToString();
                    string telecomId = table.Rows[0]["TelecomId"].ToString();
                    if (status == "SUCCESS" && !string.IsNullOrEmpty(telecomId))
                    {
                        ShowMessage("Transaction is already successfull with telecom id: " + telecomId, false);
                        return;
                    }
                    else
                    {
                        obj.Member2 = table.Rows[0]["Status"].ToString();
                        pegpayId = table.Rows[0]["PegpayTranId"].ToString();
                        proceed = datafile.UpdateTransactionInMobileMoney("FAILED", utilityRef, pegpayId, "0"); //UpdateTransactionInMobileMoney("SUCCESS", utilityRef, referencid, "STANBIC_VAS");
                        if (!proceed && (status == "FAILED" || status == "REVERSED"))
                        {
                            proceed = true;
                        }
                        bll.InsertIntoAuditLog(referencid, "UPDATE", "Fail VAS Transaction", userBranch, username, bll.GetCurrentPageName(),
               fullname + " failed the VAS transaction with id: " + referencid + " from IP: " + bll.GetIPAddress());
            
                    }

                }
                else
                {
                    proceed = true;
                }

            }
            else if (IsBillPayment(serviceId))
            {
                proceed = MoveTransactionToDeletedTable(referencid, serviceId, out message);
            }
            else
            {
                proceed = true;
            }

            if (proceed)
            {
                bool updated = false;// 
                int count = 0;
                while (!updated)
                {
                    updated = datafile.UpdateVasTransactionStatus(loggedinUser, "FAILED", utilityRef, referencid);
                    if (count > 3)
                    {
                        break;
                    }
                    count++;
                }
                if (updated)
                {
                    txtUtilityRef.Text = "";
                    txtBenId.Text = "";
                    txt_benName.Text = "";
                    txtTranAmount.Text = "";
                    txtTranDate.Text = "";
                    txtReferenceId.Text = "";
                    txtServiceName.Text = "";
                    lbl_serviceId.Text = "";
                    txt_status.Text = "";
                    ShowMessage("Transaction Status Updated Successfully", false);
                    MultiView2.ActiveViewIndex = -1;
                }
                else
                {
                    ShowMessage("Update Failed: " + message, true);
                }
            }
            else
            {
                ShowMessage("Update Failed", true);
            }
        }
        catch (Exception ee)
        {
            ShowMessage("Update Failed: " + ee.Message, true);
        }
        //try
        //{
        //    string referencid = txtReferenceId.Text;
        //    string reason = txtUtilityRef.Text;
        //    string serviceId = lbl_serviceId.Text;
        //    string serviceName = txtServiceName.Text;

        //    if (string.IsNullOrEmpty(reason))
        //    {
        //        ShowMessage("Please provide the reason for failure", true);
        //        return;
        //    }

        //    bool updated = datafile.UpdateVasTransactionStatus(loggedinUser, "FAILED", reason, referencid);
        //    if (updated)
        //    {
        //        if (IsAirtimeTransaction(serviceName) || IsMobileDataRequest(serviceName) || serviceId == "13")
        //        {
        //            ShowMessage("Transaction Status Updated Successfully", false);
        //        }
        //        else if (serviceId == "12")
        //        {
        //            // update the  transaction in mobile money
        //            DataTable table = datafile.GetTransactionByVendorCode(referencid, "STANBIC_VAS");
        //            if (table.Rows.Count > 0)
        //            {
        //                string pegpayId = table.Rows[0]["PegpayTranId"].ToString();
        //                bool executed = datafile.FailInMobileMoney(reason, pegpayId, ""); //UpdateTransactionInMobileMoney("SUCCESS", utilityRef, referencid, "STANBIC_VAS");
        //                if (executed)
        //                {
        //                    ShowMessage("Transaction Status Updated Successfully", false);
        //                }
        //                else
        //                {
        //                    ShowMessage("Update Failed", true);
        //                }
        //            }
        //        }
        //        else if (isFlexipaySchoolsTransaction(referencid))
        //        {

        //        }
        //        //
        //        else
        //        {
        //            datafile.deleteTransactionFromReceivedTransaction("STANBIC_VAS", referencid, reason);
        //        }

        //    }
        //    else
        //    {
        //        ShowMessage("Update Failed", true);
        //    }
        //}
        //catch (Exception ee)
        //{

        //}
    }

    private bool IsBillPayment(string serviceId)
    {
        bool itsValid = false;
        string[] data = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
        List<string> serviceIds = new List<string>();
        serviceIds.AddRange(data);

        if (serviceIds.Contains(serviceId))
        {
            itsValid = true;
        }
        return itsValid;
    }

    private bool MoveTransactionToDeletedTable(string referenceId, string serviceId, out string message)
    {
        bool moved = false;
        message = "";
        try
        {
            DataTable table = datafile.GetTransactionByVendorCode(referenceId, "STANBIC_VAS");
            if (table.Rows.Count == 1)
            {
                string utilityRef = table.Rows[0]["UtilityTranRef"].ToString();
                string utilityCode = table.Rows[0]["UtilityCode"].ToString();
                if (string.IsNullOrEmpty(utilityRef))
                {
                    moved = datafile.TransferToReceivedFromDeleted("STANBIC_VAS", referenceId, "");

                }
                else
                {
                    message = "Transaction Allready Succssull at " + utilityCode;
                }
            }
            else
            {
                moved = true;
            }

        }
        catch (Exception ee)
        {

        }
        return moved;
    }
    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string ServId = e.Item.Cells[0].Text;
        string BeneId = e.Item.Cells[2].Text;
        string benName = e.Item.Cells[1].Text;
        string RefId = e.Item.Cells[3].Text;
        string ServName = e.Item.Cells[4].Text;

        string Amount = e.Item.Cells[5].Text;
        string utilityRef = e.Item.Cells[6].Text;
        string status = e.Item.Cells[7].Text;
        string date = e.Item.Cells[8].Text;


        if (e.CommandName == "btnFail")
        {
            txtBenId.Text = BeneId;
            txt_benName.Text = benName;
            txtTranAmount.Text = Amount;
            txtTranDate.Text = date;
            txtReferenceId.Text = RefId;
            txtServiceName.Text = ServName;
            lbl_serviceId.Text = ServId;
            txt_status.Text = status;
            MultiView2.ActiveViewIndex = 1;
            btn_Fail.Visible = true;
            btn_Success.Visible = false;
        }
        else if (e.CommandName == "btnCompete")
        {
            txtBenId.Text = BeneId;
            txt_benName.Text = benName;
            txtTranAmount.Text = Amount;
            txtTranDate.Text = date;
            txtReferenceId.Text = RefId;
            txtServiceName.Text = ServName;
            lbl_serviceId.Text = ServId;
            txt_status.Text = status;
            MultiView2.ActiveViewIndex = 1;
            btn_Fail.Visible = false;
            btn_Success.Visible = true;
        }
        else
        {

        }
    }
    protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {

    }
    protected void btn_Proceed_Click(object sender, EventArgs e)
    {
        try
        {
            string utilityRef = lbl_realId.Text;
            string referenceId = txtReferenceId.Text;
            bool updated = false;
            int runCount = 0;
            while (!updated)
            {

                updated = datafile.UpdateVasTransactionStatus(loggedinUser, "SUCCESS", utilityRef, referenceId);
                if (runCount > 4)
                {
                    break;
                }
                runCount++;
            }
            if (updated)
            {
                bll.InsertIntoAuditLog(referenceId, "UPDATE", "Complete VAS Transaction", userBranch, username, bll.GetCurrentPageName(),
fullname + " completed the VAS transaction with id: " + referenceId + " from IP: " + bll.GetIPAddress());
            
                ShowMessage("Transaction Status Updated Successfully", false);
                txtUtilityRef.Text = "";
                txtBenId.Text = "";
                txt_benName.Text = "";
                txtTranAmount.Text = "";
                txtTranDate.Text = "";
                txtReferenceId.Text = "";
                txtServiceName.Text = "";
                lbl_serviceId.Text = "";
                txt_status.Text = "";
                MultiView2.ActiveViewIndex = 1;
            }
            else
            {
                ShowMessage("Update Failed", true);
            }
        }
        catch (Exception ee)
        {

        }
    }
}
