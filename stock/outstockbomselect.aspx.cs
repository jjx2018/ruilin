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

namespace AppBoxPro.stock
{
    public partial class outstockbomselect : PageBase
    {
        public string pstr = "";
        log4net.ILog log = log4net.LogManager.GetLogger("magPlan.aspx");
        private bool AppendToEnd = false;
        static int seq = 0;
        static Hashtable htClickColsName = new Hashtable();
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                IQueryable<Dept> q = DB.Depts;
                ddlDept.DataSource = q.ToList();
                ddlDept.DataTextField = "Name";
                ddlDept.DataValueField = "ID";
                ddlDept.DataBind();
                ddlDept.Items.Add("请选择", "");
                ddlDept.SelectedIndex = ddlDept.Items.Count - 1;

                //IQueryable<User> yw = DB.Users;
                //txtRecOrderPerson.DataSource = yw.ToList();
                //txtRecOrderPerson.DataTextField = "ChineseName";
                //txtRecOrderPerson.DataValueField = "ChineseName";
                //txtRecOrderPerson.DataBind();

                CheckPowerWithButton("BOMDelete", btnDelete);
                ResolveDeleteButtonForGrid(btnDelete, Grid2);

                //btnClear.OnClientClick = SF1.GetResetReference();
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
                string[] args = requestArg.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                //log.Info(requestArg);
                if (requestArg.Equals("RefreshGrid2"))
                {
                    BindGrid2();
                }
                else if (args != null && args.Length > 0 && args[0].Equals("updaterowcss"))
                {
                    // Grid2.Rows[int.Parse(ridx)].RowCssClass = "color1";
                    CommFunction.updateRowCss(args[1], Grid2, "color1");
                }
            }

        }
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid2.GetDeleteSelectedRowsReference(), String.Empty);
        }

        private void LoadData()
        {

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
                int SN = GetQueryIntValue("sn");
                pstr = pstr.TrimEnd(new char[] { ',' });
                pstr += "})";
                var q = from a in appdb.orderdetail
                        where a.SN == SN
                        select a;
                OrderDetail item = q.SingleOrDefault();
                labOrderno.Text = item.OrderNo;
                labClientNo.Text = item.ClinetNo;
                labProNo.Text = item.ItemNo;
                labProName.Text = item.ItemName;
                labQuantity.Text = item.Quantity.ToString();
                labUnit.Text = item.Unit;
                labColor.Text = item.Color;
                labIsNew.Text = item.IsNew;
                labIsPackingmaterials.Text = item.IsPackingmaterials;
                labCountryPackVer.Text = item.CountryPackVer;
                labIsChange.Text = item.IsChange;
                labDemand1.Text = item.Demand1;
                labDemand2.Text = item.Demand2;
                txtQuantity.Text = item.Quantity.ToString();
            }

            BindGrid2();
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



        private void BindGrid2()
        {

            using (var appdb = new AppContext())
            {
                string OrderNo = GetQueryValue("od");
                string ProNo = GetQueryValue("ProNo");
                int OdtSN = GetQueryIntValue("sn");

                bool cbk = cbisPick.Checked;

                var qqq = from a in appdb.bomdtl
                          join b in appdb.bombase on a.FSN equals b.SN into basejoin
                          from k in basejoin.DefaultIfEmpty()
                          join c in appdb.allitems on a.AllitemSN equals c.SN into itemjoin
                          from d in itemjoin.DefaultIfEmpty()
                          join e in appdb.instruction.Where(u => u.IsConfirm == 0) on a.SN equals e.BomSN into instructionjoin
                          from f in instructionjoin.DefaultIfEmpty()
                          where k.OdtSN == OdtSN

                          select new { a.IsPick, a.SN, a.FSN, a.Seq, a.ItemNo, a.ItemName, a.Spec, a.Material, a.SurfaceDeal, a.OrderUsingQuantity, a.Sclass, a.MainFrom, a.WorkShop, a.ZuHe, a.SUBSN, a.ParentSN, a.ProUsingQuantity, SupplierId = d.SupplierId == null ? "" : d.SupplierId.ToString(), a.ZongCheng, a.BaseNum, a.StoreHouse, a.AllitemSN, a.IsValid, ISN = f.SN.ToString() };

                //只显示为生成领料单的
                if (cbk)
                {
                    qqq = qqq.Where(u => u.IsPick == null || u.IsPick == 0);
                }

                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    qqq = qqq.Where(u => u.ItemNo.Contains(searchText) || u.ItemName.Contains(searchText) || u.Sclass.Contains(searchText) || u.MainFrom.Contains(searchText) || u.WorkShop.Contains(searchText) || u.StoreHouse.Contains(searchText));
                }
                foreach (DictionaryEntry de in htClickColsName)
                {
                    switch (de.Key.ToString())
                    {
                        case "ItemNo":
                            qqq = qqq.Where(u => u.ItemNo == de.Value.ToString());
                            break;
                        case "ItemName":
                            qqq = qqq.Where(u => u.ItemName == de.Value.ToString());
                            break;
                        case "Spec":
                            qqq = qqq.Where(u => u.Spec == de.Value.ToString());
                            break;
                        case "Material":
                            qqq = qqq.Where(u => u.Material == de.Value.ToString());
                            break;
                        case "SurfaceDeal":
                            qqq = qqq.Where(u => u.SurfaceDeal == de.Value.ToString());
                            break;
                        case "ProUsingQuantity":
                            qqq = qqq.Where(u => u.ProUsingQuantity.ToString() == de.Value.ToString());
                            break;
                        case "ZongCheng":
                            qqq = qqq.Where(u => u.ZongCheng.ToString() == de.Value.ToString());
                            break;
                        case "BaseNum":
                            qqq = qqq.Where(u => u.BaseNum.ToString() == de.Value.ToString());
                            break;
                        case "Sclass":
                            qqq = qqq.Where(u => u.Sclass.ToString() == de.Value.ToString());
                            break;
                        case "MainFrom":
                            qqq = qqq.Where(u => u.MainFrom.ToString() == de.Value.ToString());
                            break;
                        case "WorkShop":
                            qqq = qqq.Where(u => u.WorkShop.ToString() == de.Value.ToString());
                            break;
                        case "StoreHouse":
                            qqq = qqq.Where(u => u.StoreHouse.ToString() == de.Value.ToString());
                            break;
                    }
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = qqq.Count();// q.Count();

                // 排列和数据库分页
                //qqq = SortAndPage<MyBom>(qqq, Grid2);
                //qqq = SortAndPage(qqq, Grid2);
                JObject jObject = new JObject();
                jObject.Add("ItemNo", "<span style='color:red'>共计：" + qqq.Count() + "条</span>");
                jObject.Add("Material", "<span style='color:#000;background-color:#ccc;height:25px;line-height:25px;font-size:18px;padding:5px;'>灰色为无效</span>");
                jObject.Add("SurfaceDeal", "<span style='color:#000;background-color:#1AA348;height:25px;line-height:25px;font-size:18px;padding:5px;'>绿色为未确认</span>");
                Grid2.SummaryData = jObject;
                Grid2.DataSource = qqq;// itemq.Take(2);// q;
                Grid2.DataBind();
                if (qqq.Count() > 0)
                {
                    txtFSN.Text = Grid2.Rows[0].Values[18].ToString();
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
            catch (Exception ee)
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
            CheckPowerWithLinkButtonField("BOMDelete", Grid2, "deleteField");
            //CheckPowerWithWindowField("InstockChangePassword", Grid1, "changePasswordField");

        }
        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("InstockEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("BOMDelete", Grid2, "deleteField");
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
        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            Grid2.SortDirection = e.SortDirection;
            Grid2.SortField = e.SortField;
            BindGrid2();
        }

        protected void Grid2_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid2.PageIndex = e.NewPageIndex;
            BindGrid2();
        }

        protected void btnDeleteSelected2_Click(object sender, EventArgs e)
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
                appdb.orderdetail.Where(u => ids.Contains(u.SN)).Delete();
            }
            // 重新绑定表格
            BindGrid2();
        }



        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            int ID = GetSelectedDataKeyID(Grid2);

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
                    appdb.orderdetail.Where(u => u.FSN == ID).Delete();
                    BindGrid2();
                }
            }
            else if (e.CommandName == "StoreConfirm")
            {
                int odtsn = GetQueryIntValue("sn");
                using (var appdb = new AppContext())
                {
                    string itemno = Grid2.Rows[e.RowIndex].Values[2].ToString();
                    string orderno = GetQueryValue("od");
                    Instruction item = appdb.instruction
                    .Where(u => u.ItemNo == itemno && u.OrderNo == orderno && u.OdtSN == odtsn && u.IsConfirm == 0).FirstOrDefault();
                    if (item != null)
                    {
                        Alert.Show("该物料存在未确认的备货确认单，请先确认！");
                        return;
                    }
                }
                //Grid2.Rows[e.RowIndex].RowCssClass = "color1";
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/StoreConfirm/SendInstruction.aspx?id=" + Grid2.DataKeys[e.RowIndex][0].ToString() + "&fsn=" + Grid2.Rows[e.RowIndex].Values[18].ToString() + "&k=1&odtsn=" + GetQueryValue("sn") + "&ridx=" + e.RowIndex, "备货确认单", Unit.Parse("900"), Unit.Parse("800")));
            }
        }
        protected void Grid2_OnRowClick(object sender, FineUIPro.GridRowClickEventArgs e)
        {

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



        protected void ddlGridPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid2.PageSize = Convert.ToInt32(ddlGridPageSize2.SelectedValue);

            BindGrid2();
        }


        #endregion

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
                seq = 0;
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
                    string sqlval = "", sqlitem = "";
                    for (int i = 0; i < newAddedList.Count; i++)
                    {
                        #region add BomDetail
                        sql = "insert into BomDetail(allitemsn,inputedate,inputer,IsValid,FSN,";
                        sqlval = " values((select sn from rlitems where itemno='" + newAddedList[i]["ItemNo"].ToString() + "'),getdate(),'" + User.Identity.Name + "',0," + txtFSN.Text + ",";
                        foreach (string key in newAddedList[i].Keys)
                        {
                            if (key.ToLower() == "seq")
                            {
                                sql += key + ",";
                                sqlval += "'" + seq + "',";
                            }
                            else if (key.ToLower() == "supplierid")
                            {
                                if (newAddedList[i]["SupplierId"].ToString() != "")
                                {
                                    sql += key + ",";
                                    sqlval += "'" + newAddedList[i][key].ToString() + "',";
                                    sqlitem = "UPDATE RLItems  SET SupplierId=" + newAddedList[i]["SupplierId"].ToString() + " where ItemNo='" + newAddedList[i]["ItemNo"].ToString() + "'";  //AllitemSN
                                    al.Add(sqlitem);
                                }
                            }
                            else if (key.ToLower() == "savetype")
                            {
                                continue;
                            }
                            else if (key.ToLower() == "allitemsn")
                            {
                                continue;
                            }
                            else if (key == "ParentSN")
                            {
                                if (newAddedList[i][key].ToString() != "")
                                {
                                    sql += key + ",";
                                    sqlval += "'" + newAddedList[i][key].ToString() + "',";
                                }
                            }
                            else if (key.ToUpper() == "SUBSN")
                            {
                                if (newAddedList[i][key].ToString() != "")
                                {
                                    sql += key + ",";
                                    sqlval += "'" + newAddedList[i][key].ToString() + "',";
                                }
                            }
                            else
                            {
                                sql += key + ",";
                                sqlval += "'" + newAddedList[i][key].ToString() + "',";
                            }
                        }
                        sql = sql.TrimEnd(new char[] { ',' }) + ")";
                        sqlval = sqlval.TrimEnd(new char[] { ',' }) + ")";
                        sql = sql + sqlval;
                        al.Add(sql);
                        log.Info("sql item add:::" + sql);
                        #endregion
                        seq = (int.Parse(seq) + 1).ToString();


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
                // 在操作之前进行权限检查
                if (!CheckPower("BOMAdd"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
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
                    defaultObj.Add("Seq", getseq());
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
                    defaultObj.Add("SupplierId", Grid2.Rows[rowIndex].Values[15].ToString());
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
        string getseq()
        {
            string sql = "select seq from BomDetail where sn=(select max(sn) from BomDetail where fsn=" + txtFSN.Text + ") ";
            SQLHelper.DbHelperSQL.SetConnectionString("");
            string seqstr = SQLHelper.DbHelperSQL.GetSingle(sql, 30);
            if (!string.IsNullOrEmpty(seqstr))
            {
                if (seqstr.IndexOf(".") != -1)
                {
                    seqstr = seqstr.Substring(0, seqstr.IndexOf("."));
                    seqstr = (int.Parse(seqstr) + 1).ToString();
                }
                else
                {
                    seqstr = (int.Parse(seqstr) + 1).ToString();
                }
            }
            else
            {
                seqstr = "1";
            }
            return seqstr;
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                // 在操作之前进行权限检查
                if (!CheckPower("BOMAdd"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                if (Grid2.GetNewAddedList().Count == 0)
                {
                    Grid2.RecordCount = 0;
                    Grid2.DataSource = null;
                    Grid2.DataBind();
                }
                // 新增数据初始值
                JObject defaultObj = new JObject();
                string deleteScript = GetDeleteScript();
                //defaultObj.Add("OrderNo", Grid1.Rows[Grid1.SelectedRowIndex].Values[1].ToString());
                //defaultObj.Add("ProNo", Grid1.Rows[Grid1.SelectedRowIndex].Values[3].ToString());
                //defaultObj.Add("ProName", Grid1.Rows[Grid1.SelectedRowIndex].Values[4].ToString());
                defaultObj.Add("Seq", getseq());
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
                // 在操作之前进行权限检查
                if (!CheckPower("BOMAdd"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                if (Grid2.SelectedRowIndex == -1)
                {
                    Alert.Show("未选择一个物料或者选择的物料尚未保存");
                    return;
                }

                seq++;

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

                defaultObj.Add("Seq", Grid2.Rows[Grid2.SelectedRowIndex].Values[1].ToString() + "." + seq.ToString());
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
                // 在操作之前进行权限检查
                if (!CheckPower("BOMPLSend"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
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
                string OdtSN = GetQueryValue("sn");
                string orderno = "", prono = "", pname = "";
                orderno = GetQueryValue("od");
                prono = GetQueryValue("ProNo");
                pname = GetQueryValue("ItemName");
                Alert.Show(CommFunction.PLsendInstruction(orderno, prono, pname, ddlDept.SelectedText, ddlUser.SelectedValue, OdtSN, User.Identity.Name, Grid2));

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

        protected void Grid2_RowDataBound(object sender, GridRowEventArgs e)
        {
            //if (!string.IsNullOrEmpty(Grid2.Rows[e.RowIndex].Values[26].ToString()))
            //{
            //    e.RowCssClass = "color1";
            //}
            //if (Grid2.Rows[e.RowIndex].Values[25].ToString() == "1")
            //{
            //    e.RowSelectable = false;
            //    e.RowCssClass = "color2";
            //    foreach (GridColumn column in Grid2.Columns)
            //    {
            //        e.CellCssClasses[column.ColumnIndex] = "f-grid-cell-uneditable";
            //    }
            //}
            if (User.Identity.Name != "admin")
            {
                if (!CheckPower("BOMEdit"))
                {
                    e.CellCssClasses[2] = "f-grid-cell-uneditable";
                    e.CellCssClasses[7] = "f-grid-cell-uneditable";
                    e.CellCssClasses[8] = "f-grid-cell-uneditable";
                }
            }
        }

        protected void Unnamed_Load(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string[] s = txtClickColsName.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (s == null || s.Length == 0)
            {
                return;
            }
            htClickColsName.Remove(s[0]);
            BindGrid2();
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

        protected void Grid2_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            string[] s = Grid2.SelectedCell;
            for (int i = 0; i < Grid2.Columns.Count; i++)
            {
                if (s[1] == Grid2.Columns[i].ColumnID && !htClickColsName.ContainsKey(s[1]))
                {
                    htClickColsName.Add(s[1], Grid2.Rows[e.RowIndex].Values[i].ToString());
                    break;
                }
            }
            BindGrid2();
            updatecol();
        }

        protected void cbisPick_CheckedChanged(object sender, CheckedEventArgs e)
        {
            BindGrid2();
        }

        protected void btnOutstock_Click(object sender, EventArgs e)
        {
            if (Grid2.SelectedRowIndex < 0)
            {
                Alert.Show("没有选中任何行");
                return;
            }
            string fname = printOrderForHtmlBySQL("PrinterSet", "ProduceOrderHead", "ProduceOrderContent", "ProduceOrderFoot");

            string str = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string s = @" parent.addExampleTab({
                id: " + str + @"+'_tab',
                iframeUrl: 'pdf/" + fname + @"',
                title:'打印领料单',
                iconFont: 'sign-in',
                refreshWhenExist: true});";
            PageContext.RegisterStartupScript(s);

            //BindGrid2();
        }

        string printOrderForHtmlBySQL(string printsetfile, string htmlheadfile, string headcontentfile, string htmlfootfile)
        {
            string fname = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".html";
            #region //获取模板文件内容
            StringBuilder printsettext = new StringBuilder(FileOper.getFileContent(HttpContext.Current.Server.MapPath("~/Model/" + printsetfile + ".html")));
            //int pagecount = Grid2.SelectedRowIndexArray.Length;
            //int labcount = int.Parse(numLabCount2.Text);
            printsettext.Replace("$pagecount", "");

            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/pdf/" + fname), FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(printsettext.ToString());
            //获取模板内容
            StringBuilder htmlheadtext = new StringBuilder(FileOper.getFileContent(HttpContext.Current.Server.MapPath("~/Model/" + htmlheadfile + ".html")));
            StringBuilder headcontenttext = new StringBuilder(FileOper.getFileContent(HttpContext.Current.Server.MapPath("~/Model/" + headcontentfile + ".html")));
            StringBuilder htmlfoottext = new StringBuilder(FileOper.getFileContent(HttpContext.Current.Server.MapPath("~/Model/" + htmlfootfile + ".html")));

            #endregion

            try
            {
               

                #region 将模板内容循环添加PDF中
                SQLHelper.DbHelperSQL.SetConnectionString("");
                object[] obj = new object[] { "LL" };
                DataTable dtll = SQLHelper.DbHelperSQL.ExecuteProc_ReturnDataTable("getseq", obj, 20);

                string orderno ="LL"+ dtll.Rows[0][0].ToString();

                //生成领料出仓表头
                StockHeader stockHeader = new StockHeader();
                stockHeader.jingbanren = GetChineseName();
                stockHeader.title = "领料单";
                stockHeader.StockHeaderProsn = orderno;
                stockHeader.optdate = DateTime.Now;

                MYDB.StockHeaders.Add(stockHeader);


                int pageindex = 1;
                //替换头部
                htmlheadtext.Replace("$ProOrderNo", orderno);
                htmlheadtext.Replace("$ProNo", "");
                htmlheadtext.Replace("$InputeDate", DateTime.Now.ToString("yyyy-MM-dd"));
                htmlheadtext.Replace("$Quantity", labQuantity.Text);
                htmlheadtext.Replace("$Inputer", GetChineseName());
                htmlheadtext.Replace("$SaleOrderNo", labOrderno.Text);
                htmlheadtext.Replace("$Remark", "");
                sw.WriteLine(htmlheadtext);
                StringBuilder str = new StringBuilder();
                string itemrows = "";

                str.Clear();

                if (pageindex != 1)
                {
                    str.Append("<div class='page'><br></div>");
                }
                str.Append(headcontenttext.ToString());

                int[] selections = Grid2.SelectedRowIndexArray;

                // 添加文档内容 
                foreach (int i in selections)
                {
                    itemrows += "<tr><td class='b_b_r'>" + Grid2.Rows[i].Values[2].ToString() + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[3].ToString() + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[4].ToString() + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[15].ToString() + "</td><td class='b_b_r'></td><td class='b_b_r'></td><td class='b_b_r td_c'>" + Grid2.Rows[i].Values[8].ToString() + "</td><td class='b_b'>" + Grid2.Rows[i].Values[9].ToString() + "</td></tr>";


                    ProductStockList productStockList = new ProductStockList();
                    productStockList.Mark = "02";
                    productStockList.ProductProperties = ProductProperties.生产入库;
                    productStockList.ItemNo= Grid2.Rows[i].Values[2].ToString();
                    productStockList.ItemName = Grid2.Rows[i].Values[3].ToString();
                    productStockList.Spec = Grid2.Rows[i].Values[4].ToString();
                    //实际领料数量
                    productStockList.Quantity = decimal.Parse(Grid2.Rows[i].Values[9].ToString());

                    //库位
                    productStockList.Space = Grid2.Rows[i].Values[15].ToString();
                    productStockList.StockHeaderProsn = orderno;
                    productStockList.PDate = DateTime.Now;
                    MYDB.ProductStockLists.Add(productStockList);
                }

                //更新状态，已经领料
                foreach(int SN in GetSelectedDataKeyIDs(Grid2))
                {
                    MYDB.bomdtl.Where(u => u.SN == SN).Update(u => new BomDetail
                    {
                        IsPick=1
                    }) ;
                }

                MYDB.SaveChanges();
                str.Replace("$items", itemrows);
                sw.WriteLine(str);
                pageindex++;

                //}
                #endregion
                sw.WriteLine(htmlfoottext.ToString());
                sw.WriteLine(" </div></body></html>");
            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
            return fname;
        }
    }
}