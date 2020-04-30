<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialFinish.aspx.cs" Inherits="AppBoxPro.MaterialFinish.MaterialFinish" %>

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

        .f-grid-row .f-grid-cell-hlfMajor {
            background-color: #b200ff;
            color: #fff;
        }

         .f-grid-row .f-grid-cell-hlfMajor a,
         .f-grid-row .f-grid-cell-hlfMajor a:hover {
             color: #fff;
         }
          .f-grid-row.color1,
        .f-grid-row.color1 .f-icon,
        .f-grid-row.color1 a {
            background-color: #FDDA01;
             /*background-color: #0094ff;*/
            color: #000;
        }
             .f-grid-row.color2,
             .f-grid-row.color1 .f-icon,
             .f-grid-row.color1 a {
                 background-color: #ccc;
                 /*background-color: #0094ff;*/
                 color: #000;
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
                    DataKeyNames="odtsn" AllowSorting="true"  SortField="SN"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" EnableTextSelection="true" EnableRowDoubleClickEvent="true"
                    OnRowCommand="Grid1_RowCommand" OnRowDoubleClick="Grid1_RowDoubleClick" OnPageIndexChange="Grid1_PageIndexChange" OnRowDataBound="Grid1_RowDataBound" >
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                
                                <f:SimpleForm ID="SF1" runat="server" Width="100%" ShowBorder="false" ShowHeader="false" LabelWidth="100px" Layout="HBox" BoxConfigChildMargin="0 5 0 5px">
                                    <Items>
                                
                                <f:TwinTriggerBox ID="ttbSearchMessage" Width="400px" runat="server" ShowLabel="false" EmptyText="可输入订单号、名称搜索"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>

                               
                               
                                <f:DatePicker ID="datePickerFrom" DateFormatString="yyyy-MM-dd" LabelWidth="80px"  Width="200px"    runat="server" Label="接单开始"></f:DatePicker>
                                <f:DatePicker ID="datePickerTo" DateFormatString="yyyy-MM-dd" LabelWidth="80px"  Width="200px"    runat="server" Label="接单结束"  CompareControl="datePickerFrom" CompareOperator="GreaterThan" CompareMessage="结束日期应大于开始日期"></f:DatePicker>
                                         <f:RadioButtonList ID="rbtIsQT" runat="server" Label="状态" LabelWidth="50px" ColumnWidth="80px" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="rbtIsQT_SelectedIndexChanged">
                             <f:RadioItem Text="全部" Value=""  Selected="true" />
                             <f:RadioItem Text="未齐套" Value="0" />
                             <f:RadioItem Text="齐套" Value="1"  />
                         </f:RadioButtonList>
                                 <f:Button ID="btnSearch" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                                 <f:Button  ID="btnClear"  Icon="Cancel" EnablePostBack="false" runat="server"
                        Text="清空">
                    </f:Button>
                               <f:Button ID="btnBack" Pressed="true" ValidateForms="haha"  runat="server" Text="后退" Icon="RewindBlue" OnClick="btnBack_Click"></f:Button>
                        <f:TextBox ID="txtClickColsName" runat="server" Hidden="true" Label="列名" LabelWidth="50px"></f:TextBox>
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
                        <f:RowNumberField Width="35px" EnablePagingNumber="true" HeaderText="" />
                         <f:TemplateField HeaderText="查看明细" Width="90px" ToolTip="查看明细">
                    <ItemTemplate>
                        <a href="javascript:;" style="color:blue;text-decoration:underline;" onclick="lookOrder('<%#Eval("odtsn")%>','<%#Eval("OrderNo")%>','<%#Eval("ItemName")%>')">查看明细</a>
                    </ItemTemplate>
                </f:TemplateField>
              
                        <f:BoundField DataField="OrderNo" SortField="OrderNo"  Width="140px" HeaderText="订单编号" />
                        <f:BoundField DataField="ItemNo" SortField="ItemNo"  Width="130px" HeaderText="产品编号" />
                        <f:BoundField DataField="ItemName" SortField="ItemName"  Width="150px" HeaderText="产品名称" />
                        <f:BoundField DataField="Quantity"  SortField="Quantity"  Width="60px" HeaderText="数量" />
                        <f:BoundField DataField="RecOrderDate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="下单日期" />
                        <f:BoundField DataField="OutGoodsDate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="交货日期" />
                        <f:BoundField DataField="InputeDate" DataFormatString="{0:yyyy-MM-dd}" Width="130px" HeaderText="BOM导入日期" />
                        <f:BoundField DataField="plandate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="最晚计划日期" />
                        <f:BoundField DataField="pdate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="最晚入库日期" />
                        <f:BoundField DataField="ZhuangPeiDate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="计划组装日期" />
                        <f:BoundField DataField="qtl"  SortField="qtl"  Hidden="true" Width="200px" HeaderText="物料齐套率" />
                        <f:RenderField DataField="qtl" SortField="qtl" RendererFunction="retval"  Width="200px" HeaderText="物料齐套率" ></f:RenderField>

                        
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
</html>
    <script type="text/javascript">
        function lookOrder(ID, orderno,proname) {
            parent.addExampleTab({
                id: 'MaterialFinish_'+ID + '_tab',
                iframeUrl: 'MaterialFinish/showDetail.aspx?sn=' + escape(ID) + '&od=' + escape(orderno),
                title: proname + '-齐套明细',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });
        }
        function retval(value) {
            if (value) {
                value = parseFloat(value);
                if (value >= 100) {
                    return parseInt(value) + '%';
                }
                else {
                    return value + '%';
                }
            }
        }
    </script>
