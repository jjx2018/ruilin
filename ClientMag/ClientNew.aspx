<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientNew.aspx.cs" Inherits="AppBoxPro.ClientMag.ClientNew" %>

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
                    Title="SimpleForm">
                    <Rows>
                        <f:FormRow runat="server">
                            <Items>
                                 <f:TextBox ID="tbxClinetNo" FocusOnPageLoad="true" NextFocusControl="tbxName" runat="server" Label="客户编号" Required="true" ShowRedStar="true">
                                </f:TextBox>
                                <f:TextBox ID="tbxName" NextFocusControl="txtCountry" runat="server" Label="客户名称" Required="true" ShowRedStar="true">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="txtCountry" NextFocusControl="tbxAddress" runat="server" Label="国家">
                                </f:TextBox>
                                <f:TextBox ID="tbxAddress" NextFocusControl="tbxPhone" runat="server" Label="地址" Required="true" ShowRedStar="true" >
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="tbxPhone" NextFocusControl="tbxContactman" runat="server" Label="电话/传真">
                                </f:TextBox>
                                <f:TextBox ID="tbxContactman" NextFocusControl="txtEmail" runat="server" Label="联系人">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="txtEmail" NextFocusControl="txtWebsite" runat="server" Label="邮件">
                                </f:TextBox>
                                <f:TextBox ID="txtWebsite" NextFocusControl="txtBusiMan" runat="server" Label="网址">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextBox ID="txtBusiMan" NextFocusControl="txtPayMode" runat="server" Label="业务负责人">
                                </f:TextBox>
                                <f:TextBox ID="txtPayMode" NextFocusControl="tbxRemark" runat="server" Label="付款方式">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow4" runat="server" Hidden="true">
                            <Items>
                                <f:TextBox ID="tbxBank" runat="server" Label="开户行">
                                </f:TextBox>
                                <f:TextBox ID="tbxAccount" runat="server" Label="账号">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextArea ID="tbxRemark" runat="server" Label="备注">
                                </f:TextArea>
                                <f:TextArea ID="tbxRemark1" runat="server" Label="备注1">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:TextArea ID="tbxRemark2" runat="server" Label="备注2">
                                </f:TextArea>
                                <f:TextArea ID="tbxRemark3" runat="server" Label="备注3">
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
