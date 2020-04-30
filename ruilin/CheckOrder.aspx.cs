using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using FineUIPro;
namespace AppBoxPro.ruilin
{
    public partial class CheckOrder : PageBase
    { 
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "OrderEdit";
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //k=0  审核   1反审核
                int k = GetQueryIntValue("k");
                
                if(k==0)
                {
                    btnSave.Text = "审核";
                    btnSaveClose.Text = "审核后关闭";
                }
                else
                {
                    btnSave.Text = "反审核";
                    btnSaveClose.Text = "反审核后关闭";
                }
                LoadData();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            //int id = GetQueryIntValue("id");
            //using (var appdb = new AppContext())
            //{
            //    Client current = appdb.clients
            //        .Where(u => u.SN == id).FirstOrDefault();
            //    if (current == null)
            //    {
            //        // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
            //        Alert.Show("参数错误！", String.Empty, ActiveWindow.GetHideReference());
            //        return;
            //    }

            //    tbxClinetNo.Text = current.ClientNo;
            //    tbxName.Text = current.Name;
            //    tbxAddress.Text = current.Address;
            //    tbxPhone.Text = current.Telephone;
            //    tbxFax.Text = current.Fax;
            //    tbxBank.Text = current.Bank;
            //    tbxAccount.Text = current.Account;
            //    tbxRemark.Text = current.Abstract;
            //    tbxsubjectcode.Text = current.subjectcode;
            //}

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                saveitem();
                Alert.Show("保存成功");
                string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid1');");
                PageContext.RegisterStartupScript(scripts);
            }
            catch (Exception ee)
            {
                Alert.Show("保存失败:" + ee.Message);
            }

        }
        private void saveitem()
        {
            using (var appdb = new AppContext())
            {
                int id = GetQueryIntValue("id");
                OrderHeader item = appdb.orderheader
               .Where(u => u.SN == id).FirstOrDefault();
                item.Remark = tbxRemark.Text;
                item.Checker = User.Identity.Name;
                item.CheckDate = DateTime.Now;
                int k = GetQueryIntValue("k");
                if (k == 0)
                {
                    item.IsCheck = 1;
                }
                else
                {
                    item.IsCheck = 0;
                }
               
                appdb.SaveChanges();
            }
        }
        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            saveitem();
            string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid1');");
            PageContext.RegisterStartupScript(scripts + ActiveWindow.GetHidePostBackReference());
           
        }

    }
}