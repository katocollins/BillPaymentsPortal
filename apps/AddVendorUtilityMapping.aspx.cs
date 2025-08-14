using InterLinkClass.ControlObjects;
using InterLinkClass.CoreMerchantAPI;
using InterLinkClass.EntityObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class AddVendorUtilityMapping : System.Web.UI.Page
    {
        BusinessLogin bll = new BusinessLogin();

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
                }
            }
            catch (Exception ex)
            {
                bll.ShowMessage(lblmsg, ex.Message, true, Session);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                addVendorUtilityMapping();
            }
            catch (Exception ex)
            {
                string msg = "FAILED: " + ex.Message;
                bll.ShowMessage(lblmsg, msg, true, Session);
            }
        }

        private void LoadData()
        {
            bll.LoadVendors(userBranch, ddVendor);
            bll.LoadUtilities(userBranch, ddUtility);
        }

        private void addVendorUtilityMapping()
        {
            string response = "";
            try
            {
                if (ddVendor.SelectedValue == "")
                {
                    bll.ShowMessage(lblmsg, "Please select a Vendor", true, Session);
                    ddVendor.Focus();
                }
                else if (ddUtility.SelectedValue == "")
                {
                    bll.ShowMessage(lblmsg, "Please select a utility", true, Session);
                    ddUtility.Focus();
                }
                else
                {
                    string[] searchParams = GetSearchParameters();
                    response = bll.AddVendorUtilityMap(searchParams);
                    if (response.Contains("SUCCESSFULLY"))
                    {
                        bll.ShowMessage(lblmsg, response, false, Session);
                    }
                    else
                    {
                        bll.ShowMessage(lblmsg, response, true, Session);
                    }

                }

            }
            catch (Exception ex)
            {

                bll.ShowMessage(lblmsg, ex.Message, true, Session);
            }

        }

        private string[] GetSearchParameters()
        {
            List<string> searchCriteria = new List<string>();
            string Vendor = ddVendor.SelectedValue;
            string utility = ddUtility.SelectedValue;

            searchCriteria.Add(Vendor);
            searchCriteria.Add(utility);

            return searchCriteria.ToArray();
        }
    }
}