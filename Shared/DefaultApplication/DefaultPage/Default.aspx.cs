using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DefaultPage
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void MemberRegisterBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }

        protected void MemberLoginBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void StaffLoginBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void aryanBtn_Click(object sender, EventArgs e)
        {
            //Response.Redirect("");
        }

        protected void tinaBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TinaTryIt.aspx");
        }

        protected void noahBtn_Click(object sender, EventArgs e)
        {
            //Response.Redirect("");
        }
    }
}
