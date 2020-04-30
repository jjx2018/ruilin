using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class GoodsClass
    {
        [Key]
        public int SN { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public int? SortIndex { get; set; }
    }
}