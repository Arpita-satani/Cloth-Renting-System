<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Categories.aspx.cs" Inherits="ClothRenting2.Categories" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Categories</title>

    <!-- CLIENT CSS LINK -->
    <link href="Assets/css/site.css" rel="stylesheet" />
</head>
<body>
<form runat="server">

<div class="container">
    <h2>All Categories</h2>

    <asp:Repeater ID="rptCategories" runat="server">
        <ItemTemplate>
            <div class="cat-box">
                <a href='Products.aspx?catid=<%# Eval("CategoryID") %>'>
                    <%# Eval("CategoryName") %>
                </a>
            </div>
        </ItemTemplate>
    </asp:Repeater>

</div>

</form>
</body>
</html>


