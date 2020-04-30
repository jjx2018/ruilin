using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppBoxPro 
{
    public class Provider
    {
        [Key]
        public int SN { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string ProviderNo { get; set; }
        /// <summary>
        /// 供应商代号
        /// </summary>
        public string subjectcode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 开户账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string Bank { get; set; }
        public string Abstract { get; set; }
        public int? RecFlag { get; set; }
        public string Rank { get; set; }
        public string Stype { get; set; }
        public string ContactMan { get; set; }
        public string Email { get; set; }
        public string PurchaseMan { get; set; }
        public string IsValid { get; set; }
    }
}