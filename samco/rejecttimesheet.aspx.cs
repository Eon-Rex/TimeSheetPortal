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
using System.Collections.Generic;

namespace samco
{
    public partial class rejecttimesheet : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                try
                {

                
                txttimekeeper.Text = Session["Username"].ToString();
                txtworkdate.Attributes.Add("readonly", "readonly");
                txttimekeeper.Attributes.Add("readonly", "readonly");
                DropDownList1.Attributes.Add("readonly", "readonly");
                customername.Attributes.Add("readonly", "readonly");
                DropDownList3.Attributes.Add("readonly", "readonly");
                DropDownList4.Attributes.Add("readonly", "readonly");
                txthours.Attributes.Add("readonly", "readonly");
                DropDownList2.Attributes.Add("readonly", "readonly");
                npgcode.Attributes.Add("readonly", "readonly");
                npgseccode.Attributes.Add("readonly", "readonly");
                txtdis.Attributes.Add("readonly", "readonly");
                BindEditsection();
                }
                catch (Exception ex)
                {
                    
                    Label4.Text = "Your Session Expired";
                }

            }

        }
        public async void BindEditsection()
        {
            string TranId = Convert.ToString(Session["BIDIDHISTORY"]);
            Label2.Text = TranId;
            samco.ServiceReference3.MobileCustomeServiceClient obj = new ServiceReference3.MobileCustomeServiceClient();
            obj.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
            obj.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
            obj.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
            samco.ServiceReference3.CallContext con = new samco.ServiceReference3.CallContext();
            con.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();


            var objectpass = new { _TransactionId = TranId };

            string EditMyTimeSheet = "EditMyTimeSheet";

            
            string jsonstring1 = (string)await D365Call.PostD365Service(EditMyTimeSheet, objectpass);
            samco.ServiceReference3.ACX_DCEditTimeSheet[] CDATA = JsonConvert.DeserializeObject<samco.ServiceReference3.ACX_DCEditTimeSheet[]>(jsonstring1);


            //samco.ServiceReference3.ACX_DCEditTimeSheet[] CDATA = obj.EditMyTimeSheet(con, TranId);

            DataTable CDt = new DataTable();
            CDt.Columns.Add("WORKDATE", typeof(DateTime));
            CDt.Columns.Add("PROJID", typeof(string));
            CDt.Columns.Add("CUSTNAME", typeof(string));
            CDt.Columns.Add("PROJCATID", typeof(string));
            CDt.Columns.Add("PROJLINEID", typeof(string));
            CDt.Columns.Add("HOUR", typeof(int));
            CDt.Columns.Add("MINUTES", typeof(int));
            CDt.Columns.Add("NPGCODE", typeof(string));
            CDt.Columns.Add("NPGSECTIONID", typeof(string));
            CDt.Columns.Add("DESCRISION", typeof(string));
            CDt.Columns.Add("EmployeeId", typeof(string));  
          
            for (int i = 0; i < CDATA.Count(); i++)
            {
                CDt.Rows.Add(CDATA[i].workDate, CDATA[i].projId, CDATA[i].custName, CDATA[i].projCategoryId, CDATA[i].projLineProtId, CDATA[i].hour, CDATA[i].minutes, CDATA[i].emplnpgCode, CDATA[i].npgemplSectionId, CDATA[i].narration,CDATA[i].employeeId);
            }
            DataTable final = new DataTable();
            final = CDt;
            //txtworkdate.Text = final.Rows[0]["WORKDATE"].ToString();
            DropDownList1.Text = final.Rows[0]["PROJID"].ToString();
            customername.Text = final.Rows[0]["CUSTNAME"].ToString();
            DropDownList3.Text = final.Rows[0]["PROJCATID"].ToString();
            DropDownList4.Text = final.Rows[0]["PROJLINEID"].ToString();

            txttimekeeper.Text = final.Rows[0]["EmployeeId"].ToString();
            txthours.Text = final.Rows[0]["HOUR"].ToString();
            DropDownList2.Text = final.Rows[0]["MINUTES"].ToString();
            npgcode.Text = final.Rows[0]["NPGCODE"].ToString();
            npgseccode.Text = final.Rows[0]["NPGSECTIONID"].ToString();
            txtdis.Text = final.Rows[0]["DESCRISION"].ToString();
            DateTime mydate = Convert.ToDateTime(final.Rows[0]["WORKDATE"].ToString());
            string dateToInsert = mydate.ToString("MM/dd/yyyy");
            txtworkdate.Text = dateToInsert;
            
        }
        protected async void Button1_Click(object sender, EventArgs e)
        {
            
            samco.ServiceReference3.MobileCustomeServiceClient obj = new ServiceReference3.MobileCustomeServiceClient();
            obj.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
            obj.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
            obj.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
            samco.ServiceReference3.CallContext con = new samco.ServiceReference3.CallContext();
            con.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();

            samco.ServiceReference3.ACX_DCRejectTimeSheet obj2 = new ServiceReference3.ACX_DCRejectTimeSheet();
            obj2.transationId = Label2.Text;
            obj2.mobileRejectReason = TextBox2.Text;

            string RejectTimeSheet = "RejectTimeSheet";
            var apiobjcall = new
            {
                _DCRejectTimeSheet = obj2
            };
            string result = (string)await D365Call.PostD365Service(RejectTimeSheet, apiobjcall);
            

            List<SaveRecords> response = JsonConvert.DeserializeObject<List<SaveRecords>>(result);

            SaveRecords resultdata = response.FirstOrDefault();
            //string resultdata = response.message;
            
            //ScriptManager.RegisterClientScriptBlock((Control)this.imgsave, this.GetType(), "A Key", "alert('" + resultdata.message + "');", true);
            
            //samco.ServiceReference3.ACX_ReturnTimeSheetAttributes result1 =JsonConvert.DeserializeObject<samco.ServiceReference3.ACX_ReturnTimeSheetAttributes>(jsonstring);
            //samco.ServiceReference3.ACX_ReturnTimeSheetAttributes result1= obj.RejectTimeSheet(con, obj2);
            //string finmsg = result1.message;


            ScriptManager.RegisterClientScriptBlock(Button1, this.GetType(), "A Key", "alert('" + resultdata.message + "');", true);
            //ScriptManager.RegisterClientScriptBlock(Button1, this.GetType(), "A Key", "alert('" + finmsg + "');", true);
            Response.Redirect("timesheetreviews.aspx");

        }
    }
}