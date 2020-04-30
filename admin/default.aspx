<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="AppBoxPro.admin._default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" Layout="Container" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            runat="server"  BodyPadding="5px">
            <Items>
<f:Panel ID="Panel5" Layout="Column" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            runat="server"  BodyPadding="0px">
            <Items>
                             <f:Panel ID="Panel2" Title="系统公告" Height="200px" ColumnWidth="33%" runat="server" CssStyle="margin-bottom:5px;"
                            Margin="0px 5px 0 0" ShowBorder="true" ShowHeader="true" EnableIFrame="true" IFrameUrl="~/pub/showNotice.aspx">
                            <Items>
                                 
                            </Items>
                        </f:Panel>
                <f:Panel ID="Panel4" Title="订单数" Height="200px" ColumnWidth="34%" runat="server" CssStyle="margin-bottom:5px;"
                            Margin="0px 5px 0 0" ShowBorder="true" ShowHeader="true" EnableIFrame="true" IFrameUrl="~/pub/showOrders.html">
                            <Items>
                                 
                            </Items>
                        </f:Panel>
                <f:Panel ID="Panel3" Title="待确认单数" Height="200px" ColumnWidth="33%" runat="server" CssStyle="margin-bottom:5px;"
                            BodyPadding="0px" ShowBorder="true" ShowHeader="true" EnableIFrame="true" IFrameUrl="~/pub/waitConfirm.html">
                            <Items>
                                 
                            </Items>
                        </f:Panel>
 
                 </Items>
    </f:Panel>
<f:Panel ID="Panel6" Layout="Column" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            runat="server"  BodyPadding="0px">
            <Items>
                             <f:Panel ID="Panel7" Title="生产计划" Height="250px" ColumnWidth="33%" runat="server" CssStyle="margin-bottom:5px;"
                            Margin="0px 5px 0 0" ShowBorder="true" ShowHeader="true" EnableIFrame="true" IFrameUrl="~/pub/ProducePlan.html">
                            <Items>
                                 
                            </Items>
                        </f:Panel>
                <f:Panel ID="Panel8" Title="采购计划" Height="250px" ColumnWidth="34%" runat="server" CssStyle="margin-bottom:5px;"
                            Margin="0px 5px 0 0" ShowBorder="true" ShowHeader="true" EnableIFrame="true" IFrameUrl="~/pub/PurchasePlan.html">
                            <Items>
                                 
                            </Items>
                        </f:Panel>
                <f:Panel ID="Panel9" Title="发外加工" Height="250px" ColumnWidth="33%" runat="server" CssStyle="margin-bottom:5px;"
                            BodyPadding="5px" ShowBorder="true" ShowHeader="true" EnableIFrame="true" IFrameUrl="~/pub/SendOut.html">
                            <Items>
                                 
                            </Items>
                        </f:Panel>
 
                 </Items>
    </f:Panel>
<f:Panel ID="Panel10" Layout="Column" ShowBorder="false" ShowHeader="false" AutoScroll="true"
            runat="server"  BodyPadding="0px">
            <Items>
                             <f:Panel ID="Panel11" Title="进仓" Height="200px" ColumnWidth="50%" runat="server" CssStyle="margin-bottom:5px;"
                            Margin="0px 5px 0 0" ShowBorder="true" ShowHeader="true" EnableIFrame="true" IFrameUrl="~/pub/Instock.html">
                            <Items>
                                 
                            </Items>
                        </f:Panel>
                <f:Panel ID="Panel12" Title="出仓" Height="200px" ColumnWidth="50%" runat="server" CssStyle="margin-bottom:5px;"
                            Margin="0px 5px 0 0" ShowBorder="true" ShowHeader="true" EnableIFrame="true" IFrameUrl="~/pub/Outstock.html">
                            <Items>
                                 
                            </Items>
                        </f:Panel>
                
 
                 </Items>
    </f:Panel>



            </Items>
        </f:Panel>
     </form>
</body>
</html>
