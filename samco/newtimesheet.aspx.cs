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
    public partial class newtimesheet : System.Web.UI.Page
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
            this.customername.Attributes.Add("readonly", "readonly");
            this.txttimekeeper.Attributes.Add("readonly", "readonly");
            
           
            try
            {
                this.txttimekeeper.Text = this.Session["Username"].ToString();
                string _EmployeeId = this.Session["UserId"].ToString();
                
                //string _EmployeeId = "Mushahid.khan";

                MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
                samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
                CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();


                var employeeId = new { _EmployeeId = _EmployeeId };

                try
                {
                    string RetrieveProjIdsnew = "RetrieveProjIds";
                    string jsonstring1 = (string)await D365Call.PostD365Service(RetrieveProjIdsnew, employeeId);
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

                }
                catch (Exception ex)
                {

                }
                
                ////For bind data Category

                //ACX_DCCategoryList[] acxDcCategoryListArray = customeServiceClient.RetrieveProjCategoryIds(CallContext,"");
                //DataTable dataTable2 = new DataTable();
                //dataTable2.Columns.Add("CategoryName", typeof(string));
                //dataTable2.Columns.Add("Value1", typeof(string));
                //dataTable2.Rows.Add((object)"-1", (object)"--select--");
                //for (int index = 0; index < ((IEnumerable<ACX_DCCategoryList>)acxDcCategoryListArray).Count<ACX_DCCategoryList>(); ++index)
                //    dataTable2.Rows.Add((object)acxDcCategoryListArray[index].projCategoryId, (object)acxDcCategoryListArray[index].projCategoryName);
                //this.DropDownList3.DataSource = (object)dataTable2;
                //this.DropDownList3.DataTextField = "Value1";
                //this.DropDownList3.DataValueField = "CategoryName";
                //this.DropDownList3.DataBind();

                ////

                string RetrieveNewLinePropertyIds = "RetrieveNewLinePropertyIds";
                string jsonstring2 = (string)await D365Call.PostD365Service(RetrieveNewLinePropertyIds, null);
                ACX_DCLinePropertyList[] linePropertyListArray = JsonConvert.DeserializeObject<ACX_DCLinePropertyList[]>(jsonstring2);

                //ACX_DCLinePropertyList[] linePropertyListArray = customeServiceClient.RetrieveNewLinePropertyIds(CallContext);
                DataTable dataTable3 = new DataTable();
                dataTable3.Columns.Add("LineName", typeof(string));
                dataTable3.Columns.Add("Value2", typeof(string));
                dataTable3.Rows.Add((object)"-1", (object)"--select--");
                for (int index = 0; index < ((IEnumerable<ACX_DCLinePropertyList>)linePropertyListArray).Count<ACX_DCLinePropertyList>(); ++index)
                    dataTable3.Rows.Add((object)linePropertyListArray[index].projLineProtId, (object)linePropertyListArray[index].projLineProtName);
                this.DropDownList4.DataSource = (object)dataTable3;
                this.DropDownList4.DataTextField = "Value2";
                this.DropDownList4.DataValueField = "LineName";
                this.DropDownList4.DataBind();


                string RetrieveNPGCodeIds = "RetrieveNPGCodeIds";
                string jsonstring3 = (string)await D365Call.PostD365Service(RetrieveNPGCodeIds, employeeId);
                ACX_DCNPGCodeList[] acxDcnpgCodeListArray = JsonConvert.DeserializeObject<ACX_DCNPGCodeList[]>(jsonstring3);
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
                this.Label2.Text = "Your Session Expired";
            }
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

            //string _EmployeeId = "Mushahid.khan";

            MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
            samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
            CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();


            //For bind data Category
            var projId = new
            {
                _projId=selectedValue 
            };

            string RetrieveProjCategoryIds = "RetrieveProjCategoryIds";
            string jsonstring4 = (string)await D365Call.PostD365Service(RetrieveProjCategoryIds, projId);

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
            var RetrieveProjDetailobj = new
            {
                _projId = selectedValue,
                _EmployeeId = _EmployeeId
            };

            string RetrieveProjDetail = "RetrieveProjDetail";
            string jsonstring5 = (string)await D365Call.PostD365Service(RetrieveProjDetail, RetrieveProjDetailobj);
            ACX_DCProjDetail[] acxDcProjDetailArray = JsonConvert.DeserializeObject<ACX_DCProjDetail[]>(jsonstring5);

            //ACX_DCProjDetail[] acxDcProjDetailArray = customeServiceClient.RetrieveProjDetail(CallContext, selectedValue, _EmployeeId);
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
            DataTable dataTable3 = dataTable1;
            this.customername.Text = dataTable3.Rows[0]["CustomerName"].ToString();
            this.DropDownList4.SelectedValue = dataTable3.Rows[0]["linePropertyId"].ToString();
            string str1 = dataTable3.Rows[0]["emplNPGCode"].ToString();

            //string str1 = dataTable3.Rows[0]["NPGSectionCode"].ToString();
            if (str1 != "" && str1 != "0")
                this.DropDownList5.SelectedValue = dataTable3.Rows[0]["emplNPGCode"].ToString();
            dataTable3.Rows[0]["emplNPGCode"].ToString();
            string _NPGCode = dataTable3.Rows[0]["emplNPGCode"].ToString();
            dataTable3.Rows[0]["emplNPGSection"].ToString();
            dataTable3.Rows[0]["NPGCode"].ToString();
            dataTable3.Rows[0]["NPGSectionCode"].ToString();

            var RetrieveNPGSectionCodeIdsobj = new
            {
                _NPGCode = _NPGCode
            };

            string RetrieveNPGSectionCodeIds = "RetrieveNPGSectionCodeIds";
            string jsonstring6 = (string)await D365Call.PostD365Service(RetrieveNPGSectionCodeIds, RetrieveNPGSectionCodeIdsobj);
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
            string str2 = dataTable3.Rows[0]["emplNPGSection"].ToString();
            if (!(str2 != "") || !(str2 != "0"))
                return;
            this.DropDownList6.SelectedValue = dataTable3.Rows[0]["emplNPGSection"].ToString();
        }

        protected async void imgsave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.txtworkdate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Work Date.....');", true);
                this.txtworkdate.Focus();
            }
            else if (this.DropDownList1.SelectedValue == "-1")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select ProjectId.....');", true);
                this.DropDownList1.Focus();
            }
            else if (this.DropDownList3.SelectedValue == "-1")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Category.....');", true);
                this.DropDownList3.Focus();
            }
            else if (this.DropDownList4.SelectedValue == "-1")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Line Property.....');", true);
                this.DropDownList4.Focus();
            }
            else if (this.txthours.SelectedItem.Text == "Select")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Hours.....');", true);
                this.txthours.Focus();
            }
            else if (this.DropDownList2.SelectedItem.Text == "Select")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Select Minutes.....');", true);
                this.DropDownList2.Focus();
            }
            else if (this.txtdis.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('Write Some Description.....');", true);
                this.txtdis.Focus();
            }
            else
            {
                MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
                customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
                samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
                CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();

                ACX_DCSaveTimeSheet acxDcSaveTimeSheet = new ACX_DCSaveTimeSheet();

                
                //acxDcSaveTimeSheet.employeeId = "Mushahid.khan";
                acxDcSaveTimeSheet.employeeId = this.Session["UserId"].ToString();
                //string str = Convert.ToDateTime(this.txtworkdate.Text).ToString("dd/MM/yyyy");
                //       //IIS
                //       //string str = DateTime.ParseExact(this.txtworkdate.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                //string str = DateTime.ParseExact(this.txtworkdate.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");//iis
                //acxDcSaveTimeSheet.workDate = Convert.ToDateTime(this.txtworkdate.Text);

                DateTime parsedDate = DateTime.ParseExact(this.txtworkdate.Text, "dd/MM/yyyy", (IFormatProvider)CultureInfo.InvariantCulture);
                acxDcSaveTimeSheet.workDate = parsedDate;

                string selectedValue = this.DropDownList1.SelectedValue;
                string text = this.DropDownList1.SelectedItem.Text;
                acxDcSaveTimeSheet.projId = this.DropDownList1.SelectedValue;
                acxDcSaveTimeSheet.projCategoryId = this.DropDownList3.SelectedValue;
                acxDcSaveTimeSheet.projLineProtId = this.DropDownList4.SelectedValue;
                acxDcSaveTimeSheet.hour = Convert.ToInt32(this.txthours.SelectedValue);
                acxDcSaveTimeSheet.minutes = Convert.ToInt32(this.DropDownList2.SelectedValue);
                acxDcSaveTimeSheet.npgCode = this.DropDownList5.SelectedValue;
                acxDcSaveTimeSheet.npgSectionId = this.DropDownList6.SelectedValue;
                acxDcSaveTimeSheet.narration = this.txtdis.Text;
                // CallContext CallContext = callContext;
                ACX_DCSaveTimeSheet _DCSaveTimeSheet = acxDcSaveTimeSheet;
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

                SaveRecords resultdata = response.FirstOrDefault();
                //string resultdata = response.message;


                if (resultdata.status == "true") {
                    ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('" + "TimeSheet Entery Created" + "');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('" + resultdata.message + "');", true);
                }
                //ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('" + customeServiceClient.SaveTimeSheet(CallContext, _DCSaveTimeSheet).message + "');", true);
                this.clear();
            }
        }

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

            var npgcode = new
            {
                _NPGCode = _NPGCode
            };
            string RetrieveNPGSectionCodeIds = "RetrieveNPGSectionCodeIds";

            string jsonstring6 = (string)await D365Call.PostD365Service(RetrieveNPGSectionCodeIds, npgcode);
            ACX_DCNPGSectionCodeList[] dcnpgSectionCodeListArray = JsonConvert.DeserializeObject<ACX_DCNPGSectionCodeList[]>(jsonstring6);


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

        protected async void DropDownList5_SelectedIndexChanged1(object sender, EventArgs e)
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
            var npgcode = new {
                _NPGCode = _NPGCode
            };
            string RetrieveNPGSectionCodeIds = "RetrieveNPGSectionCodeIds";

            string jsonstring6 = (string)await D365Call.PostD365Service(RetrieveNPGSectionCodeIds, npgcode);
            ACX_DCNPGSectionCodeList[] dcnpgSectionCodeListArray = JsonConvert.DeserializeObject<ACX_DCNPGSectionCodeList[]>(jsonstring6);


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

        protected async void Button1_Click(object sender, EventArgs e)
        {
            this.txttimekeeper.Text = this.Session["Username"].ToString();
            //string _EmployeeId = "Mushahid.khan";
            string _EmployeeId = this.Session["UserId"].ToString();

            string text = this.TextBox1.Text;
            MobileCustomeServiceClient customeServiceClient = new MobileCustomeServiceClient();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
            customeServiceClient.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
            samco.ServiceReference3.CallContext CallContext = new samco.ServiceReference3.CallContext();
            CallContext.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();

            var searchProjectobj = new {
                _ProjId = text,
                _EmployeeId = _EmployeeId
            };
            var searchProjectobjelse = new {
                _EmployeeId = _EmployeeId
            };
            string searchProject = "searchProject";
            string jsonstring = (string)await D365Call.PostD365Service(searchProject, searchProjectobj);
            ACX_DCProjListSearch[] dcProjListSearchArray = JsonConvert.DeserializeObject<ACX_DCProjListSearch[]>(jsonstring);


            //ACX_DCProjListSearch[] dcProjListSearchArray = customeServiceClient.searchProject(CallContext, text, _EmployeeId);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Code", typeof(string));
            dataTable.Columns.Add("Value", typeof(string));
            dataTable.Rows.Add((object)"-1", (object)"--select--");
            for (int index = 0; index < ((IEnumerable<ACX_DCProjListSearch>)dcProjListSearchArray).Count<ACX_DCProjListSearch>(); ++index)
                dataTable.Rows.Add((object)dcProjListSearchArray[index].projId, (object)(dcProjListSearchArray[index].projId + "-" + dcProjListSearchArray[index].projName));
            string str1 = dataTable.Rows[1]["Code"].ToString();
            string str2 = dataTable.Rows[1]["Value"].ToString();
            string str3 = "No such records exits!";
            string str4 = "Records Exits and Added to list";
            string str5 = "";
            if (str1 == str5 && str2 == "-")
            {
                ScriptManager.RegisterClientScriptBlock((Control)this.Button1, this.GetType(), "A Key", "alert('" + str3 + "');", true);
            }
            else
            {
                string RetrieveProjIdsNew = "RetrieveProjIds";
                string jsonstring1 = (string)await D365Call.PostD365Service(RetrieveProjIdsNew, searchProjectobjelse);
                ACX_DCProjList[] acxDcProjListArray = JsonConvert.DeserializeObject<ACX_DCProjList[]>(jsonstring1);


                //ACX_DCProjList[] acxDcProjListArray = customeServiceClient.RetrieveProjIdsNew(CallContext, _EmployeeId);

                for (int index = 0; index < ((IEnumerable<ACX_DCProjList>)acxDcProjListArray).Count<ACX_DCProjList>(); ++index)
                    dataTable.Rows.Add((object)acxDcProjListArray[index].projId, (object)(acxDcProjListArray[index].projId + "-" + acxDcProjListArray[index].projName));
                this.DropDownList1.DataSource = (object)dataTable;
                this.DropDownList1.DataTextField = "Value";
                this.DropDownList1.DataValueField = "Code";
                this.DropDownList1.DataBind();
                ScriptManager.RegisterClientScriptBlock((Control)this.Button1, this.GetType(), "A Key", "alert('" + str4 + "');", true);
            }
        }
    }

    public class SaveRecords
    {
        public string message { get; set; }
        public string status { get; set; }
    }



}