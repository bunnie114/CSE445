
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HashTryIt.aspx.cs" Inherits="AryanTryIt.HashTryIt" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Password Hashing TryIt</title>
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

        .result {
            margin-top: 15px;
            overflow-wrap: anywhere;
        }

        input, textarea {
            padding: 8px;
        }

        button, input[type=submit] {
            padding: 8px 15px;
            margin-top: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Password Hashing TryIt</h2>
            <p>Test the PasswordHashLibrary DLL component.</p>

            <h3>Hash Password</h3>
            <div class="field">
                <asp:TextBox ID="txtPassword" runat="server"
                    TextMode="Password" />
            </div>

            <asp:Button ID="btnHash" runat="server"
                Text="Hash Password"
                OnClick="btnHash_Click" />

            <div class="result">
                <asp:Label ID="lblHashResult" runat="server" />
            </div>

            <h3>Verify Password</h3>
            <p>Enter a password and the generated hash.</p>

            <div class="field">
                <asp:TextBox ID="txtVerifyPassword" runat="server"
                    TextMode="Password" />
            </div>

            <div class="field">
                <asp:TextBox ID="txtHash" runat="server"
                    TextMode="MultiLine" Rows="3" Width="450px" />
            </div>

            <asp:Button ID="btnVerify" runat="server"
                Text="Verify Password"
                OnClick="btnVerify_Click" />

            <div class="result">
                <asp:Label ID="lblVerifyResult" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
