<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.Master" AutoEventWireup="true"
    CodeFile="RespuestaEncuesta.aspx.cs" Inherits="Comercial_RespuestaEncuesta" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="tablaBotones">
        <tr>
            <td>
                Gracias por participar de nuestras encuestas.
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTitulo" runat="server" Text="lblTitulo"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPregunta" runat="server" Text="lblPregunta"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" GridLines="None"
                    AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                    <Columns>
                        <asp:BoundField DataField="sRespuesta" HeaderText="Respuesta" SortExpression="sRespuesta" ItemStyle-Width="90%"  />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" ItemStyle-Width="10%" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
