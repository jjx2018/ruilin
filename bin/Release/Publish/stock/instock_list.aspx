<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="instock_list.aspx.cs" Inherits="AppBoxPro.stock.instock_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server" ShowAjaxLoadingMaskText="false" AjaxLoadingType="Mask"></f:PageManager>
        <f:Panel runat="server" ShowBorder="false" ShowHeader="false" BodyPadding="5px 5px 5px 5px">
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:Button runat="server" Text="作废" ConfirmText="是否作废此单?" ID="btnCanel" OnClick="btnCanel_Click" Hidden="true"></f:Button>
                        <f:Button runat="server" Text="打印" ID="btnprint" OnClick="btnprint_Click"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:ContentPanel runat="server" ShowBorder="false" ShowHeader="false">
                    <h2 style="text-align:center">入库单</h2>
                </f:ContentPanel>

                <f:Form runat="server" ShowBorder="false" ShowHeader="false" LabelAlign="Left" Title="入库单" TitleAlign="Center">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:Label runat="server" Label="单号" ID="lbOrderno" LabelWidth="50px"></f:Label>
                                <f:Label runat="server" Label="入库日期" ID="lbDate" LabelWidth="80px"></f:Label>
                                <f:Label runat="server" Label="经办人" ID="lbJinban" LabelWidth="70px"></f:Label>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid runat="server" ID="Grid1" ShowHeader="false" SummaryPosition="Bottom" EnableSummary="true">
                    <Columns>
                        <f:RowNumberField></f:RowNumberField>
                        <f:RenderField HeaderText="料号" ColumnID="ItemNo" DataField="ItemNo" ExpandUnusedSpace="true">
                        </f:RenderField>
                        <f:RenderField HeaderText="名称" ColumnID="ItemName" DataField="ItemName" ExpandUnusedSpace="true"></f:RenderField>
                        <f:RenderField HeaderText="规格" ColumnID="Spec" DataField="Spec" ExpandUnusedSpace="true"></f:RenderField>
                        <f:RenderField HeaderText="入库数量" ColumnID="Quantity" DataField="Quantity">
                        </f:RenderField>
                        <f:RenderField HeaderText="库位" ColumnID="Space" DataField="Space">
                        </f:RenderField>
                        <f:RenderField HeaderText="备注" ColumnID="remark" DataField="remark">
                        </f:RenderField>
                    </Columns>
                </f:Grid>
                <f:Form runat="server" ShowBorder="false" ShowHeader="false" IsFluid="true" LabelAlign="Left">
                    <Rows>
                        <f:FormRow>
                            <Items>
                                <f:Label runat="server" ID="lbremark" Label="备注"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow>
                            <Items>
                                <f:Label runat="server" ID="lbpurorderno" Label="关联采购单号" Hidden="true" LabelWidth="110px"></f:Label>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        <script>
            var btnPrint = '<%= btnprint.ClientID%>';

            function preview() {
                window.print();
            }
        </script>
    </form>
</body>
</html>
