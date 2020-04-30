using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using FineUIPro;
using EntityFramework.Extensions;
using System.Data.OleDb;
using System.Data;
using System.Collections;
using System.Threading;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace AppBoxPro.BomMag
{
    public partial class ProjectBomDetail : PageBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger("magBom.aspx");
        static Hashtable htClickColsName = new Hashtable();
        public string prono = "", proname = "", sdate = "", ver = "", fileno = "", inputer = "", inputerdate = "";
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "ProjectBOMView";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pid = GetQueryIntValue("sn");
                using (var appdb = new AppContext())
                {
                    ProBomHeader item = appdb.probombase.Where(u => u.SN == pid).FirstOrDefault();
                    prono = item.ProNo;
                    proname = item.ProName;
                    sdate = item.BomDate.Value.ToString("yyyy-MM-dd");
                    ver = item.Ver.ToString();
                    fileno = item.FileNo;
                    inputer = item.Inputer;
                    inputerdate = item.InputeDate.Value.ToString("yyyy-MM-dd");
                    
                }
                LoadData();
            }
        }

        private void LoadData()
        {
            // 权限检查
            //CheckPowerWithButton("InstockEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("InstockDelete", btnDeleteSelected);
            //CheckPowerWithButton("InstockNew", btnNew);



            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnNew.OnClientClick = Window1.GetShowReference("~/admin/user_new.aspx", "新增用户");

            // 每页记录数
            
            Grid2.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize2.SelectedValue = ConfigHelper.PageSize.ToString();
            //BindDDLItemName();
            //BindDDLCompany();
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

         private void BindGrid2()
        {
            

            

            using (var appdb = new AppContext())
            {
                
                int pid = GetQueryIntValue("sn");
                var q = from a in appdb.probomdtl
                        where a.FSN == pid
                        select a;





                //IQueryable<YW_ProcessRec> q = appdb.processRec; //.Include(u => u.Dept);
                // 在用户名称中搜索
                string searchText = TwinTriggerBox3.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.ItemName.Contains(searchText) || u.ItemNo.Contains(searchText) || u.Spec.Contains(searchText) || u.Sclass.Contains(searchText) || u.MainFrom.Contains(searchText) || u.WorkShop.Contains(searchText) || u.StoreHouse.Contains(searchText));
                }
                //Alert.Show(inq.ToString());

                foreach (DictionaryEntry de in htClickColsName)
                {
                    switch (de.Key.ToString())
                    {
                        case "ItemNo":
                            q = q.Where(u => u.ItemNo == de.Value.ToString());
                            break;
                        case "ItemName":
                            q = q.Where(u => u.ItemName == de.Value.ToString());
                            break;
                        case "Spec":
                            q = q.Where(u => u.Spec == de.Value.ToString());
                            break;
                        case "Material":
                            q = q.Where(u => u.Material == de.Value.ToString());
                            break;
                        case "SurfaceDeal":
                            q = q.Where(u => u.SurfaceDeal == de.Value.ToString());
                            break;
                        case "ProUsingQuantity":
                            q = q.Where(u => u.ProUsingQuantity.ToString() == de.Value.ToString());
                            break;
                        case "ZongCheng":
                            q = q.Where(u => u.ZongCheng.ToString() == de.Value.ToString());
                            break;
                        case "BaseNum":
                            q = q.Where(u => u.BaseNum.ToString() == de.Value.ToString());
                            break;
                        case "Sclass":
                            q = q.Where(u => u.Sclass.ToString() == de.Value.ToString());
                            break;
                        case "MainFrom":
                            q = q.Where(u => u.MainFrom.ToString() == de.Value.ToString());
                            break;
                        case "WorkShop":
                            q = q.Where(u => u.WorkShop.ToString() == de.Value.ToString());
                            break;
                        case "StoreHouse":
                            q = q.Where(u => u.StoreHouse.ToString() == de.Value.ToString());
                            break;
                    }
                }


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = q.Count();// q.Count();

                // 排列和数据库分页
                //inq = SortAndPage<ProBomDetail>(inq, Grid2);
                //inq = SortAndPage(inq, Grid2);
                JObject jObject = new JObject();
                jObject.Add("ItemNo", "<span style='color:red'>共计：" + q.Count() + "条</span>");
                Grid2.SummaryData = jObject;
                Grid2.DataSource = q;// itemq.Take(2);// q;
                Grid2.DataBind();
            }
        }

        #endregion

        #region Events


        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("InstockEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("InstockDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("InstockChangePassword", Grid1, "changePasswordField");

        }

        protected void Grid1_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {
            //User user = e.DataItem as User;

            //// 不能删除超级管理员
            //if (user.Name == "admin")
            //{
            //    FineUI.LinkButtonField deleteField = Grid1.FindColumn("deleteField") as FineUI.LinkButtonField;
            //    deleteField.Enabled = false;
            //    deleteField.ToolTip = "不能删除超级管理员！";
            //}

        }

       
        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            Grid2.SortDirection = e.SortDirection;
            Grid2.SortField = e.SortField;
            BindGrid2();
        }
         
        protected void Grid2_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid2.PageIndex = e.NewPageIndex;
            BindGrid2();
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (Grid2.SelectedRowIndex == -1)
            {
                Alert.Show("请先选择需要删除的数据");
                return;
            }
            // 在操作之前进行权限检查
            if (!CheckPower("ProjectBOMDelete"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
            List<int> ids = GetSelectedDataKeyIDs(Grid2);

            // 执行数据库操作
            //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
            //DB.SaveChanges();

            using (AppContext appdb = new AppContext())
            {
                appdb.probomdtl.Where(u => ids.Contains(u.SN)).Delete();
            }
            // 重新绑定表格
            BindGrid2();
        }



       
         
        
        protected void btnSearch2_click(object sender, EventArgs e)
        {
            BindGrid2();
        }
        protected void OnDateFrom_Changed(object sender, EventArgs e)
        {
            BindGrid2();
        }

        protected void OnDateTo_Changed(object sender, EventArgs e)
        {
            BindGrid2();
        }

    

        
        protected void ddlGridPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid2.PageSize = Convert.ToInt32(ddlGridPageSize2.SelectedValue);

            BindGrid2();
        }


 

        #endregion
       

        protected void TwinTriggerBox3_Trigger2Click(object sender, EventArgs e)
        {
            TwinTriggerBox3.ShowTrigger1 = true;
            BindGrid2();
        }

        protected void TwinTriggerBox3_Trigger1Click(object sender, EventArgs e)
        {
            TwinTriggerBox3.Text = String.Empty;
            TwinTriggerBox3.ShowTrigger1 = false;
            BindGrid2();
        }
        #region  export excel
         
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Grid2.PageSize = 1000000;
            BindGrid2();
            Response.Write(GetGridTableHtml(Grid2));
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

        

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string[] s = txtClickColsName.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (s == null || s.Length == 0)
            {
                return;
            }
            htClickColsName.Remove(s[0]);
            BindGrid2();
            updatecol();
        }
        void updatecol()
        {
            txtClickColsName.Text = "";
            foreach (DictionaryEntry de in htClickColsName)
            {
                txtClickColsName.Text += de.Key.ToString() + ",";
            }

        }

        protected void Grid2_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            string[] s = Grid2.SelectedCell;
            for (int i = 0; i < Grid2.Columns.Count; i++)
            {
                if (s[1] == Grid2.Columns[i].ColumnID && !htClickColsName.ContainsKey(s[1]))
                {
                    htClickColsName.Add(s[1], Grid2.Rows[e.RowIndex].Values[i].ToString());
                    break;
                }
            }
            BindGrid2();
            updatecol();
        }
    }
}