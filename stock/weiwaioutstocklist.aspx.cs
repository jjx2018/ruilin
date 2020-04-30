using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.stock
{
    public partial class weiwaioutstocklist : PageBase
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

                int sn = GetQueryIntValue("sn");
                string sendoutorderno = GetQueryValue("sendoutorderno");

                var inq = from a in  MYDB.sendoutprocessdetail
                          join d in MYDB.bomdtl
                          on a.BomSN equals d.SN
                           where a.FSN == sn
                           select new
                           {
                               a.SN,
                               d.SurfaceDeal,
                               a.InputeDate,
                               a.SendOutPlanNo,
                               a.SendOutOrderNo,
                               a.SaleOrderNo,
                               a.ProNo,
                               a.ProName,
                               a.ItemNo,
                               a.Unit,
                               a.ItemName,
                               a.Spec,
                               a.Quantity,
                               a.Remark
                           };

                var q = from a in MYDB.SendOutStockLists
                        where a.SendOutOrderNo == sendoutorderno
                        group a by new
                        {
                            a.ItemNo,
                            a.SendOutOrderNo,
                        }
                        into g
                        select new
                        {
                            g.Key.ItemNo,
                            g.Key.SendOutOrderNo,
                            //委外进仓
                            InstockQuantity = g.Sum(u => u.Mark == "02" ? u.Quantity: 0  ),
                            //委外出仓
                            OutstockQuantity = g.Sum(u => u.Mark == "03" ? u.Quantity: 0),
                        };

                var qq = from a in inq
                         join b in q
                         on new { a.SendOutOrderNo, a.ItemNo } equals new { b.SendOutOrderNo, b.ItemNo }
                         into temp
                         from tt in temp.DefaultIfEmpty()
                         select new
                         {
                             a.SN,
                             a.SurfaceDeal,
                             a.InputeDate,
                             a.SendOutPlanNo,
                             a.SendOutOrderNo,
                             a.SaleOrderNo,
                             a.ProNo,
                             a.ProName,
                             a.ItemNo,
                             a.Unit,
                             a.ItemName,
                             a.Spec,
                             a.Quantity,
                             tt.InstockQuantity,
                             tt.OutstockQuantity,
                             a.Remark
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {

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