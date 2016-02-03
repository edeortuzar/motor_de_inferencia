using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Bitacora : System.Web.UI.Page
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Master.MensajeError = "";
            CargarComboPatentes();
            CargarComboUsuarios();
            Funciones.LlenarGrillaVacia(gvDatos);
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarGrilla();
    }

    #endregion

    #region Metodos y Funciones

    private void CargarGrilla()
    {
        DataTable oData = new DataTable();
        DateTime dFechaDesde = new DateTime(Convert.ToInt32(txtFechaDesde.Value.Substring(6, 4)), Convert.ToInt32(txtFechaDesde.Value.Substring(3, 2)), Convert.ToInt32(txtFechaDesde.Value.Substring(0, 2)));
        DateTime dFechaHasta = new DateTime(Convert.ToInt32(txtFechaHasta.Value.Substring(6, 4)), Convert.ToInt32(txtFechaHasta.Value.Substring(3, 2)), Convert.ToInt32(txtFechaHasta.Value.Substring(0, 2)));

        oData = TuCuento.Negocio.Bitacora_NEG.TraerBitacora(dFechaDesde, dFechaHasta, ddlUsuarios.SelectedValue, Convert.ToInt32(ddlPatente.SelectedValue));

        Session["DatosBitacora"] = oData;

        gvDatos.DataSource = oData;
        gvDatos.DataBind();

        Funciones.LlenarGrillaVacia(gvDatos);
    }

    private void CargarComboUsuarios()
    {
        DataTable oDatos = TuCuento.Negocio.Usuario_NEG.ListarUsuarios();
        ddlUsuarios.DataSource = oDatos;
        ddlUsuarios.DataTextField = "sCod_Usuario";
        ddlUsuarios.DataValueField = "sCod_Usuario";
        ddlUsuarios.DataBind();

        ddlUsuarios.Items.Add(new ListItem("Elija", "-1"));
        ddlUsuarios.SelectedValue = "-1";

    }

    private void CargarComboPatentes()
    {
        DataTable oDatos = TuCuento.Negocio.Patente_NEG.ListarPatentes(-1, -1);
        ddlPatente.DataSource = oDatos;
        ddlPatente.DataTextField = "sDesc_Patente";
        ddlPatente.DataValueField = "nCod_Patente";
        ddlPatente.DataBind();

        ddlPatente.Items.Add(new ListItem("Elija", "-1"));
        ddlPatente.SelectedValue = "-1";
    }

    #endregion

    
}
