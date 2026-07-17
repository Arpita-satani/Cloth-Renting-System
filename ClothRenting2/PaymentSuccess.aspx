<%@ Page Title="Payment Success" Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeFile="PaymentSuccess.aspx.cs" Inherits="ClothRenting2.PaymentSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div style="text-align:center;margin-top:100px">

<h2 style="color:green">🎉 Payment Successful</h2>

<p>Your order has been placed successfully.</p>

<br>

<a href="MyOrders.aspx"
style="background:#007bff;color:white;padding:10px 20px;text-decoration:none;border-radius:5px">

View My Orders

</a>

</div>

</asp:Content>