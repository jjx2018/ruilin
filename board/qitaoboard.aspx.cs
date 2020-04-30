using FineUIPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace AppBoxPro.board
{
    public partial class qitaoboard : PageBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // 设置主题
            if (PageManager.Instance != null)
            {
                var pm = PageManager.Instance;
                var themeValue = ConfigHelper.Theme;
                pm.CustomTheme = String.Empty;
                pm.Theme = (Theme)Enum.Parse(typeof(Theme), "Dot_Luv", true);
            }


            // 设置页面标题
            Page.Title = ConfigHelper.Title;

            // 禁用表单的自动完成功能
            Form.Attributes["autocomplete"] = "off";
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {

            var qqq = from a in MYDB.v_proqtl select a;

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = qqq.Count();// q.Count();

            // 排列和数据库分页
            qqq = SortAndPage(qqq, Grid1);

            Grid1.DataSource = qqq;
            Grid1.DataBind();


        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {

            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {

        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {

        }

        protected void timer1_Tick(object sender, EventArgs e)
        {
            int maxIndex = Grid1.PageCount;
            if (Grid1.PageIndex + 1 == maxIndex)
            {
                Grid1.PageIndex = 0;
                BindGrid();
            }
            else
            {
                Grid1.PageIndex++;
                BindGrid();
            }
        }
    }
}