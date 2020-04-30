<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailedit.aspx.cs" Inherits="AppBoxPro.ruilin.OrderDetailedit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <style>
        .mywnd {
            top: 128px !important;
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
                        <f:Button ID="btnSave" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSave_Click"
                            runat="server" Text="保存">
                        </f:Button>
                         <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="保存后关闭">
                        </f:Button>
                       
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
                                 <f:TextBox ID="txtOrderNo" Readonly="true" NextFocusControl="txtClinetNo" LabelWidth="130px" Width="400px"  runat="server" Label="订单编号" Required="true" ShowRedStar="true">
                                </f:TextBox>
                                <f:TextBox ID="txtClinetNo" NextFocusControl="txtItemNo"  runat="server" FocusOnPageLoad="true" Label="客人编号" Required="true" ShowRedStar="true">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                              <f:TextBox ID="txtItemNo" NextFocusControl="txtItemName" AutoPostBack="true" LabelWidth="130px" Width="400px"  runat="server" Label="型号" OnBlur="txtItemNo_Blur" Required="true" OnTextChanged="txtItemNo_TextChanged" ShowRedStar="true">
                                </f:TextBox>
                              
                                <f:TextBox ID="txtItemName" NextFocusControl="txtQuantity" runat="server" Label="产品名称" Required="true" ShowRedStar="true" >
                                </f:TextBox>
                                 
                            </Items>
                        </f:FormRow>
                       <f:FormRow runat="server" ID="searchRow" Hidden="true">
                            <Items>
                                <f:TextBox ID="txtisOpen" Hidden="false" runat="server"></f:TextBox>
                                </Items>
                           </f:FormRow>
                         <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                 <f:NumberBox ID="txtQuantity" NextFocusControl="txtUnit" runat="server" LabelWidth="130px" Width="400px"  Label="数量">
                                </f:NumberBox>
                                <f:TextBox ID="txtUnit" NextFocusControl="txtColor" runat="server" Label="单位">
                                </f:TextBox>
                                </Items>
                             </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                               <f:TextBox ID="txtColor" NextFocusControl="txtConutryVer" runat="server" LabelWidth="130px" Width="400px"  Label="颜色">
                                </f:TextBox>
                                <f:TextBox ID="txtInputer" runat="server" Label="录入人">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                              <f:RadioButtonList ID="rbtIsNew" Label="是否新产品" LabelWidth="130px" Width="400px"  runat="server">
                                  <f:RadioItem Text="是" Value="是" />
                                  <f:RadioItem Text="否" Value="否" />
                              </f:RadioButtonList>
                              <f:RadioButtonList ID="rbtIspacking" Label="是否新包材" runat="server">
                                  <f:RadioItem Text="是" Value="是" />
                                  <f:RadioItem Text="否" Value="否" />
                              </f:RadioButtonList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                              <f:TextBox ID="txtConutryVer" NextFocusControl="tbxDemand1" runat="server" LabelWidth="130px" Width="400px"  Label="国家包材版本">
                                </f:TextBox>
                              <f:RadioButtonList ID="rbtIsChange" Label="是否变更" runat="server">
                                  <f:RadioItem Text="是" Value="是" />
                                  <f:RadioItem Text="否" Value="否" />
                              </f:RadioButtonList>
                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:TextArea ID="tbxDemand1" Height="100px" LabelWidth="130px" Width="400px"  runat="server" Label="变更备注1">
                                </f:TextArea>  
                                 <f:TextArea ID="tbxDemand2" Height="100px" runat="server" Label="变更备注2">
                                </f:TextArea>                                 
                            </Items>
                        </f:FormRow>
                    <f:FormRow ID="FormRow1" runat="server" Hidden="true">
                            <Items>
                              <f:TextArea ID="tbxDemand3" Height="60px" runat="server" Label="要求3">
                                </f:TextArea>      
                                  <f:TextArea ID="tbxDemand4" Height="60px" runat="server" Label="要求4">
                                </f:TextArea>                                 
                            </Items>
                        </f:FormRow>
                         <f:FormRow ID="FormRow2" runat="server" Hidden="true">
                            <Items>
                                <f:TextArea ID="tbxDemand5" Height="60px" runat="server" Label="要求5">
                                </f:TextArea>  
                                <f:TextArea ID="tbxDemand6" Height="60px" runat="server" Label="要求6">
                                </f:TextArea>                                
                            </Items>
                        </f:FormRow>
                           <f:FormRow ID="FormRow3" runat="server" Hidden="true">
                            <Items>
                              <f:TextArea ID="tbxDemand7" Height="60px" runat="server" Label="要求7">
                                </f:TextArea> 
                                  <f:TextArea ID="tbxDemand8" Height="60px" runat="server" Label="要求8">
                                </f:TextArea>                           
                            </Items>
                        </f:FormRow>
                           <f:FormRow ID="FormRow5" runat="server" Hidden="true">
                            <Items>
                                   <f:TextArea ID="tbxDemand9" Height="60px" runat="server" Label="要求">
                                </f:TextArea>  
                                 <f:TextArea ID="tbxDemand10" Height="60px" runat="server" Label="要求10">
                                </f:TextArea>                           
                            </Items>
                        </f:FormRow>
                           <f:FormRow ID="FormRow6" runat="server" Hidden="true">
                            <Items>
                                <f:TextArea ID="tbxDemand11" Height="60px" runat="server" Label="要求11">
                                </f:TextArea>  
                                 <f:TextArea ID="tbxDemand12" Height="60px" runat="server" Label="要求12">
                                </f:TextArea>                                
                            </Items>
                        </f:FormRow>




                        <f:FormRow runat="server">
                            <Items>
                                <f:TextArea ID="tbxRemark" runat="server" LabelWidth="130px" Label="备注">
                                </f:TextArea>
                                <f:HiddenField ID="txtID" runat="server"></f:HiddenField>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
       <f:Window ID="Window1" Title="编辑"  EnableIFrame="true" runat="server" Hidden="true" 
            EnableMaximize="true" EnableResize="true"  ShowHeader="false"  Target="Self" IsModal="false" Width="700px"
            Height="300px" Margin="0px" BodyPadding="0px;" CssClass="mywnd" OnClose="Window1_Close">
        </f:Window>
         <f:Window ID="Window2"  Title="编辑"  EnableIFrame="true" runat="server" Hidden="true" 
            EnableMaximize="true" EnableResize="true"  ShowHeader="false"  Target="Self" IsModal="false" Width="200px"
            Height="100px" Margin="0px" BodyPadding="0px;" IFrameUrl="sucess.aspx" CssClass="mywnd" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>