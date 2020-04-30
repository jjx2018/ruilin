<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckOrder.aspx.cs" Inherits="AppBoxPro.ruilin.CheckOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
                        <f:Button ID="btnSave" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSave_Click"
                            runat="server" Text="审核">
                        </f:Button>
                         <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="审核后关闭">
                        </f:Button>
                        <f:ToolbarSeparator runat="server"></f:ToolbarSeparator>
                        <f:ToolbarFill runat="server"></f:ToolbarFill>
                        <f:Label ID="msglab" CssStyle="width:200px; height:50px;color:red;font-weight:bolder;font-size:30px;line-height:50px;" runat="server"></f:Label>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                    Title="SimpleForm">
                    <Rows>

                        <f:FormRow runat="server">
                            <Items>
                                <f:TextArea ID="tbxRemark" Height="170px" runat="server" Label="">
                                </f:TextArea>                               
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
      
    </form>
</body>
</html>
