using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppBoxPro 
{
    public class AllItem
    {
        [Key]
        public int Sn { get; set; }
        /// <summary>
        /// 料号
        /// </summary>
        public string ItemNo { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
       /// <summary>
        /// 规格
       /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 工艺加工费
        /// </summary>
        public double? ProcessCost { get; set; }
        /// <summary>
        /// 工艺加工费类型
        /// </summary>
        public string ProcessCostType { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double? Price { get; set; }
        /// <summary>
        /// 单重
        /// </summary>
        public double? UnitWeight { get; set; }
        public int? ProUsingQuantity { get; set; }
        /// <summary>
        /// 材质
        /// </summary>
        public string Material { get; set; }
        /// <summary>
        /// 表面处理或颜色
        /// </summary>
        public string ItemColor { get; set; }
        /// <summary>
        /// 底数
        /// </summary>
        public int? BaseNum { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public int? SupplierId { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        public string WorkShop { get; set; }
        /// <summary>
        /// 主要来源
        /// </summary>
        public string MainFrom { get; set; }
        public string ZongCheng { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string StoreHouse { get; set; }
        //public string Class { get; set; }
        //public string SubjectCode { get; set; }
        //public string EngName { get; set; }
        //public string Model { get; set; }
        //public string Barcode { get; set; }
        //public string MastStore { get; set; }
        //public string SourceType { get; set; }
        //
        //public string SupItemNo { get; set; }
        //public string RegisterCode { get; set; }
        //public string SizeSchem { get; set; }
        //public string ISO9000 { get; set; }
        //public string RoHS { get; set; }
        //public string RefCost { get; set; }
        //public string Price { get; set; }
        //public string PredictPrice { get; set; }
        //public string ProcessFee { get; set; }
        //public string CoPrice { get; set; }
        //public string Package { get; set; }
        //public string PackageNum { get; set; }
        //public string RefSpec { get; set; }
        //public string RefLots { get; set; }
        //public string RefAdd { get; set; }
        //public string Unit { get; set; }
        //public string PackageUnit { get; set; }
        //public string ValidDate { get; set; }
        //public string StockPos { get; set; }
        //public string Manufacture { get; set; }
        //public string Litigant { get; set; }
        //public string Bom { get; set; }
        //public string Samplebill { get; set; }
        //public string Stocknum { get; set; }
        //public string StockOkNum { get; set; }
        //public string StockSum { get; set; }
        //public string Tax { get; set; }
        //public string TaxType { get; set; }
        //public string TaxType3 { get; set; }
        //public string CostPreType { get; set; }
        //public string Accounts { get; set; }
        //public string OrderType { get; set; }
        //public string LotSize { get; set; }
        //public string LTBuy { get; set; }
        //public string LTMission { get; set; }
        //public string LTCoProcess { get; set; }
        //public string WastRate { get; set; }
        //public string MaxStock { get; set; }
        //public string MinStock { get; set; }
        //public string Record { get; set; }
        //public string Inputer { get; set; }
        //public string Auditer { get; set; }
        //public string AuditDate { get; set; }
        //public string ProDiscrip { get; set; }
        //public string ProPicture { get; set; }
        //public string Layer { get; set; }
        //public string Thickness { get; set; }

        //public string SubCls1 { get; set; }
        //public string SubCls2 { get; set; }
        //public string SubCls3 { get; set; }
        //public string Remark { get; set; }
        //public string Reserve1 { get; set; }
        //public string Reserve2 { get; set; }
        //public string Reserve3 { get; set; }
        //public string RecFlag { get; set; }
        //public string ProKeZhong { get; set; }
        //public string ProRongZhong { get; set; }
        //public string ProArea { get; set; }
        //public string SalePrice1 { get; set; }
        //public string SalePrice2 { get; set; }
        //public string SalePrice3 { get; set; }
        //public string SalePrice4 { get; set; }
        //public string SalePrice5 { get; set; }
        //public string SalePrice6 { get; set; }
        //public string SalePrice7 { get; set; }
        //public string SalePrice8 { get; set; }
        //public string CostPrice1 { get; set; }
        //public string CostPrice2 { get; set; }
        //public string CostPrice3 { get; set; }
        //public string CostPrice4 { get; set; }
        //public string CostPrice5 { get; set; }
        //public string CostPrice6 { get; set; }
        //public string CostPrice7 { get; set; }
        //public string CostPrice8 { get; set; }
        //public string InPack { get; set; }
        //public string PackSpec { get; set; }
        //public string PackUnit1 { get; set; }
        //public string PackUnit2 { get; set; }
        //public string PackUnit3 { get; set; }
        //public string PackUnit4 { get; set; }
        //public string PackUnit5 { get; set; }
        //public string PackUnit6 { get; set; }
        //public string PackUnit7 { get; set; }
        //public string PackUnit8 { get; set; }
        //public string MoldFlag { get; set; }
        //public string MaftName { get; set; }
        //public string MaftMan { get; set; }
        //public string MaftAdd { get; set; }
        //public string MaftTel { get; set; }
        //public string MaftFax { get; set; }
        //public string MaftRange { get; set; }
        //
        //public string AddReserve2 { get; set; }
        //public string AddReserve3 { get; set; }
        //public string AddReserve4 { get; set; }
        //public string AddReserve5 { get; set; }
        //public string AddReserve6 { get; set; }
        //public string AddReserve7 { get; set; }
        //public string AddReserve8 { get; set; }
        //
        //public string UnitName { get; set; }         
    }
}