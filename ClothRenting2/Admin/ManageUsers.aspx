<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs"
Inherits="ClothRenting2.Admin.ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style>
.table-box { margin-top: 20px; padding: 15px; background: #fff; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
.table { width: 100%; border-collapse: collapse; }
.table th { background-color: #f8f9fa; color: #333; padding: 12px; border-bottom: 2px solid #dee2e6; }
.table td { padding: 12px; border-bottom: 1px solid #dee2e6; vertical-align: middle; }
.status-active { color: #28a745; font-weight: bold; }
.status-blocked { color: #dc3545; font-weight: bold; }
.btn-action { text-decoration: none; padding: 5px 10px; border-radius: 4px; font-size: 14px; }
</style>

<h2>Manage Users</h2>

<div class="table-box">

<asp:GridView ID="gvUsers" runat="server"
AutoGenerateColumns="false"
CssClass="table"
OnRowCommand="gvUsers_RowCommand">

<Columns>

<asp:BoundField DataField="UserID" HeaderText="ID" />

<asp:BoundField DataField="FullName" HeaderText="Name" />

<asp:BoundField DataField="Email" HeaderText="Email" />

<asp:BoundField DataField="Mobile" HeaderText="Mobile" />

<asp:TemplateField HeaderText="Status">
<ItemTemplate>
<span class='<%# Convert.ToBoolean(Eval("IsActive")) ? "status-active" : "status-blocked" %>'>
<%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "Blocked" %>
</span>
</ItemTemplate>
</asp:TemplateField>

<asp:BoundField DataField="CreatedDate"
HeaderText="Created On"
DataFormatString="{0:dd-MMM-yyyy}" />

<asp:TemplateField HeaderText="Action">
<ItemTemplate>

<asp:LinkButton ID="lnkBlock"
runat="server"
CommandName="BlockUser"
CommandArgument='<%# Eval("UserID") %>'
CssClass="btn-action"
Text='<%# Convert.ToBoolean(Eval("IsActive")) ? "Block" : "Unblock" %>' />

&nbsp; | &nbsp;

<asp:LinkButton ID="lnkDelete"
runat="server"
CommandName="DeleteUser"
CommandArgument='<%# Eval("UserID") %>'
CssClass="btn-action"
ForeColor="Red"
Text="Delete"
OnClientClick="return confirm('Are you sure to delete this user?');" />

</ItemTemplate>
</asp:TemplateField>

</Columns>

</asp:GridView>

</div>

</asp:Content>