<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="TrangChu.aspx.cs" Inherits="ShopBanQuanAo.TrangChu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- Tailwind CSS -->
    <script src="https://cdn.tailwindcss.com"></script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- TIÊU ĐỀ -->
    <div class="text-center mb-10">
        <h1 class="text-4xl font-bold">KD Fashion</h1>
        <p class="text-gray-500 mt-2">Thời trang hiện đại dành cho bạn</p>
    </div>

    <!-- BANNER -->
    <div class="mb-10">
        <img src="HinhAnh/banner.jpg" class="w-full rounded-lg" />
    </div>

    <!-- SẢN PHẨM NỔI BẬT -->
    <h2 class="text-2xl font-bold mb-6">Sản phẩm nổi bật</h2>

    <div class="grid grid-cols-4 gap-6">

        <!-- SẢN PHẨM 1 -->
        <div class="border rounded-lg p-4 text-center bg-white">
            <img src="HinhAnh/ao1.jpg" class="w-full h-60 object-cover rounded" />
            <h3 class="mt-3 font-semibold">Áo Thun Nam</h3>
            <p class="text-red-500 font-bold">250.000đ</p>
        </div>

        <!-- SẢN PHẨM 2 -->
        <div class="border rounded-lg p-4 text-center bg-white">
            <img src="HinhAnh/ao2.jpg" class="w-full h-60 object-cover rounded" />
            <h3 class="mt-3 font-semibold">Áo Khoác</h3>
            <p class="text-red-500 font-bold">450.000đ</p>
        </div>

        <!-- SẢN PHẨM 3 -->
        <div class="border rounded-lg p-4 text-center bg-white">
            <img src="HinhAnh/ao3.jpg" class="w-full h-60 object-cover rounded" />
            <h3 class="mt-3 font-semibold">Áo Hoodie</h3>
            <p class="text-red-500 font-bold">390.000đ</p>
        </div>

        <!-- SẢN PHẨM 4 -->
        <div class="border rounded-lg p-4 text-center bg-white">
            <img src="HinhAnh/ao4.jpg" class="w-full h-60 object-cover rounded" />
            <h3 class="mt-3 font-semibold">Áo Sơ Mi</h3>
            <p class="text-red-500 font-bold">320.000đ</p>
        </div>

    </div>

</asp:Content>