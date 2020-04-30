<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="weiwaioutstockdetail.aspx.cs" Inherits="AppBoxPro.stock.weiwaioutstockdetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>

        <f:Panel runat="server" ShowBorder="false" ShowHeader="false" AutoScroll="true" IsFluid="true">
            <Items>
                <f:Grid runat="server" IsFluid="true" ShowBorder="false" ShowHeader="false" Height="600px" IsDatabasePaging="true" AllowSorting="true" OnSort="Grid1_Sort" ID="Grid1" SortDirection="ASC" SortField="SN" AllowPaging="true" OnPageIndexChange="Grid1_PageIndexChange" EnableSummary="true" SummaryPosition="Bottom" EnableRowDoubleClickEvent="true"  OnRowDoubleClick="Grid1_RowDoubleClick">
                    <Toolbars>
                        <f:Toolbar runat="server">
                            <Items>
                                <f:TextBox runat="server" EmptyText="关键词" ID="tbxsearch"></f:TextBox>
                                <f:DatePicker runat="server" ID="dpStart" Label="起始时间"></f:DatePicker>
                                <f:DatePicker runat="server" ID="dpEnd" ></f:DatePicker>
                                <f:Button runat="server" Text="查询" Icon="SystemSearch" ID="btnsearch" OnClick="btnsearch_Click"></f:Button>
                                <f:Button ID="btnBack"  Text="后退" runat="server" Icon="PageBack" OnClick="btnBack_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:RowNumberField></f:RowNumberField>
                        <f:BoundField HeaderText="料号" DataField="ItemNo" SortField="ItemNo" ColumnID="ItemNo"></f:BoundField>
                        <f:BoundField HeaderText="名称" DataField="ItemName" SortField="ItemName" ColumnID="ItemName"></f:BoundField>
                        <f:BoundField HeaderText="规格" DataField="Spec" SortField="Spec" ColumnID="Spec"></f:BoundField>
                        <f:BoundField HeaderText="委外数量" DataField="purQut" ColumnID="purQut" SortField="purQut"></f:BoundField>

                        <f:BoundField HeaderText="已出库数量" DataField="stockOutQut" ColumnID="stockOutQut" SortField="stockOutQut"></f:BoundField>
                        <f:BoundField HeaderText="未出库数量" DataField="resStockOutQut" ColumnID="resStockOutQut" SortField="resStockOutQut"></f:BoundField>

                        <f:BoundField HeaderText="已入库数量" DataField="stockInQut" ColumnID="stockInQut" SortField="stockInQut" Hidden="true"></f:BoundField>
                        <f:BoundField HeaderText="未入库数量" DataField="resStockInQut" ColumnID="resStockInQut" SortField="resStockInQut" Hidden="true"></f:BoundField>
                        <f:BoundField HeaderText="出入库日期" DataField="PDate" DataFormatString="{0:yyyy-MM-dd}" SortField="PDate"></f:BoundField>
                        <f:BoundField HeaderText="计划开工日期" DataField="InputeDate" DataFormatString="{0:yyyy-MM-dd}" SortField="InputeDate"></f:BoundField>
                        <f:BoundField HeaderText="计划完成日期" DataField="PlanFinishDate" DataFormatString="{0:yyyy-MM-dd}" SortField="PlanFinishDate"></f:BoundField>
                        
                        <f:BoundField HeaderText="订单号" DataField="SaleOrderNo" SortField="SaleOrderNo" ColumnID="SaleOrderNo"></f:BoundField>
                        <f:BoundField HeaderText="委外单号" DataField="SendOutOrderNo" SortField="SendOutOrderNo" ColumnID="SendOutOrderNo"></f:BoundField>
                        <f:BoundField HeaderText="材质" DataField="Material" SortField="Material" ColumnID="Material"></f:BoundField>
                        <f:BoundField HeaderText="供应商" DataField="Provider" SortField="Provider" ColumnID="Provider"></f:BoundField>
                        <f:BoundField HeaderText="表面处理" DataField="SurfaceDeal" SortField="SurfaceDeal" ColumnID="SurfaceDeal"></f:BoundField>
                        <f:BoundField HeaderText="类别" DataField="Sclass" SortField="Sclass" ColumnID="Sclass"></f:BoundField>
                        <f:BoundField HeaderText="车间" DataField="WorkShop" SortField="WorkShop" ColumnID="WorkShop"></f:BoundField>
                        <f:BoundField HeaderText="仓库" DataField="Space" SortField="Space" ColumnID="Space"></f:BoundField>
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
