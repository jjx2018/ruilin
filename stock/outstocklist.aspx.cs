using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.stock
{
    public partial class outstocklist : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string stockheaderprosn = GetQueryValue("stockheaderprosn");
            var q = from a in MYDB.ProductStockLists
                    where a.StockHeaderProsn == stockheaderprosn
                    select a
                  ;
            Grid1.DataSource = q;
            Grid1.DataBind();



        }
    }
}