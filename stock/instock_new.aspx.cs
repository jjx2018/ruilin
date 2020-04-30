using FineUIPro;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppBoxPro.stock
{
    public partial class instock_new : PageBase
    {
        private bool AppendToEnd = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();

                dpoptdate.SelectedDate = DateTime.Now;

                tbxorderno.Text ="IN-"+DateTime.Now.ToString("yyyyMMddHHmmssffff");

                // 新增数据初始值
                JObject defaultObj = new JObject();
                defaultObj.Add("ItemNo", "");
                defaultObj.Add("ItemName", "");
                defaultObj.Add("Spec", "");
                defaultObj.Add("Unit", "");
                defaultObj.Add("Quantity", "");
                defaultObj.Add("remark", "");

                PageContext.RegisterStartupScript( Grid1.GetAddNewRecordReference(defaultObj));

                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);

                // 重置表格
                btnReset.OnClientClick = Confirm.GetShowReference("确定要重置表格数据？", String.Empty, Grid1.GetRejectChangesReference(), String.Empty);


                // 删除选中行按钮
                btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;

                tbxjingbanren.Text = GetChineseName();
            }
        }


        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        }

        protected void tbxEditorItemNo_TriggerClick(object sender, EventArgs e)
        {
            string[] selectedCell = Grid1.SelectedCell;
            if (selectedCell != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference("instock_new_select.aspx?rowid=" + selectedCell[0]));
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
           List<Dictionary<string,object>> newAddedList=  Grid1.GetNewAddedList();

            //if (newAddedList.Count == 0)
            //{
            //    Alert.Show("没有新增数据");
            //    return;
            //}

            //Stock stock = new Stock();
            //stock.isCancel = false;
            //stock.StockOrderNo = tbxorderno.Text;
            //stock.JinBanRen = tbxjingbanren.Text;
            //stock.PDate = GetDatePickerSelectedTime(dpoptdate);
            //stock.Properties = ddlPro.SelectedValue;
            //stock.remark = tbxremark.Text;

            //MYDB.stock.Add(stock);

            //int count = 0;

            //if (AppendToEnd)
            //{
            //    for (int i = 0; i < newAddedList.Count; i++)
            //    {
            //        if(string.IsNullOrEmpty(newAddedList[i]["ItemNo"].ToString()))
            //        {
            //            continue;
            //        }
            //        StockList item = new StockList();
            //        item.Mark = "02";
            //        item.ItemNo = newAddedList[i]["ItemNo"].ToString();
            //        item.StockOrderNo = tbxorderno.Text;
            //        item.ItemName= newAddedList[i]["ItemName"].ToString();
            //        item.Spec = newAddedList[i]["Spec"].ToString();
                    
            //        item.Quantity=decimal.Parse( newAddedList[i]["Quantity"].ToString());
            //        item.PDate = GetDatePickerSelectedTime(dpoptdate);
            //        item.Remark= newAddedList[i]["remark"].ToString();
            //        item.Space = newAddedList[i]["Space"].ToString();
            //        item.Stock = stock;
            //        count++;
            //        MYDB.PurchaseStockLists.Add(item);
            //    }
            //}

            //if (count == 0)
            //{
            //    Alert.Show("数据信息填写不完整");
            //    return;
            //}

            MYDB.SaveChanges();

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}