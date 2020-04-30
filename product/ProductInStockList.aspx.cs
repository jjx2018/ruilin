using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.product
{
    public partial class ProductInStockList : PageBase
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

            using (var appdb = new AppContext())
            {

                int sn = GetQueryIntValue("sn");
                string proorderno = GetQueryValue("proorderno");

                var inq = from a in appdb.produceorderdetail
                          join b in appdb.produceorderheader on a.ProOrderNo equals b.ProOrderNo

                          where b.SN == sn
                          select new
                          {
                              a.SN,
                              a.InputeDate,
                              a.Quantity,
                              a.ProOrderNo,
                              a.ProPlanNo,
                              a.ProNo,
                              a.ProName,
                              a.ItemNo,
                              a.Unit,
                              a.ItemName,
                              a.Spec,
                              //a.CheckDate,
                              a.Remark,
                              a.SaleOrderNo,
                              //b.Provider
                          };

                var q = from a in appdb.ProductStockLists
                        where a.ProductOrderNo == proorderno
                        group a by new
                        {
                            a.ItemNo,
                            a.ProductOrderNo
                        }
                        into g
                        select new
                        {
                            g.Key.ItemNo,
                            g.Key.ProductOrderNo,
                            InstockQuantity = g.Sum(u => u.Quantity == null ? 0 : u.Quantity),
                        };

                var qq = from a in inq
                         join b in q
                         on new { a.ProOrderNo, a.ItemNo } equals new { ProOrderNo=b.ProductOrderNo, b.ItemNo }
                         into temp
                         from tt in temp.DefaultIfEmpty()
                         select new
                         {
                             a.SN,
                             a.InputeDate,
                             a.ProPlanNo,
                             a.ProOrderNo,
                             a.SaleOrderNo,
                             a.ProNo,
                             a.ProName,
                             a.ItemNo,
                             a.Unit,
                             a.ItemName,
                             a.Spec,
                             a.Quantity,
                             tt.InstockQuantity,
                             //a.CheckDate,
                             a.Remark,
                             //a.Provider,
                         };


                string searchText = tbxSearch.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    qq = qq.Where(u => u.ItemNo.Contains(searchText) || u.ItemName.Contains(searchText));
                }
                // 进仓 出仓

                //日期 筛选
                if (DateFrom.SelectedDate.HasValue)
                {
                    qq = qq.Where(u => u.InputeDate >= DateFrom.SelectedDate);
                }
                if (DateTo.SelectedDate.HasValue)
                {
                    qq = qq.Where(u => u.InputeDate <= DateTo.SelectedDate.Value.AddDays(1));
                }


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = qq.Count();// q.Count();

                // 排列和数据库分页
                qq = SortAndPage(qq, Grid1);

                Grid1.DataSource = qq;// itemq.Take(2);// q;
                Grid1.DataBind();
            }


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {

        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {

        }

        protected void Grid1_RowCommand(object sender, FineUIPro.GridCommandEventArgs e)
        {

        }

        protected void Window1_Close(object sender, FineUIPro.WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}