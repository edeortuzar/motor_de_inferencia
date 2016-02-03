using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;

public partial class Usuarios_ABMFamilia : System.Web.UI.Page
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

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        HabDesCtrl(true);
        InicializarControles();
        CargarPatentes();
        txtNombre.Focus();
    }

    protected void gvDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        InicializarControles();

        GridViewRow row = gvDatos.SelectedRow;
        HfAccion.Value = "EDIT";
        HfnCod_Familia.Value = Server.HtmlDecode(row.Cells[1].Text.Trim());
        this.txtNombre.Text = Server.HtmlDecode(row.Cells[2].Text.Trim());
        this.chkHab.Checked = ((CheckBox)row.FindControl("chkHabGrilla")).Checked;

        HabDesCtrl(true);

        CargarPatentes();

        //Traigo las patentes asociadas
        DataTable oPatentes = TuCuento.Negocio.Familia_NEG.TraerFamiliaPatente(Convert.ToInt32(HfnCod_Familia.Value));

        //Muevo las patentes según corresponda
        List<ListItem> elementos = new List<ListItem>();
        foreach (ListItem item in lvIzquierda.Items)
        {
            
            for (int nI = 0; nI < (oPatentes.Rows.Count); nI++)
            {
                if (item.Value == oPatentes.Rows[nI]["nCod_Patente"].ToString())
                {
                    elementos.Add(item);
                    lvDerecha.Items.Add(item);
                    break;
                }
            }

        }

        lvDerecha.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lvIzquierda.Items.Remove(item);
        }

        txtNombre.Focus();
    }

    protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Oculto las columnas de códigos
        if (e.Row.Cells.Count > 1)
            e.Row.Cells[1].Visible = false;

    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        //Validar lo ingresado
        if (ValidarDatos())
        {
            //Cargo los datos en un objeto
            TuCuento.Entidades.Familia oFamilia = new TuCuento.Entidades.Familia();

            if (HfAccion.Value == "EDIT")
                oFamilia.nCod_Flia = Convert.ToInt32(HfnCod_Familia.Value);

            oFamilia.sDesc_Flia = txtNombre.Text.Trim();
            oFamilia.nHab = (chkHab.Checked ? 1 : 0);
            oFamilia.lstPatentes = new List<TuCuento.Entidades.Patente>();

            //Cargo las patentes seleccionadas
            foreach (ListItem item in lvDerecha.Items)
            {
                TuCuento.Entidades.Patente oPatente = new TuCuento.Entidades.Patente();
                oPatente.nCod_Patente = Convert.ToInt32(item.Value);

                oFamilia.lstPatentes.Add(oPatente);
            }
            
            if (TuCuento.Negocio.Familia_NEG.Persistir(oFamilia))
            {
                InicializarPagina();
                LbMensaje.Text = "Se actualizaron los datos.";
            }
            else
            {
                Master.MensajeError = "Ocurrio un error al intentar grabar la Familia, por favor, vuelva a intentarlo mas tarde. Disculpe las molestias.";
            }

        }
    }

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

    #region Metodos

    private void InicializarPagina()
    {
        CargarGrilla();
        HabDesCtrl(false);
        InicializarControles();
        CargarPatentes();
    }

    private void CargarGrilla()
    {
        DataTable oTabla = TuCuento.Negocio.Familia_NEG.ListarFamilias();

        gvDatos.DataSource = oTabla;
        gvDatos.DataBind();

        Funciones.LlenarGrillaVacia(gvDatos);
    }

    private void HabDesCtrl(Boolean bParam)
    {
        btnGrabar.Enabled = bParam;
        txtNombre.Enabled = bParam;
        chkHab.Enabled = bParam;

        lvIzquierda.Enabled = bParam;
        lvDerecha.Enabled = bParam;
        btnDesSel.Enabled = bParam;
        btnDesSelAll.Enabled = bParam;
        btnSel.Enabled = bParam;
        btnSelAll.Enabled = bParam;

    }

    private void InicializarControles()
    {
        Master.MensajeError = "";

        LbMensaje.Text = "";
        txtNombre.Text = "";
        chkHab.Checked = true;

        HfAccion.Value = "";

    }

    private bool ValidarDatos()
    {
        bool bResp = true;

        if (txtNombre.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo nombre.";
            bResp = false;
        }

        if (!bResp)
            Master.MensajeError = sMensajeError;

        return bResp;
    }

    private void CargarPatentes()
    {
        //Borro las dos listas
        lvIzquierda.Items.Clear();
        lvDerecha.Items.Clear();

        //Traigo los datos y lo cargo del lado izquierdo
        lvIzquierda.DataSource = TuCuento.Negocio.Patente_NEG.ListarPatentes(-1, 1);
        lvIzquierda.DataTextField = "sDesc_Patente";
        lvIzquierda.DataValueField = "nCod_Patente";
        lvIzquierda.DataBind();
    }

    #endregion
}
