<%@ Page Title="Manage Orders" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ManageOrders.aspx.cs" Inherits="ClothRenting2.Admin.ManageOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .admin-wrapper { padding: 20px; background: #f8f9fa; min-height: 100vh; font-family: 'Segoe UI', sans-serif; }
        .header-box { display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px; background: white; padding: 15px; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.05); }
        
        .order-table-container { background: white; border-radius: 10px; box-shadow: 0 4px 15px rgba(0,0,0,0.1); overflow: hidden; }
        .main-table { width: 100%; border-collapse: collapse; table-layout: fixed; } /* Fixed layout for alignment */
        .main-table th { background: #2c3e50; color: white; padding: 12px; text-align: left; font-size: 13px; }
        .main-table td { padding: 12px; border-bottom: 1px solid #eee; font-size: 13px; vertical-align: top; word-wrap: break-word; }

        .status-badge { padding: 5px 10px; border-radius: 15px; font-size: 11px; font-weight: 700; text-transform: uppercase; display: inline-block; }
        .pending { background: #fff3cd; color: #856404; border: 1px solid #ffeeba; }
        .completed { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
        .cancelled { background: #f8d7da; color: #842029; }

        .item-list { width: 100%; background: #f9f9f9; border: 1px solid #ddd; font-size: 11px; border-collapse: collapse; }
        .item-list th { background: #eee; color: #333; padding: 4px; }
        .item-list td { padding: 4px; border: 1px solid #ddd; }

        .action-container { display: flex; flex-direction: column; gap: 6px; }
        .ddl-custom { padding: 5px; border-radius: 4px; border: 1px solid #ccc; width: 100%; }
        .btn-action { padding: 6px; border: none; border-radius: 4px; color: white; cursor: pointer; font-size: 12px; font-weight: 600; }
        .btn-save { background: #28a745; }
        .btn-delete { background: #dc3545; }
    </style>

    <div class="admin-wrapper">
        <div class="header-box">
            <h2 style="margin:0;">📦 Order Management</h2>
            <span>Total Orders: <asp:Label ID="lblTotal" runat="server" Font-Bold="true" color="#007bff" /></span>
        </div>

        <div class="order-table-container">
            <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="main-table" 
                DataKeyNames="OrderID" OnRowCommand="gvOrders_RowCommand" OnRowDataBound="gvOrders_RowDataBound" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="OrderID" HeaderText="ID" ItemStyle-Width="40px" />
                    
                    <asp:TemplateField HeaderText="Customer" ItemStyle-Width="180px">
                        <ItemTemplate>
                            <strong><%# Eval("UserName") %></strong><br />
                            <small style="color:#666;"><%# Eval("Email") %></small>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dates" ItemStyle-Width="140px">
                        <ItemTemplate>
                            <%# Eval("StartDate", "{0:dd MMM}") %> - <%# Eval("EndDate", "{0:dd MMM yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Payment" ItemStyle-Width="120px">
                        <ItemTemplate>
                            <div style="font-weight:bold; color:#2c3e50;">₹ <%# Eval("TotalAmount", "{0:N2}") %></div>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' CssClass="status-badge" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Product Details">
                        <ItemTemplate>
                            <asp:Repeater ID="rptItems" runat="server">
                                <HeaderTemplate>
                                    <table class="item-list">
                                        <tr><th>Product Name</th><th>Qty</th><th>Size</th></tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("ProductName") %></td>
                                        <td><%# Eval("Quantity") %></td>
                                        <td><%# Eval("Size") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></table></FooterTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Actions" ItemStyle-Width="130px">
                        <ItemTemplate>
                            <div class="action-container">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl-custom">
                                    <asp:ListItem>Pending</asp:ListItem>
                                    <asp:ListItem>Completed</asp:ListItem>
                                    <asp:ListItem>Cancelled</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="btnSave" runat="server" Text="Update" CommandName="UpdateStatus" CommandArgument='<%# Eval("OrderID") %>' CssClass="btn-action btn-save" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteOrder" CommandArgument='<%# Eval("OrderID") %>' CssClass="btn-action btn-delete" OnClientClick="return confirm('Truly delete this order?');" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>