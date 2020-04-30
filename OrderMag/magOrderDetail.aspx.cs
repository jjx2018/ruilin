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

namespace AppBoxPro.OrderMag
{
    public partial class magOrderDetail : PageBase
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

                //btnAddDetail.Enabled = false;
                //btnDeleteSelected2.Enabled = false;
                //btnBOM.Enabled = false;

                // 删除选中行按钮
                //btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                btnAddDetail.OnClientClick = Window1.GetShowReference("~/OrderMag/OrderDetailNew.aspx?id=" + GetQueryValue("pid") + "&od=" + GetQueryValue("od"), "新增产品");
                CheckPowerWithButton("OrderDelete", btnDeleteSelected2);
                ResolveDeleteButtonForGrid(btnDeleteSelected2, Grid2);

                btnReset.OnClientClick = SF2.GetResetReference();

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
                else if (requestArg.Equals("RefreshGrid1"))
                {

                }
            }

        }


        private void LoadData()
        {
            // 权限检查
            //CheckPowerWithButton("InstockEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("InstockDelete", btnDeleteSelected);
            //CheckPowerWithButton("InstockNew", btnNew);

            int id = GetQueryIntValue("pid");
            using (var appdb = new AppContext())
            {
                OrderHeader current = appdb.orderheader
                    .Where(u => u.SN == id).FirstOrDefault();
                if (current == null)
                {
                    // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                    Alert.Show("参数错误！", String.Empty, ActiveWindow.GetHideReference());
                    return;
                }
                txtOrderNo.Text = current.OrderNo;
                txtClientCode.Text = current.ClientCode;
                txtClinetOrderNo.Text = current.ClientOrderNo;
                txtBuis.Text = current.RecOrderPerson;
                txtContainerType.Text = current.ContainerType;
                txtChecker.Text = current.Checker;
                checkDate.SelectedDate = current.CheckDate;
                recOrderDate.SelectedDate = current.RecOrderDate;
                outOrderDate.SelectedDate = current.OutGoodsDate;
                txtLotno.Text = current.LotNo;

            }
            //btnNew.OnClientClick = Window1.GetShowReference("~/admin/user_new.aspx", "新增用户");
            Grid2.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            // 每页记录数
            BindGrid2();
            BindGrid1();
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                int id = GetQueryIntValue("pid");
                using (var appdb = new AppContext())
                {
                    OrderHeader item = appdb.orderheader
                        .Where(u => u.SN == id).FirstOrDefault();
                    item.ClientOrderNo = txtClinetOrderNo.Text;
                    item.LotNo = txtLotno.Text;
                    item.ClientCode = txtClientCode.Text;
                    item.RecOrderPerson = txtBuis.Text;// GetChineseName(User.Identity.Name);
                    item.Updater = User.Identity.Name;
                    item.UpdateDate = DateTime.Now;
                    item.RecOrderDate = recOrderDate.SelectedDate;
                    item.CheckDate = checkDate.SelectedDate;
                    item.OutGoodsDate = outOrderDate.SelectedDate;
                    item.ContainerType = txtContainerType.Text;
                    item.Checker = txtChecker.Text;


                    appdb.SaveChanges();

                }
                Alert.Show("保存成功");
            }
            catch (Exception ee)
            {
                Alert.Show("保存失败");
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



        private void BindGrid2()
        {




            using (var appdb = new AppContext())
            {
                int pid = GetQueryIntValue("pid");
                var inq = from a in appdb.orderdetail
                          join b in appdb.v_userinfor on a.Inputer equals b.userid into userjoin
                          from c in userjoin.DefaultIfEmpty()
                          join b in appdb.orderheader on a.FSN equals b.SN
                          join d in appdb.v_userinfor on a.Updater equals d.userid into d_join
                          from e in d_join.DefaultIfEmpty()
                          where b.SN == pid
                          select new { a.OrderNo, a.ItemNo, a.ItemName, a.Quantity,  SurfaceDeal=a.Color, a.Price, a.ClinetNo, a.IsBom, a.BomVer, a.SN, a.InputerDate,c.ChineseName, Updater = e.ChineseName, a.UpdateDate };





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
                if (rbtIsBom.SelectedValue != "")
                {
                    int isbom = int.Parse(rbtIsBom.SelectedValue);
                    inq = inq.Where(u => u.IsBom == isbom);
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



        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("InstockEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("OrderDelete", Grid2, "deleteField");
            //CheckPowerWithWindowField("InstockChangePassword", Grid1, "changePasswordField");

        }

        protected void Grid2_PreRowDataBound(object sender, GridPreRowEventArgs e)
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
            else if (e.CommandName == "toBOM")
            {
                if (Grid2.Rows[e.RowIndex].Values[2].ToString() == "配件")
                {
                    string s = @" parent.addExampleTab({
                id: 'RealBom_'+" + ID.ToString() + @"+'_tab',
                iframeUrl: 'BomMag/RealBom.aspx?sn=" + ID + @"&od=" + Grid2.Rows[e.RowIndex].Values[1].ToString() + @"&id=" + Grid2.Rows[e.RowIndex].Values[2].ToString() + @"&q=" + Grid2.Rows[e.RowIndex].Values[4].ToString()+@"&ispj=1" + @"',
                title:'" + Grid2.Rows[e.RowIndex].Values[3].ToString() + @"',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });";
                    PageContext.RegisterStartupScript(s);
                }
                else
                {
                    string sql = "select count(*) from dbo.proBomHeader where ver=(select max(ver) from proBomHeader where prono='" + Grid2.Rows[e.RowIndex].Values[2].ToString() + "')";

                    SQLHelper.DbHelperSQL.SetConnectionString("");
                    if (SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() == "0")
                    {
                        Alert.Show("该产品未找到对应的工程BOM");
                        return;
                    }
                    else
                    {



                        string s = @" parent.addExampleTab({
                id: 'RealBom_'+" + ID.ToString() + @"+'_tab',
                iframeUrl: 'BomMag/RealBom.aspx?sn=" + ID + @"&od=" + Grid2.Rows[e.RowIndex].Values[1].ToString() + @"&id=" + Grid2.Rows[e.RowIndex].Values[2].ToString() + @"&q=" + Grid2.Rows[e.RowIndex].Values[4].ToString() + @"&ispj=0" + @"',
                title:'" + Grid2.Rows[e.RowIndex].Values[3].ToString() + @"',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });";
                        PageContext.RegisterStartupScript(s);
                    }
                }
            }
        }

        protected void Grid2_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference("~/OrderMag/OrderDetailEdit.aspx?id=" + Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString() + "&k=1", "产品详情"));
        }

        protected void Grid2_RowDataBound(object sender, GridRowEventArgs e)
        {

        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid2.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid2();
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
                
                foreach (int i in Grid2.SelectedRowIndexArray)
                {
                    string sql = "select count(*) from dbo.proBomHeader where ver=(select max(ver) from proBomHeader where prono='" + Grid2.Rows[i].Values[2].ToString() + "')";

                    SQLHelper.DbHelperSQL.SetConnectionString("");
                    if (SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() == "0")
                    {
                        Alert.Show("该产品未找到对应的工程BOM");
                        continue;
                    }
                    if (Grid2.Rows[i].Values.GetValue(8).ToString() == "0")
                    {
                        sql = "select count(*) from dbo.proBomHeader where ver=(select max(ver) from proBomHeader where prono='" + Grid2.Rows[i].Values[2].ToString() + "')";
                        if (SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() == "0")
                        {
                            continue;
                        }

                        CommFunction.MakeBomByOrder(Grid2.DataKeys[i][0].ToString(), Grid2.Rows[i].Values[2].ToString(), Grid2.Rows[i].Values[1].ToString(), Grid2.Rows[i].Values[4].ToString(), User.Identity.Name, "OrderDetail");
                        //makeBom(Grid2.Rows[i].Values[2].ToString(), Grid2.Rows[i].Values[1].ToString(), Grid2.Rows[i].Values[4].ToString(), Grid2.DataKeys[i][0].ToString(), "OrderDetail");
                    }
                }
                Alert.Show("生成成功");
            }
            catch (Exception ee)
            {
                Alert.Show("生成失败：" + ee.ToString());
            }
        }




        protected void Grid2_RowClick(object sender, GridRowClickEventArgs e)
        {

        }
        protected void rbtIsBom_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid2();
        }
        protected void ttbSearch_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearch.Text = String.Empty;
            ttbSearch.ShowTrigger1 = false;

            BindGrid1();
        }

        protected void ttbSearch_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearch.ShowTrigger1 = true;

            BindGrid1();
        }

        private void BindGrid1()
        {
            using (var appdb = new AppContext())
            {
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                DateTime d2 = d1.AddDays(-1);
                var q = from a in appdb.probombase
                        join b in appdb.v_userinfor on a.Inputer equals b.userid into userjoin
                        from c in userjoin.DefaultIfEmpty()
                        select new { a.ProName, a.ProNo, a.ClientCode, a.BomDate, a.Ver, a.FileNo, a.Remark, a.BomExcel, a.Inputer, a.InputeDate, c.ChineseName, a.SN };

                //在产品名称中搜索
                string searchText = ttbSearch.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.ProNo.Contains(searchText)|| u.ProName.Contains(searchText));
                }




                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();// q.Count();

                // 排列和数据库分页
                //q = SortAndPage(q, Grid2);

                Grid1.DataSource = q.Take(50);// q;
                Grid1.DataBind();
            }
        }

        protected void Grid1_RowClick(object sender, GridRowClickEventArgs e)
        {
            txtCurProno.Text = Grid1.Rows[e.RowIndex].Values[1].ToString();
            txtBomSn.Text = Grid1.DataKeys[e.RowIndex][0].ToString();
            txtProname.Text = Grid1.Rows[e.RowIndex].Values[2].ToString();
        }
        protected void btnMakeBom_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid2.SelectedRowIndexArray.Length <= 0)
                {
                    Alert.Show("请选择产品");
                    return;
                }
                SQLHelper.DbHelperSQL.SetConnectionString("");
                string sql = "select count(*) from  Instruction where orderno='" + txtOrderNo.Text + "' and prono='" + Grid2.Rows[Grid2.SelectedRowIndex].Values[2] + "' ";
                if (int.Parse(SQLHelper.DbHelperSQL.GetSingle(sql, 30)) > 0)
                {
                    Alert.Show("已进行了备货确认无法再次生成生产BOM");
                    return;
                }
                string odtSN = Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString();
                sql = "select sn from dbo.BomHeader where OdtSN=" + odtSN + "";
                string bomSN = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
                ArrayList al = new ArrayList();
                if (!string.IsNullOrEmpty(bomSN))
                {
                    //删除旧BOM
                    sql = "delete BomHeader where sn=" + bomSN;
                    log.Info("copy bom:::" + sql);
                    al.Add(sql);
                    sql = "delete BomDetail where fsn=" + bomSN;
                    log.Info("copy bom:::" + sql);
                    al.Add(sql);
                }

                //sql = "select count(*) from dbo.proBomHeader where ver=(select max(ver) from proBomHeader where prono='" + Grid2.Rows[Grid2.SelectedRowIndex].Values[2] + "') and  prono='" + Grid2.Rows[Grid2.SelectedRowIndex].Values[2] + "'";
                if(Grid2.Rows[Grid2.SelectedRowIndex].Values[2].ToString()!=txtCurProno.Text)
                {
                    //复制BOM给当前产品
                    sql = "INSERT INTO proBomHeader(AllitemSN,ProName,ProNo,Ver,ClientProNo,ClientCode,Inputer,InputeDate,BomDate,fileno,BomExcel,QuoteProNo,QuoteBomVer,QuoteBomSN) select AllitemSN, '" + Grid2.Rows[Grid2.SelectedRowIndex].Values[3] + "', '" + Grid2.Rows[Grid2.SelectedRowIndex].Values[2] + "', (select ISNULL(max(ver)+1,1) from proBomHeader where prono='" + Grid2.Rows[Grid2.SelectedRowIndex].Values[2] + "'), ClientProNo, ClientCode, Inputer, InputeDate, BomDate, fileno, BomExcel,ProNo,Ver,sn from dbo.proBomHeader where sn =" + txtBomSn.Text;
                    log.Info("copy bom:::" + sql);
                    al.Add(sql);
                    sql = "INSERT INTO proBomDetail(subsn,ParentSN,Seq,FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity,Sclass,MakeMethod,Inputer,InputeDate,Updater,UpdateDate,WorkShop,ZuHe,Remark,ZongCheng,BaseNum,MainFrom,StoreHouse) select b.subsn,(case when CHARINDEX('.',seq)!= 0 then(select(a.SN) from ProBomDetail a  where a.fsn = " + txtBomSn.Text + " and a.Seq = REVERSE(SUBSTRING(REVERSE(b.seq), CHARINDEX('.', REVERSE(b.seq)) + 1, LEN(b.seq) - CHARINDEX('.', REVERSE(b.seq))))) else null end) ParentSN,Seq,(select sn from proBomHeader where ProNo='" + Grid2.Rows[Grid2.SelectedRowIndex].Values[2] + "' and ver=(select max(ver) from proBomHeader where prono='" + Grid2.Rows[Grid2.SelectedRowIndex].Values[2] + "')),AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity,Sclass,MakeMethod,Inputer,InputeDate,Updater,UpdateDate,WorkShop,ZuHe,Remark,ZongCheng,BaseNum,MainFrom,StoreHouse  from proBomDetail b   where fsn =" + txtBomSn.Text;
                    al.Add(sql);
                    log.Info("copy bom:::" + sql);
                }
                
                
                if (al.Count>0&&SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                {
                   
                }
                else
                {
                    Alert.Show("复制BOM失败");
                    return;
                }

                sql = "select max(ver) from proBomHeader where prono='" + Grid2.Rows[Grid2.SelectedRowIndex].Values[2] + "'";
                string ver = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
                string fsn = "";
                if (CommFunction.reMakeBom(odtSN, Grid2.Rows[Grid2.SelectedRowIndex].Values[2].ToString(), Grid2.Rows[Grid2.SelectedRowIndex].Values[1].ToString(), ver, Grid2.Rows[Grid2.SelectedRowIndex].Values[4].ToString(), User.Identity.Name, out fsn))
                {
                    Alert.Show("生成成功");

                }
                else
                {

                    Alert.Show("生成失败");
                }
            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
        }

    }
}