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
    /// 发外出仓
    /// </summary>
    public partial class weiwaioutstock : PageBase
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
                var qqq = from a in appdb.sendoutprocessheader
                          select a;

                string searchText = tbxGrid1.Text;
                if (!String.IsNullOrEmpty(searchText))
                {
                    qqq = qqq.Where(u => u.SendOutPlanNo.Contains(searchText) || u.SendOutOrderNo.Contains(searchText) || u.SaleOrderNo.Contains(searchText) || u.Provider.Contains(searchText) || u.JBRName.Contains(searchText));
                }

                if (datePickerFrom.SelectedDate.HasValue)
                {
                    DateTime dtstart = datePickerFrom.SelectedDate.Value;
                    qqq = qqq.Where(u => u.InputeDate >= dtstart);
                }

                if (datePickerTo.SelectedDate.HasValue)
                {
                    DateTime dtend = datePickerTo.SelectedDate.Value.AddDays(1);
                    qqq = qqq.Where(u => u.InputeDate <= dtend);
                }


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = qqq.Count();// q.Count();

                // 排列和数据库分页
                qqq = SortAndPage<SendOutProcessHeader>(qqq, Grid1);

                Grid1.DataSource = qqq;// itemq.Take(2);// q;
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
    }
}