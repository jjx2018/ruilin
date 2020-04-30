using FineUIPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppBoxPro.mobile
{
    public class PageBaseMobile : PageBase
    {

        #region static readonly

        protected static readonly string DATALIST_ITEM_TEMPLATE = "<table class=\"item-table\"><tr><td><img class=\"item-img\" src=\"{0}\"><div class=\"item-text\">{1}</div><div class=\"item-desc\">{2}</div></td></tr></table>";

        protected static readonly string DATALIST_SIMPLE_ITEM_TEMPLATE = "<table class=\"item-table\"><tr><td><img class=\"item-img\" src=\"{0}\"><div class=\"item-text\">{1}</div></td></tr></table>";

        #endregion

        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            var pm = PageManager.Instance;

            // 如果不是AJAX回发
            if (pm != null && !pm.IsFineUIAjaxPostBack && !Constants.IS_BASE)
            {
                // 强制设为大字体、移动设备自适应
                pm.MobileAdaption = true;
                pm.DisplayMode = DisplayMode.Large;
            }

        }


        #endregion

        #region ShowNotify

        /// <summary>
        /// 显示通知对话框
        /// </summary>
        /// <param name="message"></param>
        public virtual void ShowNotify(string message)
        {
            ShowNotify(message, MessageBoxIcon.None);
        }

        /// <summary>
        /// 显示通知对话框
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageIcon"></param>
        public virtual void ShowNotify(string message, MessageBoxIcon messageIcon)
        {
            Notify notify = new Notify();
            notify.Target = Target.Self;
            notify.ShowHeader = false;
            notify.PositionX = Position.Center;
            notify.PositionY = Position.Center;
            notify.Message = message;
            notify.MessageBoxIcon = messageIcon;
            notify.MessageAlign = TextAlign.Center;
            notify.MinWidth = 200;
            notify.DisplayMilliseconds = 3000;
            notify.IsModal = true;
            notify.HideOnMaskClick = true;
            notify.Show();
        }


        #endregion

    }
}