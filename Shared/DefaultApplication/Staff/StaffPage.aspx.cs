using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Staff
{
    public partial class StaffPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Make sure the user is authorized to access this page
            string identity = Context.User.Identity.Name;

            if (!identity.EndsWith("|Staff"))
            {
                Response.Redirect("../Page0/Default.aspx?unauthorized=true");
            }
        }

        protected void DefaultBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Page0/Default.aspx");
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Server.Transfer("../Page0/Default.aspx");

        }
    }
}