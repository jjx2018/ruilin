<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magProvider.aspx.cs" Inherits="AppBoxPro.ProviderMag.magProvider" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch"
            BoxConfigPosition="Start" ShowHeader="false" Title="供应商管理">
            <Items>
                
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" DataKeyNames="SN" AllowSorting="true"
                    OnSort="Grid1_Sort" SortField="ProviderNo" SortDirection="ASC" AllowPaging="true"
                    IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound" OnRowCommand="Grid1_RowCommand"
                    OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                    <Toolbars>
                          <f:Toolbar runat="server">
                            <Items>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="姓名搜索"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click" Width="300px">
                                </f:TwinTriggerBox>

                                <f:Button ID="btnSearch" Pressed="true" ValidateForms="haha"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_Click"></f:Button>
                        <f:Button ID="btnBack" Pressed="true" ValidateForms="haha"  runat="server" Text="后退" Icon="RewindBlue" OnClick="btnBack_Click"></f:Button>
                          <f:TextBox ID="txtClickColsName" runat="server" Hidden="true" Label="列名" LabelWidth="50px"></f:TextBox>
                          
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnExcel" EnableAjax="false" DisableControlBeforePostBack="false" OnClick="btnExcel_Click" runat="server" Icon="PageExcel"    Text="导出Excel">
                            </f:Button> 
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                 <f:Button ID="btnNew" runat="server" Icon="Add" EnablePostBack="false" Text="新增供应商">
                                </f:Button>
                                <f:Button ID="btnDeleteSelected" ConfirmText="确定删除选中的记录？" ConfirmTarget="Top" Icon="Delete" OnClick="btnDeleteSelected_Click" runat="server" Text="删除选中记录" >
                            </f:Button>

                                 <f:FileUpload runat="server" Width="380px" LabelWidth="60px" ID="filePhoto" EmptyText="请选择Excel模板" Label="模板" Required="true" ButtonIcon="Add"
                    ShowRedStar="true">
                </f:FileUpload>
                        <f:Button ID="btnImport" OnClick="btnImport_Click" Icon="PageWhiteExcel" Text="导入Excel" EnablePostBack="true" runat="server">
                        </f:Button>

                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                               
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
                      <f:TemplateField HeaderText="物料明细" Width="100px" ToolTip="查看物料明细">
                    <ItemTemplate>
                        <a href="javascript:;" style="color:blue;text-decoration:underline;" onclick="lookOrder('<%#Eval("SN")%>','<%#Eval("Name")%>')">物料明细</a>
                    </ItemTemplate>
                </f:TemplateField>
                        <f:BoundField ColumnID="ProviderNo" DataField="ProviderNo" SortField="ProviderNo" Width="120px" HeaderText="供应商代号" />
                        <f:BoundField ColumnID="Name"  DataField="Name" SortField="Name" Width="300px" HeaderText="供应商名称" />
                        <f:BoundField ColumnID="Rank"  DataField="Rank" SortField="Rank" Width="100px" HeaderText="供应商等级" />
                        <f:BoundField ColumnID="Stype"  DataField="Stype" SortField="Stype" Width="90px" HeaderText="类型" />
                        <f:BoundField ColumnID="Address"  DataField="Address" ExpandUnusedSpace="true" HeaderText="地址" />
                        <f:BoundField ColumnID="ContactMan"  DataField="ContactMan" SortField="ContactMan" Width="90px" HeaderText="联系人" />

                        <f:BoundField ColumnID="Telephone"  DataField="Telephone" ExpandUnusedSpace="true" HeaderText="联系电话" />
                        <f:BoundField ColumnID="Fax"  DataField="Fax" ExpandUnusedSpace="true" HeaderText="传真号码" />
                        <f:BoundField ColumnID="Email"  DataField="Email" ExpandUnusedSpace="true" HeaderText="邮箱地址" />
                        <f:BoundField ColumnID="PurchaseMan"  DataField="PurchaseMan" ExpandUnusedSpace="true" HeaderText="采购员名称" />
                        <f:BoundField ColumnID="IsValid"  DataField="IsValid" ExpandUnusedSpace="true" HeaderText="失效" />
                        <f:WindowField ColumnID="editField" TextAlign="Center" Icon="Pencil" ToolTip="编辑" WindowID="Window1"
                             HeaderText="编辑" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/ProviderMag/Provideredit.aspx?id={0}"
                            Width="60px" />
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" HeaderText="删除" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" CloseAction="Hide" runat="server" IsModal="true" Hidden="true" Target="Top"
            EnableResize="true" EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank"
            Width="900px" Height="550px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>
<script type="text/javascript">
    function lookOrder(ID, Name) {
        parent.addExampleTab({
            id: 'lookMaterial_' + ID + '_tab',
            iframeUrl: 'ProviderMag/lookMaterial.aspx?pid=' + escape(ID) + '&n=' + escape(Name),
            title: Name + '-物料明细',
            iconFont: 'sign-in',
            refreshWhenExist: true
        });
    }
</script>