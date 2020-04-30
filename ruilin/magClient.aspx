<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magClient.aspx.cs" Inherits="AppBoxPro.ruilin.magClient" %>

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
            BoxConfigPosition="Start" ShowHeader="false" Title="客户管理">
            <Items>
               
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" DataKeyNames="SN" AllowSorting="true"
                    OnSort="Grid1_Sort" SortField="SN" SortDirection="DESC" AllowPaging="true"
                    IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound" OnRowCommand="Grid1_RowCommand"
                    OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar runat="server">
                            <Items>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="姓名搜索"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click" Width="300px">
                                </f:TwinTriggerBox>
                                <f:Button ID="btnSearch" Pressed="true" ValidateForms="haha"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_Click"></f:Button>
                        
                          
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Button ID="btnExcel" EnableAjax="false" DisableControlBeforePostBack="false" OnClick="btnExcel_Click" runat="server" Icon="PageExcel"    Text="导出Excel">
                            </f:Button> 
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:Button ID="btnNew" runat="server" Icon="Add" EnablePostBack="false" Text="新增客户">
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
                     
                        <f:BoundField DataField="ClientNo" SortField="ClientNo" Width="100px" HeaderText="客户编号" />
                        <f:BoundField DataField="subjectcode" SortField="subjectcode" Width="100px" HeaderText="客户代号" Hidden="true" />
                          <f:TemplateField HeaderText="查看订单" Width="100px" ToolTip="查看订单">
                    <ItemTemplate>
                        <a href="javascript:;" style="color:blue;text-decoration:underline;" onclick="lookOrder('<%#Eval("SN")%>','<%#Eval("ClientNo")%>')">查看订单</a>
                    </ItemTemplate>
                </f:TemplateField>
                        <f:BoundField DataField="Name" SortField="Name" Width="300px" HeaderText="客户名称" />
                        <f:BoundField DataField="Country" ExpandUnusedSpace="true" HeaderText="国家" />
                        <f:BoundField DataField="busiowner" ExpandUnusedSpace="true" HeaderText="业务部门负责人" />
                        <f:BoundField DataField="Address" ExpandUnusedSpace="true" HeaderText="客户地址" />
                        <f:BoundField DataField="Telephone" Width="200px" HeaderText="客户电话" />
                        <f:WindowField ColumnID="editField" TextAlign="Center" Icon="Pencil" ToolTip="编辑" WindowID="Window1"
                            Title="编辑" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/ruilin/Clientedit.aspx?id={0}"
                            Width="50px" />
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
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
 <script src="../js/notify_group.js"></script>
    <script type="text/javascript">

        var _orderNumber = 0;

        function onOperation1Click(event) {
            // 创建一个消息对话框实例
            var displayTime = 3000;// + Math.random() * 10000;

            var allMessageIcons = ['information', 'warning', 'question', 'error', 'success'];
            showNotifyGroup({
                message: '这是第 <strong>' + _orderNumber + '</strong> 条提示信息，显示' + Math.floor(displayTime / 1000) + '秒',
                messageIcon: allMessageIcons[_orderNumber % allMessageIcons.length],
                header: true,
                displayMilliseconds: displayTime
            }, true);

            _orderNumber++;
        }
        function lookOrder(ID,t) {
            parent.addExampleTab({
                id: ID + '_tab',
                iframeUrl: 'ruilin/ClientOrder.aspx?t=' + escape(t),
                title: '客户订单',
                iconFont: 'sign-in',
                refreshWhenExist: true
            });
        }
    </script>