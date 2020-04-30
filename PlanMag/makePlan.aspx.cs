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
using Newtonsoft.Json;

namespace AppBoxPro.PlanMag
{
    public partial class makePlan : PageBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Allitem.aspx");
        //StringBuilder cols = new StringBuilder();
        //StringBuilder opers = new StringBuilder();
        //static ArrayList alClickColsName = new ArrayList();
        static Hashtable htClickColsName = new Hashtable();
        //static string ClickColName = "";
        public string pstr = "";
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "PlanView";
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
                    IQueryable<Provider> q = appdb.providers;
                    ddlProvider.DataSource = q.ToList();
                    ddlProvider.DataTextField = "Name";
                    ddlProvider.DataValueField = "SN";
                    ddlProvider.DataBind();

                    ddlSupplierId.DataSource = q.ToList();
                    ddlSupplierId.DataTextField = "Name";
                    ddlSupplierId.DataValueField = "SN";
                    ddlSupplierId.DataBind();
                    
                    pstr = "({";
                    foreach (Provider p in q.ToList())
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
                CheckPowerWithButton("PlanDelete", btnDelete);
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
            //int sn = GetSelectedDataKeyID(Grid1);

            //if (e.CommandName == "Delete")
            //{
            //    // 在操作之前进行权限检查
            //    if (!CheckPower("PlanDelete"))
            //    {
            //        CheckPowerFailWithAlert();
            //        return;
            //    }
            //    using (var appdb = new AppContext())
            //    {

