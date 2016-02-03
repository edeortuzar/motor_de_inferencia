using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;

public partial class Registro : System.Web.UI.Page
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
            CargarComboPreguntas();
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (Validar())
        {
            TuCuento.Entidades.Usuario oUsuario = new TuCuento.Entidades.Usuario();
            TuCuento.Entidades.Familia oFlia = new TuCuento.Entidades.Familia();

            oUsuario.sCod_Usuario = txtUsuario.Text.Trim();
            oUsuario.sNombre = txtNombre.Text.Trim();
            oUsuario.sApellido = txtApellido.Text.Trim();
            oUsuario.sEmail = txtMail.Text.Trim();
            oUsuario.sPSW = txtPsw.Text.Trim();
            oUsuario.Pregunta.nCod_Pregunta = Convert.ToInt32(ddlPregunta.SelectedValue);
            oUsuario.sRespuesta = txtRespuesta.Text.Trim();


            oFlia.nCod_Flia = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["FliaVisitante"].ToString());
            oUsuario.lstFamilias.Add(oFlia);

            oUsuario.nHab = 2;

            if (TuCuento.Negocio.Usuario_NEG.Persistir(oUsuario))
            {
                pnlRegistro.Visible = false;
                pnlResultado.Visible = true;

                //Envio el mail para activar la cuenta
                string bodyHTML = string.Empty;
                string sCod_Usuario = txtUsuario.Text.Trim();
                string sURL = GetApplicationPath() + "/ConfirmarRegistro.aspx?sCod_Usuario=" + sCod_Usuario;
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
                        <h1 style='text-align:center; color:#ffffff;'>Bienvenido/a a TuCuento.com.ar</h1></td>
                        </tr>
                        <tr>
                        <td style='border-right:1px solid #000; vertical-align:top; background-color:#FFFFFF;'>
	                        <h4 style='font-family:Arial, Helvetica, sans-serif;color:#000;'>Los datos de tu usuario son: </h4>
	                        <h4 style='font-family:Arial, Helvetica, sans-serif;color:#000;'>Usuario: ";
                bodyHTML = bodyHTML + sCod_Usuario + @" </h4>
	                        <h4 style='font-family:Arial, Helvetica, sans-serif;color:#000;'>Y tu contraseña es: ";
                bodyHTML = bodyHTML + txtPsw.Text.Trim() + @"</h4>
	                        </br>
	                        <h4 style='font-family:Arial, Helvetica, sans-serif;color:#000;'>Por favor, para activar tu usuario tienes que pinchar sobre el siguiente link:</h4>
	                        <a href='";
                bodyHTML = bodyHTML + sURL + @"'>";
                bodyHTML = bodyHTML + sURL + @"</a>
                        </td>
                        </tr>
                        <tr>
                        <td colspan='2' style='background-color:#3399FF; color:#FFFFFF; font-family:Arial, Helvetica, sans-serif; font-size:.8em; border-top:1px solid #000;'>Copyright (c) 2010. TuCuento.com.ar</td>
                        </tr>
                        </body>
                        </html>";

                if (oFunc.EnviarMail(oUsuario.sEmail, "tucuento@tucuento.com.ar", bodyHTML, "TuCuento - Activar cuenta"))
                {
                    LbMensajeFinal.Text = "Gracias por registrarte. Se envió un mail a la casilla " + oUsuario.sEmail + ". Ahí encontraras información para activar tu cuenta.";
                }
                else
                {
                    LbMensajeFinal.Text = "Hubo un error al envíar el mail para la activación de la cuenta. Por favor, pongase en contacto con el administrador de sistemas.";
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

    private void CargarComboPreguntas()
    {
        DataTable oDato = TuCuento.Negocio.PreguntaClave_NEG.ListarPreguntaClave(-1, 1);
        ddlPregunta.DataSource = oDato;
        ddlPregunta.DataTextField = "sPregunta";
        ddlPregunta.DataValueField = "nCod_Pregunta";
        ddlPregunta.DataBind();

    }

    private bool Validar()
    {
        bool bResp = true;
        bool bValPSW = true;

        if (txtUsuario.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el nombre de usuario.";
            bResp = false;
        }
        else
        {
            //Verifico que el nombre de usuario no este siendo usado
            TuCuento.Entidades.Usuario oUsuario = new TuCuento.Entidades.Usuario();
            oUsuario.sCod_Usuario = txtUsuario.Text.Trim();
            DataTable oData = TuCuento.Negocio.Usuario_NEG.TraerUsuario(oUsuario);

            if (oData.Rows.Count > 0)
            {
                sMensajeError = "El nombre de usuario especificado ya esta registrado, por favor, modifique el nombre de usuario.";
                bResp = false;
            }
        }

        if (txtNombre.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar su nombre.";
            bResp = false;
        }

        if (txtApellido.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar su apellido.";
            bResp = false;
        }
        
        if (txtPsw.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo contraseña.";
            bResp = false;
            bValPSW = false;
        }

        if (txtPswRepeat.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo repetir contraseña.";
            bResp = false;
            bValPSW = false;
        }

        if (bValPSW && txtPsw.Text.Trim() != txtPswRepeat.Text.Trim())
        {
            sMensajeError = "La contraseña no es igual a la contraseña que repitio.";
            bResp = false;
        }

        if (bValPSW && bResp && !ValidarPSW())
        {
            bResp = false;
        }

        if (txtMail.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar su e-mail.";
            bResp = false;
        }
        else
        {
            string ExpresionReg = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
              + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
                [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
                [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
              + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

            if (!Regex.IsMatch(txtMail.Text.Trim(), ExpresionReg))
            {
                sMensajeError = "Debe completar el campo e-mail con e-mail válido.";

                bResp = false;
            }
        }

        if (!bResp)
            Master.MensajeError = sMensajeError;
        
        return bResp;
    }

    private bool ValidarPSW()
    {
        bool bResp = true;

        string sEspacio  = " ";
        string sPSW  = txtPsw.Text; 
        Regex rValPSW;
        string sMensajeErrorPSW = "";
 
        //No puede contener espacios
        if (sPSW.IndexOf(sEspacio) > -1) {
             sMensajeErrorPSW += "\nLa contraseña no puede contener espacios.\n";
        }

        //Debe contener letras tanto en mayusculas y minusculas como caracteres numericos. También verifica que el tamaño de la contraseña sea de 6 a 15 caracteres.
        rValPSW = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,15}$");
        if (!(rValPSW.IsMatch(sPSW)))
        {
            sMensajeErrorPSW += "\nLa contraseña debe contener letras tanto en mayusculas y minusculas como caracteres numericos. el tamaño de la contraseña sea de 6 a 15 caracteres.\n";
        }

        //Si tiene algo mal muestro el error
        if (sMensajeErrorPSW != ""){
            sMensajeError = sMensajeErrorPSW;
            bResp = false;
        }

        return bResp;
    }

    public string GetApplicationPath()
    {
        string applicationPath = "";

        if (this.Page.Request.Url != null)
            applicationPath = this.Page.Request.Url.AbsoluteUri.Substring(
             0, this.Request.Url.AbsoluteUri.ToLower().IndexOf(
              this.Request.ApplicationPath.ToLower(),
               this.Request.Url.AbsoluteUri.ToLower().IndexOf(
              this.Page.Request.Url.Authority.ToLower()) +
              this.Page.Request.Url.Authority.Length) +
             this.Request.ApplicationPath.Length);
        return applicationPath;
    }

    #endregion
}
