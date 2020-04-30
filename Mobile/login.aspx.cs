using FineUIPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.mobile
{
    public partial class login : PageBaseMobile
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = tbxUserName.Text.Trim();
            string password = tbxPassword.Text.Trim();

            User user = DB.Users.Where(u => u.Name == userName).FirstOrDefault();

            if (user != null)
            {
                if (PasswordUtil.ComparePasswords(user.Password, password))
                {
                    if (!user.Enabled)
                    {
                        ShowNotify("用户未启用，请联系管理员！", MessageBoxIcon.Error);
                    }
                    else
                    {
                        // 登录成功
                        //logger.Info(String.Format("登录成功：用户“{0}”", user.Name));

                        LoginSuccess(user);

                        return;
                    }
                }
                else
                {
                    //logger.Warn(String.Format("登录失败：用户“{0}”密码错误", userName));
                    ShowNotify("用户名或密码错误！", MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {
                //logger.Warn(String.Format("登录失败：用户“{0}”不存在", userName));
                ShowNotify("用户名或密码错误！", MessageBoxIcon.Error);
                return;
            }


        }

        private void LoginSuccess(User user)
        {
            RegisterOnlineUser(user);

            // 用户所属的角色字符串，以逗号分隔
            string roleIDs = String.Empty;
            if (user.Roles != null)
            {
                roleIDs = String.Join(",", user.Roles.Select(r => r.ID).ToArray());
            }

            bool isPersistent = false;
            DateTime expiration = DateTime.Now.AddMinutes(120);
            CreateFormsAuthenticationTicket(user.Name, roleIDs, isPersistent, expiration);

            Response.Redirect("choosejob.aspx");
        }
    }
}