<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magItemColor.aspx.cs" Inherits="AppBoxPro.ruilin.magItemColor" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Grid1" EnableFStateValidation="false" runat="server" />
        <f:Grid ID="Grid1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ShowHeader="false" Title="" EnableCollapse="false"
            runat="server" DataKeyNames="SN" AllowCellEditing="true" ClicksToEdit="2"  OnPreDataBound="Grid1_PreDataBound"  EnableTextSelection="true" 
            DataIDField="SN" EnableCheckBoxSelect="true"  KeepCurrentSelection="true" OnPageIndexChange="Grid1_PageIndexChange"  AllowSorting="true" OnSort="Grid1_Sort"  SortField="SN" OnRowCommand="Grid1_RowCommand"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true">
            <Toolbars>
                 <f:Toolbar ID="Toolbar2" runat="server">
                    <Items>
                       <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="订单编号、产品号、料号、颜色搜索"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click" Width="350px">
                           
                                </f:TwinTriggerBox>
                         
                         
                        <f:SimpleForm ID="SimpleForm2" BodyPadding="10px" runat="server" AutoScroll="true"
                            ShowBorder="true" ShowHeader="false" Hidden="true">
                            <Items>
                                 <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" DataKeyNames="SN" AllowSorting="true" DataIDField="ClientNo" DataTextField="Name" 
                    OnSort="Grid1_Sort" SortField="SN" SortDirection="DESC" AllowPaging="true"
                    IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound" OnRowCommand="Grid1_RowCommand"
                    OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar3" runat="server">
                            <Items>
                               
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                     
                        <f:BoundField DataField="ClientNo" SortField="ClientNo" Width="100px" HeaderText="客户编号" />
                        <f:BoundField DataField="subjectcode" SortField="subjectcode" Width="100px" HeaderText="客户代号" />
                        <f:BoundField DataField="Name" SortField="Name" Width="300px" HeaderText="客户名称" />
                        <f:BoundField DataField="Address" ExpandUnusedSpace="true" HeaderText="客户地址" />
                        <f:BoundField DataField="Telephone" ExpandUnusedSpace="true" HeaderText="客户电话" />                     
                    </Columns>
                </f:Grid>
                            </Items>
                        </f:SimpleForm>
                  






                         <f:Button ID="btnSearch" Pressed="true" ValidateForms="haha"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                        </Items>
                     </f:Toolbar>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnNew"   Text="新增数据" Icon="Add" EnablePostBack="false" runat="server">
                        </f:Button>
                          <f:Button ID="Button1"  OnClick="btnSave_Click" runat="server" Icon="SystemSave"    Text="保存数据">
                            </f:Button> 
                        <f:Button ID="btnDelete" Hidden="false" OnClick="btnDeleteSelected_Click" Text="删除选中行" Icon="Delete"  runat="server">
                        </f:Button>
                        
                         
                          <f:Button ID="btnExcel" EnableAjax="false" DisableControlBeforePostBack="false" OnClick="Button1_Click" runat="server" Icon="PageExcel"    Text="导出Excel">
                            </f:Button>
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
                <f:RowNumberField />
                <f:RenderField Width="300px" ColumnID="OrderNo" DataField="OrderNo"
                      HeaderText="订单号">
                    <Editor>
                        <f:DropDownList Required="false" OnSelectedIndexChanged="BindddlOrderNo" EnableEdit="true" ForceSelection="false" AutoPostBack="true" runat="server" ID="ddlOrderNo">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                 <f:RenderField Width="180px" ColumnID="PID" DataField="PID"
                      HeaderText="产品编号">
                    <Editor>
                        <f:DropDownList Required="false" runat="server"  AutoPostBack="true" EnableEdit="true" ForceSelection="false" OnSelectedIndexChanged="BindPID" ID="ddlPID">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                  <f:RenderField Width="150px" ColumnID="ItemNo" DataField="ItemNo"
                      HeaderText="料号">
                    <Editor>
                        <f:DropDownList Required="false" runat="server" EnableEdit="true" ForceSelection="false" ID="ddlItemNo">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                 
                
                 <f:RenderField Width="100px" ColumnID="Color" DataField="Color"
                      HeaderText="颜色">
                    <Editor>
                      <f:DropDownList Required="false" runat="server" EnableEdit="true" ForceSelection="false" ID="ddlColor">
                            
                        </f:DropDownList>
                    </Editor>
                </f:RenderField>
                
               
                
               <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                 
               
            </Columns>
        </f:Grid>
       
    </form>
    <script>

        function renderGender(value) {
            if (value == 1)
                return '<span style="color:red;">早餐</span>';
            else if (value == 2)
                return '<span style="color:green;">午餐</span>';
            else if (value == 3)
                return '<span style="color:orange;">晚餐</span>';
        }
        function renderState(value) {
            if (value == "已发布")
                return '<span style="color:red;">已发布</span>';
            else
                return '<span style="color:gray;">未发布</span>';

        }


    </script>
</body>
</html>