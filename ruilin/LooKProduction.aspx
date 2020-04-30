<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LooKProduction.aspx.cs" Inherits="AppBoxPro.ruilin.LooKProduction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
                    AllowPaging="true" IsDatabasePaging="true" EnableRowSelectEvent="true" OnPageIndexChange="Grid1_PageIndexChange" >
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                
                                <f:TwinTriggerBox ID="TwinTriggerBox1" runat="server" ShowLabel="false" EmptyText="请输入订单编号或型号或名称或客人编号"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage1_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage1_Trigger1Click" Width="450px">
                                </f:TwinTriggerBox>
                                
                                <f:Button ID="Button2" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                              
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
                        <f:BoundField DataField="OrderNo" TextAlign="Center" SortField="OrderNo" Width="150px" HeaderText="订单编号" />
                        <f:BoundField DataField="ProNo" TextAlign="Center"   Width="300px" HeaderText="型号" />
                        <f:BoundField DataField="ProName" TextAlign="Center" SortField="ItemNo"  ExpandUnusedSpace="true"  Width="120px" HeaderText="产品名称" />
                           
                        
                    </Columns>
                </f:Grid>





                    </Items>
                </f:Panel>





        
       
       
       
       
       
       <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server" 
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="1000px"
            Height="720px">
        </f:Window>
           <f:Window ID="Window2" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="600px"
            Height="320px">
        </f:Window>
            <f:Window ID="Window3" Title="选择BOM" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="600px"
            Height="320px">
        </f:Window>
    </form>

</body>