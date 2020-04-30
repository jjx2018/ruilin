using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class RLItems
    {
        [Key]
        public int SN { get; set; }
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public string Spec { get; set; }
        public string Material { get; set; }
        public string SurfaceDeal { get; set; }
        public int? ProUsingQuantity { get; set; }
        public string Sclass { get; set; }
        public string Inputer { get; set; }
        public DateTime? InputeDate { get; set; }
        public string Updater { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string WorkShop { get; set; }
        public int? ZuHe { get; set; }
        public string Remark { get; set; }
        public string ZongCheng { get; set; }
        public int? BaseNum { get; set; }
        public string MainFrom { get; set; }
        public string StoreHouse { get; set; }
        public double? Price { get; set; }
        public double? ProcessCost { get; set; }
        public string ProcessCostType { get; set; }
        public double? UnitWeight { get; set; }
        public int? SupplierId { get; set; }
        public string Unit { get; set; }

    }
}