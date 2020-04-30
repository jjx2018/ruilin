<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order_Edit.aspx.cs" Inherits="AppBoxPro.ruilin.Order_Edit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <style>
        .mywnd {
            top: 145px !important;
            left: 107px !important;
            bottom: 2px !important;
            right: 2px !important;
        }
    </style>
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
                       
                        <f:Button ID="btnSave" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="保存">
                        </f:Button>
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
                                 <f:TextBox ID="txtOrderNo" NextFocusControl="txtClinetOrderNo" Readonly="true"  runat="server" FocusOnPageLoad="true"  Label="订单编号" Required="true" ShowRedStar="true">
                                </f:TextBox>
                                <f:TextBox ID="txtClinetOrderNo" NextFocusControl="txtLotno" LabelWidth="120px" runat="server" Label="客人订单号码" Required="true" ShowRedStar="true">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        
                         <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:TextBox ID="txtLotno" NextFocusControl="txtClientCode"  runat="server" Label="批号">
                                </f:TextBox>  
                                 <f:TextBox ID="txtClientCode" NextFocusControl="txtBuis"  LabelWidth="120px" Readonly="true"  runat="server" Label="客户代码">
                                </f:TextBox>                                 
                            </Items>
                        </f:FormRow>
                    <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                              <f:TextBox ID="txtBuis" NextFocusControl="txtContainerType"  runat="server" Label="业务">
                                </f:TextBox>      
                              <f:TextBox ID="txtContainerType" NextFocusControl="txtChecker"  LabelWidth="120px"  runat="server" Label="柜型">
                                </f:TextBox>      
                                                                
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                              <f:TextBox ID="txtOrderType"  NextFocusControl="checkDate" runat="server" Label="订单类型">
                                </f:TextBox>    
                             <f:DatePicker ID="checkDate" NextFocusControl="recOrderDate"  LabelWidth="120px"  runat="server" Label="评审日期">
                                </f:DatePicker>                 
                            </Items>
                        </f:FormRow>
                         <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                               <f:DatePicker ID="recOrderDate" NextFocusControl="outOrderDate" runat="server" Label="接单日期">
                                </f:DatePicker>   
                                <f:DatePicker ID="outOrderDate"  LabelWidth="120px"   runat="server" Label="出货日期">
                                </f:DatePicker>                                
                            </Items>
                        </f:FormRow>
                            
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        
    </form>
</body>
</html>
