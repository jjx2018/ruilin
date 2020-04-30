<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="purchasesummary_C.aspx.cs" Inherits="AppBoxPro.stock.purchasesummary_C" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <f:Grid runat="server" ID="Grid1" IsDatabasePaging="false" SortField="" AllowPaging="true"  ShowHeader="false">
            <Toolbars>
                <f:Toolbar  runat="server">
                    <Items>
                        <f:DatePicker runat="server" ID="dpstart"></f:DatePicker>
                        <f:DatePicker runat="server" ID="dpend"></f:DatePicker>
                        <f:TextBox runat="server" ID="tbxsearch" EmptyText="请输入采购人/供应商"></f:TextBox>
                        <f:Button runat="server" Text="搜索" ID="btnsearch"  OnClick="btnsearch_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:BoundField HeaderText="采购单号" DataField="PurPlanNo" Width="200px"  ExpandUnusedSpace="false"></f:BoundField>
                <f:BoundField DataFormatString="{0:yyyy-MM-dd}" HeaderText="日期" DataField="PDate" Width="120px"></f:BoundField>
                <f:BoundField HeaderText="采购人" DataField="Purchaser" Width="100px"></f:BoundField>
                <f:BoundField HeaderText="供应商" DataField="Provider"></f:BoundField>
                <f:WindowField HeaderText="审核" TextAlign="Center" IconFont="ArrowCircleODown" ToolTip="审核"
                            WindowID="Window1" Title="入库审核" DataIFrameUrlFields="PurPlanNo" DataIFrameUrlFormatString="~/stock/instockpurchase_new_C.aspx?PurOrderNo={0}"
                            Width="60px" />

            </Columns>
        </f:Grid>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank"
            Width="900px" Height="600px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>
