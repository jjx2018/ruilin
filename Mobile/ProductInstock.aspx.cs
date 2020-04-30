using AppBoxPro.mobile;
using EntityFramework.Extensions;
using FineUIPro;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.Mobile
{
    public partial class ProductInstock : PageBaseMobile
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
            string status = GetQueryValue("status");
            if (tbxbarcode.Text == "")
            {
                return;
            }

            var q = from a in MYDB.produceorderdetail
                    where a.BarCode == tbxbarcode.Text
                    select a;


            if (q.Count() > 0)
            {
                var res = q.FirstOrDefault();

                if (res.Status ==null)
                {
                    ShowNotify("该条码未品检");
                    return;
                }

                if (res.Status == 1)
                {
                    ShowNotify("该条码已入库");
                    return;
                }

                if (res.result == result.不合格)
                {
                    ShowNotify("该条码品检不合格");
                    return;
                }

                tbxprosn.Text = res.ProOrderNo;
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


            //岗位
            string status = GetQueryValue("status");
            Toolbar1.Title = "生产入库";
        }

        protected void tbxprosn_Blur(object sender, EventArgs e)
        {
            BindForm();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();

            if (newAddedList.Count == 0)
            {
                ShowNotify("没有要提交的数据");
                return;
            }


            for (int i = 0; i < newAddedList.Count; i++)
            {
                string barcode = newAddedList[i]["barcode"].ToString();
                MYDB.produceorderdetail.Where(u => u.BarCode == barcode).Update(u => new ProduceOrderDetail
                {
                    Status = 1
                });
            }

            StockHeader stockHeader = new StockHeader();
            stockHeader.title = "生产入库单";
            stockHeader.optdate = DateTime.Now;
            //执行存储过程获取流水号
            SQLHelper.DbHelperSQL.SetConnectionString("");
            object[] obj = new object[] { "SC" };
            DataTable dt = SQLHelper.DbHelperSQL.ExecuteProc_ReturnDataTable("GetSeq", obj, 10);
            string prosn = dt.Rows[0][0].ToString();
            string StockHeaderProsn = "SC" + prosn;
            ShowNotify(StockHeaderProsn);
            stockHeader.StockHeaderProsn = StockHeaderProsn;
            stockHeader.jingbanren = GetChineseName();

            MYDB.StockHeaders.Add(stockHeader);

            for (int i = 0; i < newAddedList.Count; i++)
            {
                ProductStockList item = new ProductStockList();

                string barcode = newAddedList[i]["barcode"].ToString();
                var prodetail = MYDB.produceorderdetail.Where(u => u.BarCode == barcode).FirstOrDefault();

                item.Barcode = newAddedList[i]["barcode"].ToString();
                //订单号
                item.StockOrderNo = newAddedList[i]["saleno"].ToString();

                item.ProductOrderNo = newAddedList[i]["orderno"].ToString();
                item.ItemNo = newAddedList[i]["itemno"].ToString();
                item.ProNo = newAddedList[i]["prono"].ToString();
                item.ItemName = newAddedList[i]["itemname"].ToString();
                item.Spec = newAddedList[i]["spec"].ToString();

                //item.ChoujianQut = purdetail.ChoujianQut;
                //item.checkDate = purdetail.CheckDate;

                item.Quantity = decimal.Parse(newAddedList[i]["qut"].ToString());
                item.Space = newAddedList[i]["space"].ToString();
                item.Remark = newAddedList[i]["remark"].ToString();
                item.ProductProperties = ProductProperties.生产入库;
                item.Mark = "02";
                item.Unit = newAddedList[i]["unit"].ToString();
                item.PDate = DateTime.Now;
                item.StockHeaderProsn = StockHeaderProsn;

                MYDB.ProductStockLists.Add(item);

            }

            MYDB.SaveChanges();

            ShowNotify("提交成功");

            //清除页面数据
            PageContext.RegisterStartupScript("clearLocalStorage()");

        }

    }
}