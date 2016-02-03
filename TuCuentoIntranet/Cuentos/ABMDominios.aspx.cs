using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class Cuentos_ABMDominios : System.Web.UI.Page
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
            InicializarPagina();
        }
    }

    protected void gvDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        InicializarControles();

        GridViewRow row = gvDatos.SelectedRow;
        HfAccion.Value = "EDIT";
        HfnCod_Dominio.Value = Server.HtmlDecode(row.Cells[1].Text.Trim());
        this.txtDescripcion.Text = Server.HtmlDecode(row.Cells[2].Text.Trim());
        this.chkHab.Checked = ((CheckBox)row.FindControl("chkHabGrilla")).Checked;

        HabDesCtrl(true);
        txtDescripcion.Focus();

    }

    protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Oculto las columnas de códigos
        if (e.Row.Cells.Count > 1)
            e.Row.Cells[1].Visible = false;

    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        HabDesCtrl(true);
        InicializarControles();
        txtDescripcion.Focus();
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        if (ValidarDatos())
        {
            //Cargo los datos en un objeto
            TuCuento.Entidades.Dominio oDominio = new TuCuento.Entidades.Dominio();

            if (HfAccion.Value == "EDIT")
                oDominio.nCod_Dominio = Convert.ToInt32(HfnCod_Dominio.Value);

            oDominio.sDescripcion = txtDescripcion.Text.Trim();
            oDominio.nHab = (chkHab.Checked ? 1 : 0);
            
            if (TuCuento.Negocio.Dominio_NEG.Persistir(oDominio))
            {
                InicializarPagina();
                LbMensaje.Text = "Se actualizaron los datos.";
            }
            else
            {
                Master.MensajeError = "Ocurrio un error al generar el dominio. Por favor, intente mas tarde.";
            }
        }
    }

    #endregion

    #region Metodos

    private void InicializarPagina()
    {
        CargarGrilla();
        HabDesCtrl(false);
        InicializarControles();
    }

    private void CargarGrilla()
    {
        DataTable oTabla = TuCuento.Negocio.Dominio_NEG.ListarDominios();
        gvDatos.DataSource = oTabla;
        gvDatos.DataBind();

        Funciones.LlenarGrillaVacia(gvDatos);
    }

    private void HabDesCtrl(Boolean bParam)
    {
        btnGrabar.Enabled = bParam;
        txtDescripcion.Enabled = bParam;
        chkHab.Enabled = bParam;
    }

    private void InicializarControles()
    {
        Master.MensajeError = "";
        
        LbMensaje.Text = "";
        txtDescripcion.Text = "";
        chkHab.Checked = true;

        HfAccion.Value = "";
        HfnCod_Dominio.Value = "";

    }

    private bool ValidarDatos()
    {
        bool bResp = true;

        if (txtDescripcion.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo descripción.";
            bResp = false;
        }

        //Verifico que no este repetida la descripcion
        DataTable oTabla = TuCuento.Negocio.Dominio_NEG.ValDominio(Convert.ToInt32((HfnCod_Dominio.Value == "" ? "0" : HfnCod_Dominio.Value) ), txtDescripcion.Text.Trim());
        if (oTabla.Rows.Count > 0)
        {
            sMensajeError = "El nombre de dominio especificado esta repetido.";
            bResp = false;
        }

        if (!bResp)
            Master.MensajeError = sMensajeError;

        return bResp;
    }

    #endregion





    
}
