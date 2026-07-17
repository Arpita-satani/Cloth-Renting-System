<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/Admin/AdminMaster.master"
    CodeFile="ViewFeedback.aspx.cs"
    Inherits="ClothRenting2.Admin.ViewFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2 class="page-title">⭐ User Feedback</h2>

<div class="admin-table-wrap">

    <asp:GridView ID="gvFeedback" runat="server"
        AutoGenerateColumns="False"
        CssClass="admin-table"
        OnRowCommand="gvFeedback_RowCommand">

        <Columns>

            <asp:BoundField DataField="FeedbackID" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="FeedbackText" HeaderText="Feedback" />
            <asp:BoundField DataField="CreatedAt"
                HeaderText="Date"
                DataFormatString="{0:dd-MMM-yyyy}" />

            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDelete" runat="server"
                        Text="Delete"
                        CssClass="btn-delete"
                        CommandName="DeleteFeedback"
                        CommandArgument='<%# Eval("FeedbackID") %>'
                        OnClientClick="return confirm('Delete this feedback?');" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</div>

</asp:Content>

