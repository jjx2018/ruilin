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
    public partial class CollectBom : PageBase
    {
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
                //using (var appdb = new AppContext())
                //{
                //    IQueryable<Client> q = appdb.clients;
                //    q = q.Where(u => u.subjectcode != null);
                //    txtClientCode.DataSource = q.ToList();
                //    txtClientCode.DataTextField = "subjectcode";
                //    txtClientCode.DataValueField = "subjectcode";
                //    txtClientCode.DataBind();

                //}
                //IQueryable<User> yw = DB.Users;
                //txtRecOrderPerson.DataSource = yw.ToList();
                //txtRecOrderPerson.DataTextField = "ChineseName";
                //txtRecOrderPerson.DataValueField = "ChineseName";
                //txtRecOrderPerson.DataBind();
                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                // 新增数据初始值
                JObject defaultObj = new JObject();
                defaultObj.Add("ItemNo", "");
                DayOfWeek dw = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).DayOfWeek;
                string ss = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dw);
                defaultObj.Add("ItemName", "");
                defaultObj.Add("Spec", "");
                defaultObj.Add("Material", "");
                defaultObj.Add("SurfaceDeal", "");
                defaultObj.Add("UsingQuantity", "");
                defaultObj.Add("Sclass", "");
                defaultObj.Add("MakeMethod", "");
                defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));

                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid2.GetAddNewRecordReference(defaultObj, AppendToEnd);
               
                //txtOrderNo.Attributes.Add("onkeydown", "if (window.event.keyCode==13) window.event.keyCode=9;");

                // 删除选中行按钮
                //btnDelete.OnClientClick = Grid2.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                CheckPowerWithButton("OrderDelete", btnDelete);
                ResolveDeleteButtonForGrid(btnDelete, Grid2);
               
                //btnClear.OnClientClick = SF1.GetResetReference();
                // 每页记录数
                Grid1.PageSize = ConfigHelper.PageSize;
                ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
                Grid2.PageSize = ConfigHelper.PageSize;
                ddlGridPageSize2.SelectedValue = ConfigHelper.PageSize.ToString();
                LoadData();
            }
            else
            {
                string requestArg = GetRequestEventArgument(); // 此函数所在文件：PageBase.cs
                //log.Info(requestArg);
                if (requestArg.Equals("RefreshGrid2"))
                {
                    BindGrid2();
                }
                else if (requestArg.Equals("RefreshGrid"))
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
            //datePickerFrom.SelectedDate = DateTime.Today.AddMonths(-6);
            //datePickerTo.SelectedDate = DateTime.Today;

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
            using (var appdb = new AppContext())
            {
               
                string searchText = TwinTriggerBox1.Text.Trim();
                var instructionsql = from a in appdb.instruction
                                  group a by new
                                  {
                                      a.ItemNo,a.ItemName
                                  } into g
                                  select new
                                  {
                                      g.Key.ItemNo,g.Key.ItemName,
                                      confirmquantity = (double)g.Sum(p => p.ConfirmQuantity)
                                  };
                //Alert.Show(instructionsql.ToString());
                var bomdtlgroup = from a in appdb.bomdtl
                                  group a by new { a.ItemNo, a.ItemName } into g
                                  select new Item{ ItemNo=g.Key.ItemNo, ItemName=g.Key.ItemName, UsingQuantity = g.Sum(p => p.OrderUsingQuantity) };
                //Alert.Show(bomdtlgroup.ToString());
                var q = from a in bomdtlgroup
                        join b in instructionsql on new { a.ItemNo, a.ItemName } equals new {b.ItemNo,b.ItemName} into b_join
                        from b in b_join.DefaultIfEmpty()
                       
                        select new  { ItemNo = a.ItemNo, ItemName = a.ItemName, UsingQuantity = a.UsingQuantity - (b.confirmquantity == null ? 0 : b.confirmquantity) };
                //Alert.Show(q.ToString());
                //string sql = "select b.ItemNo,b.ItemName,sum(b.UsingQuantity) UsingQuantity from BomDetaiL b,BomHeader c where b.fsn=c.sn and b.itemno not in(select itemno from instruction where orderno=c.orderno and prono=c.prono) GROUP BY b.ItemNo,b.ItemName,b.UsingQuantity ";
                //sql = "select b.ItemNo,b.ItemName,sum(b.usingquantity)-(case when c.confirmquantity is null then 0 else c.confirmquantity end) UsingQuantity from BomDetaiL b left join (select  itemno,sum(confirmquantity) confirmquantity from instruction a group by  itemno) c on b.itemno=c.itemno ";
                ////var q=from a in appdb.bomdtl 

                
                //if (!String.IsNullOrEmpty(searchText))
                //{
                //    sql += " where b.itemno like '%" + searchText + "%' ";
                //}
                //sql += " GROUP BY b.ItemNo,b.ItemName,b.usingquantity ,c.confirmquantity";
                //log.Info("sql grid1::"+sql);
                
                //IQueryable<Item> q = appdb.items.SqlQuery(sql).AsQueryable();

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.ItemName.Contains(searchText) || u.ItemNo.Contains(searchText)); ;
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
                string  itemno = Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString();// GetSelectedDataKeyID(Grid1).ToString();
                double usingquantity = double.Parse(Grid1.Rows[Grid1.SelectedRowIndex].Values[3].ToString());
                var instructionsql = from a in appdb.instruction
                                     group a by new
                                     {
                                         a.ItemNo,a.ItemName,a.OrderNo,a.ProNo,a.ProName
                                     } into g
                                     select new
                                     {
                                         g.Key.ItemNo,g.Key.ItemName,g.Key.OrderNo,g.Key.ProNo,g.Key.ProName,
                                         confirmquantity = (double)g.Sum(p => p.ConfirmQuantity)
                                     };
                //Alert.Show(instructionsql.ToString());
                var qqq = from a in appdb.bomdtl
                          from d in appdb.bombase  
                          where a.FSN==d.SN && a.ItemNo==itemno
                          select new MyBom { SN = a.SN, FSN = a.FSN, AllitemSN = a.AllitemSN, ItemNo = a.ItemNo, ItemName = a.ItemName, Spec = a.Spec, Material = a.Material, SurfaceDeal = a.SurfaceDeal, UsingQuantity = a.OrderUsingQuantity, Sclass = a.Sclass, MakeMethod = a.MakeMethod, Inputer = a.Inputer, InputeDate = a.InputeDate, Updater = a.Updater, UpdateDate = a.UpdateDate, WorkShop = a.WorkShop, OrderNo = d.OrderNo, ProNo = d.ProNo, ProName = d.ProName };
                   qqq=   from g in qqq       
                          join b in instructionsql on new { g.ItemNo,g.OrderNo,g.ProNo } equals new { b.ItemNo, b.OrderNo,b.ProNo } into b_join
                          from b in b_join.DefaultIfEmpty()
                          where (g.UsingQuantity - (b.confirmquantity == null ? 0 : b.confirmquantity))>0
                          select new MyBom { SN = g.SN, FSN = g.FSN, AllitemSN = g.AllitemSN, ItemNo = g.ItemNo, ItemName = g.ItemName, Spec = g.Spec, Material = g.Material, SurfaceDeal = g.SurfaceDeal, UsingQuantity = g.UsingQuantity - (b.confirmquantity == null ? 0 : b.confirmquantity), Sclass = g.Sclass, MakeMethod = g.MakeMethod, Inputer = g.Inputer, InputeDate = g.InputeDate, Updater = g.Updater, UpdateDate = g.UpdateDate, WorkShop = g.WorkShop, OrderNo = g.OrderNo, ProNo = g.ProNo, ProName = g.ProName };

                //Alert.Show(qqq.ToString());


                //IQueryable<YW_ProcessRec> q = appdb.processRec; //.Include(u => u.Dept);
                // 在用户名称中搜索
                string searchText = TwinTriggerBox1.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    qqq = qqq.Where(u => u.ItemNo.Contains(searchText) || u.ItemName.Contains(searchText));
                }
                // 进仓 出仓

                //日期 筛选
                //if (DateFrom.SelectedDate.HasValue)
                //{
                //    inq = inq.Where(u => u.InputerDate >= DateFrom.SelectedDate);
                //}
                //if (DateTo.SelectedDate.HasValue)
                //{
                //    inq = inq.Where(u => u.InputerDate <= DateTo.SelectedDate);
                //}


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = qqq.Count();// q.Count();

                // 排列和数据库分页
                //qqq = SortAndPage<MyBom>(qqq, Grid2);
                qqq = SortAndPage(qqq, Grid2);

                Grid2.DataSource = qqq;// itemq.Take(2);// q;
                Grid2.DataBind();
            }
        }

        #endregion

        #region Events


        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("InstockEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("OrderDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("InstockChangePassword", Grid1, "changePasswordField");

        }
        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("InstockEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("OrderDelete", Grid2, "deleteField");
            //CheckPowerWithWindowField("InstockChangePassword", Grid1, "changePasswordField");

        }
        protected void Grid2_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
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

        protected void btnDeleteSelected2_Click(object sender, EventArgs e)
        {
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



        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            int ID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
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
        protected void Grid2_OnRowClick(object sender, FineUIPro.GridRowClickEventArgs e)
        {
            //Alert.Show("rowclick" + e.RowIndex.ToString() + ":::::" + Grid1.SelectedRowIndex);
            if (Grid1.SelectedRowIndex == -1)
                return;
            //Grid2.SelectedRowIndex = e.RowIndex;
            //string meetid = Grid1.Rows[e.RowIndex].Values[1].ToString();
            //Alert.Show(meetid);
            //btnAddDetail.Enabled = true;
            //btnDeleteSelected2.Enabled = true;
            //btnAddDetail.OnClientClick = Window1.GetShowReference("~/ruilin/OrderDetailNew.aspx?id=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString() + "&od=" + Grid1.DataKeys[Grid1.SelectedRowIndex][1].ToString(), "新增产品");
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
        protected void ddlGridPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid2.PageSize = Convert.ToInt32(ddlGridPageSize2.SelectedValue);

            BindGrid2();
        }

        protected void ddlItemName_SelectIndexChanged(object sender, EventArgs e)
        {
            BindDDLSpec();
        }



        #endregion
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            //int ID = GetSelectedDataKeyID(Grid1);

            //if (e.CommandName == "Delete")
            //{
            //    // 在操作之前进行权限检查
            //    if (!CheckPower("OrderDelete"))
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

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference("~/ruilin/OrderDetailEdit.aspx?id=" + Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString() + "&k=1", "产品详情"));
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {

        }

        protected void tbxItemNo_TriggerClick(object sender, EventArgs e)
        {
            string[] selectedCell = Grid2.SelectedCell;
            if (selectedCell != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference("searchitemforadd.aspx?rowid=" + selectedCell[0]));

            }
        }

        protected void Grid1_RowSelect(object sender, GridRowSelectEventArgs e)
        {
            BindGrid2();
        }

        protected void txtMaterial_TriggerClick(object sender, EventArgs e)
        {
            string[] selectedCell = Grid2.SelectedCell;
            if (selectedCell != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference("searchBomForAdd.aspx?rowid=" + selectedCell[0]));

            }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                // 新增数据初始值
                JObject defaultObj = new JObject();
                string deleteScript = GetDeleteScript();

                int[] selections = Grid2.SelectedRowIndexArray;
                foreach (int rowIndex in selections)
                {
                    defaultObj.Add("ItemNo", Grid2.Rows[rowIndex].Values[1].ToString());
                    defaultObj.Add("ItemName", Grid2.Rows[rowIndex].Values[2].ToString());
                    defaultObj.Add("Spec", Grid2.Rows[rowIndex].Values[3].ToString());
                    defaultObj.Add("Material", Grid2.Rows[rowIndex].Values[4].ToString());
                    defaultObj.Add("SurfaceDeal", Grid2.Rows[rowIndex].Values[5].ToString());
                    defaultObj.Add("UsingQuantity", Grid2.Rows[rowIndex].Values[6].ToString());
                    defaultObj.Add("Sclass", Grid2.Rows[rowIndex].Values[7].ToString());
                    defaultObj.Add("MakeMethod", Grid2.Rows[rowIndex].Values[8].ToString());
                    defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));
                    PageContext.RegisterStartupScript(Grid2.GetAddNewRecordReference(defaultObj, AppendToEnd));
                    defaultObj.RemoveAll();
                }

            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid2.GetModifiedData().Count == 0 && Grid2.GetNewAddedList().Count == 0)
                {
                    Alert.Show("表格数据没有变化！");
                    return;
                }
                ArrayList al = new ArrayList();
                string sql = "", s = "";
                // 新增数据
                List<Dictionary<string, object>> newAddedList = Grid2.GetNewAddedList();
                for (int i = 0; i < newAddedList.Count; i++)
                {
                    //s += newAddedList[i]["ItemNo"].ToString() + "----" + newAddedList[i]["Name"].ToString() + "----" + newAddedList[i]["Spec"].ToString() + "----" + newAddedList[i]["MaterialNo"].ToString() + "----" + newAddedList[i]["ItemColor"].ToString();
                    sql = "INSERT INTO BomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,UsingQuantity,Sclass,MakeMethod,Inputer,InputeDate) values('" + Grid2.Rows[i].Values[11].ToString() + "',(select sn from allitem where itemno='" + newAddedList[i]["ItemNo"].ToString() + "'),'" + newAddedList[i]["ItemNo"].ToString() + "','" + newAddedList[i]["ItemName"].ToString() + "','" + newAddedList[i]["Spec"].ToString() + "','" + newAddedList[i]["Material"].ToString() + "','" + newAddedList[i]["SurfaceDeal"].ToString() + "'," + newAddedList[i]["UsingQuantity"].ToString() + ",'" + newAddedList[i]["Sclass"].ToString() + "','" + newAddedList[i]["MakeMethod"].ToString() + "','" + User.Identity.Name + "',getdate())";
                    s += sql + "---";
                    al.Add(sql);
                }

                //Alert.Show(s);
                //return;
                //s = "";
                // 修改的现有数据
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid2.GetModifiedDict();
                SQLHelper.DbHelperSQL.SetConnectionString("");
                foreach (int rowIndex in modifiedDict.Keys)
                {
                    sql = "update BomDetail set ";
                    for (int i = 0; i < Grid2.Columns.Count; i++)
                    {
                        if (modifiedDict[rowIndex].ContainsKey(Grid2.Columns[i].ColumnID))
                        {
                            sql += Grid2.Columns[i].ColumnID + "='" + modifiedDict[rowIndex][Grid2.Columns[i].ColumnID].ToString() + "',";
                        }

                    }
                    sql = sql.TrimEnd(new char[] { ',' });
                    sql += " where sn=" + Grid2.DataKeys[rowIndex][0];
                    s += sql + "------";
                    al.Add(sql);
                }
                //al.Add(sql);
                //sql = "update AllItem set ItemNo='" + modifiedDict[rowIndex]["ItemNo"].ToString() + "',Name='" + modifiedDict[rowIndex]["Name"].ToString() + "',Spec='" + modifiedDict[rowIndex]["Spec"].ToString() + "',MaterialNo='" + modifiedDict[rowIndex]["MaterialNo"].ToString() + "',ItemColor='" + modifiedDict[rowIndex]["ItemColor"].ToString() + "',AddReserve1='" + modifiedDict[rowIndex]["AddReserve1"].ToString() + "',ClassName='" + modifiedDict[rowIndex]["ClassName"].ToString() + "' where sn=" + GetSelectedDataKeyID(Grid2);
                //Alert.Show(s);

                SQLHelper.DbHelperSQL.SetConnectionString("");
                if (SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                {
                    Alert.Show("保存成功");
                    BindGrid2();
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
            List<int> deletedRows = Grid2.GetDeletedList();
            foreach (int rowIndex in deletedRows)
            {
                //int rowID = Convert.ToInt32(Grid2.DataKeys[rowIndex][0]);
                Grid2.Rows.RemoveAt(rowIndex);
            }

            // 在操作之前进行权限检查
            if (!CheckPower("BOMDelete"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            // 从每个选中的行中获取ID（在Grid2中定义的DataKeyNames）
            List<int> ids = GetSelectedDataKeyIDs(Grid2);

            // 执行数据库操作
            //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
            //DB.SaveChanges();
            if (ids.Count > 0)
            {
                using (var appdb = new AppContext())
                {
                    appdb.bomdtl.Where(u => ids.Contains(u.SN)).Delete();
                }
            }

            //// 重新绑定表格
            BindGrid();
        }

         
    }
}