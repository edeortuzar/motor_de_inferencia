<%@ Page Title="" Language="C#" StylesheetTheme="TuCuento" MasterPageFile="~/MasterPage/TuCuento.master" AutoEventWireup="true" CodeFile="ABMUsuario.aspx.cs" Inherits="Usuarios_ABMUsuario" %>

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
        onselectedindexchanged="gvDatos_SelectedIndexChanged">
       <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Select" ImageUrl="~/App_Themes/TuCuento/Imagenes/edicion.gif" />
            <asp:BoundField DataField="sCod_Usuario" HeaderText="C&#243;digo" SortExpression="sCod_Usuario" />
            <asp:BoundField DataField="sNombre" HeaderText="Nombre" SortExpression="sNombre" />
            <asp:BoundField DataField="sApellido" HeaderText="Apellido" SortExpression="sApellido" />
            <asp:BoundField DataField="sEmail" HeaderText="Email" SortExpression="sEmail" />
            <asp:TemplateField HeaderText="Hab.">
                <ItemTemplate>
                    <asp:CheckBox ID="chkHabGrilla" Enabled="false" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "nHab").ToString() == "1" %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>

<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
    </asp:GridView>
    <br />
    <br />
    <table class="tablaCampos">
        <tr>
            <td class="tdtablaCamposTitulo">
                Código</td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Nombre</td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtNombre" runat="server" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Apellido</td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtApellido" runat="server" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Habilitado</td>
            <td class="tdtablaCamposControles">
                <asp:CheckBox ID="chkHab" runat="server" /></td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                E-Mail</td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtEMail" runat="server" MaxLength="50"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Blanquear contraseña</td>
            <td class="tdtablaCamposControles">
                <asp:CheckBox ID="chkBlanPSW" runat="server" /></td>
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
                <table id="tablaFamilias" class="tablaBotones">
                    <tr>
                        <td class="tdtablaCamposTitulo" colspan="3">
                            Familias
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 45%">
                            <asp:ListBox ID="lstFlia" runat="server" SelectionMode="Multiple" SkinID="listGrande"></asp:ListBox>
                        </td>
                        <td style="width: 10%">
                            <asp:Button ID="btnSelFlia" runat="server" Text=">" SkinID="BotonChico" OnClick="btnSelFlia_Click" />
                            <br />
                            <asp:Button ID="btnSelAllFlia" runat="server" Text=">>" SkinID="BotonChico" OnClick="btnSelAllFlia_Click" />
                            <br />
                            <asp:Button ID="btnDesSelFlia" runat="server" Text="<" SkinID="BotonChico" OnClick="btnDesSelFlia_Click" />
                            <br />
                            <asp:Button ID="btnDesSelAllFlia" runat="server" Text="<<" SkinID="BotonChico" OnClick="btnDesSelAllFlia_Click" />
                        </td>
                        <td style="width: 45%">
                            <asp:ListBox ID="lstFliaSel" runat="server" SkinID="listGrande" SelectionMode="Multiple"></asp:ListBox>
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
            </td>
        </tr>
    </table>
    
</asp:Content>

