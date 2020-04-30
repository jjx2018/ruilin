<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaseDataMag.aspx.cs" Inherits="AppBoxPro.BaseDataMag.BaseDataMag" %>

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
            DataIDField="SN" EnableCheckBoxSelect="true"  KeepCurrentSelection="true" OnPageIndexChange="Grid1_PageIndexChange"  AllowSorting="true" OnSort="Grid1_Sort"  SortField="SN" OnRowCommand="Grid1_RowCommand"  AllowColumnLocking="false"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" EnterSameAsTab="true" TabEditableCell="true" OnAfterEdit="Grid1_AfterEdit">
            <Toolbars>
                 <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                       <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="仓库代码、仓库名称搜索"
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
                        <f:Button ID="btnNew"   Text="新增" Icon="Add" OnClick="btnNew_Click" EnablePostBack="true" runat="server">
                        </f:Button>
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
                  <f:RenderField Width="150px" ColumnID="TypeCode"   EnableLock="true" Locked="true"  DataField="TypeCode"
                      HeaderText="代码">
                    <Editor>
                        <f:TextBox ID="tbxTypeCode"  Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="200px"  EnableLock="true" Locked="true"  ColumnID="TypeName" DataField="TypeName"
                      HeaderText="名称">
                    <Editor>
                        <f:TextBox ID="tbxTypeName" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="180px" EnableColumnEdit="true" ColumnID="SType" DataField="SType"
                      HeaderText="分类">
                    <Editor>
                       <f:DropDownList ID="ddlSType" runat="server">
                           <f:ListItem Value="类别" Text="类别" />
                           <f:ListItem Value="车间" Text="车间" />
                           <f:ListItem Value="主要来源" Text="主要来源" />
                           <f:ListItem Value="仓库" Text="仓库" />
                           <f:ListItem Value="总成" Text="总成" />
                       </f:DropDownList>
                    </Editor>
                </f:RenderField>

                 <f:RenderField Width="180px" EnableColumnEdit="true" ColumnID="SortIndex" DataField="SortIndex"
                      HeaderText="排序">
                    <Editor>
                        <f:TextBox ID="tbxSortIndex" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                 
               
            </Columns>
             
        </f:Grid>
       
    </form>
    <script>
       

        function onGridAfterEdit(event, value, params) {
            var me = this, columnId = params.columnId, rowId = params.rowId;
            //if (columnId === 'ChineseScore' || columnId === 'MathScore') {
            var UsingQuantity = me.getCellValue(rowId, 'UsingQuantity');
            var ConfirmQuantity = me.getCellValue(rowId, 'ConfirmQuantity');

            me.updateCellValue(rowId, 'RealUsingQuantity', parseInt(UsingQuantity) - parseInt(ConfirmQuantity));
            me.updateCellValue(rowId, 'IsConfirm', 1);

            //}
        }
 
    </script>
</body>
</html>