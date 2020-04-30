<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductRec_c.aspx.cs" Inherits="AppBoxPro.product.ProductRec_c" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel5" EnableFStateValidation="false" runat="server" />
        <f:Panel ID="Panel5" CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            Layout="VBox" AutoScroll="true" Margin="0px" ShowHeader="false" Title="1"
            BoxConfigChildMargin="0 0 0 0" BodyPadding="0">
            <Items>
                <f:Panel ID="Panel3" Title="面板1" Height="290px" runat="server"
                    BodyPadding="0px" ShowBorder="false" ShowHeader="false" Layout="VBox">
                    <Items>
                        <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                            EnableCheckBoxSelect="true" EnableMultiSelect="false"
                            DataKeyNames="SN,ProOrderNo" AllowSorting="true" OnSort="Grid1_Sort" SortField="SN"
                            SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
                            EnableTextSelection="true" KeepCurrentSelection="false"
                            OnPageIndexChange="Grid1_PageIndexChange" EnableRowClickEvent="true" OnRowClick="Grid1_RowClick" OnRowCommand="Grid1_RowCommand">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" runat="server">
                                    <Items>

                                        <f:SimpleForm ID="SF1" runat="server" Width="100%" ShowBorder="false" ShowHeader="false" LabelWidth="100px" Layout="HBox" BoxConfigChildMargin="0 5 0 5px">
                                            <Items>

                                                <f:DatePicker ID="datePickerFrom" DateFormatString="yyyy-MM-dd" LabelWidth="80px" Width="200px" runat="server" Label="开始日期"></f:DatePicker>
                                                <f:DatePicker ID="datePickerTo" DateFormatString="yyyy-MM-dd" LabelWidth="80px" Width="200px" runat="server" Label="结束日期" CompareControl="datePickerFrom" CompareOperator="GreaterThan" CompareMessage="结束日期应大于开始日期"></f:DatePicker>
                                                <f:Button ID="btnSearch" Pressed="true" runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_Click"></f:Button>

                                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                                </f:ToolbarFill>
                                            </Items>
                                        </f:SimpleForm>
                                    </Items>
                                </f:Toolbar>


                            </Toolbars>
                            <Columns>
                                <f:RowNumberField Width="30px" EnablePagingNumber="true" />
                                <f:BoundField DataField="ProPlanNo" SortField="ProPlanNo" Width="150px" HeaderText="生产计划单号" />
                                <f:BoundField DataField="ProOrderNo" SortField="ProOrderNo" Width="150px" HeaderText="生产单号" />
                                <f:BoundField DataField="SaleOrderNo" SortField="SaleOrderNo" Width="150px" HeaderText="销售单号" />
                                <f:BoundField DataField="ProNo" SortField="ProNo" Width="150px" HeaderText="品号" />
                                <f:BoundField DataField="ProName" SortField="ProName" Width="200px" HeaderText="品名" />
                                <f:BoundField DataField="Quantity" SortField="Quantity" Width="90px" HeaderText="计划数量" />
                                <f:BoundField DataField="WorkShop" Width="150px" HeaderText="生产车间" />

                            </Columns>
                        </f:Grid>



                    </Items>
                </f:Panel>
                <f:Panel ID="Panel4" Title="产品生产记录明细" BoxFlex="1" MinHeight="270px" Margin="0"
                    runat="server" BodyPadding="1px 0px 0px 0px" ShowBorder="false" ShowHeader="true" Layout="VBox">
                    <Items>
                        <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                            EnableCheckBoxSelect="true"
                            DataKeyNames="SN" AllowSorting="true" SortField="Role" OnSort="Grid2_Sort"
                            SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" EnableRowDoubleClickEvent="true" OnRowCommand="Grid2_RowCommand" AllowCellEditing="true" ClicksToEdit="1">
                            <Toolbars>
                                <f:Toolbar runat="server">
                                    <Items>
                                        <f:ButtonGroup runat="server">
                                            <f:Button runat="server" Text="审核/保存" ID="btnpass" OnClick="btnpass_Click" ConfirmText="是否保存品检记录"></f:Button>
                                            <f:Button runat="server" Text="反审核" ID="btnnopass" OnClick="btnnopass_Click" ConfirmText="是否不通过选中记录" Hidden="true"></f:Button>
                                        </f:ButtonGroup>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                            <Columns>
                                <f:RowNumberField Width="30px" />
                                <f:BoundField DataField="Role" HeaderText="工序" SortField="Role"></f:BoundField>
                                <f:BoundField DataField="Quantity" SortField="Quantity" Width="90px" HeaderText="生产数量" ColumnID="Quantity" />
                                <f:RenderField DataField="choujianqut" SortField="choujianqut" Width="90px" HeaderText="抽检数量(可编辑)" ColumnID="choujianqut">
                                    <Editor>
                                        <f:NumberBox runat="server" NoDecimal="true" NoNegative="true"></f:NumberBox>
                                    </Editor>
                                </f:RenderField>

                                <f:RenderField DataField="result" SortField="result" Width="90px" HeaderText="抽检结果(可编辑)" ColumnID="result" RendererFunction="renderResult">
                                    <Editor>
                                        <f:DropDownList runat="server" ID="ddlResult">
                                            <f:ListItem Text="合格" Value="0"/>
                                            <f:ListItem Text="不合格" Value="1" />
                                            <f:ListItem Text="特采" Value="2" />
                                        </f:DropDownList>
                                    </Editor>
                                </f:RenderField>

                                <f:BoundField DataField="optdate" HeaderText="生产日期" Width="150px"></f:BoundField>
                                <f:RenderField HeaderText="审核状态" DataField="State" RendererFunction="renderCheck" Hidden="true"></f:RenderField>
                                <f:BoundField DataField="ProPlanNo" SortField="ProPlanNo" Width="150px" HeaderText="生产计划单号" Hidden="true" />
                                <f:BoundField DataField="ProOrderNo" SortField="ProOrderNo" Width="150px" HeaderText="生产单号" />
                                <f:BoundField DataField="SaleOrderNo" SortField="SaleOrderNo" Width="150px" HeaderText="销售单号" />
                                <f:BoundField DataField="ProNo" SortField="ProNo" Width="150px" HeaderText="品号" />
                                <f:BoundField DataField="ProName" SortField="ProName" Width="200px" HeaderText="品名" />
                                <f:BoundField DataField="ItemNo" SortField="ItemNo" Width="150px" HeaderText="料号" />
                                <f:BoundField DataField="ItemName" SortField="ItemName" Width="200px" HeaderText="名称" />
                                <f:BoundField DataField="Spec" SortField="Spec" Width="300px" HeaderText="规格" />

                                <f:BoundField DataField="WorkShop" Width="150px" HeaderText="生产车间" />
                                <f:LinkButtonField CommandName="Delete" HeaderText="操作" ConfirmText="是否删除该记录" Icon="Delete" Width="80px"></f:LinkButtonField>
                            </Columns>
                        </f:Grid>


                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>

        <script>

            var ddlResultID = '<%=ddlResult.ClientID %>';

            function renderCheck(value) {
                if (value == 'True') {
                    return "通过"
                } else if (value == 'False') {
                    return "未通过"
                } else {
                    return "未审核"
                }
            }


            function renderResult(value) {
                return F(ddlResultID).getTextByValue(value);
            }
        </script>
    </form>
</body>
</html>
