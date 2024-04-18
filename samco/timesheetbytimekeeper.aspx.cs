using AjaxControlToolkit;
using Newtonsoft.Json;
using samco.ServiceReference3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace samco.CBol
{
    public partial class timesheetbytimekeeper : System.Web.UI.Page
    {
        D365ServiceRefrence.D365ServiceCall D365Call = new D365ServiceRefrence.D365ServiceCall();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("LogOutError.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            if (this.IsPostBack)
                return;
            this.txtworkdate.Attributes.Add("readonly", "readonly");
            this.txtTodate.Attributes.Add("readonly", "readonly");
            try
            {
                string str = this.Session["UserId"].ToString();
                //string str = Request.Cookies["UserID"]?.Value;
                MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
                samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
                CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString(); 

                string _manager = str;
                var objectPass = new   { _manager = _manager};

                string RetrieveManagerEmployeesIds = "RetrieveManagerEmployeesIds";
                string jsonstring1 = (string)await D365Call.PostD365Service(RetrieveManagerEmployeesIds, objectPass);
                ACX_DCEmployeeList[] acxDcEmployeeListArray = JsonConvert.DeserializeObject<ACX_DCEmployeeList[]>(jsonstring1);

                //ACX_DCEmployeeList[] acxDcEmployeeListArray = customeServiceClient.RetrieveManagerEmployeesIds(CallContext, _manager);
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Code", typeof(string));
                dataTable.Columns.Add("Value", typeof(string));
                dataTable.Rows.Add((object)"-1", (object)"--select--");
                for (int index = 0; index < ((IEnumerable<ACX_DCEmployeeList>)acxDcEmployeeListArray).Count<ACX_DCEmployeeList>(); ++index)
                {
                    dataTable.Rows.Add((object)acxDcEmployeeListArray[index].emplId, (object)(acxDcEmployeeListArray[index].emplId + "-" + acxDcEmployeeListArray[index].emplName));
                }
                this.DropDownList1.DataSource = (object)dataTable;
                this.DropDownList1.DataTextField = "Value";
                this.DropDownList1.DataValueField = "Code";
                this.DropDownList1.DataBind();
            }
            catch (Exception ex)
            {
                this.Label4.Text = "Your Session Expired";
            }
        }

        protected void lbkSignOut_Click(object sender, EventArgs e)
        {
            this.Session.Abandon();
            foreach (string cookieName in Request.Cookies.AllKeys)
            {
                HttpCookie cookie = Request.Cookies[cookieName];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }
            }
            this.Response.Redirect("default.aspx");
        }

        protected async void imgsave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.txtworkdate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Date.....');", true);
                this.txtworkdate.Focus();
            }
            else if (this.DropDownList1.SelectedValue == "-1")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Search TimeKeeper.....');", true);
                this.DropDownList1.Focus();
            }
            else
            {
                // For IIS
                DateTime dtFromDate = Convert.ToDateTime(DateTime.ParseExact(this.txtworkdate.Text, "dd/MM/yyyy", (IFormatProvider)System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
                DateTime dtToDate = Convert.ToDateTime(DateTime.ParseExact(this.txtTodate.Text, "dd/MM/yyyy", (IFormatProvider)System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));

                // For Dev
                //DateTime dtFromDate = Convert.ToDateTime(Convert.ToDateTime(txtworkdate.Text).ToString("dd/MM/yyyy"));
                //DateTime dtToDate = Convert.ToDateTime(Convert.ToDateTime(txtTodate.Text).ToString("dd/MM/yyyy"));

                string selectedValue = this.DropDownList1.SelectedValue;
                string str = this.Session["UserId"].ToString();
                MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
                samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
                CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();

                DateTime _fromDate = dtFromDate;
                DateTime _toDate = dtToDate;
                string _manager = str;
                string _timekeeper = selectedValue;

                var objectPass = new
                {
                    _fromDate = _fromDate,
                    _toDate = _toDate,
                    _manager = _manager,
                    _timekeeper = _timekeeper
                };


                string ReviewTimeSheetByTimekeeper = "ReviewTimeSheetByTimekeeper";
                string jsonstring1 = (string)await D365Call.PostD365Service(ReviewTimeSheetByTimekeeper, objectPass);
                ACX_DCReviewTimeSheetByTimeKeeker[] sheetByTimeKeekerArray = JsonConvert.DeserializeObject<ACX_DCReviewTimeSheetByTimeKeeker[]>(jsonstring1);


                //ACX_DCReviewTimeSheetByTimeKeeker[] sheetByTimeKeekerArray = customeServiceClient.ReviewTimeSheetByTimekeeper(CallContext, _fromDate, _toDate, _manager, _timekeeper);
                DataTable dataTable1 = new DataTable();
                dataTable1.Columns.Add("projId", typeof(string));
                dataTable1.Columns.Add("hour", typeof(int));
                dataTable1.Columns.Add("minutes", typeof(int));
                dataTable1.Columns.Add("projLineProtId", typeof(string));
                dataTable1.Columns.Add("projCategoryId", typeof(string));
                dataTable1.Columns.Add("categoryName", typeof(string));
                dataTable1.Columns.Add("transationId", typeof(string));
                for (int index = 0; index < ((IEnumerable<ACX_DCReviewTimeSheetByTimeKeeker>)sheetByTimeKeekerArray).Count<ACX_DCReviewTimeSheetByTimeKeeker>(); ++index)
                    dataTable1.Rows.Add((object)sheetByTimeKeekerArray[index].projId, (object)sheetByTimeKeekerArray[index].hour, (object)sheetByTimeKeekerArray[index].minutes, (object)sheetByTimeKeekerArray[index].projLineProtId, (object)sheetByTimeKeekerArray[index].projCategoryId, (object)sheetByTimeKeekerArray[index].categoryName, (object)sheetByTimeKeekerArray[index].transationId);
                DataTable dataTable2 = new DataTable();
                this.gvbidhistory.DataSource = (object)dataTable1;
                this.gvbidhistory.DataBind();
            }
        }

        protected void gvbidhistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "Status"))
                return;
            e.CommandArgument.ToString();
            ((LinkButton)((Control)e.CommandSource).NamingContainer.FindControl("btnDelete")).CommandArgument.ToString();
            this.Response.Redirect("rejecttimesheet.aspx");
        }

        protected void gvbidhistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            e.Row.Attributes["onclick"] = this.Page.ClientScript.GetPostBackClientHyperlink((Control)this.gvbidhistory, "Select$" + (object)e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }

        protected void gvbidhistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvbidhistory.PageIndex = e.NewPageIndex;
        }

        protected void gvbidhistory_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string text = this.gvbidhistory.SelectedRow.Cells[0].Text;
        }

        protected void gvbidhistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = this.gvbidhistory.SelectedRow.Cells[0].Text;
            this.Session["BIDIDHISTORY"] = (object)(this.gvbidhistory.SelectedRow.FindControl("lblCountry") as Label).Text;
           
            this.Response.Redirect("rejecttimesheet.aspx");
        }
    }
}