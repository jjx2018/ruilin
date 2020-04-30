<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="purchasecheck.aspx.cs" Inherits="AppBoxPro.stock.purchasecheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server" AutoSizePanelID="Panel5" EnableFStateValidation="false"></f:PageManager>

        <f:Panel CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            Layout="VBox" AutoScroll="true" Margin="0px" ShowHeader="false" Title=""
            BoxConfigChildMargin="0 0 0 0" BodyPadding="0">
            <Items>
                <f:Grid runat="server" ID="Grid1" IsDatabasePaging="true" DataKeyNames="SN" Title="采购入库品检" SortDirection="ASC" SortField="SN" EnableCheckBoxSelect="true" EnableMultiSelect="true">
                    <Toolbars>
                        <f:Toolbar runat="server">
                            <Items>
                                <f:Button runat="server" Text="通过选中记录" ID="btnPass" OnClick="btnPass_Click" ></f:Button>
                                <f:Button runat="server" Text="不通过选中记录" ID="btnNoPass" OnClick="btnNoPass_Click" ></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:BoundField DataField="PurOrderNo" SortField="PurOrderNo" HeaderText="采购单号"></f:BoundField>
                        <f:BoundField DataField="ProNo" SortField="ProNo" HeaderText="产品号"></f:BoundField>
                        <f:BoundField DataField="ItemNo" SortField="ItemNo" HeaderText="料号"></f:BoundField>
                        <f:BoundField DataField="ItemName" SortField="ItemName" HeaderText="品名"></f:BoundField>
                        <f:BoundField DataField="Spec" SortField="Spec" HeaderText="规格"></f:BoundField>
                        <f:BoundField DataField="Quantity" SortField="Quantity" HeaderText="数量"></f:BoundField>
                        <f:BoundField DataField="Unit" SortField="Unit" HeaderText="单位"></f:BoundField>
                        <f:BoundField DataField="PDate" SortField="PDate" HeaderText="入库日期" DataFormatString="{0:yyyy-MM-dd}"></f:BoundField>
                        <f:BoundField DataField="Space" SortField="Space" HeaderText="库位"></f:BoundField>
                        <f:BoundField DataField="result" SortField="result" HeaderText="品检结果"></f:BoundField>
                        <f:BoundField DataField="checkDate" SortField="checkDate" HeaderText="品检日期"></f:BoundField>
                    </Columns>
                </f:Grid>
            </Items>

        </f:Panel>


    </form>
</body>
</html>
