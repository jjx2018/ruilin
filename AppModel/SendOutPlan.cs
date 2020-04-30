using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppBoxPro
{
    public class SendOutPlan
    {
        [Key]
        public int SN { get; set; }
        /// <summary>
        /// 委外计划号
        /// </summary>
        public string SendOutPlanNo { get; set; }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public string ProviderID { get; set; }
        public string Provider { get; set; }
        public string ProNo { get; set; }
        public string ProName { get; set; }
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public string Spec { get; set; }
        public double? Quantity { get; set; }
        public string Unit { get; set; }
        public string SaleOrderNo { get; set; }
        public int? State { get; set; }
        public string Inputer { get; set; }
        public DateTime? InputeDate { get; set; }
        public string Remark { get; set; }
        public string WorkShop { get; set; }
        public int BomSN { get; set; }
        /// <summary>
        /// 计划完成时间
        /// </summary>
        public DateTime? PlanFinishDate { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public DateTime? ZhuangPeiDate { get; set; }
        /// <summary>
        /// instruction 中的主键
        /// </summary>
        public int? ISN { get; set; }
    }
}