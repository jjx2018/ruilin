using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    /// <summary>
    /// 采购入库、领料出库记录    
    /// </summary>
    public class PurchaseStockList
    {
        [Key]
        public int SN { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string StockOrderNo { get; set; }

        /// <summary>
        /// 关联的采购单号
        /// </summary>
        public string PurOrderNo { get; set; }

        public string ProNo { get; set; }

        /// <summary>
        /// 料号
        /// </summary>
        public string ItemNo { get; set; }

        public string ItemName { get; set; }

        public string Spec { get; set; }

        /// <summary>
        /// 进出仓标记  02进仓  03出仓
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal? Quantity { get; set; }

        /// <summary>
        /// 抽检数
        /// </summary>
        public decimal? ChoujianQut { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? PDate { get; set; }

        public string Remark { get; set; }


        /// <summary>
        /// 仓位
        /// </summary>
        public string Space { get; set; }

        /// <summary>
        /// 属性 :采购入库、领料出库
        /// </summary>
        public PurchaseProperties? PurchaseProperties { get; set; }

        /// <summary>
        /// 品检结果
        /// </summary>
        public result? result { get; set; }

        /// <summary>
        /// 品检日期
        /// </summary>
        public DateTime ? checkDate { get; set; }

        public string StockHeaderProsn { get; set; }
        public string Barcode { get; set; }
    }


    public enum PurchaseProperties
    {
        采购入库,
        领料出库,
    }

}