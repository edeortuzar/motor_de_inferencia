using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class CambioClave : System.Web.UI.Page
{
    #region Propiedades

    private string _sMensajeError;
    string nl = Environment.NewLine;

    private string sMensajeError
    {
        get { return _sMensajeError; }
        set
        {
            if (_sMensajeError == "")
            {
                _sMensajeError = value;
            }
            else
            {
                _sMensajeError = _sMensajeError + nl + value;
            }
        }
    }

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlRegistro.Visible = true;
            pnlResultado.Visible = false;
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        Master.MensajeError = "";

        if (Validar())
        {
            TuCuento.Entidades.Usuario oUsuario = new TuCuento.Entidades.Usuario();
            TuCuento.Negocio.Usuario_NEG oNegocio = new TuCuento.Negocio.Usuario_NEG();
            
            oUsuario = oNegocio.ValidarUsuario(Master.sCod_Usuario, txtPswOLD.Text.Trim());

            if (oUsuario.sCod_Usuario != null)
            {
                //Actualizo la contraseña a la nueva contraseña
                oUsuario.sPSW = txtPsw.Text.Trim();
            }
            else
            {
                LbMensaje.Text = "La contraseña actual especificada es incorrecta.";
                return;
            }

            if (TuCuento.Negocio.Usuario_NEG.ActualizarPSW(oUsuario))
            {
                pnlRegistro.Visible = false;
                pnlResultado.Visible = true;

                //Envio el mail de que se modifico la contraseña
                string bodyHTML = string.Empty;
                string sCod_Usuario = Master.sCod_Usuario;
                Funciones oFunc = new Funciones();

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
	                        <h4 style='font-family:Arial, Helvetica, sans-serif;color:#000;'>Se ha cambiado su contraseña. La nueva contraseña es";
                bodyHTML = bodyHTML + oUsuario.sPSW + @"</h4>
                        </td>
                        </tr>
                        <tr>
                        <td colspan='2' style='background-color:#3399FF; color:#FFFFFF; font-family:Arial, Helvetica, sans-serif; font-size:.8em; border-top:1px solid #000;'>Copyright (c) 2010. TuCuento.com.ar</td>
                        </tr>
                        </body>
                        </html>";

                if (oFunc.EnviarMail(oUsuario.sEmail, "tucuento@tucuento.com.ar", bodyHTML, "TuCuento - Cambio de contraseña"))
                {
                    LbMensajeFinal.Text = "La contraseña se modifico correctamente.";
                }
                else
                {
                    LbMensajeFinal.Text = "La contraseña se modifico correctamente.";
                }
            }
            else
                LbMensaje.Text = "Ocurrio un error al actualizar los datos";

        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }

    #endregion

    #region Metodos

    private bool Validar()
    {
        bool bResp = true;
        bool bValPSW = true;

        if (txtPswOLD.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo contraseña actual.";
            bResp = false;
            bValPSW = false;
        }
        
        if (txtPsw.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo nueva contraseña.";
            bResp = false;
            bValPSW = false;
        }

        if (txtPswRepeat.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo repetir nueva contraseña.";
            bResp = false;
            bValPSW = false;
        }

        if (bValPSW && txtPsw.Text.Trim() != txtPswRepeat.Text.Trim())
        {
            sMensajeError = "La contraseña nueva no es igual a la contraseña nueva que repitio.";
            bResp = false;
        }

        if (bValPSW && bResp && !ValidarPSW())
        {
            bResp = false;
        }

        if (bValPSW && bResp && txtPsw.Text.Trim()==txtPswOLD.Text.Trim())
        {
            sMensajeError = "La contraseña nueva no puede ser igual a la contraseña actual.";
            bResp = false;
        }

        if (!bResp)
            Master.MensajeError = sMensajeError;

        return bResp;
    }

    private bool ValidarPSW()
    {
        bool bResp = true;

        string sEspacio = " ";
        string sPSW = txtPsw.Text;
        Regex rValPSW;
        string sMensajeErrorPSW = "";

        //No puede contener espacios
        if (sPSW.IndexOf(sEspacio) > -1)
        {
            sMensajeErrorPSW += "\nLa contraseña no puede contener espacios.\n";
        }

        //Debe contener letras tanto en mayusculas y minusculas como caracteres numericos. También verifica que el tamaño de la contraseña sea de 6 a 15 caracteres.
        rValPSW = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,15}$");
        if (!(rValPSW.IsMatch(sPSW)))
        {
            sMensajeErrorPSW += "\nLa contraseña debe contener letras tanto en mayusculas y minusculas como caracteres numericos. el tamaño de la contraseña sea de 6 a 15 caracteres.\n";
        }

        //Si tiene algo mal muestro el error
        if (sMensajeErrorPSW != "")
        {
            sMensajeError = sMensajeErrorPSW;
            bResp = false;
        }

        return bResp;
    }

    #endregion
}
