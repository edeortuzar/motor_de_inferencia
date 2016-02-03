<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.master" AutoEventWireup="true" CodeFile="ABMPatente.aspx.cs" Inherits="Usuarios_ABMPatente" Theme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False"
    GridLines="None"
    AllowPaging="false"
    CssClass="mGrid"
    PagerStyle-CssClass="pgr"
    AlternatingRowStyle-CssClass="alt"
        onselectedindexchanged="gvDatos_SelectedIndexChanged" OnRowCreated="gvDatos_RowCreated">
       <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Select" ImageUrl="~/App_Themes/TuCuento/Imagenes/edicion.gif" />
            <asp:BoundField DataField="nCod_Patente" HeaderText="C&#243;digo" SortExpression="nCod_Patente" />
            <asp:BoundField DataField="sDesc_Patente" HeaderText="Nombre" SortExpression="sDesc_Patente" />
            <asp:BoundField DataField="nNodo" HeaderText="Nodo" SortExpression="nNodo" />
            <asp:BoundField DataField="nNodo_Padre" HeaderText="Nodo Padre" SortExpression="nNodo_Padre" />
            <asp:BoundField DataField="sUrl" HeaderText="URL" SortExpression="sUrl" />
            <asp:TemplateField HeaderText="Hab.">
                <ItemTemplate>
                    <asp:CheckBox ID="chkHabGrilla" Enabled="false" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "nHab").ToString() == "1" %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <table class="tablaCampos">
        <tr>
            <td class="tdtablaCamposTitulo">
                Nombre</td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtNombre" runat="server" MaxLength="250"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Nodo</td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtNodo" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Nodo Padre</td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtNodoPadre" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                URL</td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtURL" runat="server" MaxLength="500"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Habilitado</td>
            <td class="tdtablaCamposControles">
                <asp:CheckBox ID="chkHab" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="tablaBotones">
                    <tr>
                        <td>
                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar" 
                                onclick="btnGrabar_Click" /></td>
                        <td>
                            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" 
                                onclick="btnNuevo_Click" /></td>
                    </tr>
                </table>
                <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
                <asp:HiddenField ID="HfAccion" runat="server" />
                 <asp:HiddenField ID="HfnCod_Patente" runat="server" />
            </td>
        </tr>
    </table>

</asp:Content>

