using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.test
{
    public partial class magconn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnEnc_Click(object sender, EventArgs e)
        {
            try
            {

                TextBox2.Text = SQLHelper.DbHelperSQL.Encrypt(TextBox1.Text);

            }
            catch (Exception ee)
            {
                Response.Write(ee.ToString());
            }
        }

        protected void btnDec_Click(object sender, EventArgs e)
        {
            TextBox1.Text = SQLHelper.DbHelperSQL.Decrypt(TextBox2.Text);
        }
    }
}