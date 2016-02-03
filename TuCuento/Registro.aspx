<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.Master" AutoEventWireup="true"
    Theme="TuCuento" CodeFile="Registro.aspx.cs" Inherits="Registro" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlRegistro" runat="server">
        <table class="tablaCampos">
            <tr>
                <td class="tdtablaCamposTitulo">
                    Usuario
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtUsuario" runat="server" MaxLength="15"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtablaCamposTitulo">
                    Nombre
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtablaCamposTitulo">
                    Apellido
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtApellido" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtablaCamposTitulo">
                    Contraseña
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtPsw" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtablaCamposTitulo">
                    Repetir contraseña
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtPswRepeat" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtablaCamposTitulo">
                    E-Mail
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtMail" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtablaCamposTitulo">
                    Pregunta de seguridad
                </td>
                <td class="tdtablaCamposControles">
                    <asp:DropDownList ID="ddlPregunta" runat="server" SkinID="ComboGrande">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdtablaCamposTitulo">
                    Respuesta
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtRespuesta" runat="server" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="tablaBotones">
            <tr>
                <td>
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                </td>
                <td>
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlResultado" runat="server">
        <table class="tablaBotones">
            <tr>
                <td>
                    <asp:Label ID="LbMensajeFinal" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
