
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WordFilterTryIt.aspx.cs" Inherits="AryanTryIt.WordFilterTryIt" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>WordFilter TryIt</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 40px;
        }

        .container {
            max-width: 650px;
        }

        .field {
            margin-bottom: 15px;
        }

        textarea {
            padding: 10px;
            width: 95%;
        }

        input[type=submit] {
            padding: 8px 15px;
        }

        .result {
            margin-top: 20px;
            overflow-wrap: anywhere;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>WordFilter REST Service TryIt</h2>
            <p>Enter text or HTML content to remove tags and common words.</p>

            <div class="field">
                <h3>Input Text</h3>
                <asp:TextBox ID="txtInput" runat="server"
                    TextMode="MultiLine" Rows="7"
                    ValidateRequestMode="Disabled" />
            </div>

            <asp:Button ID="btnFilter" runat="server"
                Text="Filter Text"
                OnClick="btnFilter_Click" />

            <div class="result">
                <h3>Filtered Result</h3>
                <asp:Label ID="lblResult" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
