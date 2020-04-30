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
    public partial class Order_New : PageBase
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


                LoadData();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHidePostBackReference();

        }


        #endregion

        #region Events


        private int SaveItem()
        {
            try
            {
                string OrderNo = txtOrderNo.Text.Trim();
                 
                //User user = DB.Users.Where(u => u.Name == inputUserName).FirstOrDefault();

                using (var appdb = new AppContext())
                {
                    OrderHeader user = appdb.orderheader.Where(u => u.OrderNo == OrderNo).FirstOrDefault();
                    if (user != null)
                    {

                        return 0;
                    }


                    OrderHeader item = new OrderHeader();
                    item.OrderNo = OrderNo;
                    item.ClientOrderNo = txtClinetOrderNo.Text;
                    item.LotNo = txtLotno.Text;
                    item.ClientCode = txtClientCode.Text;
                    item.RecOrderPerson = txtBuis.Text;// GetChineseName(User.Identity.Name);
                    item.Inputer = User.Identity.Name;
                    item.InputerDate = DateTime.Now;
                    item.RecOrderDate = recOrderDate.SelectedDate;
                    item.CheckDate = checkDate.SelectedDate;
                    item.OutGoodsDate = outOrderDate.SelectedDate;
                    item.IsBom = 0;
                    item.IsCheck = 0;
                    item.ContainerType = txtContainerType.Text;
                    item.OrderType = txtOrderType.Text;

                    appdb.orderheader.Add(item);
                    appdb.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ee)
            {
                return 2;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {



            try
            {
                string OrderNo = txtOrderNo.Text.Trim();

                if (OrderNo.Length == 12 && OrderNo.Substring(0, 2) == "RL" && CommFunction.IsNumeric(OrderNo.Substring(2, 10)))
                {

                }
                else
                {
                    Alert.Show("订单号格式不正确",string.Empty, txtOrderNo.GetFocusReference());
                   
                    return  ;
                }
                int i = SaveItem();
                if (i == 1)
                {
                    Notify n = new Notify();
                    n.PositionX = Position.Center;
                    n.PositionY = Position.Top;
                    n.DisplayMilliseconds = 3000;

                    n.Message = "保存成功";

                    n.Show();
                }
                else if (i == 0)
                {
                    Alert.Show("该订单编号已经存在请更改");
                }
                else
                {
                    Alert.Show("保存失败");
                }
                //msglab.Text = "保存成功";
                string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid1');");
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
                string OrderNo = txtOrderNo.Text.Trim();

                if (OrderNo.Length == 12 && OrderNo.Substring(0, 2) == "RL" && CommFunction.IsNumeric(OrderNo.Substring(2, 10)))
                {

                }
                else
                {
                    Alert.Show("订单号格式不正确", string.Empty, txtOrderNo.GetFocusReference());
                    return;
                }
                int i = SaveItem();
                if (i == 1)
                {
                    string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid1');");
                    PageContext.RegisterStartupScript(scripts + Alert.GetShowInTopReference("保存成功") + ActiveWindow.GetHideReference());
                }
                else if (i == 0)
                {
                    Alert.Show("该订单编号已经存在请更改");
                }
                else
                {
                    Alert.Show("保存失败");
                }
            }
            catch (Exception ee)
            {
                Alert.Show("保存失败:" + ee.Message);
            }
            finally
            {
                
            }

        }
        #endregion




        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            //PageContext.RegisterStartupScript("parent.__doPostBack('','RefreshGrid2');");
            string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid1');");
            PageContext.RegisterStartupScript(scripts);
        }

        protected void txtClientCode_Blur(object sender, EventArgs e)
        {
            try
            {
                string sql = "select max(SUBSTRING(OrderNo,5,len(orderno)-4)) from OrderHeader where InputerDate>=CONVERT(varchar(100), GETDATE(), 23)";
                SQLHelper.DbHelperSQL.SetConnectionString("");
                string orderno = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
                Alert.Show(orderno);
                if (string.IsNullOrEmpty(orderno))
                {
                    sql = "Select right(CONVERT(varchar(100), GETDATE(), 112),6)";
                    orderno = txtClientCode.Text + SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() + "01";
                }
                else
                {
                    orderno = txtClientCode.Text + (Int64.Parse(orderno) + 1).ToString();
                }
                txtOrderNo.Text = orderno;
            }
            catch (Exception ee)
            {
                Alert.Show(ee.ToString());
            }
        }

        protected void txtOrderNo_Blur(object sender, EventArgs e)
        {
            try
            {
                string orderno = txtOrderNo.Text;
                if(orderno.Length==12&&orderno.Substring(0,2)=="RL"&&CommFunction.IsNumeric(orderno.Substring(2,10)))
                {

                }
                else
                {
                    Alert.Show("订单号格式不正确", string.Empty, txtOrderNo.GetFocusReference());
                    //PageContext.RegisterStartupScript(txtOrderNo.GetFocusReference(true));
                }
            }catch(Exception ee)
            {
                Alert.Show(ee.Message);
            }
        }
    }
}