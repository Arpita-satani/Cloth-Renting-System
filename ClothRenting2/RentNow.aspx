<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Site.Master"
    CodeFile="RentNow.aspx.cs"
    Inherits="ClothRenting2.RentNow" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<section style="padding-top:120px;">
<div class="container">
<div class="row justify-content-center">
<div class="col-lg-6">

<div class="card shadow p-4">
<h3 class="text-center mb-3">Rent This Product</h3>

<asp:Label runat="server" Text="Full Name" />
<asp:TextBox ID="txtName" runat="server" CssClass="form-control" ReadOnly="true" />
<br />

<asp:Label runat="server" Text="Mobile Number" />
<asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" />
<br />

<asp:Label runat="server" Text="Rent From" />
<asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"
    TextMode="Date"
    AutoPostBack="true"
    OnTextChanged="DateChanged" />
<br />

<asp:Label runat="server" Text="Rent To" />
<asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"
    TextMode="Date"
    AutoPostBack="true"
    OnTextChanged="DateChanged" />
<br />

<asp:Label runat="server" Text="Size" />
<asp:DropDownList ID="ddlSize" runat="server" CssClass="form-control">
    <asp:ListItem>S</asp:ListItem>
    <asp:ListItem>M</asp:ListItem>
    <asp:ListItem>L</asp:ListItem>
    <asp:ListItem>XL</asp:ListItem>
</asp:DropDownList>
<br />

<asp:Label runat="server" Text="Quantity" />
<asp:DropDownList ID="ddlQty" runat="server"
    CssClass="form-control"
    AutoPostBack="true"
    OnSelectedIndexChanged="ddlQty_SelectedIndexChanged">
    <asp:ListItem>1</asp:ListItem>
    <asp:ListItem>2</asp:ListItem>
    <asp:ListItem>3</asp:ListItem>
</asp:DropDownList>
<br />

<asp:Label runat="server" Text="Price (per day)" />
<asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" ReadOnly="true" />
<br />

<asp:Label runat="server" Text="Total Price" />
<asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" ReadOnly="true" />
<br />

<asp:Button ID="btnRentNow" runat="server"
    Text="Confirm Rent"
    CssClass="btn btn-primary w-100"
    OnClick="btnRentNow_Click" />

<br /><br />
<asp:Label ID="lblMsg" runat="server" />

</div>
</div>
</div>
</div>
</section>

</asp:Content>
