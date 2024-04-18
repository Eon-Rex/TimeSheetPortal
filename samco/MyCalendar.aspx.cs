using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;

namespace samco
{
    public partial class MyCalendar : System.Web.UI.Page
    {
        D365ServiceRefrence.D365ServiceCall D365Call = new D365ServiceRefrence.D365ServiceCall();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("LogOutError.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            if (!IsPostBack)
            {
                txtfromdate.Attributes.Add("readonly", "readonly");
                txtTodate.Attributes.Add("readonly", "readonly");

                try
                {
                   

                }
                catch (Exception ex)
                {
                    
                    Label4.Text = "Your Session Expired";
                }
            }

        }
        protected void lbkSignOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            
            Response.Redirect("default.aspx");
        }
        protected async void imgsave_Click(object sender, ImageClickEventArgs e)
        {
            if (txtfromdate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(imgsave, this.GetType(), "A Key", "alert('Select From Date.....');", true);
                txtfromdate.Focus();
                txtTodate.Text = "";
                return;
            }
            if (txtTodate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(imgsave, this.GetType(), "A Key", "alert('Select To Date.....');", true);
                txtTodate.Focus();
                return;
            }
            try
            {

                samco.ServiceReference3.MobileCustomeServiceClient obj = new ServiceReference3.MobileCustomeServiceClient();
                obj.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
                obj.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
                obj.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
                samco.ServiceReference3.CallContext con = new samco.ServiceReference3.CallContext();
                con.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();

                string userid = Session["UserId"].ToString();
               
                //DateTime Cdate = Convert.ToDateTime(Convert.ToDateTime(txtfromdate.Text).ToString("dd/MM/yyyy"));
                //DateTime Cdate1 = Convert.ToDateTime(Convert.ToDateTime(txtTodate.Text).ToString("dd/MM/yyyy"));

                DateTime Cdate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime Cdate1 = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                
                var objects = new
                {
                    _fromDate = Cdate,
                    _toDate = Cdate1,
                    _EmployeeId = userid
                };

                string GetCalenderDaysTimeSheet = "GetCalenderDaysTimeSheet";

                string jsonstring1 = (string)await D365Call.PostD365Service(GetCalenderDaysTimeSheet, objects);
                samco.ServiceReference3.ACX_DCGetCalenderTimeSheet[] CDATA = JsonConvert.DeserializeObject< samco.ServiceReference3.ACX_DCGetCalenderTimeSheet[]>(jsonstring1);


                //samco.ServiceReference3.ACX_DCGetCalenderTimeSheet[] CDATA = obj.GetCalenderDaysTimeSheet(con, Cdate, Cdate1, userid);
                DataTable CDt = new DataTable();
                CDt.Columns.Add("projId", typeof(string));
                CDt.Columns.Add("hour", typeof(int));
                CDt.Columns.Add("minutes", typeof(int));
                CDt.Columns.Add("projLineProtId", typeof(string));
                CDt.Columns.Add("projCategoryId", typeof(string));
                CDt.Columns.Add("categoryName", typeof(string));
                CDt.Columns.Add("transationId", typeof(string));
                for (int i = 0; i < CDATA.Count(); i++)
                {
                    CDt.Rows.Add(CDATA[i].projId, CDATA[i].hour, CDATA[i].minutes, CDATA[i].projLineProtId, CDATA[i].projCategoryId, CDATA[i].categoryName, CDATA[i].transationId);
                }
                DataTable final = new DataTable();
                final = CDt;
                gvbidhistory.DataSource = final;
                gvbidhistory.DataBind();

            }

            catch (Exception ex)
            {
                // LblMessage.Text = ex.Message.ToString();
            }

        }
        protected void gvbidhistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "Status")
            {
                string Key = e.CommandArgument.ToString();
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton UserId = (LinkButton)row.FindControl("btnDelete");
                string Value = UserId.CommandArgument.ToString();               
                Response.Redirect("EditTimesheet.aspx");

            }
        }
        protected void gvbidhistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvbidhistory, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }
        protected void gvbidhistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvbidhistory.PageIndex = e.NewPageIndex;
          

        }
        protected void gvbidhistory_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string myvalue = gvbidhistory.SelectedRow.Cells[0].Text;
                      

        }
        protected void gvbidhistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string myvalue = gvbidhistory.SelectedRow.Cells[0].Text;            
            string EValue = (gvbidhistory.SelectedRow.FindControl("lblCountry") as Label).Text;
            Session["BIDIDHISTORY"] = EValue;
            Response.Redirect("EditTimesheet.aspx");

        }

    }
}