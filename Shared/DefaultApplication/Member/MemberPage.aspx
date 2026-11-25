<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberPage.aspx.cs" Inherits="Member.MemberPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .button{
            padding: 10px 15px;
            border: none;
            border-radius: 5px !important;
            font-weight: bold;
            font-size: 16px;
        }

        .page-btn{
            background-color: #a60029;
            color: #ecfbfa;
            margin-right: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome to the Member Page</h1>
            <asp:Button ID="DefaultBtn" CssClass="page-btn button" runat="server" Text="Default Page" OnClick="DefaultBtn_Click" />
        </div>
    </form>
</body>
</html>