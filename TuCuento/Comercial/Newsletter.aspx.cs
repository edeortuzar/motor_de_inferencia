using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class Newsletter : System.Web.UI.Page
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rdSuscribir.Checked = true;
            rdNoSuscribir.Checked = false;
            txtEmail.Focus();
        }
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        bool bResp = true;

        Master.MensajeError = "";

        if (txtEmail.Text.Trim().Length == 0)
        {
            Master.MensajeError = "Debe completar el campo e-mail.";

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

            if (!Regex.IsMatch(txtEmail.Text.Trim(), ExpresionReg))
            {
                Master.MensajeError = "Debe completar el campo e-mail con e-mail válido.";

                bResp = false;
            }
        }

        if (bResp)
        {
            if (TuCuento.Negocio.Newsletter_NEG.PersistirSuscripcion(txtEmail.Text.Trim(), (rdSuscribir.Checked ? 1 : 0)))
            {
                LbMensaje.Text = "La " + (rdSuscribir.Checked ? "suscripción" : "baja") + " al newsletter quedo registrada.";
            }
            else
            {
                Master.MensajeError = "Ocurrio un error, por favor, intente más tarde. Disculpe, las molestias.";
            }

        }

    }

    #endregion
}
