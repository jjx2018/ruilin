using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    /// <summary>
    /// 扫描明细记录
    /// </summary>
    public class ProduceOrderRec
    {

        [Key]
        public int SN { get; set; }

        public int ProduceOrderDetailSn { get; set; }

        public DateTime? optdate { get; set; }

        /// <summary>
        /// 生产数
        /// </summary>
        public decimal? Quantity { get; set; }

        /// <summary>
        /// 抽检数
        /// </summary>
        public decimal? choujianqut { get; set; }

        /// <summary>
        /// 抽检结果
        /// </summary>
        public result? result { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public role Role { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public bool? State { get; set; }

        public string StockHeaderProsn { get; set; }
    }

    public enum role
    {
        毛胚,
        注塑,
        焊接,
        喷涂,
        委外,
        组装
    }

    /// <summary>
    /// 品检结果
    /// </summary>
    public enum result
    {
        合格,
        不合格,
        特采,
    }
}
