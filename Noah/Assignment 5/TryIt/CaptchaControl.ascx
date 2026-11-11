<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptchaControl.ascx.cs" Inherits="TryIt.CaptchaControl" %>

<!----
<style>
    div {
        margin: 10px 0px 10px 10px;
    }

    .result{
        margin-top: 10px;
        margin-left: 20px;
        display: block
    }

    .label{
        display: inline-block;
    }
</style>
---->

<asp:Image ID="CaptchaImg" runat="server" Height="110px" Width="227px" />
<br />
<asp:Button ID="GenerateBtn" runat="server" Text="Generate Captcha" OnClick="GenerateBtn_Click" />
<div>
    <p class="label">Captcha Response: </p>
    <asp:TextBox ID="CaptchaInputTxt" runat="server"></asp:TextBox>
    <asp:Button ID="CheckBtn" runat="server" Text="Verify" OnClick="CheckBtn_Click" />
</div>

<!----
<asp:Label ID="ResultLbl" class="result" runat="server">
    Result
</asp:Label>
---->

