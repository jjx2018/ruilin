using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro 
{
    public class PurchasePlan
    {
        [Key]
        public int SN { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public string SeqNo { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        public string PurPlanNo { get; set; }
        /// <summary>
        /// 性质
        /// </summary>
        public string Properties { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? PDate { get; set; }
        /// <summary>
        /// 采购人
        /// </summary>
        public string Purchaser { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string Checker { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Dept { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public string Project { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Provider { get; set; }
        /// <summary>
        /// 供应商名字
        /// </summary>
        public string ProviderName { get; set; }
        public string ProNo { get;set; }
        public string ProName { get; set; }
        /// <summary>
        /// 料号、品号
        /// </summary>
        public string ItemNo { get; set; }
        /// <summary>
        /// 料名、品名
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        public double? Quantity { get; set; }
        /// <summary>
        /// 预交货日期
        /// </summary>
        public DateTime? PreDeliveryDate { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 销售订单号
        /// </summary>
        public string SaleOrderNo { get; set; }
        /// <summary>
        /// 状态： 1结束、0未结束
        /// </summary>
        public int? State { get; set; }
        /// <summary>
        /// 前置单据
        /// </summary>
        public string PreBill { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public double? StockQuantity { get; set; }
        /// <summary>
        /// 已采购数量
        /// </summary>
        public double? PurchasedQuantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Inputer { get; set; }

        public DateTime? InputeDate { get; set; }
        public string Remark { get; set; }
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