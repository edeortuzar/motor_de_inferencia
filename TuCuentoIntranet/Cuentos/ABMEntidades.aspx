<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.master" AutoEventWireup="true"
    CodeFile="ABMEntidades.aspx.cs" Inherits="Cuentos_ABMEntidades" StylesheetTheme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="divGrilla">
    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" GridLines="None"
        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
        OnRowCreated="gvDatos_RowCreated" OnSelectedIndexChanged="gvDatos_SelectedIndexChanged">
        <Columns>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="Select"
                        CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/App_Themes/TuCuento/Imagenes/edicion.gif" ToolTip="Seleccionar" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="nCod_Entidad" HeaderText="C&#243;digo" SortExpression="nCod_Entidad" />
            <asp:BoundField DataField="sNombre" HeaderText="Nombre" SortExpression="sNombre" ItemStyle-Width="40%"/>
            <asp:BoundField DataField="sDescripcion" HeaderText="Descripci&#243;n" SortExpression="sDescripcion" ItemStyle-Width="50%" />
            <asp:TemplateField HeaderText="Hab." ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:CheckBox ID="chkHabGrilla" Enabled="false" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "nHab").ToString() == "1" %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
    <br />
    <table class="tablaCampos">
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
                Descripción
            </td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Habilitado
            </td>
            <td class="tdtablaCamposControles">
                <asp:CheckBox ID="chkHab" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Atributos
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td>
                            <table class="tablaCampos">
                                <tr>
                                    <td class="tdtablaCamposTitulo">
                                        Nombre
                                    </td>
                                    <td class="tdtablaCamposControles">
                                        <asp:TextBox ID="txtAttrNombre" runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdtablaCamposTitulo">
                                        Descripción
                                    </td>
                                    <td class="tdtablaCamposControles">
                                        <asp:TextBox ID="txtAttrDescripcion" runat="server" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdtablaCamposTitulo">
                                        Habilitado
                                    </td>
                                    <td class="tdtablaCamposControles">
                                        <asp:CheckBox ID="chkAttrHab" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdtablaCamposTitulo">
                                        Tipo
                                    </td>
                                    <td class="tdtablaCamposControles">
                                        <asp:DropDownList ID="ddlTipoAtributo" runat="server" SkinID="ComboChico" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlTipoAtributo_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdtablaCamposTitulo">
                                        Entidad
                                    </td>
                                    <td class="tdtablaCamposControles">
                                        <asp:DropDownList ID="ddlEntidad" runat="server" SkinID="ComboGrande">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table class="tablaBotones">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnNuevoAttr" runat="server" Text="Nuevo Attr." 
                                                        onclick="btnNuevoAttr_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="divGrilla">
                            <asp:GridView ID="gvAtributos" runat="server" AutoGenerateColumns="false" GridLines="None"
                                AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                OnSelectedIndexChanged="gvAtributos_SelectedIndexChanged" OnRowCreated="gvAtributos_RowCreated">
                                <Columns>
                                    <asp:TemplateField ShowHeader="False" ItemStyle-Width="3%">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="Select"
                                                CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/App_Themes/TuCuento/Imagenes/edicion.gif" ToolTip="Seleccionar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nCod_Atributo" HeaderText="C&#243;digo" SortExpression="nCod_Atributo" />
                                    <asp:BoundField DataField="sNombre" HeaderText="Nombre" SortExpression="sNombre" ItemStyle-Width="30%" />
                                    <asp:BoundField DataField="sDescripcion" HeaderText="Descripci&#243;n" SortExpression="sDescripcion" ItemStyle-Width="44%"/>
                                    <asp:BoundField DataField="sDesc_TipoAtributo" HeaderText="Tipo" SortExpression="sDesc_TipoAtributo" ItemStyle-Width="20%"/>
                                    <asp:BoundField DataField="nCod_TipoAtributo" HeaderText="Codigo Tipo" SortExpression="nCod_TipoAtributo" />
                                    <asp:TemplateField HeaderText="Entidad">
                                        <ItemTemplate>
                                            <asp:Label ID="sNombre_Entidad" Text='<%# Eval("TipoEntidad.sNombre") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CodEntidad">
                                        <ItemTemplate>
                                            <asp:Label ID="nCod_Entidad" Text='<%# Eval("TipoEntidad.nCod_Entidad") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hab." ItemStyle-Width="3%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkHabGrilla" Enabled="false" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "nHab").ToString() == "1" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            </div>
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
                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar" OnClick="btnGrabar_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnNuevo" runat="server" Text="Nueva entidad" OnClick="btnNuevo_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HfAccion" runat="server" />
    <asp:HiddenField ID="HfAccionAttr" runat="server" />
    <asp:HiddenField ID="HfnCod_Entidad" runat="server" />
    <asp:HiddenField ID="HfnCod_Atributo" runat="server" />
</asp:Content>
