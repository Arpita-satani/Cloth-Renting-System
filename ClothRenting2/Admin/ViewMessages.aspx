<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Admin/AdminMaster.master"
    CodeFile="ViewMessages.aspx.cs"
    Inherits="ClothRenting2.Admin.ViewMessages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2 class="page-title">📩 Contact Messages</h2>

<div class="admin-table-wrap">

    <asp:GridView ID="gvMessages" runat="server"
        AutoGenerateColumns="False"
        CssClass="admin-table"
        DataKeyNames="MessageID"
        OnRowCommand="gvMessages_RowCommand">

        <Columns>

            <asp:BoundField DataField="MessageID" HeaderText="ID" />

            <asp:BoundField DataField="Name" HeaderText="Name" />

            <asp:BoundField DataField="Email" HeaderText="Email" />

            <asp:BoundField DataField="Message" HeaderText="Message" />

            <asp:BoundField DataField="CreatedAt"
                HeaderText="Date"
                DataFormatString="{0:dd-MMM-yyyy}" />

            
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDelete" runat="server"
                        Text="Delete"
                        CssClass="btn-delete"
                        CommandName="DeleteMsg"
                        CommandArgument='<%# Eval("MessageID") %>'
                        OnClientClick="return confirm('Delete this message?');" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</div>

</asp:Content>
