using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MakeFortebetPayment : System.Web.UI.Page
{
    string message = "";
    BusinessLogin bll = new BusinessLogin();
    DataLogin datafile = new DataLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserName"] == null))
        {
            Response.Redirect("Default.aspx");
        }
        if (IsPostBack == false)
        {
            rbnMethod.SelectedValue = "0";
            MultiView1.ActiveViewIndex = 0;
            MultiView2.ActiveViewIndex = 0;
        }
    }

    protected void btnSubmitSingle_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtTelecomId.Text))
        {
            message = "ENTER TELECOM ID";
            txtTelecomId.Focus();
            ShowMessage(message, true);
        }
        else if (String.IsNullOrEmpty(txtAmount.Text))
        {
            message = "ENTER TRANSACTION AMOUNT";
            txtAmount.Focus();
            ShowMessage(message, true);
        }
        else if (String.IsNullOrEmpty(txtPhone.Text))
        {
            message = "ENTER PHONE NUMBER";
            txtPhone.Focus();
            ShowMessage(message, true);
        }
        else if (!IsvalidTelecomId(txtTelecomId.Text.Trim()))
        {
            message = "INVALID TELECOM ID";
            txtTelecomId.Focus();
            ShowMessage(message, true);
        }
        else if (!IsvalidAmount(txtAmount.Text.Trim()))
        {
            message = "INVALID TRANSACTION AMOUNT";
            txtAmount.Focus();
            ShowMessage(message, true);
        }
        else if (!IsvalidTel(txtPhone.Text.Trim()))
        {
            message = "INVAID PHONE NUMBER";
            txtPhone.Focus();
            ShowMessage(message, true);
        }
        else
        {
            try
            {
                string phone = FormatPhoneNumber(txtPhone.Text.Trim());
                string feedback = bll.ProcessAIrtelFortebetTransaction(txtTelecomId.Text.Trim(), phone, txtAmount.Text.Trim());
                if (feedback == "SUCCESS")
                {
                    message = "TRANSACTION HAS BEEN SUCCESSFULLY POSTED.";
                    ShowMessage(message, false);
                }
                else
                {
                    ShowMessage(feedback, true);
                }
            }
            catch (Exception ee)
            {
                message = ee.Message;
                ShowMessage(message, true);
            }
        }
    }
    private void ShowMessage(string Message, bool Error)
    {
        Label lblmsg = (Label)Master.FindControl("lblmsg");
        if (Error) { lblmsg.ForeColor = System.Drawing.Color.Red; lblmsg.Font.Bold = false; }
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

    protected void rbnMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbnMethod.SelectedValue.ToString() == "0")
            {
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = 0;
            }
            else
            {
                MultiView1.ActiveViewIndex = 1;
                Loafiles();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, true);
        }
    }

    private void Loafiles()
    {
        DataTable files = datafile.ExecuteDataSet("GetAirtelFortebetFiles").Tables[0];
        if (files.Rows.Count > 0)
        {
            DataGrid2.DataSource = files;
            DataGrid2.DataBind();
        }
    }

    private bool IsvalidTelecomId(string telecomId)
    {
        bool valid = false;
        // different lengths of these telecom ids: just keep adding new ones
        int[] len = { 11, 12,13,14,15 };
        if (len.Contains(telecomId.Length))
        {
            valid = true;
        }
        return valid;
    }

    private bool IsvalidFile(string filepath, out string error)
    {
        bool valid = false;
        error = "";
        try
        {
            string[] trans = File.ReadAllLines(filepath);
            int x = 0;
            foreach (string fileTran in trans)
            {
                string[] data = fileTran.Split(new string[] { "," }, StringSplitOptions.None);

                if (!(data.Length == 4))
                {
                    valid = false;
                    error = "Invalid Column length. At line " + x + ". File should have 4 columns (Telecom id, Date, Phone Number and Amount)";
                    return valid;
                }

                string telecomId = data[0];
                string phone = data[2];
                string amount = data[3];


                if (telecomId.Contains("Transaction ID"))
                {
                    x++;
                    continue;
                }

                if (amount == "0")
                {
                    x++;
                    continue;
                }
                if (!IsvalidTelecomId(telecomId.Trim()))
                {
                    valid = false;
                    x++;
                    error = "INVALID TELECOM ID. At line " + x + " (" + telecomId + ")";
                    return valid;
                }
                else if (!IsvalidAmount(amount.Trim()))
                {
                    valid = false;
                    x++;
                    error = "INVALID TRANSACTION AMOUNT. At line " + x + " (" + amount + ")";
                    return valid;
                }
                else if (!IsvalidTel(phone.Trim()))
                {
                    valid = false;
                    x++;
                    error = "INVAID PHONE NUMBER. At line " + x + " (" + phone + ")";
                    return valid;
                }
                else
                {
                    valid = true;
                }
                x++;
            }
        }
        catch (Exception ee)
        {
            error = ee.Message;
        }
        return valid;
    }
    private bool IsvalidAmount(string amount)
    {
        bool valid = false;
        try
        {
            double tranAmount = Double.Parse(amount);
            if (tranAmount <= 5000000 && tranAmount > 0)
            {
                valid = true;
            }
        }
        catch (Exception ee)
        {

        }

        return valid;
    }
    private bool IsvalidTel(string phone)
    {
        bool valid = false;
        if (phone.StartsWith("256") && phone.Length == 12)
        {
            valid = true;
        }
        else if (phone.StartsWith("07") && phone.Length == 10)
        {
            valid = true;
        }
        else if (phone.StartsWith("7") && phone.Length == 9)
        {
            valid = true;
        }
        return valid;
    }
    public string FormatPhoneNumber(string phone)
    {
        string phoneNumber = "";
        if (phone.StartsWith("256"))
        {
            phoneNumber = phone;
        }
        else if (phone.StartsWith("7"))
        {
            phoneNumber = "256" + phone;
        }
        else if (phone.StartsWith("07"))
        {
            phoneNumber = "256" + phone.Remove(0, 1);
        }
        else
        {
            phoneNumber = phone;
        }
        return phoneNumber;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            FileInfo file = new FileInfo(FileUpload1.FileName);
            string filename = "";
            string uploadedby = Session["UserName"] as string;
           
            if (!FileUpload1.HasFile)
            {
                message = "UPLOAD A FILE";
                FileUpload1.Focus();
                ShowMessage(message, true);
            }
            else if (FileUpload1.PostedFile.ContentLength >= 30000000)
            {
                FileUpload1.Focus();
                ShowMessage("This file is too big for upload", false);
            }
            else if ((String.IsNullOrEmpty(FileUpload1.FileName)) || !(file.Extension == ".csv"))
            {
                FileUpload1.Focus();
                ShowMessage("Please upload a csv document", true);
            }
            else
            {
                filename = Path.GetFileName(FileUpload1.FileName);
                filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + filename;
                string extension = Path.GetExtension(filename);
                string filePath = @"D:\PePay\Fortebet\ForteBet Airtel Files\";
                string fileDirectoryy = filePath + filename;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileUpload1.SaveAs(fileDirectoryy);
                if (!IsvalidFile(fileDirectoryy, out message))
                {
                    FileUpload1.Focus();
                    ShowMessage(message, true);
                }
                else
                {
                    bool save = bll.SaveAirelFortebetFile(fileDirectoryy, uploadedby);
                    if (save)
                    {
                        message = "SUCCESSFULLY UPLOADED THE FILE FOR PROCESSING";
                        ShowMessage(message, false);
                        Loafiles();
                    }
                }
            }
        }
        catch (Exception ee)
        {
            message = ee.Message;
            ShowMessage(message, true);
        }
    }

    protected void DataGrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {

    }
}