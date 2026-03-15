using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ShopBanQuanAo
{
    public partial class SanPham : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSanPham();
            }
        }

        void LoadSanPham()
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                string query = "SELECT MaSanPham, TenSanPham, Gia, HinhAnh FROM SanPham";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                
                try
                {
                    sqlConn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    
                    rptSanPham.DataSource = dt;
                    rptSanPham.DataBind();
                }
                catch (Exception ex)
                {
                    // Có thể thêm code xử lý lỗi ở đây nếu cần
                }
            }
        }

        protected void btnThemGio_Click(object sender, EventArgs e)
        {
            string ten = "Áo khoác bomber thời trang";
            int gia = 350000;
            string hinh = "HinhAnh/quan1.jpg";

            DataTable cart;

            if (Session["Cart"] == null)
            {
                cart = new DataTable();

                cart.Columns.Add("TenSP");
                cart.Columns.Add("Gia", typeof(int));
                cart.Columns.Add("SoLuong", typeof(int));
                cart.Columns.Add("HinhAnh");
                cart.Columns.Add("TongTien", typeof(int));

                Session["Cart"] = cart;
            }
            else
            {
                cart = (DataTable)Session["Cart"];
            }

            DataRow row = cart.NewRow();

            row["TenSP"] = ten;
            row["Gia"] = gia;
            row["SoLuong"] = 1;
            row["HinhAnh"] = hinh;
            row["TongTien"] = gia;

            cart.Rows.Add(row);

            Session["Cart"] = cart;

            Response.Redirect("Cart.aspx");
        }
    }
}