<%@ Page Title="Quản lý đơn hàng" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="QuanLyDonHang.aspx.cs" Inherits="ShopBanQuanAo.Admin.QuanLyDonHang" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.tailwindcss.com"></script>
</asp:Content>

<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="p-8">
        <div class="flex justify-between items-center mb-6">
            <div>
                <h2 class="text-3xl font-bold">Quản lý đơn hàng</h2>
                <p class="text-gray-500">Theo dõi và xử lý đơn hàng KD STORE</p>
            </div>
        </div>

        <asp:Label ID="lblMessage" runat="server" CssClass="block mb-4 text-sm text-red-600"></asp:Label>

        <div class="bg-white shadow rounded-xl overflow-hidden">
            <asp:GridView 
                ID="gvDonHang" 
                runat="server" 
                AutoGenerateColumns="False"
                CssClass="min-w-full text-sm text-left"
                HeaderStyle-CssClass="bg-gray-100"
                RowStyle-CssClass="border-b"
                DataKeyNames="MaDonHang"
                >

                <Columns>
                    <asp:BoundField DataField="MaDonHang" HeaderText="Mã đơn hàng" ItemStyle-CssClass="px-6 py-4 font-bold text-pink-600" />
                    <asp:BoundField DataField="KhachHang" HeaderText="Khách hàng" ItemStyle-CssClass="px-6 py-4" />
                    <asp:BoundField DataField="NgayDat" HeaderText="Ngày đặt" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="px-6 py-4" />
                    <asp:BoundField DataField="TongTien" HeaderText="Tổng tiền" DataFormatString="{0:N0} đ" ItemStyle-CssClass="px-6 py-4 font-semibold" />
                    <asp:TemplateField HeaderText="Thao tác">
                        <ItemTemplate>
                            <a href='ChiTietDonHang.aspx?MaDon=<%# Eval("MaDonHang") %>' class="text-blue-600 hover:underline">Xem chi tiết</a>
                        </ItemTemplate>
                        <ItemStyle CssClass="px-6 py-4" />
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>
    </div>
</asp:Content>