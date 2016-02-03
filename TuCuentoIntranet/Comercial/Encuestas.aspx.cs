using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Comercial_Encuestas : System.Web.UI.Page
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

    private List<TuCuento.Entidades.EncuestaDetalle> lstDetalle
    {
        get { return (List<TuCuento.Entidades.EncuestaDetalle>)Session["EncuestaDetalle"]; }
        set
        {
            Session["EncuestaDetalle"] = value;
        }
    }

    #endregion

    #region Eventos
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarGrilla();
            HabControles(false);
        }

    }

    protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Oculto las columnas de códigos
        if (e.Row.Cells.Count > 1)
            e.Row.Cells[1].Visible = false;

    }

    protected void gvRespuestas_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Oculto las columnas de códigos
        if (e.Row.Cells.Count > 1)
            e.Row.Cells[1].Visible = false;

    }

    protected void gvRespuestas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int nId = e.RowIndex + 1;

        //Lo quito de la lista
        foreach (TuCuento.Entidades.EncuestaDetalle oDetalle in lstDetalle)
        {
            if (oDetalle.nIdRespuesta == nId)
            {
                lstDetalle.Remove(oDetalle);
                break;
            }
        }

        gvRespuestas.DataSource = lstDetalle;
        gvRespuestas.DataBind();

    }

    protected void btnNueva_Click(object sender, EventArgs e)
    {
        BlanquearControles();
        HabControles(true);
        txtTitulo.Focus();
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        Master.MensajeError = "";

        if (Validar())
        {
            TuCuento.Entidades.Encuesta oEncuesta = new TuCuento.Entidades.Encuesta();

            oEncuesta.sTitulo = txtTitulo.Text.Trim();
            oEncuesta.sPregunta = txtPregunta.Text.Trim();
            oEncuesta.dFechaFin = txtFechaFin.Value;
            oEncuesta.nHab = 1;

            //Cargo el detalle
            oEncuesta.lstDetalle = lstDetalle;

            //Cargo los usuarios seleccionados
            foreach (ListItem item in lvDerecha.Items)
            {
                TuCuento.Entidades.Usuario oUsuario = new TuCuento.Entidades.Usuario();
                oUsuario.sCod_Usuario = item.Value;

                oEncuesta.lstUsuario.Add(oUsuario);
            }

            if (TuCuento.Negocio.Encuesta_NEG.PersistirEncuesta(oEncuesta))
            {
                LbMensaje.Text = "Se actualizaron los datos";
                CargarGrilla();
                BlanquearControles();
                HabControles(false);
            }
            else
            {
                Master.MensajeError = "Ocurrio un error al grabar la información, por favor, intente más tarde.";
            }

        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        TuCuento.Entidades.EncuestaDetalle oDetalle = new TuCuento.Entidades.EncuestaDetalle();

        oDetalle.nIdRespuesta = lstDetalle.Count;
        oDetalle.nIdRespuesta++;

        oDetalle.sRespuesta = txtRespuesta.Text.Trim();

        lstDetalle.Add(oDetalle);

        gvRespuestas.DataSource = lstDetalle;
        gvRespuestas.DataBind();

        txtRespuesta.Text = "";
        txtRespuesta.Focus();

    }

    #region Listas

    protected void btnSel_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();
        foreach (ListItem item in lvIzquierda.Items)
        {
            if (item.Selected)
            {
                elementos.Add(item);
                lvDerecha.Items.Add(item);
            }
        }

        lvDerecha.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lvIzquierda.Items.Remove(item);
        }

    }

    protected void btnSelAll_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();

        foreach (ListItem item in lvIzquierda.Items)
        {
            elementos.Add(item);
            lvDerecha.Items.Add(item);
        }

        lvDerecha.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lvIzquierda.Items.Remove(item);
        }

    }

    protected void btnDesSel_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();
        foreach (ListItem item in lvDerecha.Items)
        {
            if (item.Selected)
            {
                elementos.Add(item);
                lvIzquierda.Items.Add(item);
            }
        }

        lvIzquierda.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lvDerecha.Items.Remove(item);
        }
    }

    protected void btnDesSelAll_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();

        foreach (ListItem item in lvDerecha.Items)
        {
            elementos.Add(item);
            lvIzquierda.Items.Add(item);
        }

        lvIzquierda.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lvDerecha.Items.Remove(item);
        }
    }

    #endregion

    #endregion

    #region Metodos

    private bool Validar()
    {
        bool bResp = true;

        if (txtTitulo.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo titulo.";
            bResp = false;
        }

        if (txtFechaFin.Value.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo fecha fin.";
            bResp = false;
        }

        if (txtPregunta.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo pregunta.";
            bResp = false;
        }

        if (gvRespuestas.Rows.Count == 0)
        {
            sMensajeError = "La encuesta debe tener al menos una respuesta.";
            bResp = false;
        }

        if (lvDerecha.Items.Count == 0)
        {
            sMensajeError = "Debe seleccionar al menos un usuario.";
            bResp = false;
        }

        if (!bResp)
            Master.MensajeError = sMensajeError;

        return bResp;
    }

    private void CargarGrilla()
    {
        gvDatos.DataSource = TuCuento.Negocio.Encuesta_NEG.ListarEncuestas();
        gvDatos.DataBind();

        Funciones.LlenarGrillaVacia(gvDatos);
    }

    private void HabControles(bool bHab)
    {
        txtPregunta.Enabled = bHab;
        txtRespuesta.Enabled = bHab;
        txtTitulo.Enabled = bHab;
        btnAgregar.Enabled = bHab;
        btnGrabar.Enabled = bHab;

        btnDesSel.Enabled = bHab;
        btnDesSelAll.Enabled = bHab;
        btnSel.Enabled = bHab;
        btnSelAll.Enabled = bHab;

    }

    private void BlanquearControles()
    {
        txtPregunta.Text = "";
        txtRespuesta.Text = "";
        txtTitulo.Text = "";

        lstDetalle = new List<TuCuento.Entidades.EncuestaDetalle>();

        gvRespuestas.DataSource = null;
        gvRespuestas.DataBind();

        Funciones.LlenarGrillaVacia(gvRespuestas);

        CargarUsuarios();
    }

    private void CargarUsuarios()
    {
        //Borro las dos listas
        lvIzquierda.Items.Clear();
        lvDerecha.Items.Clear();

        //Traigo los datos y lo cargo del lado izquierdo
        lvIzquierda.DataSource = TuCuento.Negocio.Usuario_NEG.TraerUsuariosHabilitados();
        lvIzquierda.DataTextField = "sCod_Usuario";
        lvIzquierda.DataValueField = "sCod_Usuario";
        lvIzquierda.DataBind();
    }

    #endregion
    
}
