<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qitaoboard.aspx.cs" Inherits="AppBoxPro.board.qitaoboard" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="refresh" content="500" />
    <title></title>
    <style>
        .f-grid-row.colorred,
        .f-grid-row.colorred .f-icon,
        .f-grid-row.colorred a {
            background-color: red;
            color: #fff;
        }

        .f-grid-row.coloryellow,
        .f-grid-row.coloryellow .f-icon,
        .f-grid-row.coloryellow a {
            background-color: yellow;
            color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Grid1" EnableFStateValidation="false" runat="server" />
        <f:Timer runat="server" Interval="5" ID="timer1" OnTick="timer1_Tick" Enabled="true"></f:Timer>
        <f:Grid ID="Grid1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="true" Title="齐套看板" EnableCollapse="false"
            runat="server" DataKeyNames="SN" AllowCellEditing="true" ClicksToEdit="1" OnPreDataBound="Grid1_PreDataBound" EnableTextSelection="true"
            DataIDField="SN" EnableCheckBoxSelect="false" KeepCurrentSelection="false" OnPageIndexChange="Grid1_PageIndexChange" AllowSorting="true"  SortField="SN" EnterNavigate="true" TabVerticalNavigate="false" EnterVerticalNavigate="true"
            SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowDataBound="Grid1_RowDataBound"  PageSize="20">
            <Toolbars>
            </Toolbars>
            <PageItems>
                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                </f:ToolbarSeparator>
                <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                </f:ToolbarText>

            </PageItems>

            <Columns>
                <f:RowNumberField Width="30px" />
                <f:BoundField DataField="OrderNo" SortField="OrderNo"  Width="140px" HeaderText="订单编号" />
                        <f:BoundField DataField="ItemNo" SortField="ItemNo"  Width="130px" HeaderText="产品编号" />
                        <f:BoundField DataField="ItemName" SortField="ItemName"  Width="150px" HeaderText="产品名称" />
                        <f:BoundField DataField="Quantity"  SortField="Quantity"  Width="60px" HeaderText="数量" />
                        <f:BoundField DataField="RecOrderDate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="下单日期" />
                <f:BoundField DataField="ZhuangPeiDate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="计划组装日期" />
                        <f:BoundField DataField="OutGoodsDate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="交货日期" />
                        <f:BoundField DataField="InputeDate" DataFormatString="{0:yyyy-MM-dd}" Width="130px" HeaderText="BOM导入日期" />
                        <f:BoundField DataField="plandate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="最晚计划日期" />
                        <f:BoundField DataField="pdate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="最晚入库日期" />
                        
                        <f:BoundField DataField="qtl"  SortField="qtl"  Hidden="true" Width="200px" HeaderText="物料齐套率" />
                        <f:RenderField DataField="qtl" SortField="qtl" RendererFunction="retval"  Width="200px" HeaderText="物料齐套率" ></f:RenderField>
            </Columns>
        </f:Grid>

    </form>
    <script>
        function UsingQuantity(value) {
            return value;
        }

        function retval(value) {
            if (value) {
                value = parseFloat(value);
                if (value >= 100) {
                    return parseInt(value) + '%';
                }
                else {
                    return value + '%';
                }
            }
        }

    </script>
</body>
</html>
