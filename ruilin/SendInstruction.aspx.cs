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
    public partial class SendInstruction : PageBase
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
                return "BOMEdit";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string k = GetQueryValue("k");
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
                                if (tb.ID != "txtQuantity" && tb.ID != "txtUsingQuantity")
                                    tb.Readonly = true;
                            }
                            else if (subctrl.GetType().Name == "NumberBox")
                            {
                                NumberBox numbox = (NumberBox)subctrl;
                                if (numbox.ID != "txtQuantity")
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

                using (var appdb = new AppContext())
                {
                    
                    int sn = GetQueryIntValue("id");
                    int fsn = GetQueryIntValue("fsn");
                    BomHeader item = appdb.bombase
                        .Where(u => u.SN == fsn).FirstOrDefault();
                    txtOrderNo.Text = item.OrderNo;
                    txtProNo.Text = item.ProNo;
                    txtProName.Text = item.ProName;
                    //txtClinetNo.Text = item.ClientNo;
                    txtQuantity.Text = "";
                    BomDetail current = appdb.bomdtl
                       .Where(u => u.SN == sn).FirstOrDefault();
                    if (current == null)
                    {
                        // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                        Alert.Show("参数错误！", String.Empty, ActiveWindow.GetHideReference());
                        return;
                    }
                    txtItemNo.Text = current.ItemNo;
                    txtItemName.Text = current.ItemName;
                    txtMaterial.Text = current.Material;
                    txtSclass.Text = current.Sclass;
                    txtSpec.Text = current.Spec;
                    txtSurfaceDeal.Text = current.SurfaceDeal;
                    txtUsingQuantity.Text = current.OrderUsingQuantity.ToString();
                    txtMakeThod.Text = current.MainFrom;
                    //Instruction ins = appdb.instruction
                    //    .Where(u => u.OrderNo == item.OrderNo && u.ProNo == item.ProNo && u.ItemNo == current.ItemNo).FirstOrDefault() ;
                    //var instructionsql = from a in appdb.instruction
                    //                     where a.OrderNo == item.OrderNo && a.ProNo == item.ProNo && a.ItemNo == current.ItemNo
                    //                     group a by new
                    //                     {
                    //                         a.ItemNo
                    //                     } into g
                    //                     select new 
                    //                     {
                    //                         g.Key.ItemNo,
                    //                         confirmquantity = (double)g.Sum(p => p.ConfirmQuantity)
                    //                     };
                    
                  //double? cfq=  appdb.instruction.SqlQuery("select itemno,sum(ConfirmQuantity) from instruction where orderno='"+item.OrderNo+"' and prono='"+item.ProNo+"' and itemno='"+current.ItemNo+"' group by itemno",new object[]{}).FirstOrDefault().ConfirmQuantity;
                  SQLHelper.DbHelperSQL.SetConnectionString("");
                  string sql = "select sum(ConfirmQuantity) from instruction where orderno='" + item.OrderNo + "' and prono='" + item.ProNo + "' and itemno='" + current.ItemNo + "'";

                  object sq = SQLHelper.DbHelperSQL.GetSingle(sql);
                  if (sq!=null)
                  {
                      txtUsingQuantity.Text = (current.OrderUsingQuantity - (double.Parse(sq.ToString()))).ToString();
                  }
                  else
                  {
                      txtUsingQuantity.Text = current.OrderUsingQuantity.ToString();
                  }
                    //if(instructionsql!=null)
                    //{
                    //    var ss = instructionsql.FirstOrDefault();
                    //    if (ss != null)
                    //    {
                    //        txtUsingQuantity.Text = (current.UsingQuantity - ss.confirmquantity).ToString();
                    //    }
                    //}
                    
                }
                LoadData();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
            BindGrid();
        }

        private void BindGrid()
        {
            IQueryable<User> q = DB.Users.Include(u => u.Dept);
            string dept = rbtDept.SelectedValue;
            q = q.Where(u => u.Dept.Name == dept);
            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Name.Contains(searchText) || u.ChineseName.Contains(searchText) || u.EnglishName.Contains(searchText));
            }


            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和数据库分页
            q = SortAndPage<User>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        #endregion

        #region Events
        protected void btnSearch_click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid();
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid();
        }
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        private bool SaveItem()
        {
           
            using (var appdb = new AppContext())
            {
                string itemno = txtItemNo.Text;
                string itemname = txtItemName.Text;
                string orderno=txtOrderNo.Text;
                string ProNo= txtProNo.Text;
                Instruction item = appdb.instruction
                    .Where(u => u.ItemNo == itemno&&u.OrderNo==orderno&&u.ProNo==ProNo && u.IsConfirm == 0).FirstOrDefault();
                if (item != null)
                {
                    
                    return false;
                }
                else
                {
                    item = new Instruction();
                    item.OrderNo = txtOrderNo.Text.Trim();
                    item.ProNo = txtProNo.Text.Trim();
                    item.ProName = txtProName.Text.Trim();
                    item.ItemNo = txtItemNo.Text.Trim();
                    item.ItemName = txtItemName.Text.Trim();
                    item.Spec = txtSpec.Text.Trim();
                    item.Material = txtMaterial.Text.Trim();
                    item.SurfaceDeal = txtSurfaceDeal.Text.Trim();
                    item.Sclass = txtSclass.Text.Trim();
                    item.UsingQuantity = double.Parse(txtQuantity.Text.Trim());
                    item.Inputer = User.Identity.Name;
                    item.InputeDate = DateTime.Now;
                    item.ReceiveDept = rbtDept.SelectedValue;
                    item.IsConfirm = 0;
                    item.Receiver = Grid1.Rows[Grid1.SelectedRowIndex].Values[1].ToString();
                    item.MainFrom = txtMakeThod.Text;
                    item.IsPlan = 0;
                    item.BarCode = DateTime.Now.ToString("yyyyMMddHHmmsss") + Grid1.SelectedRowIndex.ToString();
                    appdb.instruction.Add(item);
                    appdb.SaveChanges();
                    return true;
                }


            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text =="")
            {
                Alert.Show("备货数量不能为空！");
                return;
            }
            if (Grid1.SelectedRowIndex == -1)
            {
                Alert.Show("请选择要发送的人员！");
                return;
            }
            msglab.Text = "";
            try
            {
                if(SaveItem())
                {
                    msglab.Text = "保存成功";
                }
                else
                {
                    Alert.Show("该物料存在未确认的备货确认单，请先确认！");
                }
                //Alert.Show("保存成功");
               
                //string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid2');");
                //PageContext.RegisterStartupScript(scripts);
            }
            catch (Exception ee)
            {
                Alert.Show("保存失败:" + ee.Message);
            }

        }
        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text == "")
            {
                Alert.Show("备货数量不能为空！");
                return;
            }
            if (Grid1.SelectedRowIndex == -1)
            {
                Alert.Show("请选择要发送的人员！");
                return;
            }

            try
            {
                if (SaveItem())
                {
                    PageContext.RegisterStartupScript(Alert.GetShowInTopReference("保存成功"));
                }
                else
                {
                    PageContext.RegisterStartupScript(Alert.GetShowInTopReference("该物料存在未确认的备货确认单，请先确认！"));  
                }

                //PageContext.RegisterStartupScript("<script>alert('保存成功')</script");
                //System.Threading.Thread.Sleep(1000);
                //string scripts = String.Format("F.getActiveWindow().window.__doPostBack('','RefreshGrid2');");
               
                //Alert.ShowInTop("保存成功");
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
            //txtisOpen.Text = "0";
            //string openUrl = String.Format("searchitem.aspx?selected={0}", HttpUtility.UrlEncode("dddd"));

            //PageContext.RegisterStartupScript(Window1.GetSaveStateReference(txtItemNo.ClientID)
            //        + Window1.GetShowReference(openUrl));
        }


        protected void txtItemNo_TextChanged(object sender, EventArgs e)
        {
            msglab.Text = "";
            //Alert.Show(txtisOpen.Text);
            //if (txtisOpen.Text == "1")
            //{
            //    txtisOpen.Text = "0";
            //    return;
            //}
            //string openUrl = String.Format("searchitem.aspx?k={0}", HttpUtility.UrlEncode(txtItemNo.Text));
            ////Window1.GetSaveStateReference(new string[] { txtItemNo.ClientID, txtItemName.ClientID })
            ////+ Window1.GetShowReference(openUrl);
            //PageContext.RegisterStartupScript(Window1.GetSaveStateReference(new string[] { txtItemNo.ClientID, txtItemName.ClientID, txtisOpen.ClientID })
            //       + Window1.GetShowReference(openUrl) + txtItemNo.GetFocusReference());

        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            //PageContext.RegisterStartupScript("parent.__doPostBack('','RefreshGrid2');");
        }

        protected void rbtDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}