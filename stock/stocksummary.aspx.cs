using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.stock
{
    public partial class stocksummary : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //本月第一天
                dpStart.SelectedDate = DateTime.Today.AddDays(1 - DateTime.Now.Day).Date;
                //本日
                dpEnd.SelectedDate = DateTime.Now;


                BindGrid();
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            DateTime dtStart = dpStart.SelectedDate == null ? DateTime.Today.AddMonths(-12) : dpStart.SelectedDate.Value;
            DateTime dtEnd = dpEnd.SelectedDate == null ? DateTime.Now.AddDays(1) : dpEnd.SelectedDate.Value.AddDays(1);

            var allitem = from a in MYDB.allitems
                          select a;

            //上月结存
            var shangyue = from p in MYDB.PurchaseStockLists
                           where p.PDate < dtStart
                           group p by new
                           {
                               p.ItemNo,
                               p.Space
                           }
                         into g
                           select new
                           {
                               ItemNo = g.Key.ItemNo,
                               Space = g.Key.Space,
                               shangyuejincang = g.Sum(p => p.Mark == "02" ? p.Quantity : 0),
                               shangyuechucang = g.Sum(p => p.Mark == "03" ? p.Quantity : 0),
                           };

            //本月库存  品检通过的
            var benyue = from p in MYDB.PurchaseStockLists
                         where p.PDate >= dtStart && p.PDate <= dtEnd
                         group p by new
                         {
                             p.ItemNo,
                             p.Space
                         }
                         into g
                         select new
                         {
                             ItemNo = g.Key.ItemNo,
                             Space = g.Key.Space,
                             benyuejincang = g.Sum(p => p.Mark == "02" ? p.Quantity : 0),
                             benyuechucang = g.Sum(p => p.Mark == "03" ? p.Quantity : 0),
                         };


            var q = from p in MYDB.allitems
                    join s in shangyue
                    on p.ItemNo equals s.ItemNo into
                    s_join

                    from s in s_join.DefaultIfEmpty()
                    join b in benyue

                    on p.ItemNo equals b.ItemNo into
                    b_join
                    from b in b_join.DefaultIfEmpty()
                    select new
                    {
                        p.ItemNo,
                        p.ItemName,
                        p.Spec,
                        b.Space,
                        shangyuekucun = (s.shangyuejincang - s.shangyuechucang) == null ? 0 : s.shangyuejincang - s.shangyuechucang,
                        benyuekucun = (b.benyuejincang - b.benyuechucang) == null ? 0 : b.benyuejincang - b.benyuechucang,
                        benyuejincang = b.benyuejincang == null ? 0 : b.benyuejincang,
                        benyuechucang = b.benyuechucang == null ? 0 : b.benyuechucang,

                        //期末
                        qimokucun = ((s.shangyuejincang - s.shangyuechucang) == null ? 0 : s.shangyuejincang - s.shangyuechucang) + ((b.benyuejincang - b.benyuechucang) == null ? 0 : b.benyuejincang - b.benyuechucang)

                    };

            if (tbxSearch.Text != "")
            {
                string text = tbxSearch.Text;
                q = q.Where(u => u.ItemNo.Contains(text) || u.ItemName.Contains(text) || u.Spec.Contains(text));
            }

            Decimal s1 = 0;
            Decimal s2 = 0;
            Decimal s3 = 0;
            Decimal s4 = 0;

            foreach (var item in q)
            {
                s1 += (Decimal)item.shangyuekucun;
                s2 += (Decimal)item.benyuejincang;
                s3 += (Decimal)item.benyuechucang;
                s4 += (Decimal)item.qimokucun;
            }

            JObject jObject = new JObject();
            jObject.Add("s1", s1);
            jObject.Add("s2", s2);
            jObject.Add("s3", s3);
            jObject.Add("s4", s4);

            Grid1.SummaryData = jObject;

            Grid1.DataSource = q;
            Grid1.DataBind();


        }
    }
}