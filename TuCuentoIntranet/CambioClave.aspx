<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.Master" AutoEventWireup="true" CodeFile="CambioClave.aspx.cs" Inherits="CambioClave" Theme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:Panel ID="pnlRegistro" runat="server">
        <table class="tablaCampos">
            <tr>
                <td class="tdtablaCamposTitulo">
                    Contraseña actual
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtPswOLD" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtablaCamposTitulo">
                    Nueva contraseña
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtPsw" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtablaCamposTitulo">
                    Repetir nueva contraseña
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtPswRepeat" runat="server" MaxLength="50" TextMode="Password"></asp:TextBox>
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

