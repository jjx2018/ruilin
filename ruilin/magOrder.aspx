<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magOrder.aspx.cs" Inherits="AppBoxPro.ruilin.magOrder" %>

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
                <f:Panel ID="Panel3" Title="面板1" Height="290px" runat="server"
                    BodyPadding="0px" ShowBorder="false" ShowHeader="false" Layout="VBox" >
                    <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true" 
                    DataKeyNames="SN,OrderNo" AllowSorting="true" OnSort="Grid1_Sort"  SortField="SN"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" AllowCellEditing="false" ClicksToEdit="2" EnableTextSelection="true" KeepCurrentSelection="false" EnableRowDoubleClickEvent="true"
                    OnRowCommand="Grid1_RowCommand" OnRowDoubleClick="Grid1_RowDoubleClick" OnPageIndexChange="Grid1_PageIndexChange"  EnableRowClickEvent="true" OnRowClick="Grid1_OnRowClick">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                
                                <f:SimpleForm ID="SF1" runat="server" Width="100%" ShowBorder="false" ShowHeader="false" LabelWidth="100px" Layout="HBox" BoxConfigChildMargin="0 5 0 5px">
                                    <Items>
                                
                                <f:TwinTriggerBox ID="ttbSearchMessage" Width="400px" runat="server" ShowLabel="false" EmptyText="可输入订单号、批号、客户代码、业务搜索"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>

                               
                               
                                <f:DatePicker ID="datePickerFrom" DateFormatString="yyyy-MM-dd" LabelWidth="80px"  Width="200px"    runat="server" Label="接单开始"></f:DatePicker>
                                <f:DatePicker ID="datePickerTo" DateFormatString="yyyy-MM-dd" LabelWidth="80px"  Width="200px"    runat="server" Label="接单结束"  CompareControl="datePickerFrom" CompareOperator="GreaterThan" CompareMessage="结束日期应大于开始日期"></f:DatePicker>
                                 <f:Button ID="btnSearch" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                                 <f:Button  ID="btnClear"  Icon="Cancel" EnablePostBack="false" runat="server"
                        Text="清空">
                    </f:Button>
                               
                                 </Items>
                                </f:SimpleForm>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                 <f:Button ID="btnExcel" EnableAjax="false" DisableControlBeforePostBack="false"  runat="server" Icon="PageExcel"    Text="导出Excel">
                            </f:Button>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar runat="server">
                            <Items>
                                 <f:FileUpload runat="server" Width="380px" LabelWidth="60px" ID="filePhoto" EmptyText="请选择订单文件" Label="文件" Required="true" ButtonIcon="Add"
                    ShowRedStar="true">
                </f:FileUpload>
                        <f:Button ID="btnImport" OnClick="btnImport_Click" Icon="SystemSave" Text="导入订单" EnablePostBack="true" runat="server">
                        </f:Button>
                                 
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar runat="server">
                            <Items>
                                  <f:Button ID="btnNew"   Text="添加" Icon="Add" EnablePostBack="false" runat="server">
                        </f:Button>
                          <f:Button ID="Button1"  OnClick="btnSave_Click" runat="server" Icon="SystemSave"    Text="保存">
                            </f:Button> 
                        <f:Button ID="btnDelete" Hidden="false" OnClick="btnDeleteSelected_Click" Text="删除" Icon="Delete"  runat="server">
                        </f:Button>
                                 <f:Button ID="btnHeaderBom"  Hidden="false" runat="server" Icon="ApplicationFormAdd"  OnClick="btnHeaderBom_Click"  Text="生成BOM"></f:Button>
                                
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
                        <f:RowNumberField Width="35px" EnablePagingNumber="true" HeaderText="" />
               <f:RenderField Width="150px" TextAlign="Center" ColumnID="OrderNo" DataField="OrderNo"
                      HeaderText="订单编号">
                    <Editor>
                        <f:TextBox ID="txtOrderNo"  Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
               <f:RenderField Width="150px" TextAlign="Center" ColumnID="OrderType" DataField="OrderType"
                      HeaderText="订单类型">
                    <Editor>
                        <f:TextBox ID="txtOrderType"  Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
               <f:RenderField Width="150px" TextAlign="Center" ColumnID="ClientOrderNo" DataField="ClientOrderNo"
                      HeaderText="客人订单号">
                    <Editor>
                        <f:TextBox ID="txtClientOrderNo" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
               <f:RenderField Width="100px" TextAlign="Center" ColumnID="LotNo" DataField="LotNo"
                      HeaderText="批号">
                    <Editor>
                        <f:TextBox ID="txtLotNo" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
               <f:RenderField Width="100px" TextAlign="Center" ColumnID="ClientCode" DataField="ClientCode"
                      HeaderText="客户代码">
                    <Editor>
                        <f:DropDownList Required="false" runat="server" ID="txtClientCode">
                            
                        </f:DropDownList> 
                    </Editor>
                </f:RenderField>
               <f:RenderField Width="100px" TextAlign="Center" ColumnID="RecOrderPerson" DataField="RecOrderPerson"
                      HeaderText="业务">
                    <Editor>
                        <f:DropDownList Required="false" runat="server" ID="txtRecOrderPerson">
                            
                        </f:DropDownList> 
                    </Editor>
                </f:RenderField>
               <f:RenderField Width="120px" TextAlign="Center"  ColumnID="RecOrderDate" DataField="RecOrderDate"  Renderer="Date" RendererArgument="yyyy-MM-dd"
                      HeaderText="接单日期">
                    <Editor>
                         <f:DatePicker ID="txtRecOrderDate" Required="true" runat="server">
                        </f:DatePicker>
                    </Editor>
                </f:RenderField>
               <f:RenderField Width="120px" TextAlign="Center"  ColumnID="CheckDate" DataField="CheckDate"  Renderer="Date" RendererArgument="yyyy-MM-dd"
                      HeaderText="评审日期">
                    <Editor>
                        <f:DatePicker ID="txtCheckDate" Required="true" runat="server">
                        </f:DatePicker>
                    </Editor>
                </f:RenderField>
               <f:RenderField Width="120px" TextAlign="Center"  ColumnID="OutGoodsDate" DataField="OutGoodsDate"  Renderer="Date" RendererArgument="yyyy-MM-dd"
                      HeaderText="出货日期">
                    <Editor>
                         <f:DatePicker ID="txtOutGoodsDate" Required="true" runat="server">
                        </f:DatePicker>
                    </Editor>
                </f:RenderField> 
               <f:RenderField Width="80px" TextAlign="Center" ColumnID="ContainerType" DataField="ContainerType"   HeaderText="柜型"  EnableColumnEdit="false">
                    <Editor>
                          <f:TextBox ID="txtContainerType" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField> 
               <f:RenderField Width="80px" TextAlign="Center" ColumnID="Inputer" DataField="Inputer"   HeaderText="录单人"  EnableColumnEdit="false">
                    <Editor>
                          <f:TextBox ID="TextBox2" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField> 
               <f:RenderField Width="120px" TextAlign="Center" ColumnID="InputerDate" DataField="InputerDate" EnableColumnEdit="false"  Renderer="Date" RendererArgument="yyyy-MM-dd"
                      HeaderText="录单日期">
                    <Editor>
                         <f:TextBox ID="txtInputerDate" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="IsCheck" DataField="IsCheck" EnableColumnEdit="false" FieldType="Int"
                    RendererFunction="renderGender" HeaderText="审核状态" TextAlign="Center">
                    <Editor>
                        <f:TextBox ID="TextBox1" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>         
                <f:WindowField ColumnID="checkField" TextAlign="Center" Icon="Key" HeaderText="审核" ToolTip="审核" WindowID="Window2"
                            Title="审核" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/ruilin/checkorder.aspx?id={0}&k=0"
                            Width="60px" />
                <f:WindowField ColumnID="uncheckField" TextAlign="Center" Icon="Key" HeaderText="反审核" ToolTip="反审核" WindowID="Window2"
                            Title="反审核" DataIFrameUrlFields="SN" DataIFrameUrlFormatString="~/ruilin/checkorder.aspx?id={0}&k=1"
                            Width="80px" />
                <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" HeaderText="删除" Icon="Delete" ToolTip="删除"  
                            ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="60px" />
                        <f:HyperLinkField HeaderText="下载Excel" DataToolTipField="下载Excel" DataTextField="OrderExcel" Text="下载Excel"
                      DataNavigateUrlFields="OrderExcel" DataNavigateUrlFormatString="~/OrderFile/{0}"
                    UrlEncode="true" Target="_blank" ExpandUnusedSpace="true" MinWidth="150px" />
                       <%-- <f:BoundField DataField="OrderNo" SortField="OrderNo"  Width="150px" HeaderText="订单编号" />
                        <f:BoundField DataField="ClientOrderNo" SortField="ClientOrderNo"  Width="200px" HeaderText="客人订单号" />
                        <f:BoundField DataField="LotNo"  SortField="LotNo"  Width="200px" HeaderText="批号" />
                        <f:BoundField DataField="ClientCode"  SortField="ClientCode"  Width="200px" HeaderText="客户代码" />
                        <f:BoundField DataField="RecOrderPerson"   Width="80px" HeaderText="业务" />
                        <f:BoundField DataField="RecOrderDate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="接单日期" />
                        <f:BoundField DataField="SendOrderDate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="发单日期" />
                        <f:BoundField DataField="OutGoodsDate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="出货日期" />
                         <f:BoundField DataField="Inputer"   Width="80px" HeaderText="录单人" />
                        <f:BoundField DataField="InputerDate" DataFormatString="{0:yyyy-MM-dd}" Width="120px" HeaderText="录单日期" />--%>
                        


                        
                    </Columns>
                </f:Grid>



                    </Items>
                </f:Panel>
                <f:Panel ID="Panel4" Title="产品信息" BoxFlex="1" MinHeight="270px" Margin="0"
                    runat="server" BodyPadding="1px 0px 0px 0px" ShowBorder="false" ShowHeader="true" EnableCollapse="false" Layout="VBox">
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
                        <f:DropDownList ID="DropDownList1" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
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
                        <f:BoundField DataField="ItemNo" TextAlign="Center" SortField="ItemNo"   Width="200px" HeaderText="型号" />
                          <f:BoundField DataField="ItemName" TextAlign="Center" SortField="ItemName"   Width="200px" HeaderText="产品名称" DataToolTipField="ItemName" ToolTipType="Qtip" />    
                        
                        <f:BoundField DataField="Quantity" TextAlign="Center" SortField="Quantity" Width="100px" HeaderText="数量" />
                        <f:BoundField DataField="Color" TextAlign="Center" SortField="Color" Width="80px" HeaderText="颜色" />
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
        var orderno = '<%= txtOrderNo.ClientID%>';
        //alert(orderno);
        var v = document.getElementById(orderno);
        //按enter键实现tab键的效果
        v.onkeyup = function (evt) {
            alert('dddddd');
            //document.all可以判断浏览器是否是IE，是页面内所有元素的一个集合
            var isie = (document.all) ? true : false;
            var key;
            var srcobj;
            // if the agent is an IE browser, it's easy to do this.
            if (isie) {
                key = event.keyCode;
                srcobj = event.srcElement;//event.srcElement，触发这个事件的源对象
            }
            else {
                key = evt.which;
                srcobj = evt.target;//target是Firefox下的属性
            }
            if (key == 13 && srcobj.type != 'button' && srcobj.type != 'submit' && srcobj.type != 'reset' && srcobj.type != 'textarea' && srcobj.type != '') {
                if (isie)
                    event.keyCode = 9;//设置按键为tab键
                else {
                    var el = getNextElement(evt.target);
                    if (el.type != 'hidden')
                        ;   //nothing to do here.
                    else
                        while (el.type == 'hidden')
                            el = getNextElement(el);
                    if (!el)
                        return false;
                    else
                        el.focus();
                }
            }
        }
    </script>
