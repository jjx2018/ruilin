<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderEdit.aspx.cs" Inherits="AppBoxPro.ProviderMag.ProviderEdit" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="关闭">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="保存后关闭">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                    Title="SimpleForm"  >
                    <Rows>
                        <f:FormRow runat="server">
                            <Items>
                                 <f:TextBox ID="tbxClinetNo" Readonly="true" LabelWidth="110px" runat="server" Label="供应商代号" Required="true" ShowRedStar="true">
                                </f:TextBox>
                                <f:TextBox ID="tbxsubjectcode" Hidden="true" runat="server" Label="客户代号" Required="true" ShowRedStar="true">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="tbxName" FocusOnPageLoad="true" NextFocusControl="tbxAddress"  LabelWidth="110px" runat="server" Label="供应商名称" Required="true" ShowRedStar="true">
                                </f:TextBox>
                                <f:TextBox ID="tbxAddress" runat="server"  NextFocusControl="ddlRank"  Label="地址" Required="true" ShowRedStar="true" >
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                                                <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:DropDownList ID="ddlRank" runat="server" Label="供应商等级"  NextFocusControl="ddlType">
                                    <f:ListItem Text="一等" Value="一等" />
                                    <f:ListItem Text="二等" Value="二等" />
                                    <f:ListItem Text="三等" Value="三等" />
                                    <f:ListItem Text="四等" Value="四等" />
                                    <f:ListItem Text="五等" Value="五等" />
                                </f:DropDownList>
                                <f:DropDownList ID="ddlType" runat="server" Label="类型"  NextFocusControl="tbxContactman">
                                    <f:ListItem Text="外协" Value="外协" />
                                    <f:ListItem Text="五金" Value="五金" />
                                    <f:ListItem Text="塑胶" Value="塑胶" />
                                    <f:ListItem Text="五金原材料" Value="五金原材料" />
                                    <f:ListItem Text="塑胶原材料前处理" Value="塑胶原材料前处理" />
                                    <f:ListItem Text="油漆" Value="油漆" />
                                    <f:ListItem Text="泡沫" Value="泡沫" />
                                    <f:ListItem Text="包材" Value="包材" />
                                    <f:ListItem Text="劳保" Value="劳保" />
                                </f:DropDownList>
                                
                            </Items>
                        </f:FormRow>
                         <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="tbxContactman" runat="server" Label="联系人"  NextFocusControl="tbxPhone">
                                </f:TextBox>
                                <f:TextBox ID="tbxPhone" runat="server" Label="联系电话"  NextFocusControl="tbxFax">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="tbxFax" runat="server" Label="传真"  NextFocusControl="tbxEmail">
                                </f:TextBox>
                                 <f:TextBox ID="tbxEmail" runat="server" Label="邮箱地址"  NextFocusControl="tbxPurchaseMan">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="tbxPurchaseMan" runat="server" Label="采购员名称"  NextFocusControl="tbxRemark">
                                </f:TextBox>
                                 <f:RadioButtonList ID="rbtIsValid" Label="失效" runat="server"  >
                                     <f:RadioItem Text="是" Value="是" />
                                     <f:RadioItem Text="否" Value="否" Selected="true" />
                                 </f:RadioButtonList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextArea ID="tbxRemark" runat="server" Label="备注">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="550px"
            Height="350px">
        </f:Window>
    </form>
</body>
</html>