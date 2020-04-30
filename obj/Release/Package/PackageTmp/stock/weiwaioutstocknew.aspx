<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="weiwaioutstocknew.aspx.cs" Inherits="AppBoxPro.stock.weiwaioutstock_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server"></f:PageManager>
        <f:Form runat="server" ID="form" BodyPadding="5px" ShowBorder="false" ShowHeader="false">
            <Rows>
                <f:FormRow>
                    <Items>
                        <f:NumberBox ID="nbQut" NoNegative="true" runat="server" Required="true" Label="数量"></f:NumberBox>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                         <f:DropDownList runat="server" ID="ddlstorehouse" Label="库位" AutoSelectFirstItem="true"></f:DropDownList>
                    </Items>
                </f:FormRow>
                <f:FormRow>
                    <Items>
                         <f:TextBox   runat="server" ID="tbxRemark" Label="备注"></f:TextBox>
                    </Items>
                </f:FormRow>
            </Rows>
            <Items>
                
               
            </Items>
            
        </f:Form>
        <f:Grid runat="server" ID="Grid1" OnRowCommand="Grid1_RowCommand" DataKeyNames="SN" Title="记录">
            <Toolbars>
                <f:Toolbar runat="server" ToolbarAlign="Right" Position="Bottom">
                    <Items>
                        <f:Button runat="server" ID="btnSend" Text="提交" ValidateForms="form" OnClick="btnSend_Click" Type="Submit"></f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:BoundField DataField="SendOutOrderNo" HeaderText="委外单号" Width="150px"></f:BoundField>
                <f:BoundField DataField="ProNo" HeaderText="品号"></f:BoundField>
                <f:BoundField DataField="ItemNo" HeaderText="料号"></f:BoundField>
                <f:BoundField DataField="ItemName" HeaderText="品名"></f:BoundField>
                <f:BoundField DataField="Spec" HeaderText="规格"></f:BoundField>
                <f:BoundField DataField="Quantity" HeaderText="数量"></f:BoundField>
                <f:BoundField DataField="Space" HeaderText="库位"></f:BoundField>
                <f:BoundField DataField="Unit" HeaderText="单位"></f:BoundField>
                <f:BoundField DataField="PDate" HeaderText="日期" Width="150px"></f:BoundField>
                <f:LinkButtonField CommandName="Delete" HeaderText="操作" Icon="Delete" ConfirmText="确定删除该记录？"></f:LinkButtonField>
            </Columns>
        </f:Grid>
    </form>
</body>
</html>
