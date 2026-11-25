using PasswordHashLibrary;
using System;
using System.Web.Security;
using System.Web.UI;

namespace DefaultPage
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // If user navigating from staff login button, set radio button to staff, otherwise set to member
                if (Request.QueryString["type"] == "Staff" || 
                    (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]) && 
                    Request.QueryString["ReturnUrl"].Contains("Page2")))
                {
                    UserType.SelectedValue = "Staff";
                }
                else
                {
                    UserType.SelectedValue = "Member";
                }
            }
        }

        protected void DefaultBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            // Get the current path to the xml file based on the option selected by user
            string path = Server.MapPath((UserType.SelectedValue == "Member") ? "~/App_Data/member.xml" : "~/App_Data/staff.xml");

            // Get the username and password from the form
            string username = UsernameTxt.Text;
            string password = PasswordTxt.Text;

            // Make sure the fields are not empty
            if(username == "" || password == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter both username and password.');", true);
                return;
            }

            UserManager userManager = new UserManager();

            // Find the user in the xml file based on the username
            User user = userManager.findUser(path, username);

            // Make sure the user exists and the password matches
            if(user != null)
            {
                // Verify the password matches
                if(PasswordHasher.VerifyPassword(password, user.Password))
                {
                    FormsAuthentication.RedirectFromLoginPage(username, Persistent.Checked);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid username or password.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid username or password.');", true);
            }
        }
    }
}