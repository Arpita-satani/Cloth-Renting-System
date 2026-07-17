<%@ Page Language="C#" AutoEventWireup="true"
CodeFile="AdminLogin.aspx.cs"
Inherits="ClothRenting2.Admin.AdminLogin" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Admin Login</title>
    <link href="../assets/css/admin-login.css" rel="stylesheet" />
</head>

<body>
<form id="form1" runat="server">

<div class="login-wrapper">
    <div class="login-box">
        <h2>Admin Login</h2>

        <asp:TextBox ID="txtEmail" runat="server"
            CssClass="input-box"
            Placeholder="Email"></asp:TextBox>

        <asp:TextBox ID="txtPassword" runat="server"
            CssClass="input-box"
            TextMode="Password"
            Placeholder="Password"></asp:TextBox>

        <asp:Button ID="btnLogin" runat="server"
            Text="Login"
            CssClass="login-btn"
            OnClick="btnLogin_Click" />

        <asp:Label ID="lblMsg" runat="server" CssClass="msg"></asp:Label>
    </div>
</div>

</form>
</body>
</html>
