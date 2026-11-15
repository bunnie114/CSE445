
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceDirectory.aspx.cs" Inherits="AryanTryIt.ServiceDirectory" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Aryan - Service Directory</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 40px;
        }

        h2 {
            margin-bottom: 10px;
        }

        table {
            border-collapse: collapse;
            width: 100%;
        }

        th, td {
            border: 1px solid #ccc;
            padding: 12px;
            text-align: left;
            vertical-align: top;
        }

        th {
            background-color: #e8eef5;
        }

        a {
            color: #0066cc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Application and Components Service Directory</h2>

        <p>Provider: Aryan Ahlawat</p>
        <p>CSE 445 - Assignment 5</p>

        <table>
            <tr>
                <th>Component Type</th>
                <th>Operation</th>
                <th>Inputs / Outputs</th>
                <th>Description</th>
                <th>TryIt</th>
            </tr>

            <tr>
                <td>DLL - PasswordHashLibrary</td>
                <td>HashPassword<br />VerifyPassword</td>
                <td>
                    Input: Password (string)<br />
                    Output: Hash (string)<br /><br />
                    Verification Input: Password and hash<br />
                    Output: Boolean
                </td>
                <td>
                    Generates a secure password hash and
                    verifies passwords using a local C# DLL.
                </td>
                <td>
                    <a href="HashTryIt.aspx">Test DLL</a>
                </td>
            </tr>

            <tr>
                <td>Session State</td>
                <td>Save, Retrieve, Clear</td>
                <td>
                    Input: Username and Store ID (strings)<br />
                    Output: Saved session values or status message
                </td>
                <td>
                    Stores temporary username and pharmacy
                    store information using ASP.NET Session.
                </td>
                <td>
                    <a href="SessionTryIt.aspx">Test Session</a>
                </td>
            </tr>

            <tr>
                <td>RESTful Web Service</td>
                <td>WordFilter</td>
                <td>
                    Input: Text or HTML (string)<br />
                    Output: Filtered text (string)
                </td>
                <td>
                    Removes HTML tags, punctuation,
                    and common stop words from website content.
                </td>
                <td>
                    <a href="WordFilterTryIt.aspx">Test WordFilter</a>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
