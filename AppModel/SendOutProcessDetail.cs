using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppBoxPro 
{
    public class SendOutProcessDetail
    {
        [Key]
        public int SN { get; set; }
        /// <summary>
        /// 外键
        /// </summary>
        public int FSN { get; set; }
        public string SendOutPlanNo { get; set; }
        /// <summary>
        /// 发外单号
        /// </summary>
        public string SendOutOrderNo { get; set; }
        /// <summary>
        /// 销售单号
        /// </summary>
        public string SaleOrderNo { get; set; }
        /// <summary>
        /// 品号
        /// </summary>
        public string ProNo { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string ProName { get; set; }
        /// <summary>
        /// 料号
        /// </summary>
        public string ItemNo { get; set; }
        /// <summary>
        /// 料名
        /// </summary>
        public string ItemName { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public double? Quantity { get; set; }
        public string Remark { get; set; }
        public string Inputer { get; set; }
        public DateTime? InputeDate { get; set; }
        public int BomSN { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
    }
}