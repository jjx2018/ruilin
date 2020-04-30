using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class PurchaseOrderHeader
    {
        [Key]
        public int SN { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PurPlanNo { get; set; }
        public string SaleOrderNo { get; set; }
        public string PurOrderNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PurDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Provider { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProviderID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JBRID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JBRName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ContactMan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Tel { get; set; }

        public string Fax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JHDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JHPlace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JSFS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? ZZSFP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProviderConfirm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ApproveID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CheckerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MakerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Inputer { get; set; }

        public DateTime? InputeDate { get; set; }
        /// <summary>
        /// 0未审核 1审核通过  2审核不通过
        /// </summary>
        public int? State { get; set; }

    }
}