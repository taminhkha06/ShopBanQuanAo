<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="SanPham.aspx.cs" Inherits="ShopBanQuanAo.SanPham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="text-center mb-10">
    <h1 class="text-3xl font-bold">Tất Cả Sản Phẩm</h1>
</div>

<div class="grid grid-cols-4 gap-6">

<asp:Repeater ID="rptSanPham" runat="server">

<ItemTemplate>

<a href='ChiTietSanPham.aspx?id=<%# Eval("MaSanPham") %>'>

<div class="border rounded-lg p-4 text-center bg-white">

<img src='HinhAnh/<%# Eval("HinhAnh") %>' 
class="w-full h-60 object-cover rounded" />

<h3 class="mt-3 font-semibold">
<%# Eval("TenSanPham") %>
</h3>

<p class="text-red-500 font-bold">
<%# Eval("Gia") %> đ
</p>

</div>

</a>

</ItemTemplate>

</asp:Repeater>

</div>

</asp:Content>