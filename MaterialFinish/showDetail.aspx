<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showDetail.aspx.cs" Inherits="AppBoxPro.MaterialFinish.showDetail" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <style type="text/css">
        .f-grid-row .f-grid-cell-bfRowNum {
            /*background-color: #0094ff;*/
            color: #000;
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
            background-color: #1AA348;
             /*background-color: #0094ff;*/
            color: #fff;
        }
             .f-grid-row.color2,
             .f-grid-row.color1 .f-icon,
             .f-grid-row.color1 a {
                 background-color: #ccc;
                 /*background-color: #0094ff;*/
                 color: #000;
             }
            
    </style>
     <style>
        .d{background-color:#E4F1FB;}
        
    </style>
</head>
<body>
       <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel5" EnableFStateValidation="false" runat="server" />
       <f:Panel ID="Panel5" CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            Layout="VBox" AutoScroll="true" Margin="0px" ShowHeader="false" Title="1111111111111"
            BoxConfigChildMargin="0 0 0 0" BodyPadding="0">
            <Items>
 
<f:Grid ID="Grid2" BoxFlex="1" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="" EnableCollapse="false"
            runat="server" DataKeyNames="id" EnableTextSelection="true" 
                KeepCurrentSelection="false" OnPageIndexChange="Grid2_PageIndexChange"  AllowSorting="true" OnSort="Grid2_Sort"  SortField="id"   
                     AllowPaging="true" IsDatabasePaging="true"  EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid2_RowDoubleClick">

                    <Toolbars>
                         <f:Toolbar runat="server" >
                            <Items>
                               <f:Label Label="订单编号"  runat="server" LabelWidth="80px" Width="200px" ID="labOrderno"></f:Label>
                                <f:Label Label="客人编号"  runat="server" LabelWidth="80px" Width="200px" ID="labClientNo"></f:Label>
                                <f:Label Label="型号"  runat="server" LabelWidth="50px" Width="150px" ID="labProNo"></f:Label>
                                <f:Label Label="产品名称"  runat="server" LabelWidth="80px" Width="280px" ID="labProName"></f:Label>
                                <f:Label Label="数量"  runat="server" LabelWidth="50px" Width="150px" ID="labQuantity"></f:Label>
                                <f:Label Label="单位"  runat="server" LabelWidth="50px" Width="150px" ID="labUnit"></f:Label>
                                       
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar runat="server">
                            <Items>
                                 <f:Label Label="颜色"  runat="server" LabelWidth="50px" Width="150px" ID="labColor"></f:Label>
                                
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                
                                <f:Label Label="是否新产品" runat="server" LabelAlign="Left" CssStyle="text-align:left;"  LabelWidth="100px" Width="150px" ID="labIsNew"></f:Label>
                                
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                <f:Label Label="是否新包材" runat="server" LabelWidth="100px" Width="130px"  ID="labIsPackingmaterials"></f:Label>
                               
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                <f:Label Label="国家包材版本" LabelWidth="120px" Width="180px" runat="server" ID="labCountryPackVer"></f:Label>
                                 
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                <f:Label Label="是否变更" runat="server" LabelWidth="90px" Width="150px"  ID="labIsChange"></f:Label>
                                    <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                               <f:Label Label="变更备注1" runat="server" ID="labDemand1"></f:Label>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar runat="server" ColumnWidth="50% 50%">
                            <Items>
 
                                <f:Label Label="变更备注2"  runat="server" ID="labDemand2"></f:Label>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="请输入料号或名称或规格"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                                
                                <f:Button ID="btnSearch" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch2_click"></f:Button>
                                <f:Button ID="btnBack" Pressed="true" ValidateForms="haha"  runat="server" Text="后退" Icon="RewindBlue" OnClick="btnBack_Click"></f:Button>
                        <f:TextBox ID="txtClickColsName" runat="server" Hidden="true" Label="列名" LabelWidth="50px"></f:TextBox>
                               
                                <f:HiddenField ID="txtQuantity" runat="server"></f:HiddenField>
                                <f:HiddenField ID="txtFSN" runat="server"></f:HiddenField>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                  <f:Button ID="btnExcel"  Pressed="true" Enabled="true" EnableAjax="false" DisableControlBeforePostBack="false" OnClick="btnExcel_Click"  runat="server" Icon="PageExcel"     Text="导出Excel">
                            </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
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
                        <f:RowNumberField Width="50px" ColumnID="bfRowNum" />
                        <f:BoundField Width="200px" ColumnID="ItemNo" SortField="ItemNo" DataField="ItemNo" HeaderText="料号"></f:BoundField>
                        <f:BoundField Width="200px" ColumnID="ItemName" SortField="ItemName" DataField="ItemName" HeaderText="料名"></f:BoundField>
                        <f:BoundField Width="200px" ColumnID="OrderUsingQuantity" SortField="OrderUsingQuantity" DataField="OrderUsingQuantity" HeaderText="实际用量"></f:BoundField>
                        <f:BoundField Width="200px" ColumnID="Quantity" SortField="Quantity" DataField="Quantity" HeaderText="进仓数量"></f:BoundField>
                   <f:BoundField Width="200px" ColumnID="MainFrom" SortField="MainFrom" DataField="MainFrom" HeaderText="主要来源"></f:BoundField>

                       <f:BoundField DataField="PDate" ColumnID="PDate" SortField="PDate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="最晚进仓日期" />

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
        function bomState(value) {
            return value == 0 ? "<span style='color:red;'>未生成</span>" : "<span style='color:blue;'>已生成</span>";
        }
        function toBOM(ID, ORDERNO, ITEMNO, Q, TITLE) {
            parent.addExampleTab({
                id: 'RealBom_'+ID + '_tab',
                iframeUrl: 'BomMag/RealBom.aspx?sn=' + ID + '&od=' + ORDERNO + '&id=' + ITEMNO + '&q=' + Q,
                title: TITLE,
                iconFont: 'sign-in',
                refreshWhenExist: true
            });
        }
        function reloadGrid(param) {
            alert("dddd" + param);
            __doPostBack(null, 'ReloadGrid$' + param);
        }
        function renderGender(value) {
            return value == 0 ? '已审核' : '未审核';
        }

     </script>
    <script>


        var gridClientID = '<%= Grid2.ClientID %>';

        function updateGridRow(rowId, values) {
            var grid = F(gridClientID);

            // cancelEdit用来取消编辑
            grid.cancelEdit();

            grid.updateCellValue(rowId, values);
        }

    </script>
