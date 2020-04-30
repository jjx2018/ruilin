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

namespace AppBoxPro.ruilin
{
    public partial class magBom : PageBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger("magBom.aspx");
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
            datePickerFrom.SelectedDate = DateTime.Today.AddMonths(-1);
            datePickerTo.SelectedDate = DateTime.Today;

            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            //BindDDLItemName();
            //BindDDLCompany();
            BindGrid();
        }

        private void BindDDLItemName()
        {
            using (var appdb = new AppContext())
            {
                //var q = (from s in appdb.labinfor
                //         group s by new
                //         {
                //             s.proname
                //         } into g
                //         select new
                //         {
                //             ID = (int?)g.Min(p => p.ID),
                //             g.Key.proname
                //         });
                //ddlItemName.DataSource = q.ToList();
                //ddlItemName.DataBind();
            }
        }

        private void BindDDLSpec()
        {
            //从产品名称下拉列表中选择的产品名称进行筛选
            using (var appdb = new AppContext())
            {
                //var q = (from s in appdb.labinfor
                //         where s.proname == ddlItemName.SelectedText
                //         select new
                //         {
                //             s.ID,
                //             s.spec
                //         });
                //ddlSpec.DataSource = q.ToList();
                //ddlSpec.DataBind();
            }
            //ddlSpec.SelectedValue = "-1";
            //ddlSpec.Enabled = !(ddlSpec.Items.Count == 1);

        }

        private void BindDDLCompany()
        {
            using (var appdb = new AppContext())
            {
                //var q = (from s in appdb.labinfor
                //         group s by new
                //         {
                //             s.procompany
                //         } into g
                //         select new
                //         {
                //             g.Key.procompany
                //         });
                //ddlCompany.DataSource = q.ToList();
                //ddlCompany.DataBind();
                //ddlCompany.Items.Insert(0, new FineUI.ListItem("", ""));
            }
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
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                DateTime d2 = d1.AddDays(-1);
                //d1是本月的第一天，d2本月的最后一天，
                DateTime dtstart = datePickerFrom.SelectedDate == null ? d2 : datePickerFrom.SelectedDate.Value;
                DateTime dtend = datePickerTo.SelectedDate == null ? now.AddDays(1) : datePickerTo.SelectedDate.Value.AddDays(1);
                var qqq = from a in appdb.bombase
                          where a.InputeDate < dtstart.Date
                          select a;

                //在产品名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    qqq = qqq.Where(u => u.ProNo.Contains(searchText));
                }
                



                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = qqq.Count();// q.Count();

                // 排列和数据库分页
                qqq = SortAndPage<BomHeader>(qqq, Grid1);

                Grid1.DataSource = qqq;// itemq.Take(2);// q;
                Grid1.DataBind();
            }
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

            //Alert.Show(Grid2.SelectedRowIndex.ToString());
            if (Grid1.SelectedRowIndex < 0 || Grid1.Rows.Count <= 0)
            {
                return;
            }
           
            using (var appdb = new AppContext())
            {
                int pid = int.Parse(Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString());// GetSelectedDataKeyID(Grid1).ToString();

                var inq = from a in appdb.bomdtl
                          where a.FSN == pid
                          select a;





                //IQueryable<YW_ProcessRec> q = appdb.processRec; //.Include(u => u.Dept);
                // 在用户名称中搜索
                string searchText = TwinTriggerBox3.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    inq = inq.Where(u => u.ItemName.Contains(searchText) || u.ItemNo.Contains(searchText)||u.Spec.Contains(searchText));
                }
                //Alert.Show(inq.ToString());

                //日期 筛选
                //if (DateFrom.SelectedDate.HasValue)
                //{
                //    inq = inq.Where(u => u.optdate >= DateFrom.SelectedDate);
                //}
                //if (DateTo.SelectedDate.HasValue)
                //{
                //    inq = inq.Where(u => u.optdate <= DateTo.SelectedDate);
                //}


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = inq.Count();// q.Count();

                // 排列和数据库分页
                inq = SortAndPage<BomDetail>(inq, Grid2);

                Grid2.DataSource = inq;// itemq.Take(2);// q;
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

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            Grid2.SortDirection = e.SortDirection;
            Grid2.SortField = e.SortField;
            BindGrid2();
        }
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void Grid2_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid2.PageIndex = e.NewPageIndex;
            BindGrid2();
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("BOMDelete"))
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
                appdb.bomdtl.Where(u => ids.Contains(u.SN)).Delete();
            }
            // 重新绑定表格
            BindGrid2();
        }



        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            int ID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("BOMDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                using (AppContext appdb = new AppContext())
                {
                    //appdb.processRec.Where(u => u.ID == ID).Delete();

                    BindGrid();
                }
            }
        }
        protected void Grid1_OnRowClick(object sender, FineUIPro.GridRowClickEventArgs e)
        {
            //Alert.Show("rowclick" + e.RowIndex.ToString());
            //Grid2.SelectedRowIndex = e.RowIndex;
            //string meetid = Grid1.Rows[e.RowIndex].Values[1].ToString();
            //Alert.Show(meetid);

            BindGrid2();
        }
        protected void btnSearch_click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void OnDateFrom_Changed(object sender, EventArgs e)
        {
            BindGrid2();
        }

        protected void OnDateTo_Changed(object sender, EventArgs e)
        {
            BindGrid2();
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
        protected void ddlGridPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid2.PageSize = Convert.ToInt32(ddlGridPageSize2.SelectedValue);

            BindGrid2();
        }
        protected void ddlItemName_SelectIndexChanged(object sender, EventArgs e)
        {
            BindDDLSpec();
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (filePhoto.HasFile)
            {
                string fileName = filePhoto.ShortFileName;
                if (!filePhoto.HasFile)
                {
                    // 清空文件上传控件
                    filePhoto.Reset();
                    return;
                }


                fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

                filePhoto.SaveAs(Server.MapPath("~/upload/" + fileName));
                readExcel(fileName);


            }
        }

        private void readExcel(string filename)
        {
            string bomsn = "";
            try
            {
                string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/upload/" + filename) + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"";

                using (OleDbConnection conn = new OleDbConnection(connstring))
                {
                    conn.Open();
                    DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字  
                    //for(int k=0;k<sheetsName.Rows.Count;k++)
                    //{
                    //    log.Info(sheetsName.Rows[k][2].ToString());
                    //}
                    string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字  
                    string sql = string.Format("SELECT * FROM [{0}]", firstSheetName); //查询字符串   


                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                    DataSet set = new DataSet();
                    ada.Fill(set);
                    DataTable dt = set.Tables[0];
                    int i = 0;
                    SQLHelper.DbHelperSQL.SetConnectionString("");
                    string sqlbase = "", sqldtl = "", sclass = "", MastName = "", MastItemNo = "", Version = "", Reserve1 = "", Reserve2 = "", Reserve3 = "", UpdateDate = "";//产品名称、产品编号、版本、瑞麟编号、客户编号、客户代号、日期
                    ArrayList al = new ArrayList();
                    //获取表头  添加bombase
                    if(dt!=null&&dt.Rows.Count>=4)
                    {
                        MastName = dt.Rows[2][2].ToString(); MastItemNo = dt.Rows[2][4].ToString(); Version = dt.Rows[2][7].ToString();
                        Reserve1 = dt.Rows[3][2].ToString(); Reserve2 = dt.Rows[3][4].ToString(); Reserve3 = dt.Rows[3][7].ToString(); UpdateDate = dt.Rows[3][9].ToString();
                        //料号，名称，规格，材质，表面处理或颜色，底数，类别
                        //ItemNo,Name,Spec,MaterialNo,ItemColor,AddReserve1,ClassName
                        sql = "select top 1 * from allitem where itemno='" + dt.Rows[2][4].ToString() + "' or name='" + dt.Rows[2][2].ToString() + "'  ";
                        DataTable dtitem = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                        if (dtitem == null || dtitem.Rows.Count == 0)
                        {
                            sql = "insert into allitem(itemno,name) values('" + dt.Rows[2][4].ToString() + "','" + dt.Rows[2][2].ToString() + "')";
                            log.Info("sqlallitem::::" + sql);
                            al.Add(sql);
                            //产品名称、产品编号、版本、瑞麟编号、客户编号、客户代号、日期
                            sqlbase = "insert into BomBase(MastName,MastItemNo,Version,Reserve1,Reserve2,Reserve3,UpdateDate,MastItenSn) values('" + MastName + "','" + MastItemNo + "','" + Version + "','" + Reserve1 + "','" + Reserve2 + "','" + Reserve3 + "','" + UpdateDate + "',(select max(sn) from allitem))";
                            log.Info("sqlbase::::" + sqlbase);
                            al.Add(sqlbase);
                            SQLHelper.DbHelperSQL.ExecuteSqlTran(al);
                            
                        }
                        else
                        {
                            //产品名称、产品编号、版本、瑞麟编号、客户编号、客户代号、日期
                            sqlbase = "insert into BomBase(MastName,MastItemNo,Version,Reserve1,Reserve2,Reserve3,UpdateDate,MastItenSn) values('" + MastName + "','" + MastItemNo + "','" + Version + "','" + Reserve1 + "','" + Reserve2 + "','" + Reserve3 + "','" + UpdateDate + "'," + dtitem.Rows[0]["sn"].ToString() + ")";
                            log.Info("sqlbase::::" + sqlbase);
                            SQLHelper.DbHelperSQL.ExecuteSql(sqlbase, 30);
                        }
                        sql = "select max(sn) from bombase";
                        bomsn = SQLHelper.DbHelperSQL.GetSingle(sql).ToString();
                        log.Info("bombasesn:::" + sql);
                        al.Clear();
                        #region bomdtl add
                        for (i = 5; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][0].ToString() == "锐 麟 铝 制 品 有 限 公 司" || dt.Rows[i][0].ToString() == "物料清单(BOM)" || dt.Rows[i][0].ToString() == "产品名称" || dt.Rows[i][0].ToString() == "锐麟编号" || dt.Rows[i][0].ToString() == "序号")
                            {
                                sclass = "";
                                continue;
                            }
                            else
                            {
                                #region  

                                if (dt.Rows[i][0].ToString() == "变更内容" && dt.Rows[i][1].ToString() == "" && dt.Rows[i][2].ToString() == "" && dt.Rows[i][3].ToString() == "" && dt.Rows[i][4].ToString() == "" && dt.Rows[i][5].ToString() == "" && dt.Rows[i][6].ToString() == "" && dt.Rows[i][7].ToString() == "" && dt.Rows[i][8].ToString() == "" && dt.Rows[i][9].ToString() == "")
                                {
                                   break;
                                }
                                else if (dt.Rows[i][0].ToString() != "" && dt.Rows[i][1].ToString() == "" && dt.Rows[i][2].ToString() == "" && dt.Rows[i][3].ToString() == "" && dt.Rows[i][4].ToString() == "" && dt.Rows[i][5].ToString() == "" && dt.Rows[i][6].ToString() == "" && dt.Rows[i][7].ToString() == "" && dt.Rows[i][8].ToString() == "" && dt.Rows[i][9].ToString() == "")
                                {
                                    sclass = dt.Rows[i][0].ToString();
                                }
                                else
                                {
                                    #region  bomdtl add
                                    
                                    //料号，名称，规格，材质，表面处理或颜色，底数，类别
                                    //ItemNo,Name,Spec,MaterialNo,ItemColor,AddReserve1,ClassName
                                    sql = "select top 1 * from allitem where itemno='" + dt.Rows[i][1].ToString() + "' or name='" + dt.Rows[i][2].ToString() + "'  ";
                                     dtitem = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                                     if (dtitem == null || dtitem.Rows.Count == 0)
                                    {
                                        sql = "insert into allitem(itemno,name) values('" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "')";
                                        log.Info("sqlallitem::::" + sql);
                                        SQLHelper.DbHelperSQL.ExecuteSql(sql,30);
                                        //bomsn,物料sn，料号，名称，规格，材质，表面处理，用量，分类
                                        sqldtl = "insert into BomDtl(bomsn,subitemsn,Reserve1,subname,Reserve2,Reserve3,Reserve4,BaseConsume,SubCls) values(" + bomsn + ",(select max(sn) from allitem),'" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][6].ToString() + "','" + dt.Rows[i][7].ToString() + "','" + dt.Rows[i][8].ToString() + "','" + sclass + "')";
                                        log.Info("sqldtl::::" + sqldtl);
                                        al.Add(sqldtl);
                                    }
                                    else
                                    {
                                        //bomsn,物料sn，料号，名称，规格，材质，表面处理，用量，分类
                                        sqldtl = "insert into BomDtl(bomsn,subitemsn,Reserve1,subname,Reserve2,Reserve3,Reserve4,BaseConsume,SubCls) values(" + bomsn + "," + dtitem.Rows[0]["sn"].ToString() + ",'" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][6].ToString() + "','" + dt.Rows[i][7].ToString() + "','" + dt.Rows[i][8].ToString() + "','" + sclass + "')";
                                        log.Info("sqldtl::::" + sqldtl);
                                        al.Add(sqldtl);
                                    }
                                    #endregion
                                    #region 生成计划
                                     string s = "";
                                     if (dt.Rows[i][9].ToString()=="外购")
                                     {
                                         s = "采购计划";
                                     }
                                     else
                                     {
                                         s = "生产计划";
                                     }
                                     //Thread.Sleep(100);
                                    //序号，料号，名称，规格，计划名称
                                     sql = "insert into PrdMissionSL(PrdMissionNo,itemno,itemname,spec,recClass) values('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + s + "')";
                                     log.Info("plan::::" + sql);
                                     al.Add(sql);
                                    #endregion
                                }
                                #endregion
                            }
                        }
                        if(SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                        {
                            Alert.Show("导入成功");
                        }
                        else
                        {
                            Alert.Show("导入失败");
                        }
                        al.Clear();
                        #endregion
                    }
                    else
                    {
                        Alert.Show("NO Data");
                    }
                }
                

            }
            catch (Exception ee)
            {
                log.Info(ee.ToString());
            }
            finally
            {
                BindGrid();
            }
        }


        #endregion
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Grid1.PageSize = 1000000;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
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

    }
}