<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Instruction.aspx.cs" Inherits="AppBoxPro.ruilin.Instruction" %>

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
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="0px" runat="server" Layout="VBox">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" Height="30px">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="关闭">
                        </f:Button>
                       
                        <f:Button ID="btnSave" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSave_Click"
                            runat="server" Text="发送">
                        </f:Button>
                         <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="发送后关闭">
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
                                 <f:TextBox ID="txtOrderNo" Readonly="true" runat="server" Label="订单编号" Required="true" ShowRedStar="true">
                                </f:TextBox>
                                <f:TextBox ID="txtClinetNo" runat="server"  Label="客人编号" Required="false" ShowRedStar="false">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                              <f:TextBox ID="txtProNo" AutoPostBack="false" runat="server" Label="产品编号" Required="true" ShowRedStar="true">
                                </f:TextBox>
                              
                                <f:TextBox ID="txtProName" runat="server" Label="产品名称" Required="true" ShowRedStar="true" >
                                </f:TextBox>

                            </Items>
                        </f:FormRow>
                         <f:FormRow runat="server">
                            <Items>
                              <f:TextBox ID="txtItemNo" AutoPostBack="false" runat="server" Label="料号" Required="true" ShowRedStar="true">
                                </f:TextBox>
                              
                                <f:TextBox ID="txtItemName" runat="server" Label="名称" Required="true" ShowRedStar="true" >
                                </f:TextBox>

                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                
                                <f:TextBox ID="txtSpec" runat="server" Label="规格">
                                </f:TextBox>
                                 <f:TextBox ID="txtSclass"  runat="server" Label="分类">
                                </f:TextBox>  
                            </Items>
                        </f:FormRow>
                                                <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:TextBox ID="txtMaterial"  runat="server" Label="材质">
                                </f:TextBox>  
                                 <f:TextBox ID="txtSurfaceDeal"  runat="server" Label="表面处理">
                                </f:TextBox>                                 
                            </Items>
                        </f:FormRow>
                    <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                              <f:TextBox ID="txtUsingQuantity"  runat="server" Label="计划用量">
                                </f:TextBox>      
                                    <f:TextBox ID="txtQuantity" runat="server" Label="实际用量">
                                </f:TextBox>                            
                            </Items>
                        </f:FormRow>
                         
                    </Rows>
                </f:Form>

                 <f:Panel ID="Panel10" ShowBorder="true" Margin="0px" ShowHeader="false" BoxFlex="1" Layout="Fit" runat="server">
                     <Items>
                                         <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true"
                    DataKeyNames="ID,Name" AllowSorting="true" OnSort="Grid1_Sort" SortField="Name"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true"  OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="在用户名、中文名中搜索"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click" Width="300px">
                                </f:TwinTriggerBox>
                                <f:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="rbtDept_SelectedIndexChanged" runat="server" ID="rbtDept">
                                    <f:RadioItem Text="生产部" Value="生产部" Selected="true" />
                                    <f:RadioItem Text="采购部" Value="采购部" />
                                </f:RadioButtonList>
                                <f:Button ID="btnSearch" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
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
                        <f:RowNumberField Width="35px" EnablePagingNumber="true" />
                        <f:BoundField DataField="Name" SortField="Name" Width="100px" HeaderText="用户名" />
                        <f:BoundField DataField="ChineseName" SortField="ChineseName" Width="100px" HeaderText="中文名" />
                        <f:CheckBoxField DataField="Enabled" SortField="Enabled" HeaderText="启用" RenderAsStaticField="true"
                            Width="100px" />
                        <f:BoundField DataField="Gender" SortField="Gender" Width="100px" HeaderText="性别" />
                        
                    </Columns>
                </f:Grid>

                     </Items>
                     </f:Panel>
            </Items>
        </f:Panel>
       <f:Window ID="Window1" Title="编辑"  EnableIFrame="true" runat="server" Hidden="true" 
            EnableMaximize="true" EnableResize="true"  ShowHeader="false"  Target="Self" IsModal="false" Width="700px"
            Height="300px" Margin="0px" BodyPadding="0px;" CssClass="mywnd" OnClose="Window1_Close">
        </f:Window>
         
    </form>
</body>
</html>