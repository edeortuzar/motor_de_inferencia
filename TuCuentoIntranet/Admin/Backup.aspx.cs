using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BackUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        string sPath = txtPath.Text.Trim();
        string sNombreArchivo = txtNomArchivo.Text.Trim();
        
        if (sPath.Substring(sPath.Length-1,1)!="\\")
        {
            sPath = sPath + "\\";
        }

        sNombreArchivo = sPath + sNombreArchivo;

        if (sNombreArchivo.Length > 2000)
        {
            Master.MensajeError = "El path y el nombre de archivo no pueden superar los 2000 caracteres.";
        }
        else
        {
            if (TuCuento.Negocio.Backup_NEG.HacerBKBD(sNombreArchivo))
            {
                LbMensaje.Text = "El backup se realizo con éxito.";
            }
            else
            {
                Master.MensajeError = "Ocurrio un error al realizar el backup, por favor, verifique los datos ingresados.";
            }
        }
    }
    protected void btnRestore_Click(object sender, EventArgs e)
    {
        string sPath = txtPath.Text.Trim();
        string sNombreArchivo = txtNomArchivo.Text.Trim();

        if (sPath.Substring(sPath.Length - 1, 1) != "\\")
        {
            sPath = sPath + "\\";
        }

        sNombreArchivo = sPath + sNombreArchivo;

        if (sNombreArchivo.Length > 2000)
        {
            Master.MensajeError = "El path y el nombre de archivo no pueden superar los 2000 caracteres.";
        }
        else
        {
            if (TuCuento.Negocio.Backup_NEG.HacerRestoreBD(sNombreArchivo))
            {
                LbMensaje.Text = "El restore se realizo con éxito, por favor, salga y vuelva a ingresar a la aplicación.";
            }
            else
            {
                Master.MensajeError = "Ocurrio un error al realizar el restore, por favor, salga y vuelva a ingresar a la aplicación.";
            }
        }
    }
}
