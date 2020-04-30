<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magBom.aspx.cs" Inherits="AppBoxPro.ruilin.magBom" %>

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
                <f:Panel ID="Panel3" Title="面板1" Height="290px" runat="server"
                    BodyPadding="0px" ShowBorder="false" ShowHeader="false" Layout="VBox">
                    <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true" 
                    DataKeyNames="SN" AllowSorting="true" OnSort="Grid1_Sort"  SortField="SN"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange" EnableRowClickEvent="true" OnRowClick="Grid1_OnRowClick">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="请输入产品编号"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                              
                                <f:DatePicker ID="datePickerFrom" DateFormatString="yyyy-MM-dd" LabelWidth="60px"  Width="200px"    runat="server" Label="开始"></f:DatePicker>
                                <f:DatePicker ID="datePickerTo" DateFormatString="yyyy-MM-dd" LabelWidth="60px"  Width="200px"    runat="server" Label="结束"  CompareControl="datePickerFrom" CompareOperator="GreaterThan" CompareMessage="结束日期应大于开始日期"></f:DatePicker>
                                 <f:Button ID="btnSearch" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                                 <f:Button OnClientClick="formReset()" ID="btnClear"  Icon="Cancel" EnablePostBack="false" runat="server"
                        Text="清空">
                    </f:Button>
                                <f:ToolbarSeparator runat="server">
                                </f:ToolbarSeparator>
                               
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                 <f:Button ID="btnExcel" EnableAjax="false" DisableControlBeforePostBack="false"  runat="server" Icon="PageExcel"  OnClick="Button1_Click"    Text="导出Excel">
                            </f:Button>
                                 
                                
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar runat="server">
                            <Items>
                                 <f:FileUpload runat="server" Width="380px" LabelWidth="60px" ID="filePhoto" EmptyText="请选择Excel模板" Label="模板" Required="true" ButtonIcon="Add"
                    ShowRedStar="true">
                </f:FileUpload>
                        <f:Button ID="btnImport" OnClick="btnImport_Click" Icon="SystemSave" Text="导入Excel" EnablePostBack="true" runat="server">
                        </f:Button>
                                 <f:HyperLink runat="server" NavigateUrl="../model/BOM.xlsx" Text="下载BOM模板"></f:HyperLink>
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
                        <f:RowNumberField Width="35px" EnablePagingNumber="true" HeaderText="序号" />
                        <f:BoundField DataField="ProNo" SortField="ProNo"  Width="150px" HeaderText="产品编号" />
                        <f:BoundField DataField="ProName" SortField="ProName"  Width="200px" HeaderText="产品名称" />
                        <f:BoundField DataField="MyNo" SortField="MyNo"  Width="200px" HeaderText="瑞麟编号" />
                        <f:BoundField DataField="ClientNo" SortField="ClientNo"  Width="200px" HeaderText="客户编号" />
                        <f:BoundField DataField="ClientCode" SortField="ClientCode"  Width="200px" HeaderText="客户代号" />
                        <f:BoundField DataField="BomDate" SortField="BomDate" DataFormatString="{0:yyyy-MM-dd}"  Width="200px" HeaderText="日期" />
                        <f:BoundField DataField="Ver"   Width="200px" HeaderText="版本" />
                         <f:BoundField DataField="Inputer"   Width="200px" HeaderText="录入人" />
                         <f:BoundField DataField="InputeDate" SortField="InputeDate" DataFormatString="{0:yyyy-MM-dd}"  Width="200px" HeaderText="录入日期" />
                        
                    </Columns>
                </f:Grid>



                    </Items>
                </f:Panel>
                <f:Panel ID="Panel4" Title="面板2" BoxFlex="1" MinHeight="270px" Margin="0"
                    runat="server" BodyPadding="1px 0px 0px 0px" ShowBorder="false" ShowHeader="false" Layout="VBox">
                    <Items>
                  <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true"
                    DataKeyNames="SN" AllowSorting="true"    SortField="SN" OnSort="Grid2_Sort"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid2_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                
                                <f:TwinTriggerBox ID="TwinTriggerBox3" runat="server" ShowLabel="false" EmptyText="请输入料号或名称或规格"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="TwinTriggerBox3_Trigger2Click"
                                    OnTrigger1Click="TwinTriggerBox3_Trigger1Click">
                                </f:TwinTriggerBox>
                                <f:DatePicker ID="DateFrom" DateFormatString="yyyy-MM-dd" LabelWidth="60px"  Width="200px"    runat="server" Label="开始" AutoPostBack="true" OnTextChanged="OnDateFrom_Changed"></f:DatePicker>
                                <f:DatePicker ID="DateTo" DateFormatString="yyyy-MM-dd"    LabelWidth="60px"  Width="200px"     runat="server" Label="结束"  CompareControl="DateFrom" CompareOperator="GreaterThan" CompareMessage="结束日期应大于开始日期" AutoPostBack="true" OnTextChanged="OnDateTo_Changed"></f:DatePicker>
                                
                                <f:ToolbarSeparator runat="server">
                                </f:ToolbarSeparator>
                                <f:Button ID="btnDeleteSelected" Icon="Delete" runat="server" Text="删除选中记录" OnClick="btnDeleteSelected_Click">
                                </f:Button>
                               
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
                                  <f:Button ID="Button2" EnableAjax="false" DisableControlBeforePostBack="false"  runat="server" Icon="PageExcel"  OnClick="Button2_Click"    Text="导出Excel">
                            </f:Button>
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
                        <f:RowNumberField Width="60px" EnablePagingNumber="true" />
                        <f:BoundField DataField="Reserve1" SortField="Reserve1" Width="150px" HeaderText="料号" />
                        <f:BoundField DataField="SubName"  SortField="SubName"   Width="250px" HeaderText="名称" />
                        <f:BoundField DataField="Reserve2"  SortField="Reserve2"   Width="250px" HeaderText="规格" />
                              
                        <f:BoundField DataField="Reserve3"   Width="100px" HeaderText="材质" />
                       
                        <f:BoundField DataField="Reserve4"  Width="150px" HeaderText="表面处理" />
                         <f:BoundField DataField="BaseConsume"    Width="90px" HeaderText="用量" />
                         <f:BoundField DataField="SubCls"    Width="200px" HeaderText="分类" />
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" Hidden="true"
                            ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                    </Columns>
                </f:Grid>


                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>










        
       
       
       
       
       
       <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="800px"
            Height="500px" OnClose="Window1_Close">
        </f:Window>
    </form>

</body>
</html>
