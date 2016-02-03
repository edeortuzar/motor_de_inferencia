<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Newsletter.aspx.cs" Inherits="Newsletter"
    Theme="TuCuento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>TuCuento</title>
    <link href="App_Themes/TuCuento/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frm1" runat="server">
    <div>
        <div id="header">
            <div id="logo">
                <h1>
                    <a href="#">TuCuento</a></h1>
            </div>
            <div id="menu">
                <asp:Literal ID="ltMenuSuperior" runat="server"></asp:Literal>
                <table class="tablaVerde">
                    <tr>
                        <td>
                            <asp:Label ID="lblUsuario" runat="server" Text="lblUsuario"></asp:Label>
                        </td>
                        <td>
                            <asp:HyperLink ID="hlCambioClave" runat="server" NavigateUrl="~/CambioClave.aspx">Cambio de clave</asp:HyperLink>
                        </td>
                        <td>
                            <a href="<%=Page.ResolveUrl("~/Logout.aspx") %>" title="" class="tabLogout">LogOut</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="content">
            <div id="posts">
                <div class="post">
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:SiteMapPath ID="SiteMapPath1" runat="server">
                                </asp:SiteMapPath>
                            </td>
                        </tr>
                    </table>
                    <div class="divGrilla">
                        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" GridLines="None"
                            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <Columns>
                                <asp:BoundField DataField="dFecha" HeaderText="Fecha" SortExpression="dFecha" />
                                <asp:BoundField DataField="sTitulo" HeaderText="Titulo" SortExpression="sTitulo" />
                                <asp:BoundField DataField="sArchivo" HeaderText="Archivo" SortExpression="sArchivo" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <table class="tablaCampos">
                        <tr>
                            <td class="tdtablaCamposTitulo">
                                Titulo
                            </td>
                            <td class="tdtablaCamposControles">
                                <asp:TextBox ID="txtTitulo" runat="server" MaxLength="80"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtablaCamposTitulo">
                                Texto
                            </td>
                            <td class="tdtablaCamposControles">
                                <asp:TextBox ID="txtTexto" runat="server" SkinID="textoGrande" TextMode="MultiLine"
                                    MaxLength="4000"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtablaCamposTitulo">
                                Archivo
                            </td>
                            <td class="tdtablaCamposControles">
                                <asp:FileUpload ID="flNewsLetter" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table class="tablaBotones">
                        <tr>
                            <td>
                                <asp:Button ID="btnEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="sidebar">
                <asp:Literal ID="ltMenuLateral" runat="server"></asp:Literal>
            </div>
        </div>
        <div id="footer">
            <p id="copy">
                <a href="<%=Page.ResolveUrl("~/Institucional.aspx") %>">Insitucional</a> - <a href="<%=Page.ResolveUrl("~/FAQ.aspx") %>">
                    FAQ</a><br />
                &copy;2010 TuCuento<br />
                Power by TuCuento</p>
        </div>
    </div>
    </form>
</body>
</html>
