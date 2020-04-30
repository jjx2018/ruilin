<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectBomHead.aspx.cs" Inherits="AppBoxPro.BomMag.ProjectBomHead" %>

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
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true" 
                    DataKeyNames="SN" AllowSorting="true" OnSort="Grid1_Sort"  SortField="SN"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange"  EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" >
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
                                <f:Button ID="btnBack" Pressed="true" ValidateForms="haha"  runat="server" Text="后退" Icon="RewindBlue" OnClick="btnBack_Click"></f:Button>
                          <f:TextBox ID="txtClickColsName" runat="server" Hidden="true" Label="列名" LabelWidth="50px"></f:TextBox>
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
                         <f:TemplateField HeaderText="BOM明细" Width="100px" ToolTip="查看BOM明细">
                    <ItemTemplate>
                        <a href="javascript:;" style="color:blue;text-decoration:underline;" onclick="lookOrder('<%#Eval("SN")%>','<%#Eval("ProNo")%>','<%#Eval("ProName")%>')">BOM明细</a>
                    </ItemTemplate>
                </f:TemplateField>
                        <f:BoundField ColumnID="ProNo" DataField="ProNo" TextAlign="Center" SortField="ProNo"  Width="150px" HeaderText="产品编号" />
                        <f:BoundField ColumnID="ProName" DataField="ProName" TextAlign="Center" ExpandUnusedSpace="true" SortField="ProName"  Width="200px" HeaderText="产品名称" />
                        <f:BoundField ColumnID="Color" DataField="Color" TextAlign="Center" ExpandUnusedSpace="true" SortField="Color"  Width="200px" HeaderText="颜色" />
                        <f:BoundField ColumnID="ClientProNo"  DataField="ClientProNo" Hidden="true" TextAlign="Center" SortField="ClientProNo"  Width="200px" HeaderText="客户产品型号" />
                        
                        <f:BoundField ColumnID="ClientCode"  DataField="ClientCode" Hidden="true" TextAlign="Center" SortField="ClientCode"  Width="100px" HeaderText="客户代号" />
                        <f:BoundField ColumnID="BomDate"  DataField="BomDate" TextAlign="Center" SortField="BomDate" DataFormatString="{0:yyyy-MM-dd}"  Width="100px" HeaderText="日期" />
                        <f:BoundField  ColumnID="Ver" DataField="Ver"  TextAlign="Center"  Width="60px" HeaderText="版本" />
                        <f:BoundField ColumnID="FileNo"  DataField="FileNo"  TextAlign="Center" SortField="FileNo"  Width="200px" HeaderText="文件编号" />
                         <f:WindowField ColumnID="remark" TextAlign="Center" Icon="ApplicationAdd" HeaderText="添加备注" DataToolTipField="Remark" WindowID="Window1"
                            Title="备注" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/BomMag/addRemark.aspx?id={0}&t=pmh"
                            Width="90px" />
                         <f:BoundField ColumnID="ChineseName"  DataField="ChineseName"  TextAlign="Center"  Width="80px" HeaderText="录入人" />
                         <f:BoundField ColumnID="InputeDate"  DataField="InputeDate"  TextAlign="Center" SortField="InputeDate" DataFormatString="{0:yyyy-MM-dd}"  Width="100px" HeaderText="录入日期" />
                        <f:HyperLinkField HeaderText="下载Excel" DataToolTipField="下载Excel" DataTextField="BomExcel" Text="下载Excel"
                      DataNavigateUrlFields="BomExcel" DataNavigateUrlFormatString="~/BOMFile/{0}"
                    UrlEncode="true" Target="_blank" ExpandUnusedSpace="true" MinWidth="150px" />
                    </Columns>
                </f:Grid>

            </Items>
        </f:Panel>









        
       
       
       
       
       
       <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="400px" Title="<span style='color:red;'>以下物料编码不符合编码规范，请修改后再次上传</span>"
            Height="200px" OnClose="Window1_Close">
        </f:Window>
    </form>

</body>
</html>
<script type="text/javascript">
    function lookOrder(ID, itemno, itemname) {
        parent.addExampleTab({
            id: 'ProduceBomDetail_' + ID + '_tab',
            iframeUrl: 'BomMag/ProjectBomDetail.aspx?ProNo=' + escape(itemno) + '&ItemName=' + escape(itemname) + '&sn=' + ID,
            title: itemname + 'BOM明细',
            iconFont: 'sign-in',
            refreshWhenExist: true
        });
    }
</script>