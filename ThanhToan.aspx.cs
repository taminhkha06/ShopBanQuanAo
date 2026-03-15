using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;

namespace ShopBanQuanAo
{
    public partial class ThanhToan : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra giỏ hàng
            if (Session["Cart"] == null)
            {
                Response.Redirect("Cart.aspx");
            }

            if (!IsPostBack)
            {
                LoadDonHang();
            }
        }

        void LoadDonHang()
        {
            DataTable cart = Session["Cart"] as DataTable;

            if (cart != null && cart.Rows.Count > 0)
            {
                // The GridView in the .aspx expects a column named "TenSP".
                // Common cart column is "TenSanPham". Create a projection when needed.
                DataTable bindTable = cart;

                if (!cart.Columns.Contains("TenSP"))
                {
                    if (cart.Columns.Contains("TenSanPham"))
                    {
                        bindTable = new DataTable();

                        // Create columns expected by GridView (TenSP, Gia, SoLuong)
                        bindTable.Columns.Add("TenSP", typeof(string));
                        bindTable.Columns.Add("Gia", cart.Columns.Contains("Gia") ? cart.Columns["Gia"].DataType : typeof(decimal));
                        bindTable.Columns.Add("SoLuong", cart.Columns.Contains("SoLuong") ? cart.Columns["SoLuong"].DataType : typeof(int));

                        foreach (DataRow r in cart.Rows)
                        {
                            var newRow = bindTable.NewRow();
                            newRow["TenSP"] = r["TenSanPham"] ?? DBNull.Value;
                            newRow["Gia"] = cart.Columns.Contains("Gia") ? r["Gia"] ?? DBNull.Value : DBNull.Value;
                            newRow["SoLuong"] = cart.Columns.Contains("SoLuong") ? r["SoLuong"] ?? DBNull.Value : DBNull.Value;
                            bindTable.Rows.Add(newRow);
                        }
                    }
                    else
                    {
                        // Neither TenSP nor TenSanPham present — fall back to auto-generate or simple text
                        // Create minimal table to avoid DataBind exception
                        bindTable = new DataTable();
                        bindTable.Columns.Add("TenSP", typeof(string));
                        bindTable.Columns.Add("Gia", typeof(decimal));
                        bindTable.Columns.Add("SoLuong", typeof(int));

                        foreach (DataRow r in cart.Rows)
                        {
                            var newRow = bindTable.NewRow();
                            // attempt common alternatives
                            newRow["TenSP"] = cart.Columns.Contains("Name") ? r["Name"] : (cart.Columns.Contains("ProductName") ? r["ProductName"] : DBNull.Value);
                            newRow["Gia"] = cart.Columns.Contains("Gia") ? r["Gia"] : (cart.Columns.Contains("Price") ? r["Price"] : 0);
                            newRow["SoLuong"] = cart.Columns.Contains("SoLuong") ? r["SoLuong"] : (cart.Columns.Contains("Quantity") ? r["Quantity"] : 1);
                            bindTable.Rows.Add(newRow);
                        }
                    }
                }

                gvDonHang.DataSource = bindTable;
                gvDonHang.DataBind();

                decimal tong = 0;

                foreach (DataRow row in cart.Rows)
                {
                    decimal gia = 0;
                    int sl = 0;

                    if (cart.Columns.Contains("Gia") && row["Gia"] != DBNull.Value)
                        decimal.TryParse(row["Gia"].ToString(), out gia);
                    else if (cart.Columns.Contains("Price") && row["Price"] != DBNull.Value)
                        decimal.TryParse(row["Price"].ToString(), out gia);

                    if (cart.Columns.Contains("SoLuong") && row["SoLuong"] != DBNull.Value)
                        int.TryParse(row["SoLuong"].ToString(), out sl);
                    else if (cart.Columns.Contains("Quantity") && row["Quantity"] != DBNull.Value)
                        int.TryParse(row["Quantity"].ToString(), out sl);

                    tong += gia * sl;
                }

                lblTongTien.Text = tong.ToString("N0") + " VNĐ";
            }
            else
            {
                lblTongTien.Text = "0 VNĐ";
            }
        }

        protected void btnDatHang_Click(object sender, EventArgs e)
        {
            DataTable cart = Session["Cart"] as DataTable;

            if (cart == null || cart.Rows.Count == 0)
            {
                Response.Write("<script>alert('Giỏ hàng trống!');</script>");
                return;
            }

            if (string.IsNullOrEmpty(txtTen.Text) ||
                string.IsNullOrEmpty(txtPhone.Text) ||
                string.IsNullOrEmpty(txtDiaChi.Text))
            {
                Response.Write("<script>alert('Vui lòng nhập đầy đủ thông tin!');</script>");
                return;
            }

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                // TÍNH TỔNG TIỀN
                decimal tong = 0;

                foreach (DataRow r in cart.Rows)
                {
                    decimal gia = 0;
                    int sl = 0;

                    if (cart.Columns.Contains("Gia") && r["Gia"] != DBNull.Value)
                        decimal.TryParse(r["Gia"].ToString(), out gia);
                    else if (cart.Columns.Contains("Price") && r["Price"] != DBNull.Value)
                        decimal.TryParse(r["Price"].ToString(), out gia);

                    if (cart.Columns.Contains("SoLuong") && r["SoLuong"] != DBNull.Value)
                        int.TryParse(r["SoLuong"].ToString(), out sl);
                    else if (cart.Columns.Contains("Quantity") && r["Quantity"] != DBNull.Value)
                        int.TryParse(r["Quantity"].ToString(), out sl);

                    tong += gia * sl;
                }

                // PHƯƠNG THỨC THANH TOÁN
                string pt = "COD";

                if (rdCard.Checked)
                    pt = "Card";

                if (rdPaypal.Checked)
                    pt = "Paypal";

                // LƯU ĐƠN HÀNG
                // Build INSERT dynamically to include current date if DB has a date column
                HashSet<string> dhCols = GetTableColumns(con, "DonHang");
                string Choose(params string[] candidates) => candidates.FirstOrDefault(c => dhCols.Contains(c));

                var cols = new List<string>();
                var paramNames = new List<string>();
                var paramValues = new List<object>();
                int dpi = 0;
                void AddDh(string col, object val)
                {
                    cols.Add(col);
                    paramNames.Add("@d" + (dpi++));
                    paramValues.Add(val ?? DBNull.Value);
                }

                // required fields (attempt to add regardless; if column missing, we won't add)
                if (dhCols.Contains("TenKhachHang")) AddDh("TenKhachHang", txtTen.Text);
                if (dhCols.Contains("Email")) AddDh("Email", txtEmail.Text);
                if (dhCols.Contains("DienThoai")) AddDh("DienThoai", txtPhone.Text);
                if (dhCols.Contains("DiaChi")) AddDh("DiaChi", txtDiaChi.Text);
                if (dhCols.Contains("TongTien")) AddDh("TongTien", tong);
                if (dhCols.Contains("PhuongThucThanhToan")) AddDh("PhuongThucThanhToan", pt);

                // detect a date column and add current date
                string dateCol = Choose("NgayDat", "NgayDatHang", "NgayTao", "CreatedDate", "OrderDate", "Ngay", "NgayNhap");
                if (!string.IsNullOrEmpty(dateCol))
                {
                    AddDh(dateCol, DateTime.Now);
                }

                if (cols.Count == 0)
                {
                    // defensive: nothing to insert
                    con.Close();
                    Response.Write("<script>alert('Không tìm thấy cột thích hợp trên bảng DonHang. Vui lòng kiểm tra cấu trúc DB.');</script>");
                    return;
                }

                string sql = $"INSERT INTO DonHang ({string.Join(",", cols)}) OUTPUT INSERTED.MaDonHang VALUES ({string.Join(",", paramNames)})";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    for (int i = 0; i < paramNames.Count; i++)
                    {
                        cmd.Parameters.AddWithValue(paramNames[i], paramValues[i] ?? DBNull.Value);
                    }

                    int maDon = (int)cmd.ExecuteScalar();

                    // LƯU CHI TIẾT ĐƠN HÀNG (dynamic column mapping to avoid invalid column name exceptions)
                    // get DB columns for ChiTietDonHang
                    HashSet<string> ctCols = GetTableColumns(con, "ChiTietDonHang");

                    // candidates for each logical field (ordered by preference)
                    string ChooseCT(params string[] candidates) => candidates.FirstOrDefault(c => ctCols.Contains(c));

                    string dbColMaDon = ChooseCT("MaDonHang", "MaDon", "OrderId", "MaDonId");
                    string dbColMaSanPham = ChooseCT("MaSanPham", "ProductId", "ProductID", "MaSP");
                    string dbColTenSanPham = ChooseCT("TenSanPham", "TenSP", "ProductName", "Name");
                    string dbColGia = ChooseCT("Gia", "DonGia", "Price", "UnitPrice");
                    string dbColSoLuong = ChooseCT("SoLuong", "Quantity", "Qty");
                    string dbColTongTien = ChooseCT("TongTien", "ThanhTien", "Total", "Amount", "SubTotal");

                    foreach (DataRow r in cart.Rows)
                    {
                        decimal gia = 0;
                        int sl = 0;

                        if (cart.Columns.Contains("Gia") && r["Gia"] != DBNull.Value)
                            decimal.TryParse(r["Gia"].ToString(), out gia);
                        else if (cart.Columns.Contains("Price") && r["Price"] != DBNull.Value)
                            decimal.TryParse(r["Price"].ToString(), out gia);

                        if (cart.Columns.Contains("SoLuong") && r["SoLuong"] != DBNull.Value)
                            int.TryParse(r["SoLuong"].ToString(), out sl);
                        else if (cart.Columns.Contains("Quantity") && r["Quantity"] != DBNull.Value)
                            int.TryParse(r["Quantity"].ToString(), out sl);

                        // prepare values from cart
                        object valMaSanPham = DBNull.Value;
                        if (cart.Columns.Contains("MaSanPham"))
                            valMaSanPham = r["MaSanPham"];
                        else if (cart.Columns.Contains("ProductId"))
                            valMaSanPham = r["ProductId"];

                        object valTenSanPham = DBNull.Value;
                        if (cart.Columns.Contains("TenSanPham"))
                            valTenSanPham = r["TenSanPham"];
                        else if (cart.Columns.Contains("TenSP"))
                            valTenSanPham = r["TenSP"];
                        else if (cart.Columns.Contains("ProductName"))
                            valTenSanPham = r["ProductName"];
                        else if (cart.Columns.Contains("Name"))
                            valTenSanPham = r["Name"];

                        decimal tt = gia * sl;

                        // build column/param lists only with columns that exist in DB
                        var ccols = new List<string>();
                        var cparamNames = new List<string>();
                        var cparamValues = new List<object>();

                        int pi = 0;
                        void AddParam(string col, object value)
                        {
                            string pname = "@p" + (pi++);
                            ccols.Add(col);
                            cparamNames.Add(pname);
                            cparamValues.Add(value ?? DBNull.Value);
                        }

                        if (!string.IsNullOrEmpty(dbColMaDon)) AddParam(dbColMaDon, maDon);
                        if (!string.IsNullOrEmpty(dbColMaSanPham)) AddParam(dbColMaSanPham, valMaSanPham ?? DBNull.Value);
                        if (!string.IsNullOrEmpty(dbColTenSanPham)) AddParam(dbColTenSanPham, valTenSanPham ?? DBNull.Value);
                        if (!string.IsNullOrEmpty(dbColGia)) AddParam(dbColGia, gia);
                        if (!string.IsNullOrEmpty(dbColSoLuong)) AddParam(dbColSoLuong, sl);
                        if (!string.IsNullOrEmpty(dbColTongTien)) AddParam(dbColTongTien, tt);

                        if (ccols.Count == 0)
                        {
                            // Nothing to insert — skip to avoid errors
                            continue;
                        }

                        string insertSql = $"INSERT INTO ChiTietDonHang ({string.Join(",", ccols)}) VALUES ({string.Join(",", cparamNames)})";

                        using (SqlCommand cmdCT = new SqlCommand(insertSql, con))
                        {
                            for (int i = 0; i < cparamNames.Count; i++)
                            {
                                cmdCT.Parameters.AddWithValue(cparamNames[i], cparamValues[i] ?? DBNull.Value);
                            }

                            cmdCT.ExecuteNonQuery();
                        }
                    }
                }
                con.Close();
            }

            // XÓA GIỎ HÀNG
            Session["Cart"] = null;

            Response.Write("<script>alert('Đặt hàng thành công!');window.location='TrangChu.aspx'</script>");
        }

        private HashSet<string> GetTableColumns(SqlConnection con, string tableName)
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (SqlCommand cmd = new SqlCommand(@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @t", con))
            {
                cmd.Parameters.AddWithValue("@t", tableName);
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        set.Add(rdr.GetString(0));
                    }
                }
            }
            return set;
        }
    }
}