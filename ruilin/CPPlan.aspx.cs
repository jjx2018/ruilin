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


namespace AppBoxPro.ruilin
{
    public partial class CPPlan : PageBase
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
                return "PlanView";
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
                IQueryable<CPlan> q = appdb.cpplan;

                // 在职务名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(t => t.ItemName.Contains(searchText) || t.ItemNo.Contains(searchText) || t.Spec.Contains(searchText));
                }
                //sql = q.ToString();
                //Alert.Show(sql);
                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();

                // 排列和分页
                q = SortAndPage<CPlan>(q, Grid1);

                Grid1.DataSource = q;
                Grid1.DataBind();
            }
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
            CheckPowerWithWindowField("PlanEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("PlanDelete", Grid1, "deleteField");
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
                if (!CheckPower("PlanDelete"))
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
            if (!CheckPower("PlanDelete"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
            List<int> ids = GetSelectedDataKeyIDs(Grid1);

            // 执行数据库操作
            //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
            //DB.SaveChanges();

            using (var appdb = new AppContext())
            {
                appdb.purchaseplan.Where(u => ids.Contains(u.SN)).Delete();
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
            string sql = "select max(RIGHT(PURORDERNO,LEN(PURORDERNO)-2)) from PurchaseOrderHeader where InputeDate>=CONVERT(varchar(100), GETDATE(), 23)";
            SQLHelper.DbHelperSQL.SetConnectionString("");
            string PurOrderNo = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
            if (string.IsNullOrEmpty(PurOrderNo))
            {
                sql = "Select CONVERT(varchar(100), GETDATE(), 112)";
                PurOrderNo = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() + "0001";
            }
            else
            {
                PurOrderNo = (Int64.Parse(PurOrderNo) + 1).ToString();
            }
            ArrayList al = new ArrayList();
            int row = 0;
            //,Properties ,PDate ,Purchaser ,Dept ,Project ,Provider ,ProviderName
            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
            foreach (int rowIndex in modifiedDict.Keys)
            {
                if (row == 0)
                {
                    sql = "INSERT INTO PurchaseOrderHeader (PurOrderNo,PurDate,Provider,ProviderID,JBRID,JBRName,ContactMan,Tel,Fax,JHDate,JHPlace,JSFS,ZZSFP,ProviderConfirm,ApproveID,CheckerID,MakerID,Inputer,InputeDate,PurPlanNo,SaleOrderNo) values('PU" + PurOrderNo + "','" + XDDate.Text + "','" + ddlProvider.Text + "','" + ddlProvider.Value + "','" + User.Identity.Name + "','" + GetChineseName() + "','" + txtContactMan.Text + "','" + txtTel.Text + "','" + txtFax.Text + "','" + JHdate.Text + "','锐麟厂','月结',0,'','','','" + User.Identity.Name + "','" + User.Identity.Name + "',getdate(),'" + Grid1.Rows[rowIndex].Values[3].ToString() + "','" + Grid1.Rows[rowIndex].Values[1].ToString() + "')";
                    al.Add(sql);
                    row = 1;
                }
                sql = "INSERT INTO PurchaseOrderDetail (FSN,PurOrderNo,PurPlanNo,SaleOrderNo,ProNo,ProName,ItemNo,ItemName,Spec,Quantity,Unit,Remark,Inputer,InputeDate,BomSN) select (select max(sn) from PurchaseOrderHeader where PurOrderNo='PU" + PurOrderNo + "'),'PU" + PurOrderNo + "',PurPlanNo,SaleOrderNo,prono,ProName ,ItemNo ,ItemName ,Spec ,Quantity,Unit,'','" + User.Identity.Name + "',getdate(),BomSN from PurchasePlan where sn=" + Grid1.DataKeys[rowIndex][0].ToString();
                al.Add(sql);
                log.Info("sqlpur::::" + sql);
                sql = "update PurchasePlan set State=1 where sn=" + Grid1.DataKeys[rowIndex][0].ToString();
                al.Add(sql);
                log.Info("sqlpur::::" + sql);
                sql = "update AllItem set SupplierId=(select sn from Provider where name='" + modifiedDict[rowIndex]["Provider"].ToString() + "') where itemno='" + Grid1.Rows[rowIndex].Values[6].ToString() + "'";
                al.Add(sql);
                log.Info("sqlAllItem::::" + sql);
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

    }
}