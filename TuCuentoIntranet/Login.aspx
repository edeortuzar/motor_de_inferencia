<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="TuCuento" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                <td class="tdtablaCamposTitulo">Contraseña:</td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtsPSW" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="tablaBotones">
                        <tr>
                            <td>
                                <asp:Button ID="btnLogin" runat="server" Text="Entrar" 
                                    onclick="btnLogin_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
