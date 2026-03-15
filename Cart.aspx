<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="ShopBanQuanAo.Cart" %>

<!DOCTYPE html>
<html lang="vi">
<head runat="server">

<meta charset="utf-8"/>
<title>Giỏ hàng</title>

<script src="https://cdn.tailwindcss.com"></script>

<link href="https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet"/>

<style>
body{
font-family:'Plus Jakarta Sans',sans-serif;
}
</style>

</head>

<body class="bg-gray-50">

<form id="form1" runat="server">

<div class="max-w-6xl mx-auto mt-12">

<h2 class="text-3xl font-bold mb-8">Shopping Cart</h2>


<div class="bg-white shadow rounded-lg overflow-hidden">

<table class="w-full">

<thead class="border-b bg-gray-100">
<tr class="text-left text-sm font-bold">

<th class="p-4">Sản phẩm</th>
<th class="p-4">Giá</th>
<th class="p-4">Số lượng</th>
<th class="p-4">Tổng</th>
<th class="p-4"></th>

</tr>
</thead>


<tbody>

<asp:Repeater ID="rptCart" runat="server">

<ItemTemplate>

<tr class="border-b">

<td class="p-4">

<div class="flex items-center gap-4">

<img src='<%# Eval("HinhAnh") %>' width="70" class="rounded"/>

<div>

<b><%# Eval("TenSanPham") %></b>

</div>

</div>

</td>


<td class="p-4 text-red-500 font-bold">

<%# Eval("Gia","{0:N0}") %> đ

</td>


<td class="p-4">

<asp:TextBox
ID="txtSoLuong"
runat="server"
Text='<%# Eval("SoLuong") %>'
CssClass="border rounded w-16 text-center"
/>

</td>


<td class="p-4 font-bold text-pink-600">

<%# Convert.ToInt32(Eval("Gia")) * Convert.ToInt32(Eval("SoLuong")) %> đ

</td>


<td class="p-4 space-x-2">

<asp:Button
ID="btnCapNhat"
runat="server"
Text="Cập nhật"
CssClass="bg-blue-500 text-white px-3 py-1 rounded"
CommandName="update"
CommandArgument='<%# Container.ItemIndex %>'
OnCommand="btnCommand"
/>

<asp:Button
ID="btnXoa"
runat="server"
Text="Xóa"
CssClass="bg-red-500 text-white px-3 py-1 rounded"
CommandName="delete"
CommandArgument='<%# Container.ItemIndex %>'
OnCommand="btnCommand"
/>

</td>

</tr>

</ItemTemplate>

</asp:Repeater>

</tbody>

</table>

</div>


<div class="flex justify-between items-center mt-6">

<a href="SanPham.aspx" class="text-blue-500 font-semibold">
← Tiếp tục mua sắm
</a>


<h3 class="text-xl font-bold">

Tổng tiền:

<asp:Label ID="lblTongTien" runat="server" CssClass="text-red-600"></asp:Label>

</h3>

</div>


<div class="mt-6 text-right">

<asp:Button 
ID="btnThanhToan" 
runat="server" 
Text="Thanh toán"
OnClick="btnThanhToan_Click"
CssClass="bg-pink-600 text-white px-6 py-3 rounded-lg font-bold"
/>

</div>

</div>

</form>

</body>
</html>