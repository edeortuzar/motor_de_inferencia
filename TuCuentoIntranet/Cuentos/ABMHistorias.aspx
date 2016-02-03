<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.master" AutoEventWireup="true"
    CodeFile="ABMHistorias.aspx.cs" Inherits="Cuentos_ABMHistorias" StylesheetTheme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function EditarHistoria(nCod_Historia) {
            window.location = "HistoriaDetalle.aspx?nCod_Historia=" + nCod_Historia;
        }

    </script>
    
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
            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" OnRowCreated="gvDatos_RowCreated">
            <Columns>
                <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <img src="<%=Page.ResolveUrl("~/App_Themes/TuCuento/Imagenes/edicion.gif") %>" title="Seleccionar" alt="Seleccionar" 
                        onclick="EditarHistoria('<%# DataBinder.Eval(Container.DataItem, "nCod_Historia").ToString() %>')" class="CursorMano" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="sNombre" HeaderText="Nombre" SortExpression="sNombre"
                    ItemStyle-Width="32%" />
                <asp:BoundField DataField="sDescripcion" HeaderText="Descripci&#243;n" SortExpression="sDescripcion"
                    ItemStyle-Width="33%" />
                <asp:TemplateField HeaderText="Hab." ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkHabGrilla" Enabled="false" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "nHab").ToString() == "1" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="nCod_Dominio" HeaderText="C&#243;digo" SortExpression="nCod_Dominio" />
                <asp:BoundField DataField="sDesc_Dominio" HeaderText="Dominio" SortExpression="sDesc_Dominio"
                    ItemStyle-Width="22%" />
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <table class="tablaBotones">
        <tr>
            <td>
                <asp:Button ID="btnNuevo" runat="server" Text="Nueva Historia" 
                    onclick="btnNuevo_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
            </td>
        </tr>
    </table>
    
</asp:Content>
