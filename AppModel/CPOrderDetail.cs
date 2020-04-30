using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class CPOrderDetail
    {
        [Key]
        public int SN { get; set; }
        public int FSN { get; set; }
        public int? allitemSN { get; set; }
        public string CPOrderNo { get; set; }
        public string CPPlanNo { get; set; }
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
        public DateTime? InputeDate { get; set; }
        public int BomSN { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
    }
}