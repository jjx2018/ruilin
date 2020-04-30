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

namespace AppBoxPro.ruilin
{
    public partial class ProduceBom : PageBase
    {
        public string pstr = "";
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
                  IQueryable<Dept> q = DB.Depts ;
                    ddlDept.DataSource = q.ToList();
                    ddlDept.DataTextField = "Name";
                    ddlDept.DataValueField = "ID";
                    ddlDept.DataBind();
                    ddlDept.Items.Add("请选择", "");
                    ddlDept.SelectedIndex = ddlDept.Items.Count-1;
                 
                //IQueryable<User> yw = DB.Users;
                //txtRecOrderPerson.DataSource = yw.ToList();
                //txtRecOrderPerson.DataTextField = "ChineseName";
                //txtRecOrderPerson.DataValueField = "ChineseName";
                //txtRecOrderPerson.DataBind();
                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                // 新增数据初始值
                JObject defaultObj = new JObject();
                //defaultObj.Add("OrderNo","");
                //defaultObj.Add("ProNo", "");
                //defaultObj.Add("ProName", "");
                //defaultObj.Add("Seq", "");
                //defaultObj.Add("ItemNo", "");
                //defaultObj.Add("ItemName", "");
                //defaultObj.Add("Spec", "");
                //defaultObj.Add("Material", "");
                //defaultObj.Add("SurfaceDeal", "");
                //defaultObj.Add("UsingQuantity", "");
                //defaultObj.Add("Sclass", "");
                //defaultObj.Add("MakeMethod", "");
                //defaultObj.Add("WorkShop", "");
                //defaultObj.Add("SUBSN", "");
                //defaultObj.Add("ParentSN", "");
                //defaultObj.Add("ZuHe", "0");
                //defaultObj.Add("savetype", "new");
                //defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));

