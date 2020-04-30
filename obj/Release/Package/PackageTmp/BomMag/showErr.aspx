<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showErr.aspx.cs" Inherits="AppBoxPro.BomMag.showErr" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <style type="text/css">
        .f-grid-row.color1,
        .f-grid-row.color1 .f-icon,
        .f-grid-row.color1 a {
            background-color: #e4dddd;
             /*background-color: #0094ff;*/
            color: #000;
        }

        .f-grid-row.color3,
        .f-grid-row.color3 .f-icon,
        .f-grid-row.color3 a {
            background-color: #b200ff;
            color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch"
            BoxConfigPosition="Start" ShowHeader="false" Title="供应商管理">
            <Items>
               
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="true" Title="料号和序号不规范的物料"
                    EnableCheckBoxSelect="false"  AllowPaging="false"
                    IsDatabasePaging="false" KeepCurrentSelection="true" EnableTextSelection="true" >
                   
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true"  
                            runat="server">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                    
                                                <f:BoundField DataField="Seq"  Width="150px" HeaderText="序号" />

                        
                        <f:BoundField DataField="ItemNo"   Width="200px" HeaderText="料号" />
                        <f:BoundField DataField="ItemName"  ExpandUnusedSpace="true"  Width="300px" HeaderText="名称" />
                        <f:BoundField DataField="Spec"  ExpandUnusedSpace="true"  Width="300px" HeaderText="规格" />
                        <f:BoundField DataField="Material"  ExpandUnusedSpace="true"  Width="300px" HeaderText="材质" />
                        <f:BoundField DataField="SurfaceDeal"  ExpandUnusedSpace="true"  Width="300px" HeaderText="表面处理" />
                    </Columns>
                </f:Grid>
                                <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="true" Title="一物多码的物料"
                    EnableCheckBoxSelect="true" EnableMultiSelect="true"  AllowPaging="false" 
                    IsDatabasePaging="false" KeepCurrentSelection="true" EnableTextSelection="true" OnRowSelect="Grid2_RowSelect" OnRowDataBound="Grid2_RowDataBound"  >
                   <Toolbars>
                       <f:Toolbar runat="server" Position="Bottom" ToolbarAlign="Right">
                           <Items>
                                <f:Button ID="btnUpdate" OnClick="btnUpdate_Click" Icon="ThumbUp" Text="更新到物料中" EnablePostBack="true" runat="server">
                        </f:Button>
                           </Items>
                       </f:Toolbar>
                   </Toolbars>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText2" runat="server" Text="每页记录数：">
                        </f:ToolbarText>
                        <f:DropDownList ID="DropDownList1" Width="80px" AutoPostBack="true"  
                            runat="server">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                    
                                                <f:BoundField DataField="Seq"  Width="70px" HeaderText="序号" />
                        <f:BoundField DataField="wlFrom"   Width="80px" HeaderText="来源" />
                        <f:BoundField DataField="ItemNo"   Width="150px" HeaderText="料号" />
                        <f:BoundField DataField="ItemName"  ExpandUnusedSpace="true"  Width="300px" HeaderText="名称" />
                        <f:BoundField DataField="Spec"  ExpandUnusedSpace="true"  Width="300px" HeaderText="规格" />
                        <f:BoundField DataField="Material"  ExpandUnusedSpace="true"  Width="300px" HeaderText="材质" />
                        <f:BoundField DataField="SurfaceDeal"  ExpandUnusedSpace="true"  Width="300px" HeaderText="表面处理" />
                    </Columns>
                </f:Grid>

            </Items>
        </f:Panel>
      




    </form>
</body>
</html>