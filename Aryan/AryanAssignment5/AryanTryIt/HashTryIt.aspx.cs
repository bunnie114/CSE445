
using System;
using PasswordHashLibrary;

namespace AryanTryIt
{
    public partial class HashTryIt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Generate a hash for the entered password.
        protected void btnHash_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(password))
            {
                lblHashResult.Text = "Please enter a password.";
                return;
            }

            string hash = PasswordHasher.HashPassword(password);

            lblHashResult.Text = "Generated Hash: " +
                Server.HtmlEncode(hash);

            txtHash.Text = hash;
        }

        // Verify the password against the saved hash.
        protected void btnVerify_Click(object sender, EventArgs e)
        {
            string password = txtVerifyPassword.Text;
            string hash = txtHash.Text.Trim();

            if (string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(hash))
            {
                lblVerifyResult.Text = "Please enter a password and hash.";
                return;
            }

            bool valid = PasswordHasher.VerifyPassword(password, hash);

            if (valid)
            {
                lblVerifyResult.Text = "Password matches!";
            }
            else
            {
                lblVerifyResult.Text = "Password does not match.";
            }
        }
    }
}
