using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AppBoxPro
{
    public class YW_Goods
    {
       [Key]
        public int ID { get; set; }
        
        public DateTime jhdate { get; set; }// 交货日期

        public DateTime xddate { get; set; } //下单日期
        public string orderno { get; set; }//单号
        public string state { get; set; }//状态
        public string clientname { get; set; }//客户名称
        public string proname { get; set; }//产品名称
        public string spec { get; set; }//规格
        public string unit { get; set; }//单位
        public double plancount { get; set; }//计划量
        public double dzprice { get; set; }//折后单价
        public double money { get; set; }//金额
        public string remark { get; set; }//备注
        public string finished { get; set; }//已完成

    }
}