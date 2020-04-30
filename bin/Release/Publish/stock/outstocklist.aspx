<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="outstocklist.aspx.cs" Inherits="AppBoxPro.stock.outstocklist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <f:Panel runat="server" ShowBorder="false" ShowHeader="false" AutoScroll="true">
            <Items>
                <f:Grid runat="server" ID="Grid1" IsDatabasePaging="false">
                    <Columns>
                        <f:BoundField DataField="StockHeaderProsn" HeaderText="领料单号"></f:BoundField>
                        <f:BoundField DataField="ItemNo" HeaderText="料号"></f:BoundField>
                        <f:BoundField DataField="ItemName" HeaderText="品名"></f:BoundField>
                        <f:BoundField DataField="Spec" HeaderText="规格"></f:BoundField>
                        <f:BoundField DataField="Quantity" HeaderText="数量"></f:BoundField>
                        <f:BoundField DataField="Unit" HeaderText="单位"></f:BoundField>
                        <f:BoundField DataField="PDate" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}"></f:BoundField>
                        <f:BoundField DataField="Space" HeaderText="仓位"></f:BoundField>
                        
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
