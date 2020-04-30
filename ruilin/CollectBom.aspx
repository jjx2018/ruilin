<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectBom.aspx.cs" Inherits="AppBoxPro.ruilin.CollectBom" %>

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
                <f:Panel ID="Panel3" Title="面板1" Height="300px" runat="server"
                    BodyPadding="0px" ShowBorder="false" ShowHeader="false" Layout="VBox">
                    <Items>
 <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true" AllowCellEditing="false" ClicksToEdit="2" 
                     AllowSorting="true" DataKeyNames="ItemNo" SortField="ItemNo"   OnSort="Grid1_Sort"
                    AllowPaging="true" IsDatabasePaging="true" EnableRowSelectEvent="true" OnPageIndexChange="Grid1_PageIndexChange" OnRowSelect="Grid1_RowSelect">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                
                                <f:TwinTriggerBox ID="TwinTriggerBox1" runat="server" ShowLabel="false" EmptyText="请输入料号或名称或规格"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage1_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage1_Trigger1Click" Width="350px">
                                </f:TwinTriggerBox>
                                
                                <f:Button ID="Button2" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                              
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
                                  <f:Button ID="Button6" EnableAjax="false" DisableControlBeforePostBack="false"  runat="server" Icon="PageExcel"    Text="导出Excel">
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
                        <f:RowNumberField Width="30px" EnablePagingNumber="true" />
                 <f:RenderField Width="200px" ColumnID="ItemNo" SortField="ItemNo" DataField="ItemNo" EnableColumnEdit="true" TextAlign="Center"
                      HeaderText="料号">
                    <Editor>
                        <f:TriggerBox ID="tbxItemNo" TriggerIcon="Search" OnTriggerClick="tbxItemNo_TriggerClick" runat="server">
                        </f:TriggerBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="350px" ColumnID="ItemName" DataField="ItemName"  EnableColumnEdit="true" TextAlign="Center"
                      HeaderText="名称">
                    <Editor>
                        <f:TextBox ID="txtItemName2" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="80px" ColumnID="UsingQuantity" DataField="UsingQuantity" TextAlign="Center"
                      HeaderText="用量">
                    <Editor>
                        <f:NumberBox  MinValue="1" ID="txtUsingQuantity" Required="true" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                          <f:WindowField ColumnID="viewField" HeaderText="备货确认单" Title="备货确认单" ToolTip="备货确认单" TextAlign="Center" Icon="ApplicationViewIcons" WindowID="Window1" DataIFrameUrlFields="SN,FSN" DataIFrameUrlFormatString="~/ruilin/SendInstruction.aspx?id={0}&fsn={1}&k=1"
                            Width="120px" Hidden="true" />
                       
                    </Columns>
                </f:Grid>





                    </Items>
                </f:Panel>
                <f:Panel ID="Panel4" Title="产品信息" BoxFlex="1" MinHeight="270px" Margin="0"
                    runat="server" BodyPadding="1px 0px 0px 0px" ShowBorder="false" ShowHeader="false" Layout="VBox">
                    <Items>
 <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="false" ShowHeader="false"
                    EnableCheckBoxSelect="true" AllowCellEditing="true" ClicksToEdit="2" 
                    DataKeyNames="SN" AllowSorting="true"    SortField="SN" OnSort="Grid1_Sort"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid2_PageIndexChange" OnRowCommand="Grid2_RowCommand">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="请输入料号或名称或规格"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                                
                                <f:Button ID="btnSearch" Pressed="true"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                               <f:Button ID="btnCopy" Pressed="true"  runat="server" Text="复制" Icon="PageCopy" OnClick="btnCopy_Click"></f:Button>
                                
                               <f:Button ID="btnNew"   Text="新增" Icon="Add" EnablePostBack="false" runat="server">
                        </f:Button>
                          <f:Button ID="Button1"  OnClick="btnSave_Click" runat="server" Icon="SystemSave"    Text="保存">
                            </f:Button> 
                        <f:Button ID="btnDelete" Hidden="false" OnClick="btnDeleteSelected_Click" Text="删除" Icon="Delete"  runat="server">
                        </f:Button>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                  <f:Button ID="Button8" EnableAjax="false" DisableControlBeforePostBack="false"  runat="server" Icon="PageExcel"     Text="导出Excel">
                            </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList ID="ddlGridPageSize2" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize2_SelectedIndexChanged"
                            runat="server">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:RowNumberField Width="30px" EnablePagingNumber="true" />
                        <f:BoundField Width="150px" HeaderText="订单号" TextAlign="Center" DataField="OrderNo"></f:BoundField>
                        <f:BoundField Width="150px" HeaderText="产品编号" TextAlign="Center" DataField="ProNo"></f:BoundField>
                        <f:BoundField Width="150px" HeaderText="产品名" TextAlign="Center" DataField="ProName"></f:BoundField>
                 <f:RenderField Width="150px" ColumnID="ItemNo" DataField="ItemNo" EnableColumnEdit="true" TextAlign="Center"
                      HeaderText="料号">
                    <Editor>
                        <f:TriggerBox ID="TriggerBox1" TriggerIcon="Search" OnTriggerClick="tbxItemNo_TriggerClick" runat="server">
                        </f:TriggerBox>
                        <%--<f:TextBox ID="tbxItemNo" Required="true" runat="server">
                        </f:TextBox>--%>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="150px" ColumnID="ItemName" DataField="ItemName"  EnableColumnEdit="true" TextAlign="Center"
                      HeaderText="名称">
                    <Editor>
                        <f:TextBox ID="TextBox1" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="150px" ColumnID="Spec" DataField="Spec" TextAlign="Center" ExpandUnusedSpace="true"
                      HeaderText="规格">
                    <Editor>
                        <f:TextBox ID="txtSpec" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="80px" ColumnID="Material" DataField="Material" TextAlign="Center"
                      HeaderText="材质">
                    <Editor>
                        <f:TriggerBox ID="txtMaterial" TriggerIcon="Search" OnTriggerClick="txtMaterial_TriggerClick"  runat="server">
                        </f:TriggerBox>
                       <%-- <f:TextBox ID="txtMaterial" Required="true" runat="server">
                        </f:TextBox>--%>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="SurfaceDeal" DataField="SurfaceDeal" TextAlign="Center"
                      HeaderText="表面处理">
                    <Editor>
                        <f:TextBox ID="txtSurfaceDeal" Required="true" runat="server">
                        </f:TextBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="80px" ColumnID="UsingQuantity" DataField="UsingQuantity" TextAlign="Center"
                      HeaderText="用量">
                    <Editor>
                        <f:NumberBox  MinValue="1" ID="NumberBox1" Required="true" runat="server">
                        </f:NumberBox>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="Sclass" DataField="Sclass" TextAlign="Center"
                      HeaderText="分类">
                    <Editor>
                        <f:DropDownList ID="txtSclass" Required="true" runat="server">
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="100px" ColumnID="MakeMethod" DataField="MakeMethod" TextAlign="Center"
                      HeaderText="生产方式">
                    <Editor>
                        <f:DropDownList ID="txtMakeMethod" Required="true" runat="server">
                            <f:ListItem Text="采购件" Value="采购件" />
                            <f:ListItem Text="厂内生产件" Value="厂内生产件" />
                            <f:ListItem Text="委外生产件" Value="委外生产件" />
                            <f:ListItem Text="客供件" Value="客供件" />
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                          <f:WindowField ColumnID="viewField" HeaderText="备货确认单" Title="备货确认单" ToolTip="备货确认单" TextAlign="Center" Icon="ApplicationViewIcons" WindowID="Window1" DataIFrameUrlFields="SN,FSN" DataIFrameUrlFormatString="~/ruilin/SendInstruction.aspx?id={0}&fsn={1}&k=1"
                            Width="120px" />
                      
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" HeaderText="删除"
                            ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                        <f:BoundField DataField="FSN" Hidden="true" Width="250px" HeaderText="FSN" />
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
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="500px"
            Height="300px">
        </f:Window>
    </form>

</body>
</html>
    <script type="text/javascript">
        function toBOM(ID, ORDERNO, ITEMNO, Q, TITLE) {
            parent.addExampleTab({
                id: ID + '_tab',
                iframeUrl: 'ruilin/RealBom.aspx?sn=' + ID + '&od=' + ORDERNO + '&id=' + ITEMNO + '&q=' + Q,
                title: TITLE,
                iconFont: 'sign-in',
                refreshWhenExist: true
            });
        }
        function reloadGrid(param) {
            alert("dddd" + param);
            __doPostBack(null, 'ReloadGrid$' + param);
        }
        function renderGender(value) {
            return value == 0 ? '已审核' : '未审核';
        }
       
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
    <script>

        
        var gridClientID = '<%= Grid2.ClientID %>';

        function updateGridRow(rowId, values) {
            var grid = F(gridClientID);

            // cancelEdit用来取消编辑
            grid.cancelEdit();

            grid.updateCellValue(rowId, values);
        }

    </script>
