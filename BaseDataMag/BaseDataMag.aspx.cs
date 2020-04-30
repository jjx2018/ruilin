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

namespace AppBoxPro.BaseDataMag
{
    public partial class BaseDataMag : PageBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger("rlitems.aspx");
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "BaseDataView";
            }
        }

        #endregion
        public string pstr = "";
        private bool AppendToEnd = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // 删除选中单元格的客户端脚本
                //string deleteScript = GetDeleteScript();

                //// 新增数据初始值
                //JObject defaultObj = new JObject();
                //defaultObj.Add("StoreCode", "");
                //defaultObj.Add("StoreName", "");
                //defaultObj.Add("SortIndex", "");

                //defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));

                // 在第一行新增一条数据
                //btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);



                // 删除选中行按钮
                //btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                CheckPowerWithButton("BaseDataDelete", btnDelete);
                ResolveDeleteButtonForGrid(btnDelete, Grid1);

                // 每页记录数
                Grid1.PageSize = ConfigHelper.PageSize;
                ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
                // 绑定表格
                //BindGrid("", 1);
                BindGrid();

            }
        }
        // 删除选中行的脚本
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        }
        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 设置LinkButtonField的点击客户端事件
            //LinkButtonField deleteField = Grid1.FindColumn("deleteField") as LinkButtonField;
            //deleteField.OnClientClick = GetDeleteScript();
        }
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            //BindGrid(e.SortField, Grid1.PageIndex + 1);
            BindGrid();
        }
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            int sn = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("BaseDataDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                using (var appdb = new AppContext())
                {

                    appdb.basedata.Where(t => t.SN == sn).Delete();
                }
                BindGrid();
            }
        }
        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            // 删除新增未保存到数据库的数据
            List<int> deletedRows = Grid1.GetDeletedList();
            foreach (int rowIndex in deletedRows)
            {
                //int rowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                Grid1.Rows.RemoveAt(rowIndex);
            }

            // 在操作之前进行权限检查
            if (!CheckPower("BaseDataDelete"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
            List<int> ids = GetSelectedDataKeyIDs(Grid1);

            // 执行数据库操作
            //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
            //DB.SaveChanges();
            if (ids.Count > 0)
            {
                using (var appdb = new AppContext())
                {
                    appdb.basedata.Where(u => ids.Contains(u.SN)).Delete();
                }
            }
            // 重新绑定表格
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {

            Grid1.PageIndex = e.NewPageIndex;
            //BindGrid("", e.NewPageIndex + 1);
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
        protected void btnSearch_click(object sender, EventArgs e)
        {
            //BindGrid("", Grid1.PageIndex + 1);
            BindGrid();
        }
        private void BindGrid()
        {
            using (var appdb = new AppContext())
            {
                IQueryable<BaseData> q = appdb.basedata;

                // 在职务名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(t => t.TypeCode.Contains(searchText) || t.TypeName.Contains(searchText));
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();

                // 排列和分页
                q = SortAndPage<BaseData>(q, Grid1);

                Grid1.DataSource = q;
                Grid1.DataBind();
            }

        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            BindGrid();
            //BindGrid("", Grid1.PageIndex + 1);
        }
        protected void rbtState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
            //BindGrid("", Grid1.PageIndex + 1);
        }
        protected void dinnertypeChk_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
            //BindGrid("", Grid1.PageIndex + 1);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid1.GetModifiedData().Count == 0 && Grid1.GetNewAddedList().Count == 0)
                {
                    Alert.Show("表格数据没有变化！");
                    return;
                }
                ArrayList al = new ArrayList();
                string sql = "", s = "";
                // 新增数据
                SQLHelper.DbHelperSQL.SetConnectionString("");
                List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                string lastcode = "", lastname = "",lasttype="";
                for (int i = 0; i < newAddedList.Count; i++)
                {
                    if ((lastcode == newAddedList[i]["TypeCode"].ToString() || lastname == newAddedList[i]["TypeName"].ToString()) && lasttype == newAddedList[i]["SType"].ToString())
                    {
                        Alert.Show("第" + (i + 1) + "行已经存在相同的"+lasttype+ "代码或" + lasttype + "名称，请更改");
                        return;
                    }
                    else
                    {
                        sql = "select count(*) from basedata where (TypeCode='" + newAddedList[i]["TypeCode"].ToString() + "' or  TypeName='" + newAddedList[i]["TypeName"].ToString() + "') and SType='" + newAddedList[i]["SType"].ToString() + "'";

                        if (int.Parse(SQLHelper.DbHelperSQL.GetSingle(sql, 30)) > 0)
                        {
                            Alert.Show("第" + (i + 1) + "行已经存在相同的" + newAddedList[i]["SType"].ToString() + "代码或" + newAddedList[i]["SType"].ToString() + "名称，请更改");
                            return;
                        }
                    }
                    lastcode = newAddedList[i]["TypeCode"].ToString();
                    lastname = newAddedList[i]["TypeName"].ToString();
                    lasttype = newAddedList[i]["SType"].ToString();
                    sql = "insert into basedata(TypeCode,TypeName,SortIndex,SType) values('" + newAddedList[i]["TypeCode"].ToString() + "','" + newAddedList[i]["TypeName"].ToString() + "'," + newAddedList[i]["SortIndex"].ToString() + ",'" + newAddedList[i]["SType"].ToString() + "')";
                    log.Info("sql add basedata:" + sql);
                    al.Add(sql);
                }

                //Alert.Show(s);
                //return;
                //s = "";
                // 修改的现有数据
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();

                foreach (int rowIndex in modifiedDict.Keys)
                {
                    sql = "update basedata set ";
                    for (int i = 0; i < Grid1.Columns.Count; i++)
                    {
                        if (modifiedDict[rowIndex].ContainsKey(Grid1.Columns[i].ColumnID))
                        {
                            sql += Grid1.Columns[i].ColumnID + "='" + modifiedDict[rowIndex][Grid1.Columns[i].ColumnID].ToString() + "',";
                        }

                    }
                    sql = sql.TrimEnd(new char[] { ',' });
                    sql += " where sn=" + Grid1.DataKeys[rowIndex][0];

                    al.Add(sql);
                }


                SQLHelper.DbHelperSQL.SetConnectionString("");
                if (SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                {
                    Alert.Show("保存成功");
                    BindGrid();
                }
                else
                {
                    Alert.Show("保存失败");
                }
            }
            catch (Exception ee)
            {
                Alert.Show(ee.Message);
            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Grid1.PageSize = 100000;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

        #region 自定义函数

        //下载文件
        //导出excel相关
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

        protected void Button2_Click(object sender, EventArgs e)
        {


        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                string sortindex = "";
                if (Grid1.GetNewAddedList().Count != 0)
                {
                    List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                   
                    string sql = "select count(*)+1 from BaseData where stype='"+ newAddedList[0]["SType"].ToString() + "'";
                    SQLHelper.DbHelperSQL.SetConnectionString("");
                    sortindex = SQLHelper.DbHelperSQL.GetSingle(sql, 30);
                }
                else
                {
                    string sql = "select count(*)+1 from BaseData ";
                    SQLHelper.DbHelperSQL.SetConnectionString("");
                    sortindex = SQLHelper.DbHelperSQL.GetSingle(sql, 30);
                }
                // 新增数据初始值
                JObject defaultObj = new JObject();
                string deleteScript = GetDeleteScript();
                defaultObj.Add("TypeCode", "");
                defaultObj.Add("TypeName", "");
                defaultObj.Add("SortIndex", sortindex);

                defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));
                PageContext.RegisterStartupScript(Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd));
            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
        }

        protected void Grid1_AfterEdit(object sender, GridAfterEditEventArgs e)
        {
            try
            {
                string sortindex = "";
                if (Grid1.GetNewAddedList().Count != 0)
                {
                    List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();

                    string sql = "select count(*)+1 from BaseData where stype='" + newAddedList[newAddedList.Count-1]["SType"].ToString() + "'";
                    SQLHelper.DbHelperSQL.SetConnectionString("");
                    sortindex = SQLHelper.DbHelperSQL.GetSingle(sql, 30);
                    Grid1.UpdateCellValue(e.RowIndex, e.ColumnID, sortindex);
                }
            }
            catch(Exception ee)
            {
                Alert.Show(ee.Message);
            }
        }
    }
}