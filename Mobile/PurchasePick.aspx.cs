﻿using AppBoxPro.mobile;
using EntityFramework.Extensions;
using FineUIPro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.Mobile
{
    public partial class PurchasePick : PageBaseMobile
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
                }
            }
        }

        private void BindForm()
        {
            string status = GetQueryValue("status");
            if (tbxbarcode.Text == "")
            {
                return;
            }

            var q = from a in MYDB.purchaseorderDetail
                    where a.BarCode == tbxbarcode.Text
                    select a;


            if (q.Count() > 0)
            {
                var res = q.FirstOrDefault();

                if (status == "0" && res.Status != null)
                {
                    ShowNotify("条码错误");
                    return;
                }

                tbxprosn.Text = res.PurOrderNo;
                tbxitemno.Text = res.ItemNo;
                tbxprono.Text = res.ProNo;
                tbxitemname.Text = res.ItemName;
                tbxspec.Text = res.Spec;
                nbPurQut.Text = res.Quantity.ToString();
                nbQut.Text = res.Quantity.ToString();
                tbxunit.Text = res.Unit;
                tbxsaleno.Text = res.SaleOrderNo;
            }
            else
            {
                ShowNotify("查无此物料");
                form2.Reset();
                return;
            }
        }

        private void LoadData()
        {
            //库位
            var qq = from a in MYDB.storehouse select a;

            ddlstorehouse.DataSource = qq;
            ddlstorehouse.DataTextField = "StoreName";
            ddlstorehouse.DataValueField = "StoreName";
            ddlstorehouse.DataBind();


            //岗位
            string status = GetQueryValue("status");
            Toolbar1.Title = "采购收货";


        }

        protected void tbxprosn_Blur(object sender, EventArgs e)
        {
            BindForm();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();

            if (newAddedList.Count == 0)
            {
                ShowNotify("没有要提交的数据");
                return;
            }


            String status = GetQueryValue("status");

            for (int i = 0; i < newAddedList.Count; i++)
            {
                string barcode = newAddedList[i]["barcode"].ToString();
                MYDB.purchaseorderDetail.Where(u => u.BarCode == barcode).Update(u => new PurchaseOrderDetail
                {
                    Status = int.Parse(status)
                });
            }


            MYDB.SaveChanges();

            ShowNotify("提交成功");

            //清除页面数据
            PageContext.RegisterStartupScript("clearLocalStorage()");

        }

    }
}