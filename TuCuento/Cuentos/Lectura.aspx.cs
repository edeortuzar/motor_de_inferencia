using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Cuentos_Lectura : System.Web.UI.Page
{
    #region Eventos
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarGrilla();
            MostrarPanel(1);
        }
    }


    protected void gvDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        int nCod_Cuento;
        GridViewRow row = gvDatos.SelectedRow;
        nCod_Cuento = Convert.ToInt32(Server.HtmlDecode(row.Cells[1].Text.Trim()));
        
        //Busco el cuento y lo muestro en el textbox
        DataTable oTabla = TuCuento.Negocio.Cuento_NEG.ListarCuentos(nCod_Cuento);

        txtTexto.Text = oTabla.Rows[0]["sTexto"].ToString();
        txtTexto.ReadOnly = true;
        MostrarPanel(2);

    }

    protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Oculto las columnas de códigos
        if (e.Row.Cells.Count > 1)
            e.Row.Cells[1].Visible = false;

    }

    protected void btnAtras_Click(object sender, EventArgs e)
    {
        MostrarPanel(1);
    }

    #endregion

    #region Metodos

    private void CargarGrilla()
    {
        string sCod_Usuario = Master.sCod_Usuario;
        DataTable oTabla = TuCuento.Negocio.Cuento_NEG.ListarCuentos(-1, sCod_Usuario);

        gvDatos.DataSource = oTabla;
        gvDatos.DataBind();

    }

    private void MostrarPanel(int nPaso)
    {
        switch (nPaso)
        {
            case 1:
                PanelGrilla.Visible = true;
                PanelTexto.Visible = false;
                txtTexto.ReadOnly = false;
                txtTexto.Text = "";
                break;
            case 2:
                PanelGrilla.Visible = false;
                PanelTexto.Visible = true;
                break;
        }
    }

    #endregion
    
}