            //        appdb.allitems.Where(t => t.Sn == sn).Delete();
            //    }
            //    BindGrid();
            //}
        }
        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            // 删除新增未保存到数据库的数据
            //List<int> deletedRows = Grid1.GetDeletedList();
            //foreach (int rowIndex in deletedRows)
            //{
            //    //int rowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
            //    Grid1.Rows.RemoveAt(rowIndex);
            //}

            //// 在操作之前进行权限检查
            //if (!CheckPower("PlanDelete"))
            //{
            //    CheckPowerFailWithAlert();
            //    return;
            //}

            //// 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
            //List<int> ids = GetSelectedDataKeyIDs(Grid1);

            //// 执行数据库操作
            ////DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
            ////DB.SaveChanges();
            //if (ids.Count > 0)
            //{
            //    using (var appdb = new AppContext())
            //    {
            //        appdb.allitems.Where(u => ids.Contains(u.Sn)).Delete();
            //    }
            //}
            //// 重新绑定表格
            //BindGrid();
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
            htClickColsName.Clear();
            BindGrid();
        }
        private void BindGrid()
        {
            //string[] colstr = cols.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //string[] operstr=opers.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            using (var appdb = new AppContext())
            {
                
                //IQueryable<Instruction> q = appdb.instruction;
                var q = from a in appdb.instruction
                        join d in appdb.allitems on a.ItemNo equals d.ItemNo into d_join
                        from e in d_join.DefaultIfEmpty()
                        join b in appdb.v_userinfor on a.Receiver equals b.userid into userjoin
                        from c in userjoin.DefaultIfEmpty()
                        
                        select new { a.SN, a.OrderNo, a.ProNo, a.ProName, a.ItemNo, a.ItemName, a.MainFrom, a.UsingQuantity, a.ConfirmQuantity, a.RealUsingQuantity, a.IsConfirm, a.Spec, a.Material, a.SurfaceDeal, a.Sclass, a.ReceiveDept, a.Receiver, c.ChineseName, a.ConfirmDate, a.BarCode, a.Remark,a.BomSN,a.IsPlan ,e.SupplierId};
                q = q.Where(u => u.RealUsingQuantity != 0 && u.IsConfirm == 1);

                if (rbtmakethod.SelectedValue != "")
                {
                    q = q.Where(u => u.MainFrom == rbtmakethod.SelectedValue);
                }
                if (rbtIsPlan.SelectedValue != "")
                {
                    int k = int.Parse(rbtIsPlan.SelectedValue);
                    q = q.Where(u => u.IsPlan == k);
                }
                // 在职务名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(t => t.ProName.Contains(searchText) || t.ItemName.Contains(searchText) || t.ProNo.Contains(searchText) || t.ItemNo.Contains(searchText));
                }

                foreach (DictionaryEntry de in htClickColsName)
                {
                    switch(de.Key.ToString())
                    {
                        case "OrderNo":
                            q = q.Where(u => u.OrderNo == de.Value.ToString());
                            break;
                        case "ProNo":
                            q = q.Where(u => u.ProNo == de.Value.ToString());
                            break;
                        case "ProName":
                            q = q.Where(u => u.ProName == de.Value.ToString());
                            break;
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
                        case "Sclass":
                            q = q.Where(u => u.Sclass == de.Value.ToString());
                            break;
                        case "MainFrom":
                            q = q.Where(u => u.MainFrom == de.Value.ToString());
                            break;
                        case "UsingQuantity":
                            q = q.Where(u => u.UsingQuantity.ToString() == de.Value.ToString());
                            break;
                        case "ReceiveDept":
                            q = q.Where(u => u.ReceiveDept == de.Value.ToString());
                            break;
                        case "Receiver":
                            q = q.Where(u => u.Receiver == de.Value.ToString());
                            break;
                        case "ConfirmDate":
                            q = q.Where(u => u.ConfirmDate.ToString() == de.Value.ToString());
                            break;
                    }
                    FileOper.writeLog("----------------------------------------------------------");
                    FileOper.writeLog("de.key::" + de.Key + ":::de.value::" + de.Value);
                    FileOper.writeLog("----------------------------------------------------------");
                }
                FileOper.writeLog("----------------------------------------------------------");
                FileOper.writeLog(q.ToString());
                FileOper.writeLog("----------------------------------------------------------");
                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();

                // 排列和分页
                q = SortAndPage(q, Grid1);

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
                //List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                //for (int i = 0; i < newAddedList.Count; i++)
                //{
                //    //s += newAddedList[i]["ItemNo"].ToString() + "----" + newAddedList[i]["Name"].ToString() + "----" + newAddedList[i]["Spec"].ToString() + "----" + newAddedList[i]["MaterialNo"].ToString() + "----" + newAddedList[i]["ItemColor"].ToString();
                //    sql = "insert into AllItem(ItemNo,Name,Spec,MaterialNo,ItemColor,AddReserve1,ClassName) values('" + newAddedList[i]["ItemNo"].ToString() + "','" + newAddedList[i]["Name"].ToString() + "','" + newAddedList[i]["Spec"].ToString() + "','" + newAddedList[i]["MaterialNo"].ToString() + "','" + newAddedList[i]["ItemColor"].ToString() + "','" + newAddedList[i]["AddReserve1"].ToString() + "','" + newAddedList[i]["ClassName"].ToString() + "')";
                //    s += sql + "---";
                //    al.Add(sql);
                //}

                //Alert.Show(s);
                //return;
                //s = "";
                // 修改的现有数据
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                SQLHelper.DbHelperSQL.SetConnectionString("");
                foreach (int rowIndex in modifiedDict.Keys)
                {
                    sql = "update Instruction set ";
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


        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xlsx");
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

        protected void txtConfirmQuantity_Blur(object sender, EventArgs e)
        {
            try
            {
                //FineUIPro.TextBox ctrl = (FineUIPro.TextBox)Grid1.FindControl("txtConfirmQuantity");

                //Alert.Show(ctrl.Text);
                //return;
                if (Grid1.GetModifiedDict().Count <= 0)
                {
                    return;
                }
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                string s = "";
                foreach (int rowIndex in modifiedDict.Keys)
                {
                    s = modifiedDict[rowIndex]["ConfirmQuantity"].ToString();
                    double d = (double.Parse(Grid1.Rows[rowIndex].Values[7].ToString()) - double.Parse(s));
                    //Grid1.UpdateCellValue(rowIndex, "ConfirmQuantity", s);
                    Grid1.UpdateCellValue(rowIndex, "RealUsingQuantity", d.ToString());
                    Grid1.UpdateCellValue(rowIndex, "ConfirmDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Grid1.UpdateCellValue(rowIndex, "IsConfirm", "1");
                    Grid1.UpdateCellValue(rowIndex, "BarCode", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    System.Threading.Thread.Sleep(1);
                }
            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
        }

        protected void rbtmakethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            //生产、采购、委外、客供
            if (rbtmakethod.SelectedValue == "生产")
            {
                btnProduce.Enabled = true;
                btnPurchase.Enabled = false;
                btnSendOutPlan.Enabled = false;
                btnClientProvider.Enabled = false;
            }
            else if (rbtmakethod.SelectedValue == "采购")
            {
                btnProduce.Enabled = false;
                btnPurchase.Enabled = true;
                btnSendOutPlan.Enabled = false;
                btnClientProvider.Enabled = false;
            }
            else if (rbtmakethod.SelectedValue == "委外")
            {
                btnProduce.Enabled = false;
                btnPurchase.Enabled = false;
                btnSendOutPlan.Enabled = true;
                btnClientProvider.Enabled = false;
            }
            else   //客供
            {
                btnProduce.Enabled = false;
                btnPurchase.Enabled = false;
                btnSendOutPlan.Enabled = false;
                btnClientProvider.Enabled = true;
            }

            BindGrid();
        }

        protected void btnProduce_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid1.GetModifiedDict().Count > 0)
                {
                    Alert.Show("请先保存数据再生成计划");
                    return;
                }
                if (Grid1.SelectedRowIndex == -1)
                {
                    Alert.Show("请先选择数据");
                    return;
                }
                if(!dpPlanFinish.SelectedDate.HasValue)
                {
                    Alert.Show("计划完成日期不能为空");
                    return;
                }
                if (SaveProducePlan())
                {
                    Alert.Show("保存成功");
                    BindGrid();
                    dpPlanFinish.SelectedDate = null;
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
        private bool SaveProducePlan()
        {
            string sql = "select max(ProPlanNo) from ProducePlan where InputeDate>=CONVERT(varchar(100), GETDATE(), 23)";
            SQLHelper.DbHelperSQL.SetConnectionString("");
            string ProPlanNo = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
            if (string.IsNullOrEmpty(ProPlanNo))
            {
                sql = "Select CONVERT(varchar(100), GETDATE(), 112)";
                ProPlanNo = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() + "0001";
            }
            else
            {
                ProPlanNo = (Int64.Parse(ProPlanNo) + 1).ToString();
            }
            //,StockQuantity ,ProducePlandQuantity
            ArrayList al = new ArrayList();
            foreach (int i in Grid1.SelectedRowIndexArray)
            {
                sql = "INSERT INTO ProducePlan (SeqNo ,ProPlanNo ,Properties ,PDate ,Checker ,Dept ,ClientId,ProNo ,ProName ,ItemNo ,ItemName ,Spec ,Quantity ,Unit ,SaleOrderNo ,ClientOrderNo,State ,ProLotNo,WorkShop,Inputer,InputeDate,BomSN,PlanFinishDate,PlanStartDate,ZhuangPeiDate,ISN) VALUES(";
                sql += "'" + i.ToString().PadLeft(3 - i.ToString().Length, '0') + "','" + ProPlanNo + "','" + txtProperity.Text + "',getdate(),'" + GetChineseName() + "','" + GetDeptName() + "','','" + Grid1.Rows[i].Values[2].ToString() + "','" + Grid1.Rows[i].Values[3].ToString() + "','" + Grid1.Rows[i].Values[4].ToString() + "','" + Grid1.Rows[i].Values[5].ToString() + "','" + Grid1.Rows[i].Values[11].ToString() + "','" + Grid1.Rows[i].Values[9].ToString() + "','PCS','" + Grid1.Rows[i].Values[1].ToString() + "','',0,'lotno',(SELECT workshop from BomDetail where sn=" + Grid1.Rows[i].Values[20].ToString() + "),'" + User.Identity.Name + "',getdate()," + Grid1.Rows[i].Values[20].ToString() + ",'"+dpPlanFinish.SelectedDate.Value+ "','" + dpPlanStart.SelectedDate.Value + "','"+dpZhuangPei.SelectedDate.Value+"',"+Grid1.DataKeys[i][0]+")";
                al.Add(sql);
                log.Info("sqlpur::::" + sql);
                sql = "update Instruction set isplan=1 where sn=" + Grid1.DataKeys[i][0].ToString();
                al.Add(sql);
                log.Info("sqlpur::::" + sql);

            }
            if (al.Count > 0)
            {
                return SQLHelper.DbHelperSQL.ExecuteSqlTran(al);
            }
            else
            {
                return false;
            }
        }
        private bool SavePurchasePlan()
        {
            string sql = "select max(PurPlanNo) from PurchasePlan where InputeDate>=CONVERT(varchar(100), GETDATE(), 23)";
            SQLHelper.DbHelperSQL.SetConnectionString("");
            string PurPlanNo = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
            if (string.IsNullOrEmpty(PurPlanNo))
            {
                sql = "Select CONVERT(varchar(100), GETDATE(), 112)";
                PurPlanNo = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() + "0001";
            }
            else
            {
                PurPlanNo = (Int64.Parse(PurPlanNo) + 1).ToString();
            }
            //,StockQuantity ,PurchasePlandQuantity
            ArrayList al = new ArrayList();
            foreach (int i in Grid1.SelectedRowIndexArray)
            {
                sql = "INSERT INTO PurchasePlan (SeqNo ,PurPlanNo ,Properties ,PDate ,Purchaser ,Dept ,Project ,Provider ,ProviderName ,ItemNo ,ItemName ,Spec ,Quantity ,PreDeliveryDate ,Unit ,SaleOrderNo ,State ,PreBill,Inputer,InputeDate,prono,ProName,BomSN,PlanFinishDate,PlanStartDate,ZhuangPeiDate,ISN) VALUES(";
                sql += "'" + (i+1).ToString().PadLeft(3 - (i + 1).ToString().Length, '0') + "','" + PurPlanNo + "','" + txtProperity.Text + "',getdate(),'" + GetChineseName() + "','" + GetDeptName() + "','" + txtProject.Text + "',(select supplierid from RLItems where itemno='"+ Grid1.Rows[i].Values[4].ToString() + "'),(select b.Name from RLItems a,provider b where a.SupplierId=b.SN and ItemNo='" + Grid1.Rows[i].Values[4].ToString() + "'),'" + Grid1.Rows[i].Values[4].ToString() + "','" + Grid1.Rows[i].Values[5].ToString() + "','" + Grid1.Rows[i].Values[11].ToString() + "','" + Grid1.Rows[i].Values[9].ToString() + "','" + dpPreDeliveryDate.Text + "','PCS','" + Grid1.Rows[i].Values[1].ToString() + "',0,'" + txtPreBill.Text + "','" + User.Identity.Name + "',getdate(),'" + Grid1.Rows[i].Values[2].ToString() + "','" + Grid1.Rows[i].Values[3].ToString() + "'," + Grid1.Rows[i].Values[20].ToString() + ",'" + dpPlanFinish.SelectedDate.Value + "','" + dpPlanStart.SelectedDate.Value + "','"+dpZhuangPei.SelectedDate.Value+ "'," + Grid1.DataKeys[i][0] + ")";
                al.Add(sql);
                log.Info("sqlpur::::" + sql);
                sql = "update Instruction set isplan=1 where sn=" + Grid1.DataKeys[i][0].ToString();
                al.Add(sql);
                log.Info("sqlpur::::" + sql);

            }
            if (al.Count > 0)
            {
                return SQLHelper.DbHelperSQL.ExecuteSqlTran(al);
            }
            else
            {
                return false;
            }
        }
        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid1.GetModifiedDict().Count > 0)
                {
                    Alert.Show("请先保存数据再生成计划");
                    return;
                }
                if (Grid1.SelectedRowIndex == -1)
                {
                    Alert.Show("请先选择数据");
                    return;
                }
                if (!dpPlanStart.SelectedDate.HasValue)
                {
                    Alert.Show("计划开工日期不能为空");
                    return;
                }
                if (!dpPlanFinish.SelectedDate.HasValue)
                {
                    Alert.Show("计划完成日期不能为空");
                    return;
                }
                if (SavePurchasePlan())
                {
                    Alert.Show("保存成功");
                    BindGrid();
                    dpPlanStart.SelectedDate = null;
                    dpPlanFinish.SelectedDate = null;
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

        protected void rbtIsPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtIsPlan.SelectedValue == "" || rbtIsPlan.SelectedValue == "1")
            {
                btnProduce.Enabled = false;
                btnPurchase.Enabled = false;
            }
            else
            {
                if (rbtmakethod.SelectedValue == "生产")
                {
                    btnProduce.Enabled = true;
                    btnPurchase.Enabled = false;
                }
                else if (rbtmakethod.SelectedValue == "采购")
                {
                    btnProduce.Enabled = false;
                    btnPurchase.Enabled = true;
                }
                else
                {
                    btnProduce.Enabled = false;
                    btnPurchase.Enabled = false;
                }
            }
            BindGrid();
        }

        protected void btnSendOutPlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid1.GetModifiedDict().Count > 0)
                {
                    Alert.Show("请先保存数据再生成计划");
                    return;
                }
                if (Grid1.SelectedRowIndex == -1)
                {
                    Alert.Show("请先选择数据");
                    return;
                }
                if (!dpPlanFinish.SelectedDate.HasValue)
                {
                    Alert.Show("计划完成日期不能为空");
                    return;
                }
                if (SaveSendOutPlan())
                {
                    Alert.Show("保存成功");
                    dpPlanFinish.SelectedDate = null;
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
        private bool SaveSendOutPlan()
        {
            string sql = "select max(SendOutPlanNo) from SendOutPlan where InputeDate>=CONVERT(varchar(100), GETDATE(), 23)";
            SQLHelper.DbHelperSQL.SetConnectionString("");
            string SendOutPlanNo = SQLHelper.DbHelperSQL.GetSingle(sql, 30);
            if (string.IsNullOrEmpty(SendOutPlanNo))
            {
                sql = "Select CONVERT(varchar(100), GETDATE(), 112)";
                SendOutPlanNo = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() + "0001";
            }
            else
            {
                SendOutPlanNo = (Int64.Parse(SendOutPlanNo) + 1).ToString();
            }

            ArrayList al = new ArrayList();
            foreach (int i in Grid1.SelectedRowIndexArray)
            {
                sql = "INSERT INTO SendOutPlan (SendOutPlanNo,ProviderID ,Provider ,ProNo ,ProName ,ItemNo ,ItemName  ,Unit,Spec ,Quantity ,SaleOrderNo ,State ,WorkShop,Inputer,InputeDate,BomSN,PlanFinishDate,PlanStartDate,ZhuangPeiDate,ISN) VALUES(";
                sql += "'" + SendOutPlanNo + "','" + ddlProvider.SelectedValue + "','" + ddlProvider.SelectedText + "','" + Grid1.Rows[i].Values[2].ToString() + "','" + Grid1.Rows[i].Values[3].ToString() + "','" + Grid1.Rows[i].Values[4].ToString() + "','" + Grid1.Rows[i].Values[5].ToString() + "','PCS','" + Grid1.Rows[i].Values[11].ToString() + "','" + Grid1.Rows[i].Values[9].ToString() + "','" + Grid1.Rows[i].Values[1].ToString() + "',0,(SELECT workshop from BomDetail where  SN=" + Grid1.Rows[i].Values[20].ToString() + "),'" + User.Identity.Name + "',getdate()," + Grid1.Rows[i].Values[20].ToString() + ",'" + dpPlanFinish.SelectedDate.Value + "','" + dpPlanStart.SelectedDate.Value + "','"+dpZhuangPei.SelectedDate.Value+ "'," + Grid1.DataKeys[i][0] + ")";
                al.Add(sql);
                log.Info("sqlpur::::" + sql);
                sql = "update Instruction set isplan=1 where  sn=" + Grid1.DataKeys[i][0].ToString();
                al.Add(sql);
                log.Info("sqlpur::::" + sql);

            }
            if (al.Count > 0)
            {
                return SQLHelper.DbHelperSQL.ExecuteSqlTran(al);
            }
            else
            {
                return false;
            }
        }

        protected void btnClientProvider_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid1.GetModifiedDict().Count > 0)
                {
                    Alert.Show("请先保存数据再生成计划");
                    return;
                }
                if (Grid1.SelectedRowIndex == -1)
                {
                    Alert.Show("请先选择数据");
                    return;
                }
                if (!dpPlanFinish.SelectedDate.HasValue)
                {
                    Alert.Show("计划完成日期不能为空");
                    return;
                }
                if (SaveClientProviderPlan())
                {
                    Alert.Show("保存成功");
                    dpPlanFinish.SelectedDate = null;
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
        private bool SaveClientProviderPlan()
        {
            string sql = "select max(CPPlanNo) from CPlan where InputeDate>=CONVERT(varchar(100), GETDATE(), 23)";
            SQLHelper.DbHelperSQL.SetConnectionString("");
            string CPPlanNo = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
            if (string.IsNullOrEmpty(CPPlanNo))
            {
                sql = "Select CONVERT(varchar(100), GETDATE(), 112)";
                CPPlanNo = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() + "0001";
            }
            else
            {
                CPPlanNo = (Int64.Parse(CPPlanNo) + 1).ToString();
            }
            ArrayList al = new ArrayList();
            foreach (int i in Grid1.SelectedRowIndexArray)
            {
                sql = "INSERT INTO CPlan (SeqNo ,CPPlanNo ,Properties ,PDate ,ClientProvider ,Dept ,Project ,Provider ,ProviderName ,ItemNo ,ItemName ,Spec ,Quantity ,PreDeliveryDate ,Unit ,SaleOrderNo ,State ,PreBill,Inputer,InputeDate,prono,ProName,BomSN,PlanFinishDate,PlanStartDate,ZhuangPeiDate,ISN) VALUES(";
                sql += "'" + i.ToString().PadLeft(3 - i.ToString().Length, '0') + "','" + CPPlanNo + "','" + txtProperity.Text + "',getdate(),'" + GetChineseName() + "','" + GetDeptName() + "','" + txtProject.Text + "','" + ddlProvider.SelectedValue + "','" + ddlProvider.SelectedText + "','" + Grid1.Rows[i].Values[4].ToString() + "','" + Grid1.Rows[i].Values[5].ToString() + "','" + Grid1.Rows[i].Values[11].ToString() + "','" + Grid1.Rows[i].Values[9].ToString() + "','" + dpPreDeliveryDate.Text + "','PCS','" + Grid1.Rows[i].Values[1].ToString() + "',0,'" + txtPreBill.Text + "','" + User.Identity.Name + "',getdate(),'" + Grid1.Rows[i].Values[2].ToString() + "','" + Grid1.Rows[i].Values[3].ToString() + "'," + Grid1.Rows[i].Values[20].ToString() + ",'" + dpPlanFinish.SelectedDate.Value + "','" + dpPlanStart.SelectedDate.Value + "','"+dpZhuangPei.SelectedDate.Value+ "'," + Grid1.DataKeys[i][0] + ")";
                al.Add(sql);
                log.Info("sqlpur::::" + sql);
                sql = "update Instruction set isplan=1 where sn=" + Grid1.DataKeys[i][0].ToString();
                al.Add(sql);
                log.Info("sqlpur::::" + sql);

            }
            if (al.Count > 0)
            {
                return SQLHelper.DbHelperSQL.ExecuteSqlTran(al);
            }
            else
            {
                return false;
            }
        }

        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            //JArray ja=Grid1.FilteredData;
            //cols.Clear();
            //opers.Clear();
            //FileOper.writeLog(Grid1.FilteredData.ToString());
            //for (int j = 0; j < ja.Count; j++)
            //{
            //    cols.Append(ja[j]["column"].ToString() + ",");
            //    //opers.Append(ja[j]["matcher"].ToString() + ",");
            //    FileOper.writeLog("::::column::::" + ja[j]["column"].ToString() + ":::matcher::::" + ja[j]["matcher"].ToString());
            //    JArray subja = (JArray)JsonConvert.DeserializeObject(ja[j]["items"].ToString());
            //    for (int i = 0; i < subja.Count; i++)
            //    {
            //        opers.Append(subja[i]["operator"].ToString() +":" + subja[i]["value"].ToString() + ",");
            //        FileOper.writeLog("operator:"+subja[i]["operator"].ToString()+ "::value:" + subja[i]["value"].ToString());
            //    }
            //}
            //BindGrid();
        }
        #region FilterDataRowItemImplement

        private bool FilterDataRowItemImplement(object sourceObj, string fillteredOperator, JToken fillteredObj, string column)
        {
            //string matcher = ja[j]["matcher"].ToString();
            //if (matcher == "equal")
            //{
            //    opers.Append("=,");
            //}
            //else if (matcher == "contain")
            //{
            //    opers.Append("like,");
            //}
            //else if (matcher == "start")
            //{
            //    opers.Append(">=,");
            //}
            //else if (matcher == "end")
            //{

            //}
            //else if (matcher == "greater")
            //{

            //}
            //else if (matcher == "less")
            //{

            //}
            bool valid = false;

            if (column == "OrderNo")
            {
                string sourceValue = sourceObj.ToString();
                string fillteredValue = fillteredObj.Value<string>();
                if (fillteredOperator == "equal")
                {
                    if (sourceValue == fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "contain")
                {
                    if (sourceValue.Contains(fillteredValue))
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "start")
                {
                    if (sourceValue.StartsWith(fillteredValue))
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "end")
                {
                    if (sourceValue.EndsWith(fillteredValue))
                    {
                        valid = true;
                    }
                }
            }
            else if (column == "EntranceYear")
            {
                int sourceValue = Convert.ToInt32(sourceObj);
                int fillteredValue = fillteredObj.Value<int>();

                if (fillteredOperator == "greater")
                {
                    if (sourceValue > fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "less")
                {
                    if (sourceValue < fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "equal")
                {
                    if (sourceValue == fillteredValue)
                    {
                        valid = true;
                    }
                }

            }
            else if (column == "LogTime")
            {
                // 时间比较时要去掉数据源中的时分秒！
                DateTime sourceDate = Convert.ToDateTime(sourceObj);
                DateTime sourceValue = new DateTime(sourceDate.Year, sourceDate.Month, sourceDate.Day);

                DateTime fillteredValue = fillteredObj.Value<DateTime>();

                if (fillteredOperator == "greater")
                {
                    if (sourceValue > fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "less")
                {
                    if (sourceValue < fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "equal")
                {
                    if (sourceValue == fillteredValue)
                    {
                        valid = true;
                    }
                }

            }
            else if (column == "Major" || column == "Group")
            {
                string sourceValue = sourceObj.ToString();
                JArray fillteredValue = (JArray)fillteredObj;

                foreach (string filltereditem in fillteredValue)
                {
                    if (filltereditem == sourceValue)
                    {
                        valid = true;
                        break;
                    }
                }
            }
            else if (column == "AtSchool")
            {
                bool sourceValue = Convert.ToBoolean(sourceObj);
                bool fillteredValue = fillteredObj.Value<bool>();

                if (sourceValue == fillteredValue)
                {
                    valid = true;
                }
            }
            else if (column == "Gender")
            {
                int sourceValue = Convert.ToInt32(sourceObj);
                int fillteredValue = fillteredObj.Value<int>();

                if (sourceValue == fillteredValue)
                {
                    valid = true;
                }
            }


            return valid;
        }

        #endregion

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
             
            //object[] o=  Grid1.Rows[e.RowIndex].Values;
            string[] s = Grid1.SelectedCell;
            if(s[1]== "RealUsingQuantity")
            {
                return;
            }
            //GridRow row=Grid1.Rows[e.RowIndex];
            //DataRowView rowview = row.DataItem as DataRowView
            for (int i=0;i<Grid1.Columns.Count;i++)
            {
                if(s[1]==Grid1.Columns[i].ColumnID&&!htClickColsName.ContainsKey(s[1]))
                {
                    //ClickColVal = Grid1.Rows[e.RowIndex].Values[i].ToString();
                    //ClickRowIndex = e.RowIndex;
                    htClickColsName.Add(s[1], Grid1.Rows[e.RowIndex].Values[i].ToString());
                    //ClickColName = s[1];
                    break;
                }
            }
            BindGrid();
            //txtClickColsName.Text = string.Join(",", (string[])alClickColsName.ToArray(typeof(string)));
            updatecol();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            //Alert.Show("ClickColName::" + ClickColName);
            string[] s = txtClickColsName.Text.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
            if(s==null||s.Length==0)
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
                txtClickColsName.Text += de.Key.ToString()+",";
            }
            
        }

        protected void btnSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                if(Grid1.GetModifiedDict().Count==0)
                {
                    Alert.Show("没有要保存的数据");
                    return;
                }
                ArrayList al = new ArrayList();
                string sql = "";
                // 修改的现有数据
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                SQLHelper.DbHelperSQL.SetConnectionString("");
                foreach (int rowIndex in modifiedDict.Keys)
                {
                    sql = "update instruction set ";
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
                if (al.Count > 0 && SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
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

        protected void Grid1_AfterEdit(object sender, GridAfterEditEventArgs e)
        {

        }
    }
}