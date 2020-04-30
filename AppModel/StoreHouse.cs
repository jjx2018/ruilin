using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class StoreHouse
    {
        [Key]
        public int SN { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public int? SortIndex { get; set; }

    }
}