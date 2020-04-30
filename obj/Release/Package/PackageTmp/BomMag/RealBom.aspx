<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RealBom.aspx.cs" Inherits="AppBoxPro.BomMag.RealBom" %>

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

</head>
<body>
       <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />

        <f:Panel ID="Panel1" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="0px" runat="server" Layout="VBox">
            <Items>

         <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="true" runat="server" BodyPadding="10px"
                    Title="订单详细" Collapsed="false" EnableCollapse="true"  >
                      <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" Hidden="false">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="关闭" Hidden="true">
                        </f:Button>                      
                        <f:Button ID="btnSaveForm" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveForm_Click"
                            runat="server" Text="保存">
                        </f:Button>
                         <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="保存后关闭" Hidden="true">
                        </f:Button>
                         <f:DropDownList ID="ddlbomver" runat="server" Label="可选BOM版本" Width="200px" LabelWidth="130px"></f:DropDownList>
                        <f:TextBox ID="txtCurProno" Hidden="true" runat="server" Label="选择的型号"></f:TextBox>
                         <f:DropDownBox runat="server" Hidden="true" ID="ddlBOM" Label="BOM版本"  EmptyText="请选择BOM版本" EnableMultiSelect="false" MatchFieldWidth="false">
                    <PopPanel>
                        <f:Grid ID="Grid2" Width="800px" Height="300px" Hidden="true"
                            DataIDField="SN" DataTextField="Ver" EnableMultiSelect="false" KeepCurrentSelection="true"
                            PageSize="10" ShowBorder="true" ShowHeader="false"
                            AllowPaging="true" IsDatabasePaging="false" runat="server" EnableCheckBoxSelect="true"
                            DataKeyNames="SN" EnableRowClickEvent="true" OnRowClick="Grid2_RowClick"
                            >
                            <Columns>
                                <f:RowNumberField />
                                 <f:BoundField ColumnID="ProNo" DataField="ProNo" TextAlign="Center" SortField="ProNo"  Width="150px" HeaderText="产品编号" />
                        <f:BoundField ColumnID="ProName" DataField="ProName" TextAlign="Center" ExpandUnusedSpace="true" SortField="ProName"  Width="200px" HeaderText="产品名称" />
                        <f:BoundField ColumnID="ClientProNo"  DataField="ClientProNo" Hidden="true" TextAlign="Center" SortField="ClientProNo"  Width="200px" HeaderText="客户产品型号" />
                        
                        <f:BoundField ColumnID="ClientCode"  DataField="ClientCode" Hidden="true" TextAlign="Center" SortField="ClientCode"  Width="100px" HeaderText="客户代号" />
                        <f:BoundField ColumnID="BomDate" Hidden="true"  DataField="BomDate" TextAlign="Center" SortField="BomDate" DataFormatString="{0:yyyy-MM-dd}"  Width="100px" HeaderText="日期" />
                        <f:BoundField  ColumnID="Ver" DataField="Ver"  TextAlign="Center"  Width="60px" HeaderText="版本" />
                        <f:BoundField ColumnID="FileNo"  DataField="FileNo"  TextAlign="Center" SortField="FileNo"  Width="200px" HeaderText="文件编号" />
                         <f:BoundField ColumnID="ChineseName"  DataField="ChineseName"  TextAlign="Center"  Width="80px" HeaderText="录入人" />
                         <f:BoundField ColumnID="InputeDate"  DataField="InputeDate"  TextAlign="Center" SortField="InputeDate" DataFormatString="{0:yyyy-MM-dd}"  Width="100px" HeaderText="录入日期" />
                            </Columns>
                            <Toolbars>
                                <f:Toolbar runat="server" Position="Top">
                                    <Items>
                                        <f:TwinTriggerBox Width="300px" runat="server" EmptyText="在姓名中查找" ShowLabel="false" ID="ttbSearch"
                                            ShowTrigger1="false" OnTrigger1Click="ttbSearch_Trigger1Click" OnTrigger2Click="ttbSearch_Trigger2Click"
                                            Trigger1Icon="Clear" Trigger2Icon="Search">
                                        </f:TwinTriggerBox>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Grid>
                    </PopPanel>
                </f:DropDownBox>


                         <f:Button ID="btnMakeBom" Icon="SystemSaveClose" OnClick="btnMakeBom_Click"
                            runat="server" Text="重新生成BOM" >
                        </f:Button>
                        <f:TextBox ID="txtBomver" NextFocusControl="txtColor" CssStyle="color:red;" Readonly="true" LabelWidth="130px" runat="server" Label="当前BOM版本">
                                </f:TextBox>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Label ID="msglab" CssStyle="width:200px; height:20px;color:red;font-weight:bolder;font-size:30px;line-height:20px;" runat="server"></f:Label>
                    </Items>
                </f:Toolbar>
            </Toolbars>


