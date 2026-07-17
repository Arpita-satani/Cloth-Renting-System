<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Admin/AdminMaster.master"
    CodeFile="ManageCategories.aspx.cs"
    Inherits="ClothRenting2.Admin.ManageCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2>Manage Categories</h2>
    <link href="../Assets/css/admin-dashboard.css" rel="stylesheet" />


<div class="form-wrapper">
    <div class="form-box">

        <asp:HiddenField ID="hfCategoryID" runat="server" />

        <label>Parent Category</label>
        <asp:DropDownList ID="ddlParent" runat="server"></asp:DropDownList>

        <label>Category Name</label>
        <asp:TextBox ID="txtCategoryName" runat="server"></asp:TextBox>

        <label>Category Image</label>
        <asp:FileUpload ID="fuImage" runat="server" />

        <asp:Button ID="btnSave" runat="server"
            Text="Save Category"
            CssClass="btn"
            OnClick="btnSave_Click" />

    </div>
</div>

<div class="table-box">

    <asp:GridView ID="gvCategories" runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="CategoryID"
        OnRowCommand="gvCategories_RowCommand">

        <Columns>
            <asp:BoundField DataField="CategoryID" HeaderText="ID" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
            <asp:BoundField DataField="ParentName" HeaderText="Parent Category" />
            
            <asp:TemplateField HeaderText="Image">
    <ItemTemplate>
        <img src='Images/<%# Eval("CategoryImage") %>' 
             style="height:70px;width:70px;object-fit:cover;border-radius:6px;" />
    </ItemTemplate>
</asp:TemplateField>


            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="Edit"
                        CommandName="EditRow"
                        CommandArgument='<%# Eval("CategoryID") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete"
                        CommandName="DeleteRow"
                        CommandArgument='<%# Eval("CategoryID") %>' />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</div>

</asp:Content>
