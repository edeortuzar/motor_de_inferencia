<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.master" AutoEventWireup="true"
    CodeFile="ABMDominios.aspx.cs" Inherits="Cuentos_ABMDominios" StylesheetTheme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divGrilla">
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
                <asp:BoundField DataField="nCod_Dominio" HeaderText="C&#243;digo" SortExpression="nCod_Dominio" />
                <asp:BoundField DataField="sDesc_Dominio" HeaderText="Descripci&#243;n" SortExpression="sDesc_Dominio"
                    ItemStyle-Width="90%" />
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
                Descripción
            </td>
            <td class="tdtablaCamposControles">
                <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
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
                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar" OnClick="btnGrabar_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo Dominio" OnClick="btnNuevo_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
                <asp:HiddenField ID="HfAccion" runat="server" />
                <asp:HiddenField ID="HfnCod_Dominio" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
