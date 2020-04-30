<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="purchaseinstocklistsale.aspx.cs" Inherits="AppBoxPro.stock.purchaseinstocklistsale" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
            EnableCheckBoxSelect="true"
            DataKeyNames="SN" AllowSorting="true" SortField="SN" OnSort="Grid1_Sort"
            SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowCommand="Grid1_RowCommand" OnPreDataBound="Grid1_PreDataBound"  AllowCellEditing="true">
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                        <f:SimpleForm ID="SF2" runat="server" Width="100%" ShowBorder="false" ShowHeader="false" LabelWidth="100px" Layout="HBox" BoxConfigChildMargin="0 5 0 5px">
                            <Items>
                                <f:TextBox runat="server" EmptyText="可输入型号、品名搜索" ID="tbxSearch" Width="250px"></f:TextBox>

                                <f:DatePicker ID="DateFrom" DateFormatString="yyyy-MM-dd" LabelWidth="80px" Width="200px" runat="server" Label="开始"></f:DatePicker>
                                <f:DatePicker ID="DateTo" DateFormatString="yyyy-MM-dd" LabelWidth="80px" Width="200px" runat="server" Label="结束" CompareControl="DateFrom" CompareOperator="GreaterThan" CompareMessage="结束日期应大于开始日期"></f:DatePicker>
                                <f:Button ID="btnSearch"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_Click"></f:Button>
                                <f:Button ID="btnReset" Icon="Cancel" EnablePostBack="false" runat="server"
                                    Text="清空">
                                </f:Button>
                            </Items>
                        </f:SimpleForm>
                    </Items>
                </f:Toolbar>


            </Toolbars>

            <Columns>
                <f:RowNumberField Width="30px" EnablePagingNumber="true" />
                <f:BoundField DataField="PurPlanNo" SortField="PurPlanNo" Width="130px" HeaderText="采购计划单号" Hidden="true" />
                <f:BoundField DataField="PurOrderNo" SortField="PurOrderNo" Width="150px" HeaderText="采购单号" />
                <f:BoundField DataField="SaleOrderNo" SortField="SaleOrderNo" Width="130px" HeaderText="销售单号" Hidden="true" />
                <f:BoundField DataField="ProNo" SortField="ProNo" Width="120px" HeaderText="品号" />
                <f:BoundField DataField="ProName" SortField="ProName" Width="120px" HeaderText="品名" />
                <f:BoundField DataField="ItemNo" SortField="ItemNo" Width="120px" HeaderText="料号" />
                <f:BoundField DataField="ItemName" SortField="ItemName" Width="150px" HeaderText="名称" />
                <f:BoundField DataField="Spec" SortField="Spec" ExpandUnusedSpace="true" MinWidth="150px" HeaderText="规格" />
                <f:BoundField DataField="Quantity" ColumnID="Quantity" SortField="Quantity" Width="90px" HeaderText="采购数量" />
                <f:RenderField DataField="InstockQuantity" ColumnID="InstockQuantity" HeaderText="入库数">
                </f:RenderField>
                <f:WindowField HeaderText="入库" Icon="ApplicationOsxAdd" WindowID="Window1" DataIFrameUrlFormatString="~/stock/purchaseinstocknew.aspx?SN={0}" DataIFrameUrlFields="SN"></f:WindowField>
                <f:BoundField DataField="Unit" Width="90px" HeaderText="单位" />
                <f:BoundField DataField="Remark" Width="90px" HeaderText="备注" />

            </Columns>
        </f:Grid>

        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank"
            Width="1000px" Height="500px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>
