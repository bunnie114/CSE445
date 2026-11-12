<%@ Page Language="C#" %>
<%@ Import Namespace="TinaComponents" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Store Cookie TryIt</title>
</head>
<body style="font-family: Segoe UI, Arial, sans-serif; margin: 30px;">
<form id="form1" runat="server">
    <h2>Christina Phan &mdash; Store Cookie TryIt</h2>
    <p>Saves the user's preferred pharmacy store in a cookie (30 days). Inputs: Store ID, Store Name (max 50 chars each).
       Output: the saved store, or "No preferred store saved."</p>
    <p><b>Test:</b> Retrieve (none saved) &rarr; enter 5001 / Main Street &rarr; Save &rarr; Retrieve &rarr; Clear &rarr; Retrieve.</p>

    Store ID: <asp:TextBox ID="txtStoreID" runat="server" /><br /><br />
    Store Name: <asp:TextBox ID="txtStoreName" runat="server" /><br /><br />

    <asp:Button ID="btnSave" runat="server" Text="Save Preferred Store" OnClick="SaveStore_Click" />
    <asp:Button ID="btnRetrieve" runat="server" Text="Retrieve Preferred Store" OnClick="RetrieveStore_Click" />
    <asp:Button ID="btnClear" runat="server" Text="Clear Cookie" OnClick="ClearStore_Click" />
    <br /><br />
    <asp:Label ID="lblResult" runat="server" ForeColor="Blue" />

    <script runat="server">
        protected void SaveStore_Click(object sender, EventArgs e)
        {
            try
            {
                StoreCookie.SaveStore(txtStoreID.Text, txtStoreName.Text);
                lblResult.Text = "Preferred pharmacy store saved successfully.";
            }
            catch (ArgumentException ex)
            {
                lblResult.Text = Server.HtmlEncode(ex.Message);
            }
        }

        // A cookie written during a request is only readable on the NEXT request,
        // so Retrieve is a separate click (a separate postback).
        protected void RetrieveStore_Click(object sender, EventArgs e)
        {
            lblResult.Text = Server.HtmlEncode(StoreCookie.GetStore());
        }

        protected void ClearStore_Click(object sender, EventArgs e)
        {
            StoreCookie.ClearStore();
            lblResult.Text = "Preferred store cookie cleared.";
        }
    </script>
</form>
</body>
</html>
