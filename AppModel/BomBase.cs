using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppBoxPro
{
    public class BomBase
    {
        [Key]
        public int SN { get; set; }
        public int MastItenSn { get; set; }
        public string MastItemNo { get; set; }
        public string MastName { get; set; }
        public string Version { get; set; }
        public string MapNo { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string FileNo { get; set; }
        public string Reserve1 { get; set; }
        public string Reserve2 { get; set; }
        public string Reserve3 { get; set; }
        public string Reserve4 { get; set; }
        public string Reserve5 { get; set; }
        public string Reserve6 { get; set; }
        public string Reserve7 { get; set; }
        public string Reserve8 { get; set; }
        public string Reserve9 { get; set; }
        public string Reserve10 { get; set; }
        public string Remark { get; set; }











    }
}