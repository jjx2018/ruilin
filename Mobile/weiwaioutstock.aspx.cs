using AppBoxPro.mobile;
using FineUIPro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.Mobile
{
    public partial class weiwaioutstock : PageBaseMobile
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
            else
            {
                string arg = GetRequestEventArgument();
                if (arg == "tbxprosn__change")
                {
                    BindForm();
                }
            }
        }

        private void BindForm()
        {
            if (tbxbarcode.Text == "")
            {
                return;
            }

            var q = from a in MYDB.sendoutprocessdetail
                    where a.BarCode == tbxbarcode.Text
                    select a;


            if (q.Count() > 0)
            {
                var res = q.FirstOrDefault();

                tbxprosn.Text = res.SendOutOrderNo;
                tbxitemno.Text = res.ItemNo;
                tbxprono.Text = res.ProNo;
                tbxitemname.Text = res.ItemName;
                tbxspec.Text = res.Spec;
                nbPurQut.Text = res.Quantity.ToString();
                nbQut.Text = res.Quantity.ToString();
                tbxunit.Text = res.Unit;
                tbxsaleno.Text = res.SaleOrderNo;
            }
            else
            {
                ShowNotify("查无此物料");
                form2.Reset();
                return;
            }
        }

        private void LoadData()
        {
            //库位
            var qq = from a in MYDB.storehouse select a;

            ddlstorehouse.DataSource = qq;
            ddlstorehouse.DataTextField = "StoreName";
            ddlstorehouse.DataValueField = "StoreName";
            ddlstorehouse.DataBind();
        }

        protected void tbxprosn_Blur(object sender, EventArgs e)
        {
            BindForm();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string mark = GetQueryValue("mark");

            List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();

            if (newAddedList.Count == 0)
            {
                ShowNotify("没有要提交的数据");
                return;
            }

            StockHeader stockHeader = new StockHeader();
            if (mark == "02")
            {
                stockHeader.title = "委外进货单";
            }
            else if (mark == "03")
            {
                stockHeader.title = "委外出货单";
            }

            stockHeader.optdate = DateTime.Now;
            //执行存储过程获取流水号
            SQLHelper.DbHelperSQL.SetConnectionString("");
            object[] obj = new object[] { "WW" };
            DataTable dt = SQLHelper.DbHelperSQL.ExecuteProc_ReturnDataTable("GetSeq", obj, 10);
            string prosn = dt.Rows[0][0].ToString();
            string StockHeaderProsn = "WW" + prosn;
            ShowNotify(StockHeaderProsn);
            stockHeader.StockHeaderProsn = StockHeaderProsn;
            stockHeader.jingbanren = GetChineseName();

            MYDB.StockHeaders.Add(stockHeader);

            for (int i = 0; i < newAddedList.Count; i++)
            {
                SendOutStockList item = new SendOutStockList();
                item.Barcode = newAddedList[i]["barcode"].ToString();

                //订单号
                item.StockOrderNo = newAddedList[i]["saleno"].ToString();

                item.SendOutOrderNo = newAddedList[i]["orderno"].ToString();
                item.ItemNo = newAddedList[i]["itemno"].ToString();
                item.ProNo = newAddedList[i]["prono"].ToString();
                item.ItemName = newAddedList[i]["itemname"].ToString();
                item.Spec = newAddedList[i]["spec"].ToString();

                item.Quantity = decimal.Parse(newAddedList[i]["qut"].ToString());
                item.Space = newAddedList[i]["space"].ToString();
                item.Remark = newAddedList[i]["remark"].ToString();
                if (mark == "02")
                {
                    item.SendOutProperties = SendOutProperties.委外入库;
                }
                else
                {
                    item.SendOutProperties = SendOutProperties.委外出库;
                }

                item.Mark = mark;
                item.Unit = newAddedList[i]["unit"].ToString();
                item.PDate = DateTime.Now;
                item.StockHeaderProsn = StockHeaderProsn;

                MYDB.SendOutStockLists.Add(item);

            }
            MYDB.SaveChanges();

            ShowNotify("提交成功");

            //清除页面数据
            PageContext.RegisterStartupScript("clearLocalStorage()");

        }

    }
}