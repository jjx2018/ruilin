<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magProjectBom.aspx.cs" Inherits="AppBoxPro.BomMag.magProjectBom" %>

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
    </style>
</head>
<body>
       <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel5" EnableFStateValidation="false" runat="server" />
       <f:Panel ID="Panel5" CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            Layout="VBox" AutoScroll="true" Margin="0px" ShowHeader="false" Title="1111111111111"
            BoxConfigChildMargin="0 0 0 0" BodyPadding="0">
            <Items>
                <f:Panel ID="Panel3" Title="" Height="290px" runat="server" EnableCollapse="true" EnableCollapseEvent="true" 
                    BodyPadding="0px" ShowBorder="false" ShowHeader="true" Layout="VBox">
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
                        <f:Button ID="btnImport" OnClick="btnImport_Click" Icon="PageWhiteExcel" Text="导入Excel" EnablePostBack="true" runat="server">
                        </f:Button>
                                  <f:Button ID="btndelBomHead"  ConfirmText="确定删除选中的记录？" ConfirmTarget="Top" Icon="Delete" OnClick="btndelBomHead_Click" runat="server" Text="删除选中记录" >
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
                        <f:RowNumberField Width="35px"  ColumnID="bfRowNum" EnablePagingNumber="true" HeaderText="" />
                        <f:BoundField DataField="ProNo" TextAlign="Center" SortField="ProNo"  Width="150px" HeaderText="产品编号" />
                        <f:BoundField DataField="ProName" TextAlign="Center" ExpandUnusedSpace="true" SortField="ProName"  Width="200px" HeaderText="产品名称" />
                        <f:BoundField DataField="ClientProNo" Hidden="true" TextAlign="Center" SortField="ClientProNo"  Width="200px" HeaderText="客户产品型号" />
                        
                        <f:BoundField DataField="ClientCode" Hidden="true" TextAlign="Center" SortField="ClientCode"  Width="100px" HeaderText="客户代号" />
                        <f:BoundField DataField="BomDate" TextAlign="Center" SortField="BomDate" DataFormatString="{0:yyyy-MM-dd}"  Width="100px" HeaderText="日期" />
                        <f:BoundField DataField="Ver"  TextAlign="Center"  Width="60px" HeaderText="版本" />
                        <f:BoundField DataField="FileNo"  TextAlign="Center" SortField="FileNo"  Width="200px" HeaderText="文件编号" />
                         <f:WindowField ColumnID="remark" TextAlign="Center" Icon="ApplicationAdd" HeaderText="添加备注" DataToolTipField="Remark" WindowID="Window1"
                            Title="备注" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/BomMag/addRemark.aspx?id={0}&t=pmh"
                            Width="90px" />
                         <f:BoundField DataField="ChineseName"  TextAlign="Center"  Width="80px" HeaderText="录入人" />
                         <f:BoundField DataField="InputeDate"  TextAlign="Center" SortField="InputeDate" DataFormatString="{0:yyyy-MM-dd}"  Width="100px" HeaderText="录入日期" />
                        <f:HyperLinkField HeaderText="下载Excel" DataToolTipField="下载Excel" DataTextField="BomExcel" Text="下载Excel"
                      DataNavigateUrlFields="BomExcel" DataNavigateUrlFormatString="~/BOMFile/{0}"
                    UrlEncode="true" Target="_blank" ExpandUnusedSpace="true" MinWidth="150px" />
                    </Columns>
                </f:Grid>



                    </Items>
                </f:Panel>
                <f:Panel ID="Panel4" Title="面板2" BoxFlex="1" MinHeight="270px" Margin="0"
                    runat="server" BodyPadding="1px 0px 0px 0px" ShowBorder="false" ShowHeader="false" Layout="VBox">
                    <Items>
                  <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true"  SummaryPosition="Bottom" EnableSummary="true" EnableTextSelection="true" 
                    DataKeyNames="SN" AllowSorting="true"    SortField="SN" OnSort="Grid2_Sort"
                    SortDirection="ASC" AllowPaging="false" IsDatabasePaging="false" OnPageIndexChange="Grid2_PageIndexChange" EnableTree="true" TreeColumn="ItemNo" DataIDField="SN" DataParentIDField="ParentSN"  AllowColumnLocking="true" EnableTreeIcons="false" ExpandAllTreeNodes="true" OnRowDoubleClick="Grid2_RowDoubleClick" EnableRowDoubleClickEvent="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                
                                <f:TwinTriggerBox ID="TwinTriggerBox3" runat="server" ShowLabel="false" EmptyText="请输入料号、名称、规格、分类、主要来源、车间、仓库"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="TwinTriggerBox3_Trigger2Click"
                                    OnTrigger1Click="TwinTriggerBox3_Trigger1Click" Width="410px">
                                </f:TwinTriggerBox>
                                <f:DatePicker ID="DateFrom" DateFormatString="yyyy-MM-dd" LabelWidth="60px"  Width="200px"    runat="server" Label="开始" AutoPostBack="true" OnTextChanged="OnDateFrom_Changed"></f:DatePicker>
                                <f:DatePicker ID="DateTo" DateFormatString="yyyy-MM-dd"    LabelWidth="60px"  Width="200px"     runat="server" Label="结束"  CompareControl="DateFrom" CompareOperator="GreaterThan" CompareMessage="结束日期应大于开始日期" AutoPostBack="true" OnTextChanged="OnDateTo_Changed"></f:DatePicker>
                                 <f:Button ID="btnSearch2" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch2_click"></f:Button>
                                <f:Button ID="btnBack" Pressed="true" ValidateForms="haha"  runat="server" Text="后退" Icon="RewindBlue" OnClick="btnBack_Click"></f:Button>
                        <f:TextBox ID="txtClickColsName" runat="server" Hidden="true" Label="列名" LabelWidth="50px"></f:TextBox>
                               
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
                        <f:RowNumberField  ColumnID="bfRowNum"  EnableTreeNumber="false" />
                        <f:BoundField  EnableLock="true" Locked="true"  DataField="Seq" TextAlign="Left" SortField="Seq"  Width="90px" HeaderText="序号" ColumnID="Seq" />
                        <f:BoundField DataField="ItemNo" ColumnID="ItemNo"  TextAlign="Left" SortField="ItemNo" Width="250px" HeaderText="料号" />
                        <f:BoundField DataField="ItemName" ColumnID="ItemName"  TextAlign="Left" SortField="ItemName"   Width="250px" HeaderText="名称" />
                        <f:BoundField DataField="Spec"  ColumnID="Spec" TextAlign="Left" SortField="Spec"   Width="250px" HeaderText="规格" />
                              
                        <f:BoundField DataField="Material" ColumnID="Material" TextAlign="Center"   Width="100px" HeaderText="材质" />
                       
                        <f:BoundField DataField="SurfaceDeal" ColumnID="SurfaceDeal" TextAlign="Center"  Width="150px" HeaderText="表面处理" />
                         <f:BoundField DataField="ProUsingQuantity" ColumnID="ProUsingQuantity"   TextAlign="Center"  Width="60px" HeaderText="用量" />
                         <f:BoundField ColumnID="ZongCheng" DataField="ZongCheng"    Width="80px" HeaderText="总成" />
                        <f:BoundField ColumnID="BaseNum" DataField="BaseNum"    Width="80px" HeaderText="底数" />
                        <f:BoundField ColumnID="Sclass" DataField="Sclass"    Width="80px" HeaderText="分类" />
                        <f:BoundField ColumnID="MainFrom" DataField="MainFrom"    Width="90px" HeaderText="主要来源" />
                        <f:BoundField ColumnID="WorkShop" DataField="WorkShop"    Width="90px" HeaderText="生产车间" />      <f:BoundField ColumnID="StoreHouse" DataField="StoreHouse"    Width="80px" HeaderText="仓库" /> 
                         <f:WindowField ColumnID="remark" TextAlign="Center" Icon="ApplicationAdd" HeaderText="添加备注" DataToolTipField="Remark" WindowID="Window1"
                            Title="备注" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/BomMag/addRemark.aspx?id={0}&t=pmd"
                            Width="80px" />
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" Hidden="true"
                            ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                    </Columns>
                </f:Grid>


                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>









        
       
       
       
       
       
       <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="400px" Title="<span style='color:red;'>以下物料编码不符合编码规范，请修改后再次上传</span>"
            Height="200px" OnClose="Window1_Close">
        </f:Window>
    </form>

</body>
</html>