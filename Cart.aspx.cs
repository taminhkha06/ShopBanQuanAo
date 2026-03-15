using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace ShopBanQuanAo
{
    public partial class Cart : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCart();
            }
        }

        void LoadCart()
        {
            if (Session["Cart"] != null)
            {
                DataTable cart = (DataTable)Session["Cart"];
                
                // Debug: hiển thị số sản phẩm trong giỏ hàng
                if (cart.Rows.Count == 0)
                {
                    Response.Write("<script>alert('Giỏ hàng đang trống!');</script>");
                    return;
                }

                rptCart.DataSource = cart;
                rptCart.DataBind();

                int tong = 0;

                foreach (DataRow row in cart.Rows)
                {
                    int gia = Convert.ToInt32(row["Gia"]);
                    int sl = Convert.ToInt32(row["SoLuong"]);

                    tong += gia * sl;
                }

                lblTongTien.Text = tong.ToString("N0") + " đ";
            }
            else
            {
                lblTongTien.Text = "0 đ";
                Response.Write("<script>alert('Session giỏ hàng đang null!');</script>");
            }
        }



        protected void btnCommand(object sender, CommandEventArgs e)
        {
            if (Session["Cart"] == null) return;

            DataTable cart = (DataTable)Session["Cart"];

            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "delete")
            {
                cart.Rows.RemoveAt(index);
            }

            if (e.CommandName == "update")
            {
                RepeaterItem item = rptCart.Items[index];

                TextBox txt = (TextBox)item.FindControl("txtSoLuong");

                int soluong;

                if (!int.TryParse(txt.Text, out soluong))
                {
                    soluong = 1;
                }

                if (soluong < 1)
                    soluong = 1;

                cart.Rows[index]["SoLuong"] = soluong;
            }

            Session["Cart"] = cart;

            LoadCart();
        }



        protected void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (Session["Cart"] != null)
            {
                DataTable cart = (DataTable)Session["Cart"];

                if (cart.Rows.Count > 0)
                {
                    Response.Redirect("ThanhToan.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Giỏ hàng trống!');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Giỏ hàng trống!');</script>");
            }
        }
    }
}