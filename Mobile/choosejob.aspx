<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="choosejob.aspx.cs" Inherits="AppBoxPro.mobile.choosejob" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager runat="server" ></f:PageManager>
        <f:Panel runat="server" ShowBorder="false" ShowHeader="false" AutoScroll="true" ID="Panel1">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Title="请选择岗位" runat="server" HeaderStyle="true">
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:DataList runat="server" ID="DataList1" >
                    <f:DataListItem Text="备货确认" NavigateUrl="maginstruction.aspx" Target="_self" ShowArrow="true" />
                    <f:DataListItem Text="采购收货" NavigateUrl="PurchasePick.aspx?status=0" Target="_self" ShowArrow="true" />
                    <f:DataListItem Text="采购品检" NavigateUrl="PurchaseCheck.aspx?status=1" Target="_self" ShowArrow="true" />
                    <f:DataListItem Text="采购入库" NavigateUrl="PurchaseInstock.aspx?status=2" Target="_self" ShowArrow="true" />

                    <f:DataListItem Text="生产入库" NavigateUrl="ProductInstock.aspx" Target="_self" ShowArrow="true" />
                    <f:DataListItem Text="生产入库品检" NavigateUrl="ProductInstockCheck.aspx" Target="_self" ShowArrow="true" />

                    <f:DataListItem Text="毛坯" NavigateUrl="product.aspx?role=0" Target="_self" ShowArrow="true" />
                    <f:DataListItem Text="注塑" NavigateUrl="product.aspx?role=1" Target="_self" ShowArrow="true" />
                    <f:DataListItem Text="焊接" NavigateUrl="product.aspx?role=2" Target="_self" ShowArrow="true" />
                    <f:DataListItem Text="喷涂" NavigateUrl="product.aspx?role=3" Target="_self" ShowArrow="true" />
                    <%--<f:DataListItem Text="委外(生产)" NavigateUrl="product.aspx?role=4" Target="_self" ShowArrow="true" />--%>
                    <%--<f:DataListItem Text="组装" NavigateUrl="product.aspx?role=5" Target="_self" ShowArrow="true"  />--%>

                    <f:DataListItem Text="委外入库" NavigateUrl="weiwaiinstock.aspx?mark=02" Target="_self" ShowArrow="true" />
                    <f:DataListItem Text="委外出库" NavigateUrl="weiwaioutstock.aspx?mark=03" Target="_self" ShowArrow="true" />

                </f:DataList>
            </Items>
        </f:Panel>

    </form>

    <script>
        //安卓手持机调用的方法
        function callJS(prosn) {
            F(textareaClientID).setText(prosn);
        }

    </script>
</body>
</html>
