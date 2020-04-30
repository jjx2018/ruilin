using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.stock
{
    public partial class weiwaioutstockdetail : PageBase
    {
        //key-列名  value-值
        static Dictionary<string, string> dictClickColsName = new Dictionary<string, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dictClickColsName.Clear();
                LoadData();
            }
        }

        private void LoadData()
        {
            BindGrid1();
        }

        private void BindGrid1()
        {
            //料号、物料名称、规格、数量、委外入库日期、订单号、委外单号、仓库
            var q = from b in MYDB.sendoutprocessdetail
                    join a in MYDB.SendOutStockLists
                    on new { b.SendOutOrderNo, b.ProNo, b.ItemNo } equals new
                    {
                        a.SendOutOrderNo,
                        a.ProNo,
                        a.ItemNo
                    } into temp
                    from a in temp.DefaultIfEmpty()
                        //关联委外单表头，获取 供应商
                    join c in MYDB.sendoutprocessheader
                    on b.SendOutOrderNo equals c.SendOutOrderNo
                    //关联指定bom表，获取 材质、表面处理、类别、车间、
                    join d in MYDB.bomdtl
                    on new { SN = b.BomSN }
                    equals new { d.SN }
                    //关联委外计划单，获取 计划完成日期
                    join e in MYDB.sendoutplan
                    on new { d.SN } equals new { SN = e.BomSN }

                    select new
                    {
                        b.SN,
                        b.ItemNo,
                        b.ItemName,
                        b.Spec,
                        purQut = b.Quantity,
                        //已出库数
                        stockOutQut = a.Mark=="03" ? a.Quantity: 0,
                        //未出库数
                        resStockOutQut = b.Quantity - (double)(a.Mark == "03" ? a.Quantity : 0),

                        //已入库数
                        stockInQut = a.Mark == "02" ? a.Quantity: 0,
                        //未入库数
                        resStockInQut = b.Quantity - (double)(a.Mark == "02" ? a.Quantity :0 ),

                        //委外单号
                        b.SendOutOrderNo,
                        //订单号
                        b.SaleOrderNo,
                        //委外入库日期
                        a.PDate,
                        //计划完成日期
                        e.PlanFinishDate,
                        e.InputeDate,
                        //仓库
                        a.Space,
                        //材质
                        d.Material,
                        //表明处理,
                        d.SurfaceDeal,
                        //类别
                        d.Sclass,
                        //供应商
                        c.Provider,
                        //车间
                        d.WorkShop
                    };

            if (dpStart.SelectedDate.HasValue)
            {
                DateTime dtStart = dpStart.SelectedDate.Value;
                q = q.Where(u => u.PDate >= dtStart);
            }
            if (dpEnd.SelectedDate.HasValue)
            {
                DateTime dtEnd = dpEnd.SelectedDate.Value.AddDays(1);
                q = q.Where(u => u.PDate <= dtEnd);
            }

            if (tbxsearch.Text != "")
            {
                string searchtext = tbxsearch.Text;
                q = q.Where(u => u.ItemNo.Contains(searchtext) || u.Spec.Contains(searchtext) || u.ItemName.Contains(searchtext) || u.SaleOrderNo.Contains(searchtext) || u.SendOutOrderNo.Contains(searchtext) || u.Material.Contains(searchtext) || u.Provider.Contains(searchtext) || u.SurfaceDeal.Contains(searchtext) || u.Sclass.Contains(searchtext) || u.WorkShop.Contains(searchtext) || u.Space.Contains(searchtext));
            }

            foreach (string dicColKey in dictClickColsName.Keys)
            {
                string value = dictClickColsName[dicColKey].ToString();

                switch (dicColKey)
                {
                    case "ItemNo":
                        q = q.Where(u => u.ItemNo == value);
                        break;
                    case "ItemName":
                        q = q.Where(u => u.ItemName == value);
                        break;
                    case "Spec":
                        q = q.Where(u => u.Spec == value);
                        break;

                    case "SaleOrderNo":
                        q = q.Where(u => u.SaleOrderNo == value);
                        break;

                    case "PurOrderNo":
                        q = q.Where(u => u.SendOutOrderNo == value);
                        break;

                    case "Material":
                        q = q.Where(u => u.Material == value);
                        break;

                    case "Provider":
                        q = q.Where(u => u.Provider == value);
                        break;

                    case "SurfaceDeal":
                        q = q.Where(u => u.SurfaceDeal == value);
                        break;

                    case "Sclass":
                        q = q.Where(u => u.Sclass == value);
                        break;

                    case "WorkShop":
                        q = q.Where(u => u.WorkShop == value);
                        break;

                    case "Space":
                        q = q.Where(u => u.Space == value);
                        break;

                    default:
                        break;
                }
            }


            JObject summaryObject = new JObject();
            double purQut = 0;
            double stockQut = 0;
            double resStockQut = 0;
            foreach (var item in q)
            {
                purQut += (double)item.purQut;
            }
            summaryObject.Add("purQut", purQut);


            Grid1.RecordCount = q.Count();
            Grid1.SummaryData = summaryObject;
            q = SortAndPage(q, Grid1);
            Grid1.DataSource = q;
            Grid1.DataBind();

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindGrid1();
        }

        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();
        }

        protected void Grid1_PageIndexChange(object sender, FineUIPro.GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid1();
        }

        protected void Grid1_RowDoubleClick(object sender, FineUIPro.GridRowClickEventArgs e)
        {
            string[] s = Grid1.SelectedCell;

            //s[0] 选中的行id, s[1] 选中的ColumnID

            for (int i = 0; i < Grid1.Columns.Count; i++)
            {
                if (s[1] == Grid1.Columns[i].ColumnID && !dictClickColsName.ContainsKey(Grid1.Columns[i].ColumnID))
                {
                    //如果当前列名字典不包含改列名
                    dictClickColsName.Add(s[1], Grid1.Rows[e.RowIndex].Values[i].ToString());
                    break;
                    ;
                }

            }


            BindGrid1();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            //移除最后一个key
            if (dictClickColsName.Count > 0)
            {
                string lastKey = dictClickColsName.Last().Key;

                dictClickColsName.Remove(lastKey);
                BindGrid1();
            }
            else
            {
                BindGrid1();
            }
        }
    }
}