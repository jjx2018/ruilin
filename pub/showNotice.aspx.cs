using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.pub
{
    public partial class showNotice : System.Web.UI.Page
    {
        public string stitle = "";
        public string scontent = "";
        public string sdate = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                gettopgg();
            }
        }
        private void gettopgg()
        {
            
            try
            {

                 


                SQLHelper.DbHelperSQL.SetConnectionString("");
                string sql = "";
                sql = @"select top 1 id,ggname,ggcontent,CONVERT(varchar(100), pbdate, 23) pbdate,month(pbdate) sm,day(pbdate) sd,pbusername from dbo.Notice a 
where   state=1 and CONVERT(varchar(100), GETDATE(), 23) between CONVERT(varchar(100), startdate, 23) and CONVERT(varchar(100),enddate, 23) order by id desc ";

                


                DataTable dt = SQLHelper.DbHelperSQL.ReturnDataTable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    stitle = dt.Rows[0]["ggname"].ToString();
                    scontent = dt.Rows[0]["ggcontent"].ToString();
                    sdate =DateTime.Parse( dt.Rows[0]["pbdate"].ToString()).ToString("yyyy年M月d日");
                }
                 
            }
            catch (Exception ee)
            {

                Response.Write(ee.ToString());
            }
            
        }

    }
}