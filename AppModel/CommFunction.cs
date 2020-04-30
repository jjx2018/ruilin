using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AppBoxPro
{
    public class CommFunction
    {
        log4net.ILog log = log4net.LogManager.GetLogger("magPlan.aspx");
        /// <summary>
        /// 判断是否是数字  2019年9月22日20:54:40  Dennyhui
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }
        public static string MakeBomByOrder(string odtSN, string itemno, string orderno, string quantity, string userid, string tabname)
        {
            try
            {
                SQLHelper.DbHelperSQL.SetConnectionString("");
                string sql = "select count(*) from dbo.BomHeader where OdtSN=" + odtSN + "";
                FileOper.writeLog("sql11:::::" + sql);
                if (SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() != "0")
                {
                    return "已生成";
                }
                sql = "select sn from dbo.proBomHeader where ver=(select max(ver) from proBomHeader where prono='" + itemno + "') and  prono='" + itemno + "'";
                string sn = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();

                ArrayList al = new ArrayList();
                sql = "INSERT INTO BomHeader(AllitemSN,OrderNo,ProName,ProNo,Ver,ClientProNo,ClientCode,Inputer,InputeDate,BomDate,ProBomSN,OdtSN)   select AllitemSN,'" + orderno + "',ProName,ProNo,Ver,ClientProNo,ClientCode,'" + userid + "',getdate(),BomDate,sn," + odtSN + " from dbo.proBomHeader where sn=" + sn;
                FileOper.writeLog("sql22:::::" + sql);
                al.Add(sql);
                string fsn = "select sn from BomHeader where OdtSN=" + odtSN;
                sql = "INSERT INTO BomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity,OrderUsingQuantity,Sclass,Inputer,InputeDate,ParentSN,SUBSN,ZuHe,WorkShop,seq,ZongCheng,BaseNum,MainFrom,StoreHouse,IsValid) select (" + fsn + "),AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity,(case a.ZuHe when 1 then CEILING(cast(ProUsingQuantity as float) / BaseNum *" + quantity + " * (dbo.[Fn_GetParentQuantity](a.ParentSN)) ) else (ProUsingQuantity * " + quantity + ") end) OrderUsingQuantity,Sclass,Inputer,getdate(),ParentSN,SubSN,ZuHe,WorkShop,seq,ZongCheng,BaseNum,MainFrom,StoreHouse,0 from proBomDetail a where fsn=" + sn;
                al.Add(sql);
                FileOper.writeLog("sql33:::::" + sql);
                if (tabname.ToLower() == "orderdetail")
                {
                    sql = "update orderdetail set isbom=1,bomver=(select max(ver) from proBomHeader where prono='" + itemno + "') where sn=" + odtSN;
                    al.Add(sql);
                }
                else
                {
                    //sql = "update orderheader set isbom=1  where SN=(select fsn from orderdetail where SN="+ odtSN + ") and Not exists (select 1 from orderdetail where  orderno='" + orderno + "' and sn not in (select odtsn from BomHeader where orderno='" + orderno+"')) ";
                    //al.Add(sql);
                }
                sql = "update OrderHeader  set IsBom=1 where Not exists (select 1 from   dbo.OrderDetail where FSN = OrderHeader.sn and IsBom = 0)";
                al.Add(sql);
                FileOper.writeLog("sql update:::::" + sql);
                bool flag = SQLHelper.DbHelperSQL.ExecuteSqlTran(al);
                if (flag)
                {
                    return "生成成功";
                }
                else
                {
                    return "生成失败";
                }


            }
            catch (Exception ee)
            {
                FileOper.writeLog("makebom error:" + ee.ToString());
                return "生成失败";
            }
        }

        public static string MakeBomForPJ(string odtSN, string itemno, string orderno, string quantity, string userid, out string FSN)
        {
            try
            {

                SQLHelper.DbHelperSQL.SetConnectionString("");
                string sql = "select count(*) from dbo.BomHeader where OdtSN=" + odtSN + "";
                FileOper.writeLog("sql11:::::" + sql);
                if (SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() != "0")
                {
                    sql = "select SN from dbo.BomHeader where OdtSN=" + odtSN;
                    FSN = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
                    return "已生成";
                }
                string AllitemSN = "select sn from dbo.rlitems where itemno='" + itemno + "'";
                //string sn = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();

                ArrayList al = new ArrayList();
                sql = "INSERT INTO BomHeader(OrderNo,ProName,ProNo,Inputer,InputeDate,BomDate,OdtSN)   select '" + orderno + "',ItemName,ItemNo,'" + userid + "',getdate(),getdate()," + odtSN + " from dbo.OrderDetail where sn=" + odtSN;
                FileOper.writeLog("sql22:::::" + sql);
                al.Add(sql);
                //string fsn = "select sn from BomHeader where OdtSN=" + odtSN;
                //sql = "INSERT INTO BomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity,OrderUsingQuantity,Sclass,Inputer,InputeDate,ParentSN,SUBSN,ZuHe,WorkShop,seq,ZongCheng,BaseNum,MainFrom,StoreHouse,IsValid) select (" + fsn + "),AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity," + quantity + "* (case a.ZuHe when 1 then CEILING((ProUsingQuantity / BaseNum) * (select ProUsingQuantity from ProBomDetail where SN =a.ParentSN) ) else ProUsingQuantity end) OrderUsingQuantity,Sclass,Inputer,getdate(),ParentSN,SN,ZuHe,WorkShop,seq,ZongCheng,BaseNum,MainFrom,StoreHouse,0 from proBomDetail a where fsn=" + sn;
                //al.Add(sql);
                //FileOper.writeLog("sql33:::::" + sql);
                sql = "update orderdetail set isbom=1 where sn=" + odtSN;
                al.Add(sql);
                sql = "update OrderHeader  set IsBom=1 where Not exists (select 1 from   dbo.OrderDetail where FSN = OrderHeader.sn and IsBom = 0)";
                al.Add(sql);
                bool flag = SQLHelper.DbHelperSQL.ExecuteSqlTran(al);
                if (flag)
                {
                    sql = "select SN from dbo.BomHeader where odtSN=" + odtSN;
                    FSN = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
                    return "生成成功";
                }
                else
                {
                    FSN = "";
                    return "生成失败";
                }


            }
            catch (Exception ee)
            {
                FileOper.writeLog("makebom error:" + ee.ToString());
                FSN = "";
                return "生成失败";
            }
        }

        public static string MakeBom(string odtSN,string itemno,string orderno,string quantity,string userid,out string FSN)
        {
            try
            {
                
                SQLHelper.DbHelperSQL.SetConnectionString("");
                string sql = "select count(*) from dbo.BomHeader where OdtSN=" + odtSN + "";
                FileOper.writeLog("sql11:::::" + sql);
                if (SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString() != "0")
                {
                    sql = "select SN from dbo.BomHeader where OdtSN=" + odtSN;
                    FSN = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
                    return "已生成";
                }
                sql = "select sn from dbo.proBomHeader where ver=(select max(ver) from proBomHeader where prono='" + itemno + "') and  prono='" + itemno + "'";
                string sn = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();

                ArrayList al = new ArrayList();
                sql = "INSERT INTO BomHeader(AllitemSN,OrderNo,ProName,ProNo,Ver,ClientProNo,ClientCode,Inputer,InputeDate,BomDate,ProBomSN,OdtSN)   select AllitemSN,'" + orderno + "',ProName,ProNo,Ver,ClientProNo,ClientCode,'" + userid + "',getdate(),BomDate,sn," + odtSN + " from dbo.proBomHeader where sn=" + sn;
                FileOper.writeLog("sql22:::::" + sql);
                al.Add(sql);
                string fsn = "select sn from BomHeader where OdtSN=" + odtSN;
                sql = "INSERT INTO BomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity,OrderUsingQuantity,Sclass,Inputer,InputeDate,ParentSN,SUBSN,ZuHe,WorkShop,seq,ZongCheng,BaseNum,MainFrom,StoreHouse,IsValid) select (" + fsn + "),AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity, (case a.ZuHe when 1 then CEILING(cast(ProUsingQuantity as float) / BaseNum * " + quantity + " * (dbo.[Fn_GetParentQuantity](a.ParentSN))) else (ProUsingQuantity * " + quantity + ") end) OrderUsingQuantity,Sclass,Inputer,getdate(),ParentSN,SubSN,ZuHe,WorkShop,seq,ZongCheng,BaseNum,MainFrom,StoreHouse,0 from proBomDetail a where fsn=" + sn;
                al.Add(sql);
                FileOper.writeLog("sql33:::::" + sql);
                sql = "update orderdetail set isbom=1,bomver=(select max(ver) from proBomHeader where prono='" + itemno + "') where sn=" + odtSN;
                al.Add(sql);
                sql = "update OrderHeader  set IsBom=1 where Not exists (select 1 from   dbo.OrderDetail where FSN = OrderHeader.sn and IsBom = 0)";
                al.Add(sql);
                bool flag = SQLHelper.DbHelperSQL.ExecuteSqlTran(al);
                if(flag)
                {
                    sql = "select SN from dbo.BomHeader where odtSN=" + odtSN;
                    FSN = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
                    return "生成成功";
                }
                else
                {
                    FSN = "";
                    return "生成失败";
                }
               
                
            }
            catch (Exception ee)
            {
                FileOper.writeLog("makebom error:" + ee.ToString());
                FSN = "";
                return "生成失败";
            }
        }
        public static bool reMakeBom(string odtSN, string itemno, string orderno,string ver, string quantity, string userid, out string FSN)
        {
            try
            {

                SQLHelper.DbHelperSQL.SetConnectionString("");
                string sql = "";
                sql = "select sn from dbo.proBomHeader where ver=" + ver + " and   prono='" + itemno + "'";
                string sn = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
                FileOper.writeLog("sql22:::::" + sql);
                ArrayList al = new ArrayList();
                sql = "INSERT INTO BomHeader(AllitemSN,OrderNo,ProName,ProNo,Ver,ClientProNo,ClientCode,Inputer,InputeDate,BomDate,ProBomSN,OdtSN)   select AllitemSN,'" + orderno + "',ProName,ProNo,Ver,ClientProNo,ClientCode,'" + userid + "',getdate(),BomDate,sn," + odtSN + " from dbo.proBomHeader where sn=" + sn;
                FileOper.writeLog("sql22:::::" + sql);
                al.Add(sql);
                string fsn = "select max(sn) from BomHeader where orderno='"+orderno+"' and ver="+ver+" and OdtSN=" + odtSN;
                sql = "INSERT INTO BomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity,OrderUsingQuantity,Sclass,Inputer,InputeDate,ParentSN,SUBSN,ZuHe,WorkShop,seq,ZongCheng,BaseNum,MainFrom,StoreHouse,IsValid) select (" + fsn + "),AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity, (case a.ZuHe when 1 then CEILING(cast(ProUsingQuantity as float) / BaseNum * " + quantity + " * (dbo.[Fn_GetParentQuantity](a.ParentSN)) ) else (ProUsingQuantity * " + quantity + ") end) OrderUsingQuantity,Sclass,Inputer,getdate(),ParentSN,SubSN,ZuHe,WorkShop,seq,ZongCheng,BaseNum,MainFrom,StoreHouse,0 from proBomDetail a where fsn=" + sn;
                al.Add(sql);
                FileOper.writeLog("sql33:::::" + sql);
                sql = "update orderdetail set isbom=1,bomver="+ver+" where sn=" + odtSN;
                al.Add(sql);
                sql = "update OrderHeader  set IsBom=1 where Not exists (select 1 from   dbo.OrderDetail where FSN = OrderHeader.sn and IsBom = 0)";
                al.Add(sql);
                bool flag = SQLHelper.DbHelperSQL.ExecuteSqlTran(al);
                if (flag)
                {
                    sql = "select SN from dbo.BomHeader where orderno='" + orderno + "' and odtSN=" + odtSN;
                    FSN = SQLHelper.DbHelperSQL.GetSingle(sql, 30).ToString();
                }
                else
                {
                    FSN = "";
                }
                return flag;
            }
            catch (Exception ee)
            {
                FileOper.writeLog("makebom error:" + ee.ToString());
                FSN = "";
                return false;
            }
        }

        public static string PLsendInstruction(string OrderNo, string ProNo, string ProName, string ReceiveDept, string Receiver, string OdtSN,string userid,FineUIPro.Grid grid)
        {
            StringBuilder sql = new StringBuilder();
            ArrayList al = new ArrayList();
            string s = "";
            SQLHelper.DbHelperSQL.SetConnectionString("");
            foreach (int i in grid.SelectedRowIndexArray)
            {
                s = "select count(*) from Instruction where OrderNo='" + OrderNo + "' and OdtSN=" + OdtSN + " and itemno='" + grid.Rows[i].Values[2].ToString() + "' and IsConfirm=0";
                if (int.Parse(SQLHelper.DbHelperSQL.GetSingle(s, 30)) > 0)
                {
                    continue;
                }
                s = "select (case  when sum(ConfirmQuantity) is null then 0 else sum(ConfirmQuantity) end) from instruction where orderno='" + OrderNo + "' and OdtSN=" + OdtSN + " and itemno='" + grid.Rows[i].Values[2].ToString() + "'";
                sql.Clear();
                sql.Append("insert into Instruction(OrderNo,ProNo,ProName,ItemNo,ItemName,Spec,Material,SurfaceDeal,UsingQuantity,Sclass,MainFrom,Inputer,InputeDate,IsConfirm,IsPlan,ReceiveDept,Receiver,BarCode,BomSN,OdtSN)");
                sql.Append(" values(");
                sql.Append("'" + OrderNo + "',");
                sql.Append("'" + ProNo + "',");
                sql.Append("'" + ProName + "',");
                sql.Append("'" + grid.Rows[i].Values[2].ToString() + "',");//ItemNo
                sql.Append("'" + grid.Rows[i].Values[3].ToString() + "',");//ItemName
                sql.Append("'" + grid.Rows[i].Values[4].ToString() + "',");//Spec
                sql.Append("'" + grid.Rows[i].Values[5].ToString() + "',");//Material
                sql.Append("'" + grid.Rows[i].Values[6].ToString() + "',");//SurfaceDeal
                sql.Append("" + grid.Rows[i].Values[8].ToString() + "-(" + s + "),");//OrderUsingQuantity
                sql.Append("'" + grid.Rows[i].Values[9].ToString() + "',");//Sclass
                sql.Append("'" + grid.Rows[i].Values[12].ToString() + "',");//MainFrom


                sql.Append("'" + userid+ "',");
                sql.Append("getdate(),");
                sql.Append("0,");
                sql.Append("0,");

                sql.Append("'" + ReceiveDept + "',");
                sql.Append("'" + Receiver + "',");
                System.Threading.Thread.Sleep(1);
                sql.Append("'" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "',");
                sql.Append(grid.DataKeys[i][0] + ",");
                sql.Append(OdtSN);
                sql.Append(")");
                al.Add(sql.ToString());
                //FileOper.writeLog(sql.ToString());
                FileOper.writeLog(sql.ToString());
            }

            if (SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
            {
                return ("发送成功");
            }
            else
            {
                return ("发送失败");
            }
        }

        public static string NewPLsendInstruction(string OrderNo, string ProNo, string ProName, string ReceiveDept, string Receiver, string OdtSN, string userid, FineUIPro.Grid grid)
        {
            StringBuilder sql = new StringBuilder();
            ArrayList al = new ArrayList();
            string s = "";
            SQLHelper.DbHelperSQL.SetConnectionString("");
            foreach (int i in grid.SelectedRowIndexArray)
            {
                s = "select count(*) from Instruction where OrderNo='" + OrderNo + "' and OdtSN=" + OdtSN + " and itemno='" + grid.Rows[i].Values[2].ToString() + "' and IsConfirm=0";
                if (int.Parse(SQLHelper.DbHelperSQL.GetSingle(s, 30)) > 0)
                {
                    continue;
                }
                s = "select (case  when sum(ConfirmQuantity) is null then 0 else sum(ConfirmQuantity) end) from instruction where orderno='" + OrderNo + "' and OdtSN=" + OdtSN + " and itemno='" + grid.Rows[i].Values[2].ToString() + "'";
                sql.Clear();
                sql.Append("insert into Instruction(OrderNo,ProNo,ProName,ItemNo,ItemName,Spec,Material,SurfaceDeal,UsingQuantity,Sclass,MainFrom,Inputer,InputeDate,IsConfirm,IsPlan,ReceiveDept,Receiver,BarCode,BomSN,OdtSN,ConfirmQuantity,RealUsingQuantity,ConfirmDate)");
                sql.Append(" values(");
                sql.Append("'" + OrderNo + "',");
                sql.Append("'" + ProNo + "',");
                sql.Append("'" + ProName + "',");
                sql.Append("'" + grid.Rows[i].Values[2].ToString() + "',");//ItemNo
                sql.Append("'" + grid.Rows[i].Values[3].ToString() + "',");//ItemName
                sql.Append("'" + grid.Rows[i].Values[4].ToString() + "',");//Spec
                sql.Append("'" + grid.Rows[i].Values[5].ToString() + "',");//Material
                sql.Append("'" + grid.Rows[i].Values[6].ToString() + "',");//SurfaceDeal
                sql.Append("" + grid.Rows[i].Values[8].ToString() + ",");//UsingQuantity
                sql.Append("'" + grid.Rows[i].Values[9].ToString() + "',");//Sclass
                sql.Append("'" + grid.Rows[i].Values[12].ToString() + "',");//MainFrom


                sql.Append("'" + userid + "',");
                sql.Append("getdate(),");
                sql.Append("1,");//IsConfirm
                sql.Append("0,");//IsPlan

                sql.Append("'" + ReceiveDept + "',");
                sql.Append("'" + Receiver + "',");
                System.Threading.Thread.Sleep(1);
                sql.Append("'" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "',");
                sql.Append(grid.DataKeys[i][0] + ",");
                sql.Append(OdtSN+",");
                sql.Append("0,");//ConfirmQuantity
                sql.Append("" + grid.Rows[i].Values[8].ToString() + ",");//RealUsingQuantity
                sql.Append("getdate()");//ConfirmDate
                sql.Append(")");
                al.Add(sql.ToString());
                //FileOper.writeLog(sql.ToString());
                FileOper.writeLog(sql.ToString());
            }

            if (SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
            {
                return ("发送成功");
            }
            else
            {
                return ("发送失败");
            }
        }

        public static void updateRowCss(string ridx,FineUIPro.Grid grid,string cssname)
        {
            grid.Rows[int.Parse(ridx)].RowCssClass = cssname;
        }



    }
}