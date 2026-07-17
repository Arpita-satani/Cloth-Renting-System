<%@ Page Title="Manage Products" Language="C#" 
    MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" 
    CodeFile="ManageProducts.aspx.cs" 
    Inherits="ClothRenting2.Admin.ManageProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2 class="page-title">Manage Products</h2>
<link href="../Assets/css/admin-dashboard.css" rel="stylesheet" />
    <link href="../Assets/css/site.css" rel="stylesheet" />


<!-- FORM -->
<div class="form-wrapper">
    <div class="form-box">

        <asp:HiddenField ID="hfProductID" runat="server" />

        <!-- ONLY SUB CATEGORY -->
        <label>Sub Category</label>
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="input-box"></asp:DropDownList>

        <label>Product Name</label>
        <asp:TextBox ID="txtProductName" runat="server" CssClass="input-box"></asp:TextBox>

        <label>Description</label>
<asp:TextBox ID="txtDescription" runat="server" 
    TextMode="MultiLine" 
    CssClass="input-box"></asp:TextBox>



        <label>Price (Per Day)</label>
        <asp:TextBox ID="txtPrice" runat="server" CssClass="input-box"></asp:TextBox>

        <label>Product Image</label>
        <asp:FileUpload ID="fuImage" runat="server" />

        <asp:Button ID="btnSave" runat="server" 
            Text="Save Product" 
            CssClass="btn"
            OnClick="btnSave_Click" />

    </div>
</div>

<!-- GRID -->
<div class="table-box">

    <asp:GridView ID="gvProducts" runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ProductID"
        CssClass="data-table"
        OnRowCommand="gvProducts_RowCommand">

        <Columns>

            <asp:BoundField DataField="ProductID" HeaderText="ID" />
            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
            <asp:BoundField DataField="CategoryName" HeaderText="Sub Category" />
            <asp:BoundField DataField="Price" HeaderText="Price / Day" />

            
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <img src='<%# "../Admin/Images/" + Eval("Image") %>' 
                         width="80" height="90" 
                         style="object-fit:cover;border-radius:6px;" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="Edit"
                        CssClass="grid-btn"
                        CommandName="EditRow"
                        CommandArgument='<%# Eval("ProductID") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete"
                        CssClass="grid-btn delete"
                        CommandName="DeleteRow"
                        CommandArgument='<%# Eval("ProductID") %>' />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</div>

</asp:Content>
