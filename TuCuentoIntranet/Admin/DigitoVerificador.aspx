<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/TuCuento.master" AutoEventWireup="true"
    CodeFile="DigitoVerificador.aspx.cs" Inherits="DigitoVerificador" Theme="TuCuento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="False" GridLines="None"
        AllowPaging="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
        <Columns>
            <asp:BoundField DataField="sTabla" HeaderText="Tabla" SortExpression="sTabla" />
            <asp:BoundField DataField="nDVV" HeaderText="Vertical" SortExpression="nDVV" />
            <asp:BoundField DataField="nDVH" HeaderText="Horizontal" SortExpression="nDVH" />
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <table class="tablaBotones">
        <tr>
            <td>
                <asp:Button ID="btnCorregir" runat="server" Text="Corregir" 
                    onclick="btnCorregir_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
