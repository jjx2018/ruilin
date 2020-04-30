using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace AppBoxPro
{
    public class PrdMission
    {
        [Key]
        public int Sn { get; set; }

        public string PrdMissionNo { get; set; }//订单号码、生产单号
        public string OrderNo { get; set; }//客人订单号码---锐麟对客户的号码，给个字段输入即可
        public string ClientId { get; set; }//客户代号  客户代码
        public DateTime? InputDate { get; set; } //接单日期
        public string Inputer { get; set; }//业务
        public DateTime? PStartDate { get; set; } //发单日期
        public DateTime? PFinishDate { get; set; }//出货日期
        public string Reserve1 { get; set; }//批号  
    }
}