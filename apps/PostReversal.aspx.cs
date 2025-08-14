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
using InterLinkClass.Epayment;

public partial class PostReversal : System.Web.UI.Page
{
    ProcessPay Process = new ProcessPay();
    DataLogin datafile = new DataLogin();
    Datapay datapay = new Datapay();
    BusinessLogin bll = new BusinessLogin();
    DataTable dataTable = new DataTable();
    DataTable dtable = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {                
                MultiView2.ActiveViewIndex = 0;
                MultiView3.ActiveViewIndex = 0;
                Button1.Enabled = false;        
                Button MenuTool = (Button)Master.FindControl("btnCallSystemTool");
                Button MenuPayment = (Button)Master.FindControl("btnCallPayments");
                Button MenuReport = (Button)Master.FindControl("btnCalReports");
                Button MenuRecon = (Button)Master.FindControl("btnCalRecon");
                Button MenuAccount = (Button)Master.FindControl("btnCallAccountDetails");
                Button MenuBatching = (Button)Master.FindControl("btnCallBatching");
                MenuTool.Font.Underline = false;
                MenuPayment.Font.Underline = true;
                MenuReport.Font.Underline = false;
                MenuRecon.Font.Underline = false;
                MenuAccount.Font.Underline = false;
                MenuBatching.Font.Underline = false;
                DisableBtnsOnClick();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
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
    private void DisableBtnsOnClick()
    {
        string strProcessScript = "this.value='Working...';this.disabled=true;";
        Button1.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button1, "").ToString());
        Button2.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button2, "").ToString());
        Button3.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button3, "").ToString());
        Button4.Attributes.Add("onclick", strProcessScript + ClientScript.GetPostBackEventReference(Button4, "").ToString());
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            InquirePayment();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void InquirePayment()
    {
        string receiptno = txtreceiptno.Text.Trim(); 
        if (receiptno.Equals(""))
        {
            ShowMessage("Please Enter Receipt Number", true);
            ClearContrls2();
            Button1.Enabled = false;
            txtreceiptno.Focus();
        }
        else
        {
            ShowMessage(".", true);
            string districtcode = Session["DistrictCode"].ToString();
            dtable = datapay.GetPaymentToReverse(receiptno, districtcode);
            if (dtable.Rows.Count > 0)
            {
                bool Reversed = bool.Parse(dtable.Rows[0]["Reversal"].ToString());
                if (!Reversed)
                {
                    Button1.Enabled = true;
                    lblcode.Text = dtable.Rows[0]["PaymentID"].ToString();
                    txtviewReceiptno.Text = dtable.Rows[0]["Recieptno"].ToString();
                    txtAgentRef.Text = dtable.Rows[0]["VendorRef"].ToString();
                    txtcode.Text = dtable.Rows[0]["Custref"].ToString();
                    txtname.Text = dtable.Rows[0]["CustomerName"].ToString();
                    txtCustType.Text = dtable.Rows[0]["CustType"].ToString();
                    txtpaymode.Text = dtable.Rows[0]["PaymentMode"].ToString();
                    txtpaytype.Text = dtable.Rows[0]["PaymentType"].ToString();
                    txtcashier.Text = dtable.Rows[0]["Cashier"].ToString();

                    double amount = double.Parse(dtable.Rows[0]["Amount"].ToString());
                    DateTime paydate = DateTime.Parse(dtable.Rows[0]["PayDate"].ToString());
                    txtAmount.Text = amount.ToString("#,##0");
                    txtpaydate.Text = paydate.ToString("dd/MM/yyyy : HH:MM:ss");
                }
                else
                {
                    ShowMessage("Payment With Receipt Number " + receiptno + " is already reversed", true);
                    ClearContrls2();
                    Button1.Enabled = false;
                }
            }
            else
            {
                ShowMessage("No Payment found for Receipt Number " + receiptno, true);
                ClearContrls2();
                Button1.Enabled = false;
            }

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            PostTransaction();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void PostTransaction()
    {
        MultiView2.ActiveViewIndex = -1;
        MultiView3.ActiveViewIndex = 1;
        txtconfirmReceiptno.Text = txtviewReceiptno.Text.Trim();
        txtconfirmcashier.Text = txtcashier.Text.Trim();
        txtconfirmpaydate.Text = txtpaydate.Text.Trim();
        txtconfirmamount.Text = txtAmount.Text.Trim();
        string msg = "Enter Reversal Naration and Continue if you are sure";
        ShowMessage(msg, true);
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView3.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = 0;
            txtcode.Text = "";
            txtviewReceiptno.Text = ""; 
            txtname.Text = "";
            txtreceiptno.Text = "";
            txtpaytype.Text = "";
            txtpaymode.Text = "";
            txtCustType.Text = "";
            txtAgentRef.Text = ""; 
            txtAmount.Text = "";
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView2.ActiveViewIndex = 0;
            MultiView3.ActiveViewIndex = 0;
            string msg = ".";
            ShowMessage(msg, true);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    protected void Button4_Click1(object sender, EventArgs e)
    {
        try
        {
            string str = lblcode.Text.Trim();
            string naration = txtnaration.Text.Trim();
            if (naration.Equals(""))
            {
                ShowMessage("Please Enter Reversal Naration", true);
                txtnaration.Focus();
            }
            else
            {
                string ret = Process.Reversestr(str, naration, false);
                if (ret.Contains("POSTED SUCCESSFULLY"))
                {
                    string recieptnumber = txtconfirmReceiptno.Text.Trim();
                    MultiView2.ActiveViewIndex = 0;
                    MultiView3.ActiveViewIndex = 0;
                    ClearContrls();
                    string msg = "Payment for Receipt Number " + recieptnumber + " has been reversed successfully";
                    ShowMessage(msg, true);

                }
                else
                {
                    MultiView2.ActiveViewIndex = 0;
                    MultiView3.ActiveViewIndex = 0;
                    ShowMessage(ret, true);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }
    private void ClearContrls2()
    {
        txtAgentRef.Text = "";
        txtAmount.Text = "";
        txtcashier.Text = "";
        txtcode.Text = "";
        txtCustType.Text = "";
        txtname.Text = "";
        txtnaration.Text = "";
        txtpaydate.Text = "";
        txtpaymode.Text = "";
        txtpaytype.Text = "";     
        txtviewReceiptno.Text = "";
        lblcode.Text = "0";
        txtconfirmamount.Text = "";
        txtconfirmcashier.Text = "";
        txtconfirmpaydate.Text = "";
        txtconfirmReceiptno.Text = "";
    }
    private void ClearContrls()
    {
        txtAgentRef.Text = "";
        txtAmount.Text = "";
        txtcashier.Text = "";
        txtcode.Text = "";
        txtCustType.Text = "";
        txtname.Text = "";
        txtnaration.Text = "";
        txtpaydate.Text = "";
        txtpaymode.Text = "";
        txtpaytype.Text = "";
        txtreceiptno.Text = "";
        txtviewReceiptno.Text = "";
        lblcode.Text = "0";
        txtconfirmamount.Text = "";
        txtconfirmcashier.Text = "";
        txtconfirmpaydate.Text = "";
        txtconfirmReceiptno.Text = "";
        Button1.Enabled = false;
    }
}
