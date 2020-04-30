using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro 
{
    public class Notice
    {
        [Key]
        public int ID { get; set; }

        [StringLength(100)]
        public string ggname { get; set; }
        [StringLength(4000)]
        public string ggcontent { get; set; }

        public DateTime pbdate { get; set; }

        [StringLength(50)]
        public string pbusername { get; set; }
        public int state { get; set; }

        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
    }
}