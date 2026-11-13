<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptchaControl.ascx.cs" Inherits="DefaultPage.CaptchaControl" %>

<asp:Image ID="CaptchaImg" runat="server" Height="110px" Width="227px" />
<div class="captcha-container">
    <asp:Button ID="GenerateBtn" CssClass="button gen-btn" runat="server" Text="New Captcha" OnClick="GenerateBtn_Click" />

    <div class="input-section input-section2">
        <p class="label label2">Captcha Response:</p>
        <asp:TextBox ID="CaptchaInputTxt" CssClass="input-box input-box2" runat="server"></asp:TextBox>
    </div>
</div>

