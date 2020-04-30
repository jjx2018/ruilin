using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class BaseData
    {
        [Key]
        public int SN { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public int? SortIndex { get; set; }
        public string SType { get; set; }
    }
}