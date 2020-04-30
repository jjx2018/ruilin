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
    public partial class ItemPurSelfUse : PageBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Allitem.aspx");
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
                    IQueryable<ItemClass> q = appdb.itemclasses;
                    ddlItemClass.DataSource = q.ToList();
                    ddlItemClass.DataTextField = "Name";
                    ddlItemClass.DataValueField = "Name";
                    ddlItemClass.DataBind();
                    IQueryable<Provider> pq = appdb.providers;
                    ddlProvider.DataSource = pq.ToList();
                    ddlProvider.DataTextField = "Name";
                    ddlProvider.DataValueField = "SN";
                    ddlProvider.DataBind();

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
                }
                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                // 新增数据初始值
                JObject defaultObj = new JObject();
                defaultObj.Add("ItemNo", "");
                DayOfWeek dw = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).DayOfWeek;
                string ss = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dw);
                defaultObj.Add("Name", "");
                defaultObj.Add("Spec", "");
                defaultObj.Add("MaterialNo", "");
                defaultObj.Add("ItemColor", "");
                defaultObj.Add("AddReserve1", "");
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
                Grid2.PageSize = ConfigHelper.PageSize;
                ddlGridPageSize2.SelectedValue = ConfigHelper.PageSize.ToString();
                BindGrid();
                BindGrid4();
                if (!CheckPower("FinanceView"))
                {

                    GridColumn column = Grid1.FindColumn("Price");
                    column.Hidden = true;
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
                IQueryable<RLItems> q = appdb.allitems;

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
        protected void btnSave2_Click(object sender, EventArgs e)
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
                if (Grid1.GetNewAddedList().Count != 0)
                {
                    // 新增数据
                    List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                    for (int i = 0; i < newAddedList.Count; i++)
                    {
                        //s += newAddedList[i]["ItemNo"].ToString() + "----" + newAddedList[i]["Name"].ToString() + "----" + newAddedList[i]["Spec"].ToString() + "----" + newAddedList[i]["MaterialNo"].ToString() + "----" + newAddedList[i]["ItemColor"].ToString();
                        sql = "insert into AllItem(ItemNo,Name,Spec,MaterialNo,ItemColor,AddReserve1,ClassName) values('" + newAddedList[i]["ItemNo"].ToString() + "','" + newAddedList[i]["Name"].ToString() + "','" + newAddedList[i]["Spec"].ToString() + "','" + newAddedList[i]["MaterialNo"].ToString() + "','" + newAddedList[i]["ItemColor"].ToString() + "','" + newAddedList[i]["AddReserve1"].ToString() + "','" + newAddedList[i]["ClassName"].ToString() + "')";
                        s += sql + "---";
                        al.Add(sql);
                    }
                }
                //Alert.Show(s);
                //return;
                //s = "";
                if (Grid1.GetModifiedData().Count != 0)
                {
                    // 修改的现有数据
                    Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                    SQLHelper.DbHelperSQL.SetConnectionString("");
                    foreach (int rowIndex in modifiedDict.Keys)
                    {
                        sql = "update AllItem set ";
                        for (int i = 0; i < Grid1.Columns.Count; i++)
                        {
                            if (modifiedDict[rowIndex].ContainsKey(Grid1.Columns[i].ColumnID))
                            {
                                sql += Grid1.Columns[i].ColumnID + "='" + modifiedDict[rowIndex][Grid1.Columns[i].ColumnID].ToString() + "',";
                            }

                        }
                        sql = sql.TrimEnd(new char[] { ',' });
                        sql += " where sn=" + Grid1.DataKeys[rowIndex][0];
                        //s += sql + "------";
                        al.Add(sql);
                    }
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
                   
                    sql = "insert into rlitems(itemno,itemname,Spec,Material,Price,SupplierId,UnitWeight,ProcessCost,ProcessCostType,SurfaceDeal,BaseNum,Sclass,WorkShop,MainFrom,StoreHouse,ProUsingQuantity,ZongCheng) values('" + newAddedList[i]["ItemNo"].ToString() + "','" + newAddedList[i]["ItemName"].ToString() + "','" + newAddedList[i]["Spec"].ToString() + "','" + newAddedList[i]["Material"].ToString() + "'," + newAddedList[i]["Price"].ToString() + "," + newAddedList[i]["SupplierId"].ToString() + "," + newAddedList[i]["UnitWeight"].ToString() + "," + newAddedList[i]["ProcessCost"].ToString() + ",'" + newAddedList[i]["ProcessCostType"].ToString() + "','" + newAddedList[i]["SurfaceDeal"].ToString() + "'," + newAddedList[i]["BaseNum"].ToString() + ",'" + newAddedList[i]["Sclass"].ToString() + "','" + newAddedList[i]["WorkShop"].ToString() + "','" + newAddedList[i]["MainFrom"].ToString() + "','" + newAddedList[i]["StoreHouse"].ToString() + "'," + newAddedList[i]["ProUsingQuantity"].ToString() + ",'" + newAddedList[i]["ZongCheng"].ToString() + "')";
                    log.Info("sql add item:" + sql);
                    al.Add(sql);
                }

                //Alert.Show(s);
                //return;
                //s = "";
                // 修改的现有数据
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
                    s += sql + "------";
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

        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {

        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            int sn = GetSelectedDataKeyID(Grid2);

            if (e.CommandName == "Delete")
            {
                Grid2.Rows.RemoveAt(e.RowIndex);
                
            }
            
        }

        protected void Grid2_PageIndexChange(object sender, GridPageEventArgs e)
        {

        }

        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {

        }

        protected void TwinTriggerBox1_Trigger2Click(object sender, EventArgs e)
        {

        }

        protected void TwinTriggerBox1_Trigger1Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch2_Click(object sender, EventArgs e)
        {

        }
         

        protected void Button3_Click(object sender, EventArgs e)
        {

        }

        protected void btnPurSave_Click(object sender, EventArgs e)
        {
           if( save())
           {
               Alert.Show("保存成功");
           }
            else
           {
               Alert.Show("保存失败");
           }
        }

        protected void ddlGridPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAddPur_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndex == -1)
            {
                Alert.Show("请先选择数据");
                return;
            }
            // 删除选中单元格的客户端脚本
            string deleteScript = Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid2.GetDeleteSelectedRowsReference(), String.Empty);
            // 新增数据初始值
            JObject defaultObj = new JObject();
             int[] selections = Grid1.SelectedRowIndexArray;
             foreach (int rowIndex in selections)
             {
                 defaultObj.Add("Provider", Grid1.Rows[rowIndex].Values[6].ToString());
                 defaultObj.Add("ItemNo", Grid1.Rows[rowIndex].Values[1].ToString());
                 defaultObj.Add("ItemName", Grid1.Rows[rowIndex].Values[2].ToString());
                 defaultObj.Add("Spec", Grid1.Rows[rowIndex].Values[3].ToString());
                defaultObj.Add("Quantity", "");
                defaultObj.Add("SN", Grid1.DataKeys[rowIndex][0].ToString());

                defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));
                 PageContext.RegisterStartupScript(Grid2.GetAddNewRecordReference(defaultObj, AppendToEnd, "Quantity"));
                 defaultObj.RemoveAll();
             }
           
            

        }

        protected void ddlOrderno_TextChanged(object sender, EventArgs e)
        {
            BindGrid4();
        }
         private void BindGrid4()
        {
            using (var appdb = new AppContext())
            {
                var q = from a in appdb.orderdetail
                        join b in appdb.orderheader on a.FSN equals b.SN
                        select a;

                string searchText = ddlOrderno.Text;
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.ItemName.Contains(searchText) || u.ItemNo.Contains(searchText) || u.ClinetNo.Contains(searchText) || u.OrderNo.Contains(searchText)); ;
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid4.RecordCount = q.Count();

                // 排列和分页
                //q = SortAndPage<Provider>(q, Grid4);

                Grid4.DataSource = q;
                Grid4.DataBind();
            }
        }

         protected void Grid4_RowClick(object sender, GridRowClickEventArgs e)
         {
             ddlOrderno.Text = Grid4.Rows[e.RowIndex].Values[0].ToString();
             txtProno.Text = Grid4.Rows[e.RowIndex].Values[1].ToString();
             txtProName.Text = Grid4.Rows[e.RowIndex].Values[2].ToString();
             
         }
         private bool save()
         {
             bool ret = false;
             try
             {
                 string sql = "select max(RIGHT(PURORDERNO,LEN(PURORDERNO)-2)) from PurchaseOrderHeader where InputeDate>=CONVERT(varchar(100), GETDATE(), 23)";
                 SQLHelper.DbHelperSQL.SetConnectionString("");
                 string PurOrderNo = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
                 if (string.IsNullOrEmpty(PurOrderNo))
                 {
                     sql = "Select CONVERT(varchar(100), GETDATE(), 112)";
                     PurOrderNo = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() + "0001";
                 }
                 else
                 {
                     PurOrderNo = (Int64.Parse(PurOrderNo) + 1).ToString();
                 }
                 ArrayList al = new ArrayList();
                 int row = 0;
                 //,Properties ,PDate ,Purchaser ,Dept ,Project ,Provider ,ProviderName
                 //Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                 List<Dictionary<string, object>> newAddedList = Grid2.GetNewAddedList();
                 if (Grid2.GetNewAddedList().Count != 0)
                 {
                     for (int i = 0; i < newAddedList.Count; i++)
                     {
                         if (row == 0)
                         {
                             sql = "INSERT INTO PurchaseOrderHeader (PurOrderNo,PurDate,Provider,ProviderID,JBRID,JBRName,ContactMan,Tel,Fax,JHDate,JHPlace,JSFS,ZZSFP,ProviderConfirm,ApproveID,CheckerID,MakerID,Inputer,InputeDate,PurPlanNo,SaleOrderNo) values('PU" + PurOrderNo + "','" + XDDate.Text + "',(select name from Provider where sn=" + newAddedList[i]["Provider"].ToString() + "),'" + newAddedList[i]["Provider"].ToString() + "','" + User.Identity.Name + "','" + GetChineseName() + "','" + txtContactMan.Text + "','" + txtTel.Text + "','" + txtFax.Text + "','" + JHdate.Text + "','锐麟厂','月结',0,'','','','" + User.Identity.Name + "','" + User.Identity.Name + "',getdate(),NULL,'" + txtOrderno.Text + "')";
                             al.Add(sql);
                             row = 1;
                             log.Info("sqlpur::::" + sql);
                         }
                         sql = "INSERT INTO PurchaseOrderDetail (FSN,PurOrderNo,PurPlanNo,SaleOrderNo,ProNo,ProName,ItemNo,ItemName,Spec,Quantity,Unit,Remark,Inputer,InputeDate,allitemSN) values((select max(sn) from PurchaseOrderHeader where PurOrderNo='PU" + PurOrderNo+"'),'PU" + PurOrderNo + "',NULL,'" + txtOrderno.Text + "','" + txtProno.Text + "','" + txtProName.Text + "','" + newAddedList[i]["ItemNo"].ToString() + "' ,'" + newAddedList[i]["ItemName"].ToString() + "' ,'" + newAddedList[i]["Spec"].ToString() + "' ,'" + newAddedList[i]["Quantity"].ToString() + "','PCS','','" + User.Identity.Name + "',getdate()," + newAddedList[i]["SN"].ToString() + ")";
                         al.Add(sql);
                         log.Info("sqlpur::::" + sql);
                         //sql = "update PurchasePlan set State=1 where sn=" + Grid1.DataKeys[rowIndex][0].ToString();
                         //al.Add(sql);
                         //log.Info("sqlpur::::" + sql);
                         //sql = "update AllItem set SupplierId=(select sn from Provider where name='" + modifiedDict[rowIndex]["Provider"].ToString() + "') where itemno='" + Grid1.Rows[rowIndex].Values[6].ToString() + "'";
                         //al.Add(sql);
                         //log.Info("sqlAllItem::::" + sql);
                     }
                 }
                 if (al.Count > 0)
                 {
                     ret= SQLHelper.DbHelperSQL.ExecuteSqlTran(al);
                 }
                 else
                 {
                     ret= false;
                 }
             }
             catch(Exception ee)
             {
                 log.Info(ee.ToString());
             }
             return ret;
         }

    }
}