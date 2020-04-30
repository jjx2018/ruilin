using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class Stock
    {
        [Key]
        public int SN { get; set; }

        /// <summary>
        /// 单号  IN  OUT
        /// </summary>
        public string StockOrderNo { get; set; }

        /// <summary>
        /// 关联的采购单号
        /// </summary>
        public string PurOrderNo { get; set; }

        /// <summary>
        /// 性质  采购入库、普通入库、退货入库       销售出货、其他出货
        /// </summary>
        public string Properties { get; set; }


        /// <summary>
        /// 仓位
        /// </summary>
        public string Space { get; set; }

        /// <summary>
        /// 是否作废
        /// </summary>
        public bool isCancel { get; set; }


        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? PDate { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        public string JinBanRen { get; set; }

        /// <summary>
        /// 备注 ,最长200
        /// </summary>
        public string remark { get; set; }

        public virtual ICollection<StockList> stocklist { get; set; }
    }
}