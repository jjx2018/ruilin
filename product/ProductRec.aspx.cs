using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.product
{
    public partial class ProductRec : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            BindGrid1();
        }

        private void BindGrid1()
        {
            using (var appdb = new AppContext())
            {
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                DateTime d2 = d1.AddDays(-1);
                //d1是本月的第一天，d2本月的最后一天，
                DateTime dtstart = datePickerFrom.SelectedDate == null ? d2 : datePickerFrom.SelectedDate.Value;
                DateTime dtend = datePickerTo.SelectedDate == null ? now.AddDays(2) : datePickerTo.SelectedDate.Value.AddDays(1);
                var qqq = from a in appdb.produceorderheader
                          select a;

                //在产品名称中搜索
                string searchText = "";
                if (!String.IsNullOrEmpty(searchText))
                {
                    qqq = qqq.Where(u => u.SaleOrderNo.Contains(searchText) || u.Inputer.Contains(searchText) || u.ProOrderNo.Contains(searchText));
                }


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = qqq.Count();// q.Count();

                // 排列和数据库分页
                qqq = SortAndPage<ProduceOrderHeader>(qqq, Grid1);

                Grid1.DataSource = qqq;//
                Grid1.DataBind();
            }
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();
        }

        protected void Grid1_PageIndexChange(object sender, FineUIPro.GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid1();
        }

        protected void Grid1_RowClick(object sender, FineUIPro.GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndex == -1)
                return;
            BindGrid2();

        }

        private void BindGrid2()
        {
            if (Grid1.Rows.Count == 0)
            {
                Grid2.RecordCount = 0;
                Grid2.DataSource = null;
                Grid2.DataBind();
                return;
            }

            if (Grid1.SelectedRowIndex < 0 || Grid1.Rows.Count <= 0)
            {
                return;
            }

            using (var appdb = new AppContext())
            {
                string ProOrderNo = Grid1.DataKeys[Grid1.SelectedRowIndex][1].ToString();

                //已审核  且  合格、特采的
                var q = from a in appdb.produceOrderRec
                        where a.State == true&&(a.result==result.合格||a.result==result.特采)
                        group a by new
                        {
                            a.ProduceOrderDetailSn
                        } into g
                        select new
                        {
                            g.Key.ProduceOrderDetailSn,
                            maopei = g.Sum(u => u.Role == role.毛胚 ? u.Quantity : 0),

                            zhusu = g.Sum(u => u.Role == role.注塑 ? u.Quantity : 0),

                            hanjie = g.Sum(u => u.Role == role.焊接 ? u.Quantity : 0),

                            pentu = g.Sum(u => u.Role == role.喷涂 ? u.Quantity : 0),

                            weiwai= g.Sum(u => u.Role == role.委外 ? u.Quantity : 0),

                            zuzhuang = g.Sum(u => u.Role == role.组装 ? u.Quantity : 0),
                        };

                var inq = from a in appdb.produceorderdetail
                          where a.ProOrderNo == ProOrderNo
                          join b in q on a.SN equals b.ProduceOrderDetailSn
                          into a_join
                          from b in a_join.DefaultIfEmpty()
                          select new
                          {
                              a.SN,
                              a.ProPlanNo,
                              a.ProOrderNo,
                              a.SaleOrderNo,
                              a.ProNo,
                              a.ItemNo,
                              a.ItemName,
                              a.Spec,
                              a.Quantity,
                              a.WorkShop,

                              b.maopei,
                              b.zhusu,
                              b.hanjie,
                              b.pentu,
                              b.weiwai,
                              b.zuzhuang,
                          };

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = inq.Count();// q.Count();

                // 排列和数据库分页
                inq = SortAndPage(inq, Grid2);

                Grid2.DataSource = inq;// itemq.Take(2);// q;
                Grid2.DataBind();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void Grid2_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {

        }
    }
}