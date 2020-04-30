using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using FineUIPro;
using EntityFramework.Extensions;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using System.Drawing.Imaging;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Data;

namespace AppBoxPro.ruilin
{
    public partial class AccountOrderPrice : PageBase
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

                
                // 删除选中行按钮
                //btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                

                btnReset.OnClientClick = SF2.GetResetReference();
                btnClear.OnClientClick = SF1.GetResetReference();
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
            
            BindGrid();
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
                    qqq = qqq.Where(u => u.OrderNo.Contains(searchText) || u.LotNo.Contains(searchText) || u.Inputer.Contains(searchText) || u.ClientCode.Contains(searchText));
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
                int pid = int.Parse(Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString());// GetSelectedDataKeyID(Grid1).ToString();

                var inq = from a in appdb.orderdetail
                          join b in appdb.orderheader on a.FSN equals b.SN
                          where b.SN == pid
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
                inq = SortAndPage<OrderDetail>(inq, Grid2);

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
            if (Grid1.SelectedRowIndex == -1)
                return;
            //Grid2.SelectedRowIndex = e.RowIndex;
            //string meetid = Grid1.Rows[e.RowIndex].Values[1].ToString();
            //Alert.Show(meetid);
            
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

        


         protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            // 删除新增未保存到数据库的数据
            List<int> deletedRows = Grid1.GetDeletedList();
            if (deletedRows != null && Grid1.GetDeletedList().Count != 0)
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
                if (Grid1.Rows[rowindex].Values[11].ToString() == "1")
                {
                    Alert.Show("第" + (rowindex + 1) + "行订单已审核通过不能删除");
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
                    if (appdb.orderdetail.Where(u => ids.Contains(u.FSN) && u.IsBom == 1).Count() > 0)
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
            else if (e.CommandName == "toBOM")
            {
                string sql = "select count(*) from dbo.proBomHeader where ver=(select max(ver) from proBomHeader where prono='" + Grid2.Rows[e.RowIndex].Values[3].ToString() + "')";

                SQLHelper.DbHelperSQL.SetConnectionString("");
                if (SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() == "0")
                {
                    Alert.Show("该产品未找到对应的工程BOM");
                    return;
                }
                else
                {
                    string s = @" parent.addExampleTab({
                id: " + ID.ToString() + @"+'_tab',
                iframeUrl: 'ruilin/RealBom.aspx?sn=" + ID + @"&od=" + Grid2.Rows[e.RowIndex].Values[1].ToString() + @"&id=" + Grid2.Rows[e.RowIndex].Values[3].ToString() + @"&q=" + Grid2.Rows[e.RowIndex].Values[6].ToString() + @"',
                title:'" + Grid2.Rows[e.RowIndex].Values[4].ToString() + @"',
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


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList al = new ArrayList();
                string sql = "";
                for (int i = 0, count = Grid2.Rows.Count; i < count; i++)
                {
                    GridRow row = Grid2.Rows[i];
                    System.Web.UI.WebControls.TextBox tbxPrice = (System.Web.UI.WebControls.TextBox)row.FindControl("txtPrice");
                    sql = "update orderdetail set price=" + tbxPrice.Text + " where sn=" + Grid2.DataKeys[i][0].ToString() ;
                    al.Add(sql);
                    log.Info("update orderdetail:::" + sql);
                }
                if (al.Count > 0)
                {
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
                else
                {
                    Alert.Show("没有需要保存的数据");
                }
            }
            catch (Exception ee)
            {
                Alert.Show(ee.Message);
            }
        }

 
       
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window2.GetShowReference("~/ruilin/Order_Edit.aspx?id=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString() + "&k=1", "订单"));
        }


    }
}