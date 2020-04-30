using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class PartInfo
    {
        [Key]
        [StringLength(30)]
        public string partID { get; set; }
        [StringLength(30)]
        public string partName { get; set; }
    }
}