<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.Master" AutoEventWireup="true"
    CodeFile="Newsletter.aspx.cs" Inherits="Newsletter" Theme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="tablaBotones">
        <tr>
            <td>
                <asp:RadioButton ID="rdSuscribir" runat="server" Text="Suscripción a newsletter" GroupName="opciones" />
            </td>
            <td>
                <asp:RadioButton ID="rdNoSuscribir" runat="server" Text="Baja de suscripción a newsletter" GroupName="opciones"/>
            </td>
        </tr>
    </table>
    <table class="tablaCampos">
        <tr>
            <td class="tdtablaCamposTitulo">
                E-mail
            </td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="tablaBotones">
        <tr>
            <td>
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" 
                    onclick="btnAceptar_Click" />
            </td>
        </tr>
    </table>
    <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
</asp:Content>
