using AppBoxPro.mobile;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.Mobile
{
    public partial class maginstruction : PageBaseMobile
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
            else
            {
                string arg = GetRequestEventArgument();
                if (arg == "tbxprosn__change")
                {
                    BindForm();
                    //BindGrid1();
                }
            }
        }

        private void BindForm()
        {
            IQueryable<Instruction> q = MYDB.instruction;

            if (tbxProsn.Text != "")
            {
                q = q.Where(u => u.BarCode == tbxProsn.Text&&u.IsConfirm==0);

                if (q.Count() > 0)
                {
                    var res = q.FirstOrDefault();
                    tbxProsn.Text = res.BarCode;
                    tbxItemname.Text = res.ItemName;
                    tbxItemno.Text = res.ItemNo;
                    tbxSpec.Text = res.Spec;
                    tbxBeihuo.Text = res.UsingQuantity.ToString();
                }
                else
                {
                    ShowNotify("查无此备货条码");
                }




            }
        }

        private void LoadData()
        {

        }

        private void BindGrid1()
        {
            //IQueryable<Instruction> q = MYDB.instruction;

            ////在接收人名称中搜索

            ////搜索状态为 未确认的

            //string username = GetIdentityName();

            ////q = q.Where(u => u.Receiver == username && u.IsConfirm == 0);

            //q = q.Where(u => u.IsConfirm == 0);

            //if (tbxProsn.Text != "")
            //{
            //    q = q.Where(u => u.BarCode == tbxProsn.Text);
            //}

            ////if (tbxOrderNo.Text != "")
            ////{
            ////    q = q.Where(u => u.OrderNo == tbxOrderNo.Text);
            ////}

            ////if (tbxitemno.Text != "")
            ////{
            ////    q = q.Where(u => u.ItemNo == tbxitemno.Text);
            ////}

            //if (q.Count() < 1)
            //{
            //    ShowNotify("查无此物料");
            //    return;
            //}

            //Grid1.DataSource = q;
            //Grid1.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //if (Grid1.SelectedRowIndex == -1)
            //{
            //    ShowNotify("请选择要确认的物料");
            //    return;
            //}

            //int SN = GetSelectedDataKeyID(Grid1);

            double confirmqut = double.Parse(nbConfirmQuantity.Text);

            //执行存储过程，返回流水号
            //Type t = typeof(string);
            //SqlParameter[] sqlParms = new SqlParameter[1];
            //sqlParms[0] = new SqlParameter("@MaintainCate", "inst");
            //var result = DB.Database.SqlQuery(t, "exec GetSeq @MaintainCate", sqlParms).Cast<string>().First();

            MYDB.instruction.Where(u => u.BarCode == tbxProsn.Text).Update(u => new Instruction
            {
                IsConfirm = 1,
                ConfirmDate = DateTime.Now,
                ConfirmQuantity = confirmqut,
                RealUsingQuantity = u.UsingQuantity - confirmqut,
                //BarCode = result.ToString()
            });

            ShowNotify("提交成功");
            nbConfirmQuantity.Text = "";
            form2.Reset();
            //BindGrid1();

        }

        protected void tbxProsn_Blur(object sender, EventArgs e)
        {
            BindForm();
        }
    }
}