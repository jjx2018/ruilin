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

namespace AppBoxPro.BomMag
{
    public partial class ProduceBomHead : PageBase
    {
        public string pstr = "";
        static Hashtable htClickColsName = new Hashtable();

        log4net.ILog log = log4net.LogManager.GetLogger("magPlan.aspx");
        private bool AppendToEnd = false;
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "BOMView";
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
            // 权限检查
            //CheckPowerWithButton("InstockEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("InstockDelete", btnDeleteSelected);
            //CheckPowerWithButton("InstockNew", btnNew);



            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnNew.OnClientClick = Window1.GetShowReference("~/admin/user_new.aspx", "新增用户");

            // 每页记录数
            //datePickerFrom.SelectedDate = DateTime.Today.AddMonths(-6);
            //datePickerTo.SelectedDate = DateTime.Today;
            
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            
            BindGrid();
            
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
        

        protected void ttbSearchMessage1_Trigger2Click(object sender, EventArgs e)
        {
            TwinTriggerBox1.ShowTrigger1 = true;
            BindGrid();
        }

        protected void ttbSearchMessage1_Trigger1Click(object sender, EventArgs e)
        {
            TwinTriggerBox1.Text = String.Empty;
            TwinTriggerBox1.ShowTrigger1 = false;
            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                using (var appdb = new AppContext())
                {

                    string searchText = TwinTriggerBox1.Text.Trim();

                    //var qq = from a in appdb.bombase
                    //         from b in appdb.v_userinfor
                    //         where a.Inputer == b.userid
                    //         select new { a.OdtSN, b.ChineseName };
                    //var q = from a in appdb.orderdetail
                    //        join k in qq on a.SN equals k.OdtSN into k_join
                    //        from h in k_join.DefaultIfEmpty()
                    //        join b in appdb.orderheader on a.FSN equals b.SN
                    //        join d in appdb.v_userinfor on a.Inputer equals d.userid into userjoin
                    //        from c in userjoin.DefaultIfEmpty()
                    //        where a.IsBom == 1
                    //        select new { a.SN,a.FSN,a.BomVer,a.ClinetNo,a.Color,a.CountryPackVer,a.Demand1,a.Demand2,a.Inputer,a.InputerDate,a.IsBom,a.IsChange,a.IsNew,a.IsPackingmaterials,a.ItemName,a.ItemNo,a.OrderNo,a.Price,a.Quantity,a.Remark,a.Unit,c.ChineseName ,lrr=h.ChineseName};

                    var q = from a in appdb.bombase
                            join c in appdb.orderdetail on a.OdtSN equals c.SN into c_join
                            from e in c_join.DefaultIfEmpty()
                            from b in appdb.v_userinfor
                            where a.Inputer == b.userid
                            select new
                            {
                                a.SN,
                                a.Ver,
                                a.OdtSN,
                                e.Price,
                               e.Quantity,
                                b.ChineseName,
                                a.ProNo,
                                a.ProName,
                                a.OrderNo,
                                e.ClinetNo,
                                e.Remark,
                                e.Color,
                                e.IsNew,
                                e.IsPackingmaterials,
                                e.CountryPackVer,
                                e.IsChange,
                                e.Demand1,
                                e.Demand2
                            };
                    //Alert.Show(q.ToString());
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.ProName.Contains(searchText) || u.ProNo.Contains(searchText) || u.ClinetNo.Contains(searchText) || u.OrderNo.Contains(searchText)); ;
                    }

                    foreach (DictionaryEntry de in htClickColsName)
                    {
                        switch (de.Key.ToString())
                        {
                            //case "OrderNo":
                            //    q = q.Where(u => u.OrderNo == de.Value.ToString());
                            //    break;
                            //case "ClinetNo":
                            //    q = q.Where(u => u.ClinetNo == de.Value.ToString());
                            //    break;
                            //case "ProName":
                            //    q = q.Where(u => u.ProName == de.Value.ToString());
                            //    break;
                            //case "ProNo":
                            //    q = q.Where(u => u.ProNo == de.Value.ToString());
                            //    break;
                            //case "Price":
                            //    q = q.Where(u => u.Price.ToString() == de.Value.ToString());
                            //    break;
                            //case "Quantity":
                            //    q = q.Where(u => u.Quantity.ToString() == de.Value.ToString());
                            //    break;
                            //case "Ver":
                            //    q = q.Where(u => u.Ver.ToString() == de.Value.ToString());
                            //    break;
                            //case "ChineseName":
                            //    q = q.Where(u => u.ChineseName.ToString() == de.Value.ToString());
                            //    break;
                            //case "Remark":
                            //    q = q.Where(u => u.Remark.ToString() == de.Value.ToString());
                            //    break;

                        }
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
            catch (Exception ee )
            {
                Alert.Show(ee.ToString());
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string[] s = txtClickColsName.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (s == null || s.Length == 0)
            {
                return;
            }
            htClickColsName.Remove(s[0]);
            BindGrid();
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

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            string[] s = Grid1.SelectedCell;
            for (int i = 0; i < Grid1.Columns.Count; i++)
            {
                if (s[1] == Grid1.Columns[i].ColumnID && !htClickColsName.ContainsKey(s[1]))
                {
                    htClickColsName.Add(s[1], Grid1.Rows[e.RowIndex].Values[i].ToString());
                    break;
                }
            }
            BindGrid();
            updatecol();
        }


        #endregion

        #region Events



        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("InstockEdit", Grid1, "editField");
            
            //CheckPowerWithWindowField("InstockChangePassword", Grid1, "changePasswordField");

        }
       
        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
            //if (Grid2.Rows.Count <= 0) return;
            //Alert.Show(Grid2.Rows[e.RowIndex].Values[0].ToString());
            //FineUIPro.WindowField editField = Grid2.FindColumn("editField") as FineUIPro.WindowField;
            //editField.DataIFrameUrlFormatString = "~/ruilin/OrderDetailedit.aspx?id=" + Grid2.DataKeys[e.RowIndex][0].ToString() + "&k=2&p=" + Grid2.PageIndex.ToString();

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
            //int ID = GetSelectedDataKeyID(Grid1);

            //if (e.CommandName == "Delete")
            //{
            //    // 在操作之前进行权限检查
            //    if (!CheckPower("BOMDelete"))
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