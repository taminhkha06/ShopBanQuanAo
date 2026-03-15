using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace ShopBanQuanAo.Admin
{
    public partial class ChiTietDonHang : Page
    {
        string conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int maDon;
                if (int.TryParse(Request.QueryString["MaDon"], out maDon))
                {
                    LoadDetails(maDon);
                }
                else
                {
                    lblMessage.Text = "MaDon không hợp lệ hoặc không có trong querystring.";
                }
            }
        }

        void LoadDetails(int maDon)
        {
            // SELECT * để không phụ thuộc tên cột cố định
            string sql = @"SELECT * FROM ChiTietDonHang WHERE MaDonHang = @id";

            try
            {
                using (var con = new SqlConnection(conn))
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", maDon);
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var raw = new DataTable();
                        da.Fill(raw);

                        if (raw.Rows.Count == 0)
                        {
                            lblMessage.Text = "Không tìm thấy chi tiết cho đơn hàng này.";
                            gvChiTiet.DataSource = null;
                            gvChiTiet.DataBind();
                            return;
                        }

                        // Tạo bảng chuẩn để hiển thị (các BoundField trong .aspx mong đợi các tên này)
                        var dt = new DataTable();
                        dt.Columns.Add("MaSanPham", typeof(string));
                        dt.Columns.Add("TenSanPham", typeof(string));
                        dt.Columns.Add("Gia", typeof(decimal));
                        dt.Columns.Add("SoLuong", typeof(int));
                        dt.Columns.Add("TongTien", typeof(decimal));

                        // helper local functions
                        object GetFirstAvailable(DataRow r, params string[] names)
                        {
                            foreach (var n in names)
                            {
                                if (raw.Columns.Contains(n) && r[n] != DBNull.Value)
                                    return r[n];
                            }
                            return DBNull.Value;
                        }

                        decimal ParseDecimal(object o)
                        {
                            if (o == null || o == DBNull.Value) return 0m;
                            decimal v;
                            if (decimal.TryParse(o.ToString(), out v)) return v;
                            return 0m;
                        }

                        int ParseInt(object o)
                        {
                            if (o == null || o == DBNull.Value) return 0;
                            int v;
                            if (int.TryParse(o.ToString(), out v)) return v;
                            // sometimes quantity stored as decimal
                            decimal d;
                            if (decimal.TryParse(o.ToString(), out d)) return (int)d;
                            return 0;
                        }

                        foreach (DataRow r in raw.Rows)
                        {
                            var newRow = dt.NewRow();

                            // MaSanPham candidates
                            var maSp = GetFirstAvailable(r, "MaSanPham", "MaSP", "ProductId", "ProductID", "MaSanPhamCT");
                            newRow["MaSanPham"] = maSp == DBNull.Value ? (object)DBNull.Value : maSp.ToString();

                            // TenSanPham candidates
                            var ten = GetFirstAvailable(r, "TenSanPham", "TenSP", "ProductName", "Name");
                            newRow["TenSanPham"] = ten == DBNull.Value ? (object)DBNull.Value : ten.ToString();

                            // Gia candidates
                            var giaObj = GetFirstAvailable(r, "Gia", "DonGia", "Price", "UnitPrice");
                            decimal gia = ParseDecimal(giaObj);
                            newRow["Gia"] = gia;

                            // SoLuong candidates
                            var slObj = GetFirstAvailable(r, "SoLuong", "Quantity", "Qty");
                            int sl = ParseInt(slObj);
                            newRow["SoLuong"] = sl;

                            // TongTien candidates or compute
                            var ttObj = GetFirstAvailable(r, "TongTien", "ThanhTien", "Total", "Amount", "SubTotal");
                            decimal tt = ParseDecimal(ttObj);
                            if (tt == 0m)
                                tt = gia * sl;
                            newRow["TongTien"] = tt;

                            dt.Rows.Add(newRow);
                        }

                        // bind chuẩn
                        gvChiTiet.DataSource = dt;
                        gvChiTiet.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                // show clear error for dev
                lblMessage.Text = "Lỗi khi tải chi tiết: " + ex.Message;
                gvChiTiet.DataSource = null;
                gvChiTiet.DataBind();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuanLyDonHang.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}