                // 在第一行新增一条数据
                //btnNew.OnClientClick = Grid2.GetAddNewRecordReference(defaultObj, AppendToEnd);

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
                string sql = "select ClassName from GoodsClass order by SortIndex ";
                SQLHelper.DbHelperSQL.SetConnectionString("");
                DataTable dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                ddlItemClass.DataTextField = "ClassName";
                ddlItemClass.DataValueField = "ClassName";
                ddlItemClass.DataSource = dt;
                ddlItemClass.DataBind();
                sql = "select  StoreName from StoreHouse order by SortIndex ";
                dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                ddlStoreHouse.DataSource = dt;
                ddlStoreHouse.DataValueField = "StoreName";
                ddlStoreHouse.DataTextField = "StoreName";
                ddlStoreHouse.DataBind();
                sql = "select distinct(ZongCheng) ZongCheng from rlitems where ZongCheng is not null ";
                dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                ddlZongCheng.DataSource = dt;
                ddlZongCheng.DataValueField = "ZongCheng";
                ddlZongCheng.DataTextField = "ZongCheng";
                ddlZongCheng.DataBind();
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
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid2.GetDeleteSelectedRowsReference(), String.Empty);
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
            using (var appdb = new AppContext())
            {
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

            }
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            ddlUser.Enabled = false;
            BindDDLCompany();
            BindGrid();
            BindGrid3();
        }

        private void BindUser()
        {
            int id = int.Parse(ddlDept.SelectedValue);
            
            var q = from a in DB.Users
                    from b in DB.Depts
                    where a.Dept.ID == b.ID && b.ID == id
                    select a;
            //IQueryable<User> q = DB.Users;
            //q.Where(u => u.Dept.ID == id);
            ddlUser.DataSource = q.ToList();
            ddlUser.DataTextField = "ChineseName";
            ddlUser.DataValueField = "Name";
            ddlUser.DataBind();
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
            BindGrid2();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid2();
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
                 
                
                var q = from a in appdb.orderdetail
                        join b in appdb.orderheader on a.FSN equals b.SN
                        where a.IsBom==1
                        select a;
                 

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.ItemName.Contains(searchText) || u.ItemNo.Contains(searchText) || u.ClinetNo.Contains(searchText) || u.OrderNo.Contains(searchText)); ;
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
                string OrderNo = Grid1.Rows[Grid1.SelectedRowIndex].Values[1].ToString();
                string ProNo = Grid1.Rows[Grid1.SelectedRowIndex].Values[3].ToString();// GetSelectedDataKeyID(Grid1).ToString();
                //double usingquantity = double.Parse(Grid1.Rows[Grid1.SelectedRowIndex].Values[3].ToString());
                //var instructionsql = from a in appdb.instruction
                //                     group a by new
                //                     {
                //                         a.ItemNo,
                //                         a.ItemName,
                //                         a.OrderNo,
                //                         a.ProNo,
                //                         a.ProName
                //                     } into g
                //                     select new
                //                     {
                //                         g.Key.ItemNo,
                //                         g.Key.ItemName,
                //                         g.Key.OrderNo,
                //                         g.Key.ProNo,
                //                         g.Key.ProName,
                //                         confirmquantity = (double)g.Sum(p => p.ConfirmQuantity)
                //                     };
                //Alert.Show(instructionsql.ToString());
                int q = int.Parse(Grid1.Rows[Grid1.SelectedRowIndex].Values[6].ToString());
                var qqq = from a in appdb.bomdtl
                          from d in appdb.bombase
                          where a.FSN == d.SN && d.ProNo == ProNo && d.OrderNo== OrderNo
                          select a;
               


               
                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    qqq = qqq.Where(u => u.ItemNo.Contains(searchText) || u.ItemName.Contains(searchText));
                }
              
                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = qqq.Count();// q.Count();

                // 排列和数据库分页
                //qqq = SortAndPage<MyBom>(qqq, Grid2);
                //qqq = SortAndPage(qqq, Grid2);
                JObject jObject = new JObject();
                jObject.Add("ItemNo", "<span style='color:red'>共计："+qqq.Count()+"条</span>");
                Grid2.SummaryData = jObject;
                Grid2.DataSource = qqq;// itemq.Take(2);// q;
                Grid2.DataBind();
                if(qqq.Count()>0)
                {
                    txtFSN.Text = Grid2.Rows[0].Values[17].ToString();
                }
                else
                {
                    txtFSN.Text = "";
                }
            }
        }
        private void BindGrid3()
        {
            try
            {
                using (var appdb = new AppContext())
                {
                   
                    var q = from a in appdb.allitems
                            select a;
                   
                    // 在职务名称中搜索
                    string searchText = txtKeyword.Text;// ddlItemNo.Text;
                  
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(t => t.ItemNo.Contains(searchText) || t.ItemName.Contains(searchText) || t.Material.Contains(searchText) || t.Sclass.Contains(searchText) || t.SurfaceDeal.Contains(searchText));
                    }
                    q = q.Take(100);
                    // 在查询添加之后，排序和分页之前获取总记录数
                    //Grid3.RecordCount = q.Count();

                    // 排列和分页
                    //q = SortAndPage<BomDetail>(q, Grid1);

                    Grid3.DataSource = q;
                    Grid3.DataBind();

                }
            }
            catch(Exception ee)
            {
                Alert.Show(ee.ToString());
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
            labColor.Text = Grid1.Rows[e.RowIndex].Values[11].ToString();
            labIsNew.Text = Grid1.Rows[e.RowIndex].Values[12].ToString();
            labIsPackingmaterials.Text = Grid1.Rows[e.RowIndex].Values[13].ToString();
            labCountryPackVer.Text = Grid1.Rows[e.RowIndex].Values[14].ToString();
            labIsChange.Text = Grid1.Rows[e.RowIndex].Values[15].ToString();
            labDemand1.Text = Grid1.Rows[e.RowIndex].Values[16].ToString();
            labDemand2.Text = Grid1.Rows[e.RowIndex].Values[17].ToString();
            txtQuantity.Text= Grid1.Rows[e.RowIndex].Values[6].ToString();
            btnCopy.Enabled = true;
            btnDelete.Enabled = true;
            btnNew.Enabled = true;
            btnPLSend.Enabled = true;
            btnZUHE.Enabled = true;
            btnSave.Enabled = true;
            btnExcel.Enabled = true;
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
            BindGrid2();
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
                if (Grid2.GetNewAddedList().Count != 0)
                {
                    string str = "";
                    List<Dictionary<string, object>> newAddedList = Grid2.GetNewAddedList();
                    for (int i = 0; i < newAddedList.Count; i++)
                    {
                        if (string.IsNullOrEmpty(newAddedList[i]["OrderUsingQuantity"].ToString()))
                        {
                            str += (i + 1).ToString() + ",";
                        }
                    }
                    str = str.TrimEnd(new char[] { ',' });
                    if (!string.IsNullOrEmpty(str))
                    {
                        Alert.Show("第" + str + "行用量为空，请录入再保存");
                        return;
                    }
                    //if (txtFSN.Text == "")
                    //{
                        
                    //}
                    sql = "select seq from BomDetail where sn=(select max(sn) from BomDetail where fsn=" + txtFSN.Text + ") ";
                    SQLHelper.DbHelperSQL.SetConnectionString("");
                    string seq = SQLHelper.DbHelperSQL.GetSingle(sql, 30);
                    if (!string.IsNullOrEmpty(seq))
                    {
                        if (seq.IndexOf(".") != -1)
                        {
                            seq = seq.Substring(0, seq.IndexOf("."));
                            seq = (int.Parse(seq) + 1).ToString();
                        }
                        else
                        {
                            seq = (int.Parse(seq) + 1).ToString();
                        }
                    }
                    else
                    {
                        seq = "1";
                    }


                    for (int i = 0; i < newAddedList.Count; i++)
                    {


                        if (newAddedList[i]["savetype"].ToString() == "new" || newAddedList[i]["savetype"].ToString() == "copy")
                        {
                            sql = "INSERT INTO BomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,OrderUsingQuantity,Sclass,Inputer,InputeDate,ZuHe,WorkShop,seq,ProUsingQuantity,ZongCheng,BaseNum,MainFrom,StoreHouse) values(" + txtFSN.Text + ",(select sn from rlitems where itemno='" + newAddedList[i]["ItemNo"].ToString() + "'),'" + newAddedList[i]["ItemNo"].ToString() + "','" + newAddedList[i]["ItemName"].ToString() + "','" + newAddedList[i]["Spec"].ToString() + "','" + newAddedList[i]["Material"].ToString() + "','" + newAddedList[i]["SurfaceDeal"].ToString() + "'," + newAddedList[i]["OrderUsingQuantity"].ToString() + ",'" + newAddedList[i]["Sclass"].ToString() + "','" + User.Identity.Name + "',getdate()," + newAddedList[i]["ZuHe"].ToString() + ",'" + newAddedList[i]["WorkShop"].ToString() + "','" + seq + "'," + newAddedList[i]["ProUsingQuantity"].ToString() + ",'" + newAddedList[i]["ZongCheng"].ToString() + "','" + newAddedList[i]["BaseNum"].ToString() + "','" + newAddedList[i]["MainFrom"].ToString() + "','" + newAddedList[i]["StoreHouse"].ToString() + "')";
                        }
                        else
                        {
                            sql = "INSERT INTO BomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,OrderUsingQuantity,Sclass,Inputer,InputeDate,ZuHe,WorkShop,seq,ParentSN,ProUsingQuantity,ZongCheng,BaseNum,StoreHouse,MainFrom) values(" + txtFSN.Text + ",(select sn from rlitems where itemno='" + newAddedList[i]["ItemNo"].ToString() + "'),'" + newAddedList[i]["ItemNo"].ToString() + "','" + newAddedList[i]["ItemName"].ToString() + "','" + newAddedList[i]["Spec"].ToString() + "','" + newAddedList[i]["Material"].ToString() + "','" + newAddedList[i]["SurfaceDeal"].ToString() + "'," + newAddedList[i]["OrderUsingQuantity"].ToString() + ",'" + newAddedList[i]["Sclass"].ToString() + "','" + User.Identity.Name + "',getdate()," + newAddedList[i]["ZuHe"].ToString() + ",'" + newAddedList[i]["WorkShop"].ToString() + "','" + seq + "'," + newAddedList[i]["ParentSN"].ToString() + "," + newAddedList[i]["ProUsingQuantity"].ToString() + ",'" + newAddedList[i]["ZongCheng"].ToString() + "','" + newAddedList[i]["BaseNum"].ToString() + "','" + newAddedList[i]["StoreHouse"].ToString() + "','" + newAddedList[i]["MainFrom"].ToString() + "')";
                        }
                        seq = (int.Parse(seq) + 1).ToString();
                        log.Info("producebom:::" + sql);
                        al.Add(sql);
                        sql = "UPDATE RLItems  SET SupplierId=" + newAddedList[i]["SupplierId"].ToString() + " where sn=" + newAddedList[i]["AllitemSN"].ToString();  //AllitemSN
                        al.Add(sql);
                        log.Info("producebom:::" + sql);
                    }
                    sql = "update BomDetail set subsn=sn where subsn is null and FSN=" + txtFSN.Text;
                    al.Add(sql);
                    log.Info("producebom:::" + sql);
                }

                // 修改的现有数据
                if (Grid2.GetModifiedData().Count != 0)
                {
                    Dictionary<int, Dictionary<string, object>> modifiedDict = Grid2.GetModifiedDict();
                    foreach (int rowIndex in modifiedDict.Keys)
                    {
                        if (!modifiedDict[rowIndex].ContainsKey("SupplierId"))
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
                            al.Add(sql);
                            log.Info("producebom:::" + sql);
                        }
                        else
                        {
                            sql = "UPDATE RLItems  SET SupplierId=" + modifiedDict[rowIndex]["SupplierId"].ToString() + " where sn=" + Grid2.Rows[rowIndex].Values[23].ToString();  //AllitemSN
                            al.Add(sql);
                            log.Info("producebom:::" + sql);
                        }
                         
                    }
                }
                

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
                Alert.Show(ee.ToString());
            }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                int[] selections = Grid2.SelectedRowIndexArray;
                if (selections.Length == 0)
                {
                    Alert.Show("请选择要复制的物料或者复制的物料尚未保存");
                    return;
                }

                // 新增数据初始值
                JObject defaultObj = new JObject();
                string deleteScript = GetDeleteScript();


                foreach (int rowIndex in selections)
                {
                    //defaultObj.Add("OrderNo", Grid2.Rows[rowIndex].Values[1].ToString());
                    //defaultObj.Add("ProNo", Grid2.Rows[rowIndex].Values[2].ToString());
                    //defaultObj.Add("ProName", Grid2.Rows[rowIndex].Values[3].ToString());
                    defaultObj.Add("Seq", "");
                    defaultObj.Add("ItemNo", Grid2.Rows[rowIndex].Values[2].ToString());
                    defaultObj.Add("ItemName", Grid2.Rows[rowIndex].Values[3].ToString());
                    defaultObj.Add("Spec", Grid2.Rows[rowIndex].Values[4].ToString());
                    defaultObj.Add("Material", Grid2.Rows[rowIndex].Values[5].ToString());
                    defaultObj.Add("SurfaceDeal", Grid2.Rows[rowIndex].Values[6].ToString());
                    defaultObj.Add("ProUsingQuantity", Grid2.Rows[rowIndex].Values[7].ToString());
                    defaultObj.Add("OrderUsingQuantity", Grid2.Rows[rowIndex].Values[8].ToString());
                    defaultObj.Add("ZongCheng", Grid2.Rows[rowIndex].Values[9].ToString());
                    defaultObj.Add("BaseNum", Grid2.Rows[rowIndex].Values[10].ToString());
                    defaultObj.Add("WorkShop", Grid2.Rows[rowIndex].Values[11].ToString());
                    defaultObj.Add("MainFrom", Grid2.Rows[rowIndex].Values[12].ToString());
                    defaultObj.Add("Sclass", Grid2.Rows[rowIndex].Values[13].ToString());
                    defaultObj.Add("StoreHouse", Grid2.Rows[rowIndex].Values[14].ToString());
                    defaultObj.Add("SupplierId", Grid1.Rows[rowIndex].Values[15].ToString());
                    //FSN   18
                    defaultObj.Add("ZuHe", "0");  //19
                    defaultObj.Add("ParentSN", ""); //20
                    defaultObj.Add("SUBSN", "");  //21
                    defaultObj.Add("savetype", "copy"); //22
                    //AllitemSN 23
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

        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Grid2.RecordCount = 0;
                Grid2.DataSource = null;
                Grid2.DataBind();
                // 新增数据初始值
                JObject defaultObj = new JObject();
                string deleteScript = GetDeleteScript();
                //defaultObj.Add("OrderNo", Grid1.Rows[Grid1.SelectedRowIndex].Values[1].ToString());
                //defaultObj.Add("ProNo", Grid1.Rows[Grid1.SelectedRowIndex].Values[3].ToString());
                //defaultObj.Add("ProName", Grid1.Rows[Grid1.SelectedRowIndex].Values[4].ToString());
                defaultObj.Add("Seq", "");
                defaultObj.Add("ItemNo", "");
                defaultObj.Add("ItemName", "");
                defaultObj.Add("Spec", "");
                defaultObj.Add("Material", "");
                defaultObj.Add("SurfaceDeal", "");
                defaultObj.Add("ProUsingQuantity", "");
                defaultObj.Add("OrderUsingQuantity", "");
                defaultObj.Add("ZongCheng", "");
                defaultObj.Add("BaseNum", "");
                defaultObj.Add("WorkShop", "");
                defaultObj.Add("MainFrom", "");
                defaultObj.Add("Sclass", "");
                defaultObj.Add("StoreHouse", "");
                defaultObj.Add("SupplierId", "");
                defaultObj.Add("SUBSN", "");
                defaultObj.Add("ParentSN", "");
                defaultObj.Add("ZuHe", "0");
                defaultObj.Add("savetype", "new");

                defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));
                PageContext.RegisterStartupScript(Grid2.GetAddNewRecordReference(defaultObj, AppendToEnd));
            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
        }

        protected void btnZUHE_Click(object sender, EventArgs e)
        {
            try
            {

                
                if (Grid2.SelectedRowIndex == -1)
                {
                    Alert.Show("未选择一个物料或者选择的物料尚未保存");
                    return;
                }
                string sql = "select SUBSN from BomDetail where sn=" + Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString();
                SQLHelper.DbHelperSQL.SetConnectionString("");
                string SUBSN = SQLHelper.DbHelperSQL.GetSingle(sql, 30);
              
                if (string.IsNullOrEmpty(SUBSN) || SUBSN != Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString())
                {
                    sql = "update BomDetail set SUBSN=sn,ZuHe=1 where sn=" + Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString();
                    SQLHelper.DbHelperSQL.ExecuteSql(sql, 30);
                }
                // 新增数据初始值
                JObject defaultObj = new JObject();
                string deleteScript = GetDeleteScript();
               
                defaultObj.Add("Seq", "");
                defaultObj.Add("ItemNo", "");
                defaultObj.Add("ItemName", "");
                defaultObj.Add("Spec", "");
                defaultObj.Add("Material", "");
                defaultObj.Add("SurfaceDeal", "");
                defaultObj.Add("ProUsingQuantity", "");
                defaultObj.Add("OrderUsingQuantity", "");
                defaultObj.Add("ZongCheng", "");
                defaultObj.Add("BaseNum", "");
                defaultObj.Add("WorkShop", "");
                defaultObj.Add("MainFrom", "");
                defaultObj.Add("Sclass", "");
                defaultObj.Add("StoreHouse", "");
                defaultObj.Add("SupplierId", "");
                defaultObj.Add("ParentSN", Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString());
                defaultObj.Add("ZuHe", "1");
                defaultObj.Add("SUBSN", "");
                defaultObj.Add("savetype", "zuhe");
                defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));
                PageContext.RegisterStartupScript(Grid2.GetAddNewRecordReference(defaultObj, AppendToEnd));
            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
        }

        protected void btnPLSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid2.GetModifiedData().Count != 0 || Grid2.GetNewAddedList().Count != 0)
                {
                    Alert.Show("表格还有未保存的数据，请先保存！");
                    return;
                }
                if (Grid2.SelectedRowIndexArray.Length == 0)
                {
                    Alert.Show("请选择物料");
                    return;
                }
                StringBuilder sql = new StringBuilder();
                ArrayList al = new ArrayList();
                string s = "",orderno="",prono="",pname="";
                SQLHelper.DbHelperSQL.SetConnectionString("");
                foreach (int i in Grid2.SelectedRowIndexArray)
                {
                    orderno = Grid1.Rows[Grid1.SelectedRowIndex].Values[1].ToString();
                    prono = Grid1.Rows[Grid1.SelectedRowIndex].Values[3].ToString();
                    pname = Grid1.Rows[Grid1.SelectedRowIndex].Values[4].ToString();
                    s = "select count(*) from Instruction where OrderNo='" + orderno+ "' and ProNo='" + prono + "' and itemno='" + Grid2.Rows[i].Values[2].ToString() + "' and IsConfirm=0";
                    if (int.Parse(SQLHelper.DbHelperSQL.GetSingle(s, 30)) > 0)
                    {
                        continue;
                    }
                    s = "select (case  when sum(ConfirmQuantity) is null then 0 else sum(ConfirmQuantity) end) from instruction where orderno='" + orderno + "' and prono='" + prono + "' and itemno='" + Grid2.Rows[i].Values[2].ToString() + "'";
                    sql.Clear();
                    sql.Append("insert into Instruction(OrderNo,ProNo,ProName,ItemNo,ItemName,Spec,Material,SurfaceDeal,UsingQuantity,Sclass,MainFrom,Inputer,InputeDate,IsConfirm,IsPlan,ReceiveDept,Receiver,BarCode,BomSN)");
                    sql.Append(" values(");
                    sql.Append("'" + orderno + "',");
                    sql.Append("'" + prono + "',");
                    sql.Append("'" + pname + "',");
                    sql.Append("'" + Grid2.Rows[i].Values[2].ToString() + "',");//ItemNo
                    sql.Append("'" + Grid2.Rows[i].Values[3].ToString() + "',");//ItemName
                    sql.Append("'" + Grid2.Rows[i].Values[4].ToString() + "',");//Spec
                    sql.Append("'" + Grid2.Rows[i].Values[5].ToString() + "',");//Material
                    sql.Append("'" + Grid2.Rows[i].Values[6].ToString() + "',");//SurfaceDeal
                    sql.Append("" + Grid2.Rows[i].Values[8].ToString() + "-(" + s + "),");//UsingQuantity
                    sql.Append("'" + Grid2.Rows[i].Values[9].ToString() + "',");//Sclass
                    sql.Append("'" + Grid2.Rows[i].Values[12].ToString() + "',");//MainFrom


                    sql.Append("'" + User.Identity.Name + "',");
                    sql.Append("getdate(),");
                    sql.Append("0,");
                    sql.Append("0,");

                    sql.Append("'" + ddlDept.SelectedText + "',");
                    sql.Append("'" + ddlUser.SelectedValue + "',");
                    sql.Append("'" + DateTime.Now.ToString("yyyyMMddHHmmsss") + i.ToString() + "',");
                    sql.Append(Grid2.DataKeys[i][0]);
                    sql.Append(")");
                    al.Add(sql.ToString());
                    //log.Info(sql.ToString());
                    FileOper.writeLog(sql.ToString());
                }

                if (SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                {
                    Alert.Show("发送成功");
                    BindGrid2();
                }
                else
                {
                    Alert.Show("发送失败");
                }
            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlUser.Enabled = true;
            BindUser();
        }

        protected void ddlItemNo_TextChanged(object sender, EventArgs e)
        {
            //Grid3.Hidden = false;
            BindGrid3();
        }

        protected void TextBox6_TextChanged(object sender, EventArgs e)
        {
            //Grid3.Hidden = false;
            //BindGrid3();
        }

        protected void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            BindGrid3();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Grid2.PageSize = 1000000;
            BindGrid();
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
        //protected void Grid2_AfterEdit(object sender, GridAfterEditEventArgs e)
        //{
        //    // 当前选中的单元格
        //    string[] selectedCell = Grid1.SelectedCell;

        //    Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
        //    string sql = "";
        //    foreach (int rowIndex in modifiedDict.Keys)
        //    {
        //        sql = "update BomDetail set ";
        //        for (int i = 0; i < Grid2.Columns.Count; i++)
        //        {
        //            if (modifiedDict[rowIndex].ContainsKey(Grid2.Columns[i].ColumnID))
        //            {
        //                sql += Grid2.Columns[i].ColumnID + "='" + modifiedDict[rowIndex][Grid2.Columns[i].ColumnID].ToString() + "',";
        //            }

        //        }
        //        sql = sql.TrimEnd(new char[] { ',' });
        //        sql += " where sn=" + Grid2.DataKeys[rowIndex][0];

        //    }

        //    // 数据绑定时，会清空选中的行和选中的单元格
        //    //BindGrid();

        //    // 重新选中之前的单元格
        //    //PageContext.RegisterStartupScript(String.Format("F('{0}').selectCell('{1}','{2}');", Grid1.ClientID, selectedCell[0], selectedCell[1]));

        //}
    }
}