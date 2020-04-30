using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    /// <summary>
    /// 表头
    /// </summary>
    public class StockHeader
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 抬头名称:退货单、进货单、委外进货单、领料单、生产入库单、销货单、委外发货单
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string StockHeaderProsn { get; set; }

        public DateTime optdate { get; set; }
        /// <summary>
        /// 厂家名称、供应商名称
        /// </summary>
        public string name { get; set; }

        public string contact { get; set; }
        public string phone { get; set; }

        public string fax { get; set; }

        public string remark { get; set; }

        public string jingbanren { get; set; }
    }
}