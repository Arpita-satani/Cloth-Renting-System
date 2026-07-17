<%@ Page Title="Secure Payment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ClothRenting2.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style>

.payment-container{
width:900px;
margin:40px auto;
display:flex;
gap:30px;
font-family:'Poppins',sans-serif;
}

.box{
flex:1;
background:#fff;
padding:25px;
border-radius:12px;
box-shadow:0 4px 20px rgba(0,0,0,0.08);
border:1px solid #f0f0f0;
}

.title{
font-size:20px;
font-weight:600;
margin-bottom:25px;
color:#222;
border-bottom:2px solid #f9f9f9;
padding-bottom:12px;
}

.summary-item{
display:flex;
justify-content:space-between;
align-items:center;
margin-bottom:15px;
font-size:15px;
}

.summary-item span:first-child{
color:#666;
}

.summary-item span:last-child{
font-weight:600;
color:#333;
}

.total-row{
border-top:1px solid #eee;
padding-top:15px;
margin-top:10px;
color:#28a745;
font-size:18px;
font-weight:bold;
}

.input-group{
margin-bottom:18px;
}

.label-text{
display:block;
margin-bottom:6px;
font-size:14px;
font-weight:500;
color:#555;
}

.input{
width:100%;
padding:12px;
border:1px solid #ddd;
border-radius:8px;
box-sizing:border-box;
font-size:14px;
}

.error-msg{
color:red;
font-size:12px;
}

.btn-pay{
background:#28a745;
color:white;
padding:15px;
border:none;
border-radius:8px;
font-size:17px;
font-weight:600;
cursor:pointer;
width:100%;
}

</style>

<h2 style="text-align:center;margin:30px 0;">Secure Checkout</h2>

<div class="payment-container">

<!-- Order Summary -->

<div class="box">

<div class="title">Order Summary</div>

<div class="summary-item">
<span>Order ID:</span>
<span>#<asp:Label ID="lblOrderID" runat="server"/></span>
</div>

<div class="summary-item">
<span>Product:</span>
<span><asp:Label ID="lblProductName" runat="server"/></span>
</div>

<div class="summary-item">
<span>Price Per Day:</span>
<span>₹ <asp:Label ID="lblPrice" runat="server"/></span>
</div>

<div class="summary-item">
<span>Rental Days:</span>
<span><asp:Label ID="lblDays" runat="server"/> Days</span>
</div>

<div class="summary-item total-row">
<span>Total Amount:</span>
<span>₹ <asp:Label ID="lblAmount" runat="server"/></span>
</div>

</div>

<!-- Payment Box -->

<div class="box">

<div class="title">Payment Method</div>

<div class="input-group">

<asp:DropDownList ID="ddlMethod" runat="server"
CssClass="input"
AutoPostBack="true"
OnSelectedIndexChanged="ddlMethod_SelectedIndexChanged">

<asp:ListItem Value="">-- Select Method --</asp:ListItem>
<asp:ListItem>UPI</asp:ListItem>
<asp:ListItem>Credit Card</asp:ListItem>
<asp:ListItem>Debit Card</asp:ListItem>

</asp:DropDownList>

</div>

<!-- CARD PANEL -->

<asp:Panel ID="pnlCard" runat="server" Visible="false">

<div class="input-group">

<label class="label-text">Card Number</label>

<asp:TextBox ID="txtCard" runat="server"
CssClass="input"
MaxLength="16"></asp:TextBox>

<asp:RegularExpressionValidator
ID="revCard"
runat="server"
ControlToValidate="txtCard"
ValidationExpression="^[0-9]{16}$"
ErrorMessage="Card must be 16 digits"
CssClass="error-msg"/>

</div>

<div style="display:flex;gap:15px;">

<div class="input-group" style="flex:1">

<label class="label-text">Expiry</label>

<asp:TextBox ID="txtExpiry" runat="server"
CssClass="input"
placeholder="MM/YY"></asp:TextBox>

<asp:RegularExpressionValidator
ID="revExpiry"
runat="server"
ControlToValidate="txtExpiry"
ValidationExpression="^(0[1-9]|1[0-2])\/([0-9]{2})$"
ErrorMessage="Format MM/YY"
CssClass="error-msg"/>

</div>

<div class="input-group" style="flex:1">

<label class="label-text">CVV</label>

<asp:TextBox ID="txtCVV"
runat="server"
CssClass="input"
TextMode="Password"
MaxLength="3"></asp:TextBox>

<asp:RegularExpressionValidator
ID="revCVV"
runat="server"
ControlToValidate="txtCVV"
ValidationExpression="^[0-9]{3}$"
ErrorMessage="3 digit CVV"
CssClass="error-msg"/>

</div>

</div>

</asp:Panel>

<!-- UPI PANEL -->

<asp:Panel ID="pnlUPI" runat="server"
Visible="false"
style="text-align:center">

<p>Scan UPI QR</p>

<img src="assets/images/upi_qr.png"
width="160"/>

<p><b>UPI ID:</b> clothrent@upi</p>

</asp:Panel>

<asp:Button
ID="btnPay"
runat="server"
Text="Confirm & Pay"
CssClass="btn-pay"
OnClick="btnPay_Click"/>

</div>

</div>

</asp:Content>