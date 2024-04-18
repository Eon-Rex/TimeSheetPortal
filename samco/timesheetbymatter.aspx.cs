using AjaxControlToolkit;
using Newtonsoft.Json;
using samco.ServiceReference3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace samco.CBol
{
    public partial class timesheetbymatter : System.Web.UI.Page
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
            this.customername.Attributes.Add("readonly", "readonly");
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

                string _EmployeeId = str;
                var employeeId = new { _EmployeeId = _EmployeeId };
                
                string RetrieveManagerProjIds = "RetrieveManagerProjIds";
                string jsonstring1 = (string)await D365Call.PostD365Service(RetrieveManagerProjIds, employeeId);
                ACX_DCProjList[] acxDcProjListArray = JsonConvert.DeserializeObject<ACX_DCProjList[]>(jsonstring1);


                //ACX_DCProjList[] acxDcProjListArray = customeServiceClient.RetrieveManagerProjIds(CallContext, _EmployeeId);
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Code", typeof(string));
                dataTable.Columns.Add("Value", typeof(string));
                dataTable.Rows.Add((object)"-1", (object)"--select--");
                for (int index = 0; index < ((IEnumerable<ACX_DCProjList>)acxDcProjListArray).Count<ACX_DCProjList>(); ++index)
                    dataTable.Rows.Add((object)acxDcProjListArray[index].projId, (object)(acxDcProjListArray[index].projId + "-" + acxDcProjListArray[index].projName));
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

        protected async void imgsave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (this.DropDownList1.SelectedValue == "-1")
                {
                    ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Search Matter.....');", true);
                    this.DropDownList1.Focus();
                }
                else if (this.txtworkdate.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Date.....');", true);
                    this.txtworkdate.Focus();
                }
                else
                {
                    // For Dev
                    //DateTime dateTime1 = Convert.ToDateTime(Convert.ToDateTime(txtworkdate.Text).ToString("dd/MM/yyyy"));
                    //DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(txtTodate.Text).ToString("dd/MM/yyyy"));

                    // For IIS
                    DateTime dateTime1 = Convert.ToDateTime(DateTime.ParseExact(this.txtworkdate.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
                    DateTime dateTime2 = Convert.ToDateTime(DateTime.ParseExact(this.txtTodate.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"));
                    string str = this.Session["UserId"].ToString();
                    //string str = Request.Cookies["UserID"]?.Value;
                    string selectedValue = this.DropDownList1.SelectedValue;
                    MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
                    customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
                    customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
                    customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
                    samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
                    CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString(); 

                    DateTime _fromDate = dateTime1;
                    DateTime _toDate = dateTime2;
                    string _Manager = str;
                    string _projid = selectedValue;

                    var objectPass = new
                    {
                        _fromDate = _fromDate  ,
                        _toDate = _toDate,
                        _Manager = _Manager,
                        _projid = _projid
                    };


                    string ReviewTimeSheetByMatter = "ReviewTimeSheetByMatter";
                    string jsonstring1 = (string)await D365Call.PostD365Service(ReviewTimeSheetByMatter, objectPass);
                    ACX_DCReviewTimeSheetByMatter[] timeSheetByMatterArray = JsonConvert.DeserializeObject<ACX_DCReviewTimeSheetByMatter[]>(jsonstring1);


                    //ACX_DCReviewTimeSheetByMatter[] timeSheetByMatterArray = customeServiceClient.ReviewTimeSheetByMatter(CallContext, _fromDate, _toDate, _Manager, _projid);
                    DataTable dataTable1 = new DataTable();
                    dataTable1.Columns.Add("projId", typeof(string));
                    dataTable1.Columns.Add("hour", typeof(int));
                    dataTable1.Columns.Add("minutes", typeof(int));
                    dataTable1.Columns.Add("projLineProtId", typeof(string));
                    dataTable1.Columns.Add("projCategoryId", typeof(string));
                    dataTable1.Columns.Add("categoryName", typeof(string));
                    dataTable1.Columns.Add("transationId", typeof(string));
                    for (int index = 0; index < ((IEnumerable<ACX_DCReviewTimeSheetByMatter>)timeSheetByMatterArray).Count<ACX_DCReviewTimeSheetByMatter>(); ++index)
                        dataTable1.Rows.Add((object)timeSheetByMatterArray[index].projId, (object)timeSheetByMatterArray[index].hour, (object)timeSheetByMatterArray[index].minutes, (object)timeSheetByMatterArray[index].projLineProtId, (object)timeSheetByMatterArray[index].projCategoryId, (object)timeSheetByMatterArray[index].categoryName, (object)timeSheetByMatterArray[index].transationId);
                    DataTable dataTable2 = new DataTable();
                    this.gvbidhistory.DataSource = (object)dataTable1;
                    this.gvbidhistory.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected async void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = this.DropDownList1.SelectedValue;
            string text = this.DropDownList1.SelectedItem.Text;
            string str = this.Session["UserId"].ToString();
            //string str = Request.Cookies["UserID"]?.Value;

            MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();

            customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
            samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
            CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString(); 

            string _projId = selectedValue;
            string _EmployeeId = str;

            var objectPass = new
            {
                _projId = _projId,
                _EmployeeId = _EmployeeId
            };

            string RetrieveProjDetail = "RetrieveProjDetail";
            string jsonstring1 = (string)await D365Call.PostD365Service(RetrieveProjDetail, objectPass);
            ACX_DCProjDetail[] acxDcProjDetailArray = JsonConvert.DeserializeObject<ACX_DCProjDetail[]>(jsonstring1);

           // ACX_DCProjDetail[] acxDcProjDetailArray = customeServiceClient.RetrieveProjDetail(CallContext, _projId, _EmployeeId);
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("CustomerName", typeof(string));
            dataTable1.Columns.Add("NPGCode", typeof(string));
            dataTable1.Columns.Add("NPGSectionCode", typeof(string));
            dataTable1.Columns.Add("emplNPGCode", typeof(string));
            dataTable1.Columns.Add("emplNPGSection", typeof(string));
            dataTable1.Columns.Add("linePropertyId", typeof(string));
            for (int index = 0; index < ((IEnumerable<ACX_DCProjDetail>)acxDcProjDetailArray).Count<ACX_DCProjDetail>(); ++index)
                dataTable1.Rows.Add((object)acxDcProjDetailArray[index].custName, (object)acxDcProjDetailArray[index].projNPGCode, (object)acxDcProjDetailArray[index].projNPGSection, (object)acxDcProjDetailArray[index].emplNPGCode, (object)acxDcProjDetailArray[index].emplNPGSection, (object)acxDcProjDetailArray[index].linePropertyId);
            DataTable dataTable2 = new DataTable();
            this.customername.Text = dataTable1.Rows[0]["CustomerName"].ToString();
        }

        protected void lbkSignOut_Click(object sender, EventArgs e)
        {
            this.Session.Abandon();
            
            this.Response.Redirect("default.aspx");
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
