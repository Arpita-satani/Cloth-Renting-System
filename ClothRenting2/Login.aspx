<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Site.Master"
    CodeFile="Login.aspx.cs"
    Inherits="ClothRenting2.Login" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<section class="section" id="login" style="padding-top:160px;">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-5">

                <div class="contact-form">
                    <div class="section-heading">
                        <h2>Login</h2>
                        <span>Login to your account</span>
                    </div>

                    Email
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Email required"
                        ForeColor="Red" /><br />

                    Password
                    <asp:TextBox ID="txtPassword" runat="server"
                        TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtPassword"
                        ErrorMessage="Password required"
                        ForeColor="Red" /><br />

                    <asp:Button ID="btnLogin" runat="server"
                        Text="Login"
                        CssClass="main-border-button"
                        OnClick="btnLogin_Click" />

                    <br /><br />
                    <asp:Label ID="lblMsg" runat="server" />

                </div>

            </div>
        </div>
    </div>
</section>

</asp:Content>
