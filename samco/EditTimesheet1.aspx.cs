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
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace samco
{
    public partial class EditTimesheet1 : System.Web.UI.Page
    {
        samco.D365ServiceRefrence.D365ServiceCall D365Call = new D365ServiceRefrence.D365ServiceCall();
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
            this.customername.Attributes.Add("readonly", "readonly");
            this.txttimekeeper.Attributes.Add("readonly", "readonly");
            try
            {
                this.txttimekeeper.Text = this.Session["Username"].ToString();
                string _EmployeeId = this.Session["UserId"].ToString();
                MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
                samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
                CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();

                var employeeId = new { _EmployeeId = _EmployeeId };

                string RetrieveProjIdsNew = "RetrieveProjIdsNew";
                string jsonstring1 = (string)await D365Call.PostD365Service(RetrieveProjIdsNew, employeeId);
                ACX_DCProjList[] acxDcProjListArray = JsonConvert.DeserializeObject<ACX_DCProjList[]>(jsonstring1);

                //ACX_DCProjList[] acxDcProjListArray = customeServiceClient.RetrieveProjIdsNew(CallContext, _EmployeeId);
                DataTable dataTable1 = new DataTable();
                dataTable1.Columns.Add("Code", typeof(string));
                dataTable1.Columns.Add("Value", typeof(string));
                dataTable1.Rows.Add((object)"-1", (object)"--select--");
                for (int index = 0; index < ((IEnumerable<ACX_DCProjList>)acxDcProjListArray).Count<ACX_DCProjList>(); ++index)
                    dataTable1.Rows.Add((object)acxDcProjListArray[index].projId, (object)(acxDcProjListArray[index].projId + "-" + acxDcProjListArray[index].projName));
                this.DropDownList1.DataSource = (object)dataTable1;
                this.DropDownList1.DataTextField = "Value";
                this.DropDownList1.DataValueField = "Code";
                this.DropDownList1.DataBind();

                ////For Category
                ///
                var RPI = new { _projId = string.Empty };
                string RetrieveProjCategoryIds = "RetrieveProjCategoryIds";
                string jsonstring2 = (string)await D365Call.PostD365Service(RetrieveProjCategoryIds, RPI);
                ACX_DCCategoryList[] acxDcCategoryListArray = JsonConvert.DeserializeObject<ACX_DCCategoryList[]>(jsonstring2);

                //ACX_DCCategoryList[] acxDcCategoryListArray = customeServiceClient.RetrieveProjCategoryIds(CallContext, "");
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add("CategoryName", typeof(string));
                dataTable2.Columns.Add("Value1", typeof(string));
                dataTable2.Rows.Add((object)"-1", (object)"--select--");
                for (int index = 0; index < ((IEnumerable<ACX_DCCategoryList>)acxDcCategoryListArray).Count<ACX_DCCategoryList>(); ++index)
                    dataTable2.Rows.Add((object)acxDcCategoryListArray[index].projCategoryId, (object)acxDcCategoryListArray[index].projCategoryName);
                this.DropDownList3.DataSource = (object)dataTable2;
                this.DropDownList3.DataTextField = "Value1";
                this.DropDownList3.DataValueField = "CategoryName";
                this.DropDownList3.DataBind();
                ////

                string RetrieveLinePropertyIds = "RetrieveLinePropertyIds";
                string jsonstring3 = (string)await D365Call.PostD365Service(RetrieveLinePropertyIds, null);
                ACX_DCLinePropertyList[] linePropertyListArray = JsonConvert.DeserializeObject<ACX_DCLinePropertyList[]>(jsonstring3);

                //ACX_DCLinePropertyList[] linePropertyListArray = customeServiceClient.RetrieveLinePropertyIds(CallContext);
                DataTable dataTable3 = new DataTable();
                dataTable3.Columns.Add("LineName", typeof(string));
                dataTable3.Columns.Add("Value2", typeof(string));
                dataTable3.Rows.Add((object)"-1", (object)"--select--");
                for (int index = 0; index < ((IEnumerable<ACX_DCLinePropertyList>)linePropertyListArray).Count<ACX_DCLinePropertyList>(); ++index)
                    dataTable3.Rows.Add((object)linePropertyListArray[index].projLineProtId, (object)linePropertyListArray[index].projLineProtName);
                // this.DropDownList4.DataSource = (object)dataTable3;
                //this.DropDownList4.DataTextField = "Value2";
                //this.DropDownList4.DataValueField = "LineName";
                //this.DropDownList4.DataBind();
                this.BindEditsection();

                string RetrieveNPGCodeIds = "RetrieveNPGCodeIds";
                string jsonstring4 = (string)await D365Call.PostD365Service(RetrieveNPGCodeIds, employeeId);
                ACX_DCNPGCodeList[] acxDcnpgCodeListArray = JsonConvert.DeserializeObject<ACX_DCNPGCodeList[]>(jsonstring4);

                //ACX_DCNPGCodeList[] acxDcnpgCodeListArray = customeServiceClient.RetrieveNPGCodeIds(CallContext, _EmployeeId);
                DataTable dataTable4 = new DataTable();
                dataTable4.Columns.Add("LineName1", typeof(string));
                dataTable4.Columns.Add("Value3", typeof(string));
                dataTable4.Rows.Add((object)"-1", (object)"--select--");
                for (int index = 0; index < ((IEnumerable<ACX_DCNPGCodeList>)acxDcnpgCodeListArray).Count<ACX_DCNPGCodeList>(); ++index)
                    dataTable4.Rows.Add((object)acxDcnpgCodeListArray[index].npgCode, (object)acxDcnpgCodeListArray[index].npgName);
                this.DropDownList5.DataTextField = "Value3";
                this.DropDownList5.DataValueField = "LineName1";
                this.DropDownList5.DataSource = (object)dataTable4;
                this.DropDownList5.DataBind();
            }
            catch (Exception ex)
            {
                this.Label4.Text = "Your Session Expired";
            }
        }




        public async void fillCategory()
        {
            MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
            samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
            CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();
            ////For Category
            ///
            var Object = new { _projId = DropDownList1.SelectedItem.Value };

            string RetrieveProjCategoryIds = "RetrieveProjCategoryIds";
            string jsonstring4 = (string)await D365Call.PostD365Service(RetrieveProjCategoryIds, Object);
            ACX_DCCategoryList[] acxDcCategoryListArray = JsonConvert.DeserializeObject<ACX_DCCategoryList[]>(jsonstring4);

            //ACX_DCCategoryList[] acxDcCategoryListArray = customeServiceClient.RetrieveProjCategoryIds(CallContext, DropDownList1.SelectedItem.Value);
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("CategoryName", typeof(string));
            dataTable2.Columns.Add("Value1", typeof(string));
            dataTable2.Rows.Add((object)"-1", (object)"--select--");
            for (int index = 0; index < ((IEnumerable<ACX_DCCategoryList>)acxDcCategoryListArray).Count<ACX_DCCategoryList>(); ++index)
                dataTable2.Rows.Add((object)acxDcCategoryListArray[index].projCategoryId, (object)acxDcCategoryListArray[index].projCategoryName);
            this.DropDownList3.DataSource = (object)dataTable2;
            this.DropDownList3.DataTextField = "Value1";
            this.DropDownList3.DataValueField = "CategoryName";
            this.DropDownList3.DataBind();
            ////

        }

        public async void BindEditsection()
        {
            string _TransactionId = Convert.ToString(this.Session["BIDIDHISTORY"]);
            this.Label2.Text = _TransactionId;
            MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
            samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
            CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();


            var Object = new { _TransactionId = _TransactionId };
            string EditMyTimeSheet = "EditMyTimeSheet";
            string jsonstring4 = (string)await D365Call.PostD365Service(EditMyTimeSheet, Object);
            ACX_DCEditTimeSheet[] acxDcEditTimeSheetArray = JsonConvert.DeserializeObject<ACX_DCEditTimeSheet[]>(jsonstring4);

            //ACX_DCEditTimeSheet[] acxDcEditTimeSheetArray = customeServiceClient.EditMyTimeSheet(CallContext, _TransactionId);
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("WORKDATE", typeof(DateTime));
            dataTable1.Columns.Add("PROJID", typeof(string));
            dataTable1.Columns.Add("CUSTNAME", typeof(string));
            dataTable1.Columns.Add("PROJCATID", typeof(string));
            dataTable1.Columns.Add("PROJLINEID", typeof(string));
            dataTable1.Columns.Add("HOUR", typeof(int));
            dataTable1.Columns.Add("MINUTES", typeof(int));
            dataTable1.Columns.Add("NPGCODE", typeof(string));
            dataTable1.Columns.Add("NPGSECTIONID", typeof(string));
            dataTable1.Columns.Add("DESCRISION", typeof(string));
            for (int index = 0; index < ((IEnumerable<ACX_DCEditTimeSheet>)acxDcEditTimeSheetArray).Count<ACX_DCEditTimeSheet>(); ++index)
                dataTable1.Rows.Add((object)acxDcEditTimeSheetArray[index].workDate, (object)acxDcEditTimeSheetArray[index].projId, (object)acxDcEditTimeSheetArray[index].custName, (object)acxDcEditTimeSheetArray[index].projCategoryId, (object)acxDcEditTimeSheetArray[index].projLineProtId, (object)acxDcEditTimeSheetArray[index].hour, (object)acxDcEditTimeSheetArray[index].minutes, (object)acxDcEditTimeSheetArray[index].emplnpgCode, (object)acxDcEditTimeSheetArray[index].npgemplSectionId, (object)acxDcEditTimeSheetArray[index].narration);
            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = dataTable1;
            this.DropDownList1.SelectedValue = dataTable3.Rows[0]["PROJID"].ToString();
            //bind Category...modified by amol 10-Apr-2017
            fillCategory();
            this.customername.Text = dataTable3.Rows[0]["CUSTNAME"].ToString();
            this.DropDownList3.SelectedValue = dataTable3.Rows[0]["PROJCATID"].ToString();
            this.DropDownList4.SelectedValue = dataTable3.Rows[0]["PROJLINEID"].ToString();
            this.txthours.SelectedValue = dataTable3.Rows[0]["HOUR"].ToString();
            this.DropDownList2.SelectedValue = dataTable3.Rows[0]["MINUTES"].ToString();
            string _NPGCode = dataTable3.Rows[0]["NPGCODE"].ToString();
            if (_NPGCode != "" && _NPGCode != "0")
                this.DropDownList5.SelectedValue = dataTable3.Rows[0]["NPGCODE"].ToString();

            var Object1 = new { _NPGCode = _NPGCode };
            string RetrieveNPGSectionCodeIds = "RetrieveNPGSectionCodeIds";
            string jsonstring5 = (string)await D365Call.PostD365Service(RetrieveNPGSectionCodeIds, Object1);
            ACX_DCNPGSectionCodeList[] dcnpgSectionCodeListArray = JsonConvert.DeserializeObject<ACX_DCNPGSectionCodeList[]>(jsonstring5);

            //ACX_DCNPGSectionCodeList[] dcnpgSectionCodeListArray = customeServiceClient.RetrieveNPGSectionCodeIds(CallContext, _NPGCode);
            DataTable dataTable4 = new DataTable();
            dataTable4.Columns.Add("LineName11", typeof(string));
            dataTable4.Columns.Add("Value31", typeof(string));
            dataTable4.Rows.Add((object)"-1", (object)"--select--");
            for (int index = 0; index < ((IEnumerable<ACX_DCNPGSectionCodeList>)dcnpgSectionCodeListArray).Count<ACX_DCNPGSectionCodeList>(); ++index)
                dataTable4.Rows.Add((object)dcnpgSectionCodeListArray[index].npgSectionId, (object)dcnpgSectionCodeListArray[index].npgSectionName);
            this.DropDownList6.DataTextField = "Value31";
            this.DropDownList6.DataValueField = "LineName11";
            this.DropDownList6.DataSource = (object)dataTable4;
            this.DropDownList6.DataBind();
            string str = dataTable3.Rows[0]["NPGSECTIONID"].ToString();
            if (str != "" && str != "0")
                this.DropDownList6.SelectedValue = dataTable3.Rows[0]["NPGSECTIONID"].ToString();
            this.txtdis.Text = dataTable3.Rows[0]["DESCRISION"].ToString();
            this.txtworkdate.Text = Convert.ToDateTime(dataTable3.Rows[0]["WORKDATE"].ToString()).ToString("dd/MM/yyyy");
        }

        protected void lbkSignOut_Click(object sender, EventArgs e)
        {
            this.Session.Abandon();
            this.Response.Redirect("default.aspx");
        }

        protected async void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = this.DropDownList1.SelectedValue;
            string text = this.DropDownList1.SelectedItem.Text;
            string _EmployeeId = this.Session["UserId"].ToString();
            MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
            samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
            CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();

            //For Category
            var Object = new { _projId = DropDownList1.SelectedItem.Value };
            string RetrieveProjCategoryIds = "RetrieveProjCategoryIds";
            string jsonstring4 = (string)await D365Call.PostD365Service(RetrieveProjCategoryIds, Object);
            ACX_DCCategoryList[] acxDcCategoryListArray = JsonConvert.DeserializeObject<ACX_DCCategoryList[]>(jsonstring4);
            
            //ACX_DCCategoryList[] acxDcCategoryListArray = customeServiceClient.RetrieveProjCategoryIds(CallContext, DropDownList1.SelectedItem.Value);
            DataTable dsCategory = new DataTable();
            dsCategory.Columns.Add("CategoryName", typeof(string));
            dsCategory.Columns.Add("Value1", typeof(string));
            dsCategory.Rows.Add((object)"-1", (object)"--select--");
            for (int index = 0; index < ((IEnumerable<ACX_DCCategoryList>)acxDcCategoryListArray).Count<ACX_DCCategoryList>(); ++index)
                dsCategory.Rows.Add((object)acxDcCategoryListArray[index].projCategoryId, (object)acxDcCategoryListArray[index].projCategoryName);
            this.DropDownList3.DataSource = (object)dsCategory;
            this.DropDownList3.DataTextField = "Value1";
            this.DropDownList3.DataValueField = "CategoryName";
            this.DropDownList3.DataBind();
            //

            var Object2 = new { _projId = selectedValue, _EmployeeId = _EmployeeId };
            string RetrieveProjDetail = "RetrieveProjDetail";
            string jsonstring5 = (string)await D365Call.PostD365Service(RetrieveProjDetail, Object2);
            ACX_DCProjDetail[] acxDcProjDetailArray = JsonConvert.DeserializeObject<ACX_DCProjDetail[]>(jsonstring5);

            //ACX_DCProjDetail[] acxDcProjDetailArray = customeServiceClient.RetrieveProjDetail(CallContext, selectedValue, _EmployeeId);
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("CustomerName", typeof(string));
            dataTable1.Columns.Add("NPGCode", typeof(string));
            dataTable1.Columns.Add("NPGSectionCode", typeof(string));
            dataTable1.Columns.Add("linePropertyId", typeof(string));
            for (int index = 0; index < ((IEnumerable<ACX_DCProjDetail>)acxDcProjDetailArray).Count<ACX_DCProjDetail>(); ++index)
                dataTable1.Rows.Add((object)acxDcProjDetailArray[index].custName, (object)acxDcProjDetailArray[index].emplNPGCode, (object)acxDcProjDetailArray[index].emplNPGSection, (object)acxDcProjDetailArray[index].linePropertyId);

            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = dataTable1;
            this.customername.Text = dataTable3.Rows[0]["CustomerName"].ToString();
            this.DropDownList4.SelectedValue = dataTable3.Rows[0]["linePropertyId"].ToString();
            string _NPGCode = dataTable3.Rows[0]["NPGCode"].ToString();

            if (_NPGCode != "" && _NPGCode != "0")
                this.DropDownList5.SelectedValue = dataTable3.Rows[0]["NPGCode"].ToString();
            var Object1 = new { _NPGCode = _NPGCode };
            string RetrieveNPGSectionCodeIds = "RetrieveNPGSectionCodeIds";
            string jsonstring6 = (string)await D365Call.PostD365Service(RetrieveNPGSectionCodeIds, Object1);
            ACX_DCNPGSectionCodeList[] dcnpgSectionCodeListArray = JsonConvert.DeserializeObject<ACX_DCNPGSectionCodeList[]>(jsonstring6);

            //ACX_DCNPGSectionCodeList[] dcnpgSectionCodeListArray = customeServiceClient.RetrieveNPGSectionCodeIds(CallContext, _NPGCode);
            DataTable dataTable4 = new DataTable();
            dataTable4.Columns.Add("LineName11", typeof(string));
            dataTable4.Columns.Add("Value31", typeof(string));
            dataTable4.Rows.Add((object)"-1", (object)"--select--");
            for (int index = 0; index < ((IEnumerable<ACX_DCNPGSectionCodeList>)dcnpgSectionCodeListArray).Count<ACX_DCNPGSectionCodeList>(); ++index)
                dataTable4.Rows.Add((object)dcnpgSectionCodeListArray[index].npgSectionId, (object)dcnpgSectionCodeListArray[index].npgSectionName);
            this.DropDownList6.DataTextField = "Value31";
            this.DropDownList6.DataValueField = "LineName11";
            this.DropDownList6.DataSource = (object)dataTable4;
            this.DropDownList6.DataBind();
            string str = dataTable3.Rows[0]["NPGSectionCode"].ToString();
            if (!(str != "") || !(str != "0"))
                return;
            this.DropDownList6.SelectedValue = dataTable3.Rows[0]["NPGSectionCode"].ToString();
        }

        //protected void imgsave_Click(object sender, ImageClickEventArgs e)
        //{
        //    imgsave.Enabled = false;
        //    if (this.txtworkdate.Text == "")
        //    {
        //        ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Work Date.....');", true);
        //        this.txtworkdate.Focus();
        //    }
        //    else if (this.DropDownList1.SelectedValue == "-1")
        //    {
        //        ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select ProjectId.....');", true);
        //        this.DropDownList1.Focus();
        //    }
        //    else if (this.DropDownList3.SelectedValue == "-1")
        //    {
        //        ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Category.....');", true);
        //        this.DropDownList3.Focus();
        //    }
        //    else if (this.DropDownList4.SelectedValue == "-1")
        //    {
        //        ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Line Property.....');", true);
        //        this.DropDownList4.Focus();
        //    }
        //    else if (this.txthours.SelectedItem.Text == "Select")
        //    {
        //        ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Hours.....');", true);
        //        this.txthours.Focus();
        //    }
        //    else if (this.DropDownList2.SelectedItem.Text == "Select")
        //    {
        //        ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Minutes.....');", true);
        //        this.DropDownList2.Focus();
        //    }
        //    //else if (this.DropDownList5.SelectedValue == "-1")
        //    //{
        //    //    ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select NPGCODE.....');", true);
        //    //    this.DropDownList5.Focus();
        //    //}
        //    //else if (this.DropDownList5.SelectedValue == "-1")
        //    //{
        //    //    ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select NPGSectionId.....');", true);
        //    //    this.DropDownList5.Focus();
        //    //}
        //    else
        //    {
        //        MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
        //        customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
        //        customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
        //        customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
        //        samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
        //        CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();

        //        ACX_DCSaveTimeSheet acxDcSaveTimeSheet = new ACX_DCSaveTimeSheet();
        //        // string str1 = DateTime.ParseExact(this.txtworkdate.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");  // IIS
        //        string str1 = DateTime.ParseExact(this.txtworkdate.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");  // Dev

        //        acxDcSaveTimeSheet.workDate = Convert.ToDateTime(str1);

        //        acxDcSaveTimeSheet.projId = this.DropDownList1.SelectedValue;
        //        string str2 = this.Session["UserId"].ToString();
        //        acxDcSaveTimeSheet.employeeId = str2;
        //        acxDcSaveTimeSheet.projCategoryId = this.DropDownList3.SelectedValue;
        //        acxDcSaveTimeSheet.projLineProtId = this.DropDownList4.SelectedValue;
        //        acxDcSaveTimeSheet.hour = Convert.ToInt32(this.txthours.SelectedValue);
        //        acxDcSaveTimeSheet.minutes = Convert.ToInt32(this.DropDownList2.SelectedValue);
        //        acxDcSaveTimeSheet.npgCode = this.DropDownList5.SelectedValue;
        //        acxDcSaveTimeSheet.npgSectionId = this.DropDownList6.SelectedValue;
        //        acxDcSaveTimeSheet.narration = this.txtdis.Text;
        //        string str3 = Convert.ToString(this.Session["BIDIDHISTORY"]);
        //        acxDcSaveTimeSheet.transationId = str3;

        //        //CallContext CallContext = callContext;
        //        ACX_DCSaveTimeSheet _DCSaveTimeSheet = acxDcSaveTimeSheet;
        //        ACX_ReturnTimeSheetAttributes oresp = new ACX_ReturnTimeSheetAttributes();
        //        oresp = customeServiceClient.SaveTimeSheet(CallContext, _DCSaveTimeSheet);

        //        ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('" + oresp.message + "');", true);
        //        Session["TimesheetUpdateMsg"] = oresp.message;
        //        Response.Redirect("~/mytimesheets.aspx?Status=Success");


        //    }

        //}

        public void clear()
        {
            this.txtworkdate.Text = "";
            this.customername.Text = "";
            this.DropDownList1.SelectedValue = "-1";
            this.DropDownList3.SelectedValue = "-1";
            this.DropDownList4.SelectedValue = "-1";
            this.DropDownList5.SelectedValue = "-1";
            this.DropDownList6.SelectedValue = "-1";
            this.DropDownList2.SelectedIndex = 0;
            this.txthours.SelectedIndex = 0;
            //this.DropDownList2.SelectedItem.Text = "Select";
            //this.txthours.SelectedItem.Text = "Select";
            this.txtdis.Text = "";
        }

        protected async void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = this.DropDownList5.SelectedValue;
            string text = this.DropDownList5.SelectedItem.Text;
            MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
            samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
            CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();

            string _NPGCode = selectedValue;
            var Object1 = new { _NPGCode = _NPGCode };
            string RetrieveNPGSectionCodeIds = "RetrieveNPGSectionCodeIds";
            string jsonstring5 = (string)await D365Call.PostD365Service(RetrieveNPGSectionCodeIds, Object1);
            ACX_DCNPGSectionCodeList[] dcnpgSectionCodeListArray = JsonConvert.DeserializeObject<ACX_DCNPGSectionCodeList[]>(jsonstring5);

            //ACX_DCNPGSectionCodeList[] dcnpgSectionCodeListArray = customeServiceClient.RetrieveNPGSectionCodeIds(CallContext, _NPGCode);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("npgCode1", typeof(string));
            dataTable.Columns.Add("npgName1", typeof(string));
            dataTable.Rows.Add((object)"-1", (object)"--select--");
            for (int index = 0; index < ((IEnumerable<ACX_DCNPGSectionCodeList>)dcnpgSectionCodeListArray).Count<ACX_DCNPGSectionCodeList>(); ++index)
                dataTable.Rows.Add((object)dcnpgSectionCodeListArray[index].npgSectionId, (object)dcnpgSectionCodeListArray[index].npgSectionName);
            this.DropDownList6.DataSource = (object)dataTable;
            this.DropDownList6.DataTextField = "npgName1";
            this.DropDownList6.DataValueField = "npgCode1";
            this.DropDownList6.DataBind();
        }

        protected async void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {

            string _EmployeeId = this.Session["UserId"].ToString();
            MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();

            customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
            samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
            CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();

            string _ProjId = this.DropDownList1.SelectedValue;
            string _LineProperty = this.DropDownList4.SelectedValue;
            string _CategoryId = this.DropDownList3.SelectedItem.Value;
            //customeServiceClient.validateLineProperty(CallContext, _ProjId, _LineProperty, _CategoryId);

            var Object1 = new { _ProjId = _ProjId , _LineProperty = _LineProperty , _CategoryId = _CategoryId };
            string validateLineProperty = "validateLineProperty";
            string Msg = (string)await D365Call.PostD365Service(validateLineProperty, Object1);

            //string Msg = customeServiceClient.validateLineProperty(CallContext, _ProjId, _LineProperty, _CategoryId).message;





            if (Msg != "")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.savedate, this.GetType(), "A Key", "alert('" + Msg + "');", true);
                this.clears();
            }





        }

        public void clears()
        {

            this.DropDownList4.SelectedValue = "-1";





        }

        protected async void savedate_Click(object sender, EventArgs e)
        {
            if (this.txtworkdate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.savedate, this.GetType(), "A Key", "alert('Select Work Date.....');", true);
                this.txtworkdate.Focus();
            }
            else if (this.DropDownList1.SelectedValue == "-1")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.savedate, this.GetType(), "A Key", "alert('Select ProjectId.....');", true);
                this.DropDownList1.Focus();
            }
            else if (this.DropDownList3.SelectedValue == "-1")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.savedate, this.GetType(), "A Key", "alert('Select Category.....');", true);
                this.DropDownList3.Focus();
            }
            else if (this.DropDownList4.SelectedValue == "-1")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.savedate, this.GetType(), "A Key", "alert('Select Line Property.....');", true);
                this.DropDownList4.Focus();
            }
            else if (this.txthours.SelectedItem.Text == "Select")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.savedate, this.GetType(), "A Key", "alert('Select Hours.....');", true);
                this.txthours.Focus();
            }
            else if (this.DropDownList2.SelectedItem.Text == "Select")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.savedate, this.GetType(), "A Key", "alert('Select Minutes.....');", true);
                this.DropDownList2.Focus();
            }
            //else if (this.DropDownList5.SelectedValue == "-1")
            //{
            //    ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select NPGCODE.....');", true);
            //    this.DropDownList5.Focus();
            //}
            //else if (this.DropDownList5.SelectedValue == "-1")
            //{
            //    ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select NPGSectionId.....');", true);
            //    this.DropDownList5.Focus();
            //}
            else
            {
                MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
                samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
                CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();

                ACX_DCSaveTimeSheet acxDcSaveTimeSheet = new ACX_DCSaveTimeSheet();
                //string str1 = DateTime.ParseExact(this.txtworkdate.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("MM/dd/yyyy"); 
                // IIS
                //string str1 = DateTime.ParseExact(this.txtworkdate.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");  // Dev
                //acxDcSaveTimeSheet.workDate = Convert.ToDateTime(str1);
                DateTime parsedDate = DateTime.ParseExact(this.txtworkdate.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                acxDcSaveTimeSheet.workDate = parsedDate;

                acxDcSaveTimeSheet.projId = this.DropDownList1.SelectedValue;
                string str2 = this.Session["UserId"].ToString();
                acxDcSaveTimeSheet.employeeId = str2;
                acxDcSaveTimeSheet.projCategoryId = this.DropDownList3.SelectedValue;
                acxDcSaveTimeSheet.projLineProtId = this.DropDownList4.SelectedValue;
                acxDcSaveTimeSheet.hour = Convert.ToInt32(this.txthours.SelectedValue);
                acxDcSaveTimeSheet.minutes = Convert.ToInt32(this.DropDownList2.SelectedValue);
                acxDcSaveTimeSheet.npgCode = this.DropDownList5.SelectedValue;
                acxDcSaveTimeSheet.npgSectionId = this.DropDownList6.SelectedValue;
                acxDcSaveTimeSheet.narration = this.txtdis.Text;
                string str3 = Convert.ToString(this.Session["BIDIDHISTORY"]);
                acxDcSaveTimeSheet.transationId = str3;

                var jsonData = new
                {
                    _DCSaveTimeSheet = new object[]
                    {
                        acxDcSaveTimeSheet
                    }
                };

                string jsoncontent1 = JsonConvert.SerializeObject(jsonData);
                string SaveTimeSheet = "SaveTimeSheet";

                string result = (string)await D365Call.PostD365Service(SaveTimeSheet, jsonData);

                List<SaveRecords> response = JsonConvert.DeserializeObject<List<SaveRecords>>(result);

                SaveRecords timeSheetAttributes2 = response.FirstOrDefault();
                // string resultdata = response.message;



                //ScriptManager.RegisterClientScriptBlock((Control)this.savedate, this.GetType(), "A Key", "alert('" + resultdata.message + "');", true);


                //CallContext CallContext = callContext;
                //ACX_DCSaveTimeSheet _DCSaveTimeSheet = acxDcSaveTimeSheet;
                //ACX_ReturnTimeSheetAttributes oresp = new ACX_ReturnTimeSheetAttributes();
                //oresp = customeServiceClient.SaveTimeSheet(CallContext, _DCSaveTimeSheet);
                //if (timeSheetAttributes2 != null)
                //{
                //    System.Web.UI.ScriptManager.RegisterClientScriptBlock((Control)this.savedate, this.GetType(), "A Key", "alert('" + timeSheetAttributes2.message + "');", true);
                //}
                //this.Session["TimesheetUpdateMsg"] = (object)timeSheetAttributes2.message;
                //this.Response.Redirect("~/mytimesheets.aspx?Status=Success");
                if (timeSheetAttributes2 != null)
                {
                    string script = "alert('" + timeSheetAttributes2.message + "');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                    string redirectScript = "setTimeout(function() { window.location.href = 'mytimesheets.aspx?Status=Success'; }, 500);"; // 500 milliseconds (2 seconds) delay
                    ScriptManager.RegisterStartupScript(this, GetType(), "RedirectScript", redirectScript, true);
                }
            }
        }
    }
    public class SaveRecords
    {
        public string message { get; set; }
        public string status { get; set; }
    }
}
