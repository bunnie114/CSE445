<%@ Application Language="C#" %>

<script runat="server">

    // Global.asax: application-wide event handlers.

    // Runs ONCE when the pharmacy application starts (first request after a deploy/recycle).
    // Initializes the application-wide state shared by every user.
    void Application_Start(object sender, EventArgs e)
    {
        Application["VisitCount"] = 0;
        Application["AppStartTime"] = DateTime.Now;
    }

    // Runs when a NEW user session begins: increments the visit counter.
    void Session_Start(object sender, EventArgs e)
    {
        // Writing something to Session forces ASP.NET to persist the session id.
        // Without this, an unused session gets a new id on every request and
        // Session_Start fires on EVERY refresh, inflating the counter.
        Session["Counted"] = true;

        Application.Lock();   // Application state is shared by all threads
        try
        {
            int count = Application["VisitCount"] == null ? 0 : (int)Application["VisitCount"];
            Application["VisitCount"] = count + 1;
        }
        finally
        {
            Application.UnLock();
        }
    }

</script>
