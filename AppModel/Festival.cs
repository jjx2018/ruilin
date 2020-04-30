using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro 
{
    public class Festival
    {
        [Key]
        public int ID { get; set; }
        public DateTime fdate { get; set; }
        [StringLength(50)]
        public string fname { get; set; }
    }
}