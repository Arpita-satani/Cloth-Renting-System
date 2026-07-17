<%@ Page Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Clothes.aspx.cs"
    Inherits="ClothRenting.Clothes" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="MainContent"
    runat="server">

<!-- PAGE HEADING -->
<section class="page-heading">
    <div class="container">
        <h2>Available Clothes</h2>
        <span>Choose clothes on rent</span>
    </div>
</section>

<!-- CLOTHES LIST -->
<section class="section">
    <div class="container">
        <div class="row">

            <!-- ITEM 1 -->
            <div class="col-lg-4 col-md-6">
                <div class="item">
                    <div class="thumb">
                        <img src="assets/images/men-01.jpg" alt="">
                    </div>
                    <div class="down-content">
                        <h4>Men Kurta</h4>
                        <span>₹300 / day</span><br />
                        <a href="Login.aspx" class="btn btn-primary btn-sm">
                            Rent Now
                        </a>
                    </div>
                </div>
            </div>

            <!-- ITEM 2 -->
            <div class="col-lg-4 col-md-6">
                <div class="item">
                    <div class="thumb">
                        <img src="assets/images/women-01.jpg" alt="">
                    </div>
                    <div class="down-content">
                        <h4>Women Lehenga</h4>
                        <span>₹500 / day</span><br />
                        <a href="Login.aspx" class="btn btn-primary btn-sm">
                            Rent Now
                        </a>
                    </div>
                </div>
            </div>

            <!-- ITEM 3 -->
            <div class="col-lg-4 col-md-6">
                <div class="item">
                    <div class="thumb">
                        <img src="assets/images/kid-01.jpg" alt="">
                    </div>
                    <div class="down-content">
                        <h4>Kids Dress</h4>
                        <span>₹200 / day</span><br />
                        <a href="Login.aspx" class="btn btn-primary btn-sm">
                            Rent Now
                        </a>
                    </div>
                </div>
            </div>

        </div>
    </div>
</section>

</asp:Content>

