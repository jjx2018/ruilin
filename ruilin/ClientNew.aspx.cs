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
    public partial class ClientNew : PageBase
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
            Client item = new  Client();

            item.Name = tbxName.Text.Trim();
            item.ClientNo =tbxClinetNo.Text.Trim();
            item.Address =tbxAddress.Text.Trim();
            item.Telephone =tbxPhone.Text.Trim();
            //item.Fax = tbxFax.Text.Trim();
            item.Bank = tbxBank.Text.Trim();
            item.Account = tbxAccount.Text.Trim();
            item.Abstract = tbxRemark.Text.Trim();
            //item.subjectcode = tbxsubjectcode.Text.Trim();
            using (var appdb = new AppContext())
            {
                appdb.clients.Add(item);
                appdb.SaveChanges();
            }

        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            string inputUserName = tbxName.Text.Trim();
            string clinetno = tbxClinetNo.Text.Trim();

            //User user = DB.Users.Where(u => u.Name == inputUserName).FirstOrDefault();
            
            using (var appdb = new AppContext())
            {
                Client user = appdb.clients.Where(u => u.ClientNo == clinetno || u.Name == inputUserName).FirstOrDefault();
                if (user != null)
                {
                    Alert.Show("用户 " + inputUserName + " 已经存在！");
                    return;
                }
            }
            SaveItem();

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

    }
}