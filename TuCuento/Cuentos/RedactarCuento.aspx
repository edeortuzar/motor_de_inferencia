<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.Master" AutoEventWireup="true"
    CodeFile="RedactarCuento.aspx.cs" Inherits="Cuentos_RedactarCuento" StylesheetTheme="TuCuento" %>

<%@ MasterType VirtualPath="~/MasterPage/TuCuento.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="PanelRedaccion" runat="server">
        <asp:GridView ID="gvCondiciones" runat="server" AutoGenerateColumns="false" GridLines="None"
            AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
            OnRowCreated="gvCondiciones_RowCreated">
            <Columns>
                <asp:BoundField DataField="nCod_Entidad" HeaderText="C&#243;digo" SortExpression="nCod_Entidad" />
                <asp:BoundField DataField="nCod_Atributo" HeaderText="C&#243;digo" SortExpression="nCod_Atributo" />
                <asp:BoundField DataField="sAtributo" HeaderText="Atributo" SortExpression="sAtributo"
                    ItemStyle-Width="40%" />
                <asp:TemplateField HeaderText="Valor posible" ItemStyle-Width="60%">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlValPosible" runat="server" SkinID="ComboGrande">
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <table class="tablaBotones">
            <tr>
                <td>
                    <asp:Button ID="btnAtras" runat="server" Text="Atras" 
                        onclick="btnAtras_Click" />
                </td>
                <td>
                    <asp:Button ID="btnGenerar" runat="server" Text="Generar" OnClick="btnGenerar_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelCuento" runat="server">
        <table class="tablaBotones">
            <tr>
                <td>
                    <asp:TextBox ID="txtTexto" runat="server" SkinID="textoGrande" 
                        TextMode="MultiLine" ></asp:TextBox>
                </td>
            </tr>
        </table>
        
        <table class="tablaBotones">
            <tr>
                <td>
                    <asp:Button ID="btnAtrasCuento" runat="server" Text="Atras" 
                        onclick="btnAtrasCuento_Click" />
                </td>
                <td>
                    <asp:Button ID="btnGrabar" runat="server" Text="Grabar" 
                        onclick="btnGrabar_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="HfnCod_Dominio" runat="server" />
    <asp:HiddenField ID="HfsDesc_Dominio" runat="server" />
</asp:Content>
