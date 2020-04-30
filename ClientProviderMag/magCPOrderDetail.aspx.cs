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

namespace AppBoxPro.ClientProviderMag
{
    public partial class magCPOrderDetail : PageBase
    {
        static Hashtable htClickColsName = new Hashtable();

        log4net.ILog log = log4net.LogManager.GetLogger("magPlan.aspx");
        public string cplan="",cporderno="",saleorderno="",orderdate="",provider="",contactman="",tel="",fax="";
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "ClientProviderOrderView";
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
                //CheckPowerWithButton("ClientProviderOrderDelete", btnDelete);
                //ResolveDeleteButtonForGrid(btnDelete, Grid1);
                //CheckPowerWithButton("ClientProviderOrderDelete", btnDeleteSelected2);
                //ResolveDeleteButtonForGrid(btnDeleteSelected2, Grid2);

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
                else if (requestArg.Equals("RefreshGrid"))
                {
         
                }
            }
            FileOper.delfile(Server.MapPath("~/pdf/"));
        }

        

        private void LoadData()
        {
            int pid = GetQueryIntValue("pid");
            using (var appdb = new AppContext())
            {
                CPOrderHeader item = appdb.cporderheader.Where(u => u.SN == pid).FirstOrDefault();
                cplan = item.CPPlanNo;
                cporderno = item.CPOrderNo;
                saleorderno = item.SaleOrderNo;
                orderdate = item.CPDate.Value.ToString("yyyy-MM-dd");
                provider = item.Provider;
                contactman = item.ContactMan;
                tel = item.Tel;
                fax = item.Fax;
            }
                BindGrid2();
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

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid2.PageSize = Convert.ToInt32(DropDownList1.SelectedValue);

            BindGrid2();
        }



        private void BindGrid2()
        {
            

           

            using (var appdb = new AppContext())
            {
                int pid =GetQueryIntValue("pid");

                var q = from a in appdb.cporderdetail
                          join b in appdb.cporderheader on a.FSN equals b.SN
                          where b.SN == pid
                          select a;





                //IQueryable<YW_ProcessRec> q = appdb.processRec; //.Include(u => u.Dept);
                // 在用户名称中搜索
                string searchText = TwinTriggerBox2.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.ItemNo.Contains(searchText) || u.ItemName.Contains(searchText));
                }
                // 进仓 出仓

                //日期 筛选
                if (DateFrom.SelectedDate.HasValue)
                {
                    q = q.Where(u => u.InputeDate >= DateFrom.SelectedDate);
                }
                if (DateTo.SelectedDate.HasValue)
                {
                    q = q.Where(u => u.InputeDate <= DateTo.SelectedDate);
                }
                foreach (DictionaryEntry de in htClickColsName)
                {
                    switch (de.Key.ToString())
                    {
                        case "SaleOrderNo":
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
                        case "Unit":
                            q = q.Where(u => u.Unit == de.Value.ToString());
                            break;
                        case "Quantity":
                            q = q.Where(u => u.Quantity.ToString() == de.Value.ToString());
                            break;


                    }
                }


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = q.Count();// q.Count();

                // 排列和数据库分页
                q = SortAndPage<CPOrderDetail>(q, Grid2);

                Grid2.DataSource = q;// itemq.Take(2);// q;
                Grid2.DataBind();
            }
        }

        #endregion

        #region Events


         
        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("InstockEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("ClientProviderOrderDelete", Grid2, "deleteField");
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
            // 在操作之前进行权限检查
            if (!CheckPower("ClientProviderOrderDelete"))
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

        #endregion
        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            int ID = GetSelectedDataKeyID(Grid2);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("ClientProviderOrderDelete"))
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

        //protected void Grid2_RowDoubleClick(object sender, GridRowClickEventArgs e)
        //{
        //    PageContext.RegisterStartupScript(Window1.GetShowReference("~/OrderMag/OrderDetailEdit.aspx?id=" + Grid2.DataKeys[Grid2.SelectedRowIndex][0].ToString() + "&k=1", "产品详情"));
        //}

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
            Grid2.PageSize = 1000000;
            BindGrid2();
            //sql = "select ItemNo,ItemName,Spec,recClass from ("+sql+") a";
            //log.Info(sql);
            //SQLHelper.DbHelperSQL.SetConnectionString("");
            ////Grid grid = new Grid();


            //Grid2.DataSource = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 60);
            //Grid2.DataBind();
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

        #endregion











 
 
        protected void btnSelPrintLab_Click(object sender, EventArgs e)
        {
            if (Grid2.SelectedRowIndexArray.Length <= 0)
            {
                Alert.Show("请先选择要打印的客供单");
                return;
            }
            //string fname = makepdf();
            //string fname = htmltoPdfForSelLab("PurchaseLab");
            string fname = printLabForHtmlByGrid("labHeader", "CPLabHtml");
            string str = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            //            string s = @" parent.addExampleTab({
            //                id: " + str + @"+'_tab',
            //                iframeUrl: 'ruilin/showpdf.aspx?f=" + fname + @"',
            //                title:'打印客供货物标签',
            //                iconFont: 'sign-in',
            //                refreshWhenExist: true
            //            });";


            string s = @" parent.addExampleTab({
                id: " + str + @"+'_tab',
                iframeUrl: 'pdf/" + fname + @"',
                title:'打印客供物料标签',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });";
            PageContext.RegisterStartupScript(s);//"window.open('../pdf/'"+fname+",'newindow','height=600,width=900,top=0,left=0,toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no');"
                                                 //PageContext.RegisterStartupScript(Window1.GetShowReference("~/pdf/" + fname, "打印客供货物标签"));


        }
        string printLabForHtmlByGrid(string modelheadfile, string modelcontentfile)
        {
            string fname = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".html";
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

                    if (pageindex != 1)
                    {
                        htmltext.Append("<div class='page'><br></div>");
                    }
                    htmltext.Append(htmlcontent.ToString());
                    #region //替换模板内容
                    htmltext.Replace("$orderno", Grid2.Rows[i].Values[2].ToString());
                    htmltext.Replace("$sdate", DateTime.Now.ToString("yyyyMMdd"));
                    htmltext.Replace("$remark", Grid2.Rows[i].Values[11].ToString());
                    htmltext.Replace("$itemno", Grid2.Rows[i].Values[6].ToString());
                    //htmltext.Replace("$provider", Grid1.Rows[Grid1.SelectedRowIndex].Values[5].ToString());
                    htmltext.Replace("$itemname", Grid2.Rows[i].Values[7].ToString());
                    htmltext.Replace("$spec", Grid2.Rows[i].Values[8].ToString());
                    htmltext.Replace("$quantity", Grid2.Rows[i].Values[9].ToString() + Grid2.Rows[i].Values[10].ToString());
                    sql = "select material from bomdetail   where sn=" + Grid2.Rows[i].Values[12].ToString() + "";
                    htmltext.Replace("$material", SQLHelper.DbHelperSQL.GetSingle(sql, 30));
                    sql = "select surfacedeal from bomdetail    where sn=" + Grid2.Rows[i].Values[12].ToString() + "";

                    htmltext.Replace("$codevalue", Grid2.Rows[i].Values[13].ToString());


                    htmltext.Replace("$color", SQLHelper.DbHelperSQL.GetSingle(sql, 30));
                    htmltext.Replace("$caseno", "");


                    //IDAutomation.NetAssembly.FontEncoder fe = new IDAutomation.NetAssembly.FontEncoder();
                    //htmltext.Replace("$code", fe.Code128(Grid2.Rows[i].Values[2].ToString(), 0, false));
                    //fe = null;
                    System.Threading.Thread.Sleep(5);
                    System.Drawing.Bitmap bitmap = BarcodeHelper.Generate1(Grid2.Rows[i].Values[13].ToString(), 150, 150);// CreateQRCode(str, 200, 5);
                    string s = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string codejpg = HttpContext.Current.Server.MapPath("~/pdf/" + s + ".bmp");
                    //image.Save(codejpg);
                    bitmap.Save(codejpg);
                    htmltext.Replace("$code", s);
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


    }
}