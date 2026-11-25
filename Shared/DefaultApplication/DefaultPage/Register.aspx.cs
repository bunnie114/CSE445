using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DefaultPage
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DefaultBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/App_Data/member.xml");

            // Get the user credentials from the form
            string username = UsernameTxt.Text;
            string email = EmailTxt.Text;
            string password = PasswordTxt.Text;
            string passwordConfirm = ConfirmPasswordTxt.Text;

            // Make sure the fields are not empty
            if (username == "" || email == "" || password == "" || passwordConfirm == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please fill in all fields.');", true);
                return;
            }

            // Make sure the passwords match
            if (password != passwordConfirm)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Passwords do not match.');", true);
                return;
            }

            UserManager userManager = new UserManager();

            // Make sure the username is not taken
            if(userManager.findUser(path, username) != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Username is already taken.');", true);
            // Make sure the email is not taken
            }else if(userManager.emailExists(path, email))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email is already taken.');", true);
            
            }
            // Make sure the captcha is correct
            else if(!CaptchaCtrl.IsCaptchaValid())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Captcha incorrect. Please try again.');", true);
            }
            // Create the new user and add it to the xml file
            else
            {
                // Hash the password before storing it
                string hashedPassword = PasswordHashLibrary.PasswordHasher.HashPassword(password);
                User newUser = new User();
                newUser.Username = username;
                newUser.Email = email;
                newUser.Password = hashedPassword;
                userManager.addUser(path, newUser);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User registered successfully.');", true);
            }

        }
    }
}