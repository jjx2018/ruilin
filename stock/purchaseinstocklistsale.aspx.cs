﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.stock
{
    public partial class purchaseinstocklistsale : PageBase
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

                //int sn = GetQueryIntValue("sn");
                //根据销售单号查询采购入库情况
                string saleorderno = GetQueryValue("saleorderno");

                var inq = from a in appdb.purchaseorderDetail
                          join b in appdb.purchaseorderHeader on a.PurOrderNo equals b.PurOrderNo

                          where b.SaleOrderNo == saleorderno
                          select a;

                var q = from a in appdb.PurchaseStockLists
                        where  a.result == result.合格
                        group a by new
                        {
                            a.ItemNo,
                            a.PurOrderNo
                        }
                        into g
                        select new
                        {
                            g.Key.ItemNo,
                            g.Key.PurOrderNo,
                            InstockQuantity = g.Sum(u => u.Quantity == null ? 0 : u.Quantity),
                        };

                var qq = from a in inq
                         join b in q
                         on new { a.PurOrderNo, a.ItemNo } equals new { b.PurOrderNo, b.ItemNo }
                         into temp
                         from tt in temp.DefaultIfEmpty()
                         select new
                         {
                             a.SN,
                             a.InputeDate,
                             a.PurPlanNo,
                             a.PurOrderNo,
                             a.SaleOrderNo,
                             a.ProNo,
                             a.ProName,
                             a.ItemNo,
                             a.Unit,
                             a.ItemName,
                             a.Spec,
                             a.Quantity,
                             tt.InstockQuantity,
                             a.CheckDate,
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