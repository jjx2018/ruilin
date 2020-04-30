<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magNotice.aspx.cs" Inherits="AppBoxPro.other.magNotice" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>领导订餐</title>
    <script type="text/javascript">
        function formReset() {
            document.getElementById("form1").reset();
        }
        var basePath = '';

        function opentab() {
            window.addExampleTab({
                id: 'hello_fineui_tab',
                iframeUrl: basePath + 'mysacn/magcode.aspx',
                title: '历史记录',
                icon: basePath + 'res/images/filetype/vs_aspx.png',
                refreshWhenExist: true
            });
        }
</script>
    <style>
        .f-panel.mypanel {
            text-align: center;
            padding-top: 10px;
            margin-top: 10px;
            border-top: solid 1px #ccc;
        }

            .f-panel.mypanel .mybutton {
                display: inline-block;
                *display: inline;
                margin-right: 10px;
            }
            .right{
                margin-right:20px;
            }
    </style>
</head>
<body>
   <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" EnableFStateValidation="false" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"  
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="">
            <Items>
                <f:ContentPanel ID="ccpanel" runat="server" ShowHeader="true" Title="新增记录" EnableCollapse="true" >
                  <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                    Title="SimpleForm" EnableCollapse="true"  >
                      <Items>
                           <f:Panel ID="Panel4" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                            <Items>
                                 <f:TextBox ID="txtName" runat="server" Label="名称"  >
                                </f:TextBox> 
                           </Items>
                        </f:Panel>
                           <f:Panel ID="Panel5" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                            <Items>
                                 <f:TextArea ID="tbxContent" runat="server" Label="公告内容">
                                </f:TextArea>
                           </Items>
                        </f:Panel>
                           <f:Panel ID="Panel3" ShowHeader="false" ShowBorder="false" Layout="Column" runat="server">
                            <Items>
                                 <f:Label Label="有效期" runat="server"></f:Label>
                         <f:DatePicker ID="startvalidate" AutoPostBack="false"  LabelWidth="80px"  Width="130px"     DateFormatString="yyyy-MM-dd"     runat="server" Label=""    ></f:DatePicker>
                        <f:DatePicker ID="endvalidate" AutoPostBack="false"  LabelWidth="20px"  Width="140px"   DateFormatString="yyyy-MM-dd"     runat="server" Label="至"    ></f:DatePicker> 
                            </Items>
                        </f:Panel>

                           <f:Panel ID="Panel7" ShowHeader="false" ShowBorder="false" CssClass="mypanel" runat="server">
                            <Items>
                                 <f:Button ID="btnSave" CssClass="right" OnClick="btnSave_Click"  Text="保存" EnablePostBack="true" runat="server">
                        </f:Button>
                        <f:Button ID="btnPub" Text="发布" OnClick="btnPub_Click" EnablePostBack="true" runat="server">
                        </f:Button>
                           </Items>
                        </f:Panel>
                      </Items>
                </f:Form>

                </f:ContentPanel>

                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" EnableMultiSelect="false" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowClick"
                    DataKeyNames="id" AllowSorting="true" OnSort="Grid1_Sort"  SortField="id"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        
                        
                        <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="输入公告名或关键字"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click" Width="250px">
                                </f:TwinTriggerBox> 
                         <f:Label Label="日期" runat="server"></f:Label>
                         <f:DatePicker ID="dcstartdate" AutoPostBack="false"  LabelWidth="80px"  Width="130px"     DateFormatString="yyyy-MM-dd"     runat="server" Label=""    ></f:DatePicker>
                        <f:DatePicker ID="dcendDate" AutoPostBack="false"  LabelWidth="20px"  Width="140px"   DateFormatString="yyyy-MM-dd"     runat="server" Label="至"    ></f:DatePicker> 
                         <f:Button ID="btnSearch" Pressed="true" ValidateForms="haha"  runat="server" Text="查询" Icon="SystemSearch" OnClick="btnSearch_click"></f:Button>
                                 <f:Button OnClientClick="formReset()" ID="btnClear"  Icon="Cancel" EnablePostBack="false" runat="server"
                        Text="清空">
                    </f:Button>
                        
                           
                               
                                 
                         
                       
                         <f:Button ID="btnDeleteSelected" Icon="Delete" runat="server" Text="删除选中记录" OnClick="btnDeleteSelected_Click">
                                </f:Button>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                      
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
                       <f:BoundField DataField="ggname"  Width="120px" HeaderText="标题" />
                        <f:BoundField DataField="ggcontent"  Width="500px" HeaderText="内容" />
                        <f:BoundField DataField="pbdate" DataFormatString="{0:yyyy-M-d}"  Width="100px" HeaderText="发布日期" />
                        <f:BoundField DataField="pbusername"  Width="100px" HeaderText="发布人" />
                        <f:BoundField DataField="state"  Width="120px" HeaderText="状态" />
                        <f:BoundField DataField="startdate" DataFormatString="{0:yyyy-M-d}"  Width="100px" HeaderText="开始日期" />
                        <f:BoundField DataField="enddate" DataFormatString="{0:yyyy-M-d}"  Width="100px" HeaderText="结束日期" />
                       <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除" ConfirmText="确定删除此记录？"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="800px"
            Height="300px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>
