<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="instock_new.aspx.cs" Inherits="AppBoxPro.stock.instock_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <f:Grid runat="server" ID="Grid1" AllowCellEditing="true" ClicksToEdit="1" ShowHeader="false">
            <Toolbars>
                <f:Toolbar runat="server" Position="Top">
                    <Items>
                        <f:Button ID="btnNew" EnablePostBack="false" runat="server" Text="新增"></f:Button>
                        <f:Button ID="btnReset" EnablePostBack="false" runat="server" Text="重置"></f:Button>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnDelete" EnablePostBack="false" runat="server" Text="删除"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:RowNumberField></f:RowNumberField>
                <f:RenderField HeaderText="料号" ColumnID="ItemNo">
                    <Editor>
                        <f:TriggerBox ID="tbxEditorItemNo" TriggerIcon="Search" OnTriggerClick="tbxEditorItemNo_TriggerClick" runat="server" EnableEdit="false">
                        </f:TriggerBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField HeaderText="名称" ColumnID="ItemName" ExpandUnusedSpace="true"></f:RenderField>
                <f:RenderField HeaderText="规格" ColumnID="Spec"></f:RenderField>
                <f:RenderField HeaderText="入库数量" ColumnID="Quantity">
                    <Editor>
                        <f:NumberBox runat="server" NoNegative="true" MinValue="0"></f:NumberBox>
                    </Editor>
                </f:RenderField>

                <f:RenderField HeaderText="库位" ColumnID="Space">
                    <Editor>
                        <f:TextBox runat="server"></f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField HeaderText="备注" ColumnID="remark">
                    <Editor>
                        <f:TextBox runat="server"></f:TextBox>
                    </Editor>
                </f:RenderField>
            </Columns>
        </f:Grid>
        <f:Form runat="server" ShowBorder="true" ShowHeader="false" BodyPadding="5px" ID="form2">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:TextBox runat="server" ID="tbxorderno" Label="入库单号" ShowRedStar="true" Required="true" Readonly="true"></f:TextBox>
                        <f:DropDownList runat="server" ForceSelection="true" Required="true" Label="入库类型" ID="ddlPro">
                            <f:ListItem Text="其他入库" Value="其他入库" />
                            <f:ListItem Text="销售退货" Value="销售退货" />
                        </f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>

                        <f:DatePicker runat="server" ID="dpoptdate" Label="入库日期" Required="true"></f:DatePicker>
                        <f:TextBox runat="server" Label="经办人" ID="tbxjingbanren" Enabled="false"></f:TextBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                        <f:TextArea Label="备注" runat="server" ID="tbxremark"></f:TextArea>

                    </Items>
                </f:FormRow>
            </Rows>
            <Toolbars>
                <f:Toolbar runat="server" Position="Bottom" ToolbarAlign="Right">
                    <Items>
                        <f:Button runat="server" Text="保存" Icon="SystemSave" ID="btnsave" OnClick="btnsave_Click" ValidateForms="form2" OnClientClick="if(!isValid()){return false;}"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Form>

        <f:Window ID="Window1" Hidden="true" EnableIFrame="true" EnableMaximize="true"
            EnableResize="true" Target="Top" runat="server" Height="350px" Width="700px"
            Title="选择物料">
        </f:Window>

        <script>
            var gridClientID = '<%= Grid1.ClientID %>';

            function updateGridRow(rowId, values) {
                var grid = F(gridClientID);

                // cancelEdit用来取消编辑
                grid.cancelEdit();

                grid.updateCellValue(rowId, values);
            }

            function isValid() {
                var grid1 = F('<%= Grid1.ClientID %>');
                var valid = true, modifiedData = grid1.getModifiedData();

                $.each(modifiedData, function (index, rowData) {

                    // rowData.id: 行ID
                    // rowData.status: 行状态（newadded, modified, deleted）
                    // rowData.values: 行中修改单元格对象，比如 { "Name": "刘国2", "Gender": 0, "EntranceYear": 2003 }
                    if (rowData.status === 'deleted') {
                        return true; // continue
                    }

                    var itemno = rowData.values['ItemNo'];
                    if (typeof (itemno) != 'undefined' && $.trim(itemno) == '') {
                        F.alert({
                            message: '料号不能为空！',
                            ok: function () {
                                grid1.startEdit(rowData.id, 'ItemNo');
                            }
                        });

                        valid = false;

                        return false; // break
                    }

                    var Quantity = rowData.values['Quantity'];
                    if (typeof (Quantity) != 'undefined' && $.trim(Quantity) == '') {
                        F.alert({
                            message: '数量不能为空！',
                            ok: function () {
                                grid1.startEdit(rowData.id, 'Quantity');
                            }
                        });

                        valid = false;

                        return false; // break
                    }
                });


                return valid;
            }



        </script>
    </form>
</body>
</html>
