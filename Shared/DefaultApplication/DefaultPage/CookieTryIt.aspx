
<%@ Page Language="C#" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Store Cookie TryIt</title>
</head>
<body>
<form id="form1" runat="server">

    <h2>Group 9 - Store Cookie TryIt</h2>

    <p>Test the pharmacy store preference cookie.</p>

    <asp:Label runat="server" Text="Store ID:" />
    <asp:TextBox ID="txtStoreID" runat="server" />
    <br /><br />

    <asp:Label runat="server" Text="Store Name:" />
    <asp:TextBox ID="txtStoreName" runat="server" />
    <br /><br />

    <asp:Button ID="btnSave" runat="server"
        Text="Save Preferred Store"
        OnClick="SaveStore_Click" />

    <asp:Button ID="btnRetrieve" runat="server"
        Text="Retrieve Preferred Store"
        OnClick="RetrieveStore_Click" />

    <asp:Button ID="btnClear" runat="server"
        Text="Clear Cookie"
        OnClick="ClearStore_Click" />

    <br /><br />

    <asp:Label ID="lblResult" runat="server"
        ForeColor="Blue" />

    <script runat="server">

        protected void SaveStore_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtStoreID.Text) ||
                String.IsNullOrWhiteSpace(txtStoreName.Text))
            {
                lblResult.Text = "Please enter both store ID and name.";
                return;
            }

            StoreCookie.SaveStore(
                txtStoreID.Text.Trim(),
                txtStoreName.Text.Trim()
            );

            lblResult.Text = "Preferred pharmacy store saved successfully.";
        }

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
