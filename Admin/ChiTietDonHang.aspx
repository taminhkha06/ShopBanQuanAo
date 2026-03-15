<%@ Page Title="Chi tiết đơn hàng" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ChiTietDonHang.aspx.cs" Inherits="ShopBanQuanAo.Admin.ChiTietDonHang" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.tailwindcss.com"></script>
</asp:Content>

<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-8">
        <div class="flex justify-between items-center mb-6">
            <div>
                <h2 class="text-3xl font-bold">Chi tiết đơn hàng</h2>
                <p class="text-gray-500">Xem các sản phẩm của đơn</p>
            </div>
            <asp:Button ID="btnBack" runat="server" Text="Quay lại" CssClass="bg-gray-200 px-4 py-2 rounded" OnClick="btnBack_Click" />
        </div>

        <asp:Label ID="lblMessage" runat="server" CssClass="block mb-4 text-sm text-red-600"></asp:Label>

        <div class="bg-white rounded-xl shadow overflow-hidden p-4">
            <asp:GridView ID="gvChiTiet" runat="server" AutoGenerateColumns="False" CssClass="min-w-full text-sm" EmptyDataText="Không có chi tiết">
                <Columns>
                    <asp:BoundField DataField="MaSanPham" HeaderText="Mã SP" ItemStyle-CssClass="px-4 py-2" />
                    <asp:BoundField DataField="TenSanPham" HeaderText="Tên sản phẩm" ItemStyle-CssClass="px-4 py-2" />
                    <asp:BoundField DataField="Gia" HeaderText="Giá" DataFormatString="{0:N0} đ" ItemStyle-CssClass="px-4 py-2" />
                    <asp:BoundField DataField="SoLuong" HeaderText="Số lượng" ItemStyle-CssClass="px-4 py-2" />
                    <asp:BoundField DataField="TongTien" HeaderText="Thành tiền" DataFormatString="{0:N0} đ" ItemStyle-CssClass="px-4 py-2" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>