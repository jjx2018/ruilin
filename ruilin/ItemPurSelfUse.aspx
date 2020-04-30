<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemPurSelfUse.aspx.cs" Inherits="AppBoxPro.ruilin.ItemPurSelfUse" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1"   EnableFStateValidation="false" runat="server" />
         <f:Panel ID="Panel1" runat="server" ShowBorder="false" EnableCollapse="false"  IsViewPort="true"
            Layout="VBox" ShowHeader="false" Title="面板（Layout=VBox BodyPadding=5 BoxConfigChildMargin=0 0 5 0）"
            BoxConfigChildMargin="0 0 0 0" BodyPadding="0">
            <Items>
                
                        
        <f:Grid ID="Grid1" Height="300px"  ShowBorder="false" ShowHeader="false" Title="" EnableCollapse="false"
            runat="server" DataKeyNames="SN" AllowCellEditing="true" ClicksToEdit="2"  OnPreDataBound="Grid1_PreDataBound"  EnableTextSelection="true" 
            DataIDField="SN" EnableCheckBoxSelect="true"  KeepCurrentSelection="true" OnPageIndexChange="Grid1_PageIndexChange"  AllowSorting="true" OnSort="Grid1_Sort"  SortField="SN" OnRowCommand="Grid1_RowCommand"  EnterSameAsTab="true"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true">
            <Toolbars>
                 <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                       <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="名称、料号、规格、材质、类别搜索"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click" Width="400px">
                                </f:TwinTriggerBox>
                         
                         <f:Button ID="btnSearch" Pressed="true" ValidateForms="haha"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                         <f:ToolbarFill runat="server"></f:ToolbarFill>
                          <f:Button ID="btnExcel" EnableAjax="false" DisableControlBeforePostBack="false" OnClick="Button1_Click" runat="server" Icon="PageExcel"    Text="导出Excel">
                            </f:Button> 
                        </Items>
                     </f:Toolbar>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnNew"   Text="新增数据" Icon="Add" EnablePostBack="false" runat="server">
                        </f:Button>
                          <f:Button ID="Button1"  OnClick="btnSave_Click" runat="server" Icon="SystemSave"    Text="保存数据">
                            </f:Button> 
                        <f:Button ID="btnDelete" Hidden="false" OnClick="btnDeleteSelected_Click" Text="删除选中行" Icon="Delete"  runat="server">
                        </f:Button>
                         <f:Button ID="btnAddPur" Hidden="false" OnClick="btnAddPur_Click" Text="添加采购" Icon="ApplicationFormAdd"  runat="server">
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
                      HeaderText="料号">
                    <Editor>
                        <f:TextBox ID="tbxItemNo"  Required="true" runat="server">
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
                      HeaderText="供应商" RendererFunction="renderProvider" >
                     
                    <Editor>
                         <f:DropDownList Required="false" runat="server" ID="ddlSupplierId">
                            
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
               
                
               <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                 
               
            </Columns>
        </f:Grid>

                   
                        <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" DataKeyNames="SN" AllowSorting="true"
                    OnSort="Grid2_Sort" SortField="SN" SortDirection="DESC" AllowPaging="false"
                    IsDatabasePaging="true" OnPreDataBound="Grid2_PreDataBound" OnRowCommand="Grid2_RowCommand"
                    OnPageIndexChange="Grid2_PageIndexChange" AllowCellEditing="true" ClicksToEdit="2" TabVerticalNavigate="true" EnterSameAsTab="true">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar3" runat="server">
                            <Items>
 <f:DropDownBox runat="server" ID="ddlOrderno" Hidden="true"  Label="订单号"  EmptyText="请从下拉表格中选择" MatchFieldWidth="false" Values="105" 
                    EnableMultiSelect="false" EnableEdit="true"  AutoPostBack="true"   OnTextChanged="ddlOrderno_TextChanged">
                    <PopPanel>
                        <f:Grid ID="Grid4" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="SN" DataTextField="Name"
                            DataKeyNames="SN" AllowSorting="true" OnRowClick="Grid4_RowClick" EnableRowClickEvent="true" SortDirection="ASC" SortField="SN" Hidden="true" Width="550px" Height="300px" EnableMultiSelect="false">
                            <Columns>
                               <f:BoundField DataField="OrderNo" TextAlign="Center" SortField="OrderNo" Width="130px" HeaderText="订单编号" />
                        <f:BoundField DataField="ItemNo" TextAlign="Center" SortField="ItemNo"   Width="130px" HeaderText="型号" />
                          <f:BoundField DataField="ItemName" TextAlign="Center" SortField="ItemName"   Width="200px" HeaderText="产品名称" />    
                         
                        <f:BoundField DataField="Quantity" TextAlign="Center" SortField="Quantity" Width="80px" HeaderText="数量" />
                            </Columns>
                        </f:Grid>
                    </PopPanel>
                </f:DropDownBox>
                                 <f:TextBox ID="txtOrderno" Label="订单号" Required="true" runat="server">
                        </f:TextBox>
                                   <f:TextBox ID="txtProno" Label="品号" Required="true" runat="server">
                        </f:TextBox>
                                   <f:TextBox ID="txtProName" Label="品名" Required="true" runat="server">
                        </f:TextBox>
                                 
                                <f:DatePicker ID="XDDate" DateFormatString="yyyy-MM-dd" LabelWidth="80px"  Width="200px"    runat="server" Label="下单日期" ></f:DatePicker>
                                <f:DatePicker ID="JHdate" DateFormatString="yyyy-MM-dd" LabelWidth="80px"  Width="200px"    runat="server" Label="交货日期"   ></f:DatePicker>
                                 




                                 

                               
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                 <f:Button ID="Button3" EnableAjax="false" DisableControlBeforePostBack="false"  runat="server" Icon="PageExcel"  OnClick="Button3_Click"  Text="导出Excel">
                            </f:Button>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar runat="server">
                            <Items>
                                <f:TextBox ID="txtContactMan" Label="联系人" Required="true" runat="server">
                        </f:TextBox>
                                 <f:TextBox ID="txtTel" Label="电话" Required="true" runat="server">
                        </f:TextBox>
                                 <f:TextBox ID="txtFax" Label="传真" Required="true" runat="server">
                        </f:TextBox>
                                
                                 <f:Button ID="btnPurSave" runat="server" OnClick="btnPurSave_Click" Icon="Add"   Text="生成采购单">
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
                      
                       <f:RenderField Width="150px" ColumnID="Provider" DataField="Provider"  
                      HeaderText="供应商"  RendererFunction="renderProvider">
                    <Editor>
                        
                         <f:DropDownList Required="false" runat="server" ID="ddlProvider">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="Quantity" DataField="Quantity" TextAlign="Center"
                      HeaderText="采购数量">
                    <Editor>
                        <f:NumberBox  MinValue="1" ID="NumberBox1" Required="true" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                        <f:RenderField Width="150px" ColumnID="ItemNo" DataField="ItemNo"
                      HeaderText="料号">
                    <Editor>
                        <f:TextBox ID="TextBox1" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="300px" ColumnID="ItemName" DataField="ItemName"
                      HeaderText="名称">
                    <Editor>
                        <f:TextBox ID="TextBox2" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="180px" ColumnID="Spec" DataField="Spec"
                      HeaderText="规格">
                    <Editor>
                        <f:TextBox ID="TextBox3" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                        <f:RenderField Width="180px" ColumnID="SN" DataField="SN"
                      HeaderText="ALLITEMSN">
                    <Editor>
                        <f:TextBox ID="TextBox4" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                     
                        <f:WindowField ColumnID="editField" TextAlign="Center" Icon="Pencil" ToolTip="编辑" WindowID="Window1"
                            Title="编辑" DataIFrameUrlFields="SN" Hidden="true" DataIFrameUrlFormatString="~/ruilin/Provideredit.aspx?id={0}"
                            Width="50px" />
                        <f:LinkButtonField ColumnID="deleteField" Hidden="false" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                    </Columns>
                </f:Grid> 


                   
            </Items>
        </f:Panel>


















     </form>
    <script>
        var provierjson = eval('<%=pstr%>');
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
</body>
</html>