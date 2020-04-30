<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProduceBom.aspx.cs" Inherits="AppBoxPro.ruilin.ProduceBom" %>

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
    </style>
</head>
<body>
       <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel5" EnableFStateValidation="false" runat="server" />
       <f:Panel ID="Panel5" CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            Layout="VBox" AutoScroll="true" Margin="0px" ShowHeader="false" Title="1111111111111"
            BoxConfigChildMargin="0 0 0 0" BodyPadding="0">
            <Items>
                <f:Panel ID="Panel3" Title="面板1" Height="300px" runat="server"
                    BodyPadding="0px" ShowBorder="false" ShowHeader="false" Layout="VBox">
                    <Items>
 <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true" AllowCellEditing="false" ClicksToEdit="2" 
                     AllowSorting="false" DataKeyNames="SN" SortField="SN"   OnSort="Grid1_Sort"
                    AllowPaging="true" IsDatabasePaging="true" EnableRowSelectEvent="true" OnPageIndexChange="Grid1_PageIndexChange" OnRowSelect="Grid1_RowSelect">
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
                        <f:BoundField DataField="ClinetNo" TextAlign="Center"   Width="100px" HeaderText="客人编号" />
                        <f:BoundField DataField="ItemNo" TextAlign="Center" SortField="ItemNo"   Width="120px" HeaderText="型号" />
                          <f:BoundField DataField="ItemName" TextAlign="Center" SortField="ItemName"   Width="200px" HeaderText="产品名称" />    
                        <f:BoundField DataField="Price" TextAlign="Center" SortField="Price" Width="80px" HeaderText="单价" />
                        <f:BoundField DataField="Quantity" TextAlign="Center" SortField="Quantity" Width="100px" HeaderText="数量" />
                       <f:RenderField Width="120px" ColumnID="IsBom" DataField="IsBom" TextAlign="Center"
                      HeaderText="BOM状态" RendererFunction="bomState">
                    <Editor>
                        <f:TextBox ID="txtIsBom" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                         <f:BoundField  DataField="BomVer" TextAlign="Center" Width="80px" HeaderText="BOM版本" />
                        <f:BoundField  DataField="Inputer" TextAlign="Center" Width="80px" HeaderText="录入人" />
                         <f:BoundField DataField="Remark"  ExpandUnusedSpace="true"  HeaderText="备注" />
                         <f:BoundField Hidden="true" DataField="Color"  ExpandUnusedSpace="true"  HeaderText="颜色" />
                         <f:BoundField Hidden="true" DataField="IsNew"  ExpandUnusedSpace="true"  HeaderText="是否新产品" />
                         <f:BoundField  Hidden="true" DataField="IsPackingmaterials"  ExpandUnusedSpace="true"  HeaderText="是否新包材" />
                         <f:BoundField Hidden="true" DataField="CountryPackVer"  ExpandUnusedSpace="true"  HeaderText="国家包材版本" />
                         <f:BoundField Hidden="true" DataField="IsChange"  ExpandUnusedSpace="true"  HeaderText="是否变更" />
                         <f:BoundField Hidden="true"  DataField="Demand1"  ExpandUnusedSpace="true"  HeaderText="变更备注1" />
                         <f:BoundField Hidden="true" DataField="Demand2"  ExpandUnusedSpace="true"  HeaderText="变更备注2" />
                     
                         <f:WindowField ColumnID="viewField" HeaderText="详情" TextAlign="Center" Icon="ApplicationViewIcons" ToolTip="查看详情" WindowID="Window1"
                            Title="查看详情" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/ruilin/OrderDetailedit.aspx?id={0}&k=1"
                            Width="60px" />
                         <f:WindowField ColumnID="editField" Hidden="true" TextAlign="Center" Icon="Pencil" HeaderText="编辑" ToolTip="编辑" WindowID="Window1"
                            Title="编辑" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/ruilin/OrderDetailedit.aspx?id={0}&k=2"
                            Width="60px" />
                        <f:LinkButtonField ColumnID="deleteField" Hidden="true" TextAlign="Center" HeaderText="删除" Icon="Delete" ToolTip="删除"  
                            ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="60px" />
                        
                    </Columns>
                </f:Grid>





                    </Items>
                </f:Panel>
                <f:Panel ID="Panel4" Title="产品信息" BoxFlex="1" MinHeight="270px" Margin="0"
                    runat="server" BodyPadding="1px 0px 0px 0px" ShowBorder="false" ShowHeader="false" Layout="VBox">
                    <Items>
 <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false" SummaryPosition="Bottom" EnableSummary="true" EnableTextSelection="true" 
                    EnableCheckBoxSelect="true" AllowCellEditing="true" ClicksToEdit="1" 
                    DataKeyNames="SN,ItemNo" AllowSorting="true"    SortField="SN" OnSort="Grid2_Sort"
                    SortDirection="ASC" AllowPaging="false" IsDatabasePaging="true" OnPageIndexChange="Grid2_PageIndexChange" OnRowCommand="Grid2_RowCommand"  EnableTree="true" TreeColumn="ItemNo" DataIDField="SUBSN" DataParentIDField="ParentSN"  EnableTreeIcons="false"  ExpandAllTreeNodes="true"  KeepCurrentSelection="false"  AllowColumnLocking="true"  >
                    <Toolbars>
                         <f:Toolbar runat="server" >
                            <Items>
                               
                                        <f:Label Label="颜色"  runat="server" LabelWidth="50px" Width="100px" ID="labColor"></f:Label>
                                
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                
                                <f:Label Label="是否新产品" runat="server" LabelAlign="Left" CssStyle="text-align:left;"  LabelWidth="100px" Width="110px"  ID="labIsNew"></f:Label>
                                
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                <f:Label Label="是否新包材" runat="server" LabelWidth="100px" Width="110px"  ID="labIsPackingmaterials"></f:Label>
                               
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                <f:Label Label="国家包材版本" LabelWidth="120px" runat="server" ID="labCountryPackVer"></f:Label>
                                 
                                <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                                <f:Label Label="是否变更" runat="server" LabelWidth="90px" Width="100px"  ID="labIsChange"></f:Label>
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
                               <f:Button ID="btnCopy" Pressed="true" Enabled="false"  runat="server" Text="复制" Icon="PageCopy" OnClick="btnCopy_Click"></f:Button>
                                
                               <f:Button ID="btnNew" OnClick="btnNew_Click"  Pressed="true" Enabled="false"  Text="新增" Icon="Add" runat="server">
                        </f:Button>
                                 <f:Button ID="btnZUHE" OnClick="btnZUHE_Click" Pressed="true" Enabled="false"  Text="添加组合件" Icon="Add" runat="server">
                        </f:Button>
                          <f:Button ID="btnSave"  OnClick="btnSave_Click"  Pressed="true" Enabled="false" runat="server" Icon="SystemSave"    Text="保存">
                            </f:Button> 
                        <f:Button ID="btnDelete" Hidden="false"  Pressed="true" Enabled="false" OnClick="btnDeleteSelected_Click" Text="删除" Icon="Delete"  runat="server">
                        </f:Button>
                                <f:DropDownList ID="ddlDept" Label="接收部门" EnableEdit="true" LabelWidth="80px" Width="220px" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true" AutoSelectFirstItem="true" runat="server">

                                </f:DropDownList>
                                <f:DropDownList ID="ddlUser" Label="接收人" EnableEdit="true"  LabelWidth="70px" Width="160px"  runat="server">

                                </f:DropDownList>
                          <f:Button ID="btnPLSend" Hidden="false"  Pressed="true" Enabled="false" OnClick="btnPLSend_Click" Text="批量发送确认单" Icon="PageWhiteStack"  runat="server">
                        </f:Button>      
                                <f:HiddenField ID="txtQuantity" runat="server"></f:HiddenField>
                                <f:HiddenField ID="txtFSN" runat="server"></f:HiddenField>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                  <f:Button ID="btnExcel"  Pressed="true" Enabled="false" EnableAjax="false" DisableControlBeforePostBack="false" OnClick="btnExcel_Click"  runat="server" Icon="PageExcel"     Text="导出Excel">
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
                        <f:RowNumberField Width="30px" ColumnID="bfRowNum" EnableTreeNumber="true" />
                      <f:RenderField Width="90px" ColumnID="Seq" DataField="Seq" EnableColumnEdit="false" TextAlign="Left"
                      HeaderText="序号"   EnableLock="true" Locked="true">
                    <Editor>
                        <f:TextBox ID="tbxSeq" Required="true" runat="server">
                            
                        </f:TextBox>
                        
                    </Editor>
                </f:RenderField>

