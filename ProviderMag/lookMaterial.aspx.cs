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

namespace AppBoxPro.ProviderMag
{
    public partial class lookMaterial : PageBase
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
                return "AllitemView";
            }
        }

        #endregion
        public string pstr = "";
        private bool AppendToEnd = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var appdb = new AppContext())
                {
                    IQueryable<GoodsClass> q = appdb.goodsclass;
                    ddlItemClass.DataSource = q.ToList();
                    ddlItemClass.DataTextField = "ClassName";
                    ddlItemClass.DataValueField = "ClassName";
                    ddlItemClass.DataBind();
                    IQueryable<Provider> pq = appdb.providers;
                    ddlSupplierId.DataSource = pq.ToList();
                    ddlSupplierId.DataTextField = "Name";
                    ddlSupplierId.DataValueField = "SN";
                    ddlSupplierId.DataBind();
                    pstr = "({";
                    foreach (Provider p in pq.ToList())
                    {
                        pstr += "\"" + p.SN.ToString() + "\":\"" + p.Name + "\",";
                    }
                    pstr = pstr.TrimEnd(new char[] { ',' });
                    pstr += "})";
                    //var qw =from a in appdb.allitems where a.WorkShop!=null select new { a.WorkShop };
                }

                SQLHelper.DbHelperSQL.SetConnectionString("");
                string sql = "select distinct(WorkShop) WorkShop from rlitems where WorkShop is not null ";
                DataTable dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                ddlWorkShop.DataSource = dt;
                ddlWorkShop.DataValueField = "WorkShop";
                ddlWorkShop.DataTextField = "WorkShop";
                ddlWorkShop.DataBind();
               
                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                // 新增数据初始值
                JObject defaultObj = new JObject();
                defaultObj.Add("ItemNo", "");
                DayOfWeek dw = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).DayOfWeek;
                string ss = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dw);
                defaultObj.Add("Name", "");
                defaultObj.Add("Spec", "");
                defaultObj.Add("Material", "");
                defaultObj.Add("ItemColor", "");
                defaultObj.Add("ClassName", "");

                defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));

                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);



                // 删除选中行按钮
                //btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                CheckPowerWithButton("AllitemDelete", btnDelete);
                ResolveDeleteButtonForGrid(btnDelete, Grid1);

                // 每页记录数
                Grid1.PageSize = ConfigHelper.PageSize;
                ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
                // 绑定表格
                //BindGrid("", 1);
                BindGrid();
                if (!CheckPower("FinanceView"))
                {

                    GridColumn column = Grid1.FindColumn("Price");
                    column.Hidden = !column.Hidden;
                }
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
                if (!CheckPower("AllitemDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                using (var appdb = new AppContext())
                {

                    appdb.allitems.Where(t => t.SN == sn).Delete();
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
            if (!CheckPower("AllitemDelete"))
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
                    appdb.allitems.Where(u => ids.Contains(u.SN)).Delete();
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
                int SupplierId = GetQueryIntValue("pid");
                IQueryable<RLItems> q = appdb.allitems.Where(u=>u.SupplierId==SupplierId);

                // 在职务名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(t => t.ItemName.Contains(searchText) || t.Sclass.Contains(searchText) || t.Material.Contains(searchText) || t.ItemNo.Contains(searchText));
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();

                // 排列和分页
                q = SortAndPage<RLItems>(q, Grid1);

                Grid1.DataSource = q;
                Grid1.DataBind();
            }
            if (!CheckPower("FinanceView"))
            {

                GridColumn column = Grid1.FindColumn("Price");
                column.EnableColumnHide = true;
            }
        }

        //        private void BindGrid(string sortfield, int pageindex)
        //        {

        //            string sql = "select tb,menucontent,sdate,datename(weekday, sdate) weekday,money,state from   dbo.MenuAndTB ";
        //            string state = "";
        //            if (rbtState.SelectedValue == "all")
        //            {
        //                state = "";
        //            }
        //            else
        //            {
        //                state = " and state=" + rbtState.SelectedValue;
        //            }
        //            string tb = string.Join(",", dinnertypeChk.SelectedValueArray);
        //            if (!string.IsNullOrEmpty(tb))
        //            {
        //                tb = " and tb in (" + tb + ")";
        //            }
        //            string sdate = "";
        //            if (dcstartdate.Text != "" && dcendDate.Text == "")
        //            {
        //                sdate = " and sdate>='" + dcstartdate.Text + "'";
        //            }
        //            else if (dcstartdate.Text == "" && dcendDate.Text != "")
        //            {
        //                sdate = " and sdate<='" + dcendDate.Text + "'";
        //            }
        //            else if (dcstartdate.Text != "" && dcendDate.Text != "")
        //            {
        //                sdate = " and sdate between '" + dcstartdate.Text + "' and '" + dcendDate.Text + "'";
        //            }
        //            if (string.IsNullOrEmpty(sdate))
        //            {
        //                sdate = " and sdate>=CONVERT(varchar(100), GETDATE(), 23)";
        //            }
        //            sql = @"select top " + Grid1.PageSize + @" o.* from 
        //(select row_number() over(order by sdate) as rownumber,oo.* from
        //(
        //select id,(case when sdate>getdate()-1 then 'true' else 'false' end) overday,(case tb when 1 then '早餐' when 2 then '午餐' when 3 then '晚餐' when 4 then '宵夜' end) tb2, tb,menucontent,sdate,datename(weekday, sdate) weekday,money,(case state when 1 then '已发布' else '未发布' end) state,year(sdate) yy,month(sdate) mm,day(sdate) dd from dbo.MenuAndTB a 
        //where 1=1 " + state + @" " + tb + @" " + sdate + @"
        //) as oo 
        //) as o
        //where rownumber>" + ((pageindex - 1) * Grid1.PageSize) + @"
        //order by sdate,tb"; //and (DATENAME(weekday,sdate)!='星期六' and DATENAME(weekday,sdate)!='星期日') 
        //            SQLHelper.DbHelperSQL.SetConnectionString("");
        //            DataTable table = SQLHelper.DbHelperSQL.ExecuteReturnDataTable(sql, "dd", 30);
        //            sql = "select count(*) from dbo.MenuAndTB a where  1=1 " + state + " " + tb + " " + sdate + "";
        //            Grid1.RecordCount = int.Parse(SQLHelper.DbHelperSQL.GetSingle(sql, 30));
        //            Grid1.DataSource = table;
        //            Grid1.DataBind();
        //        }
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
                string sql = "", sqlval = "";
                // 新增数据
                List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                //for (int i = 0; i < newAddedList.Count; i++)
                //{

                //    sql = "insert into rlitems(itemno,itemname,Spec,Material,Price,SupplierId,UnitWeight,ProcessCost,ProcessCostType,SurfaceDeal,BaseNum,Sclass,WorkShop,MainFrom,StoreHouse,ProUsingQuantity,ZongCheng) values(";
                //    sql += "'" + newAddedList[i]["ItemNo"].ToString() + "',";
                //    sql += "'" + newAddedList[i]["ItemName"].ToString() + "',";
                //    sql += "'" + newAddedList[i]["Spec"].ToString() + "',"; 
                //    sql += "'" + newAddedList[i]["Material"].ToString() + "',"; 
                //    sql += "" + newAddedList[i]["Price"].ToString() + ","; 
                //    sql += "" + newAddedList[i]["SupplierId"].ToString() + ","; 
                //    sql += "" + newAddedList[i]["UnitWeight"].ToString() + ","; 
                //    sql += "" + newAddedList[i]["ProcessCost"].ToString() + ","; 
                //    sql += "'" + newAddedList[i]["ProcessCostType"].ToString() + "',";
                //    sql += "'" + newAddedList[i]["SurfaceDeal"].ToString() + "',"; 
                //    sql += "" + newAddedList[i]["BaseNum"].ToString() + ","; 
                //    sql += "'" + newAddedList[i]["Sclass"].ToString() + "',";
                //    sql += "'" + newAddedList[i]["WorkShop"].ToString() + "',"; 
                //    sql += "'" + newAddedList[i]["MainFrom"].ToString() + "',"; 
                //    sql += "'" + newAddedList[i]["StoreHouse"].ToString() + "',"; 
                //    sql += "" + newAddedList[i]["ProUsingQuantity"].ToString() + ","; 
                //    sql += "'" + newAddedList[i]["ZongCheng"].ToString() + "')";
                //    log.Info("sql add item:" + sql);
                //    al.Add(sql);
                //}
                for (int i = 0; i < newAddedList.Count; i++)
                {
                    sql = "insert into rlitems(";
                    sqlval = " values(";
                    foreach (string key in newAddedList[i].Keys)
                    {
                        sql += key + ",";
                        sqlval += "'" + newAddedList[i][key].ToString() + "',";
                    }
                    sql = sql.TrimEnd(new char[] { ',' }) + ")";
                    sqlval = sqlval.TrimEnd(new char[] { ',' }) + ")";
                    sql = sql + sqlval;
                    al.Add(sql);
                    log.Info("sql item add:::" + sql);
                }

                //Alert.Show(sql);
                //return;
                //s = "";
                // 修改的现有数
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                SQLHelper.DbHelperSQL.SetConnectionString("");
                foreach (int rowIndex in modifiedDict.Keys)
                {
                    sql = "update rlitems set ";
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
                //al.Add(sql);
                //sql = "update AllItem set ItemNo='" + modifiedDict[rowIndex]["ItemNo"].ToString() + "',Name='" + modifiedDict[rowIndex]["Name"].ToString() + "',Spec='" + modifiedDict[rowIndex]["Spec"].ToString() + "',MaterialNo='" + modifiedDict[rowIndex]["MaterialNo"].ToString() + "',ItemColor='" + modifiedDict[rowIndex]["ItemColor"].ToString() + "',AddReserve1='" + modifiedDict[rowIndex]["AddReserve1"].ToString() + "',ClassName='" + modifiedDict[rowIndex]["ClassName"].ToString() + "' where sn=" + GetSelectedDataKeyID(Grid1);
                //Alert.Show(s);

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
                Alert.Show(ee.ToString());
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
        protected void btndownfile_Click(object sender, EventArgs e)
        {
            downFile(Server.MapPath("~/model/订餐信息模板.xlsx"), "订餐信息模板");
        }
        //下载文件
        private void downFile(string filePath, string fileName)
        {
            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/excel";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + ".xlsx");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

            //System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            //if (fileInfo.Exists == true)
            //{
            //    const long ChunkSize = 102400;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力
            //    byte[] buffer = new byte[ChunkSize];

            //    Response.Clear();
            //    System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
            //    long dataLengthToRead = iStream.Length;//获取下载的文件总大小
            //    Response.ContentType = "application/octet-stream";
            //    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
            //    while (dataLengthToRead > 0 && Response.IsClientConnected)
            //    {
            //        int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
            //        Response.OutputStream.Write(buffer, 0, lengthRead);
            //        Response.Flush();
            //        dataLengthToRead = dataLengthToRead - lengthRead;
            //    }
            //    Response.Close();
            //}




            //FileInfo fileInfo = new FileInfo(filePath);
            //Response.Clear();
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            //Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            //Response.AddHeader("Content-Transfer-Encoding", "binary");
            //Response.ContentType = "application/octet-stream";
            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            //Response.WriteFile(fileInfo.FullName);
            //Response.Flush();
            //Response.End();

        }
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

        protected void tbxItemNo_Blur(object sender, EventArgs e)
        {
            try
            {
                string sql = "select count(*) from rlitems where itemno='" + tbxItemNo.Text + "'";
                SQLHelper.DbHelperSQL.SetConnectionString("");
                if (int.Parse(SQLHelper.DbHelperSQL.GetSingle(sql, 30)) > 0)
                {
                    Alert.Show("料号已经存在");
                }
                else
                {

                }
            }
            catch (Exception ee)
            {
                Alert.Show(ee.Message);
            }
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


                fileName = fileName.Replace(":", "").Replace(" ", "").Replace("\\", "").Replace("/", "");
                fileName = DateTime.Now.Ticks.ToString() + "-" + fileName;

                filePhoto.SaveAs(Server.MapPath("~/upload/" + fileName));
                readExcel(fileName);


            }
        }

        private void readExcel(string filename)
        {

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

                    SQLHelper.DbHelperSQL.SetConnectionString("");

                    ArrayList al = new ArrayList();

                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() == "" && dt.Rows[i][1].ToString() == "" && dt.Rows[i][2].ToString() == "" && dt.Rows[i][3].ToString() == "" && dt.Rows[i][4].ToString() == "" && dt.Rows[i][5].ToString() == "" && dt.Rows[i][6].ToString() == "" && dt.Rows[i][7].ToString() == "" && dt.Rows[i][8].ToString() == "" && dt.Rows[i][9].ToString() == "")
                        {
                            break;
                        }
                        else
                        {
                            sql = "select count(*) from rlitems  where itemno='" + dt.Rows[i][0].ToString() + "'";
                            if (int.Parse(SQLHelper.DbHelperSQL.GetSingle(sql, 30)) > 0)
                            {
                                //sql = "update rlitems set surfacedeal='" + dt.Rows[i][4].ToString() + "' where itemno='" + dt.Rows[i][0].ToString() + "'";
                                //al.Add(sql);
                            }
                            else
                            {
                                sql = "insert into rlitems(itemno,name,spec,Material,surfacedeal,UnitWeight,Sclass,Unit,SupplierId,MainFrom) values('" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][5].ToString() + "','" + dt.Rows[i][6].ToString() + "','" + dt.Rows[i][7].ToString() + "','" + dt.Rows[i][8].ToString() + "','" + dt.Rows[i][9].ToString() + "')";
                                al.Add(sql);
                            }
                        }
                    }
                    if (al.Count > 0 && SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                    {
                        Alert.Show("导入成功");
                    }
                    else
                    {
                        Alert.Show("导入失败");
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
    }
}