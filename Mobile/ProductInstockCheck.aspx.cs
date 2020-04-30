using AppBoxPro.mobile;
using EntityFramework.Extensions;
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
    public partial class ProductInstockCheck : PageBaseMobile
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

            var q = from a in MYDB.produceorderdetail
                    where a.BarCode == tbxbarcode.Text
                    select a;


            if (q.Count() > 0)
            {
                var res = q.FirstOrDefault();

                if (res.Status != null)
                {
                    ShowNotify("条码错误");
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

            Toolbar1.Title = "生产入库品检";
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
                result r = result.合格;
                string res = newAddedList[i]["result"].ToString();
                switch (res)
                {
                    case "合格":
                        r = result.合格;
                        break;
                    case "不合格":
                        r = result.不合格;
                        break;
                    case "特采":
                        r = result.特采;
                        break;
                }

                decimal choujianqut = decimal.Parse(newAddedList[i]["choujianqut"].ToString());
                string barcode = newAddedList[i]["barcode"].ToString();
                MYDB.produceorderdetail.Where(u => u.BarCode == barcode).Update(u => new ProduceOrderDetail
                {
                    Status = 0,
                    result = r,
                    ChoujianQut = choujianqut,
                    CheckDate = DateTime.Now
                });
            }

            MYDB.SaveChanges();

            ShowNotify("提交成功");

            //清除页面数据
            PageContext.RegisterStartupScript("clearLocalStorage()");

        }

    }
}