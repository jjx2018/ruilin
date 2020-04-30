﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CPPlan.aspx.cs" Inherits="AppBoxPro.ClientProviderMag.CPPlan" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch"
            BoxConfigPosition="Start" ShowHeader="false" Title="供应商管理">
            <Items>
                
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" DataKeyNames="SN" AllowSorting="true"
                    OnSort="Grid1_Sort" SortField="SN" SortDirection="DESC" AllowPaging="true"
                    IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound" OnRowCommand="Grid1_RowCommand"
                    OnPageIndexChange="Grid1_PageIndexChange" AllowCellEditing="true" ClicksToEdit="2" OnRowDoubleClick="Grid1_RowDoubleClick" EnableRowDoubleClickEvent="true" OnRowDataBound="Grid1_RowDataBound">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items> 
                                 <f:TextBox ID="txtContactMan" Label="联系人" LabelWidth="80px"  Width="210px"  Required="true" runat="server">
                        </f:TextBox>
                                 <f:TextBox ID="txtTel" Label="电话" LabelWidth="50px"  Width="180px"  Required="true" runat="server">
                        </f:TextBox>
                                 <f:TextBox ID="txtFax" Label="传真" LabelWidth="50px"  Width="180px"  Required="true" runat="server">
                        </f:TextBox> 
                                 <f:DatePicker ID="XDDate" DateFormatString="yyyy-MM-dd" LabelWidth="80px"  Width="210px"    runat="server" Label="下单日期" ></f:DatePicker>
                                <f:DatePicker ID="JHdate" DateFormatString="yyyy-MM-dd" LabelWidth="80px"  Width="210px"    runat="server" Label="交货日期"   ></f:DatePicker>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                 <f:Button ID="btnExcel" EnableAjax="false" DisableControlBeforePostBack="false"  runat="server" Icon="PageExcel"  OnClick="Button1_Click"  Text="导出Excel">
                            </f:Button>
                                
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar runat="server" Hidden="true">
                            <Items>
                                
                                <f:DropDownBox runat="server" ID="ddlProvider"  Label="供应商"  EmptyText="请从下拉表格中选择" MatchFieldWidth="false" Values="105" 
                    EnableMultiSelect="false" EnableEdit="true"  AutoPostBack="true"   LabelWidth="80px"  Width="300px"   OnTextChanged="DropDownBox1_TextChanged">
                    <PopPanel>
                        <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="SN" DataTextField="Name"
                            DataKeyNames="SN" AllowSorting="true" OnRowClick="Grid2_RowClick" EnableRowClickEvent="true" SortDirection="ASC" SortField="SN" Hidden="true" Width="550px" Height="300px" EnableMultiSelect="false">
                            <Columns>
                               <f:BoundField DataField="ProviderNo" SortField="ProviderNo" Width="100px" HeaderText="供应商编号" />
                        <f:BoundField DataField="subjectcode" SortField="subjectcode" Width="100px" HeaderText="供应商代号" />
                        <f:BoundField DataField="Name" SortField="Name" Width="100px" HeaderText="供应商名称" />
                        <f:BoundField DataField="Address"  HeaderText="地址" />
                        <f:BoundField DataField="Telephone"   HeaderText="电话" />
                            </Columns>
                        </f:Grid>
                    </PopPanel>
                </f:DropDownBox>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar runat="server">
                            <Items>
                                 <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="请输入料号或名称或规格"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click" Width="300px">
                                </f:TwinTriggerBox>
                                  <f:RadioButtonList ID="rbtIsState" runat="server" Label="状态" LabelWidth="50px" ColumnWidth="80px" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="rbtIsState_SelectedIndexChanged">
                             <f:RadioItem Text="全部" Value="" />
                             <f:RadioItem Text="未完成" Value="0"  Selected="true"/>
                             <f:RadioItem Text="已完成" Value="1"  />
                         </f:RadioButtonList>
                                 <f:Button ID="btnSearch" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                                <f:Button ID="btnBack" Pressed="true" ValidateForms="haha"  runat="server" Text="后退" Icon="RewindBlue" OnClick="btnBack_Click"></f:Button>
                        <f:TextBox ID="txtClickColsName" runat="server" Hidden="true" Label="列名" LabelWidth="50px"></f:TextBox>
                                  <f:Button ID="btnCPSave" runat="server" OnClick="btnCPSave_Click" Icon="Add"   Text="生成客供单">
                                </f:Button>
                                 
                                
                                 <f:Button ID="btnDeleteSelected" ConfirmText="确定删除选中的记录？" ConfirmTarget="Top" Icon="Delete" OnClick="btnDeleteSelected_Click" runat="server" Text="删除选中记录" >
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
                      <f:BoundField ColumnID="SaleOrderNo" DataField="SaleOrderNo" SortField="SaleOrderNo" Width="130px" HeaderText="销售单号" />
                       <f:RenderField Width="150px" ColumnID="ProviderName" EnableColumnEdit="false"  DataField="ProviderName"
                      HeaderText="供应商">
                    <Editor>
                        
                         <f:DropDownList Required="false" runat="server" ID="ddlSupplierId">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>

                        <f:BoundField ColumnID="CPPlanNo" DataField="CPPlanNo" SortField="CPPlanNo" Width="130px" HeaderText="客供计划号" />
                        <f:BoundField ColumnID="ProNo" DataField="ProNo" SortField="ProNo" Width="130px" HeaderText="品号" />
                        <f:BoundField ColumnID="ProName" DataField="ProName" SortField="ProName" Width="150px" HeaderText="品名" />
                        <f:BoundField ColumnID="ItemNo" DataField="ItemNo" SortField="ItemNo" Width="110px" HeaderText="料号" />
                        <f:BoundField ColumnID="ItemName" DataField="ItemName" SortField="ItemName" Width="150px" HeaderText="名称" />
                        <f:BoundField ColumnID="Spec" DataField="Spec" SortField="Spec" Width="300px" ExpandUnusedSpace="true" HeaderText="规格" />
                        <f:BoundField ColumnID="Quantity" DataField="Quantity" SortField="Quantity" Width="100px" HeaderText="客供数量" />
                         <f:BoundField ColumnID="Provider"  DataField="Provider"  Hidden="true"   Width="100px" HeaderText="Provider" />
                         <f:BoundField ColumnID="PlanStartDate" DataField="PlanStartDate" SortField="PlanStartDate" DataFormatString="{0:yyyy-MM-dd}" Width="150px" HeaderText="计划开工时间" />
                         <f:BoundField ColumnID="PlanFinishDate" DataField="PlanFinishDate" SortField="PlanFinishDate" DataFormatString="{0:yyyy-MM-dd}" Width="150px" HeaderText="计划完成时间" />
                         <f:BoundField ColumnID="ZhuangPeiDate" DataField="ZhuangPeiDate" SortField="ZhuangPeiDate" DataFormatString="{0:yyyy-MM-dd}" Width="150px" HeaderText="装配时间" />
                        <f:WindowField ColumnID="editField" TextAlign="Center" Icon="Pencil" ToolTip="编辑" WindowID="Window1"
                            Title="编辑" DataIFrameUrlFields="SN" Hidden="true" DataIFrameUrlFormatString="~/ProviderMag/Provideredit.aspx?id={0}"
                            Width="50px" />
                        <f:LinkButtonField ColumnID="deleteField" Hidden="true" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                         <f:BoundField ColumnID="IsValid" DataField="IsValid"  Hidden="true"  Width="100px" HeaderText="IsValid" />
                         <f:BoundField ColumnID="State" DataField="State"  Hidden="true"  Width="100px" HeaderText="State" />
                        <f:BoundField ColumnID="ISN" DataField="ISN"  Hidden="true"  Width="100px" HeaderText="ISN" />
                    </Columns>
                </f:Grid>
               
            </Items>
        </f:Panel>
        <f:Window ID="Window1" CloseAction="Hide" runat="server" IsModal="true" Hidden="true" Target="Top"
            EnableResize="true" EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank"
            Width="900px" Height="550px" OnClose="Window1_Close">
        </f:Window>





    </form>
</body>
</html>

<script type="text/javascript">

    function onGridAfterEdit(event, value, params) {
        var me = this, columnId = params.columnId, rowId = params.rowId;
        //if (columnId === 'ChineseScore' || columnId === 'MathScore') {
        var UsingQuantity = me.getCellValue(rowId, 'UsingQuantity');
        var ConfirmQuantity = me.getCellValue(rowId, 'ConfirmQuantity');

        me.updateCellValue(rowId, 'RealUsingQuantity', parseInt(UsingQuantity) - parseInt(ConfirmQuantity));
        me.updateCellValue(rowId, 'IsConfirm', 1);

        //}
    }
</script>