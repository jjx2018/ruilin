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
using System.Text;
using System.IO;

namespace AppBoxPro.MaterialFinish
{
    public partial class showDetail : PageBase
    {
        public string pstr = "";
        log4net.ILog log = log4net.LogManager.GetLogger("magPlan.aspx");
        private bool AppendToEnd = false;
        static int seq = 0;
        static Hashtable htClickColsName = new Hashtable();
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "BOMView";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                 

                //btnClear.OnClientClick = SF1.GetResetReference();
                Grid2.PageSize = ConfigHelper.PageSize;
                ddlGridPageSize2.SelectedValue = ConfigHelper.PageSize.ToString();
                LoadData();
                
            }
            else
            {
                string requestArg = GetRequestEventArgument(); // 此函数所在文件：PageBase.cs
                string[] args = requestArg.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                //log.Info(requestArg);
                if (requestArg.Equals("RefreshGrid2"))
                {
                    BindGrid2();
                }
                else if (args != null && args.Length > 0 && args[0].Equals("updaterowcss"))
                {
                    // Grid2.Rows[int.Parse(ridx)].RowCssClass = "color1";
                    CommFunction.updateRowCss(args[1], Grid2, "color1");
                }
            }

        }
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid2.GetDeleteSelectedRowsReference(), String.Empty);
        }

        private void LoadData()
        {

            using (var appdb = new AppContext())
            {
                //IQueryable<Provider> pq = appdb.providers;
                //ddlSupplierId.DataSource = pq.ToList();
                //ddlSupplierId.DataTextField = "Name";
                //ddlSupplierId.DataValueField = "SN";
                //ddlSupplierId.DataBind();
                //pstr = "({";
                //foreach (Provider p in pq.ToList())
                //{
                //    pstr += "\"" + p.SN.ToString() + "\":\"" + p.Name + "\",";
                //}
                int SN = GetQueryIntValue("sn");
                //pstr = pstr.TrimEnd(new char[] { ',' });
                //pstr += "})";

                var q = from a in appdb.orderdetail
                        where a.SN == SN
                        select a;
                OrderDetail item = q.SingleOrDefault();
                labOrderno.Text = item.OrderNo;
                labClientNo.Text = item.ClinetNo;
                labProNo.Text = item.ItemNo;
                labProName.Text = item.ItemName;
                labQuantity.Text = item.Quantity.ToString();
                labUnit.Text = item.Unit;
                labColor.Text = item.Color;
                labIsNew.Text = item.IsNew;
                labIsPackingmaterials.Text = item.IsPackingmaterials;
                labCountryPackVer.Text = item.CountryPackVer;
                labIsChange.Text = item.IsChange;
                labDemand1.Text = item.Demand1;
                labDemand2.Text = item.Demand2;
                txtQuantity.Text = item.Quantity.ToString();
            }

            BindGrid2();
        }



        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid2();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid2();
        }



        private void BindGrid2()
        {
            
            using (var appdb = new AppContext())
            {
                string OrderNo = GetQueryValue("od");
                string ProNo = labProNo.Text;
                var qqq = from a in appdb.v_qtlmx
                          where a.ProNo == ProNo && a.OrderNo == OrderNo
                          select a;
                //var qqq = from a in appdb.bomdtl
                //          join b in appdb.bombase on a.FSN equals b.SN into basejoin
                //          from k in basejoin.DefaultIfEmpty()
                //          join c in appdb.allitems on a.AllitemSN equals c.SN into itemjoin
                //          from d in itemjoin.DefaultIfEmpty()
                //          join e in appdb.instruction.Where(u => u.IsConfirm == 0) on a.SN equals e.BomSN into instructionjoin
                //          from f in instructionjoin.DefaultIfEmpty()
                //          where k.SN == SN

                          //select new { a.SN, a.FSN, a.Seq, a.ItemNo, a.ItemName, a.Spec, a.Material, a.SurfaceDeal, a.OrderUsingQuantity, a.Sclass, a.MainFrom, a.WorkShop, a.ZuHe, a.SUBSN, a.ParentSN, a.ProUsingQuantity, SupplierId = d.SupplierId == null ? "" : d.SupplierId.ToString(), a.ZongCheng, a.BaseNum, a.StoreHouse, a.AllitemSN, a.IsValid, ISN = f.SN.ToString() };



                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    qqq = qqq.Where(u => u.ItemNo.Contains(searchText) || u.ItemName.Contains(searchText) || u.ProNo.Contains(searchText) || u.ProName.Contains(searchText));
                }
                foreach (DictionaryEntry de in htClickColsName)
                {
                    switch (de.Key.ToString())
                    {
                        case "ItemNo":
                            qqq = qqq.Where(u => u.ItemNo == de.Value.ToString());
                            break;
                        case "ItemName":
                            qqq = qqq.Where(u => u.ItemName == de.Value.ToString());
                            break;
                        case "MainFrom":
                            qqq = qqq.Where(u => u.MainFrom == de.Value.ToString());
                            break;
                        case "PDate":
                            qqq = qqq.Where(u => u.PDate.Value.ToLongDateString() == de.Value.ToString());
                            break;
                    }
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = qqq.Count();// q.Count();

                // 排列和数据库分页
                //qqq = SortAndPage<MyBom>(qqq, Grid2);
                qqq = SortAndPage(qqq, Grid2);
                //JObject jObject = new JObject();
                //jObject.Add("ItemNo", "<span style='color:red'>共计：" + qqq.Count() + "条</span>");
                //jObject.Add("Material", "<span style='color:#000;background-color:#ccc;height:25px;line-height:25px;font-size:18px;padding:5px;'>灰色为无效</span>");
                //jObject.Add("SurfaceDeal", "<span style='color:#000;background-color:#1AA348;height:25px;line-height:25px;font-size:18px;padding:5px;'>绿色为未确认</span>");
                //Grid2.SummaryData = jObject;
                Grid2.DataSource = qqq;// itemq.Take(2);// q;
                Grid2.DataBind();
                //if (qqq.Count() > 0)
                //{
                //    txtFSN.Text = Grid2.Rows[0].Values[18].ToString();
                //}
                //else
                //{
                //    txtFSN.Text = "";
                //}
            }
        }

        #endregion

        #region Events

         
        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            Grid2.SortDirection = e.SortDirection;
            Grid2.SortField = e.SortField;
            BindGrid2();
        }

        protected void Grid2_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid2.PageIndex = e.NewPageIndex;
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



        protected void ddlGridPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid2.PageSize = Convert.ToInt32(ddlGridPageSize2.SelectedValue);

            BindGrid2();
        }


        #endregion

        protected void tbxItemNo_TriggerClick(object sender, EventArgs e)
        {
            string[] selectedCell = Grid2.SelectedCell;
            if (selectedCell != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference("searchitemforadd.aspx?rowid=" + selectedCell[0]));

            }
        }
         
        protected void txtMaterial_TriggerClick(object sender, EventArgs e)
        {
            string[] selectedCell = Grid2.SelectedCell;
            if (selectedCell != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference("searchBomForAdd.aspx?rowid=" + selectedCell[0]));

            }
        }


         

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Grid2.PageSize = 1000000;
            BindGrid2();
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


        protected void Unnamed_Load(object sender, EventArgs e)
        {

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
    }
}