using EntityFramework.Extensions;
using FineUIPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.stock
{
    /// <summary>
    /// 采购品检
    /// </summary>
    public partial class purchasecheck : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid1();
            }
        }

        private void BindGrid1()
        {
            IQueryable<PurchaseStockList> q = MYDB.PurchaseStockLists;

            q = q.Where(u => u.PurchaseProperties == PurchaseProperties.采购入库);

            Grid1.RecordCount = q.Count();
            q = SortAndPage(q, Grid1);
            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        protected void btnNoPass_Click(object sender, EventArgs e)
        {
            UpdateStockList(result.不合格);
        }

        protected void btnPass_Click(object sender, EventArgs e)
        {
            UpdateStockList(result.合格);
        }

        private void UpdateStockList(result res)
        {
            List<int> ids = GetSelectedDataKeyIDs(Grid1);

            MYDB.PurchaseStockLists.Where(u => ids.Contains(u.SN)).Update(u => new PurchaseStockList
            {
                result = res,
                checkDate = DateTime.Now
            });
            BindGrid1();
        }
    }
}