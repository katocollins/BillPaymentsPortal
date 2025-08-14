using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class EditAddDataBundle : System.Web.UI.Page
    {
        ProcessUsers Process = new ProcessUsers();
        DataLogin datafile = new DataLogin();
        BusinessLogin bll = new BusinessLogin();
        DataTable dataTable = new DataTable();
        DataTable dtable = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["AreaID"].ToString().Equals("1"))
                {
                    if (IsPostBack == false)
                    {
                        //Load
                        MultiView1.ActiveViewIndex = 0;
                        LoadAvailableBundle();
                        //AllAvailableDataTelecom
                        LoadSmsClientAvailableBundles();
                    }
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
        private void DisableBtnsOnClick()
        {

        }

        /// <summary>
        /// Load the dataBundles in Generic to the Portal. The Configuration between Vendors and Pegasus for data are in Generic
        /// </summary>
        public void LoadAvailableBundle()
        {
            try
            {
                DataTable dt = datafile.AllAvailableData();
                PegPayConfigs.DataSource = dt;
                PegPayConfigs.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }

        /// <summary>
        /// Load the Available Databundles in SMSClient for the pegasus to the Telecom Mapping view
        /// </summary>
        public void LoadSmsClientAvailableBundles()
        {
            try
            {
                DataTable dt = datafile.AllAvailableDataTelecom();
                TelecomConfigs.DataSource = dt;
                TelecomConfigs.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, true);
            }
        }


        /// <summary>
        /// Shows the error on the portals to the user
        /// </summary>
        /// <param name="Message">Error Message to show</param>
        /// <param name="Error">Formart for Error or not</param>
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

        protected void btnCallCustDetails_Click(object sender, EventArgs e)
        {

        }
        protected void btnAddDistrict_Click(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }
        protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {

        }
        protected void btnCallList_Click(object sender, EventArgs e)
        {

        }

        protected void PegPayConfigs_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                string command_name = e.CommandName.ToUpper();
                var record = e.Item.Cells[0].Text;
                if (command_name.Equals("EDIT_BUNDLE"))
                {
                    //Open View to Edit the DataBundle
                    string BundleID = e.Item.Cells[0].Text;
                    string NetworkCode = e.Item.Cells[1].Text;
                    string BundleVolume = e.Item.Cells[4].Text;
                    //string PegasusCode = e.Item.Cells[0].Text;
                    string BundleCode = e.Item.Cells[3].Text;
                    string IsActive = e.Item.Cells[6].Text;
                    string BundleAmount = e.Item.Cells[5].Text;
                    string Duration = e.Item.Cells[2].Text;
                    PegBundleCode.Text = BundleCode;
                    PegBundleCode.Enabled = false;
                    PegBundleAmount.Text = BundleAmount;
                    PegRecordId.Text = BundleID;
                    PegRecordId.Enabled = false;
                    PegNetwork.Text = NetworkCode;
                    PegNetwork.Enabled = false;
                    dll_pegStatus.SelectedValue = IsActive.ToLower();
                    PegDuration.Text = Duration;
                    PegBundleVolume.Text = BundleVolume;
                    MultiView1.ActiveViewIndex = 1;
                }
                else if (command_name.Equals("ACTIVATE_BUNDLE"))
                {
                    //Activate or deactivate a bundle
                }
            }
            catch (Exception ex)
            {
                ShowMessage("FAILED: " + ex.Message, true);
            }

        }

        protected void TelecomConfigs_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                var command = e.CommandName.ToUpper();
                if (command.Equals("Edit_Bundle".ToUpper()))
                {
                    SmsBundleID.Text = e.Item.Cells[0].Text;
                    SmsBundleID.Enabled = false;
                    SmsBundleCode.Text = e.Item.Cells[4].Text;
                    //SmsBundleCode.Enabled=false;    
                    SmsAmount.Text = e.Item.Cells[7].Text;
                    SmsDuration.Text = e.Item.Cells[2].Text;
                    SmsNetwork.Text = e.Item.Cells[1].Text;
                    SmsNetwork.Enabled = false;
                    SmsBundleName.Text = e.Item.Cells[5].Text;
                    SmsBundleName.Enabled = false;
                    SmsSelector.Text = e.Item.Cells[8].Text;
                    //SmsVolume.Text = e.Item.Cells[1].Text;
                    dll_smsActive.SelectedValue = e.Item.Cells[6].Text.ToLower();
                    SmsDataCode.Text = e.Item.Cells[3].Text;
                    SmsDataCode.Enabled = false;
                    MultiView1.ActiveViewIndex = 3;
                }
                else if (command.Equals("Activate_Bundle".ToUpper()))
                {

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ShowMessage("FAILED: " + ex.Message, true);
            }

        }

        protected void TelecomConfigs_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            var active = e.Item.Cells[6].Text.ToLower();
            if (active.Equals("false"))
            {
                e.Item.Cells[6].ForeColor = System.Drawing.Color.Maroon;
            }
            else if (active.Equals("false"))
            {
                e.Item.Cells[6].ForeColor = System.Drawing.Color.DarkBlue;
            }
            else
            {
                //e.Item.Cells[6].ForeColor = System.Drawing.Color.DarkBlue;
            }
        }

        protected void PegPayConfigs_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            var active = e.Item.Cells[6].Text.ToLower();
            if (active.Equals("false"))
            {
                e.Item.Cells[6].ForeColor = System.Drawing.Color.Maroon;
            }
            else if (active.Equals("true"))
            {
                e.Item.Cells[6].ForeColor = System.Drawing.Color.DarkBlue;
            }
            {
                //e.Item.Cells[6].ForeColor = System.Drawing.Color.White;
            }
        }

        protected void ViewV2PSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (ViewV2PSettings.Checked == true)
            {
                MultiView1.ActiveViewIndex = 0;
                LoadAvailableBundle();
                ViewP2TSettings.Checked = false;
                NewDataBundle.Checked = false;
            }
        }

        protected void ViewP2TSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (ViewP2TSettings.Checked == true)
            {
                MultiView1.ActiveViewIndex = 2;
                LoadSmsClientAvailableBundles();
                ViewV2PSettings.Checked = false;
                NewDataBundle.Checked = false;
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            string bundleId = PegRecordId.Text;
            string volume = PegBundleVolume.Text;
            string status = dll_pegStatus.SelectedValue.Trim();
            string duration = PegDuration.Text;
            string amount = PegBundleAmount.Text;
            try
            {
                datafile.UpdateDataBundleDetails(bundleId, volume, amount, status, duration);
                ShowMessage("Bundle Details Have Been Changed", false);
                PegBundleCode.Text = "";
                PegBundleAmount.Text = "";
                PegRecordId.Text = "";
                PegNetwork.Text = "";
                PegDuration.Text = "";
                PegBundleVolume.Text = "";
                LoadAvailableBundle();
                MultiView1.ActiveViewIndex = 0;
            }
            catch (Exception ex)
            {
                ShowMessage("FAILED: " + ex.Message, true);
            }
        }

        protected void Unnamed_Click1(object sender, EventArgs e)
        {
            try
            {
                string recordId = SmsBundleID.Text;
                string amount = SmsAmount.Text;
                string selector = SmsSelector.Text;
                string bundlecode = SmsBundleCode.Text;
                string isactive = dll_smsActive.SelectedValue.Trim();
                datafile.UpdateDataTelecomMapping(recordId, bundlecode, amount, selector, isactive);
                SmsBundleID.Text = "";
                SmsBundleCode.Text = "";
                //SmsBundleCode.Enabled=false;    
                SmsAmount.Text = "";
                SmsDuration.Text = "";
                SmsNetwork.Text = "";
                SmsBundleName.Text = "";
                SmsSelector.Text = "";
                SmsDataCode.Text = "";
                LoadSmsClientAvailableBundles();
                MultiView1.ActiveViewIndex = 2;
            }
            catch (Exception er)
            {
                ShowMessage("FAILED: " + er.Message, true);
            }

        }

        protected void NewDataBundle_CheckedChanged(object sender, EventArgs e)
        {
            if (NewDataBundle.Checked == true)
            {
                ViewV2PSettings.Checked = false;
                MultiView1.ActiveViewIndex = 4;
                ViewP2TSettings.Checked = false;

            }
        }

        protected void Unnamed_Click2(object sender, EventArgs e)
        {
            try
            {
                string bundlename = TextBox4.Text.Trim();
                string bundlecode = TextBox2.Text.Trim();
                string pegCode = TextBox3.Text.Trim();
                string networkCode = DropDownList2.SelectedValue.Trim();
                string amount = TextBox6.Text.Trim();
                string duration = TextBox7.Text.Trim();
                string volume = TextBox1.Text.Trim();
                string selector = TextBox8.Text.Trim();
                //file save or update databundledetails
                datafile.CreateDataBundleDetails(pegCode, bundlename, volume, amount, networkCode, duration);
                datafile.CreateDataTelecomMapping(pegCode, bundlecode, bundlename, networkCode, duration, amount, selector);

            }
            catch (Exception er)
            {
                ShowMessage("Failed: " + er.Message, true);
            }

        }
    }
}