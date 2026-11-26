using System;
using PasswordHashLibrary;

namespace DefaultPage
{
    public partial class Register : System.Web.UI.Page
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
                // Set the destination of the login link
                LoginLink.HRef = "Login.aspx?ReturnUrl=" + Server.UrlEncode(Request.QueryString["ReturnUrl"]);
            }
        }

        protected void DefaultBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            Message.Visible = false;
            
            string path = Server.MapPath("~/App_Data/member.xml");

            // Get the user credentials from the form
            string username = UsernameTxt.Text;
            string email = EmailTxt.Text;
            string password = PasswordTxt.Text;
            string passwordConfirm = ConfirmPasswordTxt.Text;

            // Make sure the fields are not empty
            if (username == "" || email == "" || password == "" || passwordConfirm == "")
            {
                Message.Text = "Please fill in all fields.";
                Message.Visible = true;
                return;
            }

            // Make sure the passwords match
            if (password != passwordConfirm)
            {
                Message.Text = "Passwords do not match.";
                Message.Visible = true;
                return;
            }

            UserManager userManager = new UserManager();

            // Make sure the username is not taken
            if(userManager.findUser(path, username) != null)
            {
                Message.Text = "Username is already taken.";
                Message.Visible = true;
            }
            // Make sure the email is not taken
            else if(userManager.emailExists(path, email))
            {
                Message.Text = "Email is already taken.";
                Message.Visible = true;
            }
            // Make sure the captcha is correct
            else if(!CaptchaCtrl.IsCaptchaValid())
            {
                Message.Text = "Captcha incorrect. Please try again.";
                Message.Visible = true;
            }
            // Create the new user and add it to the xml file
            else
            {
                // Hash the password before storing it
                string hashedPassword = PasswordHasher.HashPassword(password);
                User newUser = new User();
                newUser.Username = username;
                newUser.Email = email;
                newUser.Password = hashedPassword;
                userManager.addUser(path, newUser);
                Response.Redirect("Login.aspx?registered=true&ReturnUrl=../Page1/MemberPage.aspx");
            }

        }
    }
}