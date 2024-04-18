using System;
using System.Collections;
using System.Configuration;
using samco.ServiceReference3;
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
using System.Drawing;
using samco.ServiceReference3;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace samco.CBol
{
    public partial class mytimesheets : System.Web.UI.Page
    {
        samco.D365ServiceRefrence.D365ServiceCall D365Call = new D365ServiceRefrence.D365ServiceCall();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("LogOutError.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            if (this.IsPostBack)
                return;
            this.txtgettimesheet.Attributes.Add("readonly", "readonly");
            try
            {
                
            }
            catch (Exception ex)
            {
                this.Label4.Text = "Your Session Expired";
            }
        }

        protected void lbkSignOut_Click(object sender, EventArgs e)
        {
            this.Session.Abandon();
            

            this.Response.Redirect("default.aspx");
        }

        protected async void imgsave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.txtgettimesheet.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select From Date.....');", true);
                this.txtgettimesheet.Focus();
                this.txtTodate.Text = "";
            }
            else if (this.txtTodate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select To Date.....');", true);
                this.txtTodate.Focus();
            }
            else
            {
                try
                {
                    MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
                    string str = this.Session["UserId"].ToString();
                    customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
                    customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
                    customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
                    samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
                    CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();

                    //For Dev
                    //DateTime dateTime1 = Convert.ToDateTime(Convert.ToDateTime(txtgettimesheet.Text).ToString("dd/MM/yyyy"));
                    //DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(txtTodate.Text).ToString("dd/MM/yyyy"));
                    //For IIS

                    //DateTime dateTime1 = Convert.ToDateTime(DateTime.ParseExact(this.txtgettimesheet.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
                    //DateTime dateTime2 = Convert.ToDateTime(DateTime.ParseExact(this.txtTodate.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));


                    DateTime dateTime1 = DateTime.ParseExact(this.txtgettimesheet.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                    DateTime dateTime2 = DateTime.ParseExact(this.txtTodate.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                    //CallContext CallContext = callContext;
                    //DateTime _fromDate = dateTime1;
                    //DateTime _toDate = dateTime2;
                    string _EmployeeId = str;
                    var objects = new {
                        _fromDate = dateTime1,
                        _toDate = dateTime2,
                        _EmployeeId = _EmployeeId
                    };

                    string GetMyTimeSheet = "GetMyTimeSheet";

                    string jsonstring1 = (string)await D365Call.PostD365Service(GetMyTimeSheet, objects);
                    ACX_DCGetTimeSheet[] myTimeSheet = JsonConvert.DeserializeObject<ACX_DCGetTimeSheet[]>(jsonstring1);
                    

                    //ACX_DCGetTimeSheet[] myTimeSheet = customeServiceClient.GetMyTimeSheet(CallContext, _fromDate, _toDate, _EmployeeId);
                    DataTable dataTable1 = new DataTable();
                    dataTable1.Columns.Add("projId", typeof(string));
                    dataTable1.Columns.Add("hour", typeof(int));
                    dataTable1.Columns.Add("minutes", typeof(int));
                    dataTable1.Columns.Add("projLineProtId", typeof(string));
                    dataTable1.Columns.Add("projCategoryId", typeof(string));
                    dataTable1.Columns.Add("categoryName", typeof(string));
                    dataTable1.Columns.Add("transationId", typeof(string));
                    for (int index = 0; index < ((IEnumerable<ACX_DCGetTimeSheet>)myTimeSheet).Count<ACX_DCGetTimeSheet>(); ++index)
                        dataTable1.Rows.Add((object)myTimeSheet[index].projId, (object)myTimeSheet[index].hour, (object)myTimeSheet[index].minutes, (object)myTimeSheet[index].projLineProtId, (object)myTimeSheet[index].projCategoryId, (object)myTimeSheet[index].categoryName, (object)myTimeSheet[index].transationId);
                    DataTable dataTable2 = new DataTable();
                    this.gvbidhistory.DataSource = (object)dataTable1;
                    this.gvbidhistory.DataBind();
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void gvbidhistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!(e.CommandName == "Status"))
                return;
            //e.CommandArgument.ToString();
            GridViewRow gvRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lbTransId = (Label)gvRow.FindControl("lblCountry");
            this.Session["BIDIDHISTORY"] = lbTransId.Text;
            ((LinkButton)((Control)e.CommandSource).NamingContainer.FindControl("btnDelete")).CommandArgument.ToString();
            
            this.Response.Redirect("EditTimesheet.aspx");
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
            //string text = this.gvbidhistory.SelectedRow.Cells[0].Text;
            this.Session["BIDIDHISTORY"] = (object)(this.gvbidhistory.SelectedRow.FindControl("lblCountry") as Label).Text;
            this.Response.Redirect("EditTimesheet.aspx");
        }
    }
}
