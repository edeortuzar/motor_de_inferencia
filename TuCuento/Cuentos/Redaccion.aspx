<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.Master" AutoEventWireup="true" CodeFile="Redaccion.aspx.cs" Inherits="Cuentos_Redaccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript">

        function EditarHistoria(nCod_Dominio, sDesc_Dominio) {
            window.location = "RedactarCuento.aspx?nCod_Dominio=" + nCod_Dominio + '&sDesc_Dominio=' + sDesc_Dominio;
        }

    </script>

    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" GridLines="None"
        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
        OnRowCreated="gvDatos_RowCreated">
        <Columns>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <img src="<%=Page.ResolveUrl("~/App_Themes/TuCuento/Imagenes/edicion.gif") %>" title="Seleccionar" alt="Seleccionar" 
                    onclick="EditarHistoria('<%# DataBinder.Eval(Container.DataItem, "nCod_Dominio").ToString() %>', '<%# DataBinder.Eval(Container.DataItem, "sDesc_Dominio").ToString() %>')" class="CursorMano" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="nCod_Dominio" HeaderText="C&#243;digo" SortExpression="nCod_Dominio" />
            <asp:BoundField DataField="sDesc_Dominio" HeaderText="Historia" SortExpression="sDesc_Dominio"
                ItemStyle-Width="95%" />
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <asp:Label ID="LbMensaje" runat="server" SkinID="LabelRojo"></asp:Label>

</asp:Content>

