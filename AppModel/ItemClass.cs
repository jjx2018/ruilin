using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AppBoxPro
{
    public class ItemClass
    {
        [Key]
        public int Sn { get; set; }

        public string Name { get; set; }

        public string SubjectCode { get; set; }
    }
}