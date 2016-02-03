<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.master" AutoEventWireup="true" CodeFile="Bitacora.aspx.cs" Inherits="Bitacora" Theme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
        function AbrirCalendario(sOrigen) {
            window.open(document.getElementById("hfPathCalendario").value + '?destino=' + sOrigen, 'Calendario', 'width=220,height=200,left=270,top=180')
        }

        function openPopup() {
            var dFechaDesde = document.getElementById('ctl00_ContentPlaceHolder1_txtFechaDesde').value;
            var dFechaHasta = document.getElementById('ctl00_ContentPlaceHolder1_txtFechaHasta').value;
            var sURL;
            var w = document.getElementById('ctl00_ContentPlaceHolder1_ddlUsuarios').selectedIndex;
            var sUsuario = document.getElementById('ctl00_ContentPlaceHolder1_ddlUsuarios').options[w].text;
            w = document.getElementById('ctl00_ContentPlaceHolder1_ddlPatente').selectedIndex;
            var sPatente = document.getElementById('ctl00_ContentPlaceHolder1_ddlPatente').options[w].text;

            sURL = 'ReporteBitacora.aspx?dFechaDesde=' + dFechaDesde.replace("/", "") + '&dFechaHasta=' + dFechaHasta.replace("/", "") + '&sUsuario=' + sUsuario + '&sPatente=' + sPatente;
            window.open(sURL, '', 'width=770,height=600,top=0,left=0,scrollbars=yes,toolbar=yes');
        }

    </script>

    <table class="tablaCampos">
        <tr>
            <td class="tdtablaCamposTitulo">
                Fecha desde</td>
            <td class="tdtablaCamposControles">
                <input id="txtFechaDesde" runat="server" type="text" disabled="disabled" />
                <a href="javascript:AbrirCalendario('ctl00_ContentPlaceHolder1_txtFechaDesde');">
                    <img src="<%=Page.ResolveUrl("~/App_Themes/TuCuento/Imagenes/calendario.gif") %>"
                        alt="Calendario" />    
            </td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Fecha hasta</td>
            <td class="tdtablaCamposControles">
                <input id="txtFechaHasta" runat="server" type="text" disabled="disabled" />
                <a href="javascript:AbrirCalendario('ctl00_ContentPlaceHolder1_txtFechaHasta');">
                    <img src="<%=Page.ResolveUrl("~/App_Themes/TuCuento/Imagenes/calendario.gif") %>"
                        alt="Calendario" />    
            </td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Usuario</td>
            <td class="tdtablaCamposControles">
                <asp:DropDownList ID="ddlUsuarios" runat="server" SkinID="ComboGrande">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdtablaCamposTitulo">
                Patente</td>
            <td class="tdtablaCamposControles">
                <asp:DropDownList ID="ddlPatente" runat="server" SkinID="ComboGrande">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="tablaBotones">
                    <tr>
                        <td>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                                onclick="btnBuscar_Click" /></td>
                        <td>
                            <input type="button" value="Exportar" onclick="openPopup()" class="boton" />    
                        </td>
                    </tr>
                </table>
                <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false"
    GridLines="None"
    AllowPaging="false"
    CssClass="mGrid"
    PagerStyle-CssClass="pgr"
    AlternatingRowStyle-CssClass="alt">
       <Columns>
            <asp:BoundField DataField="dFecha" HeaderText="Fecha" SortExpression="dFecha" />
            <asp:BoundField DataField="sCod_Usuario" HeaderText="Usuario" SortExpression="sCod_Usuario" />
            <asp:BoundField DataField="sDesc_Patente" HeaderText="Patente" SortExpression="sDesc_Patente" />
        </Columns>
    </asp:GridView>
    <input type="hidden" id="hfPathCalendario" value="<%=Page.ResolveUrl("~/CalendarioPopUp.aspx") %>" />
</asp:Content>

