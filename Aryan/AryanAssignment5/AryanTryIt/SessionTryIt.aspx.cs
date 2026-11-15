
using System;

namespace AryanTryIt
{
    public partial class SessionTryIt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Save user information in Session.
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtStoreId.Text))
            {
                lblResult.Text = "Please enter both fields.";
                return;
            }

            Session["TryItUsername"] = txtUsername.Text.Trim();
            Session["TryItStoreId"] = txtStoreId.Text.Trim();

            lblResult.Text = "Session saved successfully!";
        }

        // Retrieve stored Session information.
        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            if (Session["TryItUsername"] != null &&
                Session["TryItStoreId"] != null)
            {
                string username = Session["TryItUsername"].ToString();
                string storeId = Session["TryItStoreId"].ToString();

                lblResult.Text = "Username: " +
                    Server.HtmlEncode(username) +
                    "<br />Store ID: " +
                    Server.HtmlEncode(storeId);
            }
            else
            {
                lblResult.Text = "No session data found.";
            }
        }

        // Remove the saved Session information.
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Session.Remove("TryItUsername");
            Session.Remove("TryItStoreId");

            lblResult.Text = "Session cleared successfully!";
        }
    }
}
