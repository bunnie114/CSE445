
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionTryIt.aspx.cs" Inherits="AryanTryIt.SessionTryIt" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Session State TryIt</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 40px;
        }

        .container {
            max-width: 600px;
        }

        .field {
            margin-bottom: 15px;
        }

        input {
            padding: 8px;
        }

        .result {
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Session State TryIt</h2>
            <p>Store and retrieve temporary user information using ASP.NET Session.</p>

            <div class="field">
                <p>Username:</p>
                <asp:TextBox ID="txtUsername" runat="server" />
            </div>

            <div class="field">
                <p>Pharmacy Store ID:</p>
                <asp:TextBox ID="txtStoreId" runat="server" />
            </div>

            <asp:Button ID="btnSave" runat="server"
                Text="Save Session"
                OnClick="btnSave_Click" />

            <asp:Button ID="btnRetrieve" runat="server"
                Text="Retrieve Session"
                OnClick="btnRetrieve_Click" />

            <asp:Button ID="btnClear" runat="server"
                Text="Clear Session"
                OnClick="btnClear_Click" />

            <div class="result">
                <asp:Label ID="lblResult" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
