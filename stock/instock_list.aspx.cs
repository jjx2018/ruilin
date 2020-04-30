using EntityFramework.Extensions;
using FineUIPro;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.stock
{
    public partial class instock_list : PageBase
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
            BindForm();
            BindGrid();
        }

        private void BindForm()
        {
            //int SN = GetQueryIntValue("SN");

            //var q = MYDB.PurchaseStockLists.Where(u => u.SN == SN).FirstOrDefault();
            //if (q != null)
            //{
            //    lbDate.Text = ((DateTime)q.PDate).ToString("yyyy-MM-dd HH:mm:ss");
            //    lbJinban.Text = q.JinBanRen;
            //    lbremark.Text = q.remark;
            //    lbOrderno.Text = q.StockOrderNo;

            //    if (!string.IsNullOrEmpty(q.PurOrderNo))
            //    {
            //        lbpurorderno.Hidden = false;
            //        lbpurorderno.Text = q.PurOrderNo;
            //    }
            //}
        }

        private void BindGrid()
        {
            //int SN = GetQueryIntValue("SN");
            //var q = from a in MYDB.PurchaseStockLists
            //        where a.Stock.SN == SN
            //        select new
            //        {
            //            a.ItemNo,
            //            a.ItemName,
            //            a.Spec,
            //            a.Quantity,
            //            a.Remark,
            //            a.Space,
            //        };

            //JObject jObject = new JObject();
            //jObject.Add("Quantity", q.Sum(u => u.Quantity));
            //Grid1.SummaryData = jObject;

            //Grid1.DataSource = q;
            //Grid1.DataBind();
        }

        protected void btnCanel_Click(object sender, EventArgs e)
        {
            //int SN = GetQueryIntValue("SN");

            //int count=  MYDB.stock.Where(u => u.SN == SN).Update(u => new Stock { isCancel = true });

            //if (count > 0)
            //{
            //    Alert.Show("作废成功!");
            //}
            //else
            //{
            //    Alert.Show("作废失败!");
            //}

        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            btnprint.Hidden = true;
            PageContext.RegisterStartupScript("preview()");
            btnprint.Hidden = false;
        }
    }
}