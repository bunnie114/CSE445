<%@ Application Language="C#" %>

<script runat="server">

    // Runs once when the pharmacy application starts.
    void Application_Start(object sender, EventArgs e)
    {
        Application["VisitCount"] = 0;
    }

    // Runs when a new user session begins.
    void Session_Start(object sender, EventArgs e)
    {
        Application.Lock();

        int count = (int)Application["VisitCount"];
        Application["VisitCount"] = count + 1;

        Application.UnLock();
    }

</script>
