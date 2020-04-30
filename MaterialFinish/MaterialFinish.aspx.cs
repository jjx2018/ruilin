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

namespace AppBoxPro.MaterialFinish
{
    public partial class MaterialFinish : PageBase
    {
        static Hashtable htClickColsName = new Hashtable();
        log4net.ILog log = log4net.LogManager.GetLogger("magPlan.aspx");

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "MaterialFinishView";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                
                 
                // 删除选中行按钮
                //btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                
                btnClear.OnClientClick = SF1.GetResetReference();
                LoadData();
            }
            else
            {
                string requestArg = GetRequestEventArgument(); // 此函数所在文件：PageBase.cs
                //log.Info(requestArg);
                if (requestArg.Equals("RefreshGrid2"))
                {

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
                
               

                var qqq = from a in appdb.v_proqtl select a;

                //var qqq = from a in appdb.orderheader
                //          from d in appdb.orderdetail
                //          join e in appdb.bombase on d.SN equals e.OdtSN into e_join
                //          from f in e_join.DefaultIfEmpty()
                //          join b in appdb.v_userinfor on a.Inputer equals b.userid into userjoin
                //          from c in userjoin.DefaultIfEmpty()
                //          where d.FSN==a.SN
                //          select new { a.SN, a.OrderNo, a.ClientOrderNo, a.LotNo, a.ClientCode, a.RecOrderPerson, a.RecOrderDate, a.CheckDate, a.OutGoodsDate, a.ContainerType, a.Inputer, a.InputerDate, a.IsCheck, c.ChineseName, a.IsBom, a.OrderType,d.ItemNo,d.ItemName,d.Quantity,f.InputeDate };

                //在产品名称中搜索
                string searchText = ttbSearchMessage.Text;
                if (!String.IsNullOrEmpty(searchText))
                {
                    qqq = qqq.Where(u => u.OrderNo.Contains(searchText) || u.ItemNo.Contains(searchText) || u.ItemName.Contains(searchText) );
                }
                //searchText为空或者选择“全部”，则列出全部
                else
                {

                }
                qqq = qqq.Where(u => u.RecOrderDate >= dtstart && u.RecOrderDate <= dtend);
                if (rbtIsQT.SelectedValue != "")
                {
                    if (rbtIsQT.SelectedValue == "0")
                    {
                        qqq = qqq.Where(u => u.qtl<100);
                    }
                    else
                    {
                        qqq = qqq.Where(u => u.qtl >= 100);
                    }
                }
                //foreach (DictionaryEntry de in htClickColsName)
                //{
                //    switch (de.Key.ToString())
                //    {
                //        case "OrderNo":
                //            qqq = qqq.Where(u => u.OrderNo == de.Value.ToString());
                //            break;
                //        //case "OrderType":
                //        //    qqq = qqq.Where(u => u.OrderType == de.Value.ToString());
                //        //    break;
                //        //case "ClientOrderNo":
                //        //    qqq = qqq.Where(u => u.ClientOrderNo == de.Value.ToString());
                //        //    break;

                //        case "RecOrderDate":
                //            qqq = qqq.Where(u => u.RecOrderDate.ToString() == de.Value.ToString());
                //            break;
                //        //case "CheckDate":
                //        //    qqq = qqq.Where(u => u.CheckDate.ToString() == de.Value.ToString());
                //        //    break;
                //        //case "OutGoodsDate":
                //        //    qqq = qqq.Where(u => u.OutGoodsDate.ToString() == de.Value.ToString());
                //        //    break;
                //        //case "ContainerType":
                //        //    qqq = qqq.Where(u => u.ContainerType.ToString() == de.Value.ToString());
                //        //    break;
                //        //case "ChineseName":
                //        //    qqq = qqq.Where(u => u.ChineseName.ToString() == de.Value.ToString());
                //        //    break;
                //        //case "InputerDate":
                //        //    DateTime dt = DateTime.Parse(de.Value.ToString());
                //        //    qqq = qqq.Where(u => u.InputerDate == dt);
                //        //    break;
                //        //case "IsCheck":
                //        //    qqq = qqq.Where(u => u.IsCheck.ToString() == de.Value.ToString());
                //        //    break;
                //    }
                //}

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = qqq.Count();// q.Count();

                // 排列和数据库分页
                qqq = SortAndPage(qqq, Grid1);

                Grid1.DataSource = qqq;// itemq.Take(2);// q;
                Grid1.DataBind();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string[] s = txtClickColsName.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (s == null || s.Length == 0)
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
                txtClickColsName.Text += de.Key.ToString() + ",";
            }

        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            string[] s = Grid1.SelectedCell;
            for (int i = 0; i < Grid1.Columns.Count; i++)
            {
                if (s[1] == Grid1.Columns[i].ColumnID && !htClickColsName.ContainsKey(s[1]))
                {
                    htClickColsName.Add(s[1], Grid1.Rows[e.RowIndex].Values[i].ToString());
                    break;
                }
            }
            BindGrid();
            updatecol();
            //PageContext.RegisterStartupScript(Window2.GetShowReference("~/OrderMag/Order_Edit.aspx?id=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString() + "&k=1", "订单"));
        }

        #endregion

        #region Events


        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("OrderEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("OrderDelete", Grid1, "deleteField");
            ////CheckPowerWithWindowField("InstockChangePassword", Grid1, "changePasswordField");

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

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
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
            //btnAddDetail.Enabled = true;
            //btnDeleteSelected2.Enabled = true;
            //btnBOM.Enabled = true;
            //btnHeaderBom.Enabled = true;
            //btnAddDetail.OnClientClick = Window1.GetShowReference("~/ruilin/OrderDetailNew.aspx?id=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString() + "&od=" + Grid1.DataKeys[Grid1.SelectedRowIndex][1].ToString(), "新增产品");
            //BindGrid2();


        }
        protected void btnSearch_click(object sender, EventArgs e)
        {
            BindGrid();
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



 
        #endregion




        

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            //if (Grid1.Rows[e.RowIndex].Values[18].ToString().Equals("1"))
            //{
            //    e.RowCssClass = "color1";
            //}

        }

       

        protected void rbtIsQT_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}