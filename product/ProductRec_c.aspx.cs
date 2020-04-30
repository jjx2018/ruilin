using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.product
{
    public partial class ProductRec_c : PageBase
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
            BindGrid1();
        }

        private void BindGrid1()
        {
            using (var appdb = new AppContext())
            {
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                DateTime d2 = d1.AddDays(-1);
                //d1是本月的第一天，d2本月的最后一天，
                DateTime dtstart = datePickerFrom.SelectedDate == null ? d2 : datePickerFrom.SelectedDate.Value;
                DateTime dtend = datePickerTo.SelectedDate == null ? now.AddDays(2) : datePickerTo.SelectedDate.Value.AddDays(1);
                var qqq = from a in appdb.produceorderheader
                          select a;

                //在产品名称中搜索
                string searchText = "";
                if (!String.IsNullOrEmpty(searchText))
                {
                    qqq = qqq.Where(u => u.SaleOrderNo.Contains(searchText) || u.Inputer.Contains(searchText) || u.ProOrderNo.Contains(searchText));
                }


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = qqq.Count();// q.Count();

                // 排列和数据库分页
                qqq = SortAndPage<ProduceOrderHeader>(qqq, Grid1);

                Grid1.DataSource = qqq;//
                Grid1.DataBind();
            }
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

        protected void Grid1_RowClick(object sender, FineUIPro.GridRowClickEventArgs e)
        {
            if (Grid1.SelectedRowIndex == -1)
                return;
            BindGrid2();

        }

        private void BindGrid2()
        {
            if (Grid1.Rows.Count == 0)
            {
                Grid2.RecordCount = 0;
                Grid2.DataSource = null;
                Grid2.DataBind();
                return;
            }

            if (Grid1.SelectedRowIndex < 0 || Grid1.Rows.Count <= 0)
            {
                return;
            }

            using (var appdb = new AppContext())
            {
                string ProOrderNo = Grid1.DataKeys[Grid1.SelectedRowIndex][1].ToString();

                //生产扫描明细  左关联   生产明细
                var inq = from a in appdb.produceOrderRec
                          join b in appdb.produceorderdetail
                          on a.ProduceOrderDetailSn equals b.SN
                          into a_join
                          from b in a_join.DefaultIfEmpty()
                          where b.ProOrderNo== ProOrderNo
                          select new
                          {
                              a.SN,
                              a.Role,
                              a.State,
                              a.optdate,
                              Quantity= a.Quantity==null?0:a.Quantity,
                              a.result,
                              a.choujianqut,

                              b.ProPlanNo,
                              b.ProOrderNo,
                              b.SaleOrderNo,
                              b.ProNo,
                              b.ItemNo,
                              b.ItemName,
                              b.Spec,
                              b.WorkShop,
                          };

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = inq.Count();// q.Count();

                // 排列和数据库分页
                inq = SortAndPage(inq, Grid2);

                Grid2.DataSource = inq;// itemq.Take(2);// q;
                Grid2.DataBind();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void Grid2_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {

        }

        protected void btnpass_Click(object sender, EventArgs e)
        {
             Dictionary<int,Dictionary<string,object>> modifiedDict =  Grid2.GetModifiedDict();

            if (modifiedDict.Count == 0)
            {
                ShowNotify("没有任何数据修改！");
                return;
            }

            foreach (int rowIndex in modifiedDict.Keys)
            {
                int SN = Convert.ToInt32(Grid2.DataKeys[rowIndex][0]);

                ProduceOrderRec produceOrderRec = MYDB.produceOrderRec.Where(u => u.SN == SN).FirstOrDefault();


                UpdateDataRow(modifiedDict[rowIndex], produceOrderRec);
            }

            MYDB.SaveChanges();
            BindGrid2();
        }

        private void UpdateDataRow(Dictionary<string, object> dictionary, ProduceOrderRec produceOrderRec)
        {
            //抽检数
            if (dictionary.ContainsKey("choujianqut"))
            {
                produceOrderRec.choujianqut = decimal.Parse(dictionary["choujianqut"].ToString());
            }

            if (dictionary.ContainsKey("result"))
            {
                //抽检结果
                produceOrderRec.result = (result)int.Parse(dictionary["result"].ToString());
            }

            produceOrderRec.State = true;

        }

        protected void btnnopass_Click(object sender, EventArgs e)
        {
            List<int> ids = GetSelectedDataKeyIDs(Grid2);

            MYDB.produceOrderRec.Where(u => ids.Contains(u.SN)).Update(u => new ProduceOrderRec
            {
                State = false
            });
            BindGrid2();
        }

        protected void Grid1_RowCommand(object sender, FineUIPro.GridCommandEventArgs e)
        {
            
        }

        protected void Grid2_RowCommand(object sender, FineUIPro.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int sn = GetSelectedDataKeyID(Grid2);

                MYDB.produceOrderRec.Where(u => u.SN == sn).Delete();
                BindGrid2();
            }
        }
    }
}