<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Site.master"
    CodeFile="ContactUs.aspx.cs"
    Inherits="ClothRenting2.ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="lux-page contact-bg">
    <div class="lux-card">

        <h1 class="text-dark">Contact Us</h1>

        <p class="lux-sub text-light">
            We'd love to hear from you. Reach out for support, feedback, or collaborations.
        </p>

        <asp:Label ID="lblMsg" runat="server" CssClass="lux-msg"></asp:Label>

        <div class="lux-group">
            <label>Your Name</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="lux-input" />
        </div>

        <div class="lux-group">
            <label>Email Address</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="lux-input" />
        </div>

        <div class="lux-group">
            <label>Your Message</label>
            <asp:TextBox ID="txtMessage" runat="server"
                TextMode="MultiLine"
                Rows="5"
                CssClass="lux-input" />
        </div>

        <asp:Button ID="btnSend" runat="server"
            Text="Send Message"
            CssClass="lux-btn"
            OnClick="btnSend_Click" />

    </div>
</div>

</asp:Content>
