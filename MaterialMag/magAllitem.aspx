﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magAllitem.aspx.cs" Inherits="AppBoxPro.MaterialMag.magAllitem" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Grid1" EnableFStateValidation="false" runat="server" />
 <f:Grid ID="Grid1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="" EnableCollapse="false"
            runat="server" DataKeyNames="SN" AllowCellEditing="true" ClicksToEdit="1"  OnPreDataBound="Grid1_PreDataBound"  EnableTextSelection="true" 
            DataIDField="SN" EnableCheckBoxSelect="true"  KeepCurrentSelection="false" OnPageIndexChange="Grid1_PageIndexChange"  AllowSorting="true" OnSort="Grid1_Sort"  SortField="SN" OnRowCommand="Grid1_RowCommand"  EnterNavigate="true" TabVerticalNavigate="true" EnterVerticalNavigate="false"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowDataBound="Grid1_RowDataBound" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">           
     <Toolbars>
                 <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                       <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="名称、料号、规格、材质、类别、车间、来源、仓库、总成、表面处理搜索"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click" Width="500px">
                                </f:TwinTriggerBox>
                         
                         <f:Button ID="btnSearch" Pressed="true" ValidateForms="haha"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                        <f:Button ID="btnBack" Pressed="true" ValidateForms="haha"  Hidden="true" runat="server" Text="后退" Icon="RewindBlue" OnClick="btnBack_Click"></f:Button>
                        <f:TextBox ID="txtClickColsName" runat="server" Hidden="true" Label="列名" LabelWidth="50px"></f:TextBox>
                        <f:FileUpload runat="server" Width="380px" LabelWidth="60px" ID="filePhoto" EmptyText="请选择Excel模板" Label="模板" Required="true" ButtonIcon="Add"
                    ShowRedStar="true">
                </f:FileUpload>
                        <f:Button ID="btnImport" OnClick="btnImport_Click" Icon="PageWhiteExcel" Text="导入Excel" EnablePostBack="true" runat="server">
                        </f:Button>   
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnExcel" EnableAjax="false" DisableControlBeforePostBack="false" OnClick="Button1_Click" runat="server" Icon="PageExcel"    Text="导出Excel">
                            </f:Button> 
                        </Items>
                     </f:Toolbar>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnNew"   Text="新增" Icon="Add" EnablePostBack="false" runat="server">
                        </f:Button>


                     <f:DropDownBox runat="server" ID="ddlProvider" LabelWidth="70px" Width="350px"  Label="供应商"  EmptyText="请选择供应商" EnableMultiSelect="false" MatchFieldWidth="false">
                    <PopPanel>
                        <f:Grid ID="Grid2" Width="800px" Height="300px" Hidden="true"
                            DataIDField="SN" DataTextField="Name"  EnableMultiSelect="false" KeepCurrentSelection="true"
                            PageSize="10" ShowBorder="true" ShowHeader="false" SortField="SN"
                            AllowPaging="true" IsDatabasePaging="false" runat="server" EnableCheckBoxSelect="true"
                            DataKeyNames="SN" EnableRowClickEvent="true" OnRowClick="Grid2_RowClick"
                            >
                            <Columns>
                                <f:RowNumberField />
                                 <f:BoundField ColumnID="ProviderNo" DataField="ProviderNo" SortField="ProviderNo" Width="120px" HeaderText="供应商代号" />
                        <f:BoundField ColumnID="Name"  DataField="Name" SortField="Name" Width="300px" HeaderText="供应商名称" />
                        <f:BoundField ColumnID="Rank"  DataField="Rank" SortField="Rank" Width="100px" HeaderText="供应商等级" />
                        <f:BoundField ColumnID="Stype"  DataField="Stype" SortField="Stype" Width="90px" HeaderText="类型" />
                       
                        <f:BoundField ColumnID="ContactMan"  DataField="ContactMan" SortField="ContactMan" Width="90px" HeaderText="联系人" />

                        <f:BoundField ColumnID="Telephone"  DataField="Telephone" Width="200px" HeaderText="联系电话" />
 <f:BoundField ColumnID="Address"  DataField="Address" ExpandUnusedSpace="true" MinWidth="300px" HeaderText="地址" />
                            </Columns>
                            <Toolbars>
                                <f:Toolbar runat="server" Position="Top">
                                    <Items>
                                        <f:TwinTriggerBox Width="300px" runat="server" EmptyText="品号、品名搜索" ShowLabel="false" ID="ttbSearch"
                                            ShowTrigger1="false" OnTrigger1Click="ttbSearchMessage2_Trigger1Click" OnTrigger2Click="ttbSearchMessage2_Trigger2Click"
                                            Trigger1Icon="Clear" Trigger2Icon="Search">
                                        </f:TwinTriggerBox>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Grid>
                    </PopPanel>
                </f:DropDownBox>




                          <f:Button ID="Button1"  OnClick="btnSave_Click" runat="server" Icon="SystemSave"    Text="保存">
                            </f:Button> 
                        <f:Button ID="btnDelete" Hidden="false" OnClick="btnDeleteSelected_Click" Text="删除选中行" Icon="Delete"  runat="server">
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
                <f:RowNumberField />
                  <f:RenderField Width="150px" ColumnID="ItemNo"   EnableLock="true" Locked="true"  DataField="ItemNo"
                      HeaderText="料号" EnableColumnEdit="false">
                    <Editor>
                        <f:TextBox ID="tbxItemNo"  Required="true" EnableBlurEvent="true" OnBlur="tbxItemNo_Blur" ValidateRequestMode="Enabled" RequiredMessage="料号不能为空" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="200px"  EnableLock="true" Locked="true"  ColumnID="ItemName" DataField="ItemName"
                      HeaderText="名称">
                    <Editor>
                        <f:TextBox ID="tbxItemName" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="180px" ColumnID="Spec" DataField="Spec"
                      HeaderText="规格">
                    <Editor>
                        <f:TextBox ID="tbxSpec" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="Material" DataField="Material"
                      HeaderText="材质">
                    <Editor>
                        <f:TextBox ID="tbxMaterialNo" Required="false" runat="server">
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
                 <f:RenderField Width="100px" ColumnID="Price" DataField="Price"
                      HeaderText="单价">
                    <Editor>
                        <f:TextBox ID="txtPrice" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="SupplierId" DataField="SupplierId"
                      HeaderText="供应商" RendererFunction="renderProvider"  >
                     
                    <Editor>
                         <f:DropDownList Required="false" runat="server" EnableEdit="true"  ID="ddlSupplierId">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="UnitWeight" DataField="UnitWeight"
                      HeaderText="单重">
                    <Editor>
                        <f:TextBox ID="txtUnitWeight" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="ProcessCost" DataField="ProcessCost"
                      HeaderText="工艺加工费">
                    <Editor>
                        <f:TextBox ID="txtProcessCost" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="ProcessCostType" DataField="ProcessCostType"
                      HeaderText="加工费类型">
                    <Editor>
                        <f:TextBox ID="txtProcessCostType" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="90px" ColumnID="SurfaceDeal" DataField="SurfaceDeal"
                      HeaderText="表面处理">
                    <Editor>
                        <f:TextBox ID="tbxItemColor" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="80px" ColumnID="BaseNum" DataField="BaseNum"
                      HeaderText="底数">
                    <Editor>
                        <f:NumberBox ID="tbxBaseNumber" MinValue="1" Required="false" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="100px" ColumnID="Sclass" DataField="Sclass" FieldType="String"
                      HeaderText="类别" EnableColumnEdit="true">
                    <Editor>
                        <f:DropDownList Required="false" runat="server" EnableEdit="true" ID="ddlItemClass">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
               
                               <f:RenderField Width="80px" ColumnID="WorkShop" DataField="WorkShop"
                      HeaderText="车间">
                    <Editor>
                        <f:DropDownList Required="false" runat="server" EnableEdit="true" ID="ddlWorkShop">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="100px" ColumnID="MainFrom" DataField="MainFrom"
                      HeaderText="主要来源">
                    <Editor>
                        <f:DropDownList Required="false" runat="server" EnableEdit="true" ID="ddlMainFrom">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="80px" ColumnID="StoreHouse" DataField="StoreHouse"
                      HeaderText="仓库">
                    <Editor>
                       <f:DropDownList Required="false" runat="server" EnableEdit="true" ID="ddlStoreHouse">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
  <f:RenderField Width="100px" ColumnID="ZongCheng" DataField="ZongCheng" TextAlign="Center"
                      HeaderText="总成">
                    <Editor>
                      <f:DropDownList ID="ddlZongCheng" Required="true" runat="server">
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                 <f:TemplateField HeaderText="查看产品" Width="100px" ToolTip="查看订查看产品单明细">
                    <ItemTemplate>
                        <a href="javascript:;" style="color:blue;text-decoration:underline;" onclick="lookOrder('<%#Eval("SN")%>','<%#Eval("ItemNo")%>')">查看产品</a>
                    </ItemTemplate>
                </f:TemplateField>
                 <f:TemplateField HeaderText="查看工艺" Width="100px" ToolTip="查看工艺">
                    <ItemTemplate>
                        <a href="javascript:;" style="color:blue;text-decoration:underline;" onclick="lookProcess('<%#Eval("SN")%>','<%#Eval("ItemNo")%>')">查看工艺</a>
                    </ItemTemplate>
                </f:TemplateField>
               <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                 
               
            </Columns>
        </f:Grid>
       
    </form>
    <script>
        var provierjson =eval('<%=pstr%>');
        console.log(provierjson);

        function renderGender(value) {
            if (value == 1)
                return '<span style="color:red;">早餐</span>';
            else if (value == 2)
                return '<span style="color:green;">午餐</span>';
            else if (value == 3)
                return '<span style="color:orange;">晚餐</span>';
        }
        function renderState(value) {
            if (value == "已发布")
                return '<span style="color:red;">已发布</span>';
            else
                return '<span style="color:gray;">未发布</span>';
        }
        function renderProvider(value) {
            if (value== '' || value == undefined)
            {
                return '';
            }
            else
            {
                if (provierjson[value] == undefined)
                {
                    return '';
                }
                return '<span style="color:red;">' + provierjson[value] + '</span>';
            }
            
        }
        function lookProcess(sn, itemno) {
            parent.addExampleTab({
                id: 'showProcess_' + sn + '_tab',
                iframeUrl: 'MaterialMag/showProcess.aspx?sn=' + escape(sn) + '&itemno=' + escape(itemno),
                title: '查看工艺',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });
        }
        function lookOrder(ID, itemno) {
            parent.addExampleTab({
                id: 'LooKProduction_'+ID + '_tab',
                iframeUrl: 'MaterialMag/LooKProduction.aspx?pid=' + escape(ID) + '&itemno=' + escape(itemno),
                title: '查看产品',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });
        }
    </script>
</body>
</html>