<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="weiwaioutstock.aspx.cs" Inherits="AppBoxPro.stock.weiwaioutstock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server" AutoSizePanelID="Panel5" EnableFStateValidation="false"></f:PageManager>

        <f:Panel CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            Layout="VBox" AutoScroll="true" Margin="0px" ShowHeader="false" Title=""
            BoxConfigChildMargin="0 0 0 0" BodyPadding="0">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true" EnableMultiSelect="false"
                    DataKeyNames="SN,SendOutOrderNo" AllowSorting="true" OnSort="Grid1_Sort" SortField="SN"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    EnableTextSelection="true" KeepCurrentSelection="false"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange" EnableRowClickEvent="true" Height="500px">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>

                                <f:SimpleForm ID="SF1" runat="server" Width="100%" ShowBorder="false" ShowHeader="false" LabelWidth="100px" Layout="HBox" BoxConfigChildMargin="0 5 0 5px">
                                    <Items>

                                        <f:TextBox runat="server" ID="tbxGrid1" EmptyText="关键词" Width="250px"></f:TextBox>
                                        <f:DatePicker ID="datePickerFrom" DateFormatString="yyyy-MM-dd" LabelWidth="50px" Width="150px" runat="server" Label="开始"></f:DatePicker>
                                        <f:DatePicker ID="datePickerTo" DateFormatString="yyyy-MM-dd" LabelWidth="50px" Width="150px" runat="server" Label="结束" CompareControl="datePickerFrom" CompareOperator="GreaterThan" CompareMessage="结束日期应大于开始日期"></f:DatePicker>
                                        <f:Button ID="btnSearch" Pressed="true" runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_Click"></f:Button>
                                        <f:Button Type="Reset" ID="btnClear" Icon="Cancel" EnablePostBack="false" runat="server"
                                            Text="清空">
                                        </f:Button>
                                    </Items>
                                </f:SimpleForm>
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
                        <f:RowNumberField Width="30px" EnablePagingNumber="true" />
                        <f:TemplateField HeaderText="订单明细" Width="100px" ToolTip="查看订单明细">
                            <ItemTemplate>
                                <a href="javascript:;" style="color: blue; text-decoration: underline;" onclick="lookOrder('<%#Eval("SN")%>','<%#Eval("SendOutOrderNo")%>')">委外单明细</a>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:BoundField DataField="SendOutPlanNo" SortField="SendOutPlanNo" Width="150px" HeaderText="委外计划单号" />
                        <f:BoundField DataField="SendOutOrderNo" SortField="SendOutOrderNo" Width="150px" HeaderText="委外单号" />
                        <f:BoundField DataField="SaleOrderNo" SortField="SaleOrderNo" Width="150px" HeaderText="销售单号" />
                        <f:BoundField DataField="SendOutDate" SortField="SendOutDate" Width="150px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="下单日期" />
                        <f:BoundField DataField="Provider" Width="150px" HeaderText="供应商" />
                        <f:BoundField DataField="JBRName" Width="120px" HeaderText="经办人" />
                        <f:BoundField DataField="ContactMan" Width="90px" HeaderText="联系人" />
                        <f:BoundField DataField="Tel" Width="90px" HeaderText="电话" />
                        <f:BoundField DataField="Fax" Width="150px" HeaderText="传真" />

                    </Columns>
                </f:Grid>



            </Items>
        </f:Panel>

        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank"
            Width="1000px" Height="500px" OnClose="Window1_Close">
        </f:Window>


        <script>
            function lookOrder(ID, sendoutorderno) {
                parent.addExampleTab({
                    id: 'weiwaioutstocklist_' + ID + '_tab',
                    iframeUrl: 'stock/weiwaioutstocklist.aspx?sn=' + escape(ID) + '&sendoutorderno=' + escape(sendoutorderno),
                    title: sendoutorderno + '-委外单明细',
                    iconFont: 'sign-in',
                    refreshWhenExist: true
                });
            }
        </script>
    </form>
</body>
</html>
