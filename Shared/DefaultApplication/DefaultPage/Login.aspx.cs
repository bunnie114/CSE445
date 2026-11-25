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
                Response.Write("type = " + Request.QueryString["type"] + "<br>");
                Response.Write("returnUrl = " + Request.QueryString["ReturnUrl"] + "<br>");

                // If user navigating from staff login button, set radio button to staff, otherwise set to member
                if (Request.QueryString["type"] == "Staff" || 
                    (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]) && 
                    Request.QueryString["ReturnUrl"].Contains("Staff.aspx")))
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
                    // If redirected from one of the content pages, prevent them from logging in as the other user type
                    string returnUrl = Request.QueryString["ReturnUrl"];
                    // If trying to access Staff page, must log in as Staff
                    if (!string.IsNullOrEmpty(returnUrl) &&
                        returnUrl.Contains("Staff.aspx") &&
                        UserType.SelectedValue != "Staff")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You must log in with a staff account to access the Staff page.');", true);
                        return;
                    }

                    // If trying to access Member page, must log in as Member
                    if (!string.IsNullOrEmpty(returnUrl) &&
                        returnUrl.Contains("Member.aspx") &&
                        UserType.SelectedValue != "Member")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You must log in with a member account to access the Member page.');", true);
                        return;
                    }

                    FormsAuthentication.RedirectFromLoginPage(
                        username,
                        Persistent.Checked
                    );

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