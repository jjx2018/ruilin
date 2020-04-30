<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="maginstruction.aspx.cs" Inherits="AppBoxPro.Mobile.maginstruction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>

        <f:Panel runat="server" ShowBorder="false" ShowHeader="true" Title="备货确认" Layout="Fit">
            <Items>
                <f:SimpleForm runat="server" ID="form2" Layout="VBox" ShowBorder="false" ShowHeader="false" BodyPadding="3px">
                    <Items>
                        <f:TextBox runat="server" Label="条码" ID="tbxProsn" Required="true" EnableBlurEvent="true" OnBlur="tbxProsn_Blur" AutoPostBack="true"></f:TextBox>
                        <f:TextBox runat="server" Label="料号" ID="tbxItemno" Required="true"></f:TextBox>
                        <f:TextBox runat="server" Label="名称" ID="tbxItemname"  Required="true"></f:TextBox>
                        <f:TextBox runat="server" Label="规格" ID="tbxSpec" Required="true"></f:TextBox>
                        <f:TextBox runat="server" Label="备货数量" ID="tbxBeihuo" Required="true"></f:TextBox>
                        <f:NumberBox NoNegative="true" runat="server" Label="确认数量" ID="nbConfirmQuantity" Required="true"></f:NumberBox>
                    </Items>
                    <Toolbars>
                        <f:Toolbar runat="server" Position="Bottom" ToolbarAlign="Right">
                            <Items>
                                <f:Button runat="server" ID="btnSubmit" Text="提交" ValidateForms="form2" OnClick="btnSubmit_Click"></f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:SimpleForm>
            </Items>
        </f:Panel>
        <script>

            var tbxprosnClientID = '<%=tbxProsn.ClientID%>';


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
