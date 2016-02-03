using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data;

public partial class RecuperoClave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        string bodyHTML = string.Empty;
        string sCod_Usuario = txtsCod_Usuario.Text.Trim();
        TuCuento.Entidades.Usuario oUsuario;
        Funciones oFunc = new Funciones();

        if (sCod_Usuario.Length == 0)
        {
            LbMensaje.Text = "Debe completar el campo Usuario.";
            return;
        }

        //Recupero el mail del usuario
        oUsuario = new TuCuento.Entidades.Usuario();
        oUsuario.sCod_Usuario = sCod_Usuario;

        DataTable oDatos = TuCuento.Negocio.Usuario_NEG.TraerUsuario(oUsuario);

        if (oDatos.Rows.Count == 0)
        {
            LbMensaje.Text = "El usuario es incorrecto o inexistente.";
            return;
        }

        bodyHTML = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'
                        'http://www.w3.org/TR/html4/loose.dtd'>
                    <html lang='es'>
                    <head>
                    </head>
                    <body>
                    <table style='width:800px; border:1px solid #000;' cellpadding='10' cellspacing='0' align='center'>
                    <tr>
                    <td style='background-color:#3399FF; border-bottom:1px solid #000;'>
                    <h1 style='text-align:center; color:#ffffff;'>TuCuento.com.ar</h1></td>
                    </tr>
                    <tr>
                    <td style='border-right:1px solid #000; vertical-align:top; background-color:#FFFFFF;'>
	                    <h4 style='font-family:Arial, Helvetica, sans-serif;color:#000;'>Te recordamos que tu usuarios es:";
        bodyHTML = bodyHTML + sCod_Usuario + @"</h4>
	                    <h4 style='font-family:Arial, Helvetica, sans-serif;color:#000;'>Y tu contraseña es: ";
        bodyHTML = bodyHTML + TuCuento.Negocio.Usuario_NEG.Desencriptar(oDatos.Rows[0]["sPSW"].ToString()) + @"</h4>
                    </td>
                    </tr>
                    <tr>
                    <td colspan='2' style='background-color:#3399FF; color:#FFFFFF; font-family:Arial, Helvetica, sans-serif; font-size:.8em; border-top:1px solid #000;'>Copyright (c) 2010. TuCuento.com.ar</td>
                    </tr>
                    </body>
                    </html>";

        if (oFunc.EnviarMail(oDatos.Rows[0]["sEmail"].ToString(), "tucuento@tucuento.com.ar", bodyHTML, "TuCuento - Recupero de clave"))
        {
            LbMensaje.Text = "Se envió un mail con la contraseña a la casilla de e-mail registrada, por favor, verifique su casilla.";
        }
        else
        {
            LbMensaje.Text = "Ocurrio un error en el envío de la contraseña, por favor, intente más tarde.";
        }

    }

}
