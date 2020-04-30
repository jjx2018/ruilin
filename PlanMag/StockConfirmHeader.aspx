<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockConfirmHeader.aspx.cs" Inherits="AppBoxPro.PlanMag.StockConfirmHeader" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <style type="text/css">
        .f-grid-row .f-grid-cell-bfRowNum {
            background-color: #0094ff;
            color: #fff;
        }

        .f-grid-row .f-grid-cell-hlfMajor {
            background-color: #b200ff;
            color: #fff;
        }

         .f-grid-row .f-grid-cell-hlfMajor a,
         .f-grid-row .f-grid-cell-hlfMajor a:hover {
             color: #fff;
         }
          .f-grid-row.color1,
        .f-grid-row.color1 .f-icon,
        .f-grid-row.color1 a {
            background-color: #1AA348;
             /*background-color: #0094ff;*/
            color: #fff;
        }
    </style>
</head>
<body>
       <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel5" EnableFStateValidation="false" runat="server" />
       <f:Panel ID="Panel5" CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            Layout="VBox" AutoScroll="true" Margin="0px" ShowHeader="false" Title="1111111111111"
            BoxConfigChildMargin="0 0 0 0" BodyPadding="0">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true" AllowCellEditing="false" ClicksToEdit="2" 
                     AllowSorting="false" DataKeyNames="SN" SortField="SN"   OnSort="Grid1_Sort"
                    AllowPaging="true" IsDatabasePaging="true" EnableRowSelectEvent="true" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                
                                <f:TwinTriggerBox ID="TwinTriggerBox1" runat="server" ShowLabel="false" EmptyText="请输入订单编号或型号或名称或客人编号"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage1_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage1_Trigger1Click" Width="450px">
                                </f:TwinTriggerBox>
                                
                                <f:Button ID="Button2" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                              <f:Button ID="btnBack" Pressed="true" ValidateForms="haha"  runat="server" Text="后退" Icon="RewindBlue" OnClick="btnBack_Click"></f:Button>
                          <f:TextBox ID="txtClickColsName" runat="server" Hidden="true" Label="列名" LabelWidth="50px"></f:TextBox>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
                                  <f:Button ID="Button6" EnableAjax="false" DisableControlBeforePostBack="false"  runat="server" Icon="PageExcel"    Text="导出Excel">
                            </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText2" runat="server" Text="每页记录数：">
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
                        <f:RowNumberField Width="30px" ColumnID="bfRowNum" TextAlign="Center" EnablePagingNumber="true" />
                         <f:TemplateField HeaderText="BOM明细" Width="100px" ToolTip="查看BOM明细">
                    <ItemTemplate>
                        <a href="javascript:;" style="color:blue;text-decoration:underline;" onclick="lookOrder('<%#Eval("SN")%>','<%#Eval("ItemNo")%>','<%#Eval("ItemName")%>','<%#Eval("OrderNo")%>')">BOM明细</a>
                    </ItemTemplate>
                </f:TemplateField>
                        <f:BoundField ColumnID="OrderNo"  DataField="OrderNo" TextAlign="Center" SortField="OrderNo" Width="150px" HeaderText="订单编号" />
                        <f:BoundField ColumnID="ClinetNo"  DataField="ClinetNo" TextAlign="Center"   Width="100px" HeaderText="客人编号" />
                        <f:BoundField ColumnID="ItemNo"  DataField="ItemNo" TextAlign="Center" SortField="ItemNo"   Width="120px" HeaderText="型号" />
                          <f:BoundField ColumnID="ItemName"  DataField="ItemName" TextAlign="Center" SortField="ItemName"   Width="200px" HeaderText="产品名称" />    
                        <f:BoundField ColumnID="Price"  DataField="Price" TextAlign="Center" SortField="Price" Width="80px" HeaderText="单价" />
                        <f:BoundField ColumnID="Quantity"  DataField="Quantity" TextAlign="Center" SortField="Quantity" Width="100px" HeaderText="数量" />
                       <f:RenderField Width="120px" ColumnID="IsBom" DataField="IsBom" TextAlign="Center"
                      HeaderText="BOM状态" RendererFunction="bomState">
                    <Editor>
                        <f:TextBox ID="txtIsBom" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                         <f:BoundField ColumnID="BomVer"   DataField="BomVer" TextAlign="Center" Width="90px" HeaderText="BOM版本" />
                        <f:BoundField  ColumnID="Inputer"  DataField="Inputer" TextAlign="Center" Width="80px" HeaderText="录入人" />
                         <f:BoundField ColumnID="Remark"  DataField="Remark"  ExpandUnusedSpace="true"  HeaderText="备注" />
                         <f:BoundField Hidden="true" DataField="Color"  ExpandUnusedSpace="true"  HeaderText="颜色" />
                         <f:BoundField Hidden="true" DataField="IsNew"  ExpandUnusedSpace="true"  HeaderText="是否新产品" />
                         <f:BoundField  Hidden="true" DataField="IsPackingmaterials"  ExpandUnusedSpace="true"  HeaderText="是否新包材" />
                         <f:BoundField Hidden="true" DataField="CountryPackVer"  ExpandUnusedSpace="true"  HeaderText="国家包材版本" />
                         <f:BoundField Hidden="true" DataField="IsChange"  ExpandUnusedSpace="true"  HeaderText="是否变更" />
                         <f:BoundField Hidden="true"  DataField="Demand1"  ExpandUnusedSpace="true"  HeaderText="变更备注1" />
                         <f:BoundField Hidden="true" DataField="Demand2"  ExpandUnusedSpace="true"  HeaderText="变更备注2" />
                     
                         <f:WindowField ColumnID="viewField" HeaderText="详情" TextAlign="Center" Icon="ApplicationViewIcons" ToolTip="查看详情" WindowID="Window1"
                            Title="查看详情" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/OrderMag/OrderDetailedit.aspx?id={0}&k=1"
                            Width="60px" />
                         <f:WindowField ColumnID="editField" Hidden="true" TextAlign="Center" Icon="Pencil" HeaderText="编辑" ToolTip="编辑" WindowID="Window1"
                            Title="编辑" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/OrderMag/OrderDetailedit.aspx?id={0}&k=2"
                            Width="60px" />
                        <f:LinkButtonField ColumnID="deleteField" Hidden="true" TextAlign="Center" HeaderText="删除" Icon="Delete" ToolTip="删除"  
                            ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="60px" />
                        <f:BoundField Hidden="true" DataField="IsConfirm"  HeaderText="IsConfirm" />
                    </Columns>
                </f:Grid>

             </Items>
        </f:Panel>










        
       
       
       
       
       
       <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="1000px"
            Height="720px">
        </f:Window>
           <f:Window ID="Window2" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="500px"
            Height="300px">
        </f:Window>
    </form>

