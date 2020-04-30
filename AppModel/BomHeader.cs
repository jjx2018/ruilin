using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AppBoxPro
{
    public class BomHeader
    {
        [Key]
        public int SN { get; set; }

        /// <summary>
        /// 订单明细中的SN
        /// </summary>
        public int? OdtSN { get; set; }
        /// <summary>
        /// 物料表中的主键
        /// </summary>
        public int? AllitemSN { get; set; }
        public int? ProBomSN { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProName { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProNo { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public int? Ver { get; set; }
        /// <summary>
        /// 客户产品编号
        /// </summary>
        public string ClientProNo { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public string ClientCode { get; set; }

        public string Inputer { get; set; }
        public DateTime? InputeDate { get; set; }
        /// <summary>
        /// 导入的日期
        /// </summary>
        public DateTime? BomDate { get; set; }
        public string Updater { get; set; }
        public DateTime? UpdateDate { get; set; }
       
    }
}