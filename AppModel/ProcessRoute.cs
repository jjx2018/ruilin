using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class ProcessRoute
    {
        [Key]
        public int sn { get; set; }
        public string itemno { get; set; }
        public int itemsn { get; set; }
        public string ProcessingSeq { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public string MoldelNo { get; set; }
        public string EquipmentNoName { get; set; }
        public string Nature { get; set; }
        public string Team { get; set; }
        public string Department { get; set; }
        public string WorkBatch { get; set; }
        public string FixPerTime { get; set; }
        public string ChangePerTime { get; set; }
        public string Price { get; set; }
        public string Remark { get; set; }

    }
}