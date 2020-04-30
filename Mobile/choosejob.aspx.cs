using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.mobile
{
    public partial class choosejob : PageBaseMobile
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataList1();
            }
        }

        private void BindDataList1()
        {
            //string username = GetIdentityName();
            //var q = DB.Users.Where(u => u.Name == username).FirstOrDefault().Roles;

            //var qqq = from a in q
            //          select new RoleName
            //          {
            //              Name = a.Name
            //          };

            //DataList1.DataSource = qqq;
            //DataList1.DataBind();
        }


        protected void DataList1_ItemDataBound(object sender, FineUIPro.DataListItemEventArgs e)
        {
            RoleName row = e.DataItem as RoleName;

            string name = row.Name;

            e.Item.Text = row.Name;

            //备货确认岗位
            if (row.Name == "备货确认")
            {
                e.Item.NavigateUrl = String.Format("maginstruction.aspx");
            }

            //生长
            else
            {
                e.Item.NavigateUrl = String.Format("product.aspx?name=" + name);
            }
            e.Item.Target = "_self";
            e.Item.ShowArrow = true;
        }

        private class RoleName
        {
            public string Name { get; set; }
        }
    }
}