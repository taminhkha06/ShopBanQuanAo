<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="DangNhap.aspx.cs" Inherits="ShopBanQuanAo.DangNhap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script src="https://cdn.tailwindcss.com?plugins=forms"></script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="flex items-center justify-center min-h-screen bg-gray-100">

<div class="w-full max-w-md bg-white shadow-xl rounded-xl p-8">

<div class="text-center mb-8">
<h2 class="text-3xl font-bold text-gray-800">Đăng nhập</h2>
<p class="text-gray-500 text-sm">Chào mừng bạn quay lại KD STORE</p>
</div>


<!-- USERNAME -->

<div class="mb-5">

<label class="block text-sm font-semibold text-gray-700 mb-2">
Email / Tài khoản
</label>

<asp:TextBox 
ID="txtUser" 
runat="server"
CssClass="w-full border rounded-lg px-4 py-3 focus:ring-2 focus:ring-pink-500 outline-none"
placeholder="Nhập email hoặc tài khoản">
</asp:TextBox>

</div>


<!-- PASSWORD -->

<div class="mb-5">

<label class="block text-sm font-semibold text-gray-700 mb-2">
Mật khẩu
</label>

<asp:TextBox 
ID="txtPass" 
runat="server"
TextMode="Password"
CssClass="w-full border rounded-lg px-4 py-3 focus:ring-2 focus:ring-pink-500 outline-none"
placeholder="Nhập mật khẩu">
</asp:TextBox>

</div>


<!-- REMEMBER -->

<div class="flex justify-between items-center mb-6">

<label class="flex items-center gap-2 text-sm text-gray-600">

<input type="checkbox" class="rounded border-gray-300">

Ghi nhớ đăng nhập

</label>

<a href="#" class="text-pink-600 text-sm hover:underline">
Quên mật khẩu?
</a>

</div>


<!-- LOGIN BUTTON -->

<asp:Button 
ID="btnLogin" 
runat="server"
Text="Đăng nhập"
OnClick="btnLogin_Click"
CssClass="w-full bg-pink-600 hover:bg-pink-700 text-white font-bold py-3 rounded-lg transition"/>


<!-- REGISTER -->

<div class="text-center mt-6 text-sm">

<span class="text-gray-600">Chưa có tài khoản?</span>

<a href="DangKy.aspx" class="text-pink-600 font-semibold hover:underline ml-1">
Đăng ký ngay
</a>

</div>


</div>

</div>

</asp:Content>