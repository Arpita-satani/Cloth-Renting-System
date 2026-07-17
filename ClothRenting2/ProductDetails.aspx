<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="ProductDetails.aspx.cs"
    Inherits="ClothRenting2.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container mt-5">
    <div class="row">

        <div class="col-md-5">
            <asp:Image ID="imgProduct" runat="server" CssClass="img-fluid rounded" />
        </div>

        <div class="col-md-7">
            <h2 id="lblName" runat="server"></h2>

            <h4 class="text-success mt-3">
                ₹ <span id="lblPrice" runat="server"></span> / Day
            </h4>

            <p class="mt-4" id="lblDescription" runat="server"></p>

            <a href="#" class="btn btn-dark btn-lg mt-3">Rent Now</a>
        </div>

    </div>
</div>

</asp:Content>
