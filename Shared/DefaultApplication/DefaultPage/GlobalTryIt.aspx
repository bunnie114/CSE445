<%@ Page Language="C#" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Global.asax TryIt</title>
</head>
<body style="font-family: Segoe UI, Arial, sans-serif; margin: 30px;">
<form id="form1" runat="server">
    <h2>Christina Phan &mdash; Global.asax TryIt</h2>
    <p>
        <b>Application_Start</b> creates an application-wide visit counter (and records the start time).
        <b>Session_Start</b> adds 1 every time a <i>new</i> user session begins.
    </p>
    <p><b>How to test:</b> note the number, click Refresh (same session &rarr; no change),
       then open this page in a private/incognito window (new session &rarr; number goes up by 1).</p>

    <h3>Visit count:</h3>
    <asp:Label ID="lblCount" runat="server" Font-Size="XX-Large" ForeColor="Blue" />
    <h3>Application started at:</h3>
    <asp:Label ID="lblStart" runat="server" />
    <br /><br />
    <asp:Button ID="btnRefresh" runat="server" Text="Refresh Counter" OnClick="RefreshCounter_Click" />

    <script runat="server">
        protected void Page_Load(object sender, EventArgs e) { DisplayCounter(); }
        protected void RefreshCounter_Click(object sender, EventArgs e) { DisplayCounter(); }

        private void DisplayCounter()
        {
            Application.Lock();
            object count = Application["VisitCount"];
            object start = Application["AppStartTime"];
            Application.UnLock();

            lblCount.Text = count == null ? "0" : count.ToString();
            lblStart.Text = start == null ? "unknown" : ((DateTime)start).ToString("G");
        }
    </script>
</form>
</body>
</html>
