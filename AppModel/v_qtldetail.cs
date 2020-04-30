using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class v_qtldetail
    {
        //public int id { get; set; }
        public string OrderNo { get; set; }
        public string ProNo { get; set; }
        public string ProName { get; set; }
        [Key]
        public Int64 id { get; set; }
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public double? OrderUsingQuantity { get; set; }
        public decimal? Quantity { get; set; }
        public string MainFrom { get; set; }
        public DateTime? PDate { get; set; }

        
    }
}