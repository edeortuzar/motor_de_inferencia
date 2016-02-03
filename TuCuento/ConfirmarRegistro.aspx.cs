using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ConfirmarRegistro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["sCod_Usuario"] == null)
            {
                LbMensaje.Text = "Ha ingresado a una página de manera indebida.";
                hfEstado.Value = "MAL";
                return;
            }

            string sCod_Usuario = Request.QueryString["sCod_Usuario"].ToString();

            if (sCod_Usuario.Trim().Length == 0)
            {
                LbMensaje.Text = "Ha ingresado a una página de manera indebida.";
                hfEstado.Value = "MAL";
            }
            else
            {
                //Valido que el usuario este y que tenga nHab=2
                TuCuento.Entidades.Usuario oUsuario = new TuCuento.Entidades.Usuario();
                oUsuario.sCod_Usuario = sCod_Usuario;

                DataTable oDatos = TuCuento.Negocio.Usuario_NEG.TraerUsuario(oUsuario);

                if (oDatos.Rows.Count == 0)
                {
                    LbMensaje.Text = "Ha ingresado a una página de manera indebida.";
                    hfEstado.Value = "MAL";
                }
                else
                {
                    if (oDatos.Rows[0]["nHab"].ToString() != "2")
                    {
                        LbMensaje.Text = "El usuario ya se encuentra activado o fue desactivado por el administrador de sistemas.";
                        hfEstado.Value = "MAL";
                    }
                    else
                    {
                        //Si está todo bien actualizo el estado a 1 para que pueda loguearse
                        oUsuario.nHab = 1;

                        if (TuCuento.Negocio.Usuario_NEG.ActualizarEstado(oUsuario))
                        {
                            LbMensaje.Text = "El usuario se activo correctamente.";
                            hfEstado.Value = "BIEN";
                        }
                        else
                        {
                            LbMensaje.Text = "Ocurrio un error al activar el usuario, por favor, pongase en contacto con el administrador de sistemas.";
                            hfEstado.Value = "MAL";
                        }
                    }
                }

            }
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        if (hfEstado.Value == "MAL")
        {
            Response.Redirect("default.aspx");
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }
}