</body>
</html>
<script>

    function lookOrder(ID, itemno, itemname, orderno) {
        parent.addExampleTab({
            id: 'StockConfirmDetail_'+ID + '_tab',
            iframeUrl: 'PlanMag/StockConfirmDetail.aspx?ProNo=' + escape(itemno) + '&od=' + escape(orderno) + '&ItemName=' + escape(itemname) + '&sn=' + ID,
            title: itemname + 'BOM明细',
            iconFont: 'sign-in',
            refreshWhenExist: true
        });
    }
    // 自定义编辑器获取函数（从Editor返回单元格）
    function editGetterName(editor) {
        return editor.getText();
    }

    // 自定义编辑器设置函数（从单元格进入Editor）
    function editSetterName(editor, val, columnId, rowId) {

        var grid2 = F(grid2ClientID);
        var rowValue = grid2.getRowValue(rowId);
        var rowCode = rowValue['ItemNo'];
        console.log(rowValue + '---' + rowCode);
        editor.setValue(rowCode, val);
    }


    function onGrid3RowClick(event, grid3RowId) {
        var grid2 = F(grid2ClientID);
        var grid2RowId = grid2.getSelectedCell()[0];
        var rowValue = this.getRowValue(grid3RowId);
        var Q = F(QuantityID);
        Q = Q.getValue();
        grid2.updateCellValue(grid2RowId, {
            'ItemNo': rowValue.ItemNo,
            'ItemName': rowValue.ItemName,
            'Spec': rowValue.Spec,
            'Material': rowValue.Material,
            'SurfaceDeal': rowValue.SurfaceDeal,
            'ProUsingQuantity': rowValue.ProUsingQuantity,
            'OrderUsingQuantity': rowValue.ProUsingQuantity * Q,
            'ZongCheng': rowValue.ZongCheng,
            'BaseNum': rowValue.BaseNum,
            'Sclass': rowValue.Sclass,
            'MainFrom': rowValue.MainFrom,
            'WorkShop': rowValue.WorkShop,
            'StoreHouse': rowValue.StoreHouse
        });

    }

    function onGridAfterEdit(event, value, params) {
        var me = this, columnId = params.columnId, rowId = params.rowId;
        if (columnId === 'ProUsingQuantity') {
            var pq = me.getCellValue(rowId, 'ProUsingQuantity');
            //var poq = me.getCellValue(rowId, 'ProOrgQuantity');
            var Q = F(QuantityID);
            //var poq = 100;// me.getCellValue(rowId, 'ProOrgQuantity');
            console.log("qqq::::" + Q.getValue());
            Q = Q.getValue();
            me.updateCellValue(rowId, 'OrderUsingQuantity', pq * Q);

        }
    }

    </script>
 <script>



     // 递归遍历节点 rowData 的所有子节点
     function resolveRowChildren(rowData, callback) {
         var children = rowData.children;
         if (children && children.length) {
             for (var i = 0, count = children.length; i < count; i++) {
                 var item = children[i];
                 callback.apply(item);
                 resolveRowChildren(item, callback);
             }
         }
     }

     function onGridRowSelect(event, rowId) {
         var grid = this, rowData = grid.getRowData(rowId);

         var rowIds = [];
         resolveRowChildren(rowData, function () {
             rowIds.push(this.id);
         });

         // 本过程中不触发事件
         F.noEvent(function () {
             // 第二个参数true：保持现有选中项
             grid.selectRows(rowIds, { keep: true });
         });
     }

     function onGridRowDeselect(event, rowId) {
         var grid = this, rowData = grid.getRowData(rowId);

         var rowIds = [];
         resolveRowChildren(rowData, function () {
             rowIds.push(this.id);
         });

         // 本过程中不触发事件
         F.noEvent(function () {
             // 第二个参数true：保持现有选中项
             grid.deselectRows(rowIds);
         });
     }

    </script>
    <script type="text/javascript">
        function bomState(value) {
            return value == 0 ? "<span style='color:red;'>未生成</span>" : "<span style='color:blue;'>已生成</span>";
        }
        function toBOM(ID, ORDERNO, ITEMNO, Q, TITLE) {
            parent.addExampleTab({
                id:'RealBom_'+ ID + '_tab',
                iframeUrl: 'BomMag/RealBom.aspx?sn=' + ID + '&od=' + ORDERNO + '&id=' + ITEMNO + '&q=' + Q,
                title: TITLE,
                iconFont: 'sign-in',
                refreshWhenExist: true
            });
        }
        function reloadGrid(param) {
            alert("dddd" + param);
            __doPostBack(null, 'ReloadGrid$' + param);
        }
        function renderGender(value) {
            return value == 0 ? '已审核' : '未审核';
        }

     </script>
    
