<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="ProductList.aspx.cs"
    Inherits="ClothRenting2.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container mt-5">
    <h2 class="text-center mb-4">Products</h2>

    <div class="row">

        <asp:Repeater ID="rpProducts" runat="server">
            <ItemTemplate>
                <div class="col-lg-3 col-md-4 mb-4">
                    <div class="product-card shadow-sm p-3 rounded">

                        
                        <a href='ProductDetails.aspx?id=<%# Eval("ProductID") %>'>
                            <img src='Admin/Images/<%# Eval("Image") %>' 
                                 class="img-fluid product-img" />
                        </a>

                        <h6 class="mt-2"><%# Eval("ProductName") %></h6>
                        <p class="price">₹ <%# Eval("Price") %> / Day</p>

                        <a href='RentNow.aspx?id=<%# Eval("ProductID") %>'
                           class="btn btn-primary btn-sm mt-2 w-100">
                            Rent Now
                        </a>

                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>

  
    <div class="text-center mt-4">
        <a href="Default.aspx" class="btn btn-outline-dark">Back to Home</a>
    </div>

</div>

</asp:Content>
