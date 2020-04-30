using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class myinfor
    {
        
        
        /// <summary>
        /// 产品编号、料号
        /// </summary>
        public string ItemNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
        public double UsingQuantity { get; set; }

    }
}