using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppBoxPro 
{
    public class ItemColor
    {
        [Key]
        public int SN { get; set; }
        [StringLength(50)]
        public string OrderNo { get; set; }
        [StringLength(50)]
        public string PID { get; set; }
        [StringLength(50)]
        public string ItemNo { get; set; }
        [StringLength(50)]
        public string Color { get; set; }
                     
    }
}