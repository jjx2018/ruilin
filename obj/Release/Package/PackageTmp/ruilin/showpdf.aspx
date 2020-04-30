<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showpdf.aspx.cs" Inherits="AppBoxPro.ruilin.showpdf" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script >
        function btnclick() {
            document.getElementById("myfrm").click();
        }
        //window.location.href = '<%=pdf %>';
    </script>
</head>
<body onload="btnclick()">
    <form id="form1" runat="server">
    <a id="myfrm" href="<%=pdf %>" style="width:100%;">打开PDF</a>
    </form>
</body>
</html>
