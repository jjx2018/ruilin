<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendOutPlanSearch.aspx.cs" Inherits="AppBoxPro.ruilin.SendOutPlanSearch" %>

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
            BoxConfigPosition="Start" ShowHeader="false" Title="">
            <Items>
                
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" DataKeyNames="SN" AllowSorting="true"
                    OnSort="Grid1_Sort" SortField="SN" SortDirection="DESC" AllowPaging="true"
                    IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound" OnRowCommand="Grid1_RowCommand"
                    OnPageIndexChange="Grid1_PageIndexChange"  KeepCurrentSelection="true" >
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                  <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="请输入料号或名称或规格"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click" Width="300px">
                                </f:TwinTriggerBox>
                                 <f:Button ID="btnSearch" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>

                                 <f:Button ID="btnProSave" runat="server" OnClick="btnProSave_Click" Icon="Add"   Text="生成委外单">
                                </f:Button>
                                <f:Button ID="Button1" Hidden="true" runat="server" Icon="Add" EnablePostBack="false" Text="新增">
                                </f:Button>
                                 
                                <f:Button ID="btnDeleteSelected" ConfirmText="确定删除选中的记录？" ConfirmTarget="Top" Icon="Delete" OnClick="btnDeleteSelected_Click" runat="server" Text="删除选中记录" >
                            </f:Button>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnExcel" EnableAjax="false" DisableControlBeforePostBack="false"  runat="server" Icon="PageExcel"  OnClick="Button1_Click"  Text="导出Excel">
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
                        <f:BoundField DataField="SendOutPlanNo" SortField="ProPlanNo" Width="150px" HeaderText="委外计划号" />
                        <f:BoundField DataField="Provider"   Width="150px" HeaderText="委外厂家" />
                        <f:BoundField DataField="ProviderID" Hidden="true" Width="150px" HeaderText="委外厂家" />
                        <f:BoundField DataField="SaleOrderNo" SortField="SaleOrderNo" Width="150px" HeaderText="销售单号" />
                        <f:BoundField DataField="ProNo" SortField="ProNo" Width="130px" HeaderText="品号" />
                        <f:BoundField DataField="ProName" SortField="ProName" Width="120px" HeaderText="品名" />
                        <f:BoundField DataField="ItemNo" SortField="ItemNo" Width="150px" HeaderText="料号" />
                        <f:BoundField DataField="ItemName" SortField="ItemName" Width="120px" HeaderText="名称" />
                        <f:BoundField DataField="Spec" SortField="Spec" MinWidth="50px" ExpandUnusedSpace="true" HeaderText="规格" />
                        <f:BoundField DataField="Quantity" SortField="Quantity" Width="100px" HeaderText="委外数量" />
                        <f:BoundField DataField="WorkShop" SortField="WorkShop" Width="120px" HeaderText="所属车间" />
                        <f:WindowField ColumnID="editField" TextAlign="Center" Icon="Pencil" ToolTip="编辑" WindowID="Window1"
                            Title="编辑" DataIFrameUrlFields="SN" Hidden="true" DataIFrameUrlFormatString="~/ruilin/Provideredit.aspx?id={0}"
                            Width="50px" />
                        <f:LinkButtonField ColumnID="deleteField" Hidden="true" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
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