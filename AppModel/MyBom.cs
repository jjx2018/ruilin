using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro 
{
    public class MyBom
    {
        [Key]
        public int SN { get; set; }
        /// <summary>
        /// bomheader表主键
        /// </summary>
        public int? FSN { get; set; }
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
        /// 物料表中的SN
        /// </summary>
        public int? AllitemSN { get; set; }
        /// <summary>
        /// 产品编号、料号
        /// </summary>
        public string ItemNo { get; set; }
        /// <summary>
        /// 产品名称
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
        /// 用量
        /// </summary>
        public double UsingQuantity { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Sclass { get; set; }
        /// <summary>
        /// 生产方式（生产、外购、发外加工）
        /// </summary>
        public string MakeMethod { get; set; }
        /// <summary>
        /// 录入人ID
        /// </summary>
        public string Inputer { get; set; }
        /// <summary>
        /// 录入日期
        /// </summary>
        public DateTime? InputeDate { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string Updater { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        public string WorkShop { get; set; }
    }
}