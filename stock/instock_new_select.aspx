<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="instock_new_select.aspx.cs" Inherits="AppBoxPro.stock.instock_new_select" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        
        <f:Grid runat="server" ID="Grid1" ShowHeader="false" EnableMultiSelect="false" EnableCheckBoxSelect="true" Height="300px" IsFluid="true">
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:Button ID="btnClose" EnablePostBack="false" Text="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                        <f:Button ID="btnSaveClose" Text="选择后关闭" runat="server" Icon="SystemSaveClose" EnablePostBack="false">
                            <Listeners>
                                <f:Listener Event="click" Handler="onGridRowSelect" />
                            </Listeners>
                        </f:Button>
                        <f:TextBox runat="server" EmptyText="请输入料号、品名、规格" ID="tbxName"></f:TextBox>
                   <f:Button runat="server" Text="查询" ID="btnSearch" OnClick="btnSearch_Click"></f:Button>
                        </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:RenderField HeaderText="料号" DataField="ItemNo" ColumnID="ItemNo"></f:RenderField>
                 <f:RenderField HeaderText="品名" DataField="ItemName" ColumnID="ItemName" ExpandUnusedSpace="true"></f:RenderField>
                 <f:RenderField HeaderText="规格" DataField="Spec" ColumnID="Spec"></f:RenderField>
            </Columns>
        </f:Grid>
    </form>

    <script>

        var gridClientID = '<%= Grid1.ClientID %>';

        function onGridRowSelect() {
            // 返回当前活动Window对象（浏览器窗口对象通过F.getActiveWindow().window获取）
            var activeWindow = F.getActiveWindow();

            // 选中行数据
            var rowData = F(gridClientID).getSelectedRow(true);
            var rowValue = rowData.values;

            var queryRowId = F.queryString('rowid');
            var selectedValues = {
                'ItemNo': rowValue['ItemNo'],
                'ItemName': rowValue['ItemName'],
                'Spec': rowValue['Spec'],
            };

            // 隐藏弹出窗体
            activeWindow.hide();

            // 调用父页面的 updateGridRow 函数
            activeWindow.window.updateGridRow(queryRowId, selectedValues);
        }

    </script>
</body>
</html>
