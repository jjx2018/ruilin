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
using System.Data.OleDb;
using System.Collections;
using System.Data;

namespace AppBoxPro.ClientMag
{
    public partial class magClient : PageBase
    {
        static Hashtable htClickColsName = new Hashtable();
        log4net.ILog log = log4net.LogManager.GetLogger("magBom.aspx");
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "ClientView";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            // 权限检查
            CheckPowerWithButton("ClientNew", btnNew);
            //CheckPowerDeleteWithButton(btnDeleteSelected);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            btnNew.OnClientClick = Window1.GetShowReference("~/ClientMag/clientnew.aspx", "新增职务");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;

            BindGrid();

        }

        private void BindGrid()
        {
            using (var appdb = new AppContext())
            {
                IQueryable<Client> q = appdb.clients;

                // 在职务名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(t => t.Name.Contains(searchText));
                }
                foreach (DictionaryEntry de in htClickColsName)
                {
                    switch (de.Key.ToString())
                    {
                        case "Name":
                            q = q.Where(u => u.Name == de.Value.ToString());
                            break;
                        case "ClientNo":
                            q = q.Where(u => u.ClientNo == de.Value.ToString());
                            break;
                        case "Country":
                            q = q.Where(u => u.Country == de.Value.ToString());
                            break;
                        case "Address":
                            q = q.Where(u => u.Address == de.Value.ToString());
                            break;
                        case "Telephone":
                            q = q.Where(u => u.Telephone == de.Value.ToString());
                            break;
                        case "ContactMan":
                            q = q.Where(u => u.ContactMan == de.Value.ToString());
                            break;
                        case "Email":
                            q = q.Where(u => u.Email.ToString() == de.Value.ToString());
                            break;
                        case "website":
                            q = q.Where(u => u.website.ToString() == de.Value.ToString());
                            break;
                        case "busiowner":
                            q = q.Where(u => u.busiowner.ToString() == de.Value.ToString());
                            break;
                        case "paymode":
                            q = q.Where(u => u.paymode.ToString() == de.Value.ToString());
                            break;

                    }
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();

                // 排列和分页
                q = SortAndPage<Client>(q, Grid1);

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
            //BindGrid("", Grid1.PageIndex + 1);
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
            CheckPowerWithWindowField("ClientEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("ClientDelete", Grid1, "deleteField");
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
                if (!CheckPower("ClientDelete"))
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
                    appdb.clients.Where(t => t.SN == sn).Delete();
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
            if (!CheckPower("ClientDelete"))
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
                appdb.clients.Where(u => ids.Contains(u.SN)).Delete();
            }

            // 重新绑定表格
            BindGrid();
        }

        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Grid1.PageSize = 100000;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }

        #region 自定义函数
        protected void btndownfile_Click(object sender, EventArgs e)
        {
            downFile(Server.MapPath("~/model/订餐信息模板.xlsx"), "订餐信息模板");
        }
        //下载文件
        private void downFile(string filePath, string fileName)
        {
            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/excel";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + ".xlsx");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();


        }
        //导出excel相关
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


        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (filePhoto.HasFile)
            {
                string fileName = filePhoto.ShortFileName;
                if (!filePhoto.HasFile)
                {
                    // 清空文件上传控件
                    filePhoto.Reset();
                    return;
                }


                fileName = fileName.Replace(":", "").Replace(" ", "").Replace("\\", "").Replace("/", "");
                fileName = DateTime.Now.Ticks.ToString() + "-" + fileName;

                filePhoto.SaveAs(Server.MapPath("~/upload/" + fileName));
                readExcel(fileName);


            }
        }

        private void readExcel(string filename)
        {

            try
            {
                string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/upload/" + filename) + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"";

                using (OleDbConnection conn = new OleDbConnection(connstring))
                {
                    conn.Open();
                    DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字  
                    //for(int k=0;k<sheetsName.Rows.Count;k++)
                    //{
                    //    log.Info(sheetsName.Rows[k][2].ToString());
                    //}
                    string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字  
                    string sql = string.Format("SELECT * FROM [{0}]", firstSheetName); //查询字符串   


                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                    DataSet set = new DataSet();
                    ada.Fill(set);
                    DataTable dt = set.Tables[0];

                    SQLHelper.DbHelperSQL.SetConnectionString("");

                    ArrayList al = new ArrayList();
                    SQLHelper.DbHelperSQL.SetConnectionString("");
                    for (int i = 2; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() == "" && dt.Rows[i][1].ToString() == "" && dt.Rows[i][2].ToString() == "" && dt.Rows[i][3].ToString() == "" && dt.Rows[i][4].ToString() == "" && dt.Rows[i][5].ToString() == "" && dt.Rows[i][6].ToString() == "" && dt.Rows[i][7].ToString() == "" && dt.Rows[i][8].ToString() == "" && dt.Rows[i][9].ToString() == "")
                        {
                            break;
                        }
                        else
                        {
                            sql = "select count(*) from client where ClientNo='" + dt.Rows[i][2].ToString() + "' or Name='" + dt.Rows[i][1].ToString() + "'";
                            if (SQLHelper.DbHelperSQL.GetSingle(sql, 30).Equals("0"))
                            {
                                sql = "insert into client(ClientNo,subjectcode,Name,Address,Country,Telephone,Fax,ContactMan,Email,website,busiowner,paymode,remark,Reserve1,Reserve2,Reserve3) values('" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][3].ToString() + "'," + dt.Rows[i][5].ToString() + ",'" + dt.Rows[i][5].ToString() + "','" + dt.Rows[i][6].ToString() + "','" + dt.Rows[i][7].ToString() + "','" + dt.Rows[i][8].ToString() + "','" + dt.Rows[i][9].ToString() + "','" + dt.Rows[i][10].ToString() + "','" + dt.Rows[i][11].ToString() + "','" + dt.Rows[i][12].ToString() + "','" + dt.Rows[i][13].ToString() + "','" + dt.Rows[i][14].ToString() + "')";
                                al.Add(sql);
                            }
                        }
                    }
                    if (al.Count > 0 && SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                    {
                        Alert.Show("导入成功");
                    }
                    else
                    {
                        Alert.Show("导入失败");
                    }
                }


            }
            catch (Exception ee)
            {

                log.Info(ee.ToString());
            }
            finally
            {
                BindGrid();
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



    }
}