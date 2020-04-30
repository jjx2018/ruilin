using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppBoxPro
{
    public class SendOutProcessHeader
    {
        [Key]
        public int SN { get; set; }
        public string Provider { get; set; }
        public string ProviderID { get; set; }
        /// <summary>
        /// 发外单号
        /// </summary>
        public string SendOutOrderNo { get; set; }
        /// <summary>
        /// 销售单号
        /// </summary>
        public string SaleOrderNo { get; set; }
        /// <summary>
        /// 计划号
        /// </summary>
        public string SendOutPlanNo { get; set; }
        /// <summary>
        /// 发外日期
        /// </summary>
        public DateTime? SendOutDate { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string JBRName { get; set; }
        public string JBRID { get; set; }
        /// <summary>
        /// 是否审核  0未审核  1已审核
        /// </summary>
        public int? IsCheck { get; set; }

        public string Remark { get; set; }
        /// <summary>
        /// 收货员
        /// </summary>
        public string RecGoodsMan { get; set; }
        /// <summary>
        /// 品检员
        /// </summary>
        public string QCMan { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        public string SendGoodsMan { get; set; }
        /// <summary>
        /// 制单、录入人
        /// </summary>
        public string Inputer { get; set; }
        public DateTime? InputeDate { get; set; }
        public string Updater { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}