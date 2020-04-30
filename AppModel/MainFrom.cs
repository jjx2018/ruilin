using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class MainFrom
    {
        [Key]
        public int SN { get; set; }
        public string MainFromCode { get; set; }
        public string MainFromName { get; set; }
        public int? SortIndex { get; set; }
    }
}