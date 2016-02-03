<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecuperoClave.aspx.cs" Inherits="RecuperoClave" Theme="TuCuento" %>

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
        <table class="tablaCampos">
            <tr>
                <td class="tdtablaCamposTitulo">Usuario:</td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtsCod_Usuario" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="tablaBotones">
                        <tr>
                            <td>
                                <asp:Button ID="btnEnviar" runat="server" Text="Enviar contraseña" 
                                    onclick="btnEnviar_Click"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="tablaBotones">
            <tr>
                <td>
                    <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
