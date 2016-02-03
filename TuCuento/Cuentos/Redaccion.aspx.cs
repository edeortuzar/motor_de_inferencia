using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Cuentos_Redaccion : System.Web.UI.Page
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarGrilla();
            if (Request.QueryString["nResultado"] != null)
            {
                if (Request.QueryString["nResultado"].ToString() == "1")
                {
                    LbMensaje.Text = "Se actualizaron los datos.";
                }
            }
        }
    }

    protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Oculto las columnas de códigos
        if (e.Row.Cells.Count > 1)
            e.Row.Cells[1].Visible = false;
    }

    #endregion

    #region Metodos

    private void CargarGrilla()
    {
        DataTable oTabla = TuCuento.Negocio.Dominio_NEG.ListarDominios(-1, 1);

        gvDatos.DataSource = oTabla;
        gvDatos.DataBind();

    }

    #endregion
}
