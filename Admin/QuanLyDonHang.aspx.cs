using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ShopBanQuanAo.Admin
{
    public partial class QuanLyDonHang : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDonHang();
            }
        }

        void LoadDonHang()
        {
            // LEFT JOIN on MaNguoiDung and alias result column as KhachHang
            string sql = @"
                SELECT 
                    DonHang.MaDonHang,
                    COALESCE(TaiKhoan.TenDangNhap, DonHang.TenKhachHang) AS KhachHang,
                    DonHang.NgayDat,
                    DonHang.TongTien
                FROM DonHang
                LEFT JOIN TaiKhoan ON DonHang.MaNguoiDung = TaiKhoan.MaTaiKhoan
                ORDER BY DonHang.NgayDat DESC";

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        lblMessage.Text = "Không tìm thấy đơn hàng nào. Kiểm tra dữ liệu trong DonHang.";
                    }
                    else
                    {
                        lblMessage.Text = $"Tìm thấy {dt.Rows.Count} đơn hàng.";
                    }

                    // verify expected columns exist
                    string[] expected = new[] { "MaDonHang", "KhachHang", "NgayDat", "TongTien" };
                    foreach (var col in expected)
                    {
                        if (!dt.Columns.Contains(col))
                        {
                            lblMessage.Text += $" Thiếu cột '{col}' trong kết quả.";
                        }
                    }

                    gvDonHang.DataSource = dt;
                    gvDonHang.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi khi tải đơn hàng: " + ex.Message;
            }
        }
    }
}