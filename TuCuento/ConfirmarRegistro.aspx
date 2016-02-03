<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfirmarRegistro.aspx.cs"
    Inherits="ConfirmarRegistro" Theme="TuCuento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TuCuento</title>
    <link href="App_Themes/TuCuento/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapperLogo">
        <div id="logo">
            <h1>
                <a href="#">TuCuento</a></h1>
        </div>
    </div>
    <div id="wrapper">
        <table class="tablaBotones">
            <tr>
                <td>
                    <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnContinuar" runat="server" Text="Continuar" 
                        onclick="btnContinuar_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfEstado" runat="server" />
    </form>
</body>
</html>
