<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showProcess.aspx.cs" Inherits="AppBoxPro.MaterialMag.showProcess" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Grid1" EnableFStateValidation="false" runat="server" />
 <f:Grid ID="Grid1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="" EnableCollapse="false"
            runat="server" DataKeyNames="sn" AllowCellEditing="true" ClicksToEdit="1"  OnPreDataBound="Grid1_PreDataBound"  EnableTextSelection="true" 
            DataIDField="sn" EnableCheckBoxSelect="true"  KeepCurrentSelection="false" OnPageIndexChange="Grid1_PageIndexChange"  AllowSorting="true" OnSort="Grid1_Sort"  SortField="sn" OnRowCommand="Grid1_RowCommand"  EnterNavigate="true" TabVerticalNavigate="true" EnterVerticalNavigate="false"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnRowDataBound="Grid1_RowDataBound" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">           
     <Toolbars>
                 <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                       <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="料号、工艺编码、工艺名称搜索"
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
                        <f:Button ID="btnNew"  Hidden="true"  Text="新增" Icon="Add" EnablePostBack="false" runat="server">
                        </f:Button>






                          <f:Button ID="Button1"  Hidden="true" OnClick="btnSave_Click" runat="server" Icon="SystemSave"    Text="保存">
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
                  <f:RenderField Width="150px" ColumnID="itemno"   EnableLock="true" Locked="true"  DataField="itemno"
                      HeaderText="料号" EnableColumnEdit="false">
                    <Editor>
                        <f:TextBox ID="tbxItemNo"  runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="200px"  EnableLock="true" Locked="true"  ColumnID="ProcessingSeq" DataField="ProcessingSeq"
                      HeaderText="加工顺序">
                    <Editor>
                        <f:TextBox ID="tbxProcessingSeq" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="180px" ColumnID="ProcessCode" DataField="ProcessCode"
                      HeaderText="工艺编码">
                    <Editor>
                        <f:TextBox ID="tbxProcessCode" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="ProcessName" DataField="ProcessName"
                      HeaderText="工艺名称">
                    <Editor>
                        <f:TextBox ID="tbxProcessName" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="90px" ColumnID="MoldelNo" DataField="MoldelNo" TextAlign="Center"
                      HeaderText="模具编号">
                    <Editor>
                        <f:NumberBox  MinValue="1" ID="txtMoldelNo" Required="true" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="EquipmentNoName" DataField="EquipmentNoName"
                      HeaderText="设备编号名称">
                    <Editor>
                        <f:TextBox ID="txtEquipmentNoName" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="Nature" DataField="Nature"
                      HeaderText="性质">
                    <Editor>
                        <f:TextBox ID="txtNature" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="Team" DataField="Team"
                      HeaderText="班组">
                    <Editor>
                        <f:TextBox ID="txtTeam" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="Department" DataField="Department"
                      HeaderText="部门">
                    <Editor>
                        <f:TextBox ID="txtDepartment" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="90px" ColumnID="WorkBatch" DataField="WorkBatch"
                      HeaderText="工时批量">
                    <Editor>
                        <f:TextBox ID="tbxWorkBatch" Required="false" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="80px" ColumnID="FixPerTime" DataField="FixPerTime"
                      HeaderText="固定人时">
                    <Editor>
                        <f:NumberBox ID="tbxFixPerTime" MinValue="1" Required="false" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="100px" ColumnID="ChangePerTime" DataField="ChangePerTime" FieldType="String"
                      HeaderText="变动人时" EnableColumnEdit="true">
                    <Editor>
                        <f:TextBox Required="false" runat="server"  ID="txtChangePerTime">
                            
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
               
                               <f:RenderField Width="80px" ColumnID="Price" DataField="Price"
                      HeaderText="工价">
                    <Editor>
                        <f:TextBox Required="false" runat="server"  ID="txtPrice">
                            
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                <f:RenderField Width="100px" ColumnID="Remark" DataField="Remark"
                      HeaderText="备注">
                    <Editor>
                        <f:TextBox Required="false" runat="server"  ID="txtRemark">
                            
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
               
               
               <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                 
               
            </Columns>
        </f:Grid>
       
    </form>
    <script>
       

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