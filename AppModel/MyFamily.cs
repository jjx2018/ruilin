using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro.AppModel
{
    public class MyFamily
    {
        [Key]
        public int id { get; set; }
        public int familyuserno { get; set; }
        public int myuserno { get; set; }

    }
}