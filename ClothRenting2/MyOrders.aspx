<%@ Page Title="My Orders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="ClothRenting2.MyOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .orders-container { width: 95%; margin: 40px auto; font-family: 'Segoe UI', sans-serif; }
        .page-title { font-size: 26px; font-weight: 600; margin-bottom: 20px; color: #333; text-align: center; }
        
        .grid-style { width: 100%; background-color: #fff; border-collapse: collapse; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 15px rgba(0,0,0,0.05); }
        .grid-style th { background-color: #333; color: white; padding: 15px; text-align: left; font-weight: 500; }
        .grid-style td { padding: 12px 15px; border-bottom: 1px solid #eee; font-size: 14px; color: #555; }
        
        .status-badge { padding: 5px 12px; border-radius: 20px; font-size: 12px; font-weight: bold; text-transform: uppercase; }
        .status-pending { background-color: #fff3cd; color: #856404; }
        .status-completed { background-color: #d4edda; color: #155724; }
        
        .no-data { text-align: center; padding: 50px; color: #999; font-style: italic; }
    </style>

    <div class="orders-container">
        <h2 class="page-title">🛍️ My Rental Orders</h2>

        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="grid-style" 
            GridLines="None" OnRowDataBound="gvOrders_RowDataBound">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                <asp:BoundField DataField="StartDate" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="EndDate" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <strong>₹ <%# Eval("TotalAmount", "{0:0.00}") %></strong>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' CssClass="status-badge" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlPay" runat="server" Text="Pay Now" 
                            NavigateUrl='<%# "Payment.aspx?OrderID=" + Eval("OrderID") %>' 
                            Visible='<%# Eval("Status").ToString() == "Pending" %>' 
                            ForeColor="#007bff" Font-Bold="true" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <div class="no-data">Aapne abhi tak koi order nahi kiya hai.</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
