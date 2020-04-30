using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AppBoxPro 
{
    public class OrderDetail
    {
        [Key]
        public int SN { get; set; }
        /// <summary>
        /// 外键 订单头表中的主键
        /// </summary>
        public int FSN { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string ClinetNo { get; set; }
        /// <summary>
        /// 我司型号、料号
        /// </summary>
        public string ItemNo { get; set; }
        /// <summary>
        /// 产品名称、物料名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public double? Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? Quantity { get; set; }
        public string Color { get; set; }
        public string Unit { get; set; }
        /// <summary>
        /// 是否新产品
        /// </summary>
        public string IsNew { get; set; }
        /// <summary>
        /// 是否新包材
        /// </summary>
        public string IsPackingmaterials { get; set; }
        /// <summary>
        /// 国家包材版本
        /// </summary>
        public string CountryPackVer { get; set; }
        /// <summary>
        /// 是否变更
        /// </summary>
        public string IsChange { get; set; }
        /// <summary>
        /// 要求1-12
        /// </summary>
        public string Demand1 { get; set; }
        public string Demand2 { get; set; }
        public string Demand3 { get; set; }
        public string Demand4 { get; set; }
        public string Demand5 { get; set; }
        public string Demand6 { get; set; }
        public string Demand7 { get; set; }
        public string Demand8 { get; set; }
        public string Demand9 { get; set; }
        public string Demand10 { get; set; }
        public string Demand11 { get; set; }
        public string Demand12 { get; set; }
        /// <summary>
        /// 备注包装需求
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 录单人
        /// </summary>
        public string Inputer { get; set; }
        /// <summary>
        /// 录单时间
        /// </summary>
        public DateTime? InputerDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Updater { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// 是否生成了BOM 0 未生成  1生成
        /// </summary>
        public int? IsBom { get; set; }
        public int? BomVer { get; set; }
        public int? IsConfirm { get; set; }
    }
}