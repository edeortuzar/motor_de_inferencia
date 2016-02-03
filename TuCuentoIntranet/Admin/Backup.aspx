<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.master" AutoEventWireup="true" CodeFile="BackUp.aspx.cs" Inherits="BackUp" Theme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tablaCampos">
        <tr>
            <td class="tdtablaCamposTitulo">
                Path</td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtPath" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Nombre archivo</td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtNomArchivo" runat="server"></asp:TextBox></td>
        </tr>
    </table>
    <table class="tablaBotones">
        <tr>
            <td>
                <asp:Button ID="btnGenerar" runat="server" Text="Generar back-up" 
                    onclick="btnGenerar_Click" />
            </td>
            <td>
                <asp:Button ID="btnRestore" runat="server" Text="Generar restore" 
                    onclick="btnRestore_Click" />
            </td>
        </tr>
    </table>
    <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
</asp:Content>

