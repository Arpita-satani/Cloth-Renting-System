<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Site.Master"
    CodeFile="Register.aspx.cs"
    Inherits="ClothRenting2.Register" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<section style="padding-top:150px;">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-5">

                <div class="contact-form">
                    <h2 style="text-align:center;">Register</h2>
                    <hr />

                    <!-- Full Name -->
                    Full Name
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtName"
                        ErrorMessage="Name required"
                        ForeColor="Red" /><br />

                    <!-- Email -->
                    Email
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Email required"
                        ForeColor="Red" />
                    <asp:RegularExpressionValidator runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Invalid Email"
                        ValidationExpression="\w+@\w+\.\w+"
                        ForeColor="Red" /><br />

                    <!-- Mobile -->
                    Mobile
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" />
                    <br />

                    <!-- Password -->
                    Password
                    <asp:TextBox ID="txtPassword" runat="server"
                        TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server"
                        ControlToValidate="txtPassword"
                        ErrorMessage="Password required"
                        ForeColor="Red" /><br />

                    <!-- Confirm Password -->
                    Confirm Password
                    <asp:TextBox ID="txtConfirm" runat="server"
                        TextMode="Password" CssClass="form-control" />
                    <asp:CompareValidator runat="server"
                        ControlToValidate="txtConfirm"
                        ControlToCompare="txtPassword"
                        ErrorMessage="Password not matched"
                        ForeColor="Red" /><br />

                    <!-- Button -->
                    <asp:Button ID="btnRegister" runat="server"
                        Text="Register"
                        CssClass="btn btn-dark w-100"
                        OnClick="btnRegister_Click" />

                    <br /><br />
                    <asp:Label ID="lblMsg" runat="server" />

                </div>

            </div>
        </div>
    </div>
</section>

</asp:Content>
