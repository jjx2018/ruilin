using EntityFramework.Extensions;
using FineUIPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.stock
{
    public partial class instock : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            btnNew.OnClientClick = Window1.GetShowReference("instock_new.aspx", "新增入库");
            BindGrid1();
        }

        private void BindGrid1()
        {
            var q = from a in MYDB.PurchaseStockLists
                    where a.StockOrderNo.StartsWith("IN")
                    select a;

            if (dpstart.SelectedDate.HasValue && dpend.SelectedDate.HasValue)
            {
                DateTime dt1 = dpstart.SelectedDate.Value;
                DateTime dt2 = dpend.SelectedDate.Value.AddDays(1);

                q = q.Where(u => u.PDate >= dt1 && u.PDate <= dt2);

            }

            if (!string.IsNullOrEmpty(ttbxorderno.Text))
            {
                q = q.Where(u => u.StockOrderNo.Contains(ttbxorderno.Text));
            }

            Grid1.RecordCount = q.Count();

            q = SortAndPage(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        protected void Window1_Close(object sender, FineUIPro.WindowCloseEventArgs e)
        {
            BindGrid1();
        }

        protected void ttbxorderno_Trigger1Click(object sender, EventArgs e)
        {
            ttbxorderno.Text = "";
            ttbxorderno.ShowTrigger1 = false;
            BindGrid1();
        }

        protected void ttbxorderno_Trigger2Click(object sender, EventArgs e)
        {
            ttbxorderno.ShowTrigger1 = true;
            BindGrid1();
        }

        protected void Grid1_RowCommand(object sender, FineUIPro.GridCommandEventArgs e)
        {
            List<int> ids = GetSelectedDataKeyIDs(Grid1);
            //删除明细
            //MYDB.PurchaseStockLists.Where(u => ids.Contains(u.Stock.SN)).Delete();
            //删除总单
            //MYDB.stock.Where(u => ids.Contains(u.SN)).Delete();
            Alert.Show("删除成功!");
            BindGrid1();
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();
        }
    }
}