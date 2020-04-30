using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.stock
{
    public partial class purchasesummary_C : PageBase
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
            var q = from a in MYDB.purchaseplan
                    group a by new
                    {
                        a.PurPlanNo,
                        a.Provider,
                        a.Purchaser,
                    }
                      into g
                    select new
                    {
                        g.Key.PurPlanNo,
                        g.Key.Purchaser,
                        g.Key.Provider,
                        PDate = g.Max(u => u.PDate)
                    };

            if (dpstart.SelectedDate.HasValue && dpend.SelectedDate.HasValue)
            {
                DateTime dt1 = dpstart.SelectedDate.Value;
                DateTime dt2 = dpend.SelectedDate.Value.AddDays(1);

                q = q.Where(u => u.PDate >= dt1 && u.PDate <= dt2);
            }

            if (!string.IsNullOrEmpty(tbxsearch.Text))
            {
                q = q.Where(u => u.Provider.Contains(tbxsearch.Text) || u.Purchaser.Contains(tbxsearch.Text));
            }

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        protected void Window1_Close(object sender, FineUIPro.WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}