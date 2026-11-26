using System;
using System.Web.Security;
using PasswordHashLibrary;

namespace DefaultPage
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // If user is already authenticated, redirect them to their correct page
            string user = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(user))
            {
                if (user.EndsWith("|Staff"))
                {
                    Response.Redirect("../Page2/StaffPage.aspx");
                    return;
                }
                else if (user.EndsWith("|Member"))
                {
                    Response.Redirect("../Page1/MemberPage.aspx");
                    return;
                }
            }

            if (!IsPostBack)
            {
                string returnUrl = Request.QueryString["ReturnUrl"];
                bool registered = Request.QueryString["registered"] == "true";

                // Inform the user of what user type they are logging into
                Title.InnerText = getUserType() + " Login";

                // Inform user of successful registration
                if (registered)
                {
                    MessageSuccess.Text = "Registration successful. Please log in.";
                    MessageSuccess.Visible = true;
                }
            }
        }

        protected void DefaultBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            Message.Visible = false;
            MessageSuccess.Visible = false;

            // Get the current path to the xml file based on the option selected by user
            string path = Server.MapPath((getUserType() == "Member") ? "~/App_Data/member.xml" : "~/App_Data/staff.xml");

            // Get the username and password from the form
            string username = UsernameTxt.Text;
            string password = PasswordTxt.Text;

            // Make sure the fields are not empty
            if(username == "" || password == "")
            {
                Message.Text = "Please fill in all fields.";
                Message.Visible = true;
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
                    FormsAuthentication.RedirectFromLoginPage(username + "|" + getUserType(), Persistent.Checked);
                }
                else
                {
                    Message.Text = "Invalid username or password.";
                    Message.Visible = true;
                }
            }
            else
            {
                Message.Text = "Invalid username or password.";
                Message.Visible = true;
            }
        }

        // Gets the type of user to determine which xml to search
        private string getUserType()
        {
            string returnUrl = Request.QueryString["ReturnUrl"];

            if (!string.IsNullOrEmpty(returnUrl) &&
                returnUrl.Contains("StaffPage.aspx"))
            {
                return "Staff";
            }

            return "Member";
        }
    }
}