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
    public partial class purchaseinstocknew : PageBase
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
            BindGrid();

            int SN = GetQueryIntValue("SN");

            //double q = (double)MYDB.purchaseorderDetail.Where(u => u.SN == SN).FirstOrDefault().PassQut;

            //nbQut.Text = q.ToString();

            var qq = from a in MYDB.storehouse select a ;

            ddlstorehouse.DataSource = qq;
            ddlstorehouse.DataTextField = "StoreName";
            ddlstorehouse.DataValueField = "StoreName";
            ddlstorehouse.DataBind();

        }

        private void BindGrid()
        {
            //该采购单，该物料的入库记录
            int SN = GetQueryIntValue("SN");

            var q = MYDB.purchaseorderDetail.Where(u => u.SN == SN).FirstOrDefault();

            string purno = q.PurOrderNo;
            string itemno = q.ItemNo;

            var qq = MYDB.PurchaseStockLists.Where(u => u.PurOrderNo == purno && u.ItemNo == itemno);

            Grid1.DataSource = qq;
            Grid1.DataBind();

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            int SN = GetQueryIntValue("SN");

            var q = MYDB.purchaseorderDetail.Where(u => u.SN == SN).FirstOrDefault();

            PurchaseStockList item = new PurchaseStockList();
            item.PurOrderNo = q.PurOrderNo;
            item.StockOrderNo = q.SaleOrderNo;
            item.ProNo = q.ProNo;
            item.ItemNo = q.ItemNo;
            item.ItemName = q.ItemName;
            item.Spec = q.Spec;
            item.Mark = "02";
            item.Quantity = decimal.Parse(nbQut.Text);
            item.Unit = q.Unit;
            item.PDate = DateTime.Now;
            item.Remark = tbxRemark.Text;
            item.Space = ddlstorehouse.SelectedText;
            item.PurchaseProperties = PurchaseProperties.采购入库;

            MYDB.PurchaseStockLists.Add(item);
            MYDB.SaveChanges();

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int SN = GetSelectedDataKeyID(Grid1);
                MYDB.PurchaseStockLists.Where(u => u.SN == SN).Delete();
                BindGrid();
            }
        }
    }
}