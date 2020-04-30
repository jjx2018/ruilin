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
    public partial class weiwaioutstock_new : PageBase
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

            double q = (double)MYDB.sendoutprocessdetail.Where(u => u.SN == SN).FirstOrDefault().Quantity;

            nbQut.Text = q.ToString();

            var qq = from a in MYDB.storehouse select a;

            ddlstorehouse.DataSource = qq;
            ddlstorehouse.DataTextField = "StoreName";
            ddlstorehouse.DataValueField = "StoreName";
            ddlstorehouse.DataBind();
        }

        private void BindGrid()
        {
            //该采购单，该物料的入库记录
            int SN = GetQueryIntValue("SN");
            string Mark = GetQueryValue("mark");

            var q = MYDB.sendoutprocessdetail.Where(u => u.SN == SN).FirstOrDefault();

            string sendno = q.SendOutOrderNo;
            string itemno = q.ItemNo;

            var qq = MYDB.SendOutStockLists.Where(u => u.SendOutOrderNo == sendno && u.ItemNo == itemno&&u.Mark==Mark);

            Grid1.DataSource = qq;
            Grid1.DataBind();

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            int SN = GetQueryIntValue("SN");

            string mark = GetQueryValue("mark");

            var q = MYDB.sendoutprocessdetail.Where(u => u.SN == SN).FirstOrDefault();

            SendOutStockList item = new SendOutStockList();
            item.SendOutOrderNo = q.SendOutOrderNo;
            item.ProNo = q.ProNo;
            item.ItemNo = q.ItemNo;
            item.ItemName = q.ItemName;
            item.Spec = q.Spec;
            item.Mark = mark;
            item.Quantity = decimal.Parse(nbQut.Text);
            item.Unit = q.Unit;
            item.PDate = DateTime.Now;
            item.Remark = tbxRemark.Text;
            item.Space = ddlstorehouse.SelectedText;
            if (mark == "02")
            {
                item.SendOutProperties = SendOutProperties.委外入库;
            }
            else if (mark == "03")
            {
                item.SendOutProperties = SendOutProperties.委外出库;
            }
            

            MYDB.SendOutStockLists.Add(item);
            MYDB.SaveChanges();

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int SN = GetSelectedDataKeyID(Grid1);
                MYDB.SendOutStockLists.Where(u => u.SN == SN).Delete();
                BindGrid();
            }
        }
    }
}