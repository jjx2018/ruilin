using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using FineUIPro;

namespace AppBoxPro.BomMag
{
    public partial class addRemark : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "ClientNew";
            }
        }

        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHidePostBackReference();

        }

        #region Events


        private void SaveItem()
        {
            int id = GetQueryIntValue("id");
            string t = GetQueryValue("t");
            using (var appdb = new AppContext())
            {
                if (t == "pmh")
                {
                    ProBomHeader pmh = appdb.probombase.Where(u => u.SN == id).FirstOrDefault();
                    pmh.Remark = tbxRemark.Text.Trim();
                    appdb.SaveChanges();
                }
                else if (t == "pmd")
                {
                    ProBomDetail pmd = appdb.probomdtl.Where(u => u.SN == id).FirstOrDefault();
                    pmd.Remark = tbxRemark.Text.Trim();
                    appdb.SaveChanges();
                }
                else if(t== "probmd")
                {
                    BomDetail pmd = appdb.bomdtl.Where(u => u.SN == id).FirstOrDefault();
                    pmd.Remark = tbxRemark.Text.Trim();
                    pmd.IsValid = 1;
                    appdb.SaveChanges();
                }

            }

        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                SaveItem();
                Alert.Show("保存成功");
            }
            catch (Exception ee)
            {
                Alert.Show("保存失败:" + ee.Message);
            }

        }
        #endregion
    }
}