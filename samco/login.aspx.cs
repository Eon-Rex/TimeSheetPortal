using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.AccountManagement;
namespace samco
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["UserId"] = txtusername.Text;
            Session["Password"] = txtpass.Text;
            Response.Redirect("Mytimesheetdash.aspx");
           
        }
        
    }
}