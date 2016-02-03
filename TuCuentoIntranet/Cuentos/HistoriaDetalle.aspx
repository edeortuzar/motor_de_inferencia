<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.master" AutoEventWireup="true"
    CodeFile="HistoriaDetalle.aspx.cs" Inherits="Cuentos_HistoriaDetalle" StylesheetTheme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Panel Principal -->
    <asp:Panel ID="PanelPrincipal" runat="server">
        <table class="tablaCampos">
            <tr>
                <td class="tdtablaCamposTitulo">
                    Dominio
                </td>
                <td class="tdtablaCamposControles">
                    <asp:DropDownList ID="ddlDominio" runat="server" SkinID="ComboGrande">
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
                    <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="200"></asp:TextBox>
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
        </table>
        <table class="tablaBotones">
            <tr>
                <td>
                    <asp:Button ID="btnCancelarPaso1" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                </td>
                <td>
                    <asp:Button ID="btnSiguientePaso1" runat="server" Text="Siguiente" OnClick="btnSiguiente_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <!-- Panel Cuerpo de la historia -->
    <asp:Panel ID="PanelCuerpo" runat="server">
        <div class="divGrilla">
        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" GridLines="None"
            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            OnSelectedIndexChanged="gvDatos_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="Select"
                            CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/App_Themes/TuCuento/Imagenes/edicion.gif"
                            ToolTip="Seleccionar" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="nOrden" HeaderText="Orden" SortExpression="nOrden" ItemStyle-Width="10%" />
                <asp:BoundField DataField="sDesc_TipoDetalle" HeaderText="Tipo detalle" SortExpression="sDesc_TipoDetalle"
                    ItemStyle-Width="20%" />
                <asp:TemplateField HeaderText="Detalle" ItemStyle-Width="65%">
                    <ItemTemplate>
                        <asp:BulletedList ID="bltDetalle" Runat="server">
                        </asp:BulletedList>
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <br />
        <table class="tablaBotones">
            <tr>
                <td>
                    <asp:Button ID="btnAnteriorPaso2" runat="server" Text="Anterior" OnClick="btnAnterior_Click" />
                </td>
                <td>
                    <asp:Button ID="btnCancelarPaso2" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                </td>
                <td>
                    <asp:Button ID="btnAgregarTexto" runat="server" Text="+Texto" OnClick="btnAgregarTexto_Click" />
                </td>
                <td>
                    <asp:Button ID="btnAgregarInferencia" runat="server" Text="+Inferencia" OnClick="btnAgregarInferencia_Click" />
                </td>
                <td>
                    <asp:Button ID="btnGrabar" runat="server" Text="Grabar" OnClick="btnGrabar_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <!-- Panel Texto -->
    <asp:Panel ID="PanelTexto" runat="server">
        <table class="tablaCampos">
            <tr>
                <td class="tdtablaCamposTitulo">
                    Texto
                </td>
                <td class="tdtablaCamposControles">
                    <asp:TextBox ID="txtTexto" runat="server" SkinID="textoGrande" TextMode="MultiLine"
                        MaxLength="4000"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="tablaBotones">
            <tr>
                <td>
                    <asp:Button ID="btnCancelarPaso3" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                </td>
                <td>
                    <asp:Button ID="btnSiguientePaso3" runat="server" Text="Siguiente" OnClick="btnSiguiente_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <!-- Panel Inferencia -->
    <asp:Panel ID="PanelInferencia" runat="server">
        <!-- Panel Condiciones -->
        <asp:Panel ID="PanelCondiciones" runat="server">
            <table id="tablaCondiciones" class="tablaBotones">
                <tr>
                    <td class="tdtablaCamposTitulo" colspan="3">
                        Condiciones
                    </td>
                </tr>
                <tr>
                    <td style="width: 45%">
                        <asp:ListBox ID="lvCondiciones" runat="server" SelectionMode="Multiple" SkinID="listGrande"></asp:ListBox>
                    </td>
                    <td style="width: 10%">
                        <asp:Button ID="btnSelCond" runat="server" Text=">" SkinID="BotonChico" OnClick="btnSelCond_Click" />
                        <br />
                        <asp:Button ID="btnSelAllCond" runat="server" Text=">>" SkinID="BotonChico" OnClick="btnSelAllCond_Click" />
                        <br />
                        <asp:Button ID="btnDesSelCond" runat="server" Text="<" SkinID="BotonChico" OnClick="btnDesSelCond_Click" />
                        <br />
                        <asp:Button ID="btnDesSelAllCond" runat="server" Text="<<" SkinID="BotonChico" OnClick="btnDesSelAllCond_Click" />
                    </td>
                    <td style="width: 45%">
                        <asp:ListBox ID="lvCondicionesSel" runat="server" SkinID="listGrande" SelectionMode="Multiple"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table class="tablaBotones">
                            <tr>
                                <td>
                                    <asp:Button ID="btnCancelarPaso5" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSiguientePaso5" runat="server" Text="Siguiente" OnClick="btnSiguiente_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <!-- Panel Acciones -->
        <asp:Panel ID="PanelAcciones" runat="server">
            <table id="tablaAcciones" class="tablaBotones">
                <tr>
                    <td class="tdtablaCamposTitulo" colspan="3">
                        Acciones
                    </td>
                </tr>
                <tr>
                    <td style="width: 45%">
                        <asp:ListBox ID="lvAcciones" runat="server" SelectionMode="Multiple" SkinID="listGrande"></asp:ListBox>
                    </td>
                    <td style="width: 10%">
                        <asp:Button ID="btnSelAcc" runat="server" Text=">" SkinID="BotonChico" 
                            onclick="btnSelAcc_Click" />
                        <br />
                        <asp:Button ID="btnSelAllAcc" runat="server" Text=">>" SkinID="BotonChico" 
                            onclick="btnSelAllAcc_Click" />
                        <br />
                        <asp:Button ID="btnDesSelAcc" runat="server" Text="<" SkinID="BotonChico" 
                            onclick="btnDesSelAcc_Click" />
                        <br />
                        <asp:Button ID="btnDesSelAllAcc" runat="server" Text="<<" SkinID="BotonChico" 
                            onclick="btnDesSelAllAcc_Click" />
                    </td>
                    <td style="width: 45%">
                        <asp:ListBox ID="lvAccionesSel" runat="server" SkinID="listGrande" SelectionMode="Multiple"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table class="tablaBotones">
                            <tr>
                                <td>
                                    <asp:Button ID="btnAnteriorPaso6" runat="server" Text="Anterior" OnClick="btnAnterior_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelarPaso6" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSiguientePaso6" runat="server" Text="Siguiente" OnClick="btnSiguiente_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <!-- Panel DetalleInferencia -->
        <asp:Panel ID="PanelDetalleInferencia" runat="server">
            <table id="tablaInferencia" class="tablaBotones">
                <tr>
                    <td>
                        <div class="divGrilla">
                            <asp:GridView ID="gvCondiciones" runat="server" AutoGenerateColumns="false" GridLines="None"
                                AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowCreated="gvCondiciones_RowCreated">
                                <Columns>
                                    <asp:BoundField DataField="nCod_Condicion" HeaderText="C&#243;digo" SortExpression="nCod_Condicion" />
                                    <asp:BoundField DataField="sNombre" HeaderText="Nombre" SortExpression="sNombre"
                                        ItemStyle-Width="40%" />
                                    <asp:TemplateField HeaderText="Valor posible" ItemStyle-Width="60%">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlValPosible" runat="server" SkinID="ComboGrande">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="tdtablaCamposTitulo">
                        Acciones
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="lvAccionesInf" runat="server" SkinID="listGrande" 
                            Enabled="False" ></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tablaBotones">
                            <tr>
                                <td>
                                    <asp:Button ID="btnAnteriorPaso7" runat="server" Text="Anterior" OnClick="btnAnterior_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelarPaso7" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSiguientePaso7" runat="server" Text="Finalizar" OnClick="btnSiguiente_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <asp:HiddenField ID="HfAccion" runat="server" />
    <asp:HiddenField ID="HfnCod_Historia" runat="server" />
    <asp:HiddenField ID="HfnOrden" runat="server" />
</asp:Content>