<Rows>
                        <f:FormRow runat="server">
                            <Items>
                                 <f:TextBox ID="txtOrderNo" Readonly="true" NextFocusControl="txtClinetNo" LabelWidth="130px" Width="400px"  runat="server" Label="订单编号" Required="true" ShowRedStar="true">
                                </f:TextBox>
                                <f:TextBox ID="txtClinetNo" NextFocusControl="txtItemNo"  runat="server" FocusOnPageLoad="true" Label="客人编号" Required="true" ShowRedStar="true">
                                </f:TextBox>
                                <f:TextBox ID="txtItemNo" NextFocusControl="txtItemName" AutoPostBack="true" LabelWidth="130px" Width="400px"  runat="server" Label="型号" OnBlur="txtItemNo_Blur" Required="true" OnTextChanged="txtItemNo_TextChanged" ShowRedStar="true">
                                </f:TextBox>
                              
                                <f:TextBox ID="txtItemName" NextFocusControl="txtQuantity" runat="server" Label="产品名称" Required="true" ShowRedStar="true" >
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                              
                                 
                            </Items>
                        </f:FormRow>
                       <f:FormRow runat="server" ID="searchRow" Hidden="true">
                            <Items>
                                <f:TextBox ID="txtisOpen" Hidden="false" runat="server"></f:TextBox>
                                </Items>
                           </f:FormRow>
                         <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                 <f:NumberBox ID="txtQuantity" NextFocusControl="txtUnit" runat="server" LabelWidth="130px" Width="400px"  Label="数量">
                                </f:NumberBox>
                                <f:TextBox ID="txtUnit" NextFocusControl="txtColor" runat="server" Label="单位">
                                </f:TextBox>
                                
                                <f:TextBox ID="txtColor" NextFocusControl="txtConutryVer" runat="server" LabelWidth="130px" Width="400px"  Label="颜色">
                                </f:TextBox>
                                <f:TextBox ID="txtInputer" runat="server" Label="录入人">
                                </f:TextBox>
                                </Items>
                             </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                               
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                              <f:RadioButtonList ID="rbtIsNew" Label="是否新产品" LabelWidth="130px" Width="400px"  runat="server">
                                  <f:RadioItem Text="是" Value="是" />
                                  <f:RadioItem Text="否" Value="否" />
                              </f:RadioButtonList>
                              <f:RadioButtonList ID="rbtIspacking" Label="是否新包材" runat="server">
                                  <f:RadioItem Text="是" Value="是" />
                                  <f:RadioItem Text="否" Value="否" />
                              </f:RadioButtonList>
                                <f:TextBox ID="txtConutryVer" NextFocusControl="tbxDemand1" runat="server" LabelWidth="130px" Width="400px"  Label="国家包材版本">
                                </f:TextBox>
                              <f:RadioButtonList ID="rbtIsChange" Label="是否变更" runat="server">
                                  <f:RadioItem Text="是" Value="是" />
                                  <f:RadioItem Text="否" Value="否" />
                              </f:RadioButtonList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                              
                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:TextArea ID="tbxDemand1" Height="100px" LabelWidth="130px" Width="400px"  runat="server" Label="变更备注1">
                                </f:TextArea>  
                                 <f:TextArea ID="tbxDemand2" Height="100px" runat="server" Label="变更备注2">
                                </f:TextArea>    
                                <f:TextArea ID="tbxRemark" runat="server" LabelWidth="130px" Label="备注">
                                </f:TextArea>                             
                            </Items>
                        </f:FormRow>
                    <f:FormRow ID="FormRow1" runat="server" Hidden="true">
                            <Items>
                              <f:TextArea ID="tbxDemand3" Height="60px" runat="server" Label="要求3">
                                </f:TextArea>      
                                  <f:TextArea ID="tbxDemand4" Height="60px" runat="server" Label="要求4">
                                </f:TextArea>                                 
                            </Items>
                        </f:FormRow>
                         <f:FormRow ID="FormRow2" runat="server" Hidden="true">
                            <Items>
                                <f:TextArea ID="tbxDemand5" Height="60px" runat="server" Label="要求5">
                                </f:TextArea>  
                                <f:TextArea ID="tbxDemand6" Height="60px" runat="server" Label="要求6">
                                </f:TextArea>                                
                            </Items>
                        </f:FormRow>
                           <f:FormRow ID="FormRow3" runat="server" Hidden="true">
                            <Items>
                              <f:TextArea ID="tbxDemand7" Height="60px" runat="server" Label="要求7">
                                </f:TextArea> 
                                  <f:TextArea ID="tbxDemand8" Height="60px" runat="server" Label="要求8">
                                </f:TextArea>                           
                            </Items>
                        </f:FormRow>
                           <f:FormRow ID="FormRow5" runat="server" Hidden="true">
                            <Items>
                                   <f:TextArea ID="tbxDemand9" Height="60px" runat="server" Label="要求">
                                </f:TextArea>  
                                 <f:TextArea ID="tbxDemand10" Height="60px" runat="server" Label="要求10">
                                </f:TextArea>                           
                            </Items>
                        </f:FormRow>
                           <f:FormRow ID="FormRow6" runat="server" Hidden="true">
                            <Items>
                                <f:TextArea ID="tbxDemand11" Height="60px" runat="server" Label="要求11">
                                </f:TextArea>  
                                 <f:TextArea ID="tbxDemand12" Height="60px" runat="server" Label="要求12">
                                </f:TextArea>                                
                            </Items>
                        </f:FormRow>




                        <f:FormRow runat="server">
                            <Items>
                                
                                <f:HiddenField ID="txtID" runat="server"></f:HiddenField>
                                <f:HiddenField ID="txtProName2" runat="server"></f:HiddenField>
                                <f:HiddenField ID="txtFSN" runat="server"></f:HiddenField>
                            </Items>
                        </f:FormRow>
                    </Rows>                
         </f:Form>

                <f:Panel ID="Panel10" ShowBorder="true" Margin="0px" ShowHeader="false" BoxFlex="1" Layout="Fit" runat="server">
                     <Items>
                  <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true" AllowCellEditing="true" ClicksToEdit="1"  SummaryPosition="Bottom" EnableSummary="true" EnableTextSelection="true" 
                    DataKeyNames="SN" AllowSorting="true"    SortField="SN" OnSort="Grid1_Sort"
                      AllowPaging="false" IsDatabasePaging="true" OnPageIndexChange="Grid1_PageIndexChange" OnRowCommand="Grid1_RowCommand" EnterSameAsTab="true" EnableTree="true" TreeColumn="ItemNo" DataIDField="SUBSN" DataParentIDField="ParentSN"   EnableTreeIcons="false" ExpandAllTreeNodes="true" OnRowDataBound="Grid1_RowDataBound">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="请输入料号或名称或规格"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                                
                                <f:Button ID="btnSearch" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                                <f:Button ID="btnCopy" Pressed="true"  runat="server" Text="复制" Icon="PageCopy" OnClick="btnCopy_Click"></f:Button>
                                
                               <f:Button ID="btnNew" OnClick="btnNew_Click"  Text="新增" Icon="Add" runat="server">
                        </f:Button>
                                <f:Button ID="btnZUHE" OnClick="btnZUHE_Click"  Text="添加组合件" Icon="Add" runat="server">
                        </f:Button>
                          <f:Button ID="Button1"  OnClick="btnSave_Click" runat="server" Icon="SystemSave"    Text="保存">
                            </f:Button> 
                         <f:Button ID="btnDelete" Hidden="false" OnClick="btnDeleteSelected_Click" Text="删除" Icon="Delete"  runat="server">
                        </f:Button>
                                <f:DropDownList ID="ddlDept" EnableEdit="true" Label="接收部门" LabelWidth="80px" Width="220px" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true" AutoSelectFirstItem="true" runat="server">

                                </f:DropDownList>
                                <f:DropDownList ID="ddlUser" Label="接收人" EnableEdit="true"  LabelWidth="70px" Width="160px"  runat="server">

                                </f:DropDownList>
                          <f:Button ID="btnPLSend" Hidden="false" OnClick="btnPLSend_Click" Text="批量发送确认单" Icon="PageWhiteStack"  runat="server">
                        </f:Button>        
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
                                  <f:Button ID="Button2" EnableAjax="false" DisableControlBeforePostBack="false"  runat="server" Icon="PageExcel"  OnClick="Button2_Click"    Text="导出Excel">
                            </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
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
                        <f:RowNumberField EnableTreeNumber="true" />
                         <f:RenderField Width="90px" ColumnID="Seq" DataField="Seq" EnableColumnEdit="false" TextAlign="Left"
                      HeaderText="序号">
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
                        <f:BoundField ColumnID="WorkShop" DataField="WorkShop"    Width="80px" HeaderText="生产车间" />      <f:BoundField ColumnID="StoreHouse" DataField="StoreHouse"    Width="80px" HeaderText="仓库" />                                   

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
                        <f:TextBox ID="txtItemName2" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField  MinWidth="50px" EnableColumnEdit="false"  ColumnID="Spec" DataField="Spec" TextAlign="Center" ExpandUnusedSpace="true"
                      HeaderText="规格">
                    <Editor>
                        <f:TextBox ID="txtSpec" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="150px" EnableColumnEdit="false"  ColumnID="Material" DataField="Material" TextAlign="Center"
                      HeaderText="材质">
                    <Editor>
                        <f:TriggerBox ID="txtMaterial" TriggerIcon="Search" OnTriggerClick="txtMaterial_TriggerClick" runat="server">
                        </f:TriggerBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="150px" EnableColumnEdit="false"  ColumnID="SurfaceDeal" DataField="SurfaceDeal" TextAlign="Center"
                      HeaderText="表面处理">
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
                 <f:RenderField Width="100px" EnableColumnEdit="false"  ColumnID="ZongCheng" DataField="ZongCheng" TextAlign="Center"
                      HeaderText="总成">
                    <Editor>
                        <f:DropDownList ID="ddlZongCheng" Required="true" runat="server">
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                 
                        <f:RenderField Width="90px" EnableColumnEdit="false"  ColumnID="BaseNum" DataField="BaseNum" TextAlign="Center"
                      HeaderText="底数">
                    <Editor>
                        <f:NumberBox  MinValue="1" ID="NumberBox1" Required="true" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                          <f:RenderField Width="100px" EnableColumnEdit="false"  ColumnID="WorkShop" DataField="WorkShop" TextAlign="Center"
                      HeaderText="生产车间">
                    <Editor>
                        <f:DropDownList ID="ddlWorkShop" Required="true" runat="server">
                            <f:ListItem Text="毛坯车间" Value="毛坯车间" />
                            <f:ListItem Text="焊接车间" Value="焊接车间" />
                            <f:ListItem Text="注塑车间" Value="注塑车间" />
                            <f:ListItem Text="装配车间" Value="装配车间" />
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                        <f:RenderField Width="100px" EnableColumnEdit="false"  ColumnID="MainFrom" DataField="MainFrom" TextAlign="Center"
                      HeaderText="主要来源">
                    <Editor>
                        <f:DropDownList ID="txtMakeMethod" Required="true" runat="server">
                            <f:ListItem Text="采购件" Value="采购件" />
                            <f:ListItem Text="厂内生产件" Value="厂内生产件" />
                            <f:ListItem Text="委外生产件" Value="委外生产件" />
                            <f:ListItem Text="客供件" Value="客供件" />
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                         <f:RenderField Width="100px" EnableColumnEdit="false"  ColumnID="Sclass" DataField="Sclass" FieldType="String"
                      HeaderText="分类" >
                    <Editor>
                        <f:DropDownList Required="false" runat="server" EnableEdit="true" ID="ddlItemClass">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                          <f:RenderField Width="80px" EnableColumnEdit="false"  ColumnID="StoreHouse" DataField="StoreHouse"
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
                          <f:LinkButtonField Width="100px" Icon="ApplicationViewIcons" TextAlign="Center" CommandName="StoreConfirm"  HeaderText="备货确认单" />
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" HeaderText="删除"
                            ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="60px" />

                        <f:BoundField DataField="FSN" Hidden="true" Width="250px" HeaderText="FSN" />
                    <f:RenderField Width="100px" Hidden="true" ColumnID="ZuHe" DataField="ZuHe" TextAlign="Center"
                      HeaderText="ZuHe">
                    <Editor>
                        <f:TextBox ID="TextBox3" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                    <f:RenderField Width="100px" Hidden="true" ColumnID="ParentSN" DataField="ParentSN" TextAlign="Center"
                      HeaderText="ParentSN">
                    <Editor>
                        <f:TextBox ID="TextBox4" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                    <f:RenderField Width="100px" Hidden="true" ColumnID="SUBSN" DataField="SUBSN" TextAlign="Center"
                      HeaderText="SUBSN">
                    <Editor>
                        <f:TextBox ID="TextBox2" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                        <f:RenderField Width="100px" Hidden="true" ColumnID="savetype" DataField="savetype" TextAlign="Center"
                      HeaderText="savetype">
                    <Editor>
                        <f:TextBox ID="TextBox5" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>  
                         <f:RenderField Width="100px" Hidden="true" ColumnID="AllitemSN" DataField="AllitemSN" TextAlign="Center"
                      HeaderText="AllitemSN">
                    <Editor>
                        <f:TextBox ID="TextBox1" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                          <f:WindowField ColumnID="remark" TextAlign="Center" Icon="ApplicationAdd" HeaderText="添加备注" DataToolTipField="Remark" WindowID="Window2"
                            Title="备注" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/BomMag/addRemark.aspx?id={0}&t=probmd"
                            Width="80px" />
                       <f:BoundField ColumnID="IsValid" Hidden="true" DataField="IsValid" HeaderText="IsValid"></f:BoundField>
                       <f:BoundField ColumnID="ISN" Hidden="true" DataField="ISN" HeaderText="ISN"></f:BoundField>

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
       <f:Window ID="Window1" Title="编辑"  EnableIFrame="true" runat="server" Hidden="true" 
            EnableMaximize="true" EnableResize="true"  ShowHeader="true"  Target="Top" IsModal="true" Width="1200px"
            Height="700px" Margin="0px" BodyPadding="0px;" CssClass="mywnd" OnClose="Window1_Close">
        </f:Window>
         <f:Window ID="Window2"  Title="编辑"  EnableIFrame="true" runat="server" Hidden="true" 
            EnableMaximize="true" EnableResize="true"  ShowHeader="false"  Target="Self" IsModal="false" Width="500px"
            Height="200px" Margin="0px" BodyPadding="0px;" IFrameUrl="sucess.aspx" CssClass="mywnd" OnClose="Window1_Close">
        </f:Window>
    </form>
     <script>

         var grid1ClientID = '<%= Grid1.ClientID %>';
         var grid3ClientID = '<%= Grid3.ClientID %>';
         var  QuantityID = '<%=txtQuantity.ClientID%>';

         // 自定义编辑器获取函数（从Editor返回单元格）
         function editGetterName(editor) {
             return editor.getText();
         }

         // 自定义编辑器设置函数（从单元格进入Editor）
         function editSetterName(editor, val, columnId, rowId) {

             var grid1 = F(grid1ClientID);
             var rowValue = grid1.getRowValue(rowId);
             var rowCode = rowValue['ItemNo'];
             console.log(rowValue + '---' + rowCode);
             editor.setValue(rowCode, val);
         }


         function onGrid3RowClick(event, grid3RowId) {
             var grid1 = F(grid1ClientID);
             var grid1RowId = grid1.getSelectedCell()[0];
             var rowValue = this.getRowValue(grid3RowId);
             var Q = F(QuantityID);
             Q = Q.getValue();

             grid1.updateCellValue(grid1RowId, {
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
                 var Q = F(QuantityID);
                 //var poq = 100;// me.getCellValue(rowId, 'ProOrgQuantity');
                 console.log("qqq::::" + Q.getValue());
                 Q = Q.getValue();
                 me.updateCellValue(rowId, 'OrderUsingQuantity', pq * Q);

             }
         }
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

        function renderGender(value) {
            return value == 1 ? '男' : '女';
        }

        var gridClientID = '<%= Grid1.ClientID %>';

        function updateGridRow(rowId, values) {
            var grid = F(gridClientID);

            // cancelEdit用来取消编辑
            grid.cancelEdit();

            grid.updateCellValue(rowId, values);
        }

    </script>
</body>
</html>
