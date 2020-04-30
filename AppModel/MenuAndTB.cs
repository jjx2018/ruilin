using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class MenuAndTB
    {
        [Key]
        public int id { get; set; }
        public int TB { get; set; }
        public DateTime sdate { get; set; }
        [StringLength(200)]
        public string menucontent { get; set; }
        public float money { get; set; }
        public int state { get; set; }

        public string weekday { get; set; }
    }
}