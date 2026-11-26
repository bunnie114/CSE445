using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Member
{
    public partial class MemberPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Make sure the user is authorized to access this page
            string identity = Context.User.Identity.Name;

            if (!identity.EndsWith("|Member"))
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
            Response.Redirect("../Page0/Default.aspx");

        }
    }
}