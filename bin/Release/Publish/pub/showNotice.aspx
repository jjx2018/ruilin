<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showNotice.aspx.cs" Inherits="AppBoxPro.pub.showNotice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width:100%; height:100%;">
        <tr><td style="text-align:center;font-weight:bolder;color:red;padding:5px 0px 5px 0px;"><%=stitle %></td></tr>
        <tr><td style="text-indent:2em;height:70px;vertical-align:top;"><%=scontent %></td></tr>
        <tr><td style="text-align:right;padding-right:10px;"><%=sdate %></td></tr>

    </table>
    </div>
    </form>
</body>
</html>
