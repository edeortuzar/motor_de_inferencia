<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.master" AutoEventWireup="true"
    CodeFile="ABMCondiciones.aspx.cs" Inherits="Cuentos_ABMCondiciones" StylesheetTheme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="tablaCampos">
        <tr>
            <td class="tdtablaCamposTitulo">
                Dominio
            </td>
            <td class="tdtablaCamposControles">
                <asp:DropDownList ID="ddlDominio" runat="server" SkinID="ComboGrande">
                </asp:DropDownList>
            </td>
            <td class="tdtablaCamposControles">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                    onclick="btnBuscar_Click" />
            </td>
        </tr>
    </table>
    <div class="divGrilla">
    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" GridLines="None"
        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
        OnRowCreated="gvDatos_RowCreated" OnSelectedIndexChanged="gvDatos_SelectedIndexChanged" >
        <Columns>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="Select"
                        CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/App_Themes/TuCuento/Imagenes/edicion.gif"
                        ToolTip="Seleccionar" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="nCod_Condicion" HeaderText="C&#243;digo" SortExpression="nCod_Condicion" />
            <asp:BoundField DataField="sNombre" HeaderText="Nombre" SortExpression="sNombre"
                ItemStyle-Width="22%" />
            <asp:BoundField DataField="sDescripcion" HeaderText="Descripci&#243;n" SortExpression="sDescripcion"
                ItemStyle-Width="22%" />
            <asp:BoundField DataField="sAtributo" HeaderText="Atributo" SortExpression="sAtributo"
                ItemStyle-Width="24%" />
            <asp:TemplateField HeaderText="Hab." ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:CheckBox ID="chkHabGrilla" Enabled="false" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "nHab").ToString() == "1" %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="nCod_Entidad" HeaderText="Entidad" SortExpression="nCod_Entidad" />
            <asp:BoundField DataField="nCod_Atributo" HeaderText="Atributo" SortExpression="nCod_Atributo" />
            <asp:BoundField DataField="nCod_Dominio" HeaderText="C&#243;digo" SortExpression="nCod_Dominio" />
            <asp:BoundField DataField="sDesc_Dominio" HeaderText="Dominio" SortExpression="sDesc_Dominio"
                ItemStyle-Width="22%" />
        </Columns>
    </asp:GridView>
    </div>
    <br />
    <br />
    <table class="tablaCampos">
        <tr>
            <td class="tdtablaCamposTitulo">
                Dominio
            </td>
            <td class="tdtablaCamposControles">
                <asp:DropDownList ID="ddlDominioCond" runat="server" SkinID="ComboGrande">
                </asp:DropDownList>
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
                Descripción
            </td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Entidad
            </td>
            <td class="tdtablaCamposControles">
                <asp:DropDownList ID="ddlEntidad" runat="server" SkinID="ComboGrande" OnSelectedIndexChanged="ddlEntidad_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Atributo
            </td>
            <td class="tdtablaCamposControles">
                <asp:DropDownList ID="ddlAtributo" runat="server" SkinID="ComboGrande">
                </asp:DropDownList>
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
                <table class="tablaBotones">
                    <tr>
                        <td>
                            <asp:Button ID="btnSeguir" runat="server" Text="Seguir" OnClick="btnSeguir_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="PanelDetalle" runat="server">
                    <table>
                        <tr>
                            <td colspan="2">
                                Valores posibles
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
                                                        Operador lógico
                                                    </td>
                                                    <td class="tdtablaCamposControles">
                                                        <asp:DropDownList ID="ddlOperador" runat="server" SkinID="ComboChico">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Panel ID="PanelTexto" runat="server">
                                                <table class="tablaCampos">
                                                    <tr>
                                                        <td class="tdtablaCamposTitulo">
                                                            Valor posible
                                                        </td>
                                                        <td class="tdtablaCamposControles">
                                                            <asp:TextBox ID="txtValTexto" runat="server" MaxLength="255"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="PanelNumero" runat="server">
                                                <table class="tablaCampos">
                                                    <tr>
                                                        <td class="tdtablaCamposTitulo">
                                                            Valor posible
                                                        </td>
                                                        <td class="tdtablaCamposControles">
                                                            <asp:TextBox ID="txtValNumero" runat="server" MaxLength="255"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="PanelBooleano" runat="server">
                                                <table class="tablaCampos">
                                                    <tr>
                                                        <td class="tdtablaCamposTitulo">
                                                            Valor posible
                                                        </td>
                                                        <td class="tdtablaCamposControles">
                                                            <asp:DropDownList ID="ddlBooleano" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="PanelFecha" runat="server">
                                                <table class="tablaCampos">
                                                    <tr>
                                                        <td class="tdtablaCamposTitulo">
                                                            Valor posible
                                                        </td>
                                                        <td class="tdtablaCamposControles">
                                                            <asp:TextBox ID="txtDia" runat="server" MaxLength="2" Width="20px"></asp:TextBox>/<asp:TextBox
                                                                ID="txtMes" runat="server" MaxLength="2" Width="20px"></asp:TextBox>/<asp:TextBox
                                                                    ID="txtAnio" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <table class="tablaCampos">
                                                <tr>
                                                    <td colspan="2">
                                                        <table class="tablaBotones">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                                                                        onclick="btnAgregar_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnNuevoValor" runat="server" Text="Nuevo Valor" 
                                                                        onclick="btnNuevoValor_Click" />
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
                                            <asp:GridView ID="gvValPosibles" runat="server" AutoGenerateColumns="false" GridLines="None"
                                                AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                OnSelectedIndexChanged="gvValPosibles_SelectedIndexChanged" OnRowCreated="gvValPosibles_RowCreated"
                                                OnRowDeleting="gvValPosibles_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnSeleccionar" runat="server" CausesValidation="False" CommandName="Select"
                                                                CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/App_Themes/TuCuento/Imagenes/edicion.gif"
                                                                ToolTip="Seleccionar" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnBorrar" runat="server" CausesValidation="False" CommandName="Delete"
                                                                CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/App_Themes/TuCuento/Imagenes/Borrar.gif"
                                                                ToolTip="Eliminar" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="nCod_ValPosible" HeaderText="C&#243;digo" SortExpression="nCod_ValPosible" />
                                                    <asp:BoundField DataField="sOperadorLogico" HeaderText="Operador" SortExpression="sOperadorLogico"
                                                        ItemStyle-Width="20%" />
                                                    <asp:BoundField DataField="sValor" HeaderText="Valor" SortExpression="sValor" ItemStyle-Width="70%" />
                                                </Columns>
                                            </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="tablaBotones">
                    <tr>
                        <td>
                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar" 
                                onclick="btnGrabar_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnNuevo" runat="server" Text="Nueva Condición" OnClick="btnNuevo_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
                <asp:HiddenField ID="HfAccion" runat="server" />
                <asp:HiddenField ID="HfAccionValPosible" runat="server" />
                <asp:HiddenField ID="HfnCod_Condicion" runat="server" />
                <asp:HiddenField ID="HfnCod_ValPosible" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
