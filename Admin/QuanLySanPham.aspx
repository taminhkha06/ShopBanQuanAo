<%@ Page Title="Quản lý sản phẩm" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="QuanLySanPham.aspx.cs" Inherits="ShopBanQuanAo.Admin.QuanLySanPham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="p-8">

<!-- Title -->
<div class="flex justify-between items-center mb-6">
    <div>
        <h2 class="text-3xl font-bold">Danh sách sản phẩm</h2>
        <p class="text-gray-500">Quản lý sản phẩm của cửa hàng</p>
    </div>

    <asp:Button 
        ID="btnThem"
        runat="server"
        Text="Thêm sản phẩm"
        CssClass="bg-pink-600 text-white px-5 py-2 rounded-lg"
        OnClick="btnThem_Click"/>
</div>

<!-- Form thêm -->
<div class="bg-white shadow rounded-xl p-6 mb-6">

<div class="grid grid-cols-3 gap-4">

<div>
<label>Tên sản phẩm</label>
<asp:TextBox ID="txtTen" runat="server" CssClass="w-full border p-2 rounded"></asp:TextBox>
</div>

<div>
<label>Giá</label>
<asp:TextBox ID="txtGia" runat="server" CssClass="w-full border p-2 rounded"></asp:TextBox>
</div>

<div>
<label>Hình ảnh</label>
<asp:FileUpload ID="fuImage" runat="server" CssClass="w-full" />
<p class="text-xs text-gray-500 mt-1">Cho phép: .jpg .jpeg .png .gif. Kích thước tối đa phụ thuộc cấu hình server.</p>
</div>

<div class="flex items-end">
<asp:Button 
ID="btnLuu"
runat="server"
Text="Lưu"
CssClass="bg-green-600 text-white px-5 py-2 rounded"
OnClick="btnThem_Click"/>
</div>

</div>

<!-- feedback -->
<asp:Label ID="lblMsg" runat="server" CssClass="block mt-4 text-sm text-red-600"></asp:Label>

</div>

<!-- Bảng sản phẩm -->

<div class="bg-white rounded-xl shadow overflow-hidden">

<asp:GridView 
ID="gvSanPham"
runat="server"
AutoGenerateColumns="False"
DataKeyNames="MaSanPham"
CssClass="w-full"
OnRowDeleting="gvSanPham_RowDeleting">

<HeaderStyle CssClass="bg-gray-100 text-left" />

<RowStyle CssClass="border-b" />

<Columns>

<asp:BoundField 
DataField="MaSanPham" 
HeaderText="ID" />

<asp:BoundField 
DataField="TenSanPham" 
HeaderText="Tên sản phẩm" />

<asp:BoundField 
DataField="Gia" 
HeaderText="Giá" 
DataFormatString="{0:N0} VNĐ" />

<%-- TemplateField hiển thị ảnh --%>
<asp:TemplateField HeaderText="Hình">
    <ItemTemplate>
        <asp:Image ID="imgThumb" runat="server" CssClass="h-16 w-16 object-cover rounded" 
            ImageUrl='<%# GetImageUrl(Eval("HinhAnh")) %>' AlternateText="No image" />
    </ItemTemplate>
    <ItemStyle CssClass="px-4 py-2" />
</asp:TemplateField>

<asp:CommandField 
ShowDeleteButton="True"
DeleteText="Xóa" />

</Columns>

</asp:GridView>

</div>

</div>

</asp:Content>