<%@ Page Title="Sub Categories" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="SubCategories.aspx.cs"
    Inherits="ClothRenting2.SubCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container mt-5">
    <h2 class="text-center mb-4">Select Sub Category</h2>

    <div class="row">

        <asp:Repeater ID="rpSub" runat="server">
            <ItemTemplate>
                <div class="col-lg-3 col-md-4 mb-4">
                    <div class="category-card">

                        <a href='ProductList.aspx?cid=<%# Eval("CategoryId") %>'>
                            <img src='Admin/Images/<%# Eval("CategoryImage") %>' class="img-fluid" />

                            <h5><%# Eval("CategoryName") %></h5>
                        </a>

                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</div>

</asp:Content>
