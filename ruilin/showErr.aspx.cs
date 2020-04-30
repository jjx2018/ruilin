using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using FineUIPro;

namespace AppBoxPro.ruilin
{
    public partial class showErr : PageBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger("magBom.aspx");
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Grid1.DataSource = (DataTable)Session["errdt"];// itemq.Take(2);// q;
                Grid1.DataBind();
                Grid2.DataSource = (DataTable)Session["errwldt"];// itemq.Take(2);// q;
                Grid2.DataBind();
            }
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(Grid2.SelectedRowIndexArray.Length==0)
                {
                    Alert.Show("请选择要更新的物料");
                    return;
                }
                string sql = "";
                ArrayList al = new ArrayList();
                foreach(int i in Grid2.SelectedRowIndexArray)
                {
                    sql = "update rlitems set itemname='" + Grid2.Rows[i].Values[3].ToString() + "',Spec='" + Grid2.Rows[i].Values[4].ToString() + "',Material='" + Grid2.Rows[i].Values[5].ToString() + "',SurfaceDeal='" + Grid2.Rows[i].Values[6].ToString() + "' where itemno='" + Grid2.Rows[i].Values[2].ToString()+"'";
                    al.Add(sql);
                    log.Info("sql update rlitems:::" + sql);
                }
                if (al.Count>0&&SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                {
                    Alert.Show("更新成功");
                }
                else
                {
                    Alert.Show("更新失败");
                }
            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
        }

        protected void Grid2_RowSelect(object sender, GridRowSelectEventArgs e)
        {
            //if(Grid2.Rows[e.RowIndex].Values[2].ToString()=="物料表")
            //{
            //    Grid2.Rows[e.RowIndex].RowSelectable = false;
                
            //}
        }

        protected void Grid2_RowDataBound(object sender, GridRowEventArgs e)
        {
            DataRowView row = e.DataItem as DataRowView;
            string wlFrom = row["wlFrom"].ToString() ;

            if (wlFrom=="物料表")
            {
                e.RowSelectable = false;
                e.RowCssClass = "color1";
            }
        }
    }
}