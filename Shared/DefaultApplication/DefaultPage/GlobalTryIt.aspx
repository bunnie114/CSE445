<%@ Page Language="C#" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Global Application TryIt</title>
</head>
<body>
<form runat="server">

    <h2>Christina Phan - Global.asax TryIt</h2>

    <p>
        This component initializes an application-wide
        visit counter when the application starts.
    </p>

    <p>
        The counter increases whenever a new user session begins.
    </p>

    <h3>Current Application Visit Count:</h3>

    <asp:Label ID="lblCount" runat="server"
        Font-Size="XX-Large"
        ForeColor="Blue" />

    <br /><br />

    <asp:Button ID="btnRefresh" runat="server"
        Text="Refresh Counter"
        OnClick="RefreshCounter_Click" />

    <script runat="server">

        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayCounter();
        }

        protected void RefreshCounter_Click(object sender, EventArgs e)
        {
            DisplayCounter();
        }

        private void DisplayCounter()
        {
            Application.Lock();

            object count = Application["VisitCount"];

            lblCount.Text = count == null
                ? "0"
                : count.ToString();

            Application.UnLock();
        }

    </script>

</form>
</body>
</html>
