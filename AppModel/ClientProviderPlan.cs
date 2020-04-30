using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class ClientProviderPlan
    {
        [Key]
        public int SN { get; set; }
        public string SeqNo { get; set; }
        public string CPPlanNo { get; set; }
        public string Properties { get; set; }
        public DateTime? PDate { get; set; }
        public string Checker { get; set; }
        public string Dept { get; set; }
        public string ClientProvider { get; set; }
        public string ProNo { get; set; }
        public string ProName { get; set; }
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public string Spec { get; set; }
        public double? Quantity { get; set; }
        public string Unit { get; set; }
        public string SaleOrderNo { get; set; }
        public string ClientOrderNo { get; set; }
        public int? State { get; set; }
        public string ProLotNo { get; set; }
        public string WorkShop { get; set; }
        public string Inputer { get; set; }
        public DateTime InputeDate { get; set; }
        public string Remark { get; set; }
        public int BomSN { get; set; }
    }
}