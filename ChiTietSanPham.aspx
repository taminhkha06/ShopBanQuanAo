<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ChiTietSanPham.aspx.cs" Inherits="ShopBanQuanAo.ChiTietSanPham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script src="https://cdn.tailwindcss.com"></script>

<link href="https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet"/>

<style>
body{
    font-family:'Plus Jakarta Sans',sans-serif;
}
</style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="max-w-6xl mx-auto mt-12">

<div class="grid grid-cols-1 md:grid-cols-2 gap-10 bg-white p-8 rounded-xl shadow">

<!-- HÌNH SẢN PHẨM -->

<div class="flex justify-center">

<asp:Image 
ID="imgSanPham" 
runat="server" 
CssClass="w-full max-w-md rounded-lg shadow"
/>

</div>


<!-- THÔNG TIN SẢN PHẨM -->

<div>

<h1 class="text-3xl font-bold mb-4">

<asp:Label ID="lblTenSanPham" runat="server"></asp:Label>

</h1>


<div class="text-2xl font-bold text-pink-600 mb-6">

<asp:Label ID="lblGia" runat="server"></asp:Label>

</div>


<div class="text-gray-600 leading-relaxed mb-6">

<b>Mô tả:</b><br />

<asp:Label ID="lblMoTa" runat="server"></asp:Label>

</div>


<!-- SIZE -->

<div class="mb-6">

<p class="font-semibold mb-2">Kích cỡ</p>

<div class="flex gap-3">

<span class="border px-4 py-1 rounded">S</span>
<span class="border px-4 py-1 rounded">M</span>
<span class="border px-4 py-1 rounded">L</span>
<span class="border px-4 py-1 rounded">XL</span>

</div>

</div>


<!-- SỐ LƯỢNG -->

<div class="mb-6">

<p class="font-semibold mb-2">Số lượng</p>

<asp:TextBox
ID="txtSoLuong"
runat="server"
Text="1"
CssClass="border rounded w-20 text-center p-2"
/>

</div>


<!-- BUTTON -->

<asp:Button
ID="btnThemGioHang"
runat="server"
Text="Thêm vào giỏ hàng"
OnClick="btnThemGioHang_Click"
CssClass="bg-pink-600 hover:bg-pink-700 text-white font-bold px-8 py-3 rounded-lg transition"
/>


</div>

</div>

</div>

</asp:Content>