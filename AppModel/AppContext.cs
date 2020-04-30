using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
 

namespace AppBoxPro
{
    public class AppContext : DbContext
    {
        public AppContext()
            : base("Default")
        {
            //Database.SetInitializer<DbContext>(new DropCreateDatabaseAlways<DbContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //这句是不要将EF生成的sql表名不要被复数 就是表名后面不要多加个S
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Client> clients { get; set; }
        //public DbSet<Item> items { get; set; }
        public DbSet<RLItems> allitems { get; set; }
        public DbSet<ItemClass> itemclasses { get; set; }
        public DbSet<ItemColor> itemcolors { get; set; }
        public DbSet<OrderHeader> orderheader { get; set; }
        public DbSet<OrderDetail> orderdetail { get; set; }
        //public DbSet<PrdMission> prdmission { get; set; }
        //public DbSet<PrdMissionDtl> prdmissiondtl { get; set; }
        public DbSet<Provider> providers { get; set; }
        public DbSet<ProBomHeader> probombase { get; set; }
        public DbSet<ProBomDetail> probomdtl { get; set; }
        public DbSet<BomHeader> bombase { get; set; }
        public DbSet<BomDetail> bomdtl { get; set; }
        public DbSet<Instruction> instruction { get; set; }
        public DbSet<COLOR> colors { get; set; }

        public DbSet<PrdMissionSL> prdmissionsl { get; set; }
        public DbSet<ProducePlan> produceplan { get; set; }
        public DbSet<ProduceOrderHeader> produceorderheader { get; set; }
        public DbSet<ProduceOrderDetail> produceorderdetail { get; set; }

        public DbSet<ProduceOrderRec> produceOrderRec { get; set; }

        public DbSet<PurchasePlan> purchaseplan { get; set; }
        public DbSet<PurchaseOrderHeader> purchaseorderHeader { get; set; }
        public DbSet<PurchaseOrderDetail> purchaseorderDetail { get; set; }
        public DbSet<Notice> notices { get; set; }
        public DbSet<SendOutProcessHeader> sendoutprocessheader { get; set; }
        public DbSet<SendOutProcessDetail> sendoutprocessdetail { get; set; }
        public DbSet<SendOutPlan> sendoutplan { get; set; }
        public DbSet<StockHeader> StockHeaders { get; set; }

        public DbSet<PurchaseStockList> PurchaseStockLists { get; set; }

        public DbSet<SendOutStockList> SendOutStockLists { get; set; }

        public DbSet<ProductStockList> ProductStockLists { get; set; }

        //GoodsClass
        public DbSet<StoreHouse> storehouse { get; set; }
        public DbSet<GoodsClass> goodsclass { get; set; }
        public DbSet<MainFrom> mainfrom { get; set; }
        public DbSet<BaseData> basedata { get; set; }
        public DbSet<CPlan> cpplan { get; set; }
        public DbSet<CPOrderHeader> cporderheader { get; set; }
        public DbSet<CPOrderDetail> cporderdetail { get; set; }
        //用户视图
        public DbSet<V_UserInfor> v_userinfor { get; set; }
        public DbSet<MaxOrderDate> v_maxorderdate { get; set; }
        public DbSet<MaxPlanDate> v_maxplandate { get; set; }
        public DbSet<v_productqtl> v_proqtl { get; set; }
        public DbSet<v_qtldetail> v_qtlmx { get; set; }
        public DbSet<ProcessRoute> processroute { get; set; }
    }
} 