<f:RenderField Width="200px" ColumnID="ItemNo" DataField="ItemNo" HeaderText="料号" EnableColumnEdit="true"
                    EditGetterFunction="editGetterName" EditSetterFunction="editSetterName">
                    <Editor>
                        <f:DropDownBox runat="server" EnableEdit="false"  AutoPostBack="true"  ID="ddlItemNo"   EmptyText="请选择" MatchFieldWidth="false" EnableMultiSelect="false" AutoShowClearIcon="true" EnableClearIconClickEvent="false" EnableClickAction="true" AlwaysDisplayPopPanel="false"  >
                            <PopPanel>
                                <f:Grid  ID="Grid3" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="SN" DataTextField="ItemNo"
                                    Hidden="true" Width="650px" Height="300px" EnableMultiSelect="false">
                                    <Toolbars>
                                        <f:Toolbar runat="server">
                                            <Items>
                                                <f:TextBox ID="txtKeyword" EmptyText="请输入料号或名称或规格" OnTextChanged="txtKeyword_TextChanged" AutoPostBack="true" Required="true" runat="server"></f:TextBox>  
                                            </Items>
                                        </f:Toolbar>
                                    </Toolbars>
                                    <Columns>
                                        <f:RowNumberField />
                        <f:BoundField ColumnID="ItemNo" DataField="ItemNo" SortField="ItemNo" Width="150px" HeaderText="料号" />
                        <f:BoundField ColumnID="ItemName" DataField="ItemName"  SortField="ItemName"   Width="250px" HeaderText="名称" />
                        <f:BoundField ColumnID="Spec" DataField="Spec"  SortField="Spec"   Width="250px" HeaderText="规格" />                              
                        <f:BoundField ColumnID="Material" DataField="Material"   Width="100px" HeaderText="材质" />                       
                        <f:BoundField ColumnID="SurfaceDeal" DataField="SurfaceDeal"  Width="100px" HeaderText="表面处理" />
                        <f:BoundField ColumnID="ProUsingQuantity" DataField="ProUsingQuantity"    Width="60px" HeaderText="产品用量" />
                        <f:BoundField ColumnID="ZongCheng" DataField="ZongCheng"    Width="80px" HeaderText="总成" />
                        <f:BoundField ColumnID="BaseNum" DataField="BaseNum"    Width="80px" HeaderText="底数" />
                        <f:BoundField ColumnID="Sclass" DataField="Sclass"    Width="80px" HeaderText="分类" />
                        <f:BoundField ColumnID="MainFrom" DataField="MainFrom"    Width="90px" HeaderText="主要来源" />
                        <f:BoundField ColumnID="WorkShop" DataField="WorkShop"    Width="80px" HeaderText="生产车间" />      
                                        <f:BoundField ColumnID="StoreHouse" DataField="StoreHouse"    Width="80px" HeaderText="仓库" />                                   

                                    </Columns>
                                    <Listeners>
                                        <f:Listener Event="rowclick" Handler="onGrid3RowClick" />
                                    </Listeners>
                                </f:Grid>
                            </PopPanel>
                        </f:DropDownBox>
                    </Editor>
                </f:RenderField>

                 <f:RenderField Width="150px" ColumnID="ItemName" DataField="ItemName"  EnableColumnEdit="false" TextAlign="Center"
                      HeaderText="名称">
                    <Editor>
                        <f:TextBox ID="TextBox1" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="150px" ColumnID="Spec" MinWidth="60px" DataField="Spec" TextAlign="Center" ExpandUnusedSpace="true" EnableColumnEdit="false"
                      HeaderText="规格" >
                    <Editor>
                        <f:TextBox ID="txtSpec" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="80px" ColumnID="Material" DataField="Material" TextAlign="Center"
                      HeaderText="材质"  EnableColumnEdit="false">
                    <Editor>
                        <f:TriggerBox ID="txtMaterial" TriggerIcon="Search" OnTriggerClick="txtMaterial_TriggerClick"  runat="server">
                        </f:TriggerBox>
                       <%-- <f:TextBox ID="txtMaterial" Required="true" runat="server">
                        </f:TextBox>--%>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="SurfaceDeal" DataField="SurfaceDeal" TextAlign="Center"
                      HeaderText="表面处理" EnableColumnEdit="false">
                    <Editor>
                        <f:TextBox ID="txtSurfaceDeal" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="90px" ColumnID="ProUsingQuantity" DataField="ProUsingQuantity" TextAlign="Center"
                      HeaderText="产品用量">
                    <Editor>
                        <f:NumberBox  MinValue="1" ID="txtProQuantity" Required="true" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="90px" ColumnID="OrderUsingQuantity" DataField="OrderUsingQuantity" TextAlign="Center"
                      HeaderText="订单用量">
                    <Editor>
                        <f:NumberBox  MinValue="1" ID="txtUsingQuantity" Required="true" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="ZongCheng" DataField="ZongCheng" TextAlign="Center"
                      HeaderText="总成" EnableColumnEdit="false">
                    <Editor>
                        <f:DropDownList ID="ddlZongCheng" Required="true" runat="server">
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                 
                        <f:RenderField Width="90px" ColumnID="BaseNum" DataField="BaseNum" TextAlign="Center"
                      HeaderText="底数" EnableColumnEdit="false">
                    <Editor>
                        <f:NumberBox  MinValue="1" ID="NumberBox1" Required="true" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                          <f:RenderField Width="100px" ColumnID="WorkShop" DataField="WorkShop" TextAlign="Center"
                      HeaderText="生产车间" EnableColumnEdit="false">
                    <Editor>
                        <f:DropDownList ID="ddlWorkShop" Required="true" runat="server">
                            <f:ListItem Text="毛坯车间" Value="毛坯车间" />
                            <f:ListItem Text="焊接车间" Value="焊接车间" />
                            <f:ListItem Text="注塑车间" Value="注塑车间" />
                            <f:ListItem Text="装配车间" Value="装配车间" />
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="MainFrom" DataField="MainFrom" TextAlign="Center"
                      HeaderText="主要来源" EnableColumnEdit="false">
                    <Editor>
                        <f:DropDownList ID="txtMakeMethod" Required="true" runat="server">
                            <f:ListItem Text="采购" Value="采购" />
                            <f:ListItem Text="生产" Value="生产" />
                            <f:ListItem Text="发外加工" Value="发外加工" />
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                         <f:RenderField Width="100px" ColumnID="Sclass" DataField="Sclass" FieldType="String"
                      HeaderText="分类" EnableColumnEdit="false">
                    <Editor>
                        <f:DropDownList Required="false" runat="server" EnableEdit="true" ID="ddlItemClass">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                          <f:RenderField Width="80px" EnableColumnEdit="false" ColumnID="StoreHouse" DataField="StoreHouse"
                      HeaderText="仓库">
                    <Editor>
                       <f:DropDownList Required="false" runat="server" EnableEdit="true" ID="ddlStoreHouse">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
<f:RenderField Width="100px" ColumnID="SupplierId" DataField="SupplierId"
                      HeaderText="供应商" RendererFunction="renderProvider" >
                     
                    <Editor>
                         <f:DropDownList Required="false" runat="server" ID="ddlSupplierId">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                          <f:WindowField ColumnID="viewField" Hidden="false" HeaderText="备货确认单" Title="备货确认单" ToolTip="备货确认单" TextAlign="Center" Icon="ApplicationViewIcons" WindowID="Window1" DataIFrameUrlFields="SN,FSN" DataIFrameUrlFormatString="~/ruilin/SendInstruction.aspx?id={0}&fsn={1}&k=1"
                            Width="100px" />
                      
                        <f:LinkButtonField ColumnID="deleteField"  TextAlign="Center" Icon="Delete" ToolTip="删除" HeaderText="删除"
                            ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                       <f:BoundField DataField="FSN" Hidden="false" Width="250px" HeaderText="FSN" />
                    <f:RenderField Width="100px" Hidden="false" ColumnID="ZuHe" DataField="ZuHe" TextAlign="Center"
                      HeaderText="ZuHe">
                    <Editor>
                        <f:TextBox ID="TextBox3" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                    <f:RenderField Width="100px" Hidden="false" ColumnID="ParentSN" DataField="ParentSN" TextAlign="Center"
                      HeaderText="ParentSN">
                    <Editor>
                        <f:TextBox ID="TextBox4" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                    <f:RenderField Width="100px" Hidden="false" ColumnID="SUBSN" DataField="SUBSN" TextAlign="Center"
                      HeaderText="SUBSN">
                    <Editor>
                        <f:TextBox ID="TextBox2" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                        <f:RenderField Width="100px" Hidden="false" ColumnID="savetype" DataField="savetype" TextAlign="Center"
                      HeaderText="savetype">
                    <Editor>
                        <f:TextBox ID="TextBox5" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>  
                        <f:RenderField Width="100px" Hidden="false" ColumnID="AllitemSN" DataField="AllitemSN" TextAlign="Center"
                      HeaderText="AllitemSN">
                    <Editor>
                        <f:TextBox ID="TextBox6" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                    </Columns>
      <Listeners>
                <f:Listener Event="rowselect" Handler="onGridRowSelect" />
                <f:Listener Event="rowdeselect" Handler="onGridRowDeselect" />
          <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
            </Listeners>
                </f:Grid>
                        
                    </Items>
                </f:Panel>
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
<script>

    var grid2ClientID = '<%= Grid2.ClientID %>';
    var grid3ClientID = '<%= Grid3.ClientID %>';
    var QuantityID = '<%=txtQuantity.ClientID%>';

    // 自定义编辑器获取函数（从Editor返回单元格）
    function editGetterName(editor) {
        return editor.getText();
    }

    // 自定义编辑器设置函数（从单元格进入Editor）
    function editSetterName(editor, val, columnId, rowId) {

        var grid2 = F(grid2ClientID);
        var rowValue = grid2.getRowValue(rowId);
        var rowCode = rowValue['ItemNo'];
        console.log(rowValue + '---' + rowCode);
        editor.setValue(rowCode, val);
    }

    
    function onGrid3RowClick(event, grid3RowId) {
        var grid2 = F(grid2ClientID);
        var grid2RowId = grid2.getSelectedCell()[0];
        var rowValue = this.getRowValue(grid3RowId);
        var Q = F(QuantityID);
        Q = Q.getValue();
        grid2.updateCellValue(grid2RowId, {
            'ItemNo': rowValue.ItemNo,
            'ItemName': rowValue.ItemName,
            'Spec': rowValue.Spec,
            'Material': rowValue.Material,
            'SurfaceDeal': rowValue.SurfaceDeal,
            'ProUsingQuantity': rowValue.ProUsingQuantity,
            'OrderUsingQuantity': rowValue.ProUsingQuantity * Q,
            'ZongCheng': rowValue.ZongCheng,
            'BaseNum': rowValue.BaseNum,
            'Sclass': rowValue.Sclass,
            'MainFrom': rowValue.MainFrom,
            'WorkShop': rowValue.WorkShop,
            'StoreHouse': rowValue.StoreHouse
        });

    }

    function onGridAfterEdit(event, value, params) {
        var me = this, columnId = params.columnId, rowId = params.rowId;
        if (columnId === 'ProUsingQuantity') {
            var pq = me.getCellValue(rowId, 'ProUsingQuantity');
            //var poq = me.getCellValue(rowId, 'ProOrgQuantity');
            var Q = F(QuantityID);
            //var poq = 100;// me.getCellValue(rowId, 'ProOrgQuantity');
            console.log("qqq::::" + Q.getValue());
            Q = Q.getValue();
            me.updateCellValue(rowId, 'OrderUsingQuantity', pq * Q);

        }
    }
    var provierjson = eval('<%=pstr%>');
    function renderProvider(value) {
        if (value == '' || value == undefined) {
            return '';
        }
        else {
            if (provierjson[value] == undefined) {
                return '';
            }
            return '<span style="color:red;">' + provierjson[value] + '</span>';
        }

    }
    </script>
 <script>
      


     // 递归遍历节点 rowData 的所有子节点
     function resolveRowChildren(rowData, callback) {
         var children = rowData.children;
         if (children && children.length) {
             for (var i = 0, count = children.length; i < count; i++) {
                 var item = children[i];
                 callback.apply(item);
                 resolveRowChildren(item, callback);
             }
         }
     }

     function onGridRowSelect(event, rowId) {
         var grid = this, rowData = grid.getRowData(rowId);

         var rowIds = [];
         resolveRowChildren(rowData, function () {
             rowIds.push(this.id);
         });

         // 本过程中不触发事件
         F.noEvent(function () {
             // 第二个参数true：保持现有选中项
             grid.selectRows(rowIds, { keep: true });
         });
     }

     function onGridRowDeselect(event, rowId) {
         var grid = this, rowData = grid.getRowData(rowId);

         var rowIds = [];
         resolveRowChildren(rowData, function () {
             rowIds.push(this.id);
         });

         // 本过程中不触发事件
         F.noEvent(function () {
             // 第二个参数true：保持现有选中项
             grid.deselectRows(rowIds);
         });
     }

    </script>
    <script type="text/javascript">
        function bomState(value) {
            return value == 0 ? "<span style='color:red;'>未生成</span>" : "<span style='color:blue;'>已生成</span>";
        }
        function toBOM(ID, ORDERNO, ITEMNO, Q, TITLE) {
            parent.addExampleTab({
                id: ID + '_tab',
                iframeUrl: 'ruilin/RealBom.aspx?sn=' + ID + '&od=' + ORDERNO + '&id=' + ITEMNO + '&q=' + Q,
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
