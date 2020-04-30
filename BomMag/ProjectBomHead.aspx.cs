using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using FineUIPro;
using EntityFramework.Extensions;
using System.Data.OleDb;
using System.Data;
using System.Collections;
using System.Threading;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace AppBoxPro.BomMag
{
    public partial class ProjectBomHead : PageBase
    {
        static Hashtable htClickColsName = new Hashtable();

        log4net.ILog log = log4net.LogManager.GetLogger("magBom.aspx");
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "ProjectBOMView";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            // 权限检查
            //CheckPowerWithButton("InstockEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("InstockDelete", btnDeleteSelected);
            //CheckPowerWithButton("InstockNew", btnNew);



            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnNew.OnClientClick = Window1.GetShowReference("~/admin/user_new.aspx", "新增用户");

            // 每页记录数
            datePickerFrom.SelectedDate = DateTime.Today.AddMonths(-1);
            datePickerTo.SelectedDate = DateTime.Today;

            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            
            BindGrid();
        }


        public string gettype(string code)
        {
            if (code == "02")
            {
                return "进仓";
            }
            else if (code == "03")
            {
                return "出仓";
            }
            return "";
        }

        private void BindGrid()
        {
            using (var appdb = new AppContext())
            {
                DateTime now = DateTime.Now;
                DateTime d1 = new DateTime(now.Year, now.Month, 1);
                DateTime d2 = d1.AddDays(-1);
                //d1是本月的第一天，d2本月的最后一天，
                DateTime dtstart = datePickerFrom.SelectedDate == null ? d2 : datePickerFrom.SelectedDate.Value;
                DateTime dtend = datePickerTo.SelectedDate == null ? now.AddDays(2) : datePickerTo.SelectedDate.Value.AddDays(1);
                var q = from a in appdb.probombase
                          join b in appdb.v_userinfor on a.Inputer equals b.userid into userjoin
                          from c in userjoin.DefaultIfEmpty()
                          where a.InputeDate < dtend.Date
                          select new { a.ProName, a.ProNo, a.ClientCode, a.BomDate, a.Ver, a.FileNo, a.Remark, a.BomExcel, a.Inputer, a.InputeDate, c.ChineseName, a.SN };

                //在产品名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.ProNo.Contains(searchText));
                }

                foreach (DictionaryEntry de in htClickColsName)
                {
                    switch (de.Key.ToString())
                    {
                        case "ProNo":
                            q = q.Where(u => u.ProNo == de.Value.ToString());
                            break;
                        case "ProName":
                            q = q.Where(u => u.ProName == de.Value.ToString());
                            break;
                        case "BomDate":
                            q = q.Where(u => u.BomDate.ToString() == de.Value.ToString());
                            break;
                        case "ClientCode":
                            q = q.Where(u => u.ClientCode == de.Value.ToString());
                            break;
                        case "Ver":
                            q = q.Where(u => u.Ver.ToString() == de.Value.ToString());
                            break;
                        case "FileNo":
                            q = q.Where(u => u.FileNo == de.Value.ToString());
                            break;
                        case "ChineseName":
                            q = q.Where(u => u.ChineseName.ToString() == de.Value.ToString());
                            break;
                        case "InputeDate":
                            q = q.Where(u => u.InputeDate.ToString() == de.Value.ToString());
                            break;

                    }
                }



                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();// q.Count();

                // 排列和数据库分页
                q = SortAndPage(q, Grid1);

                Grid1.DataSource = q;// itemq.Take(2);// q;
                Grid1.DataBind();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string[] s = txtClickColsName.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (s == null || s.Length == 0)
            {
                return;
            }
            htClickColsName.Remove(s[0]);
            BindGrid();
            updatecol();
        }
        void updatecol()
        {
            txtClickColsName.Text = "";
            foreach (DictionaryEntry de in htClickColsName)
            {
                txtClickColsName.Text += de.Key.ToString() + ",";
            }

        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            string[] s = Grid1.SelectedCell;
            for (int i = 0; i < Grid1.Columns.Count; i++)
            {
                if (s[1] == Grid1.Columns[i].ColumnID && !htClickColsName.ContainsKey(s[1]))
                {
                    htClickColsName.Add(s[1], Grid1.Rows[e.RowIndex].Values[i].ToString());
                    break;
                }
            }
            BindGrid();
            updatecol();
        }

        #endregion

        #region Events


        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("InstockEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("InstockDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("InstockChangePassword", Grid1, "changePasswordField");

        }

        protected void Grid1_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {
            //User user = e.DataItem as User;

            //// 不能删除超级管理员
            //if (user.Name == "admin")
            //{
            //    FineUI.LinkButtonField deleteField = Grid1.FindColumn("deleteField") as FineUI.LinkButtonField;
            //    deleteField.Enabled = false;
            //    deleteField.ToolTip = "不能删除超级管理员！";
            //}

        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
       

 


        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            int ID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("ProjectBOMDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                using (AppContext appdb = new AppContext())
                {
                    //appdb.processRec.Where(u => u.ID == ID).Delete();

                    BindGrid();
                }
            }
        }
        
        protected void btnSearch_click(object sender, EventArgs e)
        {
            BindGrid();
        }
        
        
       
        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }



        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }
        


        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (filePhoto.HasFile)
            {
                string fileName = filePhoto.ShortFileName;
                if (!filePhoto.HasFile)
                {
                    // 清空文件上传控件
                    filePhoto.Reset();
                    return;
                }


                fileName = fileName.Replace(":", "").Replace(" ", "").Replace("\\", "").Replace("/", "");
                fileName = DateTime.Now.Ticks.ToString() + "-" + fileName;

                filePhoto.SaveAs(Server.MapPath("~/BOMFile/" + fileName));
                readExcel(fileName);


            }
        }

        private void readExcel(string filename)
        {
            string bomsn = "";
            try
            {
                string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/BOMFile/" + filename) + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"";

                using (OleDbConnection conn = new OleDbConnection(connstring))
                {
                    conn.Open();
                    DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" }); //得到所有sheet的名字  
                    //for(int k=0;k<sheetsName.Rows.Count;k++)
                    //{
                    //    log.Info(sheetsName.Rows[k][2].ToString());
                    //}
                    string firstSheetName = sheetsName.Rows[0][2].ToString(); //得到第一个sheet的名字  
                    string sql = string.Format("SELECT * FROM [{0}]", firstSheetName); //查询字符串   


                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                    DataSet set = new DataSet();
                    ada.Fill(set);
                    DataTable dt = set.Tables[0];
                    //for(int kk = 0; kk < dt.Columns.Count; kk++)
                    //{
                    //    log.Info(dt.Columns[kk].ColumnName);
                    //}
                    #region 判断版本号是否为数字和是否版本已存在
                    if (!CommFunction.IsNumeric(dt.Rows[3][9].ToString()))
                    {
                        Alert.Show("版本号必须为数字");
                        return;
                    }
                    SQLHelper.DbHelperSQL.SetConnectionString("");
                    sql = "select count(*) from probomheader where ProNo = '" + dt.Rows[3][2].ToString() + "' and Ver = '" + dt.Rows[3][7].ToString() + "'";
                    if (int.Parse(SQLHelper.DbHelperSQL.GetSingle(sql, 30)) > 0)
                    {
                        Alert.Show("该产品已经存在相同版本的工程BOM，请修改版本再上传！", MessageBoxIcon.Error);
                        return;
                    }
                    #endregion
                    #region 判断料号的合法性
                    DataTable errdt = new DataTable();
                    errdt.Columns.Add("Seq", typeof(string)); //数据类型为 文本
                    errdt.Columns.Add("ItemNo", typeof(string)); //数据类型为 文本
                    errdt.Columns.Add("ItemName", typeof(string)); //数据类型为 文本
                    errdt.Columns.Add("Spec", typeof(string)); //数据类型为 文本
                    errdt.Columns.Add("Material", typeof(string)); //数据类型为 文本
                    errdt.Columns.Add("SurfaceDeal", typeof(string)); //数据类型为 文本

                    DataTable errwldt = new DataTable();
                    errwldt.Columns.Add("Seq", typeof(string)); //数据类型为 文本
                    errwldt.Columns.Add("ItemNo", typeof(string)); //数据类型为 文本
                    errwldt.Columns.Add("ItemName", typeof(string)); //数据类型为 文本
                    errwldt.Columns.Add("Spec", typeof(string)); //数据类型为 文本
                    errwldt.Columns.Add("Material", typeof(string)); //数据类型为 文本
                    errwldt.Columns.Add("SurfaceDeal", typeof(string)); //数据类型为 文本
                    errwldt.Columns.Add("wlFrom", typeof(string)); //数据类型为 文本

                    int i = 5; int k = 0;
                    Regex reg = new Regex(@"^[A-Z]{2}[\d]{4}$");
                    bool err = false;
                    string seq = "";
                    for (; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i][0].ToString() == "锐 麟 铝 制 品 有 限 公 司" || dt.Rows[i][0].ToString() == "物料清单(BOM)" || dt.Rows[i][0].ToString() == "产品名称" || dt.Rows[i][0].ToString() == "产品编号" || dt.Rows[i][0].ToString() == "序号")
                        {

                            continue;
                        }
                        else
                        {
                            #region 判断料号的合法性

                            if (dt.Rows[i][0].ToString() == "" && dt.Rows[i][1].ToString() == "" && dt.Rows[i][2].ToString() == "" && dt.Rows[i][3].ToString() == "" && dt.Rows[i][4].ToString() == "" && dt.Rows[i][5].ToString() == "" && dt.Rows[i][6].ToString() == "" && dt.Rows[i][7].ToString() == "" && dt.Rows[i][8].ToString() == "" && dt.Rows[i][9].ToString() == "" && dt.Rows[i][10].ToString() == "" && dt.Rows[i][11].ToString() == "")
                            {
                                break;
                            }
                            else if (dt.Rows[i][0].ToString() != "" && dt.Rows[i][1].ToString() == "" && dt.Rows[i][2].ToString() == "" && dt.Rows[i][3].ToString() == "" && dt.Rows[i][4].ToString() == "" && dt.Rows[i][5].ToString() == "" && dt.Rows[i][6].ToString() == "" && dt.Rows[i][7].ToString() == "" && dt.Rows[i][8].ToString() == "" && dt.Rows[i][9].ToString() == "" && dt.Rows[i][10].ToString() == "" && dt.Rows[i][11].ToString() == "")
                            {
                                continue;
                            }
                            else
                            {
                                seq = dt.Rows[i][0].ToString();
                                string lastseq = dt.Rows[i - 1][0].ToString();
                                if (lastseq.IndexOf(".") != -1)
                                {
                                    lastseq = lastseq.Substring(0, lastseq.LastIndexOf("."));
                                }

                                #region 料号和序号不规范的物料
                                string tstr = dt.Rows[i][1].ToString().Replace(" ", "");
                                if (tstr.Length >= 6)
                                {
                                    if (CommFunction.IsNumeric(tstr) && tstr.Length == 7)
                                    {
                                        #region 序号问题
                                        if (seq.IndexOf(".") != -1 && seq.Substring(0, seq.LastIndexOf(".")) != lastseq && dt.Select("F1='" + seq.Substring(0, seq.LastIndexOf(".")) + "'").Count() == 0)
                                        {
                                            //如果已经存在错误列表中  不在重复添加
                                            if (errdt.Select("Seq='" + dt.Rows[i - 1][0].ToString() + "'").Count() > 0)
                                            {
                                                continue;
                                            }
                                            log.Info("seq::::" + seq.Substring(0, seq.LastIndexOf(".")) + ":::lastseq:::" + lastseq + ":::::" + dt.Select("F1='" + seq.Substring(0, seq.LastIndexOf(".")) + "'").Count().ToString() + "::::::" + "F1='" + seq.Substring(0, seq.LastIndexOf(".")) + "'");
                                            err = true;
                                            DataRow dr = errdt.NewRow();
                                            dr[0] = dt.Rows[i - 1][0].ToString() + "(上一条)"; //通过索引赋值
                                            dr[1] = dt.Rows[i - 1][1].ToString();
                                            dr[2] = dt.Rows[i - 1][2].ToString();//+ reg.Match(tstr).Success.ToString()
                                            dr[3] = dt.Rows[i - 1][3].ToString();
                                            dr[4] = dt.Rows[i - 1][4].ToString();
                                            dr[5] = dt.Rows[i - 1][5].ToString();
                                            errdt.Rows.InsertAt(dr, k);
                                            k++;
                                            dr = errdt.NewRow();
                                            dr[0] = dt.Rows[i][0].ToString(); //通过索引赋值
                                            dr[1] = dt.Rows[i][1].ToString();
                                            dr[2] = dt.Rows[i][2].ToString();//+ reg.Match(tstr).Success.ToString()
                                            dr[3] = dt.Rows[i][3].ToString();
                                            dr[4] = dt.Rows[i][4].ToString();
                                            dr[5] = dt.Rows[i][5].ToString();
                                            errdt.Rows.InsertAt(dr, k);
                                            k++;
                                            continue;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region 序号问题
                                        if (seq.IndexOf(".") != -1 && seq.Substring(0, seq.LastIndexOf(".")) != lastseq && dt.Select("F1='" + seq.Substring(0, seq.LastIndexOf(".")) + "'").Count() == 0)
                                        {
                                            //如果已经存在错误列表中  不在重复添加
                                            if (errdt.Select("Seq='" + dt.Rows[i - 1][0].ToString() + "'").Count() > 0)
                                            {
                                                continue;
                                            }
                                            //&& dt.Select("F1='" + seq.Substring(0, seq.LastIndexOf(".")) + "'").Count() > 0
                                            log.Info("seq::::" + seq.Substring(0, seq.LastIndexOf(".")) + ":::lastseq:::" + lastseq + ":::::" + dt.Select("F1='" + seq.Substring(0, seq.LastIndexOf(".")) + "'").Count().ToString() + "::::::" + "F1='" + seq.Substring(0, seq.LastIndexOf(".")) + "'");
                                            err = true;
                                            DataRow dr = errdt.NewRow();
                                            dr[0] = dt.Rows[i - 1][0].ToString() + "(上一条)"; //通过索引赋值
                                            dr[1] = dt.Rows[i - 1][1].ToString();
                                            dr[2] = dt.Rows[i - 1][2].ToString();//+ reg.Match(tstr).Success.ToString()
                                            dr[3] = dt.Rows[i - 1][3].ToString();
                                            dr[4] = dt.Rows[i - 1][4].ToString();
                                            dr[5] = dt.Rows[i - 1][5].ToString();
                                            errdt.Rows.InsertAt(dr, k);
                                            k++;
                                            dr = errdt.NewRow();
                                            dr[0] = dt.Rows[i][0].ToString(); //通过索引赋值
                                            dr[1] = dt.Rows[i][1].ToString();
                                            dr[2] = dt.Rows[i][2].ToString();//+ reg.Match(tstr).Success.ToString()
                                            dr[3] = dt.Rows[i][3].ToString();
                                            dr[4] = dt.Rows[i][4].ToString();
                                            dr[5] = dt.Rows[i][5].ToString();
                                            errdt.Rows.InsertAt(dr, k);
                                            k++;
                                            continue;
                                        }
                                        #endregion
                                        tstr = tstr.Substring(0, 6);
                                        #region 料号问题
                                        if (!reg.Match(tstr).Success)
                                        {
                                            err = true;
                                            DataRow dr = errdt.NewRow();
                                            dr[0] = dt.Rows[i][0].ToString(); //通过索引赋值
                                            dr[1] = dt.Rows[i][1].ToString();
                                            dr[2] = dt.Rows[i][2].ToString();//+ reg.Match(tstr).Success.ToString()
                                            dr[3] = dt.Rows[i][3].ToString();
                                            dr[4] = dt.Rows[i][4].ToString();
                                            dr[5] = dt.Rows[i][5].ToString();
                                            errdt.Rows.InsertAt(dr, k);
                                            k++;
                                            continue;
                                        }
                                        #endregion
                                    }
                                }
                                else
                                {
                                    err = true;
                                    DataRow dr = errdt.NewRow();
                                    dr[0] = dt.Rows[i][0].ToString(); //通过索引赋值
                                    dr[1] = dt.Rows[i][1].ToString();
                                    dr[2] = dt.Rows[i][2].ToString();//+ reg.Match(tstr).Success.ToString()
                                    dr[3] = dt.Rows[i][3].ToString();
                                    dr[4] = dt.Rows[i][4].ToString();
                                    dr[5] = dt.Rows[i][5].ToString();
                                    errdt.Rows.InsertAt(dr, k);
                                    k++;
                                    continue;
                                }
                                #endregion

                                #region 一物多码
                                sql = "select sn,ItemNo,ItemName,Spec,Material,SurfaceDeal from RLItems where ItemNo='" + dt.Rows[i][1].ToString() + "' and ( ItemName!='" + dt.Rows[i][2].ToString() + "' or Spec!='" + dt.Rows[i][3].ToString() + "' or Material!='" + dt.Rows[i][4].ToString() + "' or SurfaceDeal!='" + dt.Rows[i][5].ToString() + "')";
                                log.Info("sql 一物多码::" + sql);
                                DataTable dtwl = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                                if (dtwl != null && dtwl.Rows.Count > 0)
                                {
                                    err = true;
                                    DataRow dr = errwldt.NewRow();
                                    dr[0] = dt.Rows[i][0].ToString(); //通过索引赋值
                                    dr[1] = dt.Rows[i][1].ToString();
                                    dr[2] = dt.Rows[i][2].ToString();//+ reg.Match(tstr).Success.ToString()
                                    dr[3] = dt.Rows[i][3].ToString();
                                    dr[4] = dt.Rows[i][4].ToString();
                                    dr[5] = dt.Rows[i][5].ToString();
                                    dr[6] = "导入";
                                    errwldt.Rows.InsertAt(dr, k);
                                    k++;
                                    DataRow dr2 = errwldt.NewRow();
                                    dr2[0] = dtwl.Rows[0][0].ToString(); //通过索引赋值
                                    dr2[1] = dtwl.Rows[0][1].ToString();
                                    dr2[2] = dtwl.Rows[0][2].ToString();//+ reg.Match(tstr).Success.ToString()
                                    dr2[3] = dtwl.Rows[0][3].ToString();
                                    dr2[4] = dtwl.Rows[0][4].ToString();
                                    dr2[5] = dtwl.Rows[0][5].ToString();
                                    dr2[6] = "物料表";
                                    errwldt.Rows.InsertAt(dr2, k);
                                    k++;
                                }
                                else
                                {
                                    //物料不存在
                                    DataRow dr = errwldt.NewRow();
                                    dr[0] = dt.Rows[i][0].ToString(); //通过索引赋值
                                    dr[1] = dt.Rows[i][1].ToString();
                                    dr[2] = dt.Rows[i][2].ToString();//+ reg.Match(tstr).Success.ToString()
                                    dr[3] = dt.Rows[i][3].ToString();
                                    dr[4] = dt.Rows[i][4].ToString();
                                    dr[5] = dt.Rows[i][5].ToString();
                                    dr[6] = "物料不存在";
                                    errwldt.Rows.InsertAt(dr, k);
                                    k++;
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }

                    if (err)
                    {
                        Session["errdt"] = errdt;
                        Session["errwldt"] = errwldt;
                        //str = str.TrimEnd(new char[] { ',' });
                        //Alert.Show(str);
                        PageContext.RegisterStartupScript(Window1.GetShowReference("showerr.aspx", "有问题的物料", Unit.Parse("900"), Unit.Parse("800")));
                        return;
                    }
                    #endregion



                    string sqlbase = "", sqldtl = "", sclass = "", ProName = "", ProNo = "", Ver = "", ClientCode = "", BomDate = "", ClientProNo = "", FileNo = "",Color="";//产品名称、产品编号、版本、客户代号、日期、客户产品型号
                    ArrayList al = new ArrayList();
                    //获取表头  添加bombase
                    if (dt != null && dt.Rows.Count >= 4)
                    {
                        seq = "";
                        ProName = dt.Rows[2][2].ToString();
                        ProNo = dt.Rows[3][2].ToString();
                        Color = dt.Rows[3][4].ToString();
                        Ver = dt.Rows[3][7].ToString();
                        //ClientCode = dt.Rows[2][9].ToString();
                        BomDate = dt.Rows[3][10].ToString().Replace(".", "-");
                        //ClientProNo = dt.Rows[3][5].ToString();
                        FileNo = dt.Rows[2][7].ToString();
                        //料号，名称，规格，材质，表面处理或颜色，底数，类别
                        //ItemNo,Name,Spec,MaterialNo,ItemColor,AddReserve1,ClassName
                        sql = "select top 1 * from rlitems where itemno='" + ProNo + "' ";// or name='" + dt.Rows[2][2].ToString() + "' 
                        DataTable dtitem = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                        if (dtitem == null || dtitem.Rows.Count == 0)
                        {
                            sql = "insert into rlitems(itemno,itemname) values('" + ProNo + "','" + ProName + "')";
                            log.Info("sqlallitem::::" + sql);
                            al.Add(sql);
                            //产品名称、产品编号、版本、瑞麟编号、客户编号、客户代号、日期
                            sqlbase = "insert into ProBomHeader(ProName,ProNo,Ver,ClientProNo,ClientCode,BomDate,AllitemSN,Inputer,InputeDate,BomExcel,FileNo,Color) values('" + ProName + "','" + ProNo + "','" + Ver + "','" + ClientProNo + "','" + ClientCode + "','" + BomDate + "',(select  sn from rlitems where itemno='" + ProNo + "'),'" + User.Identity.Name + "',getdate(),'" + filename + "','" + FileNo + "','" + Color + "')";
                            log.Info("sqlbase::::" + sqlbase);
                            al.Add(sqlbase);
                            SQLHelper.DbHelperSQL.ExecuteSqlTran(al);

                        }
                        else
                        {
                            //产品名称、产品编号、版本、瑞麟编号、客户编号、客户代号、日期
                            sqlbase = "insert into ProBomHeader(ProName,ProNo,Ver,ClientProNo,ClientCode,BomDate,AllitemSN,Inputer,InputeDate,BomExcel,FileNo,Color) values('" + ProName + "','" + ProNo + "','" + Ver + "','" + ClientProNo + "','" + ClientCode + "','" + BomDate + "'," + dtitem.Rows[0]["sn"].ToString() + ",'" + User.Identity.Name + "',getdate(),'" + filename + "','" + FileNo + "','" + Color + "')";
                            log.Info("sqlbase::::" + sqlbase);
                            SQLHelper.DbHelperSQL.ExecuteSql(sqlbase, 30);
                        }
                        //查询新增bom的sn
                        sql = "select max(sn) from ProBomHeader where ver='" + Ver + "' and prono='" + ProNo + "'";
                        log.Info(sql);
                        SQLHelper.DbHelperSQL.SetConnectionString("");
                        bomsn = SQLHelper.DbHelperSQL.GetSingle(sql).ToString();
                        log.Info("bombasesn:::" + sql);
                        al.Clear();

                        #region bomdtl add
                        string proquantity = "0", basenum = "0";
                        for (i = 5; i < dt.Rows.Count; i++)
                        {
                            #region 判断数量和底数是否为空
                            proquantity = dt.Rows[i][6].ToString();
                            if (!string.IsNullOrEmpty(proquantity))
                            {
                                if (proquantity.IndexOf("/") != -1)
                                {
                                    proquantity = (float.Parse(proquantity.Substring(0, proquantity.IndexOf("/"))) / float.Parse(proquantity.Substring(proquantity.IndexOf("/") + 1))).ToString();
                                }
                            }
                            else
                            {
                                proquantity = "0";
                            }
                            basenum = dt.Rows[i][7].ToString();
                            if (string.IsNullOrEmpty(basenum))
                            {
                                basenum = "0";
                            }
                            #endregion
                            if (dt.Rows[i][0].ToString() == "锐 麟 铝 制 品 有 限 公 司" || dt.Rows[i][0].ToString() == "物料清单(BOM)" || dt.Rows[i][0].ToString() == "产品名称" || dt.Rows[i][0].ToString() == "锐麟编号" || dt.Rows[i][0].ToString() == "序号")
                            {
                                sclass = "";
                                continue;
                            }
                            else
                            {
                                #region

                                if (dt.Rows[i][0].ToString() == "" && dt.Rows[i][1].ToString() == "" && dt.Rows[i][2].ToString() == "" && dt.Rows[i][3].ToString() == "" && dt.Rows[i][4].ToString() == "" && dt.Rows[i][5].ToString() == "" && dt.Rows[i][6].ToString() == "" && dt.Rows[i][7].ToString() == "" && dt.Rows[i][8].ToString() == "" && dt.Rows[i][9].ToString() == "")
                                {
                                    break;
                                }
                                else if (dt.Rows[i][0].ToString() != "" && dt.Rows[i][1].ToString() == "" && dt.Rows[i][2].ToString() == "" && dt.Rows[i][3].ToString() == "" && dt.Rows[i][4].ToString() == "" && dt.Rows[i][5].ToString() == "" && dt.Rows[i][6].ToString() == "" && dt.Rows[i][7].ToString() == "" && dt.Rows[i][8].ToString() == "" && dt.Rows[i][9].ToString() == "")
                                {
                                    sclass = dt.Rows[i][0].ToString();
                                }
                                else
                                {
                                    #region  bomdtl add
                                    //料号，名称，规格，材质，表面处理或颜色，底数，类别
                                    //ItemNo,Name,Spec,MaterialNo,ItemColor,AddReserve1,ClassName
                                    sql = "select top 1 * from rlitems where itemno='" + dt.Rows[i][1].ToString() + "' ";//or name='" + dt.Rows[i][2].ToString() + "'  
                                    dtitem = SQLHelper.DbHelperSQL.ReturnDataTable(sql, 30);
                                    seq = dt.Rows[i][0].ToString();
                                    int num = Regex.Matches(seq, ".").Count;

                                    if (dtitem == null || dtitem.Rows.Count == 0)
                                    {
                                        #region 
                                        sql = "insert into rlitems(itemno,itemname,Spec,Material,SurfaceDeal,ProUsingQuantity,BaseNum,WorkShop,MainFrom,Sclass,StoreHouse,zongcheng) values('" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][5].ToString() + "'," + proquantity + "," + basenum + ",'" + dt.Rows[i][8].ToString() + "','" + dt.Rows[i][9].ToString() + "','" + dt.Rows[i][10].ToString() + "','" + dt.Rows[i][11].ToString() + "','" + sclass + "')";
                                        log.Info("sqlallitem::::" + sql);
                                        SQLHelper.DbHelperSQL.ExecuteSql(sql, 30);
                                        if (seq.IndexOf(".") != -1)
                                        {
                                            sqldtl = "insert into ProBomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity,ZongCheng,Inputer,InputeDate,seq,parentsn,ZuHe,BaseNum,WorkShop,MainFrom,Sclass,StoreHouse) values(" + bomsn + ",(select sn from rlitems where itemno='" + dt.Rows[i][1].ToString() + "'),'" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][5].ToString() + "'," + proquantity + ",'" + sclass + "','" + User.Identity.Name + "',getdate(),'" + dt.Rows[i][0].ToString() + "',(select sn from ProBomDetail where seq='" + seq.Substring(0, seq.LastIndexOf(".")) + "' and  fsn=" + bomsn + "),1," + basenum + ",'" + dt.Rows[i][8].ToString() + "','" + dt.Rows[i][9].ToString() + "','" + dt.Rows[i][10].ToString() + "','" + dt.Rows[i][11].ToString() + "')";
                                        }
                                        else
                                        {
                                            //bomsn,物料sn，料号，名称，规格，材质，表面处理，用量，分类
                                            sqldtl = "insert into ProBomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity,ZongCheng,Inputer,InputeDate,seq,ZuHe,BaseNum,WorkShop,MainFrom,Sclass,StoreHouse) values(" + bomsn + ",(select sn from rlitems where itemno='" + dt.Rows[i][1].ToString() + "'),'" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][5].ToString() + "'," + proquantity + ",'" + sclass + "','" + User.Identity.Name + "',getdate(),'" + dt.Rows[i][0].ToString() + "',0," + basenum + ",'" + dt.Rows[i][8].ToString() + "','" + dt.Rows[i][9].ToString() + "','" + dt.Rows[i][10].ToString() + "','" + dt.Rows[i][11].ToString() + "')";
                                        }
                                        #endregion
                                        log.Info("sqldtl::::" + sqldtl);
                                        al.Add(sqldtl);
                                        SQLHelper.DbHelperSQL.ExecuteSql(sqldtl, 30);
                                    }
                                    else
                                    {
                                        //sql = "update rlitems set (itemno,itemname,Spec,Material,SurfaceDeal,ProUsingQuantity,BaseNum,WorkShop,MainFrom,Sclass,StoreHouse,zongcheng) values('" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][5].ToString() + "'," + proquantity + "," + basenum + ",'" + dt.Rows[i][8].ToString() + "','" + dt.Rows[i][9].ToString() + "','" + dt.Rows[i][10].ToString() + "','" + dt.Rows[i][11].ToString() + "','" + sclass + "')";
                                        #region 
                                        if (seq.IndexOf(".") != -1)
                                        {
                                            //bomsn,物料sn，料号，名称，规格，材质，表面处理，用量，分类
                                            sqldtl = "insert into ProBomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity,ZongCheng,Inputer,InputeDate,seq,parentsn,ZuHe,BaseNum,WorkShop,MainFrom,Sclass,StoreHouse) values(" + bomsn + "," + dtitem.Rows[0]["sn"].ToString() + ",'" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][5].ToString() + "'," + proquantity + ",'" + sclass + "','" + User.Identity.Name + "',getdate(),'" + dt.Rows[i][0].ToString() + "',(select sn from ProBomDetail where seq='" + seq.Substring(0, seq.LastIndexOf(".")) + "' and fsn=" + bomsn + "),1,'" + basenum + "','" + dt.Rows[i][8].ToString() + "','" + dt.Rows[i][9].ToString() + "','" + dt.Rows[i][10].ToString() + "','" + dt.Rows[i][11].ToString() + "')";
                                        }
                                        else
                                        {
                                            //bomsn,物料sn，料号，名称，规格，材质，表面处理，用量，分类
                                            sqldtl = "insert into ProBomDetail(FSN,AllitemSN,ItemNo,ItemName,Spec,Material,SurfaceDeal,ProUsingQuantity,ZongCheng,Inputer,InputeDate,seq,ZuHe,BaseNum,WorkShop,MainFrom,Sclass,StoreHouse) values(" + bomsn + "," + dtitem.Rows[0]["sn"].ToString() + ",'" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][5].ToString() + "'," + proquantity + ",'" + sclass + "','" + User.Identity.Name + "',getdate(),'" + dt.Rows[i][0].ToString() + "',0," + basenum + ",'" + dt.Rows[i][8].ToString() + "','" + dt.Rows[i][9].ToString() + "','" + dt.Rows[i][10].ToString() + "','" + dt.Rows[i][11].ToString() + "')";
                                        }
                                        #endregion

                                        SQLHelper.DbHelperSQL.ExecuteSql(sqldtl, 30);
                                        log.Info("sqldtl::::" + sqldtl);
                                        sql = "update proBomDetail set subsn=SN where subsn is null";
                                        SQLHelper.DbHelperSQL.ExecuteSql(sqldtl, 30);
                                        al.Add(sqldtl);
                                    }
                                    #endregion

                                }
                                #endregion
                            }
                        }
                        //if (SQLHelper.DbHelperSQL.ExecuteSqlTran(al))
                        //{
                        Alert.Show("导入成功");
                        //}
                        //else
                        //{
                        //    Alert.Show("导入失败");
                        //}
                        //al.Clear();
                        #endregion
                    }
                    else
                    {
                        Alert.Show("NO Data");
                    }
                }


            }
            catch (Exception ee)
            {
                string sql = "delete proBomHeader where sn=" + bomsn;
                ArrayList al = new ArrayList();
                al.Add(sql);
                sql = "delete proBomDetail where fsn=" + bomsn;
                SQLHelper.DbHelperSQL.SetConnectionString("");
                SQLHelper.DbHelperSQL.ExecuteSqlTran(al);
                log.Info(ee.ToString());
                Alert.Show(ee.Message);
            }
            finally
            {
                BindGrid();
            }
        }


        #endregion
        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid();
        }


       
        #region  export excel
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=myexcel.xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Grid1.PageSize = 1000000;
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        

        private string GetGridTableHtml(Grid grid)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/>");

            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");

            sb.Append("<tr>");
            foreach (GridColumn column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", column.HeaderText);
            }
            sb.Append("</tr>");


            foreach (GridRow row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (object value in row.Values)
                {
                    string html = value.ToString();
                    if (html.StartsWith(Grid.TEMPLATE_PLACEHOLDER_PREFIX))
                    {
                        // 模板列
                        string templateID = html.Substring(Grid.TEMPLATE_PLACEHOLDER_PREFIX.Length);
                        Control templateCtrl = row.FindControl(templateID);
                        html = GetRenderedHtmlSource(templateCtrl);
                    }
                    else
                    {
                        // 处理CheckBox
                        if (html.Contains("f-grid-static-checkbox"))
                        {
                            if (html.Contains("uncheck"))
                            {
                                html = "×";
                            }
                            else
                            {
                                html = "√";
                            }
                        }

                        // 处理图片
                        if (html.Contains("<img"))
                        {
                            string prefix = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "");
                            html = html.Replace("src=\"", "src=\"" + prefix);
                        }
                    }

                    sb.AppendFormat("<td>{0}</td>", html);
                }
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
        private string GetRenderedHtmlSource(Control ctrl)
        {
            if (ctrl != null)
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        ctrl.RenderControl(htw);

                        return sw.ToString();
                    }
                }
            }
            return String.Empty;
        }

        #endregion

        protected void btndelBomHead_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("ProjectBOMDelete"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
            List<int> ids = GetSelectedDataKeyIDs(Grid1);

            // 执行数据库操作
            //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
            //DB.SaveChanges();
            if (ids.Count > 0)
            {
                using (var appdb = new AppContext())
                {
                    appdb.probomdtl.Where(u => ids.Contains(u.FSN)).Delete();
                    appdb.probombase.Where(u => ids.Contains(u.SN)).Delete();

                }
            }
            // 重新绑定表格
            BindGrid();
            
        }

        
    }
}