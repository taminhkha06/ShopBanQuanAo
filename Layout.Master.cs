using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ShopBanQuanAo
{
    public partial class Layout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateCartCount();
        }

        void UpdateCartCount()
        {
            int count = 0;
            
            if (Session["Cart"] != null)
            {
                DataTable cart = (DataTable)Session["Cart"];
                count = cart.Rows.Count;
            }
            
            lblCartCount.Text = count.ToString();
        }
    }
}