using System;
using System.Data.SqlClient;
using System.Configuration;

namespace ShopBanQuanAo.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadThongKe();
            }
        }

        void LoadThongKe()
        {
            conn.Open();

            // Tổng sản phẩm
            SqlCommand sp = new SqlCommand("SELECT COUNT(*) FROM SanPham", conn);
            lblSanPham.Text = sp.ExecuteScalar().ToString();

            // Tổng đơn hàng
            SqlCommand dh = new SqlCommand("SELECT COUNT(*) FROM DonHang", conn);
            lblDonHang.Text = dh.ExecuteScalar().ToString();

            // Khách hàng
            SqlCommand kh = new SqlCommand("SELECT COUNT(*) FROM TaiKhoan WHERE VaiTro='User'", conn);
            lblKhachHang.Text = kh.ExecuteScalar().ToString();

            // Doanh thu
            SqlCommand dt = new SqlCommand("SELECT ISNULL(SUM(TongTien),0) FROM DonHang", conn);
            lblDoanhThu.Text = Convert.ToDecimal(dt.ExecuteScalar()).ToString("N0") + " VNĐ";

            conn.Close();
        }
    }
}