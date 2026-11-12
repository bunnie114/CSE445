<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DefaultPage.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            width: 80%;
            margin: 3% auto;
            padding: 25px 25px 30px 25px;
            background-color: #ffffff;
            border-radius: 5px;
        }

        .inner-section{
            width: 95%;
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
            border-radius: 100px;
            padding: 10px 15px;
            border: none;
            border-radius: 5px !important;
            font-weight: bold;
            font-size: 16px;
        }

        .member-btn{
            background-color: #2a63c1;
            color: #ecfbfa;
            margin-right: 15px;
        }

        .staff-btn{
            background-color: #bf6b28;
            color: #ecfbfa;
        }

        .tryit-btn{
            background-color: #2a6339;
            color: #ecfbfa;
            margin-right: 15px;
        }

        table{
            width: 100%;
        }

        table, tr, td{
            border-collapse: collapse;
            border: 2px solid #e1e1e1;
        }

        .top-row{
            background-color: black;
            color: white;
        }

        .top-row td{
            padding: 15px;
            font-weight: bold;
            font-size: 18px;
        }

        .odd-row{
            background-color: #f9f9f9;
        }

        .even-row{
            background-color: #ffffff;
        }

        .row td{
            padding: 5px 5px 5px 15px;
            font-size: 18px;
        }

        .auto-style1 {
            width: 222px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="section">
            <h1 class="title">About This Application</h1>
            <hr />
            <p>This is a service-oriented inventory management web application built with ASP.NET Web Forms (.NET framework). Members can login to view product information of a pharmacy by entering a website url and using the filter feature to filter out unnecessary words. Staff can login to manage the inventory of specific pharmacy stores as well as making notes for specific locations and that locations products</p>
            <h2>Navigate To:</h2>
            <div>
                <asp:Button ID="MemberRegisterBtn" class="member-btn button" runat="server" Text="Register as Member" OnClick="MemberRegisterBtn_Click" />
                <asp:Button ID="MemberLoginBtn" class="member-btn button" runat="server" Text="Member Login" OnClick="MemberLoginBtn_Click" />
                <asp:Button ID="StaffLoginBtn" class="staff-btn button" runat="server" Text="Staff Login" OnClick="StaffLoginBtn_Click" />
            </div>
        </div>

        <div class="section">
            <h1 class="title">TryIt Pages</h1>
            <hr />
            <p>Link to each member's TryIt page.</p>
            <div>
                <asp:Button ID="aryanBtn" class="tryit-btn button" runat="server" Text="Aryan's TryIt" OnClick="aryanBtn_Click" />
                <asp:Button ID="tinaBtn" class="tryit-btn button" runat="server" Text="Tina's TryIt" OnClick="tinaBtn_Click" />
                <asp:Button ID="noahBtn" class="tryit-btn button" runat="server" Text="Noah's TryIt" OnClick="noahBtn_Click" />
            </div>
        </div>

        <div class="section">
            <h1 class="title">TA Test Guide and Credentials</h1>
            <hr />
            <div class="inner-section">
                <h3>Member Account:</h3>
                <h3>Staff Account:</h3>
                <h3>Member-Registration Test:</h3>
                <h3>Access Control Test:</h3>
            </div>

        </div>

        <div class="section">
            <h1 class="title">Application and Components Summary Table</h1>
            <hr />
            <table>
                <tr class="top-row">
                    <td>Provider</td>
                    <td class="auto-style1">Page / Component Type</td>
                    <td>Component Description (inputs / outputs)</td>
                    <td>Resources and Methods / Where Used</td>
                </tr>
                <tr class="odd-row row">
                    <td>Team</td>
                    <td class="auto-style1">ASPX page (Default.aspx)</td>
                    <td>Public home page. Introduces the application, lists all components, provides TA test credentials and test cases.</td>
                    <td>ASP.NET Web Forms, C# code-bhind. Entry point for the application.</td>
                </tr>
                <tr class="even-row row">
                    <td>Aryan</td>
                    <td class="auto-style1">Test</td>
                    <td>Test</td>
                    <td>Test</td>
                </tr>
                <tr class="odd-row row">
                    <td>Aryan</td>
                    <td class="auto-style1">Test</td>
                    <td>Test</td>
                    <td>Test</td>
                </tr>
                <tr class="even-row row">
                    <td>Aryan</td>
                    <td class="auto-style1">Test</td>
                    <td>Test</td>
                    <td>Test</td>
                </tr>
                <tr class="odd-row row">
                    <td>Tina</td>
                    <td class="auto-style1">Test</td>
                    <td>Test</td>
                    <td>Test</td>
                </tr>
                <tr class="even-row row">
                    <td>Tina</td>
                    <td class="auto-style1">Test</td>
                    <td>Test</td>
                    <td>Test</td>
                </tr>
                <tr class="odd-row row">
                    <td>Tina</td>
                    <td class="auto-style1">Test</td>
                    <td>Test</td>
                    <td>Test</td>
                </tr>
                <tr class="even-row row">
                    <td>Noah</td>
                    <td class="auto-style1">User Control (CaptchaControl.ascx)</td>
                    <td>Generates and displays a CAPTCHA image and verifies user input. Input: User input for CAPTCHA text. Output: Result of verification.</td>
                    <td>System.Drawing, Random, and Bitmap for generating CAPTCHA image. Used in TryIt page.</td>
                </tr>
                <tr class="odd-row row">
                    <td>Noah</td>
                    <td class="auto-style1">Session State</td>
                    <td>Stores the most recently generated CAPTCHA text in session state. Input: Generated CAPTCHA text. Output: Stored CAPTCHA text.</td>
                    <td>Session state. Used in CaptchaControl.ascx.</td>
                </tr>
                <tr class="even-row row">
                    <td>Noah</td>
                    <td class="auto-style1">WSL Web Service (InventoryManagement.asmx)</td>
                    <td>Manages inventory of multiple pharmacy stores. Supports variety of CRUD operations for managing inventory of one or multiple stores at once. Inputs: Store/product IDs, product name, and quantity. Outputs: New inventory data or specific result messages.</td>
                    <td>WSDL, XML, XmlSerializer, StreamReader, and StreamWriter for interacting with XML. Used in TryIt page.</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
