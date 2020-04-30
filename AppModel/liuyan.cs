using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace AppBoxPro 
{
    public class liuyan  
    {
        [Key]
        public int ID { get; set; }

        [StringLength(100)]
        public string title { get; set; }
        [StringLength(400)]
        public string scontent { get; set; }

        public DateTime sdate { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        public int userno { get; set; }
    }
}