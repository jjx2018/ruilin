using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro 
{
    public class PingJia
    {
        [Key]
        public int id { get; set; }

        public int ordertimeframe { get; set; }

        public int goodcount { get; set; }

        public int badcount { get; set; }

        public DateTime gettime { get; set; }

        public int userno { get; set; }

        public string username { get; set; }

        public DateTime dzdate { get; set; }
    }
}