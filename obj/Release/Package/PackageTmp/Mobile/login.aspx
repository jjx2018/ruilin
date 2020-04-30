<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AppBoxPro.mobile.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
                .mypic {
            text-align: center;
        }

            .mypic img {
                border-radius: 20%;
                width: 200px;
                height: 100px;
            }

        .myhyperlinkcontainer {
            text-align: center;
            margin-top: 20px;
        }

        .myhyperlink {
            display: inline-block !important;
            margin-right: 5px;
        }

            .myhyperlink a {
                font-size: 0.8em;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel runat="server" ID="Panel1" ShowBorder="false" ShowHeader="false" Layout="Fit" BodyPadding="20px">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" Title="系统登录" Hidden="true" HeaderStyle="true">
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="false" ShowHeader="false" Layout="VBox" MessageTarget="None">
                    <Items>
                        <f:Image runat="server" CssClass="mypic" ImageUrl="~/res/images/login/log.png"></f:Image>
                        <f:TextBox ID="tbxUserName" Label="用户名" ShowLabel="false" EmptyText="请输入用户名" Required="true" runat="server">
                        </f:TextBox>
                        <f:TextBox ID="tbxPassword" Label="密码" ShowLabel="false" TextMode="Password" EmptyText="请输入密码" Required="true" runat="server">
                        </f:TextBox>
                        <f:Button ID="btnLogin" Text="登录" Type="Submit" ValidateForms="SimpleForm1" ValidateMessageBoxPlain="true" runat="server" OnClick="btnLogin_Click" IconFont="SignIn">
                        </f:Button>
                        <f:Panel runat="server" CssClass="myhyperlinkcontainer" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:HyperLink ID="HyperLink1" runat="server" CssClass="myhyperlink" NavigateUrl="http://telen.pro/" Text="关于我们"></f:HyperLink>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:SimpleForm>
            </Items>
        </f:Panel>


    </form>
</body>
</html>
