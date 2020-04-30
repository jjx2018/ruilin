using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AppBoxPro
{
    public class ProBomHeader
    {
        [Key]
        public int SN { get; set; }
        /// <summary>
        /// 物料表中的主键
        /// </summary>
        public int AllitemSN { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProName { get; set; }

        public string Color { get; set; }
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
        /// <summary>
        /// 存放Excel文件
        /// </summary>
        public string BomExcel { get; set; }
        public string FileNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        public string QuoteProNo { get; set; }
        public int? QuoteBomVer { get; set; }
        public int? QuoteBomSN { get; set; }


    }
}