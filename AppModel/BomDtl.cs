using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppBoxPro 
{
    public class BomDtl
    {
        [Key]
        public int SN { get; set; }
        public int BOMSN { get; set; }
        public int SubItemSn { get; set; }
        public string SubName { get; set; }
        public string SunUnit { get; set; }
        public double? BaseConsume { get; set; }
        public double? Loss { get; set; }
        public double? ForDays { get; set; }
        public string SubCls { get; set; }
        public double? WasteRate { get; set; }
        public string AssemPos { get; set; }
        public int isInCost { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
        public string Reserve6 { get; set; }
        public string Reserve7 { get; set; }
        public string Reserve8 { get; set; }
        public string Reserve9 { get; set; }
        public string Reserve10 { get; set; }
        public string Remark { get; set; }

                                              
    }
}