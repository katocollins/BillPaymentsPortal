using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace apps
{
    public partial class SMSOutubox : System.Web.UI.Page
    {
        string username = "";
        string fullname = "";
        string vendor = "";
        BusinessLogin bll = new BusinessLogin();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                username = Session["UserName"] as string;
                fullname = Session["FullName"] as string;
                vendor = Session["DistrictID"] as string;

                Session["IsError"] = null;
                //Session is invalid
                if (username == null)
                {
                    Response.Redirect("Default.aspx");
                }

                MultiView1.ActiveViewIndex = 0;
                //MultiView3.ActiveViewIndex = 0;
                //LoadAllMoMoLogs();

                if (!IsPostBack)
                {

                }

            }
            catch (Exception ex)
            {
                bll.ShowMessage(lblmsg, ex.Message, true, Session);
            }

        }






        protected string SearchResultMessage(string id, string idName, string sdate, string edate)
        {
            string message = null;
            string resp = null;

            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(sdate) && !string.IsNullOrEmpty(edate))
            {
                resp = "between " + sdate + " and " + edate + " for " + idName + " " + id + " ";

            }
            else if (!string.IsNullOrEmpty(id) && string.IsNullOrEmpty(sdate) && string.IsNullOrEmpty(edate))
            {
                resp = " for " + idName + " " + id + " ";

            }

            else if (string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(sdate) && !string.IsNullOrEmpty(edate))
            {
                resp = "between " + sdate + " and " + edate + "";

            }
            else if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(sdate) && string.IsNullOrEmpty(edate))
            {
                resp = "for " + idName + " " + id + " starting from date " + sdate + "";
            }
            else if (string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(sdate) && string.IsNullOrEmpty(edate))
            {
                resp = "starting from date " + sdate + "";
            }
            else if (string.IsNullOrEmpty(edate))
            {
                resp = "";
            }

            message = String.Format("Currently, there are no SMS Outbox logs {0}", resp);
            return message;

        }






        protected void SearchTransButton_Click(object sender, EventArgs e)
        {
            try
            {
                string phone = Phone.Text;
                string message = Message.Text;
                string mask = Mask.SelectedValue.Trim();
                string sendingPartner = Sender.SelectedValue.Trim();
                string start = startTime.Text;
                string end = endTime.Text;
                if (string.IsNullOrEmpty(start))
                {
                    start = DateTime.Now.AddDays(-5).ToString();
                }
                if (string.IsNullOrEmpty(end))
                {
                    end = DateTime.Now.AddDays(1).ToString();
                }
                else
                {
                    end = DateTime.Parse(end).AddDays(1).ToString();
                }
                var table = bll.GetSMSOutBoxRecords(phone, message, mask, sendingPartner, DateTime.Parse(start), DateTime.Parse(end));
                datagrid_cell.DataSource = table;
                datagrid_cell.DataBind();

            }
            catch (Exception er)
            {
                bll.ShowMessage(lblmsg, er.Message, true, Session);
            }
        }
    }
}