<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magCPOrderDetail.aspx.cs" Inherits="AppBoxPro.ClientProviderMag.magCPOrderDetail" %>

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
               
                  <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true"
                    DataKeyNames="SN" AllowSorting="true"    SortField="SN" OnSort="Grid2_Sort"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true"   OnRowCommand="Grid2_RowCommand" OnPreDataBound="Grid2_PreDataBound" OnPreRowDataBound="Grid2_PreRowDataBound"  OnRowDoubleClick="Grid2_RowDoubleClick" EnableRowDoubleClickEvent="true">
                    <Toolbars>
                        <f:Toolbar ID="dddd" runat="server">
                            <Items>
                                <f:Panel runat="server" ShowBorder="false" ShowHeader="false" IsFluid="true">
                                    <Content>
                                        <table  style="width: 100%;">
                <tr>
                    <td style="width:5%;">计划号：</td>
                    <td style="font-weight: normal;width:7%;"><%=cplan %></td>
                    <td style="width:6%;border:0px solid #000;">客供单号：</td>
                    <td style="font-weight: normal;width:5%;"><%=cporderno %></td>
                    <td style="width:5%;">订单号：</td>
                    <td style="font-weight: normal;width:7%;"><%=saleorderno %></td>
                    <td style="width:7%;">下单日期：</td>
                    <td style="font-weight: normal;width:7%;"><%=orderdate %></td>
                    <td style="width:5%;">供应商：</td>
                    <td style="font-weight: normal;width:7%;"><%=provider %></td>
                    <td style="width:5%;">联系人：</td>
                    <td style="font-weight: normal;width:7%;"><%=contactman %></td>
                    <td style="width:5%;">电话：</td>
                    <td style="font-weight: normal;width:7%;"><%=tel %></td>
                    <td style="width:5%;">传真：</td>
                    <td style="font-weight: normal;"><%=fax %></td>
                </tr>
                                            </table>
                                    </Content>
                                </f:Panel>
                            </Items>
                        </f:Toolbar>
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
                        <f:BoundField  DataField="CPPlanNo" SortField="CPPlanNo" Hidden="true" Width="130px" HeaderText="客供计划单号" />
                        <f:BoundField DataField="CPOrderNo" SortField="CPOrderNo" Hidden="true" Width="150px" HeaderText="客供单号" />
                        <f:BoundField DataField="SaleOrderNo" SortField="SaleOrderNo" Hidden="true" Width="130px" HeaderText="销售单号" />
                        <f:BoundField ColumnID="ProNo"  DataField="ProNo" SortField="ProNo" Width="120px" HeaderText="品号" />
                        <f:BoundField ColumnID="ProName"  DataField="ProName" SortField="ProName" Width="200px" HeaderText="品名" />
                        <f:BoundField ColumnID="ItemNo"  DataField="ItemNo" SortField="ItemNo" Width="120px" HeaderText="料号" />
                        <f:BoundField ColumnID="ItemName"  DataField="ItemName" SortField="ItemName" Width="200px" HeaderText="名称" />
                        <f:BoundField ColumnID="Spec"  DataField="Spec" SortField="Spec" ExpandUnusedSpace="true" MinWidth="50px" HeaderText="规格" />
                        <f:BoundField ColumnID="Quantity"  DataField="Quantity" SortField="Quantity" Width="90px" HeaderText="采购数量" />
                        <f:BoundField ColumnID="Unit"  DataField="Unit"  Width="90px" HeaderText="单位" />
                        <f:BoundField ColumnID="Remark"  DataField="Remark"  Width="90px" HeaderText="备注" />
                        <f:BoundField DataField="BomSN"  Width="90px" HeaderText="BomSN" />
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


