using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.ruilin
{
    public partial class showpdf : System.Web.UI.Page
    {
        public string pdf = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            pdf = "../pdf/" + Request.QueryString["f"];
            //Response.Write(pdf);
        }
    }
}