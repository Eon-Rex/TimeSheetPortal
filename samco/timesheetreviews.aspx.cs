using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace samco.CBol
{
    public partial class timesheetreviews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("LogOutError.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
        }
        protected void lbkSignOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            
            Response.Redirect("default.aspx");
        }
    }
}