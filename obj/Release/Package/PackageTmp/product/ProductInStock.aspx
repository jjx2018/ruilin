<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductInStock.aspx.cs" Inherits="AppBoxPro.product.ProductInStock" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                    DataKeyNames="SN" AllowSorting="true" OnSort="Grid1_Sort" SortField="SN"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    EnableTextSelection="true" KeepCurrentSelection="false"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange" EnableRowClickEvent="true" Height="500px">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>

                                        <f:TextBox runat="server" ID="tbxGrid1" EmptyText="关键词" Width="250px"></f:TextBox>
                                        <f:DatePicker ID="datePickerFrom" DateFormatString="yyyy-MM-dd" LabelWidth="50px" Width="150px" runat="server" Label="开始"></f:DatePicker>
                                        <f:DatePicker ID="datePickerTo" DateFormatString="yyyy-MM-dd" LabelWidth="50px" Width="150px" runat="server" Label="结束" CompareControl="datePickerFrom" CompareOperator="GreaterThan" CompareMessage="结束日期应大于开始日期"></f:DatePicker>
                                        <f:Button ID="btnSearch" Pressed="true" runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_Click"></f:Button>
                                        <f:Button Type="Reset" ID="btnClear" Icon="Cancel" EnablePostBack="false" runat="server"
                                            Text="清空">
                                        </f:Button>
                                        <f:RadioButtonList runat="server" AutoPostBack="true" ID="rblState" OnSelectedIndexChanged="rblState_SelectedIndexChanged" Width="250px">
                                            <f:RadioItem Text="全部" Value="全部" Selected="true" />
                                            <f:RadioItem Text="已完成" Value="已完成" />
                                            <f:RadioItem Text="未完成" Value="未完成" />
                                        </f:RadioButtonList>
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
                        
                        <f:BoundField DataField="PurPlanNo" SortField="PurPlanNo" Width="150px" HeaderText="生产计划单号" Hidden="true" />
                        <f:TemplateField HeaderText="生产单号" Width="120px" ToolTip="查看生产单明细">
                            <ItemTemplate>
                                <a href="javascript:;" style="color: blue; text-decoration: underline;" onclick="lookOrder('<%#Eval("SN")%>','<%#Eval("ProOrderNo")%>')"><%#Eval("ProOrderNo")%></a>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:TemplateField HeaderText="销售单号" Width="150px" ToolTip="销售单号">
                            <ItemTemplate>
                                <a href="javascript:;" style="color: blue; text-decoration: underline;" onclick="lookSaleOrder('<%#Eval("SN")%>','<%#Eval("SaleOrderNo")%>')"><%#Eval("SaleOrderNo") %></a>
                            </ItemTemplate>
                        </f:TemplateField>
                        <f:BoundField DataField="Quantity" SortField="Quantity" Width="150px" HeaderText="需生产量" />
                        <f:BoundField DataField="InstockQuantity" SortField="InstockQuantity" Width="150px" HeaderText="已入库量" />
                        <f:BoundField DataField="State" SortField="State" Width="150px" HeaderText="状态" />
                        <f:BoundField DataField="InputeDate" SortField="InputeDate" Width="150px" DataFormatString="{0:yyyy-MM-dd}" HeaderText="下单日期" />
                        <f:BoundField DataField="Provider" Width="150px" HeaderText="供应商" Hidden="true" />
                        <f:BoundField DataField="JBRName" Width="120px" HeaderText="经办人" Hidden="true"/>
                        <f:BoundField DataField="ContactMan" Width="90px" HeaderText="联系人" Hidden="true"/>
                        <f:BoundField DataField="Tel" Width="90px" HeaderText="电话" Hidden="true"/>
                        <f:BoundField DataField="Fax" Width="150px" HeaderText="传真" Hidden="true"/>

                    </Columns>
                </f:Grid>



            </Items>
        </f:Panel>

        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank"
            Width="1000px" Height="500px" OnClose="Window1_Close">
        </f:Window>


        <script>
            function lookOrder(ID, proorderno) {
                parent.addExampleTab({
                    id: 'productinstocklist_' + ID + '_tab',
                    iframeUrl: 'product/productinstocklist.aspx?sn=' + escape(ID) + '&proorderno=' + escape(proorderno),
                    title: proorderno + '-生产单明细',
                    iconFont: 'sign-in',
                    refreshWhenExist: true
                });
            }

            //通过销售订单查询
            function lookSaleOrder(ID, saleorderno) {
                parent.addExampleTab({
                    id: 'productinstocklistsale_' + ID + '_tab',
                    iframeUrl: 'product/productinstocklistsale.aspx?sn=' + escape(ID) + '&saleorderno=' + escape(saleorderno),
                    title: saleorderno + '-销售单 生产明细',
                    iconFont: 'sign-in',
                    refreshWhenExist: true
                });
            }
        </script>
    </form>
</body>
</html>
