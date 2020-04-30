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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
using System.Drawing.Imaging;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Configuration;

namespace AppBoxPro.StoreConfirm
{
    public partial class magInstruction : PageBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Instruction.aspx");
        static Hashtable htClickColsName = new Hashtable();
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "InstructionView";
            }
        }

        #endregion

        private bool AppendToEnd = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var appdb = new AppContext())
                {
                    IQueryable<ItemClass> q = appdb.itemclasses;
                    ddlSclass.DataSource = q.ToList();
                    ddlSclass.DataTextField = "Name";
                    ddlSclass.DataValueField = "Name";
                    ddlSclass.DataBind();
                }
                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                // 新增数据初始值
                JObject defaultObj = new JObject();
                defaultObj.Add("ItemNo", "");
                DayOfWeek dw = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).DayOfWeek;
                string ss = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dw);
                defaultObj.Add("Name", "");
                defaultObj.Add("Spec", "");
                defaultObj.Add("MaterialNo", "");
                defaultObj.Add("ItemColor", "");
                defaultObj.Add("AddReserve1", "");
                defaultObj.Add("ClassName", "");

                defaultObj.Add("deleteField", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(FineUIPro.Icon.Delete)));

                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);



                // 删除选中行按钮
                //btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;
                CheckPowerWithButton("InstructionDelete", btnDelete);
                ResolveDeleteButtonForGrid(btnDelete, Grid1);

                // 每页记录数
                Grid1.PageSize = ConfigHelper.PageSize;
                ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
                // 绑定表格
                //BindGrid("", 1);
                BindGrid();
            }
        }
        // 删除选中行的脚本
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        }
        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 设置LinkButtonField的点击客户端事件
            //LinkButtonField deleteField = Grid1.FindColumn("deleteField") as LinkButtonField;
            //deleteField.OnClientClick = GetDeleteScript();
        }
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            //BindGrid(e.SortField, Grid1.PageIndex + 1);
            BindGrid();
        }
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            int sn = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("InstructionDelete"))
                {
                    
                    CheckPowerFailWithAlert();
                    return;
                }
                using (var appdb = new AppContext())
                {

                    appdb.instruction.Where(t => t.SN == sn).Delete();
                }
                BindGrid();
            }
        }
        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {

            // 在操作之前进行权限检查
            if (!CheckPower("InstructionDelete"))
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
                    appdb.instruction.Where(u => ids.Contains(u.SN)).Delete();
                }
            }
            // 重新绑定表格
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {

            Grid1.PageIndex = e.NewPageIndex;
            //BindGrid("", e.NewPageIndex + 1);
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
        protected void btnSearch_click(object sender, EventArgs e)
        {
            //BindGrid("", Grid1.PageIndex + 1);
            BindGrid();
        }
        private void BindGrid()
        {
            using (var appdb = new AppContext())
            {
                //IQueryable<Instruction> q = appdb.instruction;
                var q = from a in appdb.instruction
                        join b in appdb.v_userinfor on a.Receiver equals b.userid into userjoin
                        from c in userjoin.DefaultIfEmpty()
                        select new {a.SN,a.OrderNo,a.ProNo,a.ProName,a.ItemNo,a.ItemName,a.MainFrom,a.UsingQuantity,a.ConfirmQuantity,a.RealUsingQuantity,a.IsConfirm,a.Spec,a.Material,a.SurfaceDeal,a.Sclass,a.ReceiveDept,a.Receiver,c.ChineseName,a.ConfirmDate,a.BarCode,a.Remark,a.OdtSN };
                // 在职务名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(t => t.ProName.Contains(searchText) || t.ItemName.Contains(searchText) || t.ProNo.Contains(searchText) || t.ItemNo.Contains(searchText)||t.OrderNo.Contains(searchText));
                }
                if (rbtState.SelectedValue != "")
                {
                    int k = int.Parse(rbtState.SelectedValue);
                    q = q.Where(u => u.IsConfirm == k);
                }
                if (User.Identity.Name != "admin")
                {
                    q = q.Where(u => u.Receiver == User.Identity.Name);
                }
                foreach (DictionaryEntry de in htClickColsName)
                {
                    switch (de.Key.ToString())
                    {
                        case "ItemNo":
                            q = q.Where(u => u.ItemNo == de.Value.ToString());
                            break;
                        case "ItemName":
                            q = q.Where(u => u.ItemName == de.Value.ToString());
                            break;
                        case "Spec":
                            q = q.Where(u => u.Spec == de.Value.ToString());
                            break;
                        case "Material":
                            q = q.Where(u => u.Material == de.Value.ToString());
                            break;
                        case "SurfaceDeal":
                            q = q.Where(u => u.SurfaceDeal == de.Value.ToString());
                            break;
                        case "OrderNo":
                            q = q.Where(u => u.OrderNo == de.Value.ToString());
                            break;
                        case "ProNo":
                            q = q.Where(u => u.ProNo.ToString() == de.Value.ToString());
                            break;
                        case "ProName":
                            q = q.Where(u => u.ProName.ToString() == de.Value.ToString());
                            break;
                        case "Sclass":
                            q = q.Where(u => u.Sclass.ToString() == de.Value.ToString());
                            break;
                        case "MainFrom":
                            q = q.Where(u => u.MainFrom.ToString() == de.Value.ToString());
                            break;
                       
                    }
                }

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

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            BindGrid();
            //BindGrid("", Grid1.PageIndex + 1);
        }
        protected void rbtState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
            //BindGrid("", Grid1.PageIndex + 1);
        }
        protected void dinnertypeChk_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
            //BindGrid("", Grid1.PageIndex + 1);
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
                //List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();
                //for (int i = 0; i < newAddedList.Count; i++)
                //{
                //    //s += newAddedList[i]["ItemNo"].ToString() + "----" + newAddedList[i]["Name"].ToString() + "----" + newAddedList[i]["Spec"].ToString() + "----" + newAddedList[i]["MaterialNo"].ToString() + "----" + newAddedList[i]["ItemColor"].ToString();
                //    sql = "insert into AllItem(ItemNo,Name,Spec,MaterialNo,ItemColor,AddReserve1,ClassName) values('" + newAddedList[i]["ItemNo"].ToString() + "','" + newAddedList[i]["Name"].ToString() + "','" + newAddedList[i]["Spec"].ToString() + "','" + newAddedList[i]["MaterialNo"].ToString() + "','" + newAddedList[i]["ItemColor"].ToString() + "','" + newAddedList[i]["AddReserve1"].ToString() + "','" + newAddedList[i]["ClassName"].ToString() + "')";
                //    s += sql + "---";
                //    al.Add(sql);
                //}

                //Alert.Show(s);
                //return;
                //s = "";
                // 修改的现有数据
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                SQLHelper.DbHelperSQL.SetConnectionString("");
                foreach (int rowIndex in modifiedDict.Keys)
                {
                    sql = "update Instruction set ";
                    for (int i = 0; i < Grid1.Columns.Count; i++)
                    {
                        if (modifiedDict[rowIndex].ContainsKey(Grid1.Columns[i].ColumnID))
                        {
                            sql += Grid1.Columns[i].ColumnID + "='" + modifiedDict[rowIndex][Grid1.Columns[i].ColumnID].ToString() + "',";
                        }

                    }
                    //sql = sql.TrimEnd(new char[] { ',' });
                    sql += "ConfirmDate=getdate() where sn=" + Grid1.DataKeys[rowIndex][0];
                    
                    al.Add(sql);
                    sql = "select case  when (select COUNT(*) from Instruction where OdtSN = " + Grid1.Rows[rowIndex].Values[21].ToString() + "  and OrderNo = '" + Grid1.Rows[rowIndex].Values[1].ToString() + "') >= (select COUNT(*) from BomDetail where FSN = (select SN from BomHeader where OdtSN = " + Grid1.Rows[rowIndex].Values[21].ToString() + " and OrderNo = '" + Grid1.Rows[rowIndex].Values[1].ToString() + "')) then 1 else 0 end ";
                    
                    if (SQLHelper.DbHelperSQL.GetSingle(sql,30)=="1")
                    {
                        sql = "update OrderDetail set isconfirm=1 where SN = " + Grid1.Rows[rowIndex].Values[21].ToString() + "  and OrderNo = '" + Grid1.Rows[rowIndex].Values[1].ToString() + "'";
                        al.Add(sql);
                        //Alert.Show(sql);
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
                Alert.Show(ee.Message);
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xlsx");
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

        protected void txtConfirmQuantity_Blur(object sender, EventArgs e)
        {
            try
            {
                //FineUIPro.TextBox ctrl = (FineUIPro.TextBox)Grid1.FindControl("txtConfirmQuantity");

                //Alert.Show(ctrl.Text);
                //return;
                if (Grid1.GetModifiedDict().Count <= 0)
                {
                    return;
                }
                Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
                string s = "";
                foreach (int rowIndex in modifiedDict.Keys)
                {
                    s = modifiedDict[rowIndex]["ConfirmQuantity"].ToString();
                    double d = (double.Parse(Grid1.Rows[rowIndex].Values[7].ToString()) - double.Parse(s));
                    //Grid1.UpdateCellValue(rowIndex, "ConfirmQuantity", s);
                    Grid1.UpdateCellValue(rowIndex, "RealUsingQuantity", d.ToString());
                    Grid1.UpdateCellValue(rowIndex, "ConfirmDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Grid1.UpdateCellValue(rowIndex, "IsConfirm", "1");
                    Grid1.UpdateCellValue(rowIndex, "BarCode", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    System.Threading.Thread.Sleep(1);

                }
            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
        }

        protected void rbtState_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnLabPrint_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length <= 0)
            {
                Alert.Show("请先选择要打印的记录");
                return;
            }
            //string fname = makepdf();
            string fname = printForHtmlByGrid("labHeader", "instructionLab");
            string str = DateTime.Now.ToString("yyyyMMddHHmmsss");
            //            string s = @" parent.addExampleTab({
            //                id: " + str + @"+'_tab',
            //                iframeUrl: 'ruilin/showpdf.aspx?f=" + fname + @"',
            //                title:'打印备货单标签',
            //                iconFont: 'sign-in',
            //                refreshWhenExist: true
            //            });";
            string s = @" parent.addExampleTab({
                id: " + str + @"+'_tab',
                iframeUrl: 'pdf/" + fname + @"',
                title:'打印备货物料标签',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });";
            PageContext.RegisterStartupScript(s);
            //PageContext.RegisterStartupScript("parent.addExampleTab.apply(null, ['printer_two" + DateTime.Now.ToString("yyyyMMddHHmmsss") + "', basePath+'ydcc/showpdf.aspx?f=" + fname + "', '标签打印', basePath + 'res/icon/printer.png', false]);");
        }


        #region 自定义函数
        //
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
        /// <param name="QRCodeEncodeMode">二维码编码方式</param>
        /// <param name="QRCodeErrorCorrect">纠错码等级</param>
        /// <param name="QRCodeVersion">二维码版本号 0-40</param>
        /// <param name="QRCodeScale">每个小方格的预设宽度（像素），正整数</param>
        /// <param name="size">图片尺寸（像素），0表示不设置</param>
        /// <param name="border">图片白边（像素），当size大于0时有效</param>
        /// <returns></returns>
        public System.Drawing.Image CreateQRCode(string Content, QRCodeEncoder.ENCODE_MODE QRCodeEncodeMode, QRCodeEncoder.ERROR_CORRECTION QRCodeErrorCorrect, int QRCodeVersion, int QRCodeScale, int size, int border, Color codeEyeColor)
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

                //根据参数设置二维码图片白边的最小宽度（按需缩小）
                #region 根据参数设置二维码图片白边的最小宽度
                if (image.Width <= size && border > 0)
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

                //已经确认二维码图像，为图像染色修饰
                if (true)
                {
                    //定位点方块边长
                    int beSize = qrCodeEncoder.QRCodeScale * 3;

                    int bep1_l = qrCodeEncoder.QRCodeScale * 2;
                    int bep1_t = qrCodeEncoder.QRCodeScale * 2;

                    int bep2_l = image.Width - qrCodeEncoder.QRCodeScale * 5 - 1;
                    int bep2_t = qrCodeEncoder.QRCodeScale * 2;

                    int bep3_l = qrCodeEncoder.QRCodeScale * 2;
                    int bep3_t = image.Height - qrCodeEncoder.QRCodeScale * 5 - 1;

                    int bep4_l = image.Width - qrCodeEncoder.QRCodeScale * 7 - 1;
                    int bep4_t = image.Height - qrCodeEncoder.QRCodeScale * 7 - 1;

                    System.Drawing.Graphics graphic0 = System.Drawing.Graphics.FromImage(image);

                    // Create solid brush. 
                    SolidBrush blueBrush = new SolidBrush(codeEyeColor);

                    // Fill rectangle to screen. 
                    graphic0.FillRectangle(blueBrush, bep1_l, bep1_t, beSize, beSize);
                    graphic0.FillRectangle(blueBrush, bep2_l, bep2_t, beSize, beSize);
                    graphic0.FillRectangle(blueBrush, bep3_l, bep3_t, beSize, beSize);
                    graphic0.FillRectangle(blueBrush, bep4_l, bep4_t, qrCodeEncoder.QRCodeScale, qrCodeEncoder.QRCodeScale);
                }

                //当目标图片尺寸大于二维码尺寸时，将二维码绘制在目标尺寸白色画布的中心位置
                #region 如果目标尺寸大于生成的图片尺寸，将二维码绘制在目标尺寸白色画布的中心位置
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
                #endregion
            }
            #endregion
            return image;
        }



        //新增记录生成标签
        private string htmltoPdfForInsert(string modelfile)
        {

            // 设置页面
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(GetPdfSize(int.Parse(ConfigurationManager.AppSettings["labelWidth"].ToString())), GetPdfSize(int.Parse(ConfigurationManager.AppSettings["labelHeight"].ToString())), 1);

            pageSize.BackgroundColor = new iTextSharp.text.BaseColor(0xFF, 0xFF, 0xFF);
            Document document = new Document(pageSize, 0, 0, 0, 0);
            //document.SetMargins(0f, 0f, 0f, 0f);
            document.SetPageSize(pageSize);
            string fname = DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                //Alert.Show(Server.MapPath("images\\test.pdf"));
                //创建文档

                PdfWriter pdfw = PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/pdf/" + fname + ".pdf"), FileMode.Create));

                // 打开文档
                document.Open();
                BaseFont baseFont = BaseFont.CreateFont(
       "C:\\WINDOWS\\FONTS\\simsun.ttc,0",
       BaseFont.IDENTITY_H,
       BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 60);

                //获取模板文件内容
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
                //写入pdf的字符串
                StringBuilder htmltopdftext = new StringBuilder();
                //htmltopdftext = htmltext;
                //doc.SetPageSize(PageSize.A4.Rotate());    //横向输出
                //MemoryStream outputStream = new MemoryStream();//要把PDF写到哪个串流  

                //string[] strprosn = txtProsn.Text.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                ////PdfWriter writer = PdfWriter.GetInstance(document, outputStream);
                //int pageindex = 1;
                //// 添加文档内容
                //for (int i = 0; i < int.Parse(txtPrintNum.Text.ToString()); i++)
                //{
                //    htmltopdftext = htmltext;
                //    //BaseFont bfChinese = BaseFont.CreateFont("STSongStd-Light", "UniGB-UCS2-H", false);
                //    //iTextSharp.text.Font font = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                //    string str = dpKDdate.SelectedDate.Value.ToString("yyyy年MM月dd日");
                //    htmltopdftext.Replace("$date", string.IsNullOrEmpty(str) ? DateTime.Now.ToString("yyyy年MM月dd日") : DateTime.Parse(str).ToString("yyyy年MM月dd日"));
                //    htmltopdftext.Replace("$pname", ddlProname.SelectedText);
                //    htmltopdftext.Replace("$spec", txtSpec.Text.Trim());
                //    htmltopdftext.Replace("$caseno", txtCaseno.Text.Trim());

                //    str = "http://" + ConfigurationManager.AppSettings["urlapp"].ToString() + "/ydcc/logreg.htm?id=" + strprosn[i];
                //    System.Drawing.Image image = CreateQRCode(str, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 7, 1, 100, 1);
                //    string codejpg = HttpContext.Current.Server.MapPath("~/pdf/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
                //    image.Save(codejpg);
                //    htmltopdftext.Replace("@codejpg", codejpg);

                //    string pageHtml = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(htmltopdftext.ToString()));
                //    pageHtml = getImage(pageHtml);//输出html上的图片


                //    //指定文件预设开档时的缩放为100%  

                //    PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, document.PageSize.Height, 1f);

                //    byte[] data = Encoding.UTF8.GetBytes(pageHtml);//字串转成byte[]  
                //    MemoryStream msInput = new MemoryStream(data);

                //    //使用XMLWorkerHelper把Html parse到PDF档里  
                //    XMLWorkerHelper.GetInstance().ParseXHtml(pdfw, document, msInput, null, Encoding.UTF8, new UnicodeFontFactory());
                //    //XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8);

                //    //将pdfDest设定的资料写到PDF档  
                //    PdfAction action = PdfAction.GotoLocalPage(pageindex, pdfDest, pdfw);
                //    pdfw.SetOpenAction(action);

                //    msInput.Close();
                //    //outputStream.Close();


                //    //table.AddCell("row 2; cell 2");
                //    //document.Add();

                //    document.NewPage();
                //    document.ResetPageCount();
                //    pageindex++;
                //}
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
        //选中记录生成标签
        private string htmltoPdfForSel(string modelfile)
        {

            // 设置页面大小
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(GetPdfSize(int.Parse(ConfigurationManager.AppSettings["labelWidth"].ToString())), GetPdfSize(int.Parse(ConfigurationManager.AppSettings["labelHeight"].ToString())), 1);

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
                // 添加文档内容 
                foreach (int i in Grid1.SelectedRowIndexArray)
                {
                    if (string.IsNullOrEmpty(Grid1.Rows[i].Values[18].ToString()))
                    {
                        continue;
                    }
                    htmltopdftext.Clear();
                    htmltopdftext.Append(modelstr);
                    //BaseFont bfChinese = BaseFont.CreateFont("STSongStd-Light", "UniGB-UCS2-H", false);
                    //iTextSharp.text.Font font = new iTextSharp.text.Font(bfChinese, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    #region //替换模板内容
                    string str = Grid1.Rows[i].Values[8].ToString();
                    htmltopdftext.Replace("$orderno", Grid1.Rows[i].Values[1].ToString());
                    htmltopdftext.Replace("$proname", Grid1.Rows[i].Values[3].ToString());
                    htmltopdftext.Replace("$itemname", Grid1.Rows[i].Values[5].ToString());
                    htmltopdftext.Replace("$spec", Grid1.Rows[i].Values[11].ToString());
                    htmltopdftext.Replace("$material", Grid1.Rows[i].Values[12].ToString());

                    str = "http://" + ConfigurationManager.AppSettings["urlapp"].ToString() + "/ydcc/logreg.htm?id=" + Grid1.Rows[i].Values[1].ToString();
                    //System.Drawing.Image image = CreateQRCode(str, QRCodeEncoder.ENCODE_MODE.BYTE, QRCodeEncoder.ERROR_CORRECTION.M, 7, 100,300, 1);

                    System.Drawing.Bitmap bitmap = BarcodeHelper.Generate2(Grid1.Rows[i].Values[18].ToString(), 150, 50);// CreateQRCode(str, 200, 5);
                    string codejpg = HttpContext.Current.Server.MapPath("~/pdf/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp");
                    //image.Save(codejpg);
                    bitmap.Save(codejpg);

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


        string printForHtmlByGrid(string modelheadfile, string modelcontentfile)
        {
            string fname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            System.Threading.Thread.Sleep(5);
            #region //获取模板头文件内容
            string strurlfile = HttpContext.Current.Server.MapPath("~/Model/" + modelheadfile + ".html");
            StringBuilder htmltext = new StringBuilder(FileOper.getFileContent(strurlfile));
            int pagecount = Grid1.SelectedRowIndexArray.Length;
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
                    foreach (int i in Grid1.SelectedRowIndexArray)
                    {
                        htmltext.Clear();

                        if (pageindex != 1)
                        {
                            htmltext.Append("<div class='page'><br></div>");
                        }
                        htmltext.Append(htmlcontent.ToString());
                        #region //替换模板内容

                        htmltext.Replace("$orderno", Grid1.Rows[i].Values[1].ToString());
                        htmltext.Replace("$BHQuantity", Grid1.Rows[i].Values[7].ToString());
                        htmltext.Replace("$proname", Grid1.Rows[i].Values[3].ToString());
                        htmltext.Replace("$itemno", Grid1.Rows[i].Values[4].ToString());
                        htmltext.Replace("$itemname", Grid1.Rows[i].Values[5].ToString());
                        htmltext.Replace("$spec", Grid1.Rows[i].Values[11].ToString());
                        htmltext.Replace("$material", Grid1.Rows[i].Values[12].ToString());
                        htmltext.Replace("$surfacedeal", Grid1.Rows[i].Values[13].ToString());

                        htmltext.Replace("$codevalue", Grid1.Rows[i].Values[18].ToString());



                        //IDAutomation.NetAssembly.FontEncoder fe = new IDAutomation.NetAssembly.FontEncoder();
                        //htmltext.Replace("$code", fe.Code128(Grid1.Rows[i].Values[18].ToString(), 0, false));
                        //fe = null;
                        System.Threading.Thread.Sleep(5);
                        System.Drawing.Bitmap bitmap = BarcodeHelper.Generate1(Grid1.Rows[i].Values[18].ToString(), 150, 150);// CreateQRCode(str, 200, 5);
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

        #endregion

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            if (Grid1.Rows[e.RowIndex].Values[10].ToString() == "1")
            {
                e.CellCssClasses[8] = "f-grid-cell-uneditable";
                e.CellCssClasses[10] = "f-grid-cell-uneditable";
                //e.RowSelectable = false;
                //e.RowCssClass = "color2";
                //foreach (GridColumn column in Grid1.Columns)
                //{
                //    if(column.ColumnID== "ConfirmQuantity")
                //    e.CellCssClasses[column.ColumnIndex] = "f-grid-cell-uneditable";
                //}
            }
        }

        protected void btnSaveSel_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList al = new ArrayList();
                string sql = "";
                SQLHelper.DbHelperSQL.SetConnectionString("");
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    if(Grid1.Rows[rowIndex].Values[10].ToString()=="0")
                    {
                        sql = "update Instruction set RealUsingQuantity=UsingQuantity,ConfirmQuantity=0,ConfirmDate=getdate(),IsConfirm=1 where sn=" + Grid1.DataKeys[rowIndex][0];
                        al.Add(sql);
                        sql = "select case  when (select COUNT(*) from Instruction where OdtSN = " + Grid1.Rows[rowIndex].Values[21].ToString() + "  and OrderNo = '" + Grid1.Rows[rowIndex].Values[1].ToString() + "') >= (select COUNT(*) from BomDetail where FSN = (select SN from BomHeader where OdtSN = " + Grid1.Rows[rowIndex].Values[21].ToString() + " and OrderNo = '" + Grid1.Rows[rowIndex].Values[1].ToString() + "')) then 1 else 0 end ";

                        if (SQLHelper.DbHelperSQL.GetSingle(sql, 30) == "1")
                        {
                            sql = "update OrderDetail set isconfirm=1 where SN = " + Grid1.Rows[rowIndex].Values[21].ToString() + "  and OrderNo = '" + Grid1.Rows[rowIndex].Values[1].ToString() + "'";
                            al.Add(sql);
                        }
                    }
                }
                
                if (al.Count>0&&SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                {
                    Alert.Show("保存成功");
                    BindGrid();
                }
                else
                {
                    Alert.Show("保存失败");
                }
            }
            catch(Exception ee)
            {
                Alert.Show(ee.Message);
            }
        }
    }
}