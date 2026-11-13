<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DefaultPage.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <style type="text/css">
        body{
            background-color: #f6f6f6;
            font-family: Arial, Helvetica, sans-serif;
            margin: 0;
        }

        p{
            font-size: 17px;
        }

        .section{
            width: 35%;
            min-width: 400px;
            margin: 3% auto;
            padding: 25px 25px 30px 25px;
            background-color: #ffffff;
            border-radius: 5px;
        }

        .inner-section {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            width: 90%;
            margin: 3% auto 0% auto;
            background-color: #eaf4fc;
            border: 2px solid #d8e6f9;
            border-radius: 10px;
            padding: 25px;
        }

        .title{
            margin-bottom: 0px;
        }

        hr{
            margin-bottom: 25px;
            color: #f8f8f8
        }

        .button{
            padding: 10px 15px;
            margin: 10px 5px 10px 5px;
            border: none;
            border-radius: 5px !important;
            background-color: #2688ff;
            color: #ffffff;
            font-weight: bold;
            font-size: 18px;
            width: 90%;
        }

        .input-section {
            margin: 5px 5px 10px 5px;
            padding: 0px;
            display: inline-block;
            width: 90%;
        }

        .label{
            margin: 0px;
            padding-left: 5px;
            font-size: 24px;
        }

        .input-box{
            border: 1px solid #e8e6e6;
            border-radius: 5px;
            padding: 5px;
            width: 100%;
            font-size: 20px;
            box-sizing: border-box;
        }

        .register-link{
            display: flex;
            justify-content: center;
            align-items: center;
            font-size: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="section">
            <h1 class="title">Login</h1>
            <h3>This is just a placeholder page. Functionality will be implemented in assignment 6.</h3>
            <hr />
            <div class="inner-section">
                <div class="input-section">
                    <p class="label">Username:</p>
                    <asp:TextBox ID="UsernameTxt" CssClass="input-box" runat="server"></asp:TextBox>
                </div>

                <div class="input-section">
                    <p class="label">Password:</p>
                    <asp:TextBox ID="PasswordTxt" CssClass="input-box" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                
                <br />

                <asp:Button ID="LoginBtn" CssClass="button" runat="server" Text="Login" OnClick="LoginBtn_Click" />
                <asp:Button ID="DefaultBtn" CssClass="button" runat="server" Text="Back to Default" OnClick="DefaultBtn_Click" />

                <br />

                <p class="register-link">Don't have an account? &nbsp;&nbsp; <a href="Register.aspx">Sign Up</a></p>
            </div>
        </div>
    </form>
</body>
</html>

