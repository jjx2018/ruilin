<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="magconn.aspx.cs" Inherits="AppBoxPro.test.magconn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   明文：<asp:TextBox ID="TextBox1" runat="server" Height="50px" TextMode="MultiLine" 
            Width="739px"></asp:TextBox>
        <br />
        密文：<asp:TextBox ID="TextBox2" runat="server" Height="50px" TextMode="MultiLine" 
            Width="739px"></asp:TextBox>
        <br />
        <asp:Button ID="btnEnc" runat="server" onclick="btnEnc_Click" Text="加密" />
        <asp:Button ID="btnDec" runat="server" onclick="btnDec_Click" Text="解密" />
    </form>
</body>
</html>
