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
    public partial class OrderDetailNew : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "OrderAdd";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txtOrderNo.Text = GetQueryValue("od");
                txtID.Text = GetQueryValue("id");
                txtInputer.Text = User.Identity.Name;
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
                txtClinetNo.Text = current.ClientCode;

            }
        }


        #endregion

        #region Events


        private void SaveItem()
        {
            OrderDetail item = new OrderDetail();
            item.FSN = int.Parse(txtID.Text);
            item.OrderNo = txtOrderNo.Text.Trim();
            item.ClinetNo = txtClinetNo.Text.Trim();
            item.ItemNo = txtItemNo.Text.Trim();
            item.ItemName = txtItemName.Text.Trim();
            item.Quantity = int.Parse(txtQuantity.Text);
            item.Inputer = txtInputer.Text.Trim();
            item.IsNew = rbtIsNew.SelectedValue;
            item.IsPackingmaterials = rbtIspacking.SelectedValue;
            item.IsChange = rbtIsChange.SelectedValue;
            item.CountryPackVer = txtConutryVer.Text;
            item.Color = txtColor.Text;
            item.Unit = txtUnit.Text;
            item.Demand1 = tbxDemand1.Text.Trim();
            item.Demand2 = tbxDemand2.Text.Trim();
            item.Demand3 = tbxDemand3.Text.Trim();
            item.Demand4 = tbxDemand4.Text.Trim();
            item.Demand5 = tbxDemand5.Text.Trim();
            item.Demand6 = tbxDemand6.Text.Trim();
            item.Demand7 = tbxDemand7.Text.Trim();
            item.Demand8 = tbxDemand8.Text.Trim();
            item.Demand9 = tbxDemand9.Text.Trim();
            item.Demand10 = tbxDemand10.Text.Trim();
            item.Demand11 = tbxDemand11.Text.Trim();
            item.Demand12 = tbxDemand12.Text.Trim();
            item.Remark = tbxRemark.Text;
            item.IsBom = 0;
            item.InputerDate = DateTime.Now;
            using (var appdb = new AppContext())
            {
                appdb.orderdetail.Add(item);
                appdb.SaveChanges();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            string ItemNo = txtItemNo.Text.Trim();
            string ItemName = txtItemName.Text.Trim();
            string OrderNo = txtOrderNo.Text.Trim();
            //User user = DB.Users.Where(u => u.Name == inputUserName).FirstOrDefault();

            using (var appdb = new AppContext())
            {
                OrderDetail user = appdb.orderdetail.Where(u => u.ItemNo == ItemNo&&u.Color==txtColor.Text && u.OrderNo == OrderNo).FirstOrDefault();
                if (user != null)
                {
                    Alert.Show("该订单中已经有相同的产品型号或者产品名称");
                    return;
                }
            }
            try
            {
                SaveItem();
                //Alert.Show("保存成功");
                //msglab.Text = "保存成功";
                ShowNotify("保存成功", MessageBoxIcon.Success);
                string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid2');");
                PageContext.RegisterStartupScript(scripts);
                txtItemName.Text = "";
                txtQuantity.Text = "";
                txtItemNo.Text = "";
                txtItemNo.Focus();
            }
            catch (Exception ee)
            {
                Alert.Show("保存失败:" + ee.Message);
            }

        }
        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            string ItemNo = txtItemNo.Text.Trim();
            string ItemName = txtItemName.Text.Trim();
            string OrderNo = txtOrderNo.Text.Trim();
            //User user = DB.Users.Where(u => u.Name == inputUserName).FirstOrDefault();

            using (var appdb = new AppContext())
            {
                OrderDetail user = appdb.orderdetail.Where(u => u.ItemNo == ItemNo && u.OrderNo == OrderNo).FirstOrDefault();
                if (user != null)
                {
                    Alert.Show("该订单中已经有相同的产品型号或者产品名称");
                    return;
                }
            }

            try
            {
                SaveItem();
                Alert.Show("保存成功");
            }
            catch (Exception ee)
            {
                Alert.Show("保存失败:" + ee.Message);
            }
            finally
            {
                string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid2');");
                PageContext.RegisterStartupScript(scripts + Alert.GetShowInTopReference("保存成功") + ActiveWindow.GetHideReference());
            }

        }
        #endregion

        protected void txtItemNo_Blur(object sender, EventArgs e)
        {
            txtisOpen.Text = "0";
            //string openUrl = String.Format("searchitem.aspx?selected={0}", HttpUtility.UrlEncode("dddd"));

            //PageContext.RegisterStartupScript(Window1.GetSaveStateReference(txtItemNo.ClientID)
            //        + Window1.GetShowReference(openUrl));
        }


        protected void txtItemNo_TextChanged(object sender, EventArgs e)
        {
            msglab.Text = "";
            //Alert.Show(txtisOpen.Text);
            if (txtisOpen.Text == "1")
            {
                txtisOpen.Text = "0";
                return;
            }
            string openUrl = String.Format("searchitem.aspx?k={0}", HttpUtility.UrlEncode(txtItemNo.Text));
            //Window1.GetSaveStateReference(new string[] { txtItemNo.ClientID, txtItemName.ClientID })
            //+ Window1.GetShowReference(openUrl);
            PageContext.RegisterStartupScript(Window1.GetSaveStateReference(new string[] { txtItemNo.ClientID, txtItemName.ClientID, txtisOpen.ClientID })
                   + Window1.GetShowReference(openUrl) + txtItemNo.GetFocusReference());

        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            //PageContext.RegisterStartupScript("parent.__doPostBack('','RefreshGrid2');");
        }

    }
}