<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.master" AutoEventWireup="true" CodeFile="ABMFamilia.aspx.cs" Inherits="Usuarios_ABMFamilia" Theme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False"
    GridLines="None"
    AllowPaging="false"
    CssClass="mGrid"
    PagerStyle-CssClass="pgr"
    AlternatingRowStyle-CssClass="alt" onselectedindexchanged="gvDatos_SelectedIndexChanged" OnRowCreated="gvDatos_RowCreated">
       <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Select" ImageUrl="~/App_Themes/TuCuento/Imagenes/edicion.gif" />
            <asp:BoundField DataField="nCod_Flia" HeaderText="C&#243;digo" SortExpression="nCod_Flia" />
            <asp:BoundField DataField="sDesc_Flia" HeaderText="Nombre" SortExpression="sDesc_Flia" />
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
                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Habilitado</td>
            <td class="tdtablaCamposControles">
                <asp:CheckBox ID="chkHab" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tablaPatentes" class="tablaBotones">
                    <tr>
                        <td class="tdtablaCamposTitulo" colspan="3">
                            Patentes
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 45%">
                            <asp:ListBox ID="lvIzquierda" runat="server" SelectionMode="Multiple" SkinID="listGrande"></asp:ListBox>
                        </td>
                        <td style="width: 10%">
                            <asp:Button ID="btnSel" runat="server" Text=">" SkinID="BotonChico" OnClick="btnSel_Click" />
                            <br />
                            <asp:Button ID="btnSelAll" runat="server" Text=">>" SkinID="BotonChico" OnClick="btnSelAll_Click" />
                            <br />
                            <asp:Button ID="btnDesSel" runat="server" Text="<" SkinID="BotonChico" OnClick="btnDesSel_Click" />
                            <br />
                            <asp:Button ID="btnDesSelAll" runat="server" Text="<<" SkinID="BotonChico" OnClick="btnDesSelAll_Click" />
                        </td>
                        <td style="width: 45%">
                            <asp:ListBox ID="lvDerecha" runat="server" SkinID="listGrande" SelectionMode="Multiple"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </td>
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
                <asp:HiddenField ID="HfnCod_Familia" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

