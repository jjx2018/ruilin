<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magOrderDetail.aspx.cs" Inherits="AppBoxPro.ruilin.magOrderDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
       <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel5" EnableFStateValidation="false" runat="server" />
       <f:Panel ID="Panel5" CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            Layout="VBox" AutoScroll="true" Margin="0px" ShowHeader="false" Title="1111111111111"
            BoxConfigChildMargin="0 0 0 0" BodyPadding="0">
            <Items>
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false" EnableCollapse="true" Collapsed="true" Hidden="true" AutoScroll="true" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        
                       
                        <f:Button ID="btnSave" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="保存">
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
                                 <f:TextBox ID="txtOrderNo" NextFocusControl="txtClinetOrderNo"  runat="server" FocusOnPageLoad="true"  Label="订单编号" Required="true" ShowRedStar="true">
                                </f:TextBox>
                                <f:TextBox ID="txtClinetOrderNo" NextFocusControl="txtLotno" LabelWidth="120px" runat="server" Label="客人订单号码" Required="true" ShowRedStar="true">
                                </f:TextBox>
                                 <f:TextBox ID="txtLotno" NextFocusControl="txtClientCode"  runat="server" Label="批号">
                                </f:TextBox>  
                                 <f:TextBox ID="txtClientCode" NextFocusControl="txtBuis"  LabelWidth="120px"  runat="server" Label="客户代码">
                                </f:TextBox> 
                                <f:TextBox ID="txtBuis" NextFocusControl="txtContainerType"  runat="server" Label="业务">
                                </f:TextBox>     
                            </Items>
                        </f:FormRow>
                        
                         <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                              <f:TextBox ID="txtChecker"  NextFocusControl="checkDate" runat="server" Label="订单评审">
                                </f:TextBox>      
                             <f:DatePicker ID="checkDate" NextFocusControl="recOrderDate"  LabelWidth="120px"  runat="server" Label="评审日期">
                                </f:DatePicker>      
                                 <f:DatePicker ID="recOrderDate" NextFocusControl="outOrderDate" runat="server" Label="接单日期">
                                </f:DatePicker>   
                                <f:DatePicker ID="outOrderDate"  LabelWidth="120px"   runat="server" Label="出货日期">
                                </f:DatePicker> 
                                <f:TextBox ID="txtContainerType" Width="300px" NextFocusControl="txtChecker"  LabelWidth="100px"  runat="server" Label="柜型">
                                </f:TextBox>  
                            </Items>
                        </f:FormRow>
                         
                            
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>

                <f:Panel ID="Panel4" Title="产品信息" BoxFlex="1" MinHeight="270px" Margin="0"
                    runat="server" BodyPadding="1px 0px 0px 0px" ShowBorder="false" ShowHeader="false" EnableCollapse="false" Layout="VBox">
                    <Items>
                  <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true"
                    DataKeyNames="SN" AllowSorting="true"    SortField="SN" OnSort="Grid2_Sort"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid2_RowDoubleClick" OnRowCommand="Grid2_RowCommand" OnPreDataBound="Grid2_PreDataBound" OnPreRowDataBound="Grid2_PreRowDataBound" OnRowClick="Grid2_RowClick" OnRowDataBound="Grid2_RowDataBound" AllowCellEditing="false" ClicksToEdit="1" >
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                 <f:SimpleForm ID="SF2" runat="server" Width="100%" ShowBorder="false" ShowHeader="false" LabelWidth="100px" Layout="HBox" BoxConfigChildMargin="0 5 0 5px">
                                    <Items>
                                 <f:TwinTriggerBox ID="TwinTriggerBox2" Width="300px" runat="server" ShowLabel="false" EmptyText="可输入型号、品名搜索"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage2_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage2_Trigger1Click">
                                </f:TwinTriggerBox>
                               
                                <f:DatePicker ID="DateFrom" DateFormatString="yyyy-MM-dd" LabelWidth="80px"  Width="200px"    runat="server" Label="录单开始" AutoPostBack="true" OnTextChanged="OnDateFrom_Changed"></f:DatePicker>
                                <f:DatePicker ID="DateTo" DateFormatString="yyyy-MM-dd"    LabelWidth="80px"  Width="200px"     runat="server" Label="录单结束"  CompareControl="DateFrom" CompareOperator="GreaterThan" CompareMessage="结束日期应大于开始日期" AutoPostBack="true" OnTextChanged="OnDateTo_Changed"></f:DatePicker>
                                <f:Button ID="Button2" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch2_click"></f:Button>
                                  <f:Button  ID="btnReset"  Icon="Cancel" EnablePostBack="false" runat="server"
                        Text="清空">
                    </f:Button>
                              
                            </Items>   
                               </f:SimpleForm>  
                            </Items>
                        </f:Toolbar>
                         <f:Toolbar runat="server">
                            <Items>
                                  <f:Button ID="btnAddDetail"   Text="添加" Icon="Add" EnablePostBack="false" runat="server">
                        </f:Button>
                          <f:Button ID="Button3"  Hidden="true" runat="server" Icon="SystemSave"    Text="保存">
                            </f:Button> 
                          <f:Button ID="btnDeleteSelected2" Icon="Delete" runat="server" Text="删除" OnClick="btnDeleteSelected2_Click">
                                </f:Button>
                                
                                <f:Button ID="btnBOM"  Hidden="false" runat="server" Icon="ApplicationFormAdd"  OnClick="btnBOM_Click"  Text="生成BOM">
                            </f:Button> 
                            </Items>
                        </f:Toolbar>
                        
                    </Toolbars>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText2" runat="server" Text="每页记录数：">
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
                        <f:RowNumberField Width="30px" TextAlign="Center" EnablePagingNumber="true" />
                         <f:BoundField DataField="OrderNo" TextAlign="Center" SortField="OrderNo" Width="150px" HeaderText="订单编号" />                        
                        <f:BoundField DataField="ItemNo" TextAlign="Center" SortField="ItemNo"   Width="150px" HeaderText="型号" />
                          <f:BoundField DataField="ItemName" TextAlign="Center" SortField="ItemName"   Width="200px" HeaderText="产品名称" DataToolTipField="ItemName" ToolTipType="Qtip" />    
                        
                        <f:BoundField DataField="Quantity" TextAlign="Center" SortField="Quantity" Width="100px" HeaderText="数量" />
                        <f:BoundField DataField="SurfaceDeal" TextAlign="Center" SortField="SurfaceDeal" Width="150px" HeaderText="表面处理" />
                        <f:BoundField DataField="Price" TextAlign="Center" SortField="Price" Width="80px" HeaderText="单价" />
                        <f:BoundField DataField="ClinetNo" TextAlign="Center"   Width="100px" HeaderText="客人编号" />
                       <f:RenderField Width="90px" ColumnID="IsBom" DataField="IsBom" TextAlign="Center"
                      HeaderText="BOM状态" RendererFunction="bomState">
                    <Editor>
                        <f:TextBox ID="txtIsBom" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                         <f:RenderField Width="90px" ColumnID="BomVer" DataField="BomVer" TextAlign="Center"
                      HeaderText="BOM版本">
                    <Editor>
                        <f:DropDownList ID="ddlBOM" Required="true" runat="server">
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                         <f:LinkButtonField ColumnID="toBOMField" TextAlign="Center" HeaderText="生成BOM" Icon="ApplicationAdd" ToolTip="生成BOM"  CommandName="toBOM" Width="90px" />
                        <f:BoundField  DataField="Inputer" TextAlign="Center" Width="80px" HeaderText="录入人" />
                         <f:BoundField DataField="Remark"  DataToolTipField="Remark" ToolTipType="Qtip"   Width="90px" HeaderText="备注" />
                        <f:TemplateField HeaderText="生成BOM" Hidden="true" Width="80px" ToolTip="生成BOM">
                    <ItemTemplate>
                        <a href="javascript:;" style="color:blue;text-decoration:underline;" onclick="toBOM('<%#Eval("SN")%>','<%#Eval("OrderNo")%>','<%#Eval("ItemNo")%>','<%#Eval("Quantity")%>','<%#Eval("ItemName")%>')">生成BOM</a>
                    </ItemTemplate>
                </f:TemplateField>
                        

                         <f:WindowField ColumnID="viewField" HeaderText="详情" TextAlign="Center" Icon="ApplicationViewIcons" ToolTip="查看详情" WindowID="Window1"
                            Title="查看详情" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/ruilin/OrderDetailedit.aspx?id={0}&k=1"
                            Width="60px" />
                         <f:WindowField ColumnID="editField" TextAlign="Center" Icon="Pencil" HeaderText="编辑" ToolTip="编辑" WindowID="Window1"
                            Title="编辑" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/ruilin/OrderDetailedit.aspx?id={0}&k=2"
                            Width="60px" />
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" HeaderText="删除" Icon="Delete" ToolTip="删除"  
                            ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="60px" />
                    </Columns>
                </f:Grid>


                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>










        
       
       
       
       
       
       <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server" 
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="1000px"
            Height="720px">
        </f:Window>
           <f:Window ID="Window2" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="600px"
            Height="320px">
        </f:Window>
            <f:Window ID="Window3" Title="选择BOM" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="600px"
            Height="320px">
        </f:Window>
    </form>

</body>
</html>
    <script type="text/javascript">
        function toBOM(ID,ORDERNO,ITEMNO,Q,TITLE) {
            parent.addExampleTab({
                id: ID+'_tab',
                iframeUrl: 'ruilin/RealBom.aspx?sn='+ID+'&od='+ORDERNO+'&id='+ITEMNO+'&q='+Q,
                title:TITLE,
                iconFont: 'sign-in',
                refreshWhenExist: true
            });
        }
        function reloadGrid(param) {
            alert("dddd"+param);
            __doPostBack(null, 'ReloadGrid$' + param);
        }
        function renderGender(value) {
            return value == 1 ? '已审核' : '未审核';
        }
        function bomState(value) {
            return value == 0 ? "<span style='color:red;'>未生成</span>" : "<span style='color:blue;'>已生成</span>";
        }
         
    </script>
