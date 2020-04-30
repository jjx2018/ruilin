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
    public partial class magItemColor : PageBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger("magItemColor.aspx");
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "ItemColorView";
            }
        }

        #endregion

        private bool AppendToEnd = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var appdb = new AppContext())
                {
                    IQueryable<OrderHeader> q = appdb.orderheader;
                    ddlOrderNo.DataSource = q.ToList();
                    ddlOrderNo.DataTextField = "OrderNo";
                    ddlOrderNo.DataValueField = "OrderNo";
                    ddlOrderNo.DataBind();
                    IQueryable<COLOR> q1 = appdb.colors;
                    ddlColor.DataSource = q1.ToList();
                    ddlColor.DataTextField = "name";
                    ddlColor.DataValueField = "name";
                    ddlColor.DataBind();
                    //IQueryable<BomDtl> q2 = appdb.bomdtl;
                    //ddlOrderNo.DataSource = q2.ToList();
                    //ddlOrderNo.DataTextField = "Reserve1";
                    //ddlOrderNo.DataValueField = "Reserve1";
                    //ddlOrderNo.DataBind();
                }
                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                // 新增数据初始值
                JObject defaultObj = new JObject();
                defaultObj.Add("OrderNo", "");
                DayOfWeek dw = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).DayOfWeek;
                string ss = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dw);
                defaultObj.Add("PID", "");
                defaultObj.Add("ItemNo", "");
                defaultObj.Add("Color", "");
                

                defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));

                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);



                // 删除选中行按钮
                //btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                CheckPowerWithButton("ItemColorDelete", btnDelete);
                ResolveDeleteButtonForGrid(btnDelete, Grid1);

                // 每页记录数
                Grid1.PageSize = ConfigHelper.PageSize;
                Grid2.PageSize = ConfigHelper.PageSize;
                ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
                // 绑定表格
                //BindGrid("", 1);
                BindGrid();
                //BindGrid2();
            }
        }
        public void BindddlOrderNo(object sender, EventArgs e)
        {
            //Alert.Show(ddlOrderNo.SelectedValue);
            using (var appdb = new AppContext())
            {
                IQueryable<OrderDetail> q1 = appdb.orderdetail;
                q1 = q1.Where(u => u.OrderNo == ddlOrderNo.SelectedValue);
                ddlPID.DataSource = q1.ToList();
                ddlPID.DataTextField = "Reserve2";
                ddlPID.DataValueField = "Reserve2";
                ddlPID.DataBind();
            }
        }
        public void BindPID(object sender, EventArgs e)
        {
            using (var appdb = new AppContext())
            {
                IQueryable<BomBase> q3 = appdb.bombase;
                BomBase current = appdb.bombase
                   .Where(u => u.MastItemNo == ddlPID.SelectedValue).FirstOrDefault();
                if (current != null)
                {
                    IQueryable<BomDtl> q2 = appdb.bomdtl;
                    q2 = q2.Where(u => u.BOMSN == current.SN);
                    ddlItemNo.DataSource = q2.ToList();
                    ddlItemNo.DataTextField = "Reserve1";
                    ddlItemNo.DataValueField = "Reserve1";
                    ddlItemNo.DataBind();
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
                if (!CheckPower("ItemColorDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                using (var appdb = new AppContext())
                {

                    appdb.allitems.Where(t => t.Sn == sn).Delete();
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
            if (!CheckPower("ItemColorDelete"))
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
                    appdb.itemcolors.Where(u => ids.Contains(u.SN)).Delete();
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
                IQueryable<ItemColor> q = appdb.itemcolors;

                // 在职务名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(t => t.ItemNo.Contains(searchText) || t.OrderNo.Contains(searchText) || t.PID.Contains(searchText) || t.Color.Contains(searchText));
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();

                // 排列和分页
                q = SortAndPage<ItemColor>(q, Grid1);

                Grid1.DataSource = q;
                Grid1.DataBind();
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

        private void BindGrid2()
        {
            using (var appdb = new AppContext())
            {
                IQueryable<Client> q = appdb.clients;

                // 在职务名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(t => t.Name.Contains(searchText));
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = q.Count();

                // 排列和分页
                q = SortAndPage<Client>(q, Grid1);

                Grid2.DataSource = q;
                Grid2.DataBind();
            }
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
                List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                for (int i = 0; i < newAddedList.Count; i++)
                {
                    //s += newAddedList[i]["ItemNo"].ToString() + "----" + newAddedList[i]["Name"].ToString() + "----" + newAddedList[i]["Spec"].ToString() + "----" + newAddedList[i]["MaterialNo"].ToString() + "----" + newAddedList[i]["ItemColor"].ToString();
                    sql = "insert into itemcolor(OrderNo,PID,ItemNo,Color) values('" + newAddedList[i]["OrderNo"].ToString() + "','" + newAddedList[i]["PID"].ToString() + "','" + newAddedList[i]["ItemNo"].ToString() + "','" + newAddedList[i]["Color"].ToString() + "')";
                    s += sql + "---";
                    al.Add(sql);
                    sql = "update bomdtl set reserve4='" + newAddedList[i]["Color"].ToString() + "' where bomsn=(select sn from bombase where mastitemno='" + newAddedList[i]["PID"].ToString() + "' ) and reserve1='" + newAddedList[i]["ItemNo"].ToString() + "' ";
                    al.Add(sql);
                }

                //Alert.Show(s);
                //return;
                //s = "";
                // 修改的现有数据
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                SQLHelper.DbHelperSQL.SetConnectionString("");
                string sqldtl = "", reserve4 = "", mastitemno = "", reserve1="";
                foreach (int rowIndex in modifiedDict.Keys)
                {
                    sql = "update itemcolor set ";
                   
                    for (int i = 0; i < Grid1.Columns.Count; i++)
                    {
                        if (modifiedDict[rowIndex].ContainsKey(Grid1.Columns[i].ColumnID))
                        {
                            sql += Grid1.Columns[i].ColumnID + "='" + modifiedDict[rowIndex][Grid1.Columns[i].ColumnID].ToString() + "',";
                            if (Grid1.Columns[i].ColumnID == "PID")
                            {
                                mastitemno = modifiedDict[rowIndex][Grid1.Columns[i].ColumnID].ToString();
                            }
                            else
                            {
                                mastitemno = Grid1.Rows[rowIndex].Values[2].ToString();
                            }
                            if (Grid1.Columns[i].ColumnID == "ItemNo")
                            {
                                reserve1 = modifiedDict[rowIndex][Grid1.Columns[i].ColumnID].ToString();
                            }
                            else
                            {
                                reserve1 = Grid1.Rows[rowIndex].Values[3].ToString();
                            }
                            if (Grid1.Columns[i].ColumnID == "Color")
                            {
                                reserve4 = modifiedDict[rowIndex][Grid1.Columns[i].ColumnID].ToString();
                            }
                            else
                            {
                                reserve4 = Grid1.Rows[rowIndex].Values[4].ToString();
                            }
                        }

                    }
                    sql = sql.TrimEnd(new char[] { ',' });
                    sql += " where sn=" + Grid1.DataKeys[rowIndex][0];
                    s += sql + "------";
                    al.Add(sql);
                    sqldtl = "update bomdtl set reserve4='" + reserve4 + "' where bomsn=(select sn from bombase where mastitemno='" + mastitemno + "' ) and reserve1='" + reserve1 + "' ";
                    al.Add(sqldtl);
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
                Alert.Show(ee.Message);
            }
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            //if (filePhoto.HasFile)
            //{
            //    string fileName = filePhoto.ShortFileName;
            //    if (!filePhoto.HasFile)
            //    {
            //        // 清空文件上传控件
            //        filePhoto.Reset();
            //        return;
            //    }


            //    fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
            //    fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

            //    filePhoto.SaveAs(Server.MapPath("~/upload/" + fileName));
            //    readExcel(fileName);


            //}
        }

        private void readExcel(string filename)
        {
            try
            {
                string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/upload/" + filename) + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"";

                using (OleDbConnection conn = new OleDbConnection(connstring))
                {
                    conn.Open();
                    DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字  
                    string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字  
                    string sql = string.Format("SELECT * FROM [{0}]", firstSheetName); //查询字符串   


                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                    DataSet set = new DataSet();
                    ada.Fill(set);
                    DataTable dt = set.Tables[0];
                    int i = 0, tb = 1;
                    string s = "";
                    SQLHelper.DbHelperSQL.SetConnectionString("");
                    string[] ss = null;
                    for (; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][2].ToString() == "早餐")
                        {
                            tb = 1;
                        }
                        else if (dt.Rows[i][2].ToString() == "午餐")
                        {
                            tb = 2;
                        }
                        else if (dt.Rows[i][2].ToString() == "晚餐")
                        {
                            tb = 3;
                        }
                        s = dt.Rows[i][4].ToString().Replace("\r\n", "、").Replace("\r", "、").Replace("\n", "、");
                        ss = s.Split(new string[] { "、" }, StringSplitOptions.RemoveEmptyEntries);
                        s = string.Join("、", ss);
                        sql = "update MenuAndTB set menucontent='" + s + "',state=1 where sdate='" + dt.Rows[i][0].ToString() + "' and tb=" + tb;
                        log.Info(sql);
                        SQLHelper.DbHelperSQL.ExecuteSql(sql, 30);
                    }
                    if (i == dt.Rows.Count)
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
                //BindGrid("", Grid1.PageIndex + 1);
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
    }
}