<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product.aspx.cs" Inherits="AppBoxPro.Mobile.product" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <f:Panel runat="server" ShowBorder="false" ShowHeader="false" AutoScroll="true" ID="Panel1">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Title="" runat="server" HeaderStyle="true">
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:SimpleForm  runat="server" ShowBorder="false" ShowHeader="false" Layout="VBox" MessageTarget="None" ID="SimpleForm1">
                    <Items>
                        <f:TextBox runat="server" Label="SN" ID="tbxSN" LabelWidth="60px" Required="true" Readonly="true" Hidden="true"></f:TextBox>
                        <f:TextBox runat="server" Label="条码" ID="tbxbarcode" OnBlur="tbxprosn_Blur" AutoPostBack="true" LabelWidth="60px" Required="true" EnableBlurEvent="true"></f:TextBox>
                        <f:TextBox runat="server" Label="生产单号" ID="tbxproductno"  LabelWidth="60px" Required="true" ></f:TextBox>
                        <f:TextBox runat="server" Label="料号" ID="tbxitemno" OnBlur="tbxprosn_Blur" AutoPostBack="true" LabelWidth="60px" EnableBlurEvent="true" Required="true"></f:TextBox>
                        <f:TextBox runat="server" Label="品名" ID="tbxitemname"  LabelWidth="60px"  Required="true"></f:TextBox>
                        <f:TextBox runat="server" Label="规格" ID="tbxspec"  LabelWidth="60px" Required="true"></f:TextBox>
                        <f:NumberBox runat="server" Label="生产数量" LabelWidth="60px" Required="true" NoNegative="true" ID="nbPurQut" Readonly="true"></f:NumberBox>
                        <f:NumberBox runat="server" Label="数量" LabelWidth="60px" Required="true" NoNegative="true" ID="nbQut"></f:NumberBox>
                        <f:Button ID="btnSend" Text="提交" Type="Submit" ValidateForms="SimpleForm1" ValidateMessageBoxPlain="true" runat="server" OnClick="btnSend_Click" IconFont="_Send">
                        </f:Button>
                    </Items>
                </f:SimpleForm>
            </Items>
        </f:Panel>

        <script>

            var tbxprosnClientID = '<%=tbxbarcode.ClientID%>';

            var tbxitemnoClientID = '<%=tbxitemno.ClientID%>';

            //安卓手持机调用的方法
            function callJS(prosn) {
                F(tbxprosnClientID).setText(prosn);
                //自定义回发
                __doPostBack('', 'tbxprosn__change');
            }
        </script>
    </form>
</body>
</html>
