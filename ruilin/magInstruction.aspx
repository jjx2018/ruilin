<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magInstruction.aspx.cs" Inherits="AppBoxPro.ruilin.magInstruction" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Grid1" EnableFStateValidation="false" runat="server" />
        <f:Grid ID="Grid1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="" EnableCollapse="false"
            runat="server" DataKeyNames="SN" AllowCellEditing="true" ClicksToEdit="1"  OnPreDataBound="Grid1_PreDataBound"  EnableTextSelection="true" 
            DataIDField="SN" EnableCheckBoxSelect="true"  KeepCurrentSelection="false" OnPageIndexChange="Grid1_PageIndexChange"  AllowSorting="true" OnSort="Grid1_Sort"  SortField="SN" OnRowCommand="Grid1_RowCommand"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true">
            <Toolbars>
                 <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                       <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="名称、料号、规格、材质、类别搜索"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click" Width="400px">
                                </f:TwinTriggerBox>
                          <f:RadioButtonList ID="rbtState" runat="server" ColumnWidth="150px" Width="350px" AutoPostBack="true" OnSelectedIndexChanged="rbtState_SelectedIndexChanged1">
                             <f:RadioItem Text="全部" Value="" Selected="true" />
                             <f:RadioItem Text="未确认" Value="0" />
                             <f:RadioItem Text="已确认" Value="1"  />
                         </f:RadioButtonList>
                         <f:Button ID="btnSearch" Pressed="true" ValidateForms="haha"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                       
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        
                          <f:Button ID="btnExcel" EnableAjax="false" DisableControlBeforePostBack="false" OnClick="Button1_Click" runat="server" Icon="PageExcel"    Text="导出Excel">
                            </f:Button> 
                        </Items>
                     </f:Toolbar>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnNew"   Text="新增" Hidden="true" Icon="Add" EnablePostBack="false" runat="server">
                        </f:Button>

                          <f:Button ID="Button1"  OnClick="btnSave_Click" runat="server" Icon="SystemSave"    Text="保存">
                            </f:Button> 
                          <f:Button ID="btnDelete" Hidden="false" OnClick="btnDeleteSelected_Click" Text="删除" Icon="Delete"  runat="server">
                        </f:Button>
                    <f:NumberBox runat="server" ID="numLabCount" Width="200px" MinValue="1" Text="1" Label="标签份数"></f:NumberBox>
                          <f:Button ID="btnLabPrint"  OnClick="btnLabPrint_Click" runat="server" Icon="Printer"    Text="标签打印">
                            </f:Button> 
                       
                        
                       
                        
                       
                         
                        
                    </Items>
                </f:Toolbar>
            </Toolbars>
                                <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
                            runat="server">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                        </f:DropDownList>
                    </PageItems>

            <Columns>
                <f:RowNumberField />
                  <f:RenderField Width="150px" ColumnID="OrderNo" DataField="OrderNo"
                      HeaderText="订单编号" EnableColumnEdit="false">
                    <Editor>
                        <f:TextBox ID="txtOrderNo" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
               
                  <f:RenderField Width="150px" ColumnID="ProNo" DataField="ProNo" EnableColumnEdit="false"
                      HeaderText="产品编号">
                    <Editor>
                        <f:TextBox ID="txtProNo" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                  <f:RenderField Width="150px" ColumnID="ProName" DataField="ProName" EnableColumnEdit="false"
                      HeaderText="产品名称">
                    <Editor>
                        <f:TextBox ID="txtProName" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 

                  <f:RenderField Width="150px" ColumnID="ItemNo" DataField="ItemNo" EnableColumnEdit="false"
                      HeaderText="料号">
                    <Editor>
                        <f:TextBox ID="tbxItemNo" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="300px" ColumnID="ItemName" DataField="ItemName" EnableColumnEdit="false"
                      HeaderText="名称">
                    <Editor>
                        <f:TextBox ID="txtItemName" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="MainFrom" DataField="MainFrom" EnableColumnEdit="false"
                      HeaderText="主要来源">
                    <Editor>
                        <f:TextBox ID="txtMakeMethod" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                  <f:RenderField Width="90px" ColumnID="UsingQuantity"  DataField="UsingQuantity" EnableColumnEdit="false"
                      HeaderText="备货数量" RendererFunction="UsingQuantity">
                    <Editor>
                        <f:NumberBox ID="txtUsingQuantity" MinValue="1" CssStyle="color:red;" Required="false" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>

                  <f:RenderField Width="90px" ColumnID="ConfirmQuantity" DataField="ConfirmQuantity"
                      HeaderText="确认数量" RendererFunction="ConfirmQuantity">
                      
                    <Editor>
                        <f:NumberBox ID="txtConfirmQuantity" AutoPostBack="false"  CssStyle="color:green;"  MinValue="1" Required="false" runat="server">
                          
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                  <f:RenderField Width="120px" ColumnID="RealUsingQuantity" DataField="RealUsingQuantity" EnableColumnEdit="false"
                      HeaderText="生产/采购数量" RendererFunction="RealUsingQuantity">
                    <Editor>
                        <f:NumberBox ID="txtRealUsingQuantity" MinValue="1"  CssStyle="color:blue;" Required="false" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>

                <f:RenderField Width="180px" ColumnID="IsConfirm" DataField="IsConfirm" EnableColumnEdit="true"
                      HeaderText="状态" RendererFunction="renderState" >
                    <Editor>
                        <f:DropDownList Required="false" runat="server" ID="txtIsConfirm">
                            <f:ListItem Text="未确认" Value="0" />
                            <f:ListItem Text="已确认" Value="1" />
                        </f:DropDownList>
                       
                    </Editor>
                </f:RenderField>

                 <f:RenderField Width="180px" ColumnID="Spec" DataField="Spec" EnableColumnEdit="false"
                      HeaderText="规格">
                    <Editor>
                        <f:TextBox ID="txtSpec" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="Material" DataField="Material" EnableColumnEdit="false"
                      HeaderText="材质">
                    <Editor>
                        <f:TextBox ID="txtMaterial" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="180px" ColumnID="SurfaceDeal" DataField="SurfaceDeal" EnableColumnEdit="false"
                      HeaderText="表面处理">
                    <Editor>
                        <f:TextBox ID="txtSurfaceDeal" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="100px" ColumnID="Sclass" DataField="Sclass" FieldType="String" HeaderText="类别" EnableColumnEdit="false">
                    <Editor>
                        <f:DropDownList Required="false" runat="server" ID="ddlSclass">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>                 
                
                 <f:RenderField Width="180px" ColumnID="ReceiveDept" DataField="ReceiveDept" EnableColumnEdit="false"
                      HeaderText="接收部门">
                    <Editor>
                        <f:TextBox ID="txtReceiveDept" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="180px" ColumnID="Receiver" DataField="Receiver" EnableColumnEdit="false"
                      HeaderText="接收人">
                    <Editor>
                        <f:TextBox ID="txtReceiver" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="180px" ColumnID="ConfirmDate" DataField="ConfirmDate" EnableColumnEdit="true"
                      HeaderText="确认日期">
                    <Editor>
                        <f:TextBox ID="txtConfirmDate" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="180px" ColumnID="BarCode" DataField="BarCode" EnableColumnEdit="true"
                      HeaderText="条码">
                    <Editor>
                        <f:TextBox ID="txtBarCode" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="180px" ColumnID="Remark" DataField="Remark"
                      HeaderText="备注">
                    <Editor>
                        <f:TextBox ID="txtRemark" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
            

               <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                 
               
            </Columns>
             <Listeners>
                
                 <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
            </Listeners>
        </f:Grid>
       
    </form>
    <script>
        var gridClientID = '<%= Grid1.ClientID %>'; 
        
        var uq = 0;
        var rowid;
        function onGrid1RowSelect(event, key, rowid) {
            rowid = rowid;
            //if (columnId === 'EntranceYear') {
                //if (value >= 2003) {
                //    grid.updateCellValue(rowId, 'Name.cls', 'highlight');
                //} else {
                //    grid.updateCellValue(rowId, 'Name.cls', '');
                //}
            //}

            //alert(gridClientID+"----"+rowId+"---"+event);
            var grid1 = F(gridClientID);
            uq = grid1.data[rowid].values["UsingQuantity"];
            //var currentSelectedRows = grid1.getSelectedRows();
            //grid1.getSelectedRows(function (rowData) {
            //    uq = parseInt(rowData.values['UsingQuantity'], 10);
                 
            //});
            console.log(grid1);
            console.log(grid1.data[rowid].values["UsingQuantity"]);
        }
        function onGridAfterEdit(event, value, params) {
            var me = this, columnId = params.columnId, rowId = params.rowId;
            //if (columnId === 'ChineseScore' || columnId === 'MathScore') {
            var UsingQuantity = me.getCellValue(rowId, 'UsingQuantity');
            var ConfirmQuantity = me.getCellValue(rowId, 'ConfirmQuantity');

            me.updateCellValue(rowId, 'RealUsingQuantity', parseInt(UsingQuantity) - parseInt(ConfirmQuantity));
            me.updateCellValue(rowId, 'IsConfirm', 1);

            //}
        }
        function UsingQuantity(value) {
            return "<span style='color:red;'>" + value + "</span>";
        }
        function ConfirmQuantity(value) {
            return "<span style='color:green;'>" + value + "</span>";
        }
        function RealUsingQuantity(value) {
            return "<span style='color:blue;'>" + value + "</span>";
        }
        function renderGender(value) {
            return value == 1 ? '男' : '女';
        }

        function resolveRows(columnId, newValue) {
            var grid = F(gridClientID);
            grid.getRowEls().each(function () {
                grid.updateCellValue(this, columnId, newValue);
            });
        }
        function renderState(value) {
            //0未确认  1确认
            if (value == "0")
                return '<span style="color:red;">未确认</span>';
            else
                return '<span style="color:green;">已确认</span>';

        }
        var textbox2ClientID = '<%= txtRealUsingQuantity.ClientID %>';
        var txtUsingQuantityID = '<%= txtUsingQuantity.ClientID %>';
        function onTextBoxChange() {
            var grid1 = F(gridClientID);
            console.log(uq+"---" + this.getValue() + "---");
            grid1.updateCellValue(rowid, 'RealUsingQuantity',10);
        }

    </script>
</body>
</html>