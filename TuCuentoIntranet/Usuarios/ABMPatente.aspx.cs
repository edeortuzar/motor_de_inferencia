using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;

public partial class Usuarios_ABMPatente : System.Web.UI.Page
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
        txtNombre.Focus();
    }

    protected void gvDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        InicializarControles();

        GridViewRow row = gvDatos.SelectedRow;
        HfAccion.Value = "EDIT";
        HfnCod_Patente.Value = Server.HtmlDecode(row.Cells[1].Text.Trim());
        this.txtNombre.Text = Server.HtmlDecode(row.Cells[2].Text.Trim());
        this.txtNodo.Text = Server.HtmlDecode(row.Cells[3].Text.Trim());
        this.txtNodoPadre.Text = Server.HtmlDecode(row.Cells[4].Text.Trim());
        this.txtURL.Text = Server.HtmlDecode(row.Cells[5].Text.Trim());
        this.chkHab.Checked = ((CheckBox)row.FindControl("chkHabGrilla")).Checked;
        
        HabDesCtrl(true);
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
            TuCuento.Entidades.Patente oPatente = new TuCuento.Entidades.Patente();

            if (HfAccion.Value == "EDIT")
                oPatente.nCod_Patente = Convert.ToInt32(HfnCod_Patente.Value);

            oPatente.sDesc_Patente = txtNombre.Text.Trim();
            oPatente.nHab = (chkHab.Checked ? 1 : 0);
            oPatente.nNodo = Convert.ToInt32(txtNodo.Text);
            oPatente.nNodo_Padre = Convert.ToInt32(txtNodoPadre.Text);
            oPatente.sUrl = txtURL.Text.Trim();

            if (TuCuento.Negocio.Patente_NEG.Persistir(oPatente))
            {
                InicializarPagina();
                LbMensaje.Text = "Se actualizaron los datos.";
            }
            else
            {
                Master.MensajeError = "Ocurrio un error al intentar grabar la patente, por favor, vuelva a intentarlo mas tarde. Disculpe las molestias.";
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
        DataTable oTabla = TuCuento.Negocio.Patente_NEG.ListarPatentes();

        gvDatos.DataSource = oTabla;
        gvDatos.DataBind();

        Funciones.LlenarGrillaVacia(gvDatos);
    }

    private void HabDesCtrl(Boolean bParam)
    {
        btnGrabar.Enabled = bParam;
        txtNombre.Enabled = bParam;
        txtNodo.Enabled = bParam;
        txtNodoPadre.Enabled = bParam;
        txtURL.Enabled = bParam;
        chkHab.Enabled = bParam;
    }

    private void InicializarControles()
    {
        Master.MensajeError = "";

        LbMensaje.Text = "";
        txtNombre.Text = "";
        txtNodo.Text = "";
        txtNodoPadre.Text = "";
        txtURL.Text = "";
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

        if (txtNodo.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo nodo.";
            bResp = false;
        }
        else
        {
            if (!Regex.IsMatch(txtNodo.Text, Funciones.sRegExpSoloNumeros))
            {
                sMensajeError = "Debe completar el campo nodo solo con números.";
                bResp = false;
            }
        }

        if (txtNodoPadre.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo nodo padre.";
            bResp = false;
        }
        else
        {
            if (!Regex.IsMatch(txtNodoPadre.Text, Funciones.sRegExpSoloNumeros))
            {
                sMensajeError = "Debe completar el campo nodo padre solo con números.";
                bResp = false;
            }
        }

        if (txtURL.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo URL.";
            bResp = false;
        }

        if (!bResp)
            Master.MensajeError = sMensajeError;

        return bResp;
    }

    #endregion


}
