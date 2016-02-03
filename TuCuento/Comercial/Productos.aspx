<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.Master" AutoEventWireup="true"
    CodeFile="Productos.aspx.cs" Inherits="Productos" Theme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="tablaBotones">
        <tr>
            <td>
                <asp:RadioButton ID="rdSimple" runat="server" GroupName="TipoBusqueda" 
                    Text="Simple" oncheckedchanged="rdSimple_CheckedChanged" AutoPostBack=true />
            </td>
            <td>
                <asp:RadioButton ID="rdAvanzada" runat="server" GroupName="TipoBusqueda" 
                    Text="Avanzada" oncheckedchanged="rdAvanzada_CheckedChanged" AutoPostBack=true />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlSimple" runat="server">
        <table class="tablaCampos">
            <tr>
                <td class="tdtablaCamposTitulo">
                    Producto
                </td>
                <td class="tdtablaCamposControles">
                    <asp:DropDownList ID="ddlProductos" runat="server" SkinID=ComboGrande>
                        <asp:ListItem Value="-1">Elija</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdtablaCamposTitulo">
                    Linea
                </td>
                <td class="tdtablaCamposControles">
                    <asp:DropDownList ID="ddlLineaProd" runat="server" SkinID=ComboGrande>
                        <asp:ListItem Value="-1">Elija</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlAvanzada" runat="server">
        <table class="tablaCampos">
            <tr>
                <td class="tdtablaCamposTitulo">
                    Producto
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtablaCamposTitulo">
                    Valor desde
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtValorDesde" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtablaCamposTitulo">
                    Valor Hasta
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtValorHasta" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table class="tablaBotones">
        <tr>
            <td>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
            </td>
        </tr>
    </table>
    <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" GridLines="None"
        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
        <Columns>
            <asp:BoundField DataField="sDesc_Producto" HeaderText="Descripcion" SortExpression="sDesc_Producto" />
            <asp:BoundField DataField="sDesc_Linea" HeaderText="Linea" SortExpression="sDesc_Linea" />
            <asp:BoundField DataField="nValor" HeaderText="Valor" SortExpression="nValor" />
        </Columns>
    </asp:GridView>
</asp:Content>
