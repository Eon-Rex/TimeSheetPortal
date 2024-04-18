using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace samco
{
    public partial class LogOutError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lbkSignOut_Click(object sender, EventArgs e)
        {
            this.Session.Abandon();
            this.Response.Redirect("default.aspx");
        }
    }
}