<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magCPOrderHeader.aspx.cs" Inherits="AppBoxPro.ClientProviderMag.magCPOrderHeader" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel5" EnableFStateValidation="false" runat="server" />
       <f:Panel ID="Panel5" CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            Layout="VBox" AutoScroll="true" Margin="0px" ShowHeader="false" Title=""
            BoxConfigChildMargin="0 0 0 0" BodyPadding="0">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true"  EnableMultiSelect="false"
                    DataKeyNames="SN" AllowSorting="true" OnSort="Grid1_Sort"  SortField="SN"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"  EnableTextSelection="true" KeepCurrentSelection="false"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange"  EnableRowClickEvent="true" >
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
                                        <f:Button ID="btnBack" Pressed="true" ValidateForms="haha"  runat="server" Text="后退" Icon="RewindBlue" OnClick="btnBack_Click"></f:Button>
                        <f:TextBox ID="txtClickColsName" runat="server" Hidden="true" Label="列名" LabelWidth="50px"></f:TextBox>
                                         <f:Button  ID="btnClear"  Icon="Cancel" EnablePostBack="false" runat="server"
                        Text="清空">
                    </f:Button>
                                
                                
                               
                                 
                                 </Items>
                                </f:SimpleForm>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="Button4" EnableAjax="false" DisableControlBeforePostBack="false"  runat="server" Icon="PageExcel"  OnClick="Button1_Click"  Text="导出Excel">
                            </f:Button>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar runat="server">
                            <Items>
                                 <f:Button ID="btnPrint"   OnClick="btnPrint_Click" ValidateMessageBox="false"  ValidateForms="SimpleForm1"  runat="server" Icon="Disk"  Text="打印客供单">
                                </f:Button>
                                <f:Button ID="btnPrintLab"   OnClick="btnPrintLab_Click" ValidateMessageBox="false"  ValidateForms="SimpleForm1"  runat="server" Icon="Disk"  Text="打印客供单标签">
                                </f:Button>
                                  
                                <f:Button ID="btnDeleteSelected" Hidden="true" ConfirmText="确定删除选中的记录？" ConfirmTarget="Top" Icon="Delete" OnClick="btnDeleteSelected_Click" runat="server" Text="删除选中记录" >
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
                        <f:TemplateField HeaderText="查看明细" Width="100px" ToolTip="查看明细">
                    <ItemTemplate>
                        <a href="javascript:;" style="color:blue;text-decoration:underline;" onclick="lookOrder('<%#Eval("SN")%>','<%#Eval("CPOrderNo")%>')">查看明细</a>
                    </ItemTemplate>
                </f:TemplateField>
                        <f:BoundField ColumnID="CPPlanNo" DataField="CPPlanNo" SortField="CPPlanNo" Width="150px" HeaderText="客供计划单号" />
                        <f:BoundField ColumnID="CPOrderNo"  DataField="CPOrderNo" SortField="CPOrderNo" Width="150px" HeaderText="客供单号" />
                        <f:BoundField ColumnID="SaleOrderNo"  DataField="SaleOrderNo" SortField="SaleOrderNo" Width="150px" HeaderText="销售单号" />
                        <f:BoundField ColumnID="CPDate"  DataField="CPDate" SortField="CPDate" Width="150px" DataFormatString="{0:yyyy-MM-dd}"  HeaderText="下单日期" />
                        <f:BoundField ColumnID="Provider"  DataField="Provider"  ExpandUnusedSpace="true" Width="200px" HeaderText="供应商" />
                        <f:BoundField ColumnID="JBRName"  DataField="JBRName"   Width="120px" HeaderText="经办人" />
                        <f:BoundField ColumnID="ContactMan"  DataField="ContactMan"   Width="90px" HeaderText="联系人" />
                        <f:BoundField ColumnID="Tel"  DataField="Tel"   Width="90px" HeaderText="电话" />
                        <f:BoundField ColumnID="Fax"  DataField="Fax"   Width="150px" HeaderText="传真" />
                        
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
<script type="text/javascript">
        function lookOrder(ID, orderno) {
            parent.addExampleTab({
                id: 'magCPOrderDetail_'+ID + '_tab',
                iframeUrl: 'ClientProviderMag/magCPOrderDetail.aspx?pid=' + escape(ID) + '&od=' + escape(orderno),
                title: orderno+'-明细',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });
    }

    </script>