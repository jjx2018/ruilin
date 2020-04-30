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

namespace AppBoxPro.SendOutMag
{
    public partial class SendOutOrderHeader : PageBase
    {
        static Hashtable htClickColsName = new Hashtable();

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
                return "SendOutOrderView";
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
                var q = from a in appdb.sendoutprocessheader
                          select a;

                //在产品名称中搜索
                string searchText = ttbSearchMessage.Text;
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.SendOutOrderNo.Contains(searchText) || u.SendOutPlanNo.Contains(searchText)|| u.SaleOrderNo.Contains(searchText));
                }
                //searchText为空或者选择“全部”，则列出全部
                else
                {

                }

                foreach (DictionaryEntry de in htClickColsName)
                {
                    switch (de.Key.ToString())
                    {
                        case "SaleOrderNo":
                            q = q.Where(u => u.SaleOrderNo == de.Value.ToString());
                            break;
                        case "SendOutPlanNo":
                            q = q.Where(u => u.SendOutPlanNo == de.Value.ToString());
                            break;
                        case "SendOutOrderNo":
                            q = q.Where(u => u.SendOutOrderNo == de.Value.ToString());
                            break;
                        case "SendOutDate":
                            q = q.Where(u => u.SendOutDate.Value.ToString("yyyy-MM-dd") == de.Value.ToString());
                            break;
                        case "Provider":
                            q = q.Where(u => u.Provider == de.Value.ToString());
                            break;
                        case "JBRName":
                            q = q.Where(u => u.JBRName == de.Value.ToString());
                            break;
                        
                    }
                }


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();// q.Count();

                // 排列和数据库分页
                q = SortAndPage<SendOutProcessHeader>(q, Grid1);

                Grid1.DataSource = q;// itemq.Take(2);// q;
                Grid1.DataBind();
            }
        }
         
        #endregion

        #region Events


        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("InstockEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("SendOutOrderDelete", Grid1, "deleteField");
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
                // 在操作之前进行权限检查
                if (!CheckPower("SendOutOrderDelete"))
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
            if (!CheckPower("SendOutOrderDelete"))
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
                    Alert.Show("请选择要打印的委外单");
                    return;
                }
                string fname = printOrderForHtmlBySQL("PrinterSet", "SendOutOrderHead", "SendOutOrderContent", "SendOutOrderFoot");

                string str = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                string s = @" parent.addExampleTab({
                id: " + str + @"+'_tab',
                iframeUrl: 'pdf/" + fname + @"',
                title:'打印委外单',
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
        string printOrderForHtmlBySQL(string printsetfile, string htmlheadfile, string htmlcontentfile, string htmlfootfile)
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
            StringBuilder htmlcontenttext = new StringBuilder(FileOper.getFileContent(HttpContext.Current.Server.MapPath("~/Model/" + htmlcontentfile + ".html")));
            StringBuilder htmlfoottext = new StringBuilder(FileOper.getFileContent(HttpContext.Current.Server.MapPath("~/Model/" + htmlfootfile + ".html")));

            #endregion

            try
            {
                #region 将模板内容循环添加PDF中
                SQLHelper.DbHelperSQL.SetConnectionString("");

                int pageindex = 1;
                //替换头部
                string sql = "select * from SendOutProcessHeader where sn=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString();
                SQLHelper.DbHelperSQL.SetConnectionString("");
                DataTable dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //IDAutomation.NetAssembly.FontEncoder fe = new IDAutomation.NetAssembly.FontEncoder();
                    //htmlheadtext.Replace("$code$", fe.Code128(dt.Rows[0]["CPOrderNo"].ToString(), 0, false));
                    //fe = null;
                    htmlheadtext.Replace("$SendOutOrderNo$", dt.Rows[0]["SendOutOrderNo"].ToString());
                    htmlheadtext.Replace("$SendOutDate$", DateTime.Parse(dt.Rows[0]["SendOutDate"].ToString()).ToString("yyyy年M月d日"));
                    htmlheadtext.Replace("$Provider$", dt.Rows[0]["Provider"].ToString());
                    htmlheadtext.Replace("$JBR$", dt.Rows[0]["JBRName"].ToString());
                    htmlheadtext.Replace("$Provideraddr$", SQLHelper.DbHelperSQL.GetSingle("select top 1 address from provider where sn='" + dt.Rows[0]["providerid"].ToString() + "' or name='" + dt.Rows[0]["provider"].ToString() + "'", 30));
                    htmlheadtext.Replace("$Checkcode$", "审核");
                    htmlheadtext.Replace("$Remark$", dt.Rows[0]["Remark"].ToString());
                }
                sw.WriteLine(htmlheadtext);
                StringBuilder str = new StringBuilder();
                string itemrows = "";
                float sum = 0;
                //for (int k = 0; k < labcount; k++)
                //{
                str.Clear();

                if (pageindex != 1)
                {
                    str.Append("<div class='page'><br></div>");
                }
                str.Append(htmlcontenttext.ToString());
                sql = "select * from  dbo.SendOutProcessDetail where fsn=" + dt.Rows[0]["SN"].ToString();
                dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                // 添加文档内容 
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    itemrows += "<tr><td class='b_b_r' style='padding:10px;'>" + (i + 1).ToString().PadLeft(3 - (i + 1).ToString().Length, '0') + "</td><td class='b_b_r'>" + dt.Rows[i]["itemno"].ToString() + "</td><td class='b_b_r'>" + dt.Rows[i]["itemname"].ToString() + "</td><td class='b_b_r'>" + dt.Rows[i]["spec"].ToString() + "</td><td class='b_b_r'>" + dt.Rows[i]["unit"].ToString() + "</td><td class='b_b_r'>" + dt.Rows[i]["quantity"].ToString() + "</td><td class='b_b_r'></td><td class='b_b'></td></tr>";
                    sum += float.Parse(dt.Rows[i]["quantity"].ToString());
                }
                itemrows += "<tr><td class='b_r' style='padding:10px;' colspan='5'>合计：</td><td class='b_r'>" + sum.ToString() + "</td><td class=''></td></tr>";
                str.Replace("$items$", itemrows);
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


 
        protected void btnPrintLab_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length <= 0)
            {
                Alert.Show("请先选择要打印的委外单");
                return;
            }
            //string fname = makepdf();
            //string fname = htmltoPdfForLab("PurchaseLab");
            string fname = printLabForHtmlByTable("labHeader", "SendOutLabHtml");
            string str = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            //            string s = @" parent.addExampleTab({
            //                id: " + str + @"+'_tab',
            //                iframeUrl: 'ruilin/showpdf.aspx?f=" + fname + @"',
            //                title:'打印委外单标签',
            //                iconFont: 'sign-in',
            //                refreshWhenExist: true
            //            });";
            string s = @" parent.addExampleTab({
                id: " + str + @"+'_tab',
                iframeUrl: 'pdf/" + fname + @"',
                title:'打印委外物料标签',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });";
            PageContext.RegisterStartupScript(s);
        }
        string printLabForHtmlByTable(string modelheadfile, string modelcontentfile)
        {
            string fname = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".html";
            #region //获取模板文件内容
            string strurlfile = HttpContext.Current.Server.MapPath("~/Model/" + modelheadfile + ".html");
            StringBuilder htmltext = new StringBuilder(FileOper.getFileContent(strurlfile));
            //string sql = "select * from  ProduceOrderDetail a,ProduceOrderHeader b, bomdetail c,bomheader d  where c.itemno=a.itemno and c.fsn=d.sn and d.orderno=b.saleorderno and a.fsn=b.sn and  a.proorderno='" + Grid1.Rows[Grid1.SelectedRowIndex].Values[2].ToString() + "' and  b.proorderno='" + Grid1.Rows[Grid1.SelectedRowIndex].Values[2].ToString() + "'";
            string sql = " select a.SaleOrderNo,a.ProNo,a.ProName, a.SendOutOrderNo,a.remark,a.itemno,a.itemname,a.spec,a.quantity,a.unit,c.material,c.surfacedeal,c.storehouse,BarCode  from  SendOutProcessDetail a join SendOutProcessHeader b on b.SN=a.FSN  LEFT JOIN BomDetail c on a.bomsn = c.SN   where a.fsn = " + Grid1.DataKeys[Grid1.SelectedRowIndex][0].ToString() + "";
            //Alert.Show(sql);
            //return "";
            SQLHelper.DbHelperSQL.SetConnectionString("");
            DataTable dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
            int pagecount = dt.Rows.Count;
            int labcount = int.Parse(numLabCount.Text);
            htmltext.Replace("$pagecount", "张数：" + (pagecount * labcount).ToString());

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

                for (int k = 0; k < labcount; k++)
                {
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
                        htmltext.Replace("$orderno", dt.Rows[i]["saleorderno"].ToString());
                        htmltext.Replace("$SendOutOrderNo", dt.Rows[i]["SendOutOrderNo"].ToString());
                        htmltext.Replace("$proname", dt.Rows[i]["proname"].ToString());
                        htmltext.Replace("$prono", dt.Rows[i]["prono"].ToString());

                        htmltext.Replace("$itemno", dt.Rows[i]["itemno"].ToString());
                        htmltext.Replace("$itemname", dt.Rows[i]["itemname"].ToString());
                        htmltext.Replace("$spec", dt.Rows[i]["spec"].ToString());
                        htmltext.Replace("$material", dt.Rows[i]["material"].ToString());
                        htmltext.Replace("$codevalue", dt.Rows[i]["BarCode"].ToString());

                        //IDAutomation.NetAssembly.FontEncoder fe = new IDAutomation.NetAssembly.FontEncoder();
                        //htmltext.Replace("$code", fe.Code128(dt.Rows[i]["BarCode"].ToString(), 0, false));
                        //fe = null;
                        System.Threading.Thread.Sleep(5);
                        System.Drawing.Bitmap bitmap = BarcodeHelper.Generate1(dt.Rows[i]["BarCode"].ToString(), 150, 150);// CreateQRCode(str, 200, 5);
                        string s = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                        string codejpg = HttpContext.Current.Server.MapPath("~/pdf/" + s + ".bmp");
                        //image.Save(codejpg);
                        bitmap.Save(codejpg);
                        htmltext.Replace("$code", s);


                        #endregion
                        sw.WriteLine(htmltext);
                        pageindex++;
                    }
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
 
    }
}