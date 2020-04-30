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


namespace AppBoxPro.ruilin
{
    public partial class magOrder : PageBase
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

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                using (var appdb = new AppContext())
                {
                    IQueryable<Client> q = appdb.clients;
                    q = q.Where(u => u.subjectcode != null);
                    txtClientCode.DataSource = q.ToList();
                    txtClientCode.DataTextField = "subjectcode";
                    txtClientCode.DataValueField = "subjectcode";
                    txtClientCode.DataBind();
                   
                }
                IQueryable<User> yw = DB.Users;
                txtRecOrderPerson.DataSource = yw.ToList();
                txtRecOrderPerson.DataTextField = "ChineseName";
                txtRecOrderPerson.DataValueField = "ChineseName";
                txtRecOrderPerson.DataBind();
                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                // 新增数据初始值
                JObject defaultObj = new JObject();
                defaultObj.Add("OrderNo", "");
                DayOfWeek dw = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).DayOfWeek;
                string ss = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dw);
                defaultObj.Add("ClientOrderNo", "");
                defaultObj.Add("LotNo", "");
                defaultObj.Add("ClientCode", "");
                defaultObj.Add("RecOrderPerson", "");
                defaultObj.Add("RecOrderDate", DateTime.Now.ToString("yyyy-MM-dd"));
                defaultObj.Add("SendOrderDate", "");
                defaultObj.Add("OutGoodsDate", "");
                defaultObj.Add("Inputer", User.Identity.Name);
                defaultObj.Add("InputerDate", DateTime.Now.ToString("yyyy-MM-dd"));

                defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));

                // 在第一行新增一条数据
                //btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);
                btnNew.OnClientClick = Window2.GetShowReference("~/ruilin/order_new.aspx", "新增订单");
                btnAddDetail.Enabled = false;
                btnDeleteSelected2.Enabled = false;
                btnBOM.Enabled = false;
                btnHeaderBom.Enabled = false;
                //txtOrderNo.Attributes.Add("onkeydown", "if (window.event.keyCode==13) window.event.keyCode=9;");
                
                // 删除选中行按钮
                //btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                CheckPowerWithButton("OrderDelete", btnDelete);
                ResolveDeleteButtonForGrid(btnDelete, Grid1);
                CheckPowerWithButton("OrderDelete", btnDeleteSelected2);
                ResolveDeleteButtonForGrid(btnDeleteSelected2, Grid2);

                btnReset.OnClientClick = SF2.GetResetReference();
                btnClear.OnClientClick = SF1.GetResetReference();
                LoadData();
            }
            else
            {
                string  requestArg =GetRequestEventArgument(); // 此函数所在文件：PageBase.cs
                //log.Info(requestArg);
                if (requestArg.Equals("RefreshGrid2"))
                {
                    BindGrid2();
                }
                else if (requestArg.Equals("RefreshGrid1"))
                {
                    BindGrid();
                }
            }
           
        }
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
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
            datePickerFrom.SelectedDate = DateTime.Today.AddMonths(-6);
            datePickerTo.SelectedDate = DateTime.Today;

            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            BindDDLItemName();
            BindDDLCompany();
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

        protected void ttbSearchMessage2_Trigger2Click(object sender, EventArgs e)
        {
            TwinTriggerBox2.ShowTrigger1 = true;
            BindGrid2();
        }

        protected void ttbSearchMessage2_Trigger1Click(object sender, EventArgs e)
        {
            TwinTriggerBox2.Text = String.Empty;
            TwinTriggerBox2.ShowTrigger1 = false;
            BindGrid2();
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
                DateTime dtend = datePickerTo.SelectedDate == null ? now.AddDays(2) : datePickerTo.SelectedDate.Value.AddDays(1);
                var qqq = from a in appdb.orderheader
                          select a;

                //在产品名称中搜索
                string searchText = ttbSearchMessage.Text;
                if (!String.IsNullOrEmpty(searchText))
                {
                    qqq = qqq.Where(u => u.OrderNo.Contains( searchText)||u.LotNo.Contains(searchText)||u.Inputer.Contains(searchText)||u.ClientCode.Contains(searchText));
                }
                //searchText为空或者选择“全部”，则列出全部
                else
                {

                }
                //qqq = qqq.Where(u => u.RecOrderDate >= dtstart && u.RecOrderDate <= dtend);
                

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = qqq.Count();// q.Count();

                // 排列和数据库分页
                qqq = SortAndPage<OrderHeader>(qqq, Grid1);

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
                int pid =int.Parse( Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString());// GetSelectedDataKeyID(Grid1).ToString();
                //var q = from a in appdb.probombase
                //        group a by new
                //        {
                //            a.ProNo
                //        }
                //            into g
                //            select new
                //            {
                //                g.Key.ProNo,
                //                Ver = g.Max(u => u.Ver)
                //            };
                //var inq = from a in appdb.orderdetail
                //          join b in appdb.orderheader on a.FSN equals b.SN
                //          join c in appdb.allitems on a.ItemNo equals c.ItemNo into itemjoin 
                //          from d in itemjoin.DefaultIfEmpty()
                //          where b.SN == pid
                //          select new {a.OrderNo,a.ItemNo,a.ItemName,a.Quantity,SurfaceDeal=d.SurfaceDeal==null?"":d.SurfaceDeal,a.Price,a.ClinetNo,a.IsBom,a.BomVer,a.SN,a.InputerDate };

                //log.Info("sql order::" + inq.ToString());
                var inq = from a in appdb.orderdetail
                          where a.FSN == pid
                          select a;


                //IQueryable<YW_ProcessRec> q = appdb.processRec; //.Include(u => u.Dept);
                // 在用户名称中搜索
                string searchText = TwinTriggerBox2.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    inq = inq.Where(u => u.ItemNo.Contains(searchText) || u.ItemName.Contains(searchText));
                }
                // 进仓 出仓
               
                //日期 筛选
                if (DateFrom.SelectedDate.HasValue)
                {
                    inq = inq.Where(u => u.InputerDate >= DateFrom.SelectedDate);
                }
                if (DateTo.SelectedDate.HasValue)
                {
                    inq = inq.Where(u => u.InputerDate <= DateTo.SelectedDate);
                }


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = inq.Count();// q.Count();

                // 排列和数据库分页
                inq = SortAndPage(inq, Grid2);

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
            CheckPowerWithLinkButtonField("OrderDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("InstockChangePassword", Grid1, "changePasswordField");

        }
        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("InstockEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("OrderDelete", Grid2, "deleteField");
            //CheckPowerWithWindowField("InstockChangePassword", Grid1, "changePasswordField");

        }
        protected void Grid1_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {
            //User user = e.DataItem as User;

            //// 不能删除超级管理员
            //if (user.Name == "admin")
            //{
            //FineUI.LinkButtonField deleteField = Grid1.FindColumn("deleteField") as FineUI.LinkButtonField;
            //    deleteField.Enabled = false;
            //    deleteField.ToolTip = "不能删除超级管理员！";
            //}

        }
        protected void Grid2_PreRowDataBound(object sender, GridPreRowEventArgs e)
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

        protected void btnDeleteSelected2_Click(object sender, EventArgs e)
        {
            int[] selectrows = Grid2.SelectedRowIndexArray;
            foreach (int rowindex in selectrows)
            {
                if (Grid2.Rows[rowindex].Values[7].ToString() == "1")
                {
                    Alert.Show("第" + (rowindex + 1) + "行产品已生成BOM不能删除");
                    return;
                }
            }
            // 在操作之前进行权限检查
            if (!CheckPower("OrderDelete"))
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
                appdb.orderdetail.Where(u => ids.Contains(u.SN)).Delete();
            }
            // 重新绑定表格
            BindGrid2();
        }



        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            int ID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                if (Grid1.Rows[e.RowIndex].Values[11].ToString() == "1")
                {
                    Alert.Show("订单已审核通过不能删除");
                    return;
                }
                // 在操作之前进行权限检查
                if (!CheckPower("OrderDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                using (AppContext appdb = new AppContext())
                {
                    appdb.orderdetail.Where(u => u.FSN == ID).Delete();
                    appdb.orderheader.Where(u => u.SN == ID).Delete();

                    BindGrid();
                    BindGrid2();
                }
            }
        }
        protected void Grid1_OnRowClick(object sender, FineUIPro.GridRowClickEventArgs e)
        {
            //Alert.Show("rowclick" + e.RowIndex.ToString() + ":::::" + Grid1.SelectedRowIndex);
            if (Grid1.SelectedRowIndex==-1)
            return;
            //Grid2.SelectedRowIndex = e.RowIndex;
            //string meetid = Grid1.Rows[e.RowIndex].Values[1].ToString();
            //Alert.Show(meetid);
            btnAddDetail.Enabled = true;
            btnDeleteSelected2.Enabled = true;
            btnBOM.Enabled = true;
            btnHeaderBom.Enabled = true;
            btnAddDetail.OnClientClick = Window1.GetShowReference("~/ruilin/OrderDetailNew.aspx?id=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString() + "&od=" + Grid1.DataKeys[Grid1.SelectedRowIndex][1].ToString(), "新增产品");
            BindGrid2();

           
        }
        protected void btnSearch_click(object sender, EventArgs e)
        {
            BindGrid();
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

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        protected void ddlItemName_SelectIndexChanged(object sender, EventArgs e)
        {
            BindDDLSpec();
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
                    sql = "insert into OrderHeader(OrderNo,ClientOrderNo,LotNo,ClientCode,RecOrderPerson,RecOrderDate,SendOrderDate,OutGoodsDate,Inputer,InputerDate) values('" + newAddedList[i]["OrderNo"].ToString() + "','" + newAddedList[i]["ClientOrderNo"].ToString() + "','" + newAddedList[i]["LotNo"].ToString() + "','" + newAddedList[i]["ClientCode"].ToString() + "','" + newAddedList[i]["RecOrderPerson"].ToString() + "','" + newAddedList[i]["RecOrderDate"].ToString() + "','" + newAddedList[i]["SendOrderDate"].ToString() + "','" + newAddedList[i]["OutGoodsDate"].ToString() + "','" + newAddedList[i]["Inputer"].ToString() + "','" + newAddedList[i]["InputerDate"].ToString() + "')";
                    s += sql + "---";
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
                    sql = "update OrderHeader set ";
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
        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            // 删除新增未保存到数据库的数据
            List<int> deletedRows = Grid1.GetDeletedList();
            if (deletedRows!=null && Grid1.GetDeletedList().Count != 0)
            {
                foreach (int rowIndex in deletedRows)
                {
                    //int rowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                    Grid1.Rows.RemoveAt(rowIndex);
                }
            }
           int[] selectrows = Grid1.SelectedRowIndexArray;
           foreach (int rowindex in selectrows)
           {
               if(Grid1.Rows[rowindex].Values[11].ToString()=="1")
               {
                   Alert.Show("第" + (rowindex+1) + "行订单已审核通过不能删除");
                   return;
               }
           }
            // 在操作之前进行权限检查
            if (!CheckPower("OrderDelete"))
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
                    if (appdb.orderdetail.Where(u => ids.Contains(u.FSN)&&u.IsBom==1).Count()>0)
                    {
                        Alert.Show("订单中已有生成BOM的产品，无法删除订单");
                        return;
                    }
                    
                    appdb.orderdetail.Where(u => ids.Contains(u.FSN)).Delete();
                    appdb.orderheader.Where(u => ids.Contains(u.SN)).Delete();

                }
            }
            // 重新绑定表格
            BindGrid();
            BindGrid2();
        }

        #endregion
        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            int ID = GetSelectedDataKeyID(Grid2);

            if (e.CommandName == "Delete")
            {
                if (Grid2.Rows[e.RowIndex].Values[7].ToString() == "1")
                {
                    Alert.Show("该产品已生成BOM不能删除");
                    return;
                }
                // 在操作之前进行权限检查
                if (!CheckPower("OrderDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                using (AppContext appdb = new AppContext())
                {
                    appdb.orderdetail.Where(u => u.SN == ID).Delete();

                    BindGrid2();
                }
            }
            else if(e.CommandName == "toBOM")
            {
                string sql = "select count(*) from dbo.proBomHeader where ver=(select max(ver) from proBomHeader where prono='" + Grid2.Rows[e.RowIndex].Values[2].ToString() + "')";
               
                SQLHelper.DbHelperSQL.SetConnectionString("");
                if(SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString()=="0")
                {
                    Alert.Show("该产品未找到对应的工程BOM");
                    return;
                }
                else
                {



                    string s = @" parent.addExampleTab({
                id: " + ID.ToString() + @"+'_tab',
                iframeUrl: 'ruilin/RealBom.aspx?sn="+ID+@"&od="+Grid2.Rows[e.RowIndex].Values[1].ToString()+@"&id="+Grid2.Rows[e.RowIndex].Values[2].ToString()+@"&q="+Grid2.Rows[e.RowIndex].Values[4].ToString()+@"',
                title:'"+Grid2.Rows[e.RowIndex].Values[3].ToString()+@"',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });";
                    PageContext.RegisterStartupScript(s);
                }
            }
        }

        protected void Grid2_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference("~/ruilin/OrderDetailEdit.aspx?id=" + Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString() + "&k=1", "产品详情"));
        }

        protected void Grid2_RowDataBound(object sender, GridRowEventArgs e)
        {
            
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

                filePhoto.SaveAs(Server.MapPath("~/OrderFile/" + fileName));
                readExcel(fileName);


            }
        }

        private void readExcel(string filename)
        {
            string fsn = "";
            try
            {
                string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/OrderFile/" + filename) + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"";
                ArrayList al = new ArrayList();
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
                    string OrderNo = "", LotNo = "", ClientCode = "", ClientOrderNo = "", RecOrderPersonID = "", RecOrderPerson = "", RecOrderDate = "", SendOrderDate = "", OutGoodsDate = "", checkman = "", CheckDate = "", ContainerType = "", ischeck = "", OrderType = "";
                    //产品名称、产品编号、版本、瑞麟编号、客户编号、客户代号、日期
                    
                    //获取表头  添加orderheader
                    if (dt != null && dt.Rows.Count >= 6)
                    {
                        OrderNo = dt.Rows[1][2].ToString();
                        OrderType = dt.Rows[1][5].ToString();
                        LotNo = dt.Rows[1][8].ToString();
                        ClientCode = dt.Rows[1][11].ToString();
                        ClientOrderNo = dt.Rows[2][2].ToString();
                        RecOrderPerson = dt.Rows[3][2].ToString();
                        checkman = dt.Rows[3][11].ToString();
                        ContainerType = dt.Rows[3][8].ToString();
                        RecOrderDate = dt.Rows[4][2].ToString();
                        CheckDate = dt.Rows[4][8].ToString();
                        OutGoodsDate = dt.Rows[4][11].ToString();
                        if (CheckDate != "" || checkman != "")
                        {
                            ischeck = "1";
                        }
                        else
                        {
                            ischeck = "0";
                        }
                        sql = "select sn from OrderHeader where OrderNo='" + OrderNo + "'";
                        log.Info(sql);
                        DataTable dtitem = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                        if (dtitem == null || dtitem.Rows.Count == 0)
                        {
                            //sql = "insert into allitem(itemno,name) values('" + dt.Rows[2][4].ToString() + "','" + dt.Rows[2][2].ToString() + "')";
                            //log.Info("sqlallitem::::" + sql);
                            //al.Add(sql);

                            sql = "insert into OrderHeader(OrderNo,LotNo,ClientCode,ClientOrderNo,RecOrderPerson,RecOrderDate,SendOrderDate,OutGoodsDate,Inputer,InputerDate,IsCheck,OrderExcel,ContainerType,Checker,CheckDate,OrderType) values('" + OrderNo + "','" + LotNo + "','" + ClientCode + "','" + ClientOrderNo + "','" + RecOrderPerson + "','" + RecOrderDate.Replace(".", "-") + "','" + SendOrderDate.Replace(".", "-") + "','" + OutGoodsDate.Replace(".", "-") + "','" + User.Identity.Name + "',getdate()," + ischeck + ",'" + filename + "','" + ContainerType + "','" + checkman + "','" + CheckDate + "','" + OrderType + "')";
                            log.Info("sqlbase::::" + sql);
                            al.Add(sql);
                            //SQLHelper.DbHelperSQL.ExecuteSql(sql, 30);

                        }
                        else
                        {
                            Alert.Show("订单号已存在");
                            return;
                            //产品名称、产品编号、版本、瑞麟编号、客户编号、客户代号、日期
                            //sql = "update OrderHeader set LotNo='" + LotNo + "',ClientCode='" + ClientCode + "',ClientOrderNo='" + ClientOrderNo + "',RecOrderPerson='" + RecOrderPerson + "',RecOrderDate='" + RecOrderDate.Replace(".", "-") + "',SendOrderDate='" + SendOrderDate.Replace(".", "-") + "',OutGoodsDate='" + OutGoodsDate.Replace(".", "-") + "',Updater='" + User.Identity.Name + "',UpdateDate=getdate(),OrderExcel='" + filename + "',ContainerType='" + ContainerType + "',Checker='" + checkman + "',CheckDate='" + CheckDate + "',IsCheck="+ischeck+",OrderType='"+OrderType+"' where  OrderNo='" + OrderNo + "'";
                            //log.Info("sqlbase::::" + sql);
                            //SQLHelper.DbHelperSQL.ExecuteSql(sql, 30);
                        }
                        sql = "select max(sn) from OrderHeader where  OrderNo='" + OrderNo + "'";
                        SQLHelper.DbHelperSQL.SetConnectionString("");
                        fsn = "select max(sn) from OrderHeader where  OrderNo='" + OrderNo + "'"; //SQLHelper.DbHelperSQL.GetSingle(sql).ToString();


                        #region orderdtl add
                        for (i = 6; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][0].ToString() == "" || dt.Rows[i][1].ToString() == "")
                            {
                                break;
                            }
                            else
                            {


                                #region  orderdtl add

                                //料号，名称，规格，材质，表面处理或颜色，底数，类别
                                //ItemNo,Name,Spec,MaterialNo,ItemColor,AddReserve1,ClassName
                                sql = "select top 1 * from OrderDetail where itemno='" + dt.Rows[i][2].ToString() + "' and ItemName='" + dt.Rows[i][1].ToString() + "' and OrderNo='" + OrderNo + "' ";
                                dtitem = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                                if (dtitem == null || dtitem.Rows.Count == 0)
                                {
                                    //sql = "insert into allitem(itemno,name) values('" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "')";
                                    //log.Info("sqlallitem::::" + sql);
                                    //SQLHelper.DbHelperSQL.ExecuteSql(sql, 30);
                                    //bomsn,物料sn，料号，名称，规格，材质，表面处理，用量，分类
                                    sql = "insert into OrderDetail(FSN,OrderNo,ClinetNo,ItemNo,ItemName,Quantity,Demand1,Demand2,Remark,Inputer,InputerDate,Color,Unit,IsNew,IsPackingmaterials,CountryPackVer,IsChange) values((" + fsn + "),'" + OrderNo + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][1].ToString() + "'," + dt.Rows[i][5].ToString() + ",'" + dt.Rows[i][11].ToString() + "','" + dt.Rows[i][12].ToString() + "','','" + User.Identity.Name + "',getdate(),'" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][6].ToString() + "','" + dt.Rows[i][7].ToString() + "','" + dt.Rows[i][8].ToString() + "','" + dt.Rows[i][9].ToString() + "','" + dt.Rows[i][10].ToString() + "')";
                                    log.Info("sqldtl::::" + sql);
                                    al.Add(sql);
                                    //SQLHelper.DbHelperSQL.ExecuteSql(sql, 30);
                                }
                                else
                                {
                                    sql = "update OrderDetail set ClinetNo='" + dt.Rows[i][3].ToString() + "',Quantity=" + dt.Rows[i][5].ToString() + ",Demand1='" + dt.Rows[i][11].ToString() + "',Demand2='" + dt.Rows[i][12].ToString() + "',Updater='" + User.Identity.Name + "',UpdateDate=getdate(),Color='" + dt.Rows[i][4].ToString() + "',Unit='" + dt.Rows[i][6].ToString() + "',IsNew='" + dt.Rows[i][7].ToString() + "',IsPackingmaterials='" + dt.Rows[i][8].ToString() + "',CountryPackVer='" + dt.Rows[i][9].ToString() + "',IsChange='" + dt.Rows[i][10].ToString() + "' where itemno='" + dt.Rows[i][1].ToString() + "' and ItemName='" + dt.Rows[i][2].ToString() + "' and OrderNo='" + OrderNo + "' ";
                                    log.Info("sqldtl::::" + sql);
                                    al.Add(sql);
                                    //SQLHelper.DbHelperSQL.ExecuteSql(sql, 30);
                                }
                                #endregion


                            }
                        }

                        #endregion
                    }
                    else
                    {
                        Alert.Show("没有要导入的数据");
                    }
                }
                if(al.Count>0&&SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                {
                    Alert.Show("导入成功");
                }
                else
                {
                    Alert.Show("导入失败");
                }
                

            }
            catch (Exception ee)
            {
                Alert.Show("导入失败");
                log.Info(ee.ToString());
            }
            finally
            {
                BindGrid();
            }
        }

        protected void btnBOM_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid2.SelectedRowIndexArray.Length <= 0)
                {
                    Alert.Show("请选择产品");
                    return;
                }
                SQLHelper.DbHelperSQL.SetConnectionString("");
                foreach(int i in Grid2.SelectedRowIndexArray)
                {
                    if (Grid2.Rows[i].Values.GetValue(6).ToString() == "0")
                    {
                        string sql = "select count(*) from dbo.proBomHeader where ver=(select max(ver) from proBomHeader where prono='" + Grid2.Rows[i].Values[3].ToString() + "')";
                        if (SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() == "0")
                        {
                            continue;
                        }
                        makeBom(Grid2.Rows[i].Values[3].ToString(), Grid2.Rows[i].Values[1].ToString(), Grid2.Rows[i].Values[5].ToString(), Grid2.DataKeys[i][0].ToString(), "OrderDetail");
                    }
                }
               Alert.Show("生成成功");
            }
            catch(Exception ee )
            {
                Alert.Show("生成失败："+ee.ToString());
            }
        }

        private void makeBom(string itemno, string orderno, string quantity,string keysn,string tabname)
        {
            SQLHelper.DbHelperSQL.SetConnectionString("");
            string sql = "select count(*) from dbo.BomHeader where orderno='" + orderno + "' and prono='" + itemno + "'";
            log.Info("sql11:::::" + sql);
            //if (SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() != "0")
            //{
            //    //sql = "select SN from dbo.BomHeader where orderno='" + orderno + "' and prono='" + itemno + "'";
            //    //txtFSN.Text = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
            //    return;
            //}
            ArrayList al = new ArrayList();
            sql = "INSERT INTO BomHeader(AllitemSN,OrderNo,ProName,ProNo,Ver,MyNo,ClientNo,ClientCode,Inputer,InputeDate,BomDate,ProBomSN)   select AllitemSN,'" + orderno + "',ProName,ProNo,Ver,MyNo,ClientNo,ClientCode,'" + User.Identity.Name + "',getdate(),BomDate,sn from dbo.proBomHeader where prono='" + itemno + "'";
            log.Info("sql22:::::" + sql);
            al.Add(sql);
            sql = "select sn from dbo.proBomHeader where prono='" + itemno + "'";
            string sn = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
            //sql = "INSERT INTO BomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,UsingQuantity,Sclass,MakeMethod,Inputer,InputeDate) select (select max(sn) from BomHeader),AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal," + quantity + "*UsingQuantity,Sclass,MakeMethod,Inputer,getdate() from proBomDetail where fsn=" + sn;

            sql = "INSERT INTO BomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,UsingQuantity,Sclass,MakeMethod,Inputer,InputeDate,ParentSN,SUBSN,ZuHe,WorkShop,seq) select (select max(sn) from BomHeader),AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal," + quantity + "*UsingQuantity,Sclass,MakeMethod,Inputer,getdate(),ParentSN,SN,ZuHe,WorkShop,seq from proBomDetail where fsn=" + sn;

            al.Add(sql);
            log.Info("sql33:::::" + sql);
            if (SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
            {
                sql = "update "+tabname+" set IsBom=1 where sn=" + keysn;
                log.Info("sql44:::::" + sql);
                SQLHelper.DbHelperSQL.ExecuteSql(sql, 30);
                BindGrid2();
            }
            else
            {
                throw new Exception("订单" + orderno + "生成BOM失败");
            }
        }

        protected void btnHeaderBom_Click(object sender, EventArgs e)
        {
            try
            {
                if(Grid1.SelectedRowIndexArray.Length<=0)
                {
                    Alert.Show("请选择订单");
                    return;
                }
                if (Grid1.Rows[Grid1.SelectedRowIndex].Values[11].ToString() == "0")
                {
                    Alert.Show("订单未审核通过不能生成BOM");
                    return;
                }
                string sql = "";
                SQLHelper.DbHelperSQL.SetConnectionString("");
                foreach (int i in Grid1.SelectedRowIndexArray)
                {
                    sql = "select * from orderdetail where IsBom=0 and fsn=" + Grid1.DataKeys[i][0].ToString();
                    DataTable dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        sql = "select count(*) from dbo.proBomHeader where ver=(select max(ver) from proBomHeader where prono='" + dt.Rows[j]["itemno"].ToString() + "')";
                        if (SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() == "0")
                        {
                            continue;
                        }

                        makeBom(dt.Rows[j]["itemno"].ToString(), Grid1.Rows[i].Values[1].ToString(), dt.Rows[j]["quantity"].ToString(), dt.Rows[j]["sn"].ToString(), "OrderDetail");
                    }
                    sql = "update orderheader set IsBom=1 where sn=" + Grid1.DataKeys[i][0].ToString();
                    SQLHelper.DbHelperSQL.ExecuteSql(sql, 30);
                }
                Alert.Show("生成成功");
            }
            catch (Exception ee)
            {
                Alert.Show("生成失败：" + ee.ToString());
            }
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference("~/ruilin/Order_Edit.aspx?id=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString() + "&k=1", "订单"));
        }

        protected void Grid2_RowClick(object sender, GridRowClickEventArgs e)
        {
           
        }


     }
}