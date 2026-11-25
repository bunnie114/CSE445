using PasswordHashLibrary;
using System;
using System.Security.Principal;
using System.Web.Security;
using System.Web.UI;

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
                UserType.Items.FindByValue("Member").Enabled = true;
                UserType.Items.FindByValue("Staff").Enabled = true;
                string returnUrl = Request.QueryString["ReturnUrl"];

                // Based on where user is coming from, select one of the radio buttons for user convenience
                if (Request.QueryString["type"] == "Staff" || 
                    (!string.IsNullOrEmpty(returnUrl) && 
                    returnUrl.Contains("StaffPage.aspx")))
                {
                    UserType.SelectedValue = "Staff";
                }
                else
                {
                    UserType.SelectedValue = "Member";
                }

                // If they are being redirected from one of the content pages, lock the radio button
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    if (returnUrl.Contains("StaffPage.aspx"))
                    {
                        UserType.Items.FindByValue("Member").Enabled = false;
                    }
                    else
                    {
                        UserType.Items.FindByValue("Staff").Enabled = false;
                    }
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
                    FormsAuthentication.RedirectFromLoginPage(username + "|" + UserType.SelectedValue, Persistent.Checked);
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