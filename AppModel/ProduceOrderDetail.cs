using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class ProduceOrderDetail
    {
        [Key]
        public int SN { get; set; }
        public int FSN { get; set; }
        //public int? allitemSN { get; set; }
        public string SeqNo { get; set; }
        public string ProPlanNo { get; set; }
        /// <summary>
        /// 生产单号
        /// </summary>
        public string ProOrderNo { get; set; }
        public string Properties { get; set; }
        /// <summary>
        /// 领料日期
        /// </summary>
        public DateTime? PDate { get; set; }
        public string Checker { get; set; }
        public string Dept { get; set; }
        /// <summary>
        /// 客户代号
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// 品号
        /// </summary>
        public string ProNo { get; set; }
        /// <summary>
        /// 品名
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
        public string Spec { get; set; }
        public double? Quantity { get; set; }
        public string Unit { get; set; }
        /// <summary>
        /// 销售单号
        /// </summary>
        public string SaleOrderNo { get; set; }
        public string ClientOrderNo { get; set; }
        public int? State { get; set; }
        public string ProLotNo { get; set; }
        public string WorkShop { get; set; }
        public string Inputer { get; set; }
        public DateTime? InputeDate { get; set; }
        public string Remark { get; set; }
        public int BomSN { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 标识  null，0-已品检，1-已入库
        /// </summary>
        public int? Status { get; set; }

        public result? result { get; set; }

        public decimal? ChoujianQut { get; set; }

        public DateTime? CheckDate { get; set; }
    }
}