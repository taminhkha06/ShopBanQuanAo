<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangKy.aspx.cs" Inherits="ShopBanQuanAo.DangKy" %>

<!DOCTYPE html>

<html lang="vi">
<head runat="server">

<meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0"/>

<script src="https://cdn.tailwindcss.com"></script>

<link href="https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@400;600;700&display=swap" rel="stylesheet"/>

<title>Đăng ký | KD STORE</title>

</head>

<body class="bg-gray-100 min-h-screen flex items-center justify-center">

<form id="form1" runat="server">

<div class="w-full max-w-lg bg-white rounded-xl shadow-xl p-10">

<div class="text-center mb-8">
<h2 class="text-3xl font-bold">Đăng ký tài khoản</h2>
<p class="text-gray-500">Tạo tài khoản mới tại KD Store</p>
</div>

<!-- HỌ TÊN -->

<div class="mb-4">

<label class="block text-sm font-semibold mb-2">Họ và tên</label>

<asp:TextBox 
ID="txtHoTen" 
runat="server"
CssClass="w-full border rounded-lg px-4 py-3 focus:ring-2 focus:ring-pink-500"
placeholder="Nhập họ và tên">
</asp:TextBox>

</div>

<!-- USERNAME -->

<div class="mb-4">

<label class="block text-sm font-semibold mb-2">Tên đăng nhập</label>

<asp:TextBox 
ID="txtUser" 
runat="server"
CssClass="w-full border rounded-lg px-4 py-3 focus:ring-2 focus:ring-pink-500"
placeholder="Nhập tên đăng nhập">
</asp:TextBox>

</div>

<!-- EMAIL -->

<div class="mb-4">

<label class="block text-sm font-semibold mb-2">Email</label>

<asp:TextBox 
ID="txtEmail" 
runat="server"
CssClass="w-full border rounded-lg px-4 py-3 focus:ring-2 focus:ring-pink-500"
placeholder="Nhập email">
</asp:TextBox>

</div>

<!-- PHONE -->

<div class="mb-4">

<label class="block text-sm font-semibold mb-2">Số điện thoại</label>

<asp:TextBox 
ID="txtPhone" 
runat="server"
CssClass="w-full border rounded-lg px-4 py-3 focus:ring-2 focus:ring-pink-500"
placeholder="Nhập số điện thoại">
</asp:TextBox>

</div>

<!-- PASSWORD -->

<div class="mb-4">

<label class="block text-sm font-semibold mb-2">Mật khẩu</label>

<asp:TextBox 
ID="txtPass" 
runat="server"
TextMode="Password"
CssClass="w-full border rounded-lg px-4 py-3 focus:ring-2 focus:ring-pink-500"
placeholder="Nhập mật khẩu">
</asp:TextBox>

</div>

<!-- CONFIRM PASSWORD -->

<div class="mb-6">

<label class="block text-sm font-semibold mb-2">Xác nhận mật khẩu</label>

<asp:TextBox 
ID="txtConfirm" 
runat="server"
TextMode="Password"
CssClass="w-full border rounded-lg px-4 py-3 focus:ring-2 focus:ring-pink-500"
placeholder="Nhập lại mật khẩu">
</asp:TextBox>

</div>

<!-- BUTTON -->

<asp:Button 
ID="btnDangKy" 
runat="server"
Text="Đăng ký"
OnClick="btnDangKy_Click"
CssClass="w-full bg-pink-600 hover:bg-pink-700 text-white font-bold py-3 rounded-lg"/>

<br /><br />

<asp:Label 
ID="lblThongBao" 
runat="server"
CssClass="text-center text-red-500 block">
</asp:Label>

<div class="text-center mt-6">

<span class="text-gray-600">Đã có tài khoản?</span>

<a href="DangNhap.aspx" class="text-pink-600 font-semibold hover:underline">
Đăng nhập ngay
</a>

</div>

</div>

</form>

</body>
</html>