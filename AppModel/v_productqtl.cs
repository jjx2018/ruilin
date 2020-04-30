using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class v_productqtl
    {
       
        public int SN { get; set; }
        [Key]
        public int odtsn { get; set; }
        public int? IsBom { get; set; }
        public string OrderNo { get; set; }
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public int? Quantity { get; set; }
        public DateTime? RecOrderDate { get; set; }
        public DateTime? SendOrderDate { get; set; }
        public DateTime? OutGoodsDate { get; set; }
        public DateTime? plandate { get; set; }
        public DateTime? orderdate { get; set; }
        public DateTime? InputeDate { get; set; }
        public double? qtl { get; set; }
        public DateTime? ZhuangPeiDate { get; set; }
        public DateTime? pdate { get; set; }

    }
}