using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class ordering  
    {
        [Key]
        public Int64 FID { get; set; }
        public int userno { get; set; }//用户编号
        public DateTime ordertime { get; set; }//预订时间
        public DateTime gettime { get; set; }//取餐时间
        public Int16 updatestate { get; set; } //状态
        public Int16 ordertimeframe { get; set; }//餐别
        public Decimal paymoney { get; set; }//餐费
       
    }
}