using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Data.Entity;
using EntityFramework.Extensions;
using System.IO;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Drawing.Imaging;
using System.Data;
using System.Collections;

namespace AppBoxPro.other
{
    public partial class magNotice : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "NoticeView";
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                // 每页记录数
                Grid1.PageSize = ConfigHelper.PageSize;
                ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
                // 绑定表格
                BindGrid("", 1);
            }
        }
        // 删除选中行的脚本
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        }
        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            //// 设置LinkButtonField的点击客户端事件
            //LinkButtonField deleteField = Grid1.FindColumn("deleteField") as LinkButtonField;
            //deleteField.OnClientClick = GetDeleteScript();
        }
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid(e.SortField, Grid1.PageIndex + 1);
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {

            Grid1.PageIndex = e.NewPageIndex;
            BindGrid("", e.NewPageIndex + 1);
        }
        protected void btnSearch_click(object sender, EventArgs e)
        {
            BindGrid("", Grid1.PageIndex + 1);
        }
        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid("", 1);
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid("", 1);
        }

        private void BindGrid(string sortfield, int pageindex)
        {

            string sql = " ";
            string searchText = ttbSearchMessage.Text.Trim();
            string txt = "";
            if (!String.IsNullOrEmpty(searchText))
            {
                txt = " and (ggname like '%" + searchText + "%' or ggcontent  like '%" + searchText + "%' or pbusername  like '%" + searchText + "%')";
            }
            string pbdate = "";
            if (dcstartdate.Text != "" && dcendDate.Text == "")
            {
                pbdate = " and pbdate>='" + dcstartdate.Text + "'";
            }
            else if (dcstartdate.Text == "" && dcendDate.Text != "")
            {
                pbdate = " and pbdate<='" + dcendDate.Text + "'";
            }
            else if (dcstartdate.Text != "" && dcendDate.Text != "")
            {
                pbdate = " and pbdate between '" + dcstartdate.Text + "' and '" + dcendDate.Text + "'";
            }

            sql = @"select top " + Grid1.PageSize + @" o.* from 
(select row_number() over(order by id) as rownumber,oo.* from
(
select a.id,ggname,ggcontent,pbdate,b.ChineseName pbusername,startdate,enddate,(case state when 1 then '已发布' else '未发布' end) state from dbo.Notice a left join users b on a.pbusername=b.name 
where 1=1  " + txt + @" " + pbdate + @"
) as oo 
) as o
where rownumber>" + ((pageindex - 1) * Grid1.PageSize) + @"
order by id"; //and (DATENAME(weekday,pbdate)!='星期六' and DATENAME(weekday,pbdate)!='星期日') 
            //Alert.Show(sql);
            SQLHelper.DbHelperSQL.SetConnectionString("");
            DataTable table = SQLHelper.DbHelperSQL.ExecuteReturnDataTable(sql, "dd", 30);
            sql = "select count(*) from dbo.Notice a where  1=1 " + txt + " " + pbdate + "";
            Grid1.RecordCount = int.Parse(SQLHelper.DbHelperSQL.GetSingle(sql, 30));
            Grid1.DataSource = table;
            Grid1.DataBind();
        }
        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("NoticeDelete"))
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
                appdb.notices.Where(u => ids.Contains(u.ID)).Delete();
            }

            // 重新绑定表格
            BindGrid("", Grid1.PageIndex + 1);
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
        protected void Grid1_RowClick(object sender, GridRowClickEventArgs e)
        {
            //object[] keys = Grid1.DataKeys[e.RowIndex];
            txtName.Text = Grid1.Rows[e.RowIndex].Values[0].ToString();
            tbxContent.Text = Grid1.Rows[e.RowIndex].Values[1].ToString();
            startvalidate.Text = Grid1.Rows[e.RowIndex].Values[5].ToString();
            endvalidate.Text = Grid1.Rows[e.RowIndex].Values[6].ToString();
        }
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            int ID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("NoticeDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                using (AppContext appdb = new AppContext())
                {
                    appdb.notices.Where(u => u.ID == ID).Delete();

                    BindGrid("", Grid1.PageIndex + 1);
                }
            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid("", Grid1.PageIndex + 1);
        }
        protected void rbtState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid("", Grid1.PageIndex + 1);
        }
        protected void dinnertypeChk_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid("", Grid1.PageIndex + 1);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "insert into Notice(ggname,ggcontent,pbusername,state,startdate,enddate) values('" + txtName.Text.Trim() + "','" + tbxContent.Text + "','" + User.Identity.Name + "',0,'" + startvalidate.Text + "','" + endvalidate.Text + "')";
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    sql = "update Notice set ggname='" + txtName.Text + "',ggcontent='" + tbxContent.Text + "',startdate='" + startvalidate.Text + "',enddate='" + endvalidate.Text + "' where id=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0];
                }
                SQLHelper.DbHelperSQL.SetConnectionString("");
                if (SQLHelper.DbHelperSQL.ExecuteSql(sql, 30) > 0)
                {
                    Alert.Show("保存成功");
                    BindGrid("", Grid1.PageIndex + 1);
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
        protected void btnPub_Click(object sender, EventArgs e)
        {
            try
            {

                if (Grid1.SelectedRowIndexArray.Length == 0)
                {
                    Alert.Show("没有可发布的数据，请选中要发布的行！");
                    return;
                }
                string sql = "";
                // 修改的现有数据
                //foreach (int i in Grid1.SelectedRowIndexArray)
                //{
                //    sql = "update Notice set pbdate=getdate(),state=1 where id=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0];
                //    al.Add(sql);
                //}
                sql = "update Notice set pbdate=getdate(),state=1 where id=" + Grid1.DataKeys[Grid1.SelectedRowIndex][0];
                SQLHelper.DbHelperSQL.SetConnectionString("");
                if (SQLHelper.DbHelperSQL.ExecuteSql(sql, 30) > 0)
                {
                    Alert.Show("发布成功");
                    BindGrid("", Grid1.PageIndex + 1);
                }
                else
                {
                    Alert.Show("发布失败");
                }

            }
            catch (Exception ee)
            {
                Alert.Show(ee.Message);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();

        }
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid("", Grid1.PageIndex + 1);
        }

        #region 自定义函数

        //下载文件
        private void downFile(string filePath, string fileName)
        {
            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

            //System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            //if (fileInfo.Exists == true)
            //{
            //    const long ChunkSize = 102400;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力
            //    byte[] buffer = new byte[ChunkSize];

            //    Response.Clear();
            //    System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
            //    long dataLengthToRead = iStream.Length;//获取下载的文件总大小
            //    Response.ContentType = "application/octet-stream";
            //    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
            //    while (dataLengthToRead > 0 && Response.IsClientConnected)
            //    {
            //        int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
            //        Response.OutputStream.Write(buffer, 0, lengthRead);
            //        Response.Flush();
            //        dataLengthToRead = dataLengthToRead - lengthRead;
            //    }
            //    Response.Close();
            //}




            //FileInfo fileInfo = new FileInfo(filePath);
            //Response.Clear();
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            //Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            //Response.AddHeader("Content-Transfer-Encoding", "binary");
            //Response.ContentType = "application/octet-stream";
            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            //Response.WriteFile(fileInfo.FullName);
            //Response.Flush();
            //Response.End();

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
    }
}