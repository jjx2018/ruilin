using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using FineUIPro;

namespace AppBoxPro.ProviderMag
{
    public partial class ProviderEdit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "ProviderEdit";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            int id = GetQueryIntValue("id");
            using (var appdb = new AppContext())
            {
                Provider current = appdb.providers
                    .Where(u => u.SN == id).FirstOrDefault();
                if (current == null)
                {
                    // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                    Alert.Show("参数错误！", String.Empty, ActiveWindow.GetHideReference());
                    return;
                }

                tbxClinetNo.Text = current.ProviderNo;
                tbxName.Text = current.Name;
                tbxAddress.Text = current.Address;
                tbxPhone.Text = current.Telephone;
                tbxFax.Text = current.Fax;
                ddlRank.SelectedValue = current.Rank;
                ddlType.SelectedValue=  current.Stype;
                tbxRemark.Text = current.Abstract;
                tbxsubjectcode.Text = current.subjectcode;
                tbxContactman.Text = current.ContactMan;
                tbxEmail.Text = current.Email;
                tbxPurchaseMan.Text = current.PurchaseMan;
                rbtIsValid.SelectedValue = current.IsValid;
            }

        }


        #endregion

        #region Events

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            int id = GetQueryIntValue("id");
            using (var appdb = new AppContext())
            {
                Provider item = appdb.providers
                    .Where(u => u.SN == id).FirstOrDefault();
                item.Name = tbxName.Text.Trim();
                item.Address = tbxAddress.Text.Trim();
                item.Telephone = tbxPhone.Text.Trim();
                item.Fax = tbxFax.Text.Trim();
                item.Rank = ddlRank.SelectedValue;
                item.Stype = ddlType.SelectedValue;
                item.Abstract = tbxRemark.Text.Trim();
                 
                item.ContactMan = tbxContactman.Text;
                item.Email = tbxEmail.Text;
                item.PurchaseMan = tbxPurchaseMan.Text;
                item.IsValid = rbtIsValid.SelectedValue;



                appdb.SaveChanges();

            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #endregion

    }
}