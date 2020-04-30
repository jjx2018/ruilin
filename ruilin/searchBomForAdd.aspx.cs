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
using System.IO;
using System.Text;

namespace AppBoxPro.ruilin
{
    public partial class searchBomForAdd : PageBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger("magPlan.aspx");
        private static string sql = "";
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "AllitemView";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ttbSearchMessage.Text = GetQueryValue("k");
                LoadData();
            }
        }

        private void LoadData()
        {
            // 权限检查
            //CheckPowerWithButton("PlanAdd", btnNew);
            //CheckPowerDeleteWithButton(btnDeleteSelected);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);


            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;

            BindGrid();
        }
        protected void btnSearch_click(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {
            using (var appdb = new AppContext())
            {
                IQueryable<BomDetail> q = appdb.bomdtl;

                // 在职务名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(t => t.ItemName.Contains(searchText) || t.Material.Contains(searchText) || t.Sclass.Contains(searchText) || t.SurfaceDeal.Contains(searchText));
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();

                // 排列和分页
                q = SortAndPage<BomDetail>(q, Grid1);

                Grid1.DataSource = q;
                Grid1.DataBind();
            }
        }

        #endregion

        #region Events
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }
        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid();
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("PlanEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("PlanDelete", Grid1, "deleteField");
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

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            int sn = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("PlanDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                using (var appdb = new AppContext())
                {
                    //int userCount = DB.Users.Where(u => u.Titles.Any(t => t.ID == titleID)).Count();
                    //if (userCount > 0)
                    //{
                    //    Alert.ShowInTop("删除失败！需要先清空拥有此职务的用户！");
                    //    return;
                    //}
                    appdb.prdmissionsl.Where(t => t.SN == sn).Delete();
                }
                BindGrid();
            }
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("PlanDelete"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
            List<int> ids = GetSelectedDataKeyIDs(Grid1);

            // 执行数据库操作
            //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
            //DB.SaveChanges();

            using (var appdb = new AppContext())
            {
                appdb.prdmissionsl.Where(u => ids.Contains(u.SN)).Delete();
            }

            // 重新绑定表格
            BindGrid();
        }

        #endregion

        #region  export excel
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Grid1.PageSize = 1000000;
            BindGrid();
            //sql = "select ItemNo,ItemName,Spec,recClass from ("+sql+") a";
            //log.Info(sql);
            //SQLHelper.DbHelperSQL.SetConnectionString("");
            ////Grid grid = new Grid();


            //Grid2.DataSource = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 60);
            //Grid2.DataBind();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
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

        #endregion

        
    }
}