using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class CardInfo
    {
        [Key]
        public Int64 cardID { get; set; }

        public int userNO { get; set; }//
        public string userName { get; set; }
        [Required]
        public decimal cash { get; set; }
        public int payCount { get; set; } 
    }
}