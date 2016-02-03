using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class Cuentos_ABMAcciones : System.Web.UI.Page
{

    #region Propiedades
    
    private string sValorIngresado { get; set; }

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
            CargarComboDominio();
            CargarComboBooleano();
        }
    }

    protected void gvDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlHistoria.Items.Clear();
        InicializarControles();

        GridViewRow row = gvDatos.SelectedRow;
        HfAccion.Value = "EDIT";
        ddlDominioCond.SelectedValue = Server.HtmlDecode(row.Cells[11].Text.Trim());
        HfnCod_Accion.Value = Server.HtmlDecode(row.Cells[1].Text.Trim());
        this.txtNombre.Text = Server.HtmlDecode(row.Cells[2].Text.Trim());
        this.txtDescripcion.Text = Server.HtmlDecode(row.Cells[3].Text.Trim());
        this.chkHab.Checked = ((CheckBox)row.FindControl("chkHabGrilla")).Checked;

        CargarComboHistorias();

        if ((Funciones.TipoAccion)Convert.ToInt32(Server.HtmlDecode(row.Cells[6].Text.Trim())) == Funciones.TipoAccion.ModHecho)
        {
            rdTAHecho.Checked = true;
            MostrarPanel("Hecho");

            ddlEntidad.SelectedValue = Server.HtmlDecode(row.Cells[8].Text.Trim());
            CargarComboAtributos();
            ddlAtributo.SelectedValue = Server.HtmlDecode(row.Cells[7].Text.Trim());

            MostrarPanelValor(Server.HtmlDecode(row.Cells[9].Text.Trim()));

        }
        else
        {
            rdTAHistoria.Checked = true;
            MostrarPanel("Historia");

            ddlHistoria.SelectedValue = Server.HtmlDecode(row.Cells[10].Text.Trim());

        }

        HabDesCtrl(true);
        ddlDominioCond.Focus();

    }

    protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Oculto las columnas de códigos
        if (e.Row.Cells.Count > 1)
            e.Row.Cells[1].Visible = false;

        if (e.Row.Cells.Count > 5)
            e.Row.Cells[6].Visible = false;

        if (e.Row.Cells.Count > 6)
            e.Row.Cells[7].Visible = false;

        if (e.Row.Cells.Count > 7)
            e.Row.Cells[8].Visible = false;

        if (e.Row.Cells.Count > 8)
            e.Row.Cells[9].Visible = false;

        if (e.Row.Cells.Count > 9)
            e.Row.Cells[10].Visible = false;

        if (e.Row.Cells.Count > 10)
            e.Row.Cells[11].Visible = false;

    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        HabDesCtrl(true);
        InicializarControles();
        ddlDominioCond.Focus();
    }

    protected void rdTAHecho_CheckedChanged(object sender, EventArgs e)
    {
        MostrarPanel("Hecho");
    }

    protected void rdTAHistoria_CheckedChanged(object sender, EventArgs e)
    {
        MostrarPanel("Historia");
    }

    protected void ddlEntidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarComboAtributos();
    }

    protected void ddlAtributo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAtributo.SelectedValue == "-1")
        {
            Master.MensajeError = "Debe seleccionar un atributo.";
        }
        else
        {
            MostrarPanelValor(null);
        }
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        if (ValidarDatos())
        {
            //Cargo los datos en un objeto
            TuCuento.Entidades.Accion oAccion = new TuCuento.Entidades.Accion();

            oAccion.nCod_Dominio = Convert.ToInt32(ddlDominioCond.SelectedValue);

            if (HfAccion.Value == "EDIT")
                oAccion.nCod_Accion = Convert.ToInt32(HfnCod_Accion.Value);

            oAccion.sNombre = txtNombre.Text.Trim();
            oAccion.sDescripcion = txtDescripcion.Text.Trim();
            oAccion.nHab = (chkHab.Checked ? 1 : 0);
            if (rdTAHecho.Checked)
                oAccion.nCod_TipoAccion = Funciones.CodigoTipoAccion(Funciones.TipoAccion.ModHecho.ToString());
            else
                oAccion.nCod_TipoAccion = Funciones.CodigoTipoAccion(Funciones.TipoAccion.EjecutaHistoria.ToString());

            if (PanelHecho.Visible)
            {
                oAccion.Hecho = new TuCuento.Entidades.AccionHecho();
                oAccion.Hecho.nCod_Entidad = Convert.ToInt32(ddlEntidad.SelectedValue);
                oAccion.Hecho.nCod_Atributo = Convert.ToInt32(ddlAtributo.SelectedValue);
                oAccion.Hecho.sValor = sValorIngresado;
                oAccion.Historia = null;
            }
            else
            {
                oAccion.Historia = new TuCuento.Entidades.AccionHistoria();
                oAccion.Hecho = null;
                oAccion.Historia.nCod_Historia = Convert.ToInt32(ddlHistoria.SelectedValue);
            }
            
            if (TuCuento.Negocio.Accion_NEG.Persistir(oAccion))
            {
                InicializarPagina();
                LbMensaje.Text = "Se actualizaron los datos.";
            }
            else
            {
                Master.MensajeError = "Ocurrio un error al intentar grabar la acción, por favor, vuelva a intentarlo mas tarde. Disculpe las molestias.";
            }
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        int nCod_Dominio;

        LbMensaje.Text = "";

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

    protected void ddlDominioCond_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarComboHistorias();
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

        ddlDominioCond.DataSource = oTabla;
        ddlDominioCond.DataTextField = "sDesc_Dominio";
        ddlDominioCond.DataValueField = "nCod_Dominio";
        ddlDominioCond.DataBind();

        ddlDominioCond.Items.Add(new ListItem("Elija", "0"));
        ddlDominioCond.SelectedValue = "0";

    }

    private void MostrarPanel(string sCual)
    {
        if (sCual == "Hecho")
        {
            if (ddlHistoria.Items.Count > 0)
                ddlHistoria.SelectedValue = "0";

            rdTAHistoria.Checked = false;
            PanelHecho.Visible = true;
            PanelHistoria.Visible = false;
        }
        else
        {
            ddlEntidad.SelectedValue = "0";
            ddlAtributo.Items.Clear();
            OcultarPaneles();
            rdTAHecho.Checked = false;
            PanelHecho.Visible = false;
            PanelHistoria.Visible = true;
        }
    }

    private void CargarComboAtributos()
    {
        //Traigo los atributos habilitados de la entidad seleccionada
        DataTable oTabla = TuCuento.Negocio.Entidad_NEG.TraerAtributos(Convert.ToInt32(ddlEntidad.SelectedValue), -1, 1);

        ddlAtributo.DataSource = oTabla;
        ddlAtributo.DataTextField = "sNombre";
        ddlAtributo.DataValueField = "nCod_Atributo";
        ddlAtributo.DataBind();

        ddlAtributo.Items.Add(new ListItem("Elija", "-1"));

        ddlAtributo.SelectedValue = "-1";
    }

    private void InicializarPagina()
    {
        CargarGrilla(-1);
        CargarComboEntidades();
        HabDesCtrl(false);
        InicializarControles();
    }

    private void CargarGrilla(int nCod_Dominio)
    {
        DataTable oTabla = TuCuento.Negocio.Accion_NEG.ListarAcciones(-1,-1,nCod_Dominio);

        gvDatos.DataSource = oTabla;
        gvDatos.DataBind();

        Funciones.LlenarGrillaVacia(gvDatos);
    }

    private void HabDesCtrl(Boolean bParam)
    {
        ddlDominioCond.Enabled = bParam;
        btnGrabar.Enabled = bParam;
        txtNombre.Enabled = bParam;
        txtDescripcion.Enabled = bParam;
        chkHab.Enabled = bParam;

        ddlEntidad.Enabled = bParam;
        ddlAtributo.Enabled = bParam;

        rdTAHecho.Enabled = bParam;
        rdTAHistoria.Enabled = bParam;
    }

    private void OcultarPaneles()
    {
        //Oculo los paneles
        PanelFecha.Visible = false;
        PanelNumero.Visible = false;
        PanelTexto.Visible = false;
        PanelBooleano.Visible = false;
    }

    private void InicializarControles()
    {
        Master.MensajeError = "";
        
        if (ddlDominioCond.Items.Count > 0)
            ddlDominioCond.SelectedValue = "0";

        LbMensaje.Text = "";
        txtNombre.Text = "";
        txtDescripcion.Text = "";
        chkHab.Checked = true;

        HfAccion.Value = "";
        HfnCod_Accion.Value = "";

        ddlEntidad.SelectedValue = "0";
        ddlAtributo.Items.Clear();
        if (ddlHistoria.Items.Count > 0)
            ddlHistoria.SelectedValue = "0";

        PanelHecho.Visible = false;
        PanelHistoria.Visible = false;

        rdTAHecho.Checked = false;
        rdTAHistoria.Checked = false;
        
        OcultarPaneles();

    }

    private void CargarComboEntidades()
    {
        DataTable oTabla = TuCuento.Negocio.Entidad_NEG.ListarEntidades(-1, 1);

        ddlEntidad.DataSource = oTabla;
        ddlEntidad.DataTextField = "sNombre";
        ddlEntidad.DataValueField = "nCod_Entidad";
        ddlEntidad.DataBind();

        ddlEntidad.Items.Add(new ListItem("Elija", "0"));

        ddlEntidad.SelectedValue = "0";

    }

    private void CargarComboBooleano()
    {
        ddlBooleano.Items.Add(new ListItem("Verdadero", "VERDADERO"));
        ddlBooleano.Items.Add(new ListItem("Falso", "FALSO"));
        ddlBooleano.SelectedIndex = 0;
    }

    private void CargarComboHistorias()
    {
        ddlHistoria.Items.Clear();

        DataTable oTabla = TuCuento.Negocio.Historia_NEG.ListarHistorias(-1, 1,Convert.ToInt32(ddlDominioCond.SelectedValue));

        if (oTabla.Rows.Count > 0)
        {
            ddlHistoria.DataSource = oTabla;
            ddlHistoria.DataTextField = "sNombre";
            ddlHistoria.DataValueField = "nCod_Historia";
            ddlHistoria.DataBind();
        }

        ddlHistoria.Items.Add(new ListItem("Elija", "0"));


        ddlHistoria.SelectedValue = "0";
    }

    private bool ValidarValPosible()
    {
        bool bResp = true;

        //Valido los datos ingresados
        if (PanelFecha.Visible)
        {
            if (txtDia.Text.Trim().Length == 0 || txtMes.Text.Trim().Length == 0 || txtAnio.Text.Trim().Length < 4)
            {
                sMensajeError = "Debe completar el campo fecha con el formato dd/mm/aaaa.";
                bResp = false;
            }

            if (bResp)
            {
                try
                {
                    DateTime date = new DateTime(Convert.ToInt32(txtAnio.Text), Convert.ToInt32(txtMes.Text), Convert.ToInt32(txtDia.Text));
                }
                catch (Exception ex)
                {
                    sMensajeError = "Ingreso una fecha invalida, por favor, verifique la información ingresada.";
                    bResp = false;
                }
            }

            if (bResp)
            {
                sValorIngresado = txtDia.Text + "/" + txtMes.Text + "/" + txtAnio.Text;
            }

        }

        if (PanelNumero.Visible)
        {
            Regex isnumber = new Regex(Funciones.sRegExpSoloNumeros);
            if (!isnumber.IsMatch(txtValNumero.Text))
            {
                sMensajeError = "Debe ingresar valores numéricos validos. Solo se aceptan enteros.";
                bResp = false;
            }

            if (bResp)
            {
                sValorIngresado = txtValNumero.Text.Trim();
            }

        }

        if (PanelTexto.Visible)
        {
            if (txtValTexto.Text.Trim().Length == 0)
            {
                sMensajeError = "Debe ingresar un texto.";
                bResp = false;
            }

            if (bResp)
            {
                sValorIngresado = txtValTexto.Text.Trim();
            }
        }

        if (PanelBooleano.Visible)
        {
            sValorIngresado = ddlBooleano.SelectedValue;
        }

        return bResp;

    }

    private bool ValidarDatos()
    {
        bool bResp = true;

        if (ddlDominioCond.SelectedValue == "0")
        {
            sMensajeError = "Debe seleccionar un dominio asociado.";
            bResp = false;
        }

        if (txtNombre.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo nombre.";
            bResp = false;
        }

        if (txtDescripcion.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo descripción.";
            bResp = false;
        }

        if (PanelHistoria.Visible)
        {
            if (ddlHistoria.SelectedValue == "0")
            {
                sMensajeError = "Debe seleccionar una historia si selecciona la opción 'Ejecuta una historia'.";
                bResp = false;
            }
        }

        if (PanelHecho.Visible)
        {
            if (ddlEntidad.SelectedValue == "0")
            {
                sMensajeError = "Debe seleccionar una entidad si selecciona la opción 'Modifica un hecho'.";
                bResp = false;
            }
            else
            {
                bResp = ValidarValPosible();
            }

            if (ddlAtributo.SelectedValue == "-1")
            {
                sMensajeError = "Debe seleccionar un atributo.";
                bResp = false;
            }
            

        }

        //Verifico que no este repetido el nombre
        DataTable oTabla = TuCuento.Negocio.Accion_NEG.ValAccion(Convert.ToInt32((HfnCod_Accion.Value == "" ? "0" : HfnCod_Accion.Value)), txtNombre.Text.Trim(), Convert.ToInt32(ddlDominioCond.SelectedValue));
        if (oTabla.Rows.Count > 0)
        {
            sMensajeError = "El nombre de la acción que especifico ya se encuentra cargado.";
            bResp = false;
        }

        
        if (!bResp)
            Master.MensajeError = sMensajeError;

        return bResp;
    }

    private void MostrarPanelValor(string sValor)
    {
        //Oculto todos los paneles
        OcultarPaneles();

        BlanquearControlesValPosible();

        //Segun el tipo de dato del atributo muestro el panel correspondiente
        DataTable oTabla = TuCuento.Negocio.Entidad_NEG.TraerAtributos(Convert.ToInt32(ddlEntidad.SelectedValue), Convert.ToInt32(ddlAtributo.SelectedValue), -1);
        Funciones.TipoDato nCod_TipoAtributo = (Funciones.TipoDato)Convert.ToInt32(oTabla.Rows[0]["nCod_TipoAtributo"]);

        if (nCod_TipoAtributo == Funciones.TipoDato.Entidad)
        {
            //TODO: Falta el panel de entidad y ver que se puede hacer en la condicion de ese tipo
        }

        if (nCod_TipoAtributo == Funciones.TipoDato.Texto)
        {
            PanelTexto.Visible = true;
            if (sValor != null)
            {
                txtValTexto.Text = sValor;
            }
        }

        if (nCod_TipoAtributo == Funciones.TipoDato.Número)
        {
            PanelNumero.Visible = true;
            if (sValor != null)
            {
                txtValNumero.Text = sValor;
            }
        }

        if (nCod_TipoAtributo == Funciones.TipoDato.Fecha)
        {
            PanelFecha.Visible = true;
            if (sValor != null)
            {
                string[] sFecha = sValor.Split(Convert.ToChar("/"));
                txtDia.Text = sFecha[0];
                txtMes.Text = sFecha[1];
                txtAnio.Text = sFecha[2];
            }
        }

        if (nCod_TipoAtributo == Funciones.TipoDato.Booleano)
        {
            PanelBooleano.Visible = true;
            ddlBooleano.SelectedValue = sValor;
        }
    }

    private void BlanquearControlesValPosible()
    {
        //Campos de Fecha
        txtDia.Text = "";
        txtMes.Text = "";
        txtAnio.Text = "";

        //Campo de numero
        txtValNumero.Text = "";

        //Campo de texto
        txtValTexto.Text = "";

        //Booleano
        ddlBooleano.SelectedIndex = 0;

    }

    #endregion





    
}
