using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Cuentos_ABMHistorias : System.Web.UI.Page
{
    #region Eventos
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarGrilla(-1);
            CargarComboDominio();
            if (Request.QueryString["nResultado"] != null)
            {
                if (Request.QueryString["nResultado"].ToString() == "1")
                {
                    LbMensaje.Text = "Se actualizaron los datos.";
                }
            }
        }
    }
    
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        LbMensaje.Text = "";
        Session["nCod_Historia"] = null;
        Response.Redirect("HistoriaDetalle.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        LbMensaje.Text = "";
        int nCod_Dominio;

        if (ddlDominio.SelectedValue == "0")
        {
            nCod_Dominio = -1;
        }
        else
        {
            nCod_Dominio = Convert.ToInt32(ddlDominio.SelectedValue);
        }

        CargarGrilla(nCod_Dominio);
    }

    protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Oculto las columnas de códigos
        if (e.Row.Cells.Count > 3)
            e.Row.Cells[4].Visible = false;
    }

    #endregion

    #region Metodos

    private void CargarComboDominio()
    {
        //Traigo los dominios habilitados
        DataTable oTabla = TuCuento.Negocio.Dominio_NEG.ListarDominios(-1, 1);

        ddlDominio.DataSource = oTabla;
        ddlDominio.DataTextField = "sDesc_Dominio";
        ddlDominio.DataValueField = "nCod_Dominio";
        ddlDominio.DataBind();

        ddlDominio.Items.Add(new ListItem("Elija", "0"));
        ddlDominio.SelectedValue = "0";

    }

    private void CargarGrilla(int nCod_Dominio)
    {
        DataTable oTabla = TuCuento.Negocio.Historia_NEG.ListarHistorias(-1,-1,nCod_Dominio);

        gvDatos.DataSource = oTabla;
        gvDatos.DataBind();

        Funciones.LlenarGrillaVacia(gvDatos);
    }

    #endregion
}
