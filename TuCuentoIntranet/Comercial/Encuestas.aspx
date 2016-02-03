<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.master" AutoEventWireup="true"
    CodeFile="Encuestas.aspx.cs" Inherits="Comercial_Encuestas" Theme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AbrirCalendario(sOrigen) {
            window.open(document.getElementById("hfPathCalendario").value + '?destino=' + sOrigen, 'Calendario', 'width=220,height=200,left=270,top=180')
        }

    </script>

    <div class="divGrilla">
        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" GridLines="None"
            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            OnRowCreated="gvDatos_RowCreated">
            <Columns>
                <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="Select"
                            CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/App_Themes/TuCuento/Imagenes/edicion.gif"
                            ToolTip="Seleccionar" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="nId" HeaderText="C&#243;digo" SortExpression="nId" />
                <asp:BoundField DataField="dFechaAlta" HeaderText="Fecha alta" SortExpression="dFechaAlta" />
                <asp:BoundField DataField="dFechaFin" HeaderText="Fecha fin" SortExpression="dFechaFin" />
                <asp:BoundField DataField="sTitulo" HeaderText="Titulo" SortExpression="sTitulo" />
                <asp:BoundField DataField="sPregunta" HeaderText="Pregunta" SortExpression="sPregunta" />
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
                Titulo
            </td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Fecha Fin
            </td>
            <td class="tdtablaCamposControles">
                <input id="txtFechaFin" runat="server" type="text" disabled="disabled" />
                <a href="javascript:AbrirCalendario('ctl00_ContentPlaceHolder1_txtFechaFin');">
                    <img src="<%=Page.ResolveUrl("~/App_Themes/TuCuento/Imagenes/calendario.gif") %>"
                        alt="Calendario" />
                </a>
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
            <td class="tdtablaCamposTitulo">
                Pregunta
            </td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtPregunta" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="tablaBotones">
                    <tr>
                        <td>
                            Respuestas
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="tablaBotones">
                                <tr>
                                    <td>
                                        <table class="tablaCampos">
                                            <tr>
                                                <td class="tdtablaCamposTitulo">
                                                    Respuesta
                                                </td>
                                                <td class="tdtablaCamposControles">
                                                    <asp:TextBox ID="txtRespuesta" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="tablaCampos">
                                            <tr>
                                                <td colspan="2">
                                                    <table class="tablaBotones">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                                                                    onclick="btnAgregar_Click" />
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
                                            <asp:GridView ID="gvRespuestas" runat="server" AutoGenerateColumns="false" GridLines="None"
                                                AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                OnRowCreated="gvRespuestas_RowCreated"
                                                OnRowDeleting="gvRespuestas_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnBorrar" runat="server" CausesValidation="False" CommandName="Delete"
                                                                CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/App_Themes/TuCuento/Imagenes/Borrar.gif"
                                                                ToolTip="Eliminar" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="nIdRespuesta" HeaderText="C&#243;digo" SortExpression="nIdRespuesta" />
                                                    <asp:BoundField DataField="sRespuesta" HeaderText="Respuesta" SortExpression="sRespuesta" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="tablaBotones">
                    <tr>
                        <td colspan="3">
                            Usuarios
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
                                onclick="btnGrabar_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnNueva" runat="server" Text="Nueva encuesta" 
                                onclick="btnNueva_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
                <asp:HiddenField ID="HfAccion" runat="server" />
            </td>
        </tr>
    </table>
    <input type="hidden" id="hfPathCalendario" value="<%=Page.ResolveUrl("~/CalendarioPopUp.aspx") %>" />
</asp:Content>
