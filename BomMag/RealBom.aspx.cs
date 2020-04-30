using FineUIPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using EntityFramework.Extensions;
using System.Data.OleDb;
using System.Data;
using System.Collections;
using System.Threading;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace AppBoxPro.BomMag
{
    public partial class RealBom : PageBase
    {
        public string pstr = "";
        log4net.ILog log = log4net.LogManager.GetLogger("magPlan.aspx");
        private bool AppendToEnd = false;
        private int ver;
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "BOMEdit";
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
                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                // 新增数据初始值
                //JObject defaultObj = new JObject();
                //defaultObj.Add("ItemNo", "");
                //DayOfWeek dw = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).DayOfWeek;
                //string ss = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dw);
                //defaultObj.Add("ItemName", "");
                //defaultObj.Add("Spec", "");
                //defaultObj.Add("Material", "");
                //defaultObj.Add("SurfaceDeal", "");
                //defaultObj.Add("UsingQuantity", "");
                //defaultObj.Add("Sclass", "");
                //defaultObj.Add("MakeMethod", "");
                //defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));

                //// 在第一行新增一条数据
                //btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);
                // 删除选中行按钮
                btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                txtID.Text = GetQueryValue("sn");


                using (var appdb = new AppContext())
                {
                    int sn = int.Parse(txtID.Text);
                    OrderDetail current = appdb.orderdetail
                        .Where(u => u.SN == sn).FirstOrDefault();
                    if (current == null)
                    {
                        // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                        Alert.Show("参数错误！", String.Empty, ActiveWindow.GetHideReference());
                        return;
                    }

                    txtOrderNo.Text = current.OrderNo;
                    txtItemNo.Text = current.ItemNo;
                    txtItemName.Text = current.ItemName;
                    txtClinetNo.Text = current.ClinetNo;
                    txtQuantity.Text = current.Quantity.ToString();
                    txtColor.Text = current.Color;
                    txtUnit.Text = current.Unit;
                    rbtIsChange.SelectedValue = current.IsChange;
                    rbtIsNew.SelectedValue = current.IsNew;
                    rbtIspacking.SelectedValue = current.IsPackingmaterials;
                    txtConutryVer.Text = current.CountryPackVer;
                    txtInputer.Text = current.Inputer;
                    tbxDemand1.Text = current.Demand1;
                    tbxDemand10.Text = current.Demand10;
                    tbxDemand11.Text = current.Demand11;
                    tbxDemand12.Text = current.Demand12;
                    tbxDemand2.Text = current.Demand2;
                    tbxDemand3.Text = current.Demand3;
                    tbxDemand4.Text = current.Demand4;
                    tbxDemand5.Text = current.Demand5;
                    tbxDemand6.Text = current.Demand6;
                    tbxDemand7.Text = current.Demand7;
                    tbxDemand8.Text = current.Demand8;
                    tbxDemand9.Text = current.Demand9;
                    tbxRemark.Text = current.Remark;
                    txtBomver.Text = current.BomVer.ToString();
                    string pno = current.ItemNo;

                    //ProBomHeader item =appdb.probombase.Where(s=> s.ProNo == pno && s.Ver==current.BomVer).SingleOrDefault();
                    //if(item!=null)
                    //{
                    //    ddlBOM.Value = item.SN.ToString();
                    //}
                    IQueryable<ProBomHeader> bom = appdb.probombase.Where(s => s.ProNo == pno);
                    ddlbomver.DataSource = bom.ToList();
                    ddlbomver.DataTextField = "Ver";
                    ddlbomver.DataValueField = "Ver";
                    ddlbomver.DataBind();

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
                CheckPowerWithButton("OrderDelete", btnDelete);
                ResolveDeleteButtonForGrid(btnDelete, Grid1);
              

                string sql = "select typeName from basedata where stype='类别' order by SortIndex ";
                SQLHelper.DbHelperSQL.SetConnectionString("");
                DataTable dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                ddlItemClass.DataTextField = "typeName";
                ddlItemClass.DataValueField = "typeName";
                ddlItemClass.DataSource = dt;
                ddlItemClass.DataBind();



                ddlUser.Enabled = false; 
                string fsn = "";
                string ret = "";
                string ispj = GetQueryValue("ispj");
                if(ispj.Equals("0"))//非配件
                {
                    ret = CommFunction.MakeBom(GetQueryValue("sn"), GetQueryValue("id"), GetQueryValue("od"), GetQueryValue("q"), User.Identity.Name, out fsn);
                }
                else
                {
                    ret = CommFunction.MakeBomForPJ(GetQueryValue("sn"), GetQueryValue("id"), GetQueryValue("od"), GetQueryValue("q"), User.Identity.Name, out fsn);
                }
                
                if (ret!="已生成")
                {
                    ShowNotify(ret);
                }
                txtFSN.Text = fsn;
                //makeBom();
                LoadData();
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
                BindGrid3();
                BindGrid2();
            }
            else
            {
                string requestArg = GetRequestEventArgument(); // 此函数所在文件：PageBase.cs
                string[] args = requestArg.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                //log.Info(requestArg);
                if (requestArg.Equals("RefreshGrid2"))
                {
                    
                }
                else if (args != null && args.Length > 0 && args[0].Equals("updaterowcss"))
                {
                    // Grid2.Rows[int.Parse(ridx)].RowCssClass = "color1";
                    CommFunction.updateRowCss(args[1], Grid1, "color1");
                }
            }
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

        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        }
        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
            BindGrid();
        }


        #endregion

        #region Events


        private void SaveItem()
        {
            using (var appdb = new AppContext())
            {
                int sn = int.Parse(txtID.Text);
                OrderDetail item = appdb.orderdetail
                    .Where(u => u.SN == sn).FirstOrDefault();
                item.OrderNo = txtOrderNo.Text.Trim();
                item.ClinetNo = txtClinetNo.Text.Trim();
                item.ItemNo = txtItemNo.Text.Trim();
                item.ItemName = txtItemName.Text.Trim();
                item.Quantity = int.Parse(txtQuantity.Text);
                item.Inputer = txtInputer.Text.Trim();
                item.Demand1 = tbxDemand1.Text.Trim();
                item.Demand2 = tbxDemand2.Text.Trim();
                item.Demand3 = tbxDemand3.Text.Trim();
                item.Demand4 = tbxDemand4.Text.Trim();
                item.Demand5 = tbxDemand5.Text.Trim();
                item.Demand6 = tbxDemand6.Text.Trim();
                item.Demand7 = tbxDemand7.Text.Trim();
                item.Demand8 = tbxDemand8.Text.Trim();
                item.Demand9 = tbxDemand9.Text.Trim();
                item.Demand10 = tbxDemand10.Text.Trim();
                item.Demand11 = tbxDemand11.Text.Trim();
                item.Demand12 = tbxDemand12.Text.Trim();
                item.Remark = tbxRemark.Text;

                appdb.SaveChanges();

            }
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {

            try
            {
                SaveItem();

                //PageContext.RegisterStartupScript("<script>alert('保存成功')</script");
                //System.Threading.Thread.Sleep(1000);
                string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid2');");
                PageContext.RegisterStartupScript(scripts);
                Alert.Show("保存成功");
                PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
            }
            catch (Exception ee)
            {
                Alert.Show("保存失败:" + ee.Message);
            }
            finally
            {
                //string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid2');");
                //PageContext.RegisterStartupScript(scripts + ActiveWindow.GetHideReference());
            }

        }
        #endregion

        protected void txtProNo_Blur(object sender, EventArgs e)
        {
            txtisOpen.Text = "0";
            //string openUrl = String.Format("searchitem.aspx?selected={0}", HttpUtility.UrlEncode("dddd"));

            //PageContext.RegisterStartupScript(Window1.GetSaveStateReference(txtProNo.ClientID)
            //        + Window1.GetShowReference(openUrl));
        }


        protected void txtProNo_TextChanged(object sender, EventArgs e)
        {
            msglab.Text = "";
            //Alert.Show(txtisOpen.Text);
            if (txtisOpen.Text == "1")
            {
                txtisOpen.Text = "0";
                return;
            }
            string openUrl = String.Format("searchitem.aspx?k={0}", HttpUtility.UrlEncode(txtItemNo.Text));
            //Window1.GetSaveStateReference(new string[] { txtProNo.ClientID, txtProName.ClientID })
            //+ Window1.GetShowReference(openUrl);
            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(new string[] { txtItemNo.ClientID, txtItemName.ClientID, txtisOpen.ClientID })
                   + Window1.GetShowReference(openUrl) + txtItemNo.GetFocusReference());

        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            //PageContext.RegisterStartupScript("parent.__doPostBack('','RefreshGrid2');");
        }
        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("InstockEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("InstockDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("InstockChangePassword", Grid1, "changePasswordField");

        }

        protected void Grid1_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {
            //User user = e.DataItem as User;

            //// 不能删除超级管理员
            //if (user.Name == "admin")
            //{
            //    FineUI.LinkButtonField deleteField = Grid1.FindColumn("deleteField") as FineUI.LinkButtonField;
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
            if (!CheckPower("BOMDelete"))
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
                    appdb.bomdtl.Where(u => ids.Contains(u.SN)).Delete();
                }
            }

            //// 重新绑定表格
            BindGrid();
        }



        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            int ID = GetSelectedDataKeyID(Grid1);

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
                    appdb.bomdtl.Where(u => u.SN == ID).Delete();

                    BindGrid();
                }
            }
            else if (e.CommandName == "StoreConfirm")
            {
                //e.RowSelectable = false;
                //e.RowCssClass = "color1";
                int odtsn = GetQueryIntValue("sn");
                using (var appdb = new AppContext())
                {
                    string itemno = Grid1.Rows[e.RowIndex].Values[2].ToString();
                    string orderno = txtOrderNo.Text;
                    Instruction item = appdb.instruction
                    .Where(u => u.ItemNo == itemno && u.OrderNo == orderno && u.OdtSN == odtsn && u.IsConfirm == 0).FirstOrDefault();
                    if(item!=null)
                    {
                        Alert.Show("该物料存在未确认的备货确认单，请先确认！");
                        return;
                    }
                }

                //Grid1.Rows[e.RowIndex].RowCssClass = "color1";
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/StoreConfirm/SendInstruction.aspx?id=" + Grid1.DataKeys[e.RowIndex][0].ToString() + "&fsn=" + Grid1.Rows[e.RowIndex].Values[18].ToString() + "&k=1&odtsn=" + GetQueryValue("sn") + "&ridx=" + e.RowIndex, "备货确认单", Unit.Parse("900"), Unit.Parse("800")));
            }
        }
        protected void Grid1_OnRowClick(object sender, FineUIPro.GridRowClickEventArgs e)
        {
            //Alert.Show("rowclick" + e.RowIndex.ToString());
            //Grid2.SelectedRowIndex = e.RowIndex;
            //string meetid = Grid1.Rows[e.RowIndex].Values[1].ToString();
            //Alert.Show(meetid);

            //BindGrid2();
        }
        protected void btnSearch_click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

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
            try
            {
                using (var appdb = new AppContext())
                {
                    DateTime now = DateTime.Now;
                    DateTime d1 = new DateTime(now.Year, now.Month, 1);
                    DateTime d2 = d1.AddDays(-1);
                    //d1是本月的第一天，d2本月的最后一天，
                    //DateTime dtstart = datePickerFrom.SelectedDate == null ? d2 : datePickerFrom.SelectedDate.Value;
                    //DateTime dtend = datePickerTo.SelectedDate == null ? now.AddDays(1) : datePickerTo.SelectedDate.Value.AddDays(1);
                    string itemno = GetQueryValue("id");
                    string orderno = GetQueryValue("od");
                    int OdtSN = GetQueryIntValue("sn");
                    int q = int.Parse(txtQuantity.Text);
                    var qqq = from a in appdb.bomdtl
                              join b in appdb.bombase on a.FSN equals b.SN into basejoin
                              from k in basejoin.DefaultIfEmpty()
                              join c in appdb.allitems on a.AllitemSN equals c.SN into itemjoin
                              from d in itemjoin.DefaultIfEmpty()
                              join e in appdb.instruction.Where(u=>u.IsConfirm==0) on a.SN equals e.BomSN  into instructionjoin  
                              from f in instructionjoin.DefaultIfEmpty()  
                              where k.OdtSN==OdtSN 

                              select new { a.SN, a.FSN, a.Seq, a.ItemNo, a.ItemName, a.Spec, a.Material, a.SurfaceDeal, a.OrderUsingQuantity, a.Sclass, a.MainFrom, a.WorkShop, a.ZuHe, a.SUBSN, a.ParentSN, a.ProUsingQuantity, SupplierId = d.SupplierId == null ? "" : d.SupplierId.ToString(), a.ZongCheng, a.BaseNum, a.StoreHouse, a.AllitemSN, a.IsValid, ISN =f.SN.ToString()};
                    
                    //FileOper.writeLog(qqq.ToString());
                    //在产品名称中搜索
                    string searchText = ttbSearchMessage.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        qqq = qqq.Where(u => u.ItemName.Contains(searchText) || u.ItemNo.Contains(searchText));
                    }




                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = qqq.Count();// q.Count();

                    // 排列和数据库分页
                    //qqq = SortAndPage<BomDetail>(qqq, Grid1);
                    JObject jObject = new JObject();
                    jObject.Add("ItemNo", "<span style='color:red;font-size:18px;'>共计：" + qqq.Count() + "条</span>");
                    jObject.Add("Material", "<span style='color:#000;background-color:#ccc;height:25px;line-height:25px;font-size:18px;padding:5px;'>灰色为无效</span>");
                    jObject.Add("SurfaceDeal", "<span style='color:#000;background-color:#1AA348;height:25px;line-height:25px;font-size:18px;padding:5px;'>绿色为未确认</span>");
                    Grid1.SummaryData = jObject;
                    Grid1.DataSource = qqq;// itemq.Take(2);// q;
                    Grid1.DataBind();
                }
            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }

        }
        protected void tbxItemNo_TriggerClick(object sender, EventArgs e)
        {
            string[] selectedCell = Grid1.SelectedCell;
            if (selectedCell != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference("searchitemforadd.aspx?rowid=" + selectedCell[0]));

            }
        }
        protected void txtMaterial_TriggerClick(object sender, EventArgs e)
        {
            string[] selectedCell = Grid1.SelectedCell;
            if (selectedCell != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference("searchBomForAdd.aspx?rowid=" + selectedCell[0]));

            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Grid1.PageSize = 1000000;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
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


        protected void btnSaveForm_Click(object sender, EventArgs e)
        {
            try
            {
                int sn = int.Parse(txtID.Text);
                string proname = txtItemName.Text.Replace("（", "(").Replace("）", ")");

                proname = proname.Substring(proname.LastIndexOf("(") + 1, proname.LastIndexOf(")") - proname.LastIndexOf("(") - 1);
                string proname2 = txtItemName.Text.Replace("（", "(").Replace("）", ")");
                proname2 = proname2.Substring(proname2.LastIndexOf("(") + 1, proname2.LastIndexOf(")") - proname2.LastIndexOf("(") - 1);
                //Alert.Show(proname + "-----" + proname2);
                ArrayList al = new ArrayList();
                string sql = "update OrderDetail set  ItemName='" + txtItemName.Text + "',Quantity=" + txtQuantity.Text + ",Demand1='" + tbxDemand1.Text + "',Demand2='" + tbxDemand2.Text + "',Demand3='" + tbxDemand3.Text + "',Demand4='" + tbxDemand4.Text + "',Demand5='" + tbxDemand5.Text + "',Demand6='" + tbxDemand6.Text + "',Demand7='" + tbxDemand7.Text + "',Demand8='" + tbxDemand8.Text + "',Demand9='" + tbxDemand9.Text + "',Demand10='" + tbxDemand10.Text + "',Demand11='" + tbxDemand11.Text + "',Demand12='" + tbxDemand12.Text + "',Remark='" + tbxRemark.Text + "' where sn=" + sn;
                al.Add(sql);
                log.Info(sql);
                sql = "update BomDetail set surfacedeal='" + proname + "' where surfacedeal='" + proname2 + "' and fsn=(select sn from BomHeader where orderno='" + txtOrderNo.Text + "' and prono='" + txtItemNo.Text + "')";
                al.Add(sql);
                log.Info(sql);
                SQLHelper.DbHelperSQL.SetConnectionString("");
                if (SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                {
                    txtProName2.Text = txtItemName.Text;
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
                Alert.Show(ee.ToString());
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
                if (Grid1.GetNewAddedList().Count != 0)
                {
                    string str = "";
                    List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
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
                    string sqlval = "",sqlitem="";
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
                                sqlval += "'"+seq+"',";
                            }
                            else if (key.ToLower() == "supplierid")
                            {
                                if (newAddedList[i]["SupplierId"].ToString()!="")
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
                    #region bak
                    //for (int i = 0; i < newAddedList.Count; i++)
                    //{
                    //    if (newAddedList[i]["savetype"].ToString() == "new" || newAddedList[i]["savetype"].ToString() == "copy")
                    //    {
                    //        sql = "INSERT INTO BomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,OrderUsingQuantity,Sclass,Inputer,InputeDate,ZuHe,WorkShop,seq,ProUsingQuantity,ZongCheng,BaseNum,MainFrom,StoreHouse) values(" + txtFSN.Text + ",(select sn from rlitems where itemno='" + newAddedList[i]["ItemNo"].ToString() + "'),'" + newAddedList[i]["ItemNo"].ToString() + "','" + newAddedList[i]["ItemName"].ToString() + "','" + newAddedList[i]["Spec"].ToString() + "','" + newAddedList[i]["Material"].ToString() + "','" + newAddedList[i]["SurfaceDeal"].ToString() + "'," + newAddedList[i]["OrderUsingQuantity"].ToString() + ",'" + newAddedList[i]["Sclass"].ToString() + "','" + User.Identity.Name + "',getdate()," + newAddedList[i]["ZuHe"].ToString() + ",'" + newAddedList[i]["WorkShop"].ToString() + "','" + seq + "'," + newAddedList[i]["ProUsingQuantity"].ToString() + ",'" + newAddedList[i]["ZongCheng"].ToString() + "','" + newAddedList[i]["BaseNum"].ToString() + "','" + newAddedList[i]["MainFrom"].ToString() + "','" + newAddedList[i]["StoreHouse"].ToString() + "')";
                    //    }
                    //    else
                    //    {
                    //        sql = "INSERT INTO BomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,OrderUsingQuantity,Sclass,Inputer,InputeDate,ZuHe,WorkShop,seq,ParentSN,ProUsingQuantity,ZongCheng,BaseNum,StoreHouse,MainFrom) values(" + txtFSN.Text + ",(select sn from rlitems where itemno='" + newAddedList[i]["ItemNo"].ToString() + "'),'" + newAddedList[i]["ItemNo"].ToString() + "','" + newAddedList[i]["ItemName"].ToString() + "','" + newAddedList[i]["Spec"].ToString() + "','" + newAddedList[i]["Material"].ToString() + "','" + newAddedList[i]["SurfaceDeal"].ToString() + "'," + newAddedList[i]["OrderUsingQuantity"].ToString() + ",'" + newAddedList[i]["Sclass"].ToString() + "','" + User.Identity.Name + "',getdate()," + newAddedList[i]["ZuHe"].ToString() + ",'" + newAddedList[i]["WorkShop"].ToString() + "','" + seq + "'," + newAddedList[i]["ParentSN"].ToString() + "," + newAddedList[i]["ProUsingQuantity"].ToString() + ",'" + newAddedList[i]["ZongCheng"].ToString() + "','" + newAddedList[i]["BaseNum"].ToString() + "','" + newAddedList[i]["StoreHouse"].ToString() + "','" + newAddedList[i]["MainFrom"].ToString() + "')";
                    //    }
                    //    seq = (int.Parse(seq) + 1).ToString();
                    //    log.Info("producebom:::" + sql);
                    //    al.Add(sql);
                    //    sql = "UPDATE RLItems  SET SupplierId=" + newAddedList[i]["SupplierId"].ToString() + " where sn=" + newAddedList[i]["AllitemSN"].ToString();  //AllitemSN
                    //    al.Add(sql);
                    //}
                    #endregion 
                    sql = "update BomDetail set subsn=sn where subsn is null and FSN=" + txtFSN.Text;
                    al.Add(sql);
                    log.Info("producebom:::" + sql);
                }

                // 修改的现有数据
                if (Grid1.GetModifiedData().Count != 0)
                {
                    Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                    foreach (int rowIndex in modifiedDict.Keys)
                    {
                        if (!modifiedDict[rowIndex].ContainsKey("SupplierId"))
                        {
                            sql = "update BomDetail set ";
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
                            log.Info("producebom:::" + sql);
                        }
                        else
                        {
                            sql = "UPDATE RLItems  SET SupplierId=" + modifiedDict[rowIndex]["SupplierId"].ToString() + " where sn=" + Grid1.Rows[rowIndex].Values[23].ToString();  //AllitemSN
                            al.Add(sql);
                            log.Info("producebom:::" + sql);
                        }
                    }
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
                Alert.Show(ee.ToString());
            }
        }
        protected void txtItemNo_Blur(object sender, EventArgs e)
        {
            txtisOpen.Text = "0";
            //string openUrl = String.Format("searchitem.aspx?selected={0}", HttpUtility.UrlEncode("dddd"));

            //PageContext.RegisterStartupScript(Window1.GetSaveStateReference(txtItemNo.ClientID)
            //        + Window1.GetShowReference(openUrl));
        }


        protected void txtItemNo_TextChanged(object sender, EventArgs e)
        {
            msglab.Text = "";
            //Alert.Show(txtisOpen.Text);
            if (txtisOpen.Text == "1")
            {
                txtisOpen.Text = "0";
                return;
            }
            string openUrl = String.Format("searchitem.aspx?k={0}", HttpUtility.UrlEncode(txtItemNo.Text));
            //Window1.GetSaveStateReference(new string[] { txtItemNo.ClientID, txtItemName.ClientID })
            //+ Window1.GetShowReference(openUrl);
            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(new string[] { txtItemNo.ClientID, txtItemName.ClientID, txtisOpen.ClientID })
                   + Window1.GetShowReference(openUrl) + txtItemNo.GetFocusReference());

        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                int[] selections = Grid1.SelectedRowIndexArray;
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
                    //defaultObj.Add("OrderNo", Grid1.Rows[rowIndex].Values[1].ToString());
                    //defaultObj.Add("ProNo", Grid1.Rows[rowIndex].Values[2].ToString());
                    //defaultObj.Add("ProName", Grid1.Rows[rowIndex].Values[3].ToString());
                    defaultObj.Add("Seq", "");
                    defaultObj.Add("ItemNo", Grid1.Rows[rowIndex].Values[2].ToString());
                    defaultObj.Add("ItemName", Grid1.Rows[rowIndex].Values[3].ToString());
                    defaultObj.Add("Spec", Grid1.Rows[rowIndex].Values[4].ToString());
                    defaultObj.Add("Material", Grid1.Rows[rowIndex].Values[5].ToString());
                    defaultObj.Add("SurfaceDeal", Grid1.Rows[rowIndex].Values[6].ToString());
                    defaultObj.Add("ProUsingQuantity", Grid1.Rows[rowIndex].Values[7].ToString());
                    defaultObj.Add("OrderUsingQuantity", Grid1.Rows[rowIndex].Values[8].ToString());
                    defaultObj.Add("ZongCheng", Grid1.Rows[rowIndex].Values[9].ToString());
                    defaultObj.Add("BaseNum", Grid1.Rows[rowIndex].Values[10].ToString());
                    defaultObj.Add("WorkShop", Grid1.Rows[rowIndex].Values[11].ToString());
                    defaultObj.Add("MainFrom", Grid1.Rows[rowIndex].Values[12].ToString());
                    defaultObj.Add("Sclass", Grid1.Rows[rowIndex].Values[13].ToString());
                    defaultObj.Add("StoreHouse", Grid1.Rows[rowIndex].Values[14].ToString());
                    defaultObj.Add("SupplierId", Grid1.Rows[rowIndex].Values[15].ToString());
                    //FSN   18
                    defaultObj.Add("ZuHe", "0");  //19
                    defaultObj.Add("ParentSN", ""); //20
                    defaultObj.Add("SUBSN", "");  //21
                    defaultObj.Add("savetype", "copy"); //22
                                                        //AllitemSN 23
                    defaultObj.Add("AllitemSN", Grid1.Rows[rowIndex].Values[23].ToString());
                    defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));
                    PageContext.RegisterStartupScript(Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd));
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
                PageContext.RegisterStartupScript(Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd));
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

                //Alert.Show(Grid1.SelectedRowIndex.ToString());
                ////Alert.Show(Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString());
                //return;
                if (Grid1.SelectedRowIndex == -1)
                {
                    Alert.Show("未选择一个物料或者选择的物料尚未保存");
                    return;
                }
                string sql = "select SUBSN from BomDetail where sn=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString();
                SQLHelper.DbHelperSQL.SetConnectionString("");
                string SUBSN = SQLHelper.DbHelperSQL.GetSingle(sql, 30);
                if (string.IsNullOrEmpty(SUBSN) || SUBSN != Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString())
                {
                    sql = "update BomDetail set SUBSN=sn,ZuHe=1 where sn=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString();
                    SQLHelper.DbHelperSQL.ExecuteSql(sql, 30);
                }
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
                defaultObj.Add("ParentSN", Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString());
                defaultObj.Add("ZuHe", "1");
                defaultObj.Add("SUBSN", "");
                defaultObj.Add("savetype", "zuhe");
                defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));
                PageContext.RegisterStartupScript(Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd));
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
                if (Grid1.GetModifiedData().Count != 0 || Grid1.GetNewAddedList().Count != 0)
                {
                    Alert.Show("表格还有未保存的数据，请先保存！");
                    return;
                }
                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.Show("请选择物料");
                    return;
                }
                string OdtSN = GetQueryValue("sn");
                Alert.Show(CommFunction.PLsendInstruction(txtOrderNo.Text, txtItemNo.Text, txtItemName.Text, ddlDept.SelectedText, ddlUser.SelectedValue, OdtSN, User.Identity.Name, Grid1));
                #region bak
                //StringBuilder sql = new StringBuilder();
                //ArrayList al = new ArrayList();
                //string s = "";
                //SQLHelper.DbHelperSQL.SetConnectionString("");
                //foreach (int i in Grid1.SelectedRowIndexArray)
                //{
                //    s = "select count(*) from Instruction where OrderNo='" + txtOrderNo.Text + "' and OdtSN=" + OdtSN + " and itemno='" + Grid1.Rows[i].Values[2].ToString() + "' and IsConfirm=0";
                //    if (int.Parse(SQLHelper.DbHelperSQL.GetSingle(s, 30)) > 0)
                //    {
                //        continue;
                //    }
                //    s = "select (case  when sum(ConfirmQuantity) is null then 0 else sum(ConfirmQuantity) end) from instruction where orderno='" + txtOrderNo.Text + "' and OdtSN=" + OdtSN + " and itemno='" + Grid1.Rows[i].Values[2].ToString() + "'";
                //    sql.Clear();
                //    sql.Append("insert into Instruction(OrderNo,ProNo,ProName,ItemNo,ItemName,Spec,Material,SurfaceDeal,UsingQuantity,Sclass,MainFrom,Inputer,InputeDate,IsConfirm,IsPlan,ReceiveDept,Receiver,BarCode,BomSN,OdtSN)");
                //    sql.Append(" values(");
                //    sql.Append("'" + txtOrderNo.Text + "',");
                //    sql.Append("'" + txtItemNo.Text + "',");
                //    sql.Append("'" + txtItemName.Text + "',");
                //    sql.Append("'" + Grid1.Rows[i].Values[2].ToString() + "',");//ItemNo
                //    sql.Append("'" + Grid1.Rows[i].Values[3].ToString() + "',");//ItemName
                //    sql.Append("'" + Grid1.Rows[i].Values[4].ToString() + "',");//Spec
                //    sql.Append("'" + Grid1.Rows[i].Values[5].ToString() + "',");//Material
                //    sql.Append("'" + Grid1.Rows[i].Values[6].ToString() + "',");//SurfaceDeal
                //    sql.Append("" + Grid1.Rows[i].Values[8].ToString() + "-(" + s + "),");//UsingQuantity
                //    sql.Append("'" + Grid1.Rows[i].Values[9].ToString() + "',");//Sclass
                //    sql.Append("'" + Grid1.Rows[i].Values[12].ToString() + "',");//MainFrom


                //    sql.Append("'" + User.Identity.Name + "',");
                //    sql.Append("getdate(),");
                //    sql.Append("0,");
                //    sql.Append("0,");

                //    sql.Append("'" + ddlDept.SelectedText + "',");
                //    sql.Append("'" + ddlUser.SelectedValue + "',");
                //    sql.Append("'" + DateTime.Now.ToString("yyyyMMddHHmmsss") + i.ToString() + "',");
                //    sql.Append(Grid1.DataKeys[i][0]+",");
                //    sql.Append(OdtSN);
                //    sql.Append(")");
                //    al.Add(sql.ToString());
                //    //log.Info(sql.ToString());
                //    FileOper.writeLog(sql.ToString());
                //}

                //if (SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                //{
                //    Alert.Show("保存成功");
                //    BindGrid();
                //}
                //else
                //{
                //    Alert.Show("保存失败");
                //}
                #endregion
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
        protected void ttbSearch_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearch.Text = String.Empty;
            ttbSearch.ShowTrigger1 = false;

            BindGrid2();
        }

        protected void ttbSearch_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearch.ShowTrigger1 = true;

            BindGrid2();
        }

        private void BindGrid2()
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
                    q = q.Where(u => u.ProNo.Contains(searchText));
                }




                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = q.Count();// q.Count();

                // 排列和数据库分页
                //q = SortAndPage(q, Grid2);

                Grid2.DataSource = q.Take(50);// q;
                Grid2.DataBind();
            }
        }

        protected void btnMakeBom_Click(object sender, EventArgs e)
        {
            try
            {

                SQLHelper.DbHelperSQL.SetConnectionString("");
                string sql = "select count(*) from  Instruction where orderno='" + txtOrderNo.Text + "' and prono='" + txtItemNo.Text + "' ";
                if (int.Parse(SQLHelper.DbHelperSQL.GetSingle(sql, 30)) > 0)
                {
                    Alert.Show("已进行了备货确认无法再次生成生产BOM");
                    return;
                }
                ArrayList al = new ArrayList();
                sql = "delete BomHeader where sn=" + txtFSN.Text;
                al.Add(sql);
                sql = "delete BomDetail where fsn=" + txtFSN.Text;
                al.Add(sql);
                if (SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                {
                    //Alert.Show("value:::"+ddlBOM.Text);
                    string fsn = "";
                    if (CommFunction.reMakeBom(GetQueryValue("sn"),GetQueryValue("id"), GetQueryValue("od"), ddlbomver.SelectedText, GetQueryValue("q"), User.Identity.Name, out fsn))
                    {
                        txtFSN.Text = fsn;
                        Alert.Show("生成成功");
                        BindGrid();
                    }
                    else
                    {
                        txtFSN.Text = "";
                           Alert.Show("生成失败");
                    }
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
        protected void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            BindGrid3();
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
           
            if (!string.IsNullOrEmpty(Grid1.Rows[e.RowIndex].Values[26].ToString()))
            { 
                e.RowCssClass = "color1";
            }
            if (Grid1.Rows[e.RowIndex].Values[25].ToString() == "1")
            {
                e.RowSelectable = false;
                e.RowCssClass = "color2";
                foreach (GridColumn column in Grid1.Columns)
                {
                    e.CellCssClasses[column.ColumnIndex] = "f-grid-cell-uneditable";
                }
            }
        }

        protected void Grid2_RowClick(object sender, GridRowClickEventArgs e)
        {
            txtCurProno.Text = Grid2.Rows[e.RowIndex].Values[1].ToString();
        }
    }
}