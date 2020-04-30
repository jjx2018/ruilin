using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using FineUIPro;
using EntityFramework.Extensions;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Data.OleDb;
using System.Data;
using System.Text;
using System.IO;

namespace AppBoxPro.stock
{
    public partial class outstock : PageBase
    {
        private bool AppendToEnd = false;
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //btnClear.OnClientClick = SF1.GetResetReference();
                // 每页记录数
                Grid1.PageSize = ConfigHelper.PageSize;
                ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

                LoadData();
                string sql = "select ClassName from GoodsClass order by SortIndex ";
                SQLHelper.DbHelperSQL.SetConnectionString("");

            }
            else
            {
                string requestArg = GetRequestEventArgument(); // 此函数所在文件：PageBase.cs
                //log.Info(requestArg);
                if (requestArg.Equals("RefreshGrid2"))
                {
                    //BindGrid2();
                }
                else if (requestArg.Equals("RefreshGrid"))
                {
                    BindGrid();
                }
            }

        }


        private void LoadData()
        {
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            BindGrid();
            BindGrid2();

        }



        public string gettype(string code)
        {
            if (code == "02")
            {
                return "进仓";
            }
            else if (code == "03")
            {
                return "出仓";
            }
            return "";
        }



        private void BindGrid()
        {
            using (var appdb = new AppContext())
            {

                string searchText = tbxsearch.Text.Trim();


                var q = from a in appdb.orderdetail
                        join b in appdb.orderheader on a.FSN equals b.SN
                        join d in appdb.v_userinfor on a.Inputer equals d.userid into userjoin
                        from c in userjoin.DefaultIfEmpty()
                        where a.IsBom == 1
                        select new { a.SN, a.FSN, a.BomVer, a.ClinetNo, a.Color, a.CountryPackVer, a.Demand1, a.Demand2, a.Inputer, a.InputerDate, a.IsBom, a.IsChange, a.IsNew, a.IsPackingmaterials, a.ItemName, a.ItemNo, a.OrderNo, a.Price, a.Quantity, a.Remark, a.Unit, c.ChineseName };


                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.ItemName.Contains(searchText) || u.ItemNo.Contains(searchText) || u.ClinetNo.Contains(searchText) || u.OrderNo.Contains(searchText)); ;
                }



                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();// q.Count();

                // 排列和数据库分页
                //q = SortAndPage<Item>(q, Grid1);
                q = SortAndPage(q, Grid1);

                Grid1.DataSource = q;// itemq.Take(2);// q;
                Grid1.DataBind();
            }
        }

        private void BindGrid2()
        {
            var q = from a in MYDB.StockHeaders
                    where a.title == "领料单"
                    select a;

            Grid2.DataSource = q;
            Grid2.DataBind();
        }


        #endregion

        #region Events



        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {

        }

        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {

        }
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnSearch_click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }



        #endregion
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
           
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {

        }


        private string GetGridTableHtml(Grid grid)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");

            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");

            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");


            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (object value in row.Values)
                {
                    string html = value.ToString();
                    if (html.StartsWith(Grid.TEMPLATE_PLACEHOLDER_PREFIX))
                    {
                        // 模板列
                        string templateID = html.Substring(Grid.TEMPLATE_PLACEHOLDER_PREFIX.Length);
                        Control templateCtrl = row.FindControl(templateID);
                        html = GetRenderedHtmlSource(templateCtrl);
                    }
                    else
                    {
                        // 处理CheckBox
                        if (html.Contains("f-grid-static-checkbox"))
                        {
                            if (html.Contains("uncheck"))
                            {
                                html = "×";
                            }
                            else
                            {
                                html = "√";
                            }
                        }

                        // 处理图片
                        if (html.Contains("<img"))
                        {
                            string prefix = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "");
                            html = html.Replace("src=\"", "src=\"" + prefix);
                        }
                    }

                    sb.AppendFormat("<td>{0}</td>", html);
                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        private string GetRenderedHtmlSource(Control ctrl)
        {
            if (ctrl != null)
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        ctrl.RenderControl(htw);

                        return sw.ToString();
                    }
                }
            }
            return String.Empty;
        }
    }
}