using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace ShopBanQuanAo
{
    public partial class DangNhap : Page
    {
        // Chuỗi kết nối lấy từ Web.config
        string conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Không cần xử lý gì khi load trang
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
      
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string sql = "SELECT * FROM TaiKhoan WHERE TenDangNhap=@u AND MatKhau=@p";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@u", user);
                cmd.Parameters.AddWithValue("@p", pass);

                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    Session["User"] = rd["TenDangNhap"].ToString();
                    Session["VaiTro"] = rd["VaiTro"].ToString();

                    if (rd["VaiTro"].ToString() == "Admin")
                    {
                        Response.Redirect("Admin/Dashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("TrangChu.aspx");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Sai tài khoản hoặc mật khẩu');</script>");
                }
            }
        }
    }

}