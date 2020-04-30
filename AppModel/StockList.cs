using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    /// <summary>
    /// 进出仓明细    
    /// </summary>
    public class StockList
    {
        [Key]
        public int SN { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string StockOrderNo { get; set; }

        /// <summary>
        /// 关联的采购单号
        /// </summary>
        public string PurOrderNo { get; set; }

        /// <summary>
        /// 关联的发外单号
        /// </summary>
        public string SendOutOrderNo { get; set; }

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
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? PDate { get; set; }

        public string Remark { get; set; }


        public virtual Stock Stock { get; set; }


        /// <summary>
        /// 仓位
        /// </summary>
        public string Space { get; set; }

        /// <summary>
        /// 属性 :采购入库、领料出库、发外出库
        /// </summary>
        public Properties Properties { get; set; }

    }


    public enum Properties
    {
        采购入库,
        领料出库,
        发外出库,
    }

}