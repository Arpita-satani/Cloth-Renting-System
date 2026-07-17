<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Site.master"
    CodeFile="Feedback.aspx.cs"
    Inherits="ClothRenting2.Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="lux-page">

    <div class="lux-card">

        <h1 class="lux-title">Feedback</h1>
        <p class="lux-sub">
            Your feedback helps us improve our services.
        </p>

        <asp:Label ID="lblMsg" runat="server" CssClass="lux-msg"></asp:Label>

        <div class="lux-group">
            <label>Your Name</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="lux-input" />
        </div>

        <div class="lux-group">
            <label>Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="lux-input" />
        </div>

        <div class="lux-group">
            <label>Your Feedback</label>
            <asp:TextBox ID="txtFeedback" runat="server"
                TextMode="MultiLine" Rows="5"
                CssClass="lux-input" />
        </div>

        <asp:Button ID="btnSubmit" runat="server"
            Text="Submit Feedback"
            CssClass="lux-btn"
            OnClick="btnSubmit_Click" />

    </div>

</div>

</asp:Content>
