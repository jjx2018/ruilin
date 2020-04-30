<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="instock.aspx.cs" Inherits="AppBoxPro.stock.instock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <f:Grid runat="server" ID="Grid1" IsDatabasePaging="false" DataKeyNames="SN" OnRowCommand="Grid1_RowCommand" SortDirection="DESC" SortField="PDate" AllowSorting="true" OnSort="Grid1_Sort">
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:DatePicker runat="server" ID="dpstart"></f:DatePicker>
                        <f:DatePicker runat="server" ID="dpend"></f:DatePicker>

                        <f:TwinTriggerBox ID="ttbxorderno" ShowLabel="false" OnTrigger1Click="ttbxorderno_Trigger1Click" OnTrigger2Click="ttbxorderno_Trigger2Click"
                    Trigger1Icon="Clear" ShowTrigger1="False" EmptyText="搜索单号" Trigger2Icon="Search"
                    runat="server">
                </f:TwinTriggerBox>

                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button runat="server" Text="新增" EnablePostBack="false" ID="btnNew" Icon="New"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:BoundField HeaderText="单号" DataField="StockOrderNo"  ExpandUnusedSpace="true"></f:BoundField>
                <f:BoundField HeaderText="入库日期" DataField="PDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" Width="200px" SortField="PDate"></f:BoundField>
                <f:BoundField HeaderText="所属仓库" DataField="Space" Hidden="true"></f:BoundField>
                <f:BoundField HeaderText="类型" DataField="Properties"></f:BoundField>
                <f:BoundField HeaderText="制单人" DataField="JinBanRen"></f:BoundField>
                <f:CheckBoxField HeaderText="是否作废" DataField="isCancel" Width="70px" Hidden="true"></f:CheckBoxField>
                <f:WindowField  TextAlign="Center" HeaderText="明细" IconFont="_More" ToolTip="明细"
                    WindowID="Window1" Title="入库记录" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/stock/instock_list.aspx?SN={0}"
                    Width="80px" />
                                <f:LinkButtonField HeaderText="删除" Icon="Delete" ConfirmText="是否删除选中记录" CommandName="Delete" TextAlign="Center"></f:LinkButtonField>
            </Columns>
        </f:Grid>

        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank"
            Width="1200px" Height="600px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>
