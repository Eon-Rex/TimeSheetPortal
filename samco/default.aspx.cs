using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.AccountManagement;
using System.Configuration;
using Newtonsoft.Json;

namespace samco.CBol
{
    public partial class _default : System.Web.UI.Page
    {

        D365ServiceRefrence.D365ServiceCall D365Call = new D365ServiceRefrence.D365ServiceCall();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //for generate random number Captcha
            Random rnd = new Random();
            int No1 = rnd.Next(1, 10);
            int No2 = rnd.Next(1, 10) + 2;
            lblNo1.Text = No1.ToString();
            lblNo2.Text = No2.ToString();
            //
        }

        public bool IsAuthenticate(string DomainName, string UserId, string Password)
        {
            bool IsValid = true;
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, DomainName))
                {
                    IsValid = pc.ValidateCredentials(UserId, Password);
                }
                if (!IsValid)
                {
                    throw new Exception("Incorrect password. Please try again!");
                }
                return IsValid;
            }
            catch
            {
                return false;
            }
        }

        protected async void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                samco.ServiceReference3.MobileCustomeServiceClient obj = new ServiceReference3.MobileCustomeServiceClient();
                obj.ClientCredentials.Windows.ClientCredential.Domain = ConfigurationSettings.AppSettings["Domain"].ToString();
                obj.ClientCredentials.Windows.ClientCredential.UserName = ConfigurationSettings.AppSettings["UserId"].ToString();
                obj.ClientCredentials.Windows.ClientCredential.Password = ConfigurationSettings.AppSettings["pwd"].ToString();
                samco.ServiceReference3.CallContext con = new samco.ServiceReference3.CallContext();
                con.Company = ConfigurationSettings.AppSettings["CompanyName"].ToString();
                
                string UserId = txtusername.Text;
                string Password = txtpass.Text;

                string api = "loginInMobilePortal";
                var credentials = new { _loginId = UserId };

                foreach (string cookieName in Request.Cookies.AllKeys)
                {
                    HttpCookie cookie = Request.Cookies[cookieName];
                    if (cookie != null)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(cookie);
                    }
                }
                string jsonstring1 = (string)await D365Call.PostD365Service(api, credentials);
                samco.ServiceReference3.ACX_ReturnLoginAttributes returnty = JsonConvert.DeserializeObject< samco.ServiceReference3.ACX_ReturnLoginAttributes> (jsonstring1);

                //samco.ServiceReference3.ACX_ReturnLoginAttributes returnty = obj.loginInMobilePortal(con, UserId);
                bool returnty1 = returnty.status;
                if (returnty1 == true)
                {
                    bool IsValid = IsAuthenticate("amsshardul", UserId, Password);
                    //bool IsValid = true;
                    if (IsValid)
                    {

                        string Uname = returnty.userName;
                        string Text = Encrpyt.Encryption.EncryptPlainTextToCipherText(txtusername.Text);
                       string Pass = Encrpyt.Encryption.EncryptPlainTextToCipherText(txtpass.Text);
                        string UName = Encrpyt.Encryption.EncryptPlainTextToCipherText(Uname);

                        Response.Redirect($"Mytimesheetdash.aspx?UserId={Text}&Password={Pass}&Username={UName}");

                        //Response.Redirect("Mytimesheetdash.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(BtnLogin, this.GetType(), "A Key", "alert('Password Does not exits in AX.....');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(BtnLogin, this.GetType(), "A Key", "alert('User Does not exits in AX.....');", true);
                }


            }
            catch (Exception ex)
            {
                LblMessage.Text = ex.Message.ToString();
            }
        }


    }
}