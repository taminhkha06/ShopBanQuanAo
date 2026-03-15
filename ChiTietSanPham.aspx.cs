using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ShopBanQuanAo
{
    public partial class ChiTietSanPham : System.Web.UI.Page
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
            string id = Request.QueryString["id"];

            SqlConnection ketnoi = new SqlConnection(conn);

            string sql = "SELECT * FROM SanPham WHERE MaSanPham=@id";

            SqlCommand cmd = new SqlCommand(sql, ketnoi);
            cmd.Parameters.AddWithValue("@id", id);

            ketnoi.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                lblTenSanPham.Text = dr["TenSanPham"].ToString();
                lblGia.Text = dr["Gia"].ToString() + " đ";
                lblMoTa.Text = dr["MoTa"].ToString();
                imgSanPham.ImageUrl = "HinhAnh/" + dr["HinhAnh"].ToString();
            }

            ketnoi.Close();
        }

        protected void btnThemGioHang_Click(object sender, EventArgs e)
        {
            int soLuong = Convert.ToInt32(txtSoLuong.Text);
            decimal gia =decimal.Parse(lblGia.Text.Replace("đ", "").Replace(",", "").Trim());

            

            DataTable gioHang;

            if (Session["Cart"] == null)
            {
                gioHang = new DataTable();
                gioHang.Columns.Add("MaSanPham");
                gioHang.Columns.Add("TenSanPham");
                gioHang.Columns.Add("Gia", typeof(decimal));
                gioHang.Columns.Add("SoLuong", typeof(int));
                gioHang.Columns.Add("HinhAnh");
                gioHang.Columns.Add("TongTien", typeof(decimal));
            }
            else
            {
                gioHang = (DataTable)Session["Cart"];
            }

            DataRow row = gioHang.NewRow();
            row["TenSanPham"] = lblTenSanPham.Text;
            row["MaSanPham"] = Request.QueryString["id"];
            row["Gia"] = gia;
            row["SoLuong"] = soLuong;
            row["TongTien"] = gia * soLuong;
            row["HinhAnh"] = imgSanPham.ImageUrl;

            gioHang.Rows.Add(row);

            Session["Cart"] = gioHang;

            Response.Redirect("Cart.aspx");
        }
    }
}