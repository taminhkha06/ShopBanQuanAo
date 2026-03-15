using System;
using System.Data;

namespace ShopBanQuanAo
{
    public partial class TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSanPham();
            }
        }

        void LoadSanPham()
        {
            // Code load sản phẩm cho trang chủ
            // Có thể thêm sau nếu cần
        }
    }
}