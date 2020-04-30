using FineUIPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.stock
{
    public partial class instock_new_select :PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BindGrid();
            }
        }

        private void BindGrid()
        {
            var q = from a in MYDB.allitems select a;
            if (tbxName.Text != "")
            {
                q = q.Where(u => u.ItemNo.Contains(tbxName.Text) || u.ItemName.Contains(tbxName.Text) || u.Spec.Contains(tbxName.Text));
            }

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}