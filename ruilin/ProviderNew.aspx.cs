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
    public partial class ProviderNew : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "ProviderAdd";
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

        #region InitUserRole

        private void InitUserDept()
        {
            //// 打开编辑窗口
            //string selectDeptURL = String.Format("./user_select_dept.aspx?ids=<script>{0}</script>", hfSelectedDept.GetValueReference());
            //tbSelectedDept.OnClientTriggerClick = Window1.GetSaveStateReference(hfSelectedDept.ClientID, tbSelectedDept.ClientID)
            //        + Window1.GetShowReference(selectDeptURL, "选择用户所属的部门");

        }

        #endregion

        #region InitUserRole

        private void InitUserRole()
        {
            //// 打开编辑角色的窗口
            //string selectRoleURL = String.Format("./user_select_role.aspx?ids=<script>{0}</script>", hfSelectedRole.GetValueReference());
            //tbSelectedRole.OnClientTriggerClick = Window1.GetSaveStateReference(hfSelectedRole.ClientID, tbSelectedRole.ClientID)
            //        + Window1.GetShowReference(selectRoleURL, "选择用户所属的角色");

        }
        #endregion

        #region InitUserJobTitle

        private void InitUserTitle()
        {
            //// 打开编辑角色的窗口
            //string selectJobTitleURL = String.Format("./user_select_title.aspx?ids=<script>{0}</script>", hfSelectedTitle.GetValueReference());
            //tbSelectedTitle.OnClientTriggerClick = Window1.GetSaveStateReference(hfSelectedTitle.ClientID, tbSelectedTitle.ClientID)
            //        + Window1.GetShowReference(selectJobTitleURL, "选择用户拥有的职称");

        }
        #endregion

        #endregion

        #region Events


        private void SaveItem()
        {
            Provider item = new Provider();

            item.Name = tbxName.Text.Trim();
            //item.ProviderNo = tbxClinetNo.Text.Trim();
            item.Address = tbxAddress.Text.Trim();
            item.Telephone = tbxPhone.Text.Trim();
            item.Fax = tbxFax.Text.Trim();
            item.Bank = tbxBank.Text.Trim();
            item.Account = tbxAccount.Text.Trim();
            item.Abstract = tbxRemark.Text.Trim();
            item.subjectcode = tbxsubjectcode.Text.Trim();
            using (var appdb = new AppContext())
            {
                IQueryable<Provider> q = appdb.providers;
                q = q.SortBy("ProviderNo ASC");
                //Alert.Show(q.Count().ToString());
                //return;
                int k = 0; int qc = q.Count();
                bool flag = false;
                for (int i = 1; i <= qc; i++)
                {
                    k = 0;
                    foreach (Provider p in q.ToList<Provider>())
                    {
                        
                        if (p.ProviderNo == i.ToString())
                        {
                            break;
                        }
                        else
                        {
                            k++;
                            if(k==qc)
                            {
                                k = i;
                                flag = true;
                            }
                            //else
                            //{
                            //    k = qc + 1;
                            //}
                        }
                    }
                    if(flag)
                    {
                        break;
                    }
                    else
                    {
                        if(i==qc)
                        {
                            k = qc + 1;
                        }
                    }
                }
                item.ProviderNo = k.ToString();
                appdb.providers.Add(item);
                appdb.SaveChanges();
            }

        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
           try
           {
               string inputUserName = tbxName.Text.Trim();
               string subjectcode = tbxsubjectcode.Text.Trim();

               //User user = DB.Users.Where(u => u.Name == inputUserName).FirstOrDefault();

               using (var appdb = new AppContext())
               {
                   //IQueryable<Provider> q = appdb.providers;
                   //q = q.SortBy("ProviderNo ASC");
                   //int k = 0;
                   //foreach (Provider p in q.ToList<Provider>())
                   //{
                   //    k++;
                   //}
                   //Alert.Show(q.Count().ToString()+"::::::"+k.ToString());
                   //return;
                   Provider user = appdb.providers.Where(u => u.subjectcode == subjectcode || u.Name == inputUserName).FirstOrDefault();
                   if (user != null)
                   {
                       Alert.Show("供应商 " + inputUserName + " 已经存在！");
                       return;
                   }

               }
               SaveItem();
               Alert.Show("保存成功");
           }
            catch(Exception ee)
           {
               Alert.Show("保存失败:"+ee.Message);
           }
            
            //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}