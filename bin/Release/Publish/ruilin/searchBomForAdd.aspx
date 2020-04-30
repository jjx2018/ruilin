<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="searchBomForAdd.aspx.cs" Inherits="AppBoxPro.ruilin.searchBomForAdd" %>

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
            BoxConfigPosition="Start" ShowHeader="false" Title="">
            <Items>
                <f:Form ID="Form2" runat="server" ShowHeader="false" ShowBorder="false">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="请输入料号或名称或规格"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                                 <f:Button ID="btnSearch" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                                  
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="false" DataKeyNames="SN" AllowSorting="true"
                    OnSort="Grid1_Sort" SortField="SN" SortDirection="DESC" AllowPaging="true"
                    IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound" OnRowCommand="Grid1_RowCommand"
                    OnPageIndexChange="Grid1_PageIndexChange" >
                    
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
                        <f:BoundField ColumnID="ItemNo" DataField="ItemNo" SortField="ItemNo" Width="150px" HeaderText="料号" />
                        <f:BoundField ColumnID="ItemName" DataField="ItemName"  SortField="ItemName"   Width="250px" HeaderText="名称" />
                        <f:BoundField ColumnID="Spec" DataField="Spec"  SortField="Spec"   Width="250px" HeaderText="规格" />                              
                        <f:BoundField ColumnID="Material" DataField="Material"   Width="100px" HeaderText="材质" />                       
                        <f:BoundField ColumnID="SurfaceDeal" DataField="SurfaceDeal"  Width="100px" HeaderText="表面处理" />
                        <f:BoundField ColumnID="UsingQuantity" DataField="UsingQuantity"    Width="60px" HeaderText="用量" />
                        <f:BoundField ColumnID="Sclass" DataField="Sclass"    Width="80px" HeaderText="分类" />
                        <f:BoundField ColumnID="MakeMethod" DataField="MakeMethod"    Width="90px" HeaderText="生产方式" />
                        <f:BoundField ColumnID="WorkShop" DataField="WorkShop"    Width="80px" HeaderText="生产车间" />
                     
                    </Columns>
                     <Listeners>
                <f:Listener Event="rowdblclick" Handler="onGridRowSelect" />
            </Listeners>
                </f:Grid>

            </Items>
        </f:Panel>
        <f:Window ID="Window1" CloseAction="Hide" runat="server" IsModal="true" Hidden="true" Target="Top"
            EnableResize="true" EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank"
            Width="900px" Height="350px" OnClose="Window1_Close">
        </f:Window>





    </form>
    <script>

        var gridClientID = '<%= Grid1.ClientID %>';
        function onGridRowSelect() {

            // 返回当前活动Window对象（浏览器窗口对象通过F.getActiveWindow().window获取）
            var activeWindow = F.getActiveWindow();

            // 选中行数据
            var rowData = F(gridClientID).getSelectedRow(true);
            var rowValue = rowData.values;

            var queryRowId = F.queryString('rowid');
            var selectedValues = {
                //'ItemNo': '',
                //'ItemName': '',
                //'Spec': '',
                'Material': rowValue['Material'],
                'SurfaceDeal': rowValue['SurfaceDeal'],
                'UsingQuantity': rowValue['UsingQuantity'],
                'Sclass': rowValue['Sclass'],
                'MakeMethod': rowValue['MakeMethod'],
                'WorkShop': rowValue['WorkShop']
            };

            // 隐藏弹出窗体
            activeWindow.hide();

            // 调用父页面的 updateGridRow 函数
            activeWindow.window.updateGridRow(queryRowId, selectedValues);
        }

    </script>
</body>
</html>
