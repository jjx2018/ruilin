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
    public partial class magPurchaseOrder : PageBase
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
                return "OrderView";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                //txtOrderNo.Attributes.Add("onkeydown", "if (window.event.keyCode==13) window.event.keyCode=9;");

                // 删除选中行按钮
                //btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                //CheckPowerWithButton("OrderDelete", btnDelete);
                //ResolveDeleteButtonForGrid(btnDelete, Grid1);
                //CheckPowerWithButton("OrderDelete", btnDeleteSelected2);
                //ResolveDeleteButtonForGrid(btnDeleteSelected2, Grid2);

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
                else if (requestArg.Equals("RefreshGrid"))
                {
                    BindGrid();
                }
            }
            FileOper.delfile(Server.MapPath("~/pdf/"));
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
                var qqq = from a in appdb.purchaseorderHeader
                          select a;

                //在产品名称中搜索
                string searchText = ttbSearchMessage.Text;
                if (!String.IsNullOrEmpty(searchText))
                {
                    qqq = qqq.Where(u => u.PurPlanNo.Contains(searchText) || u.PurOrderNo.Contains(searchText));
                }
                //searchText为空或者选择“全部”，则列出全部
                else
                {

                }
                //qqq = qqq.Where(u => u.InputeDate >= dtstart && u.InputeDate <= dtend);


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = qqq.Count();// q.Count();

                // 排列和数据库分页
                qqq = SortAndPage<PurchaseOrderHeader>(qqq, Grid1);

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

                var inq = from a in appdb.purchaseorderDetail
                          join b in appdb.purchaseorderHeader on a.PurOrderNo equals b.PurOrderNo
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
                    inq = inq.Where(u => u.InputeDate >= DateFrom.SelectedDate);
                }
                if (DateTo.SelectedDate.HasValue)
                {
                    inq = inq.Where(u => u.InputeDate <= DateTo.SelectedDate);
                }


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = inq.Count();// q.Count();

                // 排列和数据库分页
                inq = SortAndPage<PurchaseOrderDetail>(inq, Grid2);

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
            //btnAddDetail.Enabled = true;
            //btnDeleteSelected2.Enabled = true;
            //btnBOM.Enabled = true;

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
            foreach (int rowIndex in deletedRows)
            {
                //int rowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                Grid1.Rows.RemoveAt(rowIndex);
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
        }

        protected void Grid2_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference("~/ruilin/OrderDetailEdit.aspx?id=" + Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString() + "&k=1", "产品详情"));
        }

        protected void Grid2_RowDataBound(object sender, GridRowEventArgs e)
        {

        }


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











        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid1.SelectedRowIndex == -1)
                {
                    Alert.Show("请先选择数据");
                    return;
                }
                string fname = htmltoPdfForSel("PurchaseOrder");

                string str = DateTime.Now.ToString("yyyyMMddHHmmsss");
                string s = @" parent.addExampleTab({
                id: " + str + @"+'_tab',
                iframeUrl: 'ruilin/showpdf.aspx?f=" + fname + @"',
                title:'打印采购单',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });";
                PageContext.RegisterStartupScript(s);
                //PageContext.RegisterStartupScript("parent.addExampleTab.apply(null, ['printer_two" + DateTime.Now.ToString("yyyyMMddHHmmsss") + "', basePath+'ruilin/showpdf.aspx?f=" + fname + "', '标签打印', basePath + 'res/icon/printer.png', false]);");
            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
        }


        #region
        #region
        //新增记录生成标签   flag=0  按名称打印  1  打印空标签  2打印选中标签
        // private string htmltoPdfForInsert(string modelfile, string flag)
        // {

        //     // 设置页面
        //     iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(GetPdfSize(int.Parse(ConfigurationManager.AppSettings["labelWidth"].ToString())), GetPdfSize(int.Parse(ConfigurationManager.AppSettings["labelHeight"].ToString())), 1);

        //     pageSize.BackgroundColor = new iTextSharp.text.BaseColor(0xFF, 0xFF, 0xFF);
        //     Document document = new Document(pageSize, 0, 0, 0, 0);
        //     //document.SetMargins(0f, 0f, 0f, 0f);
        //     document.SetPageSize(pageSize);
        //     string fname = DateTime.Now.ToString("yyyyMMddHHmmss");
        //     try
        //     {
        //         //Alert.Show(Server.MapPath("images\\test.pdf"));
        //         //创建文档

        //         PdfWriter pdfw = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/pdf/" + fname + ".pdf"), FileMode.Create));

        //         // 打开文档
        //         document.Open();
        //         BaseFont baseFont = BaseFont.CreateFont(
        //"C:\\WINDOWS\\FONTS\\simsun.ttc,0",
        //BaseFont.IDENTITY_H,
        //BaseFont.NOT_EMBEDDED);
        //         iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 60);

        //         //获取模板文件内容
        //         string strurlfile = HttpContext.Current.Server.MapPath("~/Model/" + modelfile + ".html");
        //         StringBuilder htmltext = new StringBuilder();
        //         using (StreamReader sr = new StreamReader(strurlfile))
        //         {
        //             String line;
        //             while ((line = sr.ReadLine()) != null)
        //             {
        //                 htmltext.Append(line);
        //             }
        //             sr.Close();
        //         }
        //         //写入pdf的字符串
        //         StringBuilder htmltopdftext = new StringBuilder();
        //         //htmltopdftext = htmltext;
        //         //doc.SetPageSize(PageSize.A4.Rotate());    //横向输出
        //         //MemoryStream outputStream = new MemoryStream();//要把PDF写到哪个串流  

        //         string[] strprosn = txtProsn.Text.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //         //PdfWriter writer = PdfWriter.GetInstance(document, outputStream);
        //         int pageindex = 1;
        //         // 添加文档内容
        //         for (int i = 0; i < int.Parse(txtPrintNum.Text.ToString()); i++)
        //         {
        //             htmltopdftext = htmltext;
        //             //BaseFont bfChinese = BaseFont.CreateFont("STSongStd-Light", "UniGB-UCS2-H", false);
        //             //iTextSharp.text.Font font = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
        //             string str = dpKDdate.SelectedDate.Value.ToString("yyyy年MM月dd日");
        //             htmltopdftext.Replace("$date", string.IsNullOrEmpty(str) ? DateTime.Now.ToString("yyyy年MM月dd日") : DateTime.Parse(str).ToString("yyyy年MM月dd日"));
        //             if (flag == "0")
        //             {
        //                 htmltopdftext.Replace("$pname", txtItemName.Text);
        //                 htmltopdftext.Replace("$spec", txtSpec.Text.Trim());
        //                 htmltopdftext.Replace("$caseno", txtCaseno.Text.Trim());
        //             }
        //             else if (flag == "1")
        //             {
        //                 htmltopdftext.Replace("$pname", "");
        //                 htmltopdftext.Replace("$spec", "");
        //                 htmltopdftext.Replace("$caseno", "");
        //             }
        //             str = "http://" + ConfigurationManager.AppSettings["urlapp"].ToString() + "/ydcc/logreg.htm?id=" + strprosn[i];
        //             System.Drawing.Image image = CreateQRCode(str, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 7, 1, 100, 1);
        //             string codejpg = HttpContext.Current.Server.MapPath("~/pdf/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
        //             image.Save(codejpg);
        //             htmltopdftext.Replace("@codejpg", codejpg);

        //             string pageHtml = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(htmltopdftext.ToString()));
        //             pageHtml = getImage(pageHtml);//输出html上的图片


        //             //指定文件预设开档时的缩放为100%  

        //             PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, document.PageSize.Height, 1f);

        //             byte[] data = Encoding.UTF8.GetBytes(pageHtml);//字串转成byte[]  
        //             MemoryStream msInput = new MemoryStream(data);

        //             //使用XMLWorkerHelper把Html parse到PDF档里  
        //             XMLWorkerHelper.GetInstance().ParseXHtml(pdfw, document, msInput, null, Encoding.UTF8, new AppBox.HtmlToPdfHelper.UnicodeFontFactory());
        //             //XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8);

        //             //将pdfDest设定的资料写到PDF档  
        //             PdfAction action = PdfAction.GotoLocalPage(pageindex, pdfDest, pdfw);
        //             pdfw.SetOpenAction(action);

        //             msInput.Close();
        //             //outputStream.Close();


        //             //table.AddCell("row 2; cell 2");
        //             //document.Add();

        //             document.NewPage();
        //             document.ResetPageCount();
        //             pageindex++;
        //         }
        //     }
        //     catch (DocumentException de)
        //     {
        //         Alert.Show(de.Message);
        //     }
        //     catch (Exception ioe)
        //     {
        //         Alert.Show(ioe.Message);
        //     }
        //     // 关闭文档
        //     document.Close();
        //     return fname + ".pdf";
        //     //downFile(Server.MapPath("~/images/" + fname + ".pdf"), fname + ".pdf");
        // }
        //选中记录生成标签

        #endregion
        private string htmltoPdfForSel(string modelfile)
        {

            // 设置页面大小
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(GetPdfSize(int.Parse(ConfigurationManager.AppSettings["A4Width"].ToString())), GetPdfSize(int.Parse(ConfigurationManager.AppSettings["A4Height"].ToString())), 1);

            pageSize.BackgroundColor = new iTextSharp.text.BaseColor(0xFF, 0xFF, 0xFF);
            Document document = new Document(pageSize, 0, 0, 0, 0);
            //document.SetMargins(0f, 0f, 0f, 0f);
            document.SetPageSize(pageSize);
            string fname = DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                #region//创建文档
                PdfWriter pdfw = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/pdf/" + fname + ".pdf"), FileMode.Create));

                // 打开文档
                document.Open();
                BaseFont baseFont = BaseFont.CreateFont(
       "C:\\WINDOWS\\FONTS\\simsun.ttc,0",
       BaseFont.IDENTITY_H,
       BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 60);
                #endregion

                #region //获取模板文件内容
                string strurlfile = HttpContext.Current.Server.MapPath("~/Model/" + modelfile + ".html");
                StringBuilder htmltext = new StringBuilder();
                using (StreamReader sr = new StreamReader(strurlfile))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        htmltext.Append(line);
                    }
                    sr.Close();
                }
                #endregion
                //写入pdf的字符串
                StringBuilder htmltopdftext = new StringBuilder();
                string modelstr = htmltext.ToString();
                //doc.SetPageSize(PageSize.A4.Rotate());    //横向输出
                //PDF页数
                int pageindex = 1;
                #region 将模板内容循环添加PDF中
                htmltopdftext.Clear();
                htmltopdftext.Append(modelstr);
                //替换头部
                string sql = "select * from PurchaseOrderHeader where sn=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString();
                SQLHelper.DbHelperSQL.SetConnectionString("");
                DataTable dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                if (dt != null && dt.Rows.Count > 0)
                {
                    htmltopdftext.Replace("$PurOrderNo$", dt.Rows[0]["PurOrderNo"].ToString());
                    htmltopdftext.Replace("$PurDate$", dt.Rows[0]["PurDate"].ToString());
                    htmltopdftext.Replace("$Provider$", dt.Rows[0]["Provider"].ToString());
                    htmltopdftext.Replace("$JBRName$", dt.Rows[0]["JBRName"].ToString());
                    htmltopdftext.Replace("$ContactMan$", dt.Rows[0]["ContactMan"].ToString());
                    htmltopdftext.Replace("$Tel$", dt.Rows[0]["Tel"].ToString());
                    htmltopdftext.Replace("$Fax$", dt.Rows[0]["Fax"].ToString());
                    htmltopdftext.Replace("$JHDate$", dt.Rows[0]["JHDate"].ToString());
                    htmltopdftext.Replace("$JHPlace$", dt.Rows[0]["JHPlace"].ToString());
                }
                string itemrows = "";
                float sum = 0;
                // 添加文档内容 
                for (int i = 0; i < Grid2.Rows.Count; i++)
                {
                    itemrows += "<tr><td class='b_b_r' style='padding:10px;'>" + (i + 1).ToString().PadLeft(3 - (i + 1).ToString().Length, '0') + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[6].ToString() + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[7].ToString() + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[8].ToString() + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[10].ToString() + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[9].ToString() + "</td><td class='b_b'>" + Grid2.Rows[i].Values[11].ToString() + "</td></tr>";
                    //if (i == Grid2.Rows.Count - 1)
                    //{
                    //    itemrows += "<tr><td class='b_b_r' style='padding:10px;'>" + (i + 1).ToString().PadLeft(3 - (i + 1).ToString().Length, '0') + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[6].ToString() + "</td><td class='b_r'>" + Grid2.Rows[i].Values[7].ToString() + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[8].ToString() + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[10].ToString() + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[9].ToString() + "</td><td class='b_b_r'>" + Grid2.Rows[i].Values[11].ToString() + "</td></tr>";

                    //}
                    //else
                    //{
                    //    itemrows += "<tr><td class='b_r' style='padding:10px;'>" + (i + 1).ToString().PadLeft(3 - (i + 1).ToString().Length, '0') + "</td><td class='b_r'>" + Grid2.Rows[i].Values[6].ToString() + "</td><td class='b_r'>" + Grid2.Rows[i].Values[7].ToString() + "</td><td class='b_r'>" + Grid2.Rows[i].Values[8].ToString() + "</td><td class='b_r'>" + Grid2.Rows[i].Values[10].ToString() + "</td><td class='b_r'>" + Grid2.Rows[i].Values[9].ToString() + "</td><td class='b_r'>" + Grid2.Rows[i].Values[11].ToString() + "</td></tr>";
                    //}
                    sum+=float.Parse( Grid2.Rows[i].Values[9].ToString());
                }
                itemrows += "<tr><td class='b_r' style='padding:10px;' colspan='5'>合计：</td><td class='b_r'>" + sum.ToString() + "</td><td class=''></td></tr>";
                htmltopdftext.Replace("$items", itemrows);
                //BaseFont bfChinese = BaseFont.CreateFont("STSongStd-Light", "UniGB-UCS2-H", false);
                //iTextSharp.text.Font font = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                #region //生成二维码
                string str = "http://" + ConfigurationManager.AppSettings["urlapp"].ToString() + "/ydcc/logreg.htm?id=" + Grid1.Rows[0].Values[1].ToString();
                //System.Drawing.Image image = CreateQRCode(str, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 7, 100,300, 1);

                System.Drawing.Bitmap bitmap = CreateQRCode(str, 200, 5);
                string codejpg = HttpContext.Current.Server.MapPath("~/pdf/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp");
                //image.Save(codejpg);
                bitmap.Save(codejpg);

                htmltopdftext.Replace("@codejpg", codejpg);
                string checkimg = HttpContext.Current.Server.MapPath("~/res/images/check.jpg");
                htmltopdftext.Replace("$checkimg$", checkimg);
                #endregion

                #region 处理模板中的图片路径问题
                string pageHtml = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(htmltopdftext.ToString()));
                pageHtml = getImage(pageHtml);//输出html上的图片
                #endregion

                #region 写入PDF文件
                //指定文件预设开档时的缩放为100%  
                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, document.PageSize.Height, 1f);

                byte[] data = Encoding.UTF8.GetBytes(pageHtml);//字串转成byte[]  
                MemoryStream msInput = new MemoryStream(data);

                //使用XMLWorkerHelper把Html parse到PDF档里  
                XMLWorkerHelper.GetInstance().ParseXHtml(pdfw, document, msInput, null, Encoding.UTF8, new AppBoxPro.HtmlToPdfHelper.UnicodeFontFactory());
                //将pdfDest设定的资料写到PDF档  
                PdfAction action = PdfAction.GotoLocalPage(pageindex, pdfDest, pdfw);
                pdfw.SetOpenAction(action);

                msInput.Close();
                #endregion

                //开始下一页
                //document.NewPage();
                //document.ResetPageCount();
                //pageindex++;

                #endregion
            }
            catch (DocumentException de)
            {
                Alert.Show(de.Message);
            }
            catch (Exception ioe)
            {
                Alert.Show(ioe.Message);
            }
            // 关闭文档
            document.Close();
            return fname + ".pdf";
            //downFile(Server.MapPath("~/images/" + fname + ".pdf"), fname + ".pdf");
        }
        public float GetPdfSize(float size)
        {

            return (size / 10) / (float)2.54 * (float)72;

        }
        //生成二维码
        public System.Drawing.Image CreateQRCode(string Content, QRCodeEncoder.ENCODE_MODE QRCodeEncodeMode, QRCodeEncoder.ERROR_CORRECTION QRCodeErrorCorrect, int QRCodeVersion, int QRCodeScale, int size, int border)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncodeMode;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeErrorCorrect;
            qrCodeEncoder.QRCodeScale = QRCodeScale;
            qrCodeEncoder.QRCodeVersion = QRCodeVersion;
            System.Drawing.Image image = qrCodeEncoder.Encode(Content, Encoding.Default);


            #region 根据设定的目标图片尺寸调整二维码QRCodeScale设置，并添加边框
            if (size > 0)
            {
                //当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
                #region 当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
                while (image.Width < size)
                {
                    qrCodeEncoder.QRCodeScale++;
                    System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content, Encoding.Default);
                    if (imageNew.Width < size)
                    {
                        image = new System.Drawing.Bitmap(imageNew);
                        imageNew.Dispose();
                        imageNew = null;
                    }
                    else
                    {
                        qrCodeEncoder.QRCodeScale--; //新尺寸未采用，恢复最终使用的尺寸
                        imageNew.Dispose();
                        imageNew = null;
                        break;
                    }
                }
                #endregion

                //当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
                #region 当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
                while (image.Width > size && qrCodeEncoder.QRCodeScale > 1)
                {
                    qrCodeEncoder.QRCodeScale--;
                    System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content, Encoding.Default);
                    image = new System.Drawing.Bitmap(imageNew);
                    imageNew.Dispose();
                    imageNew = null;
                    if (image.Width < size)
                    {
                        break;
                    }
                }
                #endregion

                //如果目标尺寸大于生成的图片尺寸，则为图片增加白边
                #region 如果目标尺寸大于生成的图片尺寸，则为图片增加白边
                if (image.Width <= size)
                {
                    //根据参数设置二维码图片白边的最小宽度
                    #region 根据参数设置二维码图片白边的最小宽度
                    if (border > 0)
                    {
                        while (image.Width <= size && size - image.Width < border * 2 && qrCodeEncoder.QRCodeScale > 1)
                        {
                            qrCodeEncoder.QRCodeScale--;
                            System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content, Encoding.Default);
                            image = new System.Drawing.Bitmap(imageNew);
                            imageNew.Dispose();
                            imageNew = null;
                        }
                    }
                    #endregion

                    //当目标图片尺寸大于二维码尺寸时，将二维码绘制在目标尺寸白色画布的中心位置
                    if (image.Width < size)
                    {
                        //新建空白绘图
                        System.Drawing.Bitmap panel = new System.Drawing.Bitmap(size, size);
                        System.Drawing.Graphics graphic0 = System.Drawing.Graphics.FromImage(panel);
                        int p_left = 0;
                        int p_top = 0;
                        if (image.Width <= size) //如果原图比目标形状宽
                        {
                            p_left = (size - image.Width) / 2;
                        }
                        if (image.Height <= size)
                        {
                            p_top = (size - image.Height) / 2;
                        }

                        //将生成的二维码图像粘贴至绘图的中心位置
                        graphic0.DrawImage(image, p_left, p_top, image.Width, image.Height);
                        image = new System.Drawing.Bitmap(panel);
                        panel.Dispose();
                        panel = null;
                        graphic0.Dispose();
                        graphic0 = null;
                    }
                }
                #endregion
            }
            #endregion
            return image;
        }

        private static bool IsTrue() // 在Image类别对图片进行缩放的时候,需要一个返回bool类别的委托 
        {
            return true;
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="Content">内容文本</param>
        /// <param name="size">图片尺寸（像素）</param>
        /// <param name="margin">图片白边（像素）</param>
        /// <returns></returns>
        public static Bitmap CreateQRCode(string Content, int size, int margin = 5)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            qrCodeEncoder.QRCodeScale = 5;
            qrCodeEncoder.QRCodeVersion = 0;

            try
            {
                System.Drawing.Image image = qrCodeEncoder.Encode(Content);
                int resWidth = size + 2 * margin;
                int resHeight = size + 2 * margin;
                // 核心就是这里新建一个bitmap对象然后将image在这里渲染
                Bitmap newBit = new Bitmap(resWidth, resHeight, PixelFormat.Format32bppRgb);
                Graphics gg = Graphics.FromImage(newBit);

                // 设置背景白色
                for (int y = 0; y < resWidth; y++)
                {
                    for (int x = 0; x < resHeight; x++)
                    {
                        newBit.SetPixel(x, y, Color.White);
                    }
                }

                // 设置黑色边框
                //for (int i = 0; i < resWidth; i++)
                //{
                //    newBit.SetPixel(i, 0, Color.Black);
                //    newBit.SetPixel(i, resWidth - 1, Color.Black);

                //}

                //for (int j = 0; j < resHeight; j++)
                //{
                //    newBit.SetPixel(0, j, Color.Black);
                //    newBit.SetPixel(resHeight - 1, j, Color.Black);

                //}
                gg.DrawImage(image, margin, margin, size, size);
                //newBit.Save(size.ToString() + ".png");
                return newBit;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>  
        /// 判断是否有乱码  
        /// </summary>  
        /// <param name="txt"></param>  
        /// <returns></returns>  
        public static bool isMessyCode(string txt)
        {
            var bytes = Encoding.UTF8.GetBytes(txt);            //239 191 189              
            for (var i = 0; i < bytes.Length; i++)
            {
                if (i < bytes.Length - 3)
                    if (bytes[i] == 239 && bytes[i + 1] == 191 && bytes[i + 2] == 189)
                    {
                        return true;
                    }
            }
            return false;
        }

        /// <summary>
        /// html图片相对路径替换成绝对路径
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string getImage(string input)
        {
            if (input == null)
                return string.Empty;
            string tempInput = input;
            string pattern = @"<img(.|\n)+?>";
            string src = string.Empty;
            HttpContext context = HttpContext.Current;

            //Change the relative URL's to absolute URL's for an image, if any in the HTML code.
            foreach (Match m in Regex.Matches(input, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline |
            RegexOptions.RightToLeft))
            {
                if (m.Success)
                {
                    string tempM = m.Value;
                    string pattern1 = "src=[\'|\"](.+?)[\'|\"]";
                    Regex reImg = new Regex(pattern1, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    Match mImg = reImg.Match(m.Value);

                    if (mImg.Success)
                    {
                        src = mImg.Value.ToLower().Replace("src=", "").Replace("\"", "").TrimStart('/').Replace("/", "\\");

                        if (src.ToLower().Contains("http://") == false)
                        {
                            src = "src=\"" + src + "\"";
                            try
                            {
                                tempM = tempM.Remove(mImg.Index, mImg.Length);
                                tempM = tempM.Insert(mImg.Index, src);

                                //insert new url img tag in whole html code
                                tempInput = tempInput.Remove(m.Index, m.Length);
                                tempInput = tempInput.Insert(m.Index, tempM);
                                UTF8Encoding utf8 = new UTF8Encoding();//此处需要转码一下
                                Byte[] encodedBytes = utf8.GetBytes(tempInput);
                                tempInput = utf8.GetString(encodedBytes);
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }
                }
            }
            return tempInput;
        }

        #endregion

        protected void btnPrintLab_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length <= 0)
            {
                Alert.Show("请先选择要打印的采购单");
                return;
            }
            //string fname = makepdf();
            string fname = printForHtmlByTable("labHeader", "PurchaseLabHtml");
            string str = DateTime.Now.ToString("yyyyMMddHHmmsss");
//            string s = @" parent.addExampleTab({
//                id: " + str + @"+'_tab',
//                iframeUrl: 'ruilin/showpdf.aspx?f=" + fname + @"',
//                title:'打印采购单标签',
//                iconFont: 'sign-in',
//                refreshWhenExist: true
//            });";
            string s = @" parent.addExampleTab({
                id: " + str + @"+'_tab',
                iframeUrl: 'pdf/" + fname + @"',
                title:'打印采购物料标签',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });";
            PageContext.RegisterStartupScript(s);
        }

        private string htmltoPdfForLab(string modelfile)
        {

            // 设置页面大小
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(GetPdfSize(int.Parse(ConfigurationManager.AppSettings["purlabelWidth"].ToString())), GetPdfSize(int.Parse(ConfigurationManager.AppSettings["purlabelHeight"].ToString())), 1);

            pageSize.BackgroundColor = new iTextSharp.text.BaseColor(0xFF, 0xFF, 0xFF);
            Document document = new Document(pageSize, 0, 0, 0, 0);
            //document.SetMargins(0f, 0f, 0f, 0f);
            document.SetPageSize(pageSize);
            string fname = DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                #region//创建文档
                PdfWriter pdfw = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/pdf/" + fname + ".pdf"), FileMode.Create));

                // 打开文档
                document.Open();
                BaseFont baseFont = BaseFont.CreateFont(
       "C:\\WINDOWS\\FONTS\\simsun.ttc,0",
       BaseFont.IDENTITY_H,
       BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 60);
                #endregion

                #region //获取模板文件内容
                string strurlfile = HttpContext.Current.Server.MapPath("~/Model/" + modelfile + ".html");
                StringBuilder htmltext = new StringBuilder();
                using (StreamReader sr = new StreamReader(strurlfile))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        htmltext.Append(line);
                    }
                    sr.Close();
                }
                #endregion
                //写入pdf的字符串
                StringBuilder htmltopdftext = new StringBuilder();
                string modelstr = htmltext.ToString();
                //doc.SetPageSize(PageSize.A4.Rotate());    //横向输出
                //PDF页数
                int pageindex = 1;
                #region 将模板内容循环添加PDF中
                string sql = "select * from  PurchaseOrderDetail a,PurchaseOrderHeader b, bomdetail c,bomheader d  where c.itemno=a.itemno and c.fsn=d.sn and d.orderno=b.saleorderno and a.fsn=b.sn and  a.purorderno='" + Grid1.Rows[Grid1.SelectedRowIndex].Values[2].ToString() + "' and  b.purorderno='" + Grid1.Rows[Grid1.SelectedRowIndex].Values[2].ToString() + "'";
                SQLHelper.DbHelperSQL.SetConnectionString("");
                DataTable dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                // 添加文档内容 
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    htmltopdftext.Clear();
                    htmltopdftext.Append(modelstr);
                    //BaseFont bfChinese = BaseFont.CreateFont("STSongStd-Light", "UniGB-UCS2-H", false);
                    //iTextSharp.text.Font font = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    #region //替换模板内容
                    //string str = Grid1.Rows[i].Values[8].ToString();
                    htmltopdftext.Replace("$orderno", dt.Rows[i]["purorderno"].ToString());
                    htmltopdftext.Replace("$sdate", DateTime.Now.ToString("yyyyMMdd"));
                    htmltopdftext.Replace("$remark", dt.Rows[i]["remark"].ToString());
                    htmltopdftext.Replace("$itemno", dt.Rows[i]["itemno"].ToString());
                    htmltopdftext.Replace("$provider", dt.Rows[i]["provider"].ToString());
                    htmltopdftext.Replace("$itemname", dt.Rows[i]["itemname"].ToString());
                    htmltopdftext.Replace("$spec", dt.Rows[i]["spec"].ToString());
                    htmltopdftext.Replace("$quantity", dt.Rows[i]["quantity"].ToString() + dt.Rows[i]["unit"].ToString());
                    htmltopdftext.Replace("$material", dt.Rows[i]["material"].ToString());
                    htmltopdftext.Replace("$color", dt.Rows[i]["surfacedeal"].ToString());
                    htmltopdftext.Replace("$caseno","");

                    //str = "http://" + ConfigurationManager.AppSettings["urlapp"].ToString() + "/ydcc/logreg.htm?id=" + Grid1.Rows[i].Values[1].ToString();
                    //System.Drawing.Image image = CreateQRCode(str, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 7, 100,300, 1);

                    System.Drawing.Bitmap bitmap = BarcodeHelper.Generate2(dt.Rows[i]["purorderno"].ToString(), 150, 50);// CreateQRCode(str, 200, 5);
                    string codejpg = HttpContext.Current.Server.MapPath("~/pdf/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp");
                    //image.Save(codejpg);
                    bitmap.Save(codejpg);
                    codejpg = HttpContext.Current.Server.MapPath("~/Model/timg.jpg");
                    htmltopdftext.Replace("@codejpg", codejpg);
                    #endregion

                    #region 处理模板中的图片路径问题
                    string pageHtml = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(htmltopdftext.ToString()));
                    pageHtml = getImage(pageHtml);//输出html上的图片
                    #endregion

                    #region 写入PDF文件
                    //指定文件预设开档时的缩放为100%  
                    PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, document.PageSize.Height, 1f);

                    byte[] data = Encoding.UTF8.GetBytes(pageHtml);//字串转成byte[]  
                    MemoryStream msInput = new MemoryStream(data);

                    //使用XMLWorkerHelper把Html parse到PDF档里  
                    XMLWorkerHelper.GetInstance().ParseXHtml(pdfw, document, msInput, null, Encoding.UTF8, new AppBoxPro.HtmlToPdfHelper.UnicodeFontFactory());
                    //将pdfDest设定的资料写到PDF档  
                    PdfAction action = PdfAction.GotoLocalPage(pageindex, pdfDest, pdfw);
                    pdfw.SetOpenAction(action);

                    msInput.Close();
                    #endregion

                    document.NewPage();
                    document.ResetPageCount();
                    pageindex++;
                }
                #endregion
            }
            catch (DocumentException de)
            {
                Alert.Show(de.ToString());
            }
            catch (Exception ioe)
            {
                Alert.Show(ioe.ToString());
            }
            // 关闭文档
            document.Close();
            return fname + ".pdf";
            //downFile(Server.MapPath("~/images/" + fname + ".pdf"), fname + ".pdf");
        }

        protected void btnSelPrintLab_Click(object sender, EventArgs e)
        {
            if (Grid2.SelectedRowIndexArray.Length <= 0)
            {
                Alert.Show("请先选择要打印的采购单");
                return;
            }
            //string fname = makepdf();
            //string fname = htmltoPdfForSelLab("PurchaseLab");
            string fname = printForHtmlByGrid("labHeader", "PurchaseLabHtml");
            string str = DateTime.Now.ToString("yyyyMMddHHmmsss");
//            string s = @" parent.addExampleTab({
//                id: " + str + @"+'_tab',
//                iframeUrl: 'ruilin/showpdf.aspx?f=" + fname + @"',
//                title:'打印采购货物标签',
//                iconFont: 'sign-in',
//                refreshWhenExist: true
//            });";


            string s = @" parent.addExampleTab({
                id: " + str + @"+'_tab',
                iframeUrl: 'pdf/" + fname + @"',
                title:'打印采购物料标签',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });";
            PageContext.RegisterStartupScript(s);//"window.open('../pdf/'"+fname+",'newindow','height=600,width=900,top=0,left=0,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no');"
            //PageContext.RegisterStartupScript(Window1.GetShowReference("~/pdf/" + fname, "打印采购货物标签"));
            
             
        }
        string gethtml()
        {
            string html = @"<table style='width:100%; border:3px solid #000;' cellpadding='0' cellspacing='0'>
        <tr style=' font-size: 25px; font-weight:600; '>
            <td colspan='2'>
                <table style='width:100%; border:0px solid #000;' cellpadding='0' cellspacing='0'>
                    <tr style=' font-size: 25px; font-weight:600; '>
                        <td style='border-bottom: solid 3px #000; height: 40px; border-right: solid 3px #000; text-align: center; width: 10%;'>
                            单号
                        </td>
                        <td style='border-bottom: solid 3px #000; height: 40px; border-right: solid 3px #000; width: 60%; '>
                            $orderno
                        </td>
                        <td style='border-bottom: solid 3px #000; height: 40px; width: 30%; '>
                            $sdate
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style=' font-size: 25px; font-weight: 600; height: 40px; '>
            <td style='border-bottom: solid 3px #000; border-right: solid 3px #000; text-align: center; width: 10%; '>
                备注
            </td>
            <td style='border-bottom:solid 3px black; width:90%;'>
                $remark
            </td>
        </tr>
        <tr style=' font-size: 25px; font-weight: 600; height: 40px; '>
            <td colspan='2' style='border-bottom: solid 0px #000;'>
                <table style='width:100%; border:0px solid #000;' cellpadding='0' cellspacing='0'>
                    <tr style=' font-size: 25px; font-weight:600; '>
                        <td style='border-bottom: solid 3px #000; height: 40px; border-right: solid 3px #000; text-align: center; width: 15%; '>
                            物料编号
                        </td>
                        <td style='border-bottom: solid 3px #000; height: 40px; border-right: solid 3px #000; width:35%;'>
                            $itemno
                        </td>
                        <td style='border-bottom: solid 3px #000; height: 40px; border-right: solid 3px #000; width: 10%;text-align:center; '>
                            供应商
                        </td>
                        <td style='border-bottom: solid 3px #000; height: 40px; width: 40%'>
                            $provider
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr style=' font-size: 25px;  font-weight:600;'>
            <td colspan='2'>
                <table style='width:100%; border:0px solid #000;' cellpadding='0' cellspacing='0'>
                    <tr style=' font-size: 25px; font-weight:600; '>
                        <td style='border-bottom: solid 3px #000; height: 40px; border-right: solid 3px #000; text-align:center; width:15%; '>
                            物料名称
                        </td>
                        <td style='border-bottom: solid 3px #000; height: 40px;width:85%; '>
                            $itemname
                        </td>
                    </tr>
                    <tr style=' font-size: 25px; font-weight:600; '>
                        <td style='border-bottom: solid 0px #000; height: 40px; border-right: solid 3px #000; text-align: center; width: 15%; '>
                            物料规格
                        </td>
                        <td style='border-bottom: solid 0px #000; height: 40px; width: 85%;'>
                            $spec
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td colspan='2'>
                <table style='width: 100%; border-top: 3px solid #000; border-bottom: 3px solid #000; ' cellpadding='0' cellspacing='0'>
                    <tr style=' font-size: 25px; font-weight:600; '>
                        <td style='border-bottom: solid 3px #000; height: 40px; border-right: solid 3px #000;text-align:center;width:15%;'>
                            数量
                        </td>
                        <td style='border-bottom: solid 3px #000; height: 40px; border-right: solid 3px #000;text-align:center;width:35%; '>
                            $quantity
                        </td>
                        <td style='border-bottom: solid 3px #000; height: 40px; border-right: solid 3px #000; text-align: center; width: 15%; '>
                            材质
                        </td>
                        <td style='border-bottom: solid 3px #000; height: 40px; width: 35%;'>
                            $material
                        </td>
                    </tr>
                    <tr style=' font-size: 25px; font-weight:600; '>
                        <td style='border-bottom: solid 0px #000; height: 40px; border-right: solid 3px #000; text-align: center; width: 15%; '>
                            颜色
                        </td>
                        <td style='border-bottom: solid 0px #000; height: 40px; border-right: solid 3px #000; width: 35%; '>
                            $color
                        </td>
                        <td style='border-bottom: solid 0px #000; height: 40px; border-right: solid 3px #000; text-align: center; width: 15%; '>
                            库位
                        </td>
                        <td style='border-bottom: solid 0px #000; height: 40px; width: 35%;'>
                            $caseno
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr style=' font-size: 25px;  font-weight:600;'>
            <td colspan='2' class='barcode'>
                $code
            </td>
        </tr>
        <tr style=' font-size: 25px;  font-weight:600;'>
            <td colspan='2' style='text-align:center;height:20px;'>
                $codevalue
            </td>
        </tr>
        <tr style=' font-size: 25px;  font-weight:600;display:none;'>
            <td colspan='2' style='text-align:center;height:80px;'>
                <img src='@codejpg' style='width:500px;height:70px;' />
            </td>
        </tr>
    </table>";
            return html;
        }
        string printForHtmlByGrid(string modelheadfile, string modelcontentfile)
        {
            string fname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            #region //获取模板文件内容
            string strurlfile = HttpContext.Current.Server.MapPath("~/Model/" + modelheadfile + ".html");
            StringBuilder htmltext = new StringBuilder(FileOper.getFileContent(strurlfile));
            int pagecount = Grid2.SelectedRowIndexArray.Length;
            htmltext.Replace("$pagecount", "张数：" + pagecount.ToString());

            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/pdf/" + fname), FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(htmltext.ToString());
            //获取模板内容
            StringBuilder htmlcontent = new StringBuilder(FileOper.getFileContent(HttpContext.Current.Server.MapPath("~/Model/" + modelcontentfile + ".html")));
            #endregion

            try
            {
                #region 将模板内容循环添加PDF中
                SQLHelper.DbHelperSQL.SetConnectionString("");
                string sql = "";
                int pageindex = 1;
                
               
                // 添加文档内容 
                foreach (int i in Grid2.SelectedRowIndexArray)
                {
                    htmltext.Clear();
                    
                    if(pageindex!=1)
                    {
                        htmltext.Append("<div class='page'><br></div>");
                    }
                    htmltext.Append(htmlcontent.ToString());
                    #region //替换模板内容
                    htmltext.Replace("$orderno", Grid2.Rows[i].Values[2].ToString());
                    htmltext.Replace("$sdate", DateTime.Now.ToString("yyyyMMdd"));
                    htmltext.Replace("$remark", Grid2.Rows[i].Values[11].ToString());
                    htmltext.Replace("$itemno", Grid2.Rows[i].Values[6].ToString());
                    htmltext.Replace("$provider", Grid1.Rows[Grid1.SelectedRowIndex].Values[5].ToString());
                    htmltext.Replace("$itemname", Grid2.Rows[i].Values[7].ToString());
                    htmltext.Replace("$spec", Grid2.Rows[i].Values[8].ToString());
                    htmltext.Replace("$quantity", Grid2.Rows[i].Values[9].ToString() + Grid2.Rows[i].Values[10].ToString());
                    sql = "select material from bomdetail c,bomheader d where c.fsn=d.sn and c.itemno='" + Grid2.Rows[i].Values[6].ToString() + "' and d.prono='" + Grid2.Rows[i].Values[4].ToString() + "' and d.orderno='" + Grid2.Rows[i].Values[3].ToString() + "'";
                    htmltext.Replace("$material", SQLHelper.DbHelperSQL.GetSingle(sql, 30));
                    sql = "select surfacedeal from bomdetail c,bomheader d where c.fsn=d.sn and c.itemno='" + Grid2.Rows[i].Values[6].ToString() + "' and d.prono='" + Grid2.Rows[i].Values[4].ToString() + "' and d.orderno='" + Grid2.Rows[i].Values[3].ToString() + "'";

                    htmltext.Replace("$codevalue", Grid2.Rows[i].Values[2].ToString());


                    htmltext.Replace("$color", SQLHelper.DbHelperSQL.GetSingle(sql, 30));
                    htmltext.Replace("$caseno", "");
 

                    IDAutomation.NetAssembly.FontEncoder fe = new IDAutomation.NetAssembly.FontEncoder();
                    htmltext.Replace("$code", fe.Code128(Grid2.Rows[i].Values[2].ToString(), 0, false));
                    fe = null;                    
                    #endregion                 
                    sw.WriteLine(htmltext);
                    pageindex++;
                }
                #endregion
                sw.WriteLine("</body></html>");
            }
            catch(Exception ee)
            {
                Alert.Show(ee.ToString());
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
            return fname ;
        }

        string printForHtmlByTable(string modelheadfile, string modelcontentfile)
        {
            string fname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            #region //获取模板文件内容
            string strurlfile = HttpContext.Current.Server.MapPath("~/Model/" + modelheadfile + ".html");
            StringBuilder htmltext = new StringBuilder(FileOper.getFileContent(strurlfile));
            string sql = "select * from  PurchaseOrderDetail a,PurchaseOrderHeader b, bomdetail c,bomheader d  where c.itemno=a.itemno and c.fsn=d.sn and d.orderno=b.saleorderno and a.fsn=b.sn and  a.purorderno='" + Grid1.Rows[Grid1.SelectedRowIndex].Values[2].ToString() + "' and  b.purorderno='" + Grid1.Rows[Grid1.SelectedRowIndex].Values[2].ToString() + "'";
            SQLHelper.DbHelperSQL.SetConnectionString("");
            DataTable dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
            int pagecount =dt.Rows.Count;
            htmltext.Replace("$pagecount", "张数：" + pagecount.ToString());

            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/pdf/" + fname), FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(htmltext.ToString());
            //获取模板内容
            StringBuilder htmlcontent = new StringBuilder(FileOper.getFileContent(HttpContext.Current.Server.MapPath("~/Model/" + modelcontentfile + ".html")));
            #endregion

            try
            {
                #region 将模板内容循环添加PDF中
                
                int pageindex = 1;

               
                // 添加文档内容 
                for (int i = 0; i < pagecount; i++)
                {
                    htmltext.Clear();

                    if (pageindex != 1)
                    {
                        htmltext.Append("<div class='page'><br></div>");
                    }
                    htmltext.Append(htmlcontent.ToString());
                    #region //替换模板内容
                    htmltext.Replace("$orderno", dt.Rows[i]["purorderno"].ToString());
                    htmltext.Replace("$sdate", DateTime.Now.ToString("yyyyMMdd"));
                    htmltext.Replace("$remark", dt.Rows[i]["remark"].ToString());
                    htmltext.Replace("$itemno", dt.Rows[i]["itemno"].ToString());
                    htmltext.Replace("$provider", dt.Rows[i]["provider"].ToString());
                    htmltext.Replace("$itemname", dt.Rows[i]["itemname"].ToString());
                    htmltext.Replace("$spec", dt.Rows[i]["spec"].ToString());
                    htmltext.Replace("$quantity", dt.Rows[i]["quantity"].ToString() + dt.Rows[i]["unit"].ToString());
                    htmltext.Replace("$material", dt.Rows[i]["material"].ToString());
                    htmltext.Replace("$color", dt.Rows[i]["surfacedeal"].ToString());
                    htmltext.Replace("$caseno", "");
                    htmltext.Replace("$codevalue", dt.Rows[i]["purorderno"].ToString());

                    IDAutomation.NetAssembly.FontEncoder fe = new IDAutomation.NetAssembly.FontEncoder();
                    htmltext.Replace("$code", fe.Code128(dt.Rows[i]["purorderno"].ToString(), 0, false));
                    fe = null;
                    #endregion
                    sw.WriteLine(htmltext);
                    pageindex++;
                }
                #endregion
                sw.WriteLine("</body></html>");
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

        private string htmltoPdfForSelLab(string modelfile)
        {

            // 设置页面大小
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(GetPdfSize(int.Parse(ConfigurationManager.AppSettings["purlabelWidth"].ToString())), GetPdfSize(int.Parse(ConfigurationManager.AppSettings["purlabelHeight"].ToString())), 1);

            pageSize.BackgroundColor = new iTextSharp.text.BaseColor(0xFF, 0xFF, 0xFF);
            Document document = new Document(pageSize, 0, 0, 0, 0);
            //document.SetMargins(0f, 0f, 0f, 0f);
            document.SetPageSize(pageSize);
            string fname = DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                #region//创建文档
                PdfWriter pdfw = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/pdf/" + fname + ".pdf"), FileMode.Create));

                // 打开文档
                document.Open();
                BaseFont baseFont = BaseFont.CreateFont(
       "C:\\WINDOWS\\FONTS\\simsun.ttc,0",
       BaseFont.IDENTITY_H,
       BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 60);
                #endregion

                #region //获取模板文件内容
                string strurlfile = HttpContext.Current.Server.MapPath("~/Model/" + modelfile + ".html");
                StringBuilder htmltext = new StringBuilder();
                using (StreamReader sr = new StreamReader(strurlfile))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        htmltext.Append(line);
                    }
                    sr.Close();
                }
                #endregion
                //写入pdf的字符串
                StringBuilder htmltopdftext = new StringBuilder();
                string modelstr = htmltext.ToString();
                //doc.SetPageSize(PageSize.A4.Rotate());    //横向输出
                //PDF页数
                int pageindex = 1;
                #region 将模板内容循环添加PDF中
                SQLHelper.DbHelperSQL.SetConnectionString("");
                string sql = "";
                // 添加文档内容 
                foreach (int i in Grid2.SelectedRowIndexArray)
                {
                    htmltopdftext.Clear();
                    htmltopdftext.Append(modelstr);
                    //BaseFont bfChinese = BaseFont.CreateFont("STSongStd-Light", "UniGB-UCS2-H", false);
                    //iTextSharp.text.Font font = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    #region //替换模板内容
                    //string str = Grid1.Rows[i].Values[8].ToString();
                    htmltopdftext.Replace("$orderno", Grid2.Rows[i].Values[2].ToString());
                    htmltopdftext.Replace("$sdate", DateTime.Now.ToString("yyyyMMdd"));
                    htmltopdftext.Replace("$remark", Grid2.Rows[i].Values[11].ToString());
                    htmltopdftext.Replace("$itemno", Grid2.Rows[i].Values[6].ToString());
                    htmltopdftext.Replace("$provider", Grid1.Rows[Grid1.SelectedRowIndex].Values[5].ToString());
                    htmltopdftext.Replace("$itemname", Grid2.Rows[i].Values[7].ToString());
                    htmltopdftext.Replace("$spec", Grid2.Rows[i].Values[8].ToString());
                    htmltopdftext.Replace("$quantity", Grid2.Rows[i].Values[9].ToString() + Grid2.Rows[i].Values[10].ToString());
                    sql = "select material from bomdetail c,bomheader d where c.fsn=d.sn and c.itemno='" + Grid2.Rows[i].Values[6].ToString() + "' and d.prono='" + Grid2.Rows[i].Values[4].ToString() + "' and d.orderno='" + Grid2.Rows[i].Values[3].ToString() + "'";
                    htmltopdftext.Replace("$material", SQLHelper.DbHelperSQL.GetSingle(sql,30));
                    sql = "select surfacedeal from bomdetail c,bomheader d where c.fsn=d.sn and c.itemno='" + Grid2.Rows[i].Values[6].ToString() + "' and d.prono='" + Grid2.Rows[i].Values[4].ToString() + "' and d.orderno='" + Grid2.Rows[i].Values[3].ToString() + "'";

                    htmltopdftext.Replace("$codevalue", Grid2.Rows[i].Values[2].ToString());

                    
                    htmltopdftext.Replace("$color", SQLHelper.DbHelperSQL.GetSingle(sql, 30));
                    htmltopdftext.Replace("$caseno", "");
                   
                    //str = "http://" + ConfigurationManager.AppSettings["urlapp"].ToString() + "/ydcc/logreg.htm?id=" + Grid1.Rows[i].Values[1].ToString();
                    //System.Drawing.Image image = CreateQRCode(str, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 7, 100,300, 1);

                    //System.Drawing.Bitmap bitmap = BarcodeHelper.Generate2(Grid2.Rows[i].Values[2].ToString(),10800, 900);// CreateQRCode(str, 200, 5);
                    System.Drawing.Image img;
                   
                    string codejpg = HttpContext.Current.Server.MapPath("~/pdf/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp");
                    BarcodeHelper.GetBarcode(600, 1800, BarcodeLib.TYPE.CODE128, Grid2.Rows[i].Values[2].ToString(), out img, codejpg);
                    //image.Save(codejpg);
                    //bitmap.Save(codejpg);

                    htmltopdftext.Replace("@codejpg", codejpg);



                    IDAutomation.NetAssembly.FontEncoder fe = new IDAutomation.NetAssembly.FontEncoder();
                    htmltopdftext.Replace("$code", fe.Code128(Grid2.Rows[i].Values[2].ToString(), 0, false));
                    fe = null;

                    FileOper.createFile(HttpContext.Current.Server.MapPath("~/pdf/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html"), htmltopdftext.ToString());
                    #endregion

                    #region 处理模板中的图片路径问题
                    string pageHtml = htmltopdftext.ToString();// Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(htmltopdftext.ToString()));
                    pageHtml = getImage(pageHtml);//输出html上的图片
                    #endregion

                    #region 写入PDF文件
                    //指定文件预设开档时的缩放为100%  
                    PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, document.PageSize.Height, 1f);

                    byte[] data = Encoding.UTF8.GetBytes(pageHtml);//字串转成byte[]  
                    MemoryStream msInput = new MemoryStream(data);

                    //使用XMLWorkerHelper把Html parse到PDF档里  
                    XMLWorkerHelper.GetInstance().ParseXHtml(pdfw, document, msInput, null, Encoding.UTF8, new AppBoxPro.HtmlToPdfHelper.UnicodeFontFactory());
                    //将pdfDest设定的资料写到PDF档  
                    PdfAction action = PdfAction.GotoLocalPage(pageindex, pdfDest, pdfw);
                    pdfw.SetOpenAction(action);

                    msInput.Close();
                    #endregion

                    document.NewPage();
                    document.ResetPageCount();
                    pageindex++;
                }
                #endregion
            }
            catch (DocumentException de)
            {
                Alert.Show(de.ToString());
            }
            catch (Exception ioe)
            {
                Alert.Show(ioe.ToString());
            }
            finally
            {
                document.Close();
            }
            // 关闭文档
            
            return fname + ".pdf";
            //downFile(Server.MapPath("~/images/" + fname + ".pdf"), fname + ".pdf");
        }












    }
}