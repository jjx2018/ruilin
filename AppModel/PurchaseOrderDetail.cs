using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class PurchaseOrderDetail
    {
        [Key]
        public int SN { get; set; }
        public int FSN { get; set; }
        public string PurOrderNo { get; set; }
        public string PurPlanNo { get; set; }
        public string SaleOrderNo { get; set; }
        public string ProNo { get; set; }
        public string ProName { get; set; }
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public double? Quantity { get; set; }
        public double? Price { get; set; }
        public string Remark { get; set; }
        public string Inputer { get; set; }
        public DateTime InputeDate { get; set; }
        public int BomSN { get; set; }


        /// <summary>
        /// 标识  null，0-采购领料，1-采购品检，2-采购入库
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 抽检数
        /// </summary>
        public decimal? ChoujianQut { get; set; }

        /// <summary>
        /// 品检结果
        /// </summary>
        public result? result { get; set; }

        public DateTime? CheckDate { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
    }
}