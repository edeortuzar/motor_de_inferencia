<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Encuesta.aspx.cs" Inherits="Comercial_Encuesta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Encuesta</title>
    <link href="../App_Themes/TuCuento/default.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        function btnSend_Click() {

            var nRespuesta = -1;

            for (i = 0; i < document.forms.item(0).respuesta.length; i++) {
                if (document.forms.item(0).respuesta[i].checked) {
                    nRespuesta = i + 1;
                }
            }


            if (nRespuesta == -1) {
                alert("Debe seleccionar una opcion.");
            }
            else {
                window.opener.location = "RespuestaEncuesta.aspx?nRespuesta=" + nRespuesta + '&nId=' + document.getElementById("hId").value + '&sCod_Usuario=' + document.getElementById("hsCod_Usuario").value;
                self.close();
            }
        }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="hId" type="hidden" runat="server" />
    <input id="hsCod_Usuario" type="hidden" runat="server" />
    <table class="tablaVerde">
        <tr>
            <td align="center" colspan="3">
                <asp:Label ID="lblTituloEncuesta" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3">
                <asp:Label ID="lblPregunta" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
