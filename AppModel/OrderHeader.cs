using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AppBoxPro 
{
    public class OrderHeader
    {
        [Key]
        public int SN { get; set; }
        public string OrderType { get; set; }
        /// <summary>
        /// 订单号码
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        public string OrderName { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string LotNo { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string ClientCode { get; set; }
        /// <summary>
        /// 客户订单编号
        /// </summary>
        public string ClientOrderNo { get; set; }
        /// <summary>
        /// 接单人编号
        /// </summary>
        public string RecOrderPersonID { get; set; }
        /// <summary>
        /// 接单人
        /// </summary>
        public string RecOrderPerson { get; set; }
        /// <summary>
        /// 接单日期
        /// </summary>
        public DateTime? RecOrderDate { get; set; }
        /// <summary>
        /// 发单日期
        /// </summary>
        public DateTime? SendOrderDate { get; set; }
        /// <summary>
        /// 出货日期
        /// </summary>
        public DateTime? OutGoodsDate { get; set; }
        /// <summary>
        /// 录单人
        /// </summary>
        public string Inputer { get; set; }
        /// <summary>
        /// 录单时间
        /// </summary>
        public DateTime? InputerDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Updater { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// 是否审核   0 未审核  1已审核
        /// </summary>
        public int? IsCheck { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string Checker { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? CheckDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 总经理
        /// </summary>
        public string Manager { get; set; }
        /// <summary>
        /// 生产经理
        /// </summary>
        public string ProductionManager { get; set; }
        /// <summary>
        /// 文件编号
        /// </summary>
        public string FileNo { get; set; }
        /// <summary>
        /// 是否生成了BOM 0 未生成  1生成
        /// </summary>
        public int? IsBom { get; set; }
        /// <summary>
        /// 柜型
        /// </summary>
        public string ContainerType { get; set; }
        public string OrderExcel { get; set; }
        /// <summary>
        /// 0 未确认  1 已确认
        /// </summary>
        public int? IsConfirm { get; set; }
    }
}