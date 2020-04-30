<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductInstockCheck.aspx.cs" Inherits="AppBoxPro.Mobile.ProductInstockCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <f:Panel runat="server" ShowBorder="false" ShowHeader="false" AutoScroll="true" ID="Panel1" IsFluid="true">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Title="生产品检" runat="server" HeaderStyle="true">
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Grid runat="server" ShowBorder="false" ShowHeader="false" ID="Grid1" Height="150px" AllowCellEditing="true">
                    <Columns>
                        <f:RowNumberField></f:RowNumberField>
                        <f:RenderField DataField="barcode" HeaderText="条码号" ColumnID="barcode" Width="180px"></f:RenderField>
                        <f:RenderField DataField="orderno" HeaderText="生产单号" ColumnID="orderno" Width="180px">
                        </f:RenderField>
                        <f:RenderField DataField="itemno" HeaderText="料号" ColumnID="itemno">
                        </f:RenderField>
                        <f:RenderField DataField="prono" HeaderText="产品号" ColumnID="prono">
                        </f:RenderField>
                        <f:RenderField DataField="itemname" HeaderText="品名" ColumnID="itemname">
                        </f:RenderField>
                        <f:RenderField DataField="spec" HeaderText="规格" ColumnID="spec">
                        </f:RenderField>
                        <f:RenderField DataField="targetqut" HeaderText="生产数量" ColumnID="targetqut">
                        </f:RenderField>
                        <f:RenderField DataField="qut" HeaderText="入库数量" ColumnID="qut">
                        </f:RenderField>
                        <f:RenderField DataField="choujianqut" HeaderText="抽检数量" ColumnID="choujianqut">
                        </f:RenderField>
                        <f:RenderField DataField="result" HeaderText="品检结果" ColumnID="result">
                        </f:RenderField>
                        <f:RenderField DataField="unit" HeaderText="单位" ColumnID="unit">
                        </f:RenderField>
                        <f:RenderField DataField="space" HeaderText="库位" ColumnID="space">
                        </f:RenderField>
                        <f:RenderField DataField="remark" HeaderText="备注" ColumnID="remark">
                        </f:RenderField>
                        <f:RenderField DataField="saleno" HeaderText="订单号" ColumnID="saleno">
                        </f:RenderField>
                    </Columns>
                    <Listeners>
                        <f:Listener Event="rowselect" Handler="onRowSelect" />
                    </Listeners>
                </f:Grid>
                <f:Form runat="server" ShowBorder="false" ShowHeader="false" ID="form2">
                    <Rows>
                        <f:FormRow Height="40px" MarginRight="20px">
                            <Items>
                                <f:TextBox runat="server" Label="条码号" ID="tbxbarcode" OnBlur="tbxprosn_Blur" AutoPostBack="true" LabelWidth="100px" Required="true" EnableBlurEvent="true"></f:TextBox>
                            </Items>
                        </f:FormRow>

                        <f:FormRow Height="40px" MarginRight="20px">
                            <Items>
                                <f:TextBox runat="server" Label="生产单号" ID="tbxprosn" LabelWidth="100px" Required="true"></f:TextBox>
                                
                            </Items>
                        </f:FormRow>
                        <f:FormRow  Height="40px" MarginRight="20px">
                            <Items>
                                <f:TextBox runat="server" Label="订单号" ID="tbxsaleno" LabelWidth="100px" Required="true"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow Height="40px" MarginRight="20px">
                            <Items>
                                <f:TextBox runat="server" Label="料号" ID="tbxitemno" LabelWidth="60px" Required="true"></f:TextBox>
                                <f:TextBox runat="server" Label="产品号" ID="tbxprono" LabelWidth="70px" Required="false"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow Height="40px" MarginRight="20px">
                            <Items>

                                <f:TextBox runat="server" Label="品名" ID="tbxitemname" LabelWidth="60px" Required="true"></f:TextBox>
                                <f:TextBox runat="server" Label="规格" ID="tbxspec" LabelWidth="60px" Required="true"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow Height="40px" MarginRight="20px" Hidden="true">
                            <Items>
                                <f:NumberBox runat="server" Label="入库数" LabelWidth="80px" Required="true" NoNegative="true" ID="nbQut"></f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow  Height="40px" MarginRight="20px" >
                            <Items>
                                <f:NumberBox ID="nbChoujian" runat="server" Required="true" NoDecimal="true" NoNegative="true" Label="抽检数"></f:NumberBox>
                                <f:DropDownList ID="ddlresult" Required="true" ForceSelection="true" runat="server" Label="抽检结果">
                                    <f:ListItem Text="合格" Value="合格" />
                                     <f:ListItem Text="不合格" Value="不合格"/>
                                     <f:ListItem Text="特采" Value="特采"/>
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow Height="40px" MarginRight="20px">
                            <Items>
                                <f:NumberBox runat="server" Label="生产数" LabelWidth="80px" Required="true" NoNegative="true" ID="nbPurQut" Readonly="true"></f:NumberBox>
                                <f:DropDownList ID="ddlstorehouse" runat="server" Label="库位" LabelWidth="60px" Required="true"></f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow Height="40px" MarginRight="20px">
                            <Items>
                                <f:TextBox runat="server" Label="单位" LabelWidth="60px" ID="tbxunit"></f:TextBox>
                                <f:TextBox runat="server" Label="备注" LabelWidth="60px" ID="tbxremark"></f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow Height="40px" MarginRight="20px">
                            <Items>
                                <f:Button ID="btnAdd" Text="添加" Type="Button" ValidateMessageBoxPlain="true" EnablePostBack="false" runat="server" Icon="Add">
                                    <Listeners>
                                        <f:Listener Event="click" Handler="onNewClick" />
                                    </Listeners>
                                </f:Button>

                                <f:Button ID="Button2" Text="删除" Type="Button" ValidateMessageBoxPlain="true" runat="server" Icon="Delete" EnablePostBack="false">
                                    <Listeners>
                                        <f:Listener Event="click" Handler="onDeleteClick" />
                                    </Listeners>
                                </f:Button>
                                <f:Button ID="btnSend" Text="提交" Type="Submit" ValidateMessageBoxPlain="true" runat="server" OnClick="btnSend_Click" IconFont="_Send">
                                </f:Button>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>


    </form>

    <script>

        var grid1ClientID = '<%=Grid1.ClientID%>';
        var formClientID = '<%=form2.ClientID%>';

        var tbxbarcodeClientID = '<%=tbxbarcode.ClientID%>';
        var tbxprosnClientID = '<%=tbxprosn.ClientID%>';
        var tbxitemnoClientID = '<%=tbxitemno.ClientID%>';
        var tbxpronoClientID = '<%=tbxprono.ClientID%>';
        var tbxitemnameClientID = '<%=tbxitemname.ClientID%>';
        var tbxspecClientID = '<%=tbxspec.ClientID%>';
        var nbPurQutClientID = '<%=nbPurQut.ClientID%>';

        var nbQutClientID = '<%=nbQut.ClientID%>';
        var tbxunitClientID = '<%=tbxunit.ClientID%>';
        var ddlstorehouseClientID = '<%=ddlstorehouse.ClientID%>';
        var tbxremarkClientID = '<%=tbxremark.ClientID%>';
        var tbxsalenoClientID = '<%=tbxsaleno.ClientID%>';

        //抽检数
        var nbChoujianClientID = '<%=nbChoujian.ClientID%>';
        //抽检结果
        var ddlResultClientID = '<%=ddlresult.ClientID%>';

        var storagename = 'productinstockcheck';

        //安卓手持机调用的方法
        function callJS(prosn) {
            F(tbxbarcodeClientID).setText(prosn);

            //自定义回发
            __doPostBack('', 'tbxprosn__change');
        }

        function onNewClick(event) {
            var valid = F(formClientID).isValid()[0];
            console.log(F(formClientID).isValid());

            if (valid == false) {
                F.alert('表单错误');
                return;
            }

            console.log(event);
            var obj = {
                'barcode': F(tbxbarcodeClientID).getValue(),
                'orderno': F(tbxprosnClientID).getValue(),
                'itemno': F(tbxitemnoClientID).getValue(),
                'prono': F(tbxpronoClientID).getValue(),
                'itemname': F(tbxitemnameClientID).getValue(),
                'spec': F(tbxspecClientID).getValue(),
                'targetqut': F(nbPurQutClientID).getValue(),
                'qut': F(nbQutClientID).getValue(),
                'unit': F(tbxunitClientID).getValue(),
                'space': F(ddlstorehouseClientID).getValue(),
                'remark': F(tbxremarkClientID).getValue(),
                'saleno': F(tbxsalenoClientID).getValue(),
                'choujianqut': F(nbChoujianClientID).getValue(),
                'result': F(ddlResultClientID).getValue()
            }



            var storage = localStorage.getItem(storagename);
            if (storage != '' && storage != null) {
                var objs = JSON.parse(storage);

                var isRepeat = false;

                objs.forEach(function (item) {
                    console.log(item.barcode);
                    if (F(tbxbarcodeClientID).getValue() == item.barcode) {
                        console.log('重复条码')
                        isRepeat = true;
                    }
                });

                if (isRepeat == true) {
                    F.alert('重复条码');
                    return;
                }

                objs.push(obj);

                localStorage.setItem(storagename, JSON.stringify(objs));
            } else {
                var _objs = [];
                _objs.push(obj);
                localStorage.setItem(storagename, JSON.stringify(_objs));
            }
            F(grid1ClientID).addNewRecord(
                obj
                , true);

            F(formClientID).reset();
        }

        var currentRowIndex;

        function onRowSelect(event, rowId, rowIndex) {
            console.log(-rowIndex - 1);
            currentRowIndex = -rowIndex - 1;
        }

        function onDeleteClick(event) {
            var grid = F(grid1ClientID);

            // 如果没有选中项，弹出提示信息
            if (!grid.hasSelection()) {
                F.alert('请至少选择一项！');
                return;
            }

            // 删除选中行之前先弹出确认对话框
            F.confirm({
                message: '删除选中行？',
                ok: function () {
                    // 删除选中行
                    grid.deleteSelectedRows();

                    //删除localstorage
                    var objs = JSON.parse(localStorage.getItem(storagename));

                    console.log('当前删除行' + currentRowIndex)
                    objs.splice(currentRowIndex, 1);

                    console.log(JSON.stringify(objs));

                    localStorage.setItem(storagename, JSON.stringify(objs));

                }
            });
        }


        function onResetClick(event) {
            F.confirm({
                message: '确定要重置表格数据？',
                ok: function () {
                    F(grid1ClientID).rejectChanges();
                }
            });
        }


        F.ready(function () {

            var grid = F(grid1ClientID);

            var objs = localStorage.getItem(storagename);
            if (objs != '' && objs != null) {
                var objsJson = JSON.parse(objs);
                for (var i = 0; i < objsJson.length; i++) {
                    F(grid1ClientID).addNewRecord(
                        objsJson[i]
                        , true);
                }
            }

        });

        //
        function clearLocalStorage() {
            localStorage.setItem(storagename, '');
            //重置表格数据
            F(grid1ClientID).rejectChanges();
        }

    </script>
</body>
</html>
