using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AppBoxPro 
{
    public class Instruction
    {
        [Key]
        public int SN { get; set; }
        /// <summary>
        /// bom 主键
        /// </summary>
        public int BomSN { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProName { get; set; }
        /// <summary>
        /// 料号
        /// </summary>
        public string ItemNo { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 材质
        /// </summary>
        public string Material { get; set; }
        /// <summary>
        /// 表面处理
        /// </summary>
        public string SurfaceDeal { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Sclass { get; set; }
        /// <summary>
        /// 生产方式
        /// </summary>
        public string MainFrom { get; set; }
        /// <summary>
        /// 备货用量
        /// </summary>
        public double? UsingQuantity { get; set; }
        /// <summary>
        /// 实际用量
        /// </summary>
        public double? RealUsingQuantity { get; set; }
        /// <summary>
        /// 确认数量
        /// </summary>
        public double? ConfirmQuantity { get; set; }
        public string Inputer { get; set; }
        public DateTime? InputeDate { get; set; }
        /// <summary>
        /// 接收部门
        /// </summary>
        public string ReceiveDept { get; set; }
        /// <summary>
        /// 是否确认  0未确认  1确认
        /// </summary>
        public int? IsConfirm { get; set; }

        /// <summary>
        /// 是否生成了计划  0未生成 1已生成
        /// </summary>
        public int? IsPlan { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime? ConfirmDate { get; set; }
        public string Updater { get; set; }
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// 订单明细主键
        /// </summary>
        public int? OdtSN { get; set; }

    }
}