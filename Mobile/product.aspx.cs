using AppBoxPro.mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.Mobile
{
    public partial class product : PageBaseMobile
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

            var q = from a in MYDB.produceorderdetail
                    select a;


            if (tbxbarcode.Text != "")
            {
                q = q.Where(u => u.BarCode == tbxbarcode.Text);
            }
            else
            {
                ShowNotify("条码不能为空");
                return;
            }

            if (q.Count() > 0)
            {
                var res = q.FirstOrDefault();
                tbxproductno.Text = res.ProOrderNo;
                tbxitemname.Text = res.ItemName;
                tbxitemno.Text = res.ItemNo;
                tbxspec.Text = res.Spec;
                nbPurQut.Text = res.Quantity.ToString();
                tbxSN.Text = res.SN.ToString();
                nbQut.Text = res.Quantity.ToString();
            }
            else
            {
                ShowNotify("查无此物料");
                tbxproductno.Text = "";
                tbxitemname.Text ="";
                tbxitemno.Text = "";
                tbxspec.Text = "";
                nbPurQut.Text = "";
                tbxSN.Text = "";
                nbQut.Text = "";
                return;
            }
            //已入库数
        }

        private void LoadData()
        {
            Toolbar1.Title = Enum.GetName(typeof(role), GetQueryIntValue("role"));
        }

        protected void tbxprosn_Blur(object sender, EventArgs e)
        {
            BindForm();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            //首先查看该生产单的该物料是否已经品检完了
            int ProduceOrderDetailSn = int.Parse(tbxSN.Text);

            //在扫描记录里面查询  ProduceOrderRec

            var q = MYDB.produceOrderRec.Where(u => u.ProduceOrderDetailSn == ProduceOrderDetailSn);

            //存在有审核状态未通过的，都不能够提交    或者    存在还未进行审核的
            if (q.Any(u => u.State == false) || q.Any(u => u.State == null))
            {
                ShowNotify("存在未通过品检的工序，不能提交！");
                return;
            }

            //提交的数量
            int subcount = int.Parse(nbQut.Text);

            //提交成功
            using (var db = new AppContext())
            {
                ProduceOrderRec item = new ProduceOrderRec();

                item.ProduceOrderDetailSn = ProduceOrderDetailSn;

                item.optdate = DateTime.Now;
                item.Quantity = subcount;
                item.Role = (role)GetQueryIntValue("role");


                item.State = null;

                db.produceOrderRec.Add(item);
                db.SaveChanges();

                ShowNotify("提交成功");

                tbxitemno.Text = "";
                tbxitemname.Text = "";
                tbxspec.Text = "";

                nbPurQut.Text = "";
                nbQut.Text = "";
            }
        }
    }
}