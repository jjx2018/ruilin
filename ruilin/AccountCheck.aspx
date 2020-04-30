<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountCheck.aspx.cs" Inherits="AppBoxPro.ruilin.AccountCheck" %>

<!DOCTYPE html> 
<html>
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel5" EnableFStateValidation="false" runat="server" />
       <f:Panel ID="Panel5" CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            Layout="VBox" AutoScroll="true" Margin="0px" ShowHeader="false" Title="1111111111111"
            BoxConfigChildMargin="0 0 0 0" BodyPadding="0">
            <Items>
                <f:Panel ID="Panel3" Title="面板1" Height="290px" runat="server"
                    BodyPadding="0px" ShowBorder="false" ShowHeader="false" Layout="VBox">
                    <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true"  EnableMultiSelect="false"
                    DataKeyNames="SN" AllowSorting="true" OnSort="Grid1_Sort"  SortField="SN"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"  EnableTextSelection="true" KeepCurrentSelection="false"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange"  EnableRowClickEvent="true" OnRowClick="Grid1_OnRowClick">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                
                                <f:SimpleForm ID="SF1" runat="server" Width="100%" ShowBorder="false" ShowHeader="false" LabelWidth="100px" Layout="HBox" BoxConfigChildMargin="0 5 0 5px">
                                    <Items>
                                
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="请输入料号或名称或规格"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click" Width="300px">
                                </f:TwinTriggerBox>
                                         <f:DatePicker ID="datePickerFrom" DateFormatString="yyyy-MM-dd" LabelWidth="80px"  Width="200px"    runat="server" Label="接单开始"></f:DatePicker>
                                <f:DatePicker ID="datePickerTo" DateFormatString="yyyy-MM-dd" LabelWidth="80px"  Width="200px"    runat="server" Label="接单结束"  CompareControl="datePickerFrom" CompareOperator="GreaterThan" CompareMessage="结束日期应大于开始日期"></f:DatePicker>
                                 <f:Button ID="btnSearch" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                                  
                                
                                 <f:Button  ID="btnClear"  Icon="Cancel" EnablePostBack="false" runat="server"
                        Text="清空">
                    </f:Button>
                               
                                
                                         <f:Button ID="btnCheck"  OnClick="btnCheck_Click" runat="server" Icon="SystemSave"    Text="审核通过">
                            </f:Button> 
                                 </Items>
                                </f:SimpleForm>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                 <f:Button ID="btnExcel" EnableAjax="false" DisableControlBeforePostBack="false"  runat="server" Icon="PageExcel"    Text="导出Excel">
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
                       <f:RowNumberField Width="30px" EnablePagingNumber="true" />
                        <f:BoundField DataField="PurPlanNo" SortField="PurPlanNo" Width="150px" HeaderText="采购计划单号" />
                        <f:BoundField DataField="PurOrderNo" SortField="PurOrderNo" Width="150px" HeaderText="采购单号" />
                        <f:BoundField DataField="SaleOrderNo" SortField="SaleOrderNo" Width="150px" HeaderText="销售单号" />
                        <f:BoundField DataField="PurDate" SortField="PurDate" Width="150px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="下单日期" />
                        <f:RenderField Width="120px" ColumnID="CheckState" DataField="State" TextAlign="Center"
                      HeaderText="审核状态" RendererFunction="checkState">
                    <Editor>
                        <f:TextBox ID="txtCheck" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                        <f:BoundField DataField="Provider"  Width="200px" HeaderText="供应商" />
                        <f:BoundField DataField="JBRName"   Width="90px" HeaderText="经办人" />
                        <f:BoundField DataField="ContactMan"   Width="90px" HeaderText="联系人" />
                        <f:BoundField DataField="Tel"   Width="90px" HeaderText="电话" />
                        <f:BoundField DataField="Fax"   Width="90px" HeaderText="传真" />
                        
                        <f:LinkButtonField ColumnID="deleteField" Hidden="true" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />

                        
                    </Columns>
                </f:Grid>



                    </Items>
                </f:Panel>
                <f:Panel ID="Panel4" Title="产品信息" BoxFlex="1" MinHeight="270px" Margin="0"
                    runat="server" BodyPadding="1px 0px 0px 0px" ShowBorder="false" ShowHeader="true" Layout="VBox">
                    <Items>
                  <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true"
                    DataKeyNames="SN" AllowSorting="true"  AllowCellEditing="true" ClicksToEdit="2"    SortField="SN" OnSort="Grid2_Sort"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true"   OnRowCommand="Grid2_RowCommand" OnPreDataBound="Grid2_PreDataBound"  OnPreRowDataBound="Grid2_PreRowDataBound">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                 <f:SimpleForm ID="SF2" runat="server" Width="100%" ShowBorder="false" ShowHeader="false" LabelWidth="100px" Layout="HBox" BoxConfigChildMargin="0 5 0 5px">
                                    <Items>
                                 <f:TwinTriggerBox ID="TwinTriggerBox2" Width="300px" runat="server" ShowLabel="false" EmptyText="可输入型号、品名搜索"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage2_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage2_Trigger1Click">
                                </f:TwinTriggerBox>
                               
                                <f:DatePicker ID="DateFrom" DateFormatString="yyyy-MM-dd" LabelWidth="80px"  Width="200px"    runat="server" Label="录单开始" AutoPostBack="true" OnTextChanged="OnDateFrom_Changed"></f:DatePicker>
                                <f:DatePicker ID="DateTo" DateFormatString="yyyy-MM-dd"    LabelWidth="80px"  Width="200px"     runat="server" Label="录单结束"  CompareControl="DateFrom" CompareOperator="GreaterThan" CompareMessage="结束日期应大于开始日期" AutoPostBack="true" OnTextChanged="OnDateTo_Changed"></f:DatePicker>
                                <f:Button ID="Button2" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch2_click"></f:Button>
                                  <f:Button  ID="btnReset"  Icon="Cancel" EnablePostBack="false" runat="server"
                        Text="清空">
                    </f:Button>
                              <f:Button ID="btnSave"  OnClick="btnSave_Click" runat="server" Icon="SystemSave"    Text="保存数据">
                            </f:Button> 
                                        
                            </Items>   
                               </f:SimpleForm>  
                                 
                            </Items>
                        </f:Toolbar>
                       
                        
                    </Toolbars>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText2" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList ID="DropDownList1" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
                            runat="server">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:RowNumberField Width="30px" EnablePagingNumber="true" />
                        <f:BoundField DataField="PurPlanNo" SortField="PurPlanNo" Width="150px" Hidden="true" HeaderText="采购计划单号" />
                        <f:BoundField DataField="PurOrderNo" SortField="PurOrderNo" Width="150px" Hidden="true" HeaderText="采购单号" />
                        <f:BoundField DataField="SaleOrderNo" SortField="SaleOrderNo" Width="150px" Hidden="true" HeaderText="销售单号" />
                        <f:BoundField DataField="ProNo" SortField="ProNo" Width="150px" HeaderText="品号" />
                        <f:BoundField DataField="ProName" SortField="ProName" Width="200px" HeaderText="品名" />
                        <f:BoundField DataField="ItemNo" SortField="ItemNo" Width="150px" HeaderText="料号" />
                        <f:BoundField DataField="ItemName" SortField="ItemName" Width="200px" HeaderText="名称" />
                        <f:BoundField DataField="Spec" SortField="Spec" Width="300px" HeaderText="规格" />
                           
                         <f:TemplateField HeaderText="单价" Width="100px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPrice" runat="server" Width="80px"
                            Text='<%# Eval("Price") %>'></asp:TextBox>
                    </ItemTemplate>
                </f:TemplateField>
                        <f:BoundField DataField="Quantity" SortField="Quantity" Width="90px" HeaderText="采购数量" />
                        <f:BoundField DataField="Unit"  Width="90px" HeaderText="单位" />
                        <f:BoundField DataField="Remark"  Width="90px" HeaderText="备注" />
                        
                        <f:LinkButtonField ColumnID="deleteField" Hidden="true" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                    </Columns>
                </f:Grid>


                    </Items>
                </f:Panel>
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
 <script type="text/javascript">
     var gridClientID = '<%= Grid2.ClientID %>';
     var inputselector = '.f-grid-tpl input';
     function checkState(value) {
         return value == 0 ? "<span style='color:red;'>未审核</span>" : "<span style='color:blue;'>已审核</span>";
     }
     function renderGender(value) {
         return value == 0 ?  '未审核':'已审核';
     }
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