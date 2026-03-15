<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ShopBanQuanAo.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet"/>
<script src="https://cdn.tailwindcss.com"></script>

<style>
body { font-family: 'Plus Jakarta Sans', sans-serif; }
</style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="flex h-screen overflow-hidden">

<!-- SIDEBAR -->
<aside class="w-72 bg-white border-r flex flex-col">

<div class="p-6 flex items-center gap-3">
<h1 class="text-xl font-bold text-pink-600">KD STORE ADMIN</h1>
</div>

<nav class="flex-1 px-4 py-4 space-y-2">

<a href="Dashboard.aspx" class="flex items-center gap-3 px-4 py-3 rounded-xl bg-pink-600 text-white">
Dashboard
</a>

<a href="QuanLySanPham.aspx" class="flex items-center gap-3 px-4 py-3 rounded-xl hover:bg-pink-100">
Quản lý sản phẩm
</a>

<a href="QuanLyDonHang.aspx" class="flex items-center gap-3 px-4 py-3 rounded-xl hover:bg-pink-100">
Quản lý đơn hàng
</a>

<a href="../DangXuat.aspx" class="flex items-center gap-3 px-4 py-3 rounded-xl hover:bg-red-100">
Đăng xuất
</a>

</nav>

</aside>

<!-- MAIN -->
<main class="flex-1 p-8 bg-gray-100">

<h2 class="text-3xl font-bold mb-6">
Xin chào Admin 👋
</h2>

<!-- THỐNG KÊ -->

<div class="grid grid-cols-4 gap-6">

<div class="bg-white p-6 rounded-xl shadow">
<p class="text-gray-500">Tổng sản phẩm</p>
<h3 class="text-2xl font-bold text-pink-600">
<asp:Label ID="lblSanPham" runat="server" />
</h3>
</div>

<div class="bg-white p-6 rounded-xl shadow">
<p class="text-gray-500">Tổng đơn hàng</p>
<h3 class="text-2xl font-bold text-pink-600">
<asp:Label ID="lblDonHang" runat="server" />
</h3>
</div>

<div class="bg-white p-6 rounded-xl shadow">
<p class="text-gray-500">Khách hàng</p>
<h3 class="text-2xl font-bold text-pink-600">
<asp:Label ID="lblKhachHang" runat="server" />
</h3>
</div>

<div class="bg-white p-6 rounded-xl shadow">
<p class="text-gray-500">Doanh thu</p>
<h3 class="text-2xl font-bold text-pink-600">
<asp:Label ID="lblDoanhThu" runat="server" />
</h3>
</div>

</div>

</main>

</div>

</asp:Content>