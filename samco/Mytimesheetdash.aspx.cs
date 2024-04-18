using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace samco.CBol
{
    public partial class Mytimesheetdash : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserId"] == null || Session["Password"] == null || Session["Username"] == null)
            {
                string userId = Request.QueryString["UserId"];
                string password = Request.QueryString["Password"];
                string username = Request.QueryString["Username"];

                // Create session variables
                Session["UserId"] = Encrpyt.Encryption.DecryptCipherTextToPlainText(userId);
                Session["Password"] = Encrpyt.Encryption.DecryptCipherTextToPlainText(password);
                Session["Username"] = Encrpyt.Encryption.DecryptCipherTextToPlainText(username);
            }
        }
        protected void lbkSignOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            
            Response.Redirect("default.aspx");
        }
    }
}