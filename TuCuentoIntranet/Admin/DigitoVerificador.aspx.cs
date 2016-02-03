using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DigitoVerificador : System.Web.UI.Page
{
    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargarGrilla();
    }
    protected void btnCorregir_Click(object sender, EventArgs e)
    {
        //TODO
    }
    #endregion

    #region Metodos

    private void CargarGrilla()
    {
        gvDatos.DataSource = TuCuento.Negocio.DigitoVerificador_NEG.ListarInformacion();
        gvDatos.DataBind();

    }

    #endregion
}
