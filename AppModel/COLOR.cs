using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AppBoxPro 
{
    public class COLOR
    {
        [Key]
        public string code { get; set; }
        public string name { get; set; }
    }
}