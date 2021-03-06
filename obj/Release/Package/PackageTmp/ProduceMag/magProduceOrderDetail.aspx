﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magProduceOrderDetail.aspx.cs" Inherits="AppBoxPro.ProduceMag.magProduceOrderDetail" %>

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
                  <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true"
                    DataKeyNames="SN" AllowSorting="true"    SortField="SN" OnSort="Grid2_Sort"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid2_RowDoubleClick" OnRowCommand="Grid2_RowCommand" OnPreDataBound="Grid2_PreDataBound" OnPreRowDataBound="Grid2_PreRowDataBound">
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
                                         <f:Button ID="btnBack" Pressed="true" ValidateForms="haha"  runat="server" Text="后退" Icon="RewindBlue" OnClick="btnBack_Click"></f:Button>
                        <f:TextBox ID="txtClickColsName" runat="server" Hidden="true" Label="列名" LabelWidth="50px"></f:TextBox>
                                  <f:Button  ID="btnReset"  Icon="Cancel" EnablePostBack="false" runat="server"
                        Text="清空">
                    </f:Button>
                                         <f:NumberBox runat="server" ID="numLabCount2" Width="200px" MinValue="1" Text="1" Label="标签份数"></f:NumberBox>
                               <f:Button ID="btnSelPrintLab"   OnClick="btnSelPrintLab_Click" ValidateMessageBox="false"  ValidateForms="SimpleForm1"  runat="server" Icon="Disk"  Text="打印选中记录标签">
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
                        <f:DropDownList ID="ddlGridPageSize2" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize2_SelectedIndexChanged"
                            runat="server">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:RowNumberField Width="30px" EnablePagingNumber="true" />
                        <f:BoundField Hidden="true" DataField="ProPlanNo" SortField="ProPlanNo" Width="130px" HeaderText="生产计划单号" />
                        <f:BoundField Hidden="true"  DataField="ProOrderNo" SortField="ProOrderNo" Width="150px" HeaderText="生产单号" />
                        <f:BoundField Hidden="true"  DataField="SaleOrderNo" SortField="SaleOrderNo" Width="140px" HeaderText="销售单号" />
                        <f:BoundField ColumnID="ProNo" DataField="ProNo" SortField="ProNo" Width="110px" HeaderText="品号" />
                        <f:BoundField ColumnID="ProName"  DataField="ProName" SortField="ProName" Width="100px" HeaderText="品名" />
                        <f:BoundField ColumnID="ItemNo"  DataField="ItemNo" SortField="ItemNo" Width="120px" HeaderText="料号" />
                        <f:BoundField ColumnID="ItemName"  DataField="ItemName" SortField="ItemName" Width="200px" HeaderText="名称" />
                        <f:BoundField ColumnID="Spec"  DataField="Spec" SortField="Spec" ExpandUnusedSpace="true" HeaderText="规格" />
                        <f:BoundField ColumnID="Quantity"  DataField="Quantity" SortField="Quantity" Width="90px" HeaderText="生产数量" />
                        <f:BoundField ColumnID="WorkShop"  DataField="WorkShop"   Width="110px" HeaderText="生产车间" />
                        <f:BoundField DataField="BarCode"   Width="150px" HeaderText="条码" />
                        <f:LinkButtonField ColumnID="deleteField" Hidden="true" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
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