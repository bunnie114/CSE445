<%@ Page Language="C#" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Tina Phan - TryIt Hub</title>
</head>
<body style="font-family: Segoe UI, Arial, sans-serif; margin: 30px;">
    <h1>Christina (Tina) Phan &mdash; TryIt Pages</h1>
    <ul>
        <li><a href="<%= ResolveUrl("~/GlobalTryIt.aspx") %>">Global.asax &mdash; application visit counter</a></li>
        <li><a href="<%= ResolveUrl("~/CookieTryIt.aspx") %>">Cookie &mdash; preferred pharmacy store</a></li>
        <li><a href="<%= ResolveUrl("~/NotesTryIt.aspx") %>">Notes RESTful service &mdash; save / retrieve notes</a></li>
    </ul>
</body>
</html>
