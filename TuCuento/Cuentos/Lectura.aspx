<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.Master" AutoEventWireup="true"
    CodeFile="Lectura.aspx.cs" Inherits="Cuentos_Lectura" StylesheetTheme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="PanelGrilla" runat="server">
        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" GridLines="None"
            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            OnSelectedIndexChanged="gvDatos_SelectedIndexChanged" OnRowCreated="gvDatos_RowCreated">
            <Columns>
                <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="Select"
                            CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/App_Themes/TuCuento/Imagenes/edicion.gif"
                            ToolTip="Seleccionar" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="nCod_Cuento" HeaderText="C&#243;digo" SortExpression="nCod_Cuento" />
                <asp:BoundField DataField="sCod_Usuario" HeaderText="Usuario" SortExpression="sCod_Usuario"
                    ItemStyle-Width="22%" />
                <asp:BoundField DataField="sDesc_Dominio" HeaderText="Historia" SortExpression="sNombre"
                    ItemStyle-Width="24%" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="PanelTexto" runat="server">
        <table class="tablaBotones">
            <tr>
                <td>
                    <asp:TextBox ID="txtTexto" runat="server" SkinID="textoGrande" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="tablaBotones">
            <tr>
                <td>
                    <asp:Button ID="btnAtras" runat="server" Text="Atras" 
                        onclick="btnAtras_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
