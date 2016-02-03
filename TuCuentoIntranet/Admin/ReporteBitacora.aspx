<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReporteBitacora.aspx.cs"
    Inherits="Reportes_ReporteFinanciero" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TuCuento</title>
    <link rel="stylesheet" type="text/css" media="all" href="<%=Page.ResolveUrl("~/App_Themes/EstilosImpresion.css") %>" />
</head>
<body style="background-color: #ffffff;">
    <form id="form1" runat="server">
    <table id="contenedor" style="height: 100%; page-break-after: avoid;" cellpadding="0">
        <thead style="display: table-header-group;">
            <tr>
                <td>
                    <table id="CabeceraCont" class="Cabecera" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="center">
                                <img src="../App_Themes/TuCuento/Imagenes/Logo.png" alt="TuCuento" height="80px" width="80px" />
                                <hr style="color: #3f3f3f; width: 100%" />
                                <table border="0" style="width: 100%">
                                    <tr>
                                        <td align="center">
                                            <h5>
                                                <asp:Label ID="Label1" runat="server" Text="Reporte bitacora"></asp:Label>
                                            </h5>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <table border="0" cellpadding="0" cellspacing="2" width="100%" align="left">
                                                <tr class="Texto">
                                                    <td align="right">
                                                        Fecha desde:
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblFechaDesde" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Fecha hasta:
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblFechaHasta" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Texto">
                                                    <td align="right">
                                                        Usuario:
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        Patente:
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:Label ID="lblPatente" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <asp:GridView ID="gvDatos" runat="server" Width="100%" AllowSorting="false"
                                                AllowPaging="false" PageSize="15" AutoGenerateColumns="false">
                                                <RowStyle HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:BoundField SortExpression="dFecha" HeaderText="Fecha"
                                                        DataField="dFecha" />
                                                    <asp:BoundField SortExpression="sCod_Usuario" HeaderText="Usuario"
                                                        DataField="sCod_Usuario" />
                                                    <asp:BoundField SortExpression="sDesc_Patente" HeaderText="Patente"
                                                        DataField="sDesc_Patente" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </thead>
    </table>
    </form>
</body>
</html>
