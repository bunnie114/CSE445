using System;
using PasswordHashLibrary;

namespace DefaultPage
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Title.InnerText = getUserType() + " Password Change";

                // Set the destination of the login link
                LoginLink.HRef = "Login.aspx?ReturnUrl=" + Server.UrlEncode(Request.QueryString["ReturnUrl"]);
            }
        }

        protected void ChangePassBtn_Click(object sender, EventArgs e)
        {
            Message.Visible = false;

            // Get the current path to the xml file based on the option selected by user
            string path = Server.MapPath((getUserType() == "Member") ? "~/App_Data/member.xml" : "~/App_Data/staff.xml");

            // Get the username and password from the form
            string username = UsernameTxt.Text;
            string password = PasswordTxt.Text;
            string newPass = NewPassTxt.Text;
            string newPass2 = ConfirmPassTxt.Text;

            // Make sure the fields are not empty
            if (username == "" || password == "" || newPass == "" || newPass2 == "")
            {
                Message.Text = "Please fill in all fields.";
                Message.Visible = true;
                return;
            }

            // Make sure the passwords match
            if (newPass != newPass2)
            {
                Message.Text = "Passwords do not match.";
                Message.Visible = true;
                return;
            }

            UserManager userManager = new UserManager();

            // Find the user in the xml file based on the username
            User user = userManager.findUser(path, username);

            // Make sure the user exists and the password matches
            if (user != null)
            {
                // Verify the password matches
                if (PasswordHasher.VerifyPassword(password, user.Password))
                {
                    string hashedPassword = PasswordHasher.HashPassword(newPass);
                    userManager.updatePassword(path, username, hashedPassword);
                    Response.Redirect("Login.aspx?change=true&ReturnUrl=" + Request.QueryString["ReturnUrl"]);
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

        protected void DefaultBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
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