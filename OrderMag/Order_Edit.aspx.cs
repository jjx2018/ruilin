using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using FineUIPro;

namespace AppBoxPro.OrderMag
{
    public partial class Order_Edit : PageBase
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
                OrderHeader current = appdb.orderheader
                    .Where(u => u.SN == id).FirstOrDefault();
                if (current == null)
                {
                    // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                    Alert.Show("参数错误！", String.Empty, ActiveWindow.GetHideReference());
                    return;
                }
                txtOrderNo.Text = current.OrderNo;
                txtClientCode.Text = current.ClientCode;
                txtClinetOrderNo.Text = current.ClientOrderNo;
                txtBuis.Text = current.RecOrderPerson;
                txtContainerType.Text = current.ContainerType;
                //txtChecker.Text = current.Checker;
                checkDate.SelectedDate = current.CheckDate;
                recOrderDate.SelectedDate = current.RecOrderDate;
                outOrderDate.SelectedDate = current.OutGoodsDate;
                txtLotno.Text = current.LotNo;
                txtOrderType.Text = current.OrderType;
            }

        }


        #endregion

        #region Events

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                int id = GetQueryIntValue("id");
                using (var appdb = new AppContext())
                {
                    OrderHeader item = appdb.orderheader
                        .Where(u => u.SN == id).FirstOrDefault();
                    item.ClientOrderNo = txtClinetOrderNo.Text;
                    item.LotNo = txtLotno.Text;
                    item.ClientCode = txtClientCode.Text;
                    item.RecOrderPerson = txtBuis.Text;// GetChineseName(User.Identity.Name);
                    item.Updater = User.Identity.Name;
                    item.UpdateDate = DateTime.Now;
                    item.RecOrderDate = recOrderDate.SelectedDate;
                    item.CheckDate = checkDate.SelectedDate;
                    item.OutGoodsDate = outOrderDate.SelectedDate;
                    item.ContainerType = txtContainerType.Text;
                    //item.Checker = txtChecker.Text;
                    item.OrderType = txtOrderType.Text;
                    
                    appdb.SaveChanges();

                }
                string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid1');");
                PageContext.RegisterStartupScript(scripts + Alert.GetShowInTopReference("保存成功") + ActiveWindow.GetHideReference());
            }
            catch (Exception ee)
            {
                PageContext.RegisterStartupScript(Alert.GetShowInTopReference("保存失败") + ActiveWindow.GetHideReference());

            }
        }

        #endregion
    }
}