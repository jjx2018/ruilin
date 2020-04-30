using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AppBoxPro
{
    public class PrdMissionSL
    {
        [Key]
        public int SN { get; set; }
        public string PrdMissionNo { get; set; }
        public DateTime? PRODate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WORKGRP { get; set; }
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public string Spec { get; set; }
        public string QCStandard { get; set; }
        public string Client { get; set; }
        public string ORDERNO { get; set; }
        public string CHECKMAN { get; set; }
        public string PackMan { get; set; }
        public string COLOR { get; set; }
        public string LOTNO { get; set; }
        public string GRADE { get; set; }
        public double? WEIGHT { get; set; }
        public double? Quantity { get; set; }
        public double? Price { get; set; }
        public double? Cost { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Thickness { get; set; }
        public string CASENO { get; set; }
        public string LabelName { get; set; }
        public string BoxLblName { get; set; }
        public int? WlStatus { get; set; }
        public int? IsReturn { get; set; }
        public int? IsPrint { get; set; }
        public string Password { get; set; }
        public int? IsCancel { get; set; }
        public string OPERATOR { get; set; }
        public DateTime? InputDate { get; set; }
        public string KaiDan { get; set; }
        public DateTime? KaiDanDate { get; set; }
        public string Auditer { get; set; }
        public DateTime? AuditDate { get; set; }
        public string PiZhun { get; set; }
        public DateTime? PiZhunDate { get; set; }
        public string REMARK { get; set; }
        public string RESERVE1 { get; set; }
        public string RESERVE2 { get; set; }
        public string RESERVE3 { get; set; }
        public string RESERVE4 { get; set; }
        public string RESERVE5 { get; set; }
        public string RESERVE6 { get; set; }
        public string RESERVE7 { get; set; }
        public string RESERVE8 { get; set; }
        public string RESERVE9 { get; set; }
        public string RESERVE10 { get; set; }
        public string RESERVE11 { get; set; }
        public string RESERVE12 { get; set; }
        public string RESERVE13 { get; set; }
        public string RESERVE14 { get; set; }
        public string RESERVE15 { get; set; }
        public string RESERVE16 { get; set; }
        public string RESERVE17 { get; set; }
        public string RESERVE18 { get; set; }
        public string RESERVE19 { get; set; }
        public string RESERVE20 { get; set; }
        public int? ParentSn { get; set; }
        public string recClass { get; set; }

    }
}