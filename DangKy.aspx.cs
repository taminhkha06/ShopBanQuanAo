using System;
using System.Data.SqlClient;

namespace ShopBanQuanAo
{
    public partial class DangKy : System.Web.UI.Page
    {
        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            string conn = @"Data Source=DESKTOP-GIK5D08;Initial Catalog=WebBanQuanAo;Integrated Security=True";

            SqlConnection con = new SqlConnection(conn);

            string sql = "INSERT INTO TaiKhoan(TenDangNhap,MatKhau,HoTen,Email,DienThoai,VaiTro) VALUES(@user,@pass,@name,@mail,@phone,'User')";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@user", txtUser.Text);
            cmd.Parameters.AddWithValue("@pass", txtPass.Text);
            cmd.Parameters.AddWithValue("@name", txtHoTen.Text);
            cmd.Parameters.AddWithValue("@mail", txtEmail.Text);
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            lblThongBao.Text = "Đăng ký thành công!";
        }
    }
}