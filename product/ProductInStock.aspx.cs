using FineUIPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.product
{
    /// <summary>
    /// 生产入库
    /// </summary>
    public partial class ProductInStock :PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }


        private void BindGrid()
        {
            using (var appdb = new AppContext())
            {
                var qqq = from a in appdb.produceorderheader

                          select a;

                var prodetail = from a in appdb.produceorderdetail
                                select a;

                var proinstocklist = from a in appdb.ProductStockLists
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

                var qq = from a in prodetail
                         join b in proinstocklist
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
                             //采购数
                             a.Quantity,
                             //入库数
                             InstockQuantity = tt.InstockQuantity == null ? 0 : tt.InstockQuantity,
                             //a.CheckDate,
                             a.Remark
                         };

                //统一该采购单号下应采购的合计数，已入库的合计数
                var purGroup = from a in qq
                               group a by a.ProOrderNo
                              into g
                               select new
                               {
                                   PurOrderNo = g.Key,
                                   Quantity = g.Sum(u => u.Quantity),
                                   InstockQuantity = g.Sum(u => u.InstockQuantity),
                               };


                var res = from a in qqq
                          join b in purGroup
                          on a.ProOrderNo equals b.PurOrderNo
                          select new
                          {
                              a.SN,
                              a.ProOrderNo,
                              a.SaleOrderNo,
                              a.InputeDate,
                              b.Quantity,
                              b.InstockQuantity,
                              //完成状态
                              State = (double)b.InstockQuantity >= b.Quantity ? "已完成" : "未完成"
                          };

                string state = rblState.SelectedValue;
                if (state != "全部")
                {
                    res = res.Where(u => u.State == state);
                }

                string searchText = tbxGrid1.Text;
                if (!String.IsNullOrEmpty(searchText))
                {
                    res = res.Where(u => u.SaleOrderNo.Contains(searchText));
                }

                if (datePickerFrom.SelectedDate.HasValue)
                {
                    DateTime dtstart = datePickerFrom.SelectedDate.Value;
                    res = res.Where(u => u.InputeDate >= dtstart);
                }

                if (datePickerTo.SelectedDate.HasValue)
                {
                    DateTime dtend = datePickerTo.SelectedDate.Value.AddDays(1);
                    res = res.Where(u => u.InputeDate <= dtend);
                }


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = res.Count();// q.Count();

                // 排列和数据库分页
                res = SortAndPage(res, Grid1);

                Grid1.DataSource = res;
                Grid1.DataBind();
            }
        }

        protected void Window1_Close(object sender, FineUIPro.WindowCloseEventArgs e)
        {

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_RowCommand(object sender, FineUIPro.GridCommandEventArgs e)
        {

        }

        protected void Grid1_PageIndexChange(object sender, FineUIPro.GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        protected void btnSearch2_click(object sender, EventArgs e)
        {

        }

        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {

        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {

        }


        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        { }

        protected void Grid2_PreRowDataBound(object sender, GridPreRowEventArgs e)
        { }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void UpdateDataRow(Dictionary<string, object> rowDict, PurchaseOrderDetail pod)
        {
            if (rowDict.ContainsKey("PassQut"))
            {
                pod.CheckDate = DateTime.Now;
            }

        }

        protected void rblState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}