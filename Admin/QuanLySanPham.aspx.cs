using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace ShopBanQuanAo.Admin
{
    public partial class QuanLySanPham : System.Web.UI.Page
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
            using (SqlConnection con = new SqlConnection(conn))
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SanPham", con))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvSanPham.DataSource = dt;
                gvSanPham.DataBind();
            }
        }

        // helper for TemplateField: build image URL or return placeholder
        protected string GetImageUrl(object fileNameObj)
        {
            if (fileNameObj == null || fileNameObj == DBNull.Value)
                return ResolveUrl("~/assets/no-image.png"); // provide a placeholder in project
            var fileName = fileNameObj.ToString();
            if (string.IsNullOrWhiteSpace(fileName))
                return ResolveUrl("~/assets/no-image.png");
            // ensure file exists (optional)
            var physical = Server.MapPath("~/uploads/products/" + fileName);
            if (!File.Exists(physical))
                return ResolveUrl("~/assets/no-image.png");
            return ResolveUrl("~/uploads/products/" + fileName);
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;

            // validate price
            decimal gia;
            if (!decimal.TryParse(txtGia.Text, out gia))
            {
                lblMsg.Text = "Giá không hợp lệ.";
                return;
            }

            string ten = (txtTen.Text ?? "").Trim();
            if (string.IsNullOrEmpty(ten))
            {
                lblMsg.Text = "Vui lòng nhập tên sản phẩm.";
                return;
            }

            // handle file upload
            string savedFileName = null;
            if (fuImage.HasFile)
            {
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                string ext = Path.GetExtension(fuImage.FileName).ToLowerInvariant();
                if (Array.IndexOf(allowed, ext) < 0)
                {
                    lblMsg.Text = "Chỉ cho phép tập tin: .jpg, .jpeg, .png, .gif";
                    return;
                }

                string uploads = Server.MapPath("~/uploads/products");
                try
                {
                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);

                    string fileName = Guid.NewGuid().ToString("N") + ext;
                    string full = Path.Combine(uploads, fileName);
                    fuImage.SaveAs(full);
                    savedFileName = fileName; // store filename (not full path)
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Lỗi khi lưu file: " + ex.Message;
                    return;
                }
            }

            // detect image column and insert accordingly (existing logic)
            string imageCol = null;
            try
            {
                using (var con = new SqlConnection(conn))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SanPham'", con))
                    using (var rdr = cmd.ExecuteReader())
                    {
                        var candidates = new[] { "HinhAnh", "Hinh", "ImagePath", "Image", "Anh", "AnhSP" };
                        var cols = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        while (rdr.Read())
                            cols.Add(rdr.GetString(0));

                        foreach (var c in candidates)
                        {
                            if (cols.Contains(c))
                            {
                                imageCol = c;
                                break;
                            }
                        }
                    }
                }
            }
            catch { }

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    if (!string.IsNullOrEmpty(imageCol) && savedFileName != null)
                    {
                        cmd.CommandText = $"INSERT INTO SanPham (TenSanPham, Gia, [{imageCol}]) VALUES (@ten, @gia, @img)";
                        cmd.Parameters.AddWithValue("@img", savedFileName);
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO SanPham (TenSanPham, Gia) VALUES (@ten, @gia)";
                    }

                    cmd.Parameters.AddWithValue("@ten", ten);
                    cmd.Parameters.AddWithValue("@gia", gia);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMsg.CssClass = "block mt-4 text-sm text-green-600";
                lblMsg.Text = "Thêm sản phẩm thành công.";
                if (string.IsNullOrEmpty(imageCol) && savedFileName != null)
                {
                    lblMsg.Text += " (Ảnh đã lưu nhưng không có cột lưu ảnh trong DB.)";
                }

                txtTen.Text = "";
                txtGia.Text = "";

                LoadSanPham();
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "block mt-4 text-sm text-red-600";
                lblMsg.Text = "Lỗi khi thêm sản phẩm: " + ex.Message;
            }
        }

        protected void gvSanPham_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvSanPham.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string check = "SELECT COUNT(*) FROM ChiTietDonHang WHERE MaSanPham=@id";
                SqlCommand cmdCheck = new SqlCommand(check, con);
                cmdCheck.Parameters.AddWithValue("@id", id);

                int count = (int)cmdCheck.ExecuteScalar();

                if (count > 0)
                {
                    Response.Write("<script>alert('Không thể xóa vì sản phẩm đã có trong đơn hàng');</script>");
                }
                else
                {
                    string sql = "DELETE FROM SanPham WHERE MaSanPham=@id";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Xóa sản phẩm thành công');</script>");
                }
            }

            LoadSanPham();
        }
    }
}