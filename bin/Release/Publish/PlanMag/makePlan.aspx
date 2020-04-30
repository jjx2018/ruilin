<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="makePlan.aspx.cs" Inherits="AppBoxPro.PlanMag.makePlan" %>

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
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableRowDoubleClickEvent="true"   AllowColumnLocking="true" OnAfterEdit="Grid1_AfterEdit">
            <Toolbars>
                 <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                       <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="名称、料号、规格、材质、类别搜索"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click" Width="300px">
                                </f:TwinTriggerBox>
                         <f:RadioButtonList ID="rbtmakethod" runat="server" Label="类型" LabelWidth="50px" ColumnWidth="50px" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="rbtmakethod_SelectedIndexChanged">
                             <f:RadioItem Text="委外" Value="委外" Selected="true" />
                             <f:RadioItem Text="生产" Value="生产" />
                             <f:RadioItem Text="采购" Value="采购"  />
                             <f:RadioItem Text="客供" Value="客供"  />
                         </f:RadioButtonList>
                         <f:RadioButtonList ID="rbtIsPlan" runat="server" Label="状态" LabelWidth="50px" ColumnWidth="80px" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="rbtIsPlan_SelectedIndexChanged">
                             <f:RadioItem Text="全部" Value="" Selected="true" />
                             <f:RadioItem Text="未生成" Value="0" />
                             <f:RadioItem Text="已生成" Value="1"  />
                         </f:RadioButtonList>
                         <f:Button ID="btnSearch" Pressed="true" ValidateForms="haha"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                        <f:Button ID="btnBack" Pressed="true" ValidateForms="haha"  runat="server" Text="后退" Icon="RewindBlue" OnClick="btnBack_Click"></f:Button>
                        <f:TextBox ID="txtClickColsName" runat="server" Hidden="true" Label="列名" LabelWidth="50px"></f:TextBox>
                        </Items>
                     </f:Toolbar>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:DropDownList Label="供应商" Hidden="true" LabelWidth="70px"  Width="300px" ID="ddlProvider" runat="server"> 
                        </f:DropDownList>
                        <f:TextBox ID="txtProperity" runat="server" Label="性质" LabelWidth="50px"></f:TextBox>
                        <f:TextBox ID="txtProject" runat="server" Label="项目" LabelWidth="50px"></f:TextBox>
                        <f:DatePicker ID="dpPreDeliveryDate" runat="server" Label="日期" LabelWidth="50px" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                        <f:TextBox ID="txtPreBill" runat="server" Label="前置单据"  LabelWidth="80px"></f:TextBox>
                        
                          <f:Button ID="btnExcel" EnableAjax="false" DisableControlBeforePostBack="false" OnClick="Button1_Click" runat="server" Icon="PageExcel"   Hidden="true"  Text="导出Excel">
                            </f:Button> 
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                    </Items>
                </f:Toolbar>
                <f:Toolbar runat="server">
                    <Items>
                         <f:DatePicker ID="dpPlanStart" runat="server" Label="计划开工日期" LabelWidth="120px" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                         <f:DatePicker ID="dpPlanFinish" runat="server" Label="计划完成日期" LabelWidth="120px" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                       <f:DatePicker ID="dpZhuangPei" runat="server" Label="最晚装配日期" LabelWidth="120px" DateFormatString="yyyy-MM-dd"></f:DatePicker>
                    </Items>
                </f:Toolbar>
                <f:Toolbar runat="server">
                    <Items>
                         <f:Button ID="btnNew"   Text="新增" Hidden="true" Icon="Add" EnablePostBack="false" runat="server">
                        </f:Button>
                          <f:Button ID="Button1" Hidden="true" OnClick="btnSave_Click" runat="server" Icon="SystemSave"    Text="保存">
                            </f:Button> 
                          <f:Button ID="btnProduce" Enabled="false"  OnClick="btnProduce_Click" runat="server" Icon="SystemSave"    Text="生成生产计划">
                            </f:Button> 
                          <f:Button ID="btnPurchase"  Enabled="false"  OnClick="btnPurchase_Click" runat="server" Icon="SystemSave"    Text="生成采购计划">
                            </f:Button> 
                        <f:Button ID="btnSendOutPlan"  Enabled="true"  OnClick="btnSendOutPlan_Click" runat="server" Icon="SystemSave"    Text="生成委外计划">
                            </f:Button> 
                        <f:Button ID="btnClientProvider"  Enabled="true"  OnClick="btnClientProvider_Click" runat="server" Icon="SystemSave"    Text="生成客供计划">
                            </f:Button> 
                        <f:Button ID="btnDelete" Hidden="true" OnClick="btnDeleteSelected_Click" Text="删除" Icon="Delete"  runat="server">
                        </f:Button>
                         <f:Button ID="btnSaveData"  OnClick="btnSaveData_Click" runat="server" Icon="SystemSave"    Text="保存">
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
                  <f:RenderField Width="110px" ColumnID="OrderNo" SortField="OrderNo" DataField="OrderNo"
                      HeaderText="订单编号" EnableColumnEdit="false" EnableFilter="false"  EnableLock="true" Locked="true" >
                    <Editor>
                        <f:TextBox ID="txtOrderNo" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
               
                  <f:RenderField Width="100px" ColumnID="ProNo" DataField="ProNo" EnableColumnEdit="false"  EnableLock="true" Locked="true" 
                      HeaderText="产品编号">
                    <Editor>
                        <f:TextBox ID="txtProNo" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                  <f:RenderField Width="150px" ColumnID="ProName" DataField="ProName" EnableColumnEdit="false"  EnableLock="true" Locked="true" 
                      HeaderText="产品名称">
                    <Editor>
                        <f:TextBox ID="txtProName" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 

                  <f:RenderField Width="100px" ColumnID="ItemNo" DataField="ItemNo" EnableColumnEdit="false"  EnableLock="true" Locked="true" 
                      HeaderText="料号">
                    <Editor>
                        <f:TextBox ID="tbxItemNo" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="200px" ColumnID="ItemName" DataField="ItemName" EnableColumnEdit="false"  EnableLock="true" Locked="true" 
                      HeaderText="名称">
                    <Editor>
                        <f:TextBox ID="txtItemName" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="75px" ColumnID="MainFrom" DataField="MainFrom" EnableColumnEdit="false"
                      HeaderText="主要来源">
                    <Editor>
                        <f:TextBox ID="txtMakeMethod" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                  <f:RenderField Width="75px" ColumnID="UsingQuantity"  DataField="UsingQuantity" EnableColumnEdit="false"
                      HeaderText="备货数量" RendererFunction="UsingQuantity">
                    <Editor>
                        <f:NumberBox ID="txtUsingQuantity" MinValue="1" CssStyle="color:red;" Required="false" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>

                  <f:RenderField Width="75px" ColumnID="ConfirmQuantity" DataField="ConfirmQuantity"
                      HeaderText="确认数量" RendererFunction="ConfirmQuantity" EnableColumnEdit="false">
                      
                    <Editor>
                        <f:NumberBox ID="txtConfirmQuantity" AutoPostBack="false"  CssStyle="color:green;"  EnableBlurEvent="true"  OnBlur="txtConfirmQuantity_Blur" MinValue="1" Required="false" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                
                  <f:RenderField Width="110px" ColumnID="RealUsingQuantity" DataField="RealUsingQuantity" EnableColumnEdit="true"
                      HeaderText="生产/采购数量" RendererFunction="RealUsingQuantity">
                    <Editor>
                        <f:NumberBox ID="txtRealUsingQuantity" MinValue="1"  CssStyle="color:blue;" Required="false" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>

                <f:RenderField Width="80px" ColumnID="IsPlan" DataField="IsPlan" EnableColumnEdit="true"
                      HeaderText="状态" RendererFunction="renderState" >
                    <Editor>
                        <f:DropDownList Required="false" runat="server" ID="txtIsConfirm">
                            <f:ListItem Text="未生成" Value="0" />
                            <f:ListItem Text="已生成" Value="1" />
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
                 
                 <f:RenderField Width="100px" ColumnID="ReceiveDept" DataField="ReceiveDept" EnableColumnEdit="false"
                      HeaderText="接收部门">
                    <Editor>
                        <f:TextBox ID="txtReceiveDept" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="90px" ColumnID="Receiver" DataField="ChineseName" EnableColumnEdit="false"
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
            <f:BoundField DataField="BomSN" HeaderText="BomSN" ColumnID="BomSN"></f:BoundField>
                 <f:RenderField Width="100px" ColumnID="SupplierId" DataField="SupplierId"
                      HeaderText="供应商" RendererFunction="renderProvider"  Hidden="true" >
                     
                    <Editor>
                         <f:DropDownList Required="false" runat="server" EnableEdit="true"  ID="ddlSupplierId">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
               <f:LinkButtonField Hidden="true" ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                 
               
            </Columns>
        </f:Grid>
       
    </form>
    <script>
        var provierjson = eval('<%=pstr%>');
        var gridClientID = '<%= Grid1.ClientID %>';
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
                return '<span style="color:red;">未生成</span>';
            else
                return '<span style="color:green;">已生成</span>';

        }

        function renderProvider(value) {
            if (value == '' || value == undefined) {
                return '';
            }
            else {
                if (provierjson[value] == undefined) {
                    return '';
                }
                return '<span style="color:red;">' + provierjson[value] + '</span>';
            }

        }
    </script>
     <script type="text/javascript">
         var gridClientID = '<%= Grid1.ClientID %>';
         var inputselector = '.f-grid-tpl input';
         function registerEnterEvent() {
             var grid = F(gridClientID);

             grid.el.on('keydown', inputselector, function (event) {

                 // 如果是 ENTER键 或者 TAB键
                 if (event.keyCode === F.KEY.ENTER || event.keyCode === F.KEY.TAB) {
                     // 当前选中的行数组
                     var nextRow = $(this).parents('.f-grid-row').next();
                     if (nextRow.length) {
                         // 选中文本框中的文本
                         nextRow.find(inputselector).select();
                         // 选中下一行
                         grid.selectRow(nextRow);
                     }
                 }
             });

             // 点击选中文本框中的文本
             grid.el.on('click', inputselector, function (event) {
                 $(this).select();
             });

         }

         F.ready(function () {
             registerEnterEvent();
         });

    </script>
</body>
</html>