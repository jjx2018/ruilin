using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro 
{
    public class MenuInfo
    {
        [Key]
        public int menuID{get;set;}
         [StringLength(30)]
        public string  menuName{get;set;}
        public decimal UnitPrice{get;set;}
         [StringLength(50)]
        public string note { get; set; }
    }
}