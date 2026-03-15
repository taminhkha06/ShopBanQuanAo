<%@ Page Title="Thanh Toán" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ThanhToan.aspx.cs" Inherits="ShopBanQuanAo.ThanhToan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@300;400;500;600;700;800&display=swap" rel="stylesheet"/>

<style>
body{
font-family:'Plus Jakarta Sans',sans-serif;
}
.payment-radio:checked + label{
border-color:#e72361;
background-color:rgba(231,35,97,0.05);
}
</style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="flex-1 mx-auto w-full max-w-[1280px] px-6 py-10 lg:px-10">

<div class="mb-8">
<h2 class="text-3xl font-extrabold tracking-tight">Checkout</h2>

<div class="mt-2 flex items-center gap-2 text-sm text-slate-500">
<span>Cart</span>
<span>></span>
<span class="font-bold text-primary">Information & Payment</span>
</div>
</div>


<div class="grid grid-cols-1 gap-12 lg:grid-cols-12">


<!-- THÔNG TIN GIAO HÀNG -->
<div class="lg:col-span-7 space-y-10">

<section>

<div class="flex items-center gap-3 mb-6">
<div class="flex h-8 w-8 items-center justify-center rounded-full bg-primary text-white font-bold text-sm">
1
</div>
<h3 class="text-xl font-bold">Thông tin giao hàng</h3>
</div>

<div class="grid grid-cols-1 gap-4 sm:grid-cols-2">

<div class="sm:col-span-2">
<label>Họ và tên</label>
<asp:TextBox ID="txtTen" runat="server" CssClass="w-full rounded-lg border px-4 py-3"></asp:TextBox>
</div>

<div>
<label>Email</label>
<asp:TextBox ID="txtEmail" runat="server" CssClass="w-full rounded-lg border px-4 py-3"></asp:TextBox>
</div>

<div>
<label>Số điện thoại</label>
<asp:TextBox ID="txtPhone" runat="server" CssClass="w-full rounded-lg border px-4 py-3"></asp:TextBox>
</div>

<div class="sm:col-span-2">
<label>Địa chỉ</label>
<asp:TextBox ID="txtDiaChi" runat="server" CssClass="w-full rounded-lg border px-4 py-3"></asp:TextBox>
</div>

<div>
<label>Tỉnh</label>
<asp:DropDownList ID="ddlTinh" runat="server" CssClass="w-full rounded-lg border px-4 py-3">
<asp:ListItem>Hồ Chí Minh</asp:ListItem>
<asp:ListItem>Hà Nội</asp:ListItem>
<asp:ListItem>Đà Nẵng</asp:ListItem>
</asp:DropDownList>
</div>

<div>
<label>Quận</label>
<asp:DropDownList ID="ddlQuan" runat="server" CssClass="w-full rounded-lg border px-4 py-3">
<asp:ListItem>Quận 1</asp:ListItem>
<asp:ListItem>Quận 3</asp:ListItem>
<asp:ListItem>Tân Bình</asp:ListItem>
</asp:DropDownList>
</div>

</div>

</section>



<!-- PHƯƠNG THỨC THANH TOÁN -->
<section>

<div class="flex items-center gap-3 mb-6">
<div class="flex h-8 w-8 items-center justify-center rounded-full bg-primary text-white font-bold text-sm">
2
</div>

<h3 class="text-xl font-bold">Phương thức thanh toán</h3>
</div>


<div class="space-y-3">

<label class="flex items-center gap-3 border p-4 rounded-lg">
<asp:RadioButton ID="rdCOD" runat="server" GroupName="Payment" Checked="true"/>
<span>Thanh toán khi nhận hàng (COD)</span>
</label>

<label class="flex items-center gap-3 border p-4 rounded-lg">
<asp:RadioButton ID="rdCard" runat="server" GroupName="Payment"/>
<span>Thanh toán bằng thẻ</span>
</label>

<label class="flex items-center gap-3 border p-4 rounded-lg">
<asp:RadioButton ID="rdPaypal" runat="server" GroupName="Payment"/>
<span>Ví điện tử PayPal</span>
</label>

</div>

</section>

</div>



<!-- ĐƠN HÀNG -->
<div class="lg:col-span-5">

<div class="rounded-2xl bg-white p-6 shadow">

<h3 class="text-xl font-bold mb-6">
Đơn hàng
</h3>


<asp:GridView 
ID="gvDonHang" 
runat="server" 
AutoGenerateColumns="false"
CssClass="w-full">

<Columns>

<asp:BoundField DataField="TenSP" HeaderText="Sản phẩm"/>

<asp:BoundField DataField="Gia" HeaderText="Giá"/>

<asp:BoundField DataField="SoLuong" HeaderText="SL"/>

</Columns>

</asp:GridView>


<div class="mt-6 flex justify-between text-lg font-bold">
<span>Tổng tiền</span>

<asp:Label ID="lblTongTien" runat="server" Text="0 VNĐ"></asp:Label>

</div>


<asp:Button
ID="btnDatHang"
runat="server"
Text="Hoàn tất đặt hàng"
CssClass="mt-6 w-full bg-primary text-white py-3 rounded-lg"
OnClick="btnDatHang_Click"
/>


</div>

</div>

</div>

</div>

</asp:Content>