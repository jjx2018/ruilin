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

namespace AppBoxPro.MaterialMag
{
    public partial class LooKProduction : PageBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger("magPlan.aspx");

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "OrderView";
            }
        }

        #endregion

        protected void ttbSearchMessage1_Trigger2Click(object sender, EventArgs e)
        {
            TwinTriggerBox1.ShowTrigger1 = true;
            BindGrid();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void ttbSearchMessage1_Trigger1Click(object sender, EventArgs e)
        {
            TwinTriggerBox1.Text = String.Empty;
            TwinTriggerBox1.ShowTrigger1 = false;
            BindGrid();
        }

        private void BindGrid()
        {
            using (var appdb = new AppContext())
            {

                string searchText = TwinTriggerBox1.Text.Trim();

                string itemno = GetQueryValue("itemno");
                var q = from a in appdb.bombase
                        join b in appdb.bomdtl on a.SN equals b.FSN
                        where b.ItemNo == itemno
                        select a;


                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.ProNo.Contains(searchText) || u.ProName.Contains(searchText) || u.OrderNo.Contains(searchText)); ;
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
        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("InstockEdit", Grid1, "editField");

            //CheckPowerWithWindowField("InstockChangePassword", Grid1, "changePasswordField");

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
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            //int ID = GetSelectedDataKeyID(Grid1);

            //if (e.CommandName == "Delete")
            //{
            //    // 在操作之前进行权限检查
            //    if (!CheckPower("OrderDelete"))
            //    {
            //        CheckPowerFailWithAlert();
            //        return;
            //    }

            //    using (AppContext appdb = new AppContext())
            //    {
            //        appdb.orderdetail.Where(u => u.SN == ID).Delete();

            //        BindGrid2();
            //    }
            //}
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            //PageContext.RegisterStartupScript(Window1.GetShowReference("~/ruilin/OrderDetailEdit.aspx?id=" + Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString() + "&k=1", "产品详情"));
        }

    }
}