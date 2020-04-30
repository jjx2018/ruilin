using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppBoxPro 
{
    public class PrdMissionDtl
    {
        [Key]
        public int Sn { get; set; }
        public string PrdMissionNo { get; set; }
        public string Reserve1 { get; set; }//客人编号 对外的产品编码  
        public string Reserve2 { get; set; }//客人编号 对内的产品编码  
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public string Spec { get; set; }//我司型号
        public double? Quantity { get; set; }//数量
        public double? Price { get; set; }
        public string ProdDesp { get; set; }
    }
}