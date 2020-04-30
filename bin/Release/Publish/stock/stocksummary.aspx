<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stocksummary.aspx.cs" Inherits="AppBoxPro.stock.stocksummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <f:Panel runat="server" ShowBorder="false" ShowHeader="false" IsFluid="true" >
            <Items>
                <f:Toolbar runat="server">
                    <Items>
                          <f:DatePicker runat="server" ID="dpStart" Label="开始日期" Width="210px" LabelAlign="Right"></f:DatePicker>
                                <f:DatePicker runat="server" ID="dpEnd" Label="结束日期" Width="210px" LabelAlign="Right"></f:DatePicker>
                        <f:TextBox runat="server" ID="tbxSearch" EmptyText="请输入料号/名称/规格"></f:TextBox>
                        <f:Button runat="server" Text="搜索" Icon="SystemSearch" ID="btnsearch" OnClick="btnsearch_Click"></f:Button>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button runat="server" Text="导出"></f:Button>
                    </Items>
                </f:Toolbar>
                <f:Grid runat="server" ID="Grid1" SummaryPosition="Bottom" EnableSummary="true" EnableTextSelection="true" SortField="ItemNo" Height="470px" ShowHeader="false">
                    <Columns>
                        <f:RowNumberField TextAlign="Center"></f:RowNumberField>
                        <f:BoundField DataField="ItemNo" HeaderText="料号" ExpandUnusedSpace="true" SortField="ItemNo"></f:BoundField>
                        <f:BoundField DataField="ItemName" HeaderText="名称" ExpandUnusedSpace="true" SortField="ItemName"></f:BoundField>
                        <f:BoundField DataField="Spec" HeaderText="规格" SortField="Spec"></f:BoundField>
                        <f:BoundField DataField="Space" HeaderText="库位" SortField="Space"></f:BoundField>
                        <f:GroupField HeaderText="期初" HeaderTextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="shangyuekucun" HeaderText="数量" HeaderTextAlign="Center" TextAlign="Center" ColumnID="s1"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="入库" HeaderTextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="benyuejincang" HeaderText="数量" HeaderTextAlign="Center" TextAlign="Center" ColumnID="s2"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="出库" HeaderTextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="benyuechucang" HeaderText="数量" HeaderTextAlign="Center" TextAlign="Center" ColumnID="s3"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="期末" HeaderTextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="qimokucun" HeaderText="数量" HeaderTextAlign="Center" TextAlign="Center" ColumnID="s4"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>

    </form>
</body>
</html>
