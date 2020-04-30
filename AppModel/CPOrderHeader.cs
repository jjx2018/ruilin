using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class CPOrderHeader
    {
        [Key]
        public int SN { get; set; }
        public string CPOrderNo { get; set; }
        public string CPPlanNo { get; set; }
        public string SaleOrderNo { get; set; }
        public DateTime? CPDate { get; set; }
        public string Provider { get; set; }
        public string ProviderID { get; set; }
        public string JBRID { get; set; }
        public string JBRName { get; set; }
        public string ContactMan { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public DateTime? JHDate { get; set; }
        public string JHPlace { get; set; }
        public string JSFS { get; set; }
        public int? ZZSFP { get; set; }
        public string ProviderConfirm { get; set; }
        public string ApproveID { get; set; }
        public string CheckerID { get; set; }
        public string MakerID { get; set; }
        public string Inputer { get; set; }
        public DateTime? InputeDate { get; set; }
        public int? State { get; set; }

    }
}