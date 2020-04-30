using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppBoxPro 
{
    public class Client
    {
        [Key]
        public int SN { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        [StringLength(10)]
        public string ClientNo { get; set; }
        /// <summary>
        /// 客户代号
        /// </summary>
        [StringLength(20)]
        public string subjectcode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; }
        public string Country { get; set; }
        public string ContactMan { get; set; }
        public string Email { get; set; }
        public string website { get; set; }
        public string busiowner { get; set; }
        public string paymode { get; set; }
        public string remark { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        [StringLength(60)]
        public string Address { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        [StringLength(20)]
        public string Telephone { get; set; }
        /// <summary>
        /// 传值
        /// </summary>
        [StringLength(20)]
        public string Fax { get; set; }
        /// <summary>
        /// 开户账号
        /// </summary>
        [StringLength(50)]
        public string Account{get;set;}
        /// <summary>
        /// 开户银行
        /// </summary>
        [StringLength(60)]
        public string Bank{get;set;}
        /// <summary>
        /// 摘要
        /// </summary>
        [StringLength(100)]
        public string Abstract{get;set;}

    /// <summary>
    /// 未用
    /// </summary>
        public int? RecFlag{get;set;}
        /// <summary>
        /// 保留字段
        /// </summary>
        [StringLength(20)]
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3{ get; set; }

    }
}