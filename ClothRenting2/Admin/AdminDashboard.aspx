<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="AdminDashboard.aspx.cs"
    Inherits="ClothRenting2.Admin.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2 style="margin-bottom:20px;">Admin Dashboard</h2>

<div class="dashboard-cards">

    <!-- USERS -->
    <div class="dash-card users">
        <div class="icon">👥</div>
        <div class="info">
            <h4>Total Users</h4>
            <p><asp:Label ID="lblUsers" runat="server" Text="0"></asp:Label></p>
        </div>
    </div>

    <!-- PRODUCTS -->
    <div class="dash-card products">
        <div class="icon">👗</div>
        <div class="info">
            <h4>Total Products</h4>
            <p><asp:Label ID="lblProducts" runat="server" Text="0"></asp:Label></p>
        </div>
    </div>

    <!-- ORDERS -->
    <div class="dash-card orders"> 
        <div class="icon">📦</div>
        <div class="info">
            <h4>Total Orders</h4>
            <p><asp:Label ID="lblOrders" runat="server" Text="0"></asp:Label></p>
        </div>
    </div>

    <!-- REVENUE -->
    <div class="dash-card revenue">
        <div class="icon">💰</div>
        <div class="info">
            <h4>Total Revenue</h4>
            <p>₹ <asp:Label ID="lblRevenue" runat="server" Text="0"></asp:Label></p>
        </div>
    </div>

</div>

</asp:Content>
