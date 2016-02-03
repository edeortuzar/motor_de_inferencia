using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtsCod_Usuario.Focus();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {       
        Boolean bOk = true;
        string sCod_Usuario = txtsCod_Usuario.Text.Trim();
        string sPSW = txtsPSW.Text.Trim();
        //sCod_Usuario = "edeortuzar";
        //sPSW = "INIT";

        if (sCod_Usuario.Length == 0 || sPSW.Length == 0)
        {
            LbMensaje.Text = "Debe completar el campo Usuario y el campo Constraseña.";
            bOk = false;
        }
        
        if (bOk)
        {
            TuCuento.Entidades.Usuario oUsuario = new TuCuento.Entidades.Usuario();
            TuCuento.Negocio.Usuario_NEG oNegocio = new TuCuento.Negocio.Usuario_NEG();
            
            oUsuario = oNegocio.ValidarUsuario(sCod_Usuario, sPSW);

            //Blanqueo las variables de session
            for (int nI = 0; nI < Session.Count; nI++)
            {
                Session[nI] = null;
            }

            if (oUsuario.sCod_Usuario != null)
            {
                Session["oUsuarioInternet"] = oUsuario;
                Response.Redirect("default.aspx");
            }
            else
            {
                Session["oUsuarioInternet"] = null;
                LbMensaje.Text = "El usuario y/o la contraseña son incorrectos o todavía no activo su usuario.";
            }

        }

    }
}
