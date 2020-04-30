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
    public partial class OrderDetailedit : PageBase
    {
        log4net.ILog log = log4net.LogManager.GetLogger("magPlan.aspx");
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
                string k = GetQueryValue("k");
                log.Info("p:::::" + GetQueryValue("p"));
                if (k == "1")
                {
                    btnSave.Hidden = true;
                    btnSaveClose.Hidden = true;
                    foreach (Control ctrl in SimpleForm1.Controls)
                    {

                        if (ctrl.GetType().Name == "FormRow")
                        {
                            foreach (Control subctrl in ctrl.Controls)
                            {
                                //Alert.Show(subctrl.GetType().Name);
                                if (subctrl.GetType().Name == "TextBox")
                                {
                                    FineUIPro.TextBox tb = (FineUIPro.TextBox)subctrl;
                                    tb.Readonly = true;
                                }
                                else if (subctrl.GetType().Name == "NumberBox")
                                {
                                    NumberBox numbox = (NumberBox)subctrl;
                                    numbox.Readonly = true;
                                }
                                else if (subctrl.GetType().Name == "TextArea")
                                {
                                    FineUIPro.TextArea tb = (FineUIPro.TextArea)subctrl;
                                    tb.Readonly = true;
                                }
                            }
                        }
                    }
                }
                else
                {

                }
                txtID.Text = GetQueryValue("id");
                using (var appdb = new AppContext())
                {
                    int sn = int.Parse(txtID.Text);
                    OrderDetail current = appdb.orderdetail
                        .Where(u => u.SN == sn).FirstOrDefault();
                    if (current == null)
                    {
                        // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                        Alert.Show("参数错误！", String.Empty, ActiveWindow.GetHideReference());
                        return;
                    }
                    txtOrderNo.Text = current.OrderNo;
                    txtItemNo.Text = current.ItemNo;
                    txtItemName.Text = current.ItemName;
                    txtClinetNo.Text = current.ClinetNo;
                    txtQuantity.Text = current.Quantity.ToString();
                    txtColor.Text = current.Color;
                    txtUnit.Text = current.Unit;
                    rbtIsChange.SelectedValue = current.IsChange.Trim();
                    rbtIsNew.SelectedValue = current.IsNew.Trim();
                    rbtIspacking.SelectedValue = current.IsPackingmaterials.Trim();
                    txtConutryVer.Text = current.CountryPackVer;
                    txtInputer.Text = current.Inputer;
                    tbxDemand1.Text = current.Demand1;
                    tbxDemand10.Text = current.Demand10;
                    tbxDemand11.Text = current.Demand11;
                    tbxDemand12.Text = current.Demand12;
                    tbxDemand2.Text = current.Demand2;
                    tbxDemand3.Text = current.Demand3;
                    tbxDemand4.Text = current.Demand4;
                    tbxDemand5.Text = current.Demand5;
                    tbxDemand6.Text = current.Demand6;
                    tbxDemand7.Text = current.Demand7;
                    tbxDemand8.Text = current.Demand8;
                    tbxDemand9.Text = current.Demand9;
                    tbxRemark.Text = current.Remark;
                }
                LoadData();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

        }


        #endregion

        #region Events


        private void SaveItem()
        {
            using (var appdb = new AppContext())
            {
                int sn = int.Parse(txtID.Text);
                OrderDetail item = appdb.orderdetail
                    .Where(u => u.SN == sn).FirstOrDefault();
                item.OrderNo = txtOrderNo.Text.Trim();
                item.ClinetNo = txtClinetNo.Text.Trim();
                item.ItemNo = txtItemNo.Text.Trim();
                item.ItemName = txtItemName.Text.Trim();
                item.Quantity = int.Parse(txtQuantity.Text);
                item.IsNew = rbtIsNew.SelectedValue;
                item.IsPackingmaterials = rbtIspacking.SelectedValue;
                item.IsChange = rbtIsChange.SelectedValue;
                item.CountryPackVer = txtConutryVer.Text;
                item.Color = txtColor.Text;
                item.Unit = txtUnit.Text;
                item.Inputer = txtInputer.Text.Trim();
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
                item.UpdateDate = DateTime.Now;
                item.Updater = User.Identity.Name;
                appdb.SaveChanges();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveItem();
                //Alert.Show("保存成功");
                msglab.Text = "保存成功";
                string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid2');");
                PageContext.RegisterStartupScript(scripts);
            }
            catch (Exception ee)
            {
                Alert.Show("保存失败:" + ee.Message);
            }

        }
        protected void btnSaveClose_Click(object sender, EventArgs e)
        {

            try
            {
                SaveItem();

                //PageContext.RegisterStartupScript("<script>alert('保存成功')</script");
                //System.Threading.Thread.Sleep(1000);
                string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid2');");
                PageContext.RegisterStartupScript(scripts + Alert.GetShowInTopReference("保存成功"));

                PageContext.RegisterStartupScript(ActiveWindow.GetHideReference());
            }
            catch (Exception ee)
            {
                Alert.Show("保存失败:" + ee.Message);
            }
            finally
            {
                //string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid2');");
                //PageContext.RegisterStartupScript(scripts + ActiveWindow.GetHideReference());
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