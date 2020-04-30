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

namespace AppBoxPro.ClientProviderMag
{
    public partial class CPPlan : PageBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger("magPlan.aspx");
        static Hashtable htClickColsName = new Hashtable();

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "ClientProviderView";
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

                    IQueryable<Provider> pq = appdb.providers;
                    ddlSupplierId.DataSource = pq.ToList();
                    ddlSupplierId.DataTextField = "Name";
                    ddlSupplierId.DataValueField = "Name";
                    ddlSupplierId.DataBind();
                }
                LoadData();
            }
        }

        private void LoadData()
        {
            // 权限检查

            //CheckPowerDeleteWithButton(btnDeleteSelected);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);



            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            BindGrid2();
            BindGrid();
        }
        protected void btnSearch_click(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {
            using (var appdb = new AppContext())
            {
                var qbom = from e in appdb.bombase
                           from f in appdb.bomdtl
                           where e.SN == f.FSN
                           select new { e.ProNo, e.ProName, SaleOrderNo = e.OrderNo, f.ItemNo, f.ItemName, f.IsValid,BomSN =(int?) e.SN };
                if (rbtIsState.SelectedValue != "")
                {
                    int k = int.Parse(rbtIsState.SelectedValue);
                    if (k == 0)
                    {
                        qbom = qbom.Where(u => u.IsValid == 0);
                    }
                }
                var q = from a in appdb.cpplan
                            //join b in appdb.allitems on a.ItemNo equals b.ItemNo into joinitem
                            //from c in joinitem.DefaultIfEmpty()
                        join d in qbom on new { a.ProNo, a.ItemNo, a.SaleOrderNo,a.BomSN } equals new { d.ProNo, d.ItemNo, d.SaleOrderNo,d.BomSN} into d_join
                        from g in d_join.DefaultIfEmpty()
                        select new { a.ItemNo, a.ItemName, a.ProName, a.ProNo, a.CPPlanNo, a.SaleOrderNo, a.ProviderName, a.Spec, a.Quantity, a.PlanStartDate, a.PlanFinishDate, a.SN, a.BomSN, a.State, g.IsValid,a.ZhuangPeiDate, a.ISN };

                // 在职务名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(t => t.ItemName.Contains(searchText) || t.ItemNo.Contains(searchText) || t.Spec.Contains(searchText));
                }
                if (rbtIsState.SelectedValue != "")
                {
                    int k = int.Parse(rbtIsState.SelectedValue);
                    q = q.Where(u => u.State == k);
                }
                foreach (DictionaryEntry de in htClickColsName)
                {
                    switch (de.Key.ToString())
                    {
                        case "OrderNo":
                            q = q.Where(u => u.SaleOrderNo == de.Value.ToString());
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
                        case "CPPlanNo":
                            q = q.Where(u => u.CPPlanNo == de.Value.ToString());
                            break;
                        case "ProviderName":
                            q = q.Where(u => u.ProviderName == de.Value.ToString());
                            break;
                        case "Quantity":
                            q = q.Where(u => u.Quantity.ToString() == de.Value.ToString());
                            break;
                        case "PlanFinishDate":
                            q = q.Where(u => u.PlanFinishDate.ToString() == de.Value.ToString());
                            break;

                    }
                }

                //Alert.Show(q.ToString());
                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();

                // 排列和分页
                q = SortAndPage(q, Grid1);

                Grid1.DataSource = q;
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
        }

        #endregion

        #region Events
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

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithWindowField("ClientProviderEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("ClientProviderDelete", Grid1, "deleteField");
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
            int sn = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("ClientProviderDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                using (var appdb = new AppContext())
                {
                    //int userCount = DB.Users.Where(u => u.Titles.Any(t => t.ID == titleID)).Count();
                    //if (userCount > 0)
                    //{
                    //    Alert.ShowInTop("删除失败！需要先清空拥有此职务的用户！");
                    //    return;
                    //}
                    appdb.prdmissionsl.Where(t => t.SN == sn).Delete();
                }
                BindGrid();
            }
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndex == -1)
            {
                Alert.Show("请先选择需要删除的数据");
                return;
            }
            // 在操作之前进行权限检查
            if (!CheckPower("ClientProviderDelete"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
            List<int> ids = GetSelectedDataKeyIDs(Grid1);
            string isns = "";
            foreach (int i in Grid1.SelectedRowIndexArray)
            {
                isns += Grid1.Rows[i].Values[18].ToString() + ",";
            }
            isns = isns.TrimEnd(new char[] { ',' });
            string sql = "update instruction set isplan=0 where sn in (" + isns + ")";
            // 执行数据库操作
            //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
            //DB.SaveChanges();

            using (var appdb = new AppContext())
            {
                appdb.purchaseplan.Where(u => ids.Contains(u.SN)).Delete();
                SQLHelper.DbHelperSQL.SetConnectionString("");
                SQLHelper.DbHelperSQL.ExecuteSql(sql, 30);
            }

            // 重新绑定表格
            BindGrid();
        }

        #endregion

        #region  export excel
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Grid1.PageSize = 1000000;
            BindGrid();
            //sql = "select ItemNo,ItemName,Spec,recClass from ("+sql+") a";
            //log.Info(sql);
            //SQLHelper.DbHelperSQL.SetConnectionString("");
            ////Grid grid = new Grid();


            //Grid2.DataSource = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 60);
            //Grid2.DataBind();
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

        #endregion

        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
        }
        private void BindGrid2()
        {
            using (var appdb = new AppContext())
            {
                IQueryable<Provider> q = appdb.providers;

                // 在职务名称中搜索
                string searchText = ddlProvider.Text;
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(t => t.Name.Contains(searchText));
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = q.Count();

                // 排列和分页
                q = SortAndPage<Provider>(q, Grid2);

                Grid2.DataSource = q;
                Grid2.DataBind();
            }
        }

        protected void btnCPSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid1.SelectedRowIndex == -1)
                {
                    Alert.Show("请先选择数据");
                    return;
                }
                if (save())
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
        private bool save()
        {
            string sql = "select max(RIGHT(CPORDERNO,LEN(CPORDERNO)-2)) from CPOrderHeader where InputeDate>=CONVERT(varchar(100), GETDATE(), 23)";
            SQLHelper.DbHelperSQL.SetConnectionString("");
            string CPORDERNO = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
            if (string.IsNullOrEmpty(CPORDERNO))
            {
                sql = "Select CONVERT(varchar(100), GETDATE(), 112)";
                CPORDERNO = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() + "0001";
            }
            else
            {
                CPORDERNO = (Int64.Parse(CPORDERNO) + 1).ToString();
            }
            ArrayList al = new ArrayList();
            int row = 0;
            //,Properties ,PDate ,Purchaser ,Dept ,Project ,Provider ,ProviderName
            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
            foreach (int rowIndex in Grid1.SelectedRowIndexArray)
            {
                if (row == 0)
                {
                    sql = "INSERT INTO CPOrderHeader (CPOrderNo,CPDate,Provider,ProviderID,JBRID,JBRName,ContactMan,Tel,Fax,JHDate,JHPlace,JSFS,ZZSFP,ProviderConfirm,ApproveID,CheckerID,MakerID,Inputer,InputeDate,CPPlanNo,SaleOrderNo) values('CP" + CPORDERNO + "','" + XDDate.Text + "','" + Grid1.Rows[rowIndex].Values[2].ToString() + "','" + Grid1.Rows[rowIndex].Values[10].ToString() + "','" + User.Identity.Name + "','" + GetChineseName() + "','" + txtContactMan.Text + "','" + txtTel.Text + "','" + txtFax.Text + "','" + JHdate.Text + "','锐麟厂','月结',0,'','','','" + User.Identity.Name + "','" + User.Identity.Name + "',getdate(),'" + Grid1.Rows[rowIndex].Values[3].ToString() + "','" + Grid1.Rows[rowIndex].Values[1].ToString() + "')";
                    al.Add(sql);
                    row = 1;
                }
                System.Threading.Thread.Sleep(1);
                sql = "INSERT INTO CPOrderDetail (FSN,CPOrderNo,CPPlanNo,SaleOrderNo,ProNo,ProName,ItemNo,ItemName,Spec,Quantity,Unit,Remark,Inputer,InputeDate,BomSN,BarCode) select (select max(sn) from CPOrderHeader where CPOrderNo='CP" + CPORDERNO + "'),'CP" + CPORDERNO + "',CPPlanNo,SaleOrderNo,prono,ProName ,ItemNo ,ItemName ,Spec ,Quantity,Unit,'','" + User.Identity.Name + "',getdate(),BomSN,'" + DateTime.Now.ToString("yyyyMMddHHmmssfff")+ "' from CPlan where sn=" + Grid1.DataKeys[rowIndex][0].ToString();
                al.Add(sql);
                log.Info("sqlpur::::" + sql);
                sql = "update CPlan set State=1 where sn=" + Grid1.DataKeys[rowIndex][0].ToString();
                al.Add(sql);
                log.Info("sqlpur::::" + sql);
                //sql = "update AllItem set SupplierId=(select sn from Provider where name='" + modifiedDict[rowIndex]["Provider"].ToString() + "') where itemno='" + Grid1.Rows[rowIndex].Values[6].ToString() + "'";
                //al.Add(sql);
                //log.Info("sqlAllItem::::" + sql);
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

        protected void DropDownBox1_TextChanged(object sender, EventArgs e)
        {
            BindGrid2();
        }

        protected void Grid2_RowClick(object sender, GridRowClickEventArgs e)
        {
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                Grid1.UpdateCellValue(i, "Provider", ddlProvider.Text);
            }

        }
        protected void rbtIsState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            if (Grid1.Rows[e.RowIndex].Values[14].ToString() == "1")
            {
                e.RowSelectable = false;
                e.RowCssClass = "color2";
                foreach (GridColumn column in Grid1.Columns)
                {
                    e.CellCssClasses[column.ColumnIndex] = "f-grid-cell-uneditable";
                }
            }
        }
    }
}