using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class Cuentos_ABMCondiciones : System.Web.UI.Page
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

    private List<TuCuento.Entidades.CondicionValPosible> lstValPosibles
    {
        get { return (List<TuCuento.Entidades.CondicionValPosible>)Session["ValPosibles"]; }
        set
        {
            Session["ValPosibles"] = value;
        }
    }

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InicializarPagina();
            CargarComboBooleano();
            CargarComboDominio();
        }
    }

    protected void btnSeguir_Click(object sender, EventArgs e)
    {
        if (ddlEntidad.SelectedValue == "0")
        {
            Master.MensajeError = "Debe seleccionar una entidad y un atributo.";
        }
        else
        {
            PanelDetalle.Visible = true;

            //Oculto todos los paneles
            OcultarPaneles();

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
            }

            if (nCod_TipoAtributo == Funciones.TipoDato.Número)
            {
                PanelNumero.Visible = true;
            }

            if (nCod_TipoAtributo == Funciones.TipoDato.Fecha)
            {
                PanelFecha.Visible = true;
            }

            if (nCod_TipoAtributo == Funciones.TipoDato.Booleano)
            {
                PanelBooleano.Visible = true;
            }

            ddlEntidad.Enabled = false;
            ddlAtributo.Enabled = false;

            CargarComboOperadores(nCod_TipoAtributo);

            BlanquearControlesValPosible();
        }
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        HabDesCtrl(true);
        InicializarControles();
        ddlDominioCond.Focus();
    }

    protected void ddlEntidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarComboAtributos();
    }

    protected void gvDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        InicializarControles();

        GridViewRow row = gvDatos.SelectedRow;
        HfAccion.Value = "EDIT";
        ddlDominioCond.SelectedValue = Server.HtmlDecode(row.Cells[8].Text.Trim());
        HfnCod_Condicion.Value = Server.HtmlDecode(row.Cells[1].Text.Trim());
        this.txtNombre.Text = Server.HtmlDecode(row.Cells[2].Text.Trim());
        this.txtDescripcion.Text = Server.HtmlDecode(row.Cells[3].Text.Trim());
        this.chkHab.Checked = ((CheckBox)row.FindControl("chkHabGrilla")).Checked;
        ddlEntidad.SelectedValue = Server.HtmlDecode(row.Cells[6].Text.Trim());
        
        CargarComboAtributos();
        Session["cuentoAEditar"] = "SSS";
        Server.Transfer("");
        
        //-------------------------------
        //Pagina destino

        GridViewRow row2 = (GridViewRow)Session["cuentoAEditar"];

        ddlAtributo.SelectedValue = Server.HtmlDecode(row.Cells[7].Text.Trim());

        //Paso los valores posibles a la lista
        DataTable oTabla = TuCuento.Negocio.Condicion_NEG.TraerValoresPosibles(Convert.ToInt32(HfnCod_Condicion.Value));

        foreach (DataRow fila in oTabla.Rows)
        {
            TuCuento.Entidades.CondicionValPosible oValPosible = new TuCuento.Entidades.CondicionValPosible();
            oValPosible.nCod_ValPosible = Convert.ToInt32(fila["nCod_ValPosible"]);
            oValPosible.sOperadorLogico  = fila["sOperadorLogico"].ToString();
            oValPosible.sValor  = fila["sValor"].ToString();
            lstValPosibles.Add(oValPosible);
        }

        gvValPosibles.DataSource = lstValPosibles;
        gvValPosibles.DataBind();

        HabDesCtrl(true);
        txtNombre.Focus();

        //Inhabilito los combos de entidad y atributo
        ddlEntidad.Enabled = false;
        ddlAtributo.Enabled = false;
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
    }

    protected void gvValPosibles_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Oculto la columna del código
        if (e.Row.Cells.Count > 2)
            e.Row.Cells[2].Visible = false;

    }

    protected void gvValPosibles_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gvValPosibles.SelectedRow;
        HfAccionValPosible.Value = "EDIT";
        HfnCod_ValPosible.Value = Server.HtmlDecode(row.Cells[2].Text.Trim());

        ddlOperador.SelectedValue = Server.HtmlDecode(row.Cells[3].Text.Trim());

        if (PanelBooleano.Visible)
        {
            ddlBooleano.SelectedValue = Server.HtmlDecode(row.Cells[4].Text.Trim());
        }

        if (PanelTexto.Visible)
        {
            txtValTexto.Text = Server.HtmlDecode(row.Cells[4].Text.Trim());
        }

        if (PanelNumero.Visible)
        {
            txtValNumero.Text = Server.HtmlDecode(row.Cells[4].Text.Trim());
        }

        if (PanelFecha.Visible)
        {
            string[] sDato = Server.HtmlDecode(row.Cells[4].Text.Trim()).Split(Convert.ToChar("/"));
            txtDia.Text = sDato[0];
            txtMes.Text = sDato[1];
            txtAnio.Text = sDato[0];
        }

    }

    protected void gvValPosibles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int nId = e.RowIndex+1;

        //Lo quito de la lista
        foreach (TuCuento.Entidades.CondicionValPosible oValPosible in lstValPosibles)
        {
            if (oValPosible.nCod_ValPosible == nId)
            {
                lstValPosibles.Remove(oValPosible);
                break;
            }
        }

        gvValPosibles.DataSource = lstValPosibles;
        gvValPosibles.DataBind();

    }
  
    protected void btnAgregar_Click(object sender, EventArgs e)
    {

        if (ValidarValPosible())
        {
            if (HfAccionValPosible.Value == "EDIT")
            {
                //Lo busco en la lista y lo modifico
                foreach (TuCuento.Entidades.CondicionValPosible oValPosible in lstValPosibles)
                {
                    if (oValPosible.nCod_ValPosible == Convert.ToInt32(HfnCod_ValPosible.Value))
                    {
                        oValPosible.sOperadorLogico = ddlOperador.SelectedValue;

                        oValPosible.sValor = this.sValorIngresado;

                        break;
                    }
                }
            }
            else
            {
                TuCuento.Entidades.CondicionValPosible oValPosible = new TuCuento.Entidades.CondicionValPosible();

                oValPosible.nCod_ValPosible = lstValPosibles.Count;
                oValPosible.nCod_ValPosible++;

                oValPosible.sOperadorLogico = ddlOperador.SelectedValue;

                oValPosible.sValor = this.sValorIngresado;

                lstValPosibles.Add(oValPosible);
            }

            gvValPosibles.DataSource = lstValPosibles;
            gvValPosibles.DataBind();

            BlanquearControlesValPosible();
            
        }

    }

    protected void btnNuevoValor_Click(object sender, EventArgs e)
    {
        BlanquearControlesValPosible();
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        //Validar lo ingresado
        if (ValidarDatos())
        {
            //Cargo los datos en un objeto
            TuCuento.Entidades.Condicion oCondicion = new TuCuento.Entidades.Condicion();

            oCondicion.nCod_Dominio = Convert.ToInt32(ddlDominioCond.SelectedValue);

            if (HfAccion.Value == "EDIT")
                oCondicion.nCod_Condicion = Convert.ToInt32(HfnCod_Condicion.Value);

            oCondicion.sNombre = txtNombre.Text.Trim();
            oCondicion.sDescripcion = txtDescripcion.Text.Trim();
            oCondicion.nHab = (chkHab.Checked ? 1 : 0);
            oCondicion.lstValPosible = lstValPosibles;
            oCondicion.nCod_Entidad = Convert.ToInt32(ddlEntidad.SelectedValue);
            oCondicion.nCod_Atributo = Convert.ToInt32(ddlAtributo.SelectedValue);

            if (TuCuento.Negocio.Condicion_NEG.Persistir(oCondicion))
            {               
                InicializarPagina();
                LbMensaje.Text = "Se actualizaron los datos.";
            }
            else
            {
                Master.MensajeError = "Ocurrio un error al intentar grabar la condición, por favor, vuelva a intentarlo mas tarde. Disculpe las molestias.";
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

        ddlDominio.Items.Add(new ListItem("Elija","0"));
        ddlDominio.SelectedValue = "0";

        ddlDominioCond.DataSource = oTabla;
        ddlDominioCond.DataTextField = "sDesc_Dominio";
        ddlDominioCond.DataValueField = "nCod_Dominio";
        ddlDominioCond.DataBind();

        ddlDominioCond.Items.Add(new ListItem("Elija", "0"));
        ddlDominioCond.SelectedValue = "0";

    }

    private void CargarComboAtributos()
    {
        //Traigo los atributos habilitados de la entidad seleccionada
        DataTable oTabla = TuCuento.Negocio.Entidad_NEG.TraerAtributos(Convert.ToInt32(ddlEntidad.SelectedValue), -1, 1);

        ddlAtributo.DataSource = oTabla;
        ddlAtributo.DataTextField = "sNombre";
        ddlAtributo.DataValueField = "nCod_Atributo";
        ddlAtributo.DataBind();

        ddlAtributo.SelectedIndex = 0;
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
        DataTable oTabla = TuCuento.Negocio.Condicion_NEG.ListarCondiciones(-1,-1,nCod_Dominio);

        gvDatos.DataSource = oTabla;
        gvDatos.DataBind();

        Funciones.LlenarGrillaVacia(gvDatos);
    }

    private void HabDesCtrl(Boolean bParam)
    {
        btnSeguir.Enabled = bParam;
        btnGrabar.Enabled = bParam;
        ddlDominioCond.Enabled = bParam;
        txtNombre.Enabled = bParam;
        txtDescripcion.Enabled = bParam;
        chkHab.Enabled = bParam;

        ddlEntidad.Enabled = bParam;
        ddlAtributo.Enabled = bParam;

        btnAgregar.Enabled = bParam;
        btnNuevoValor.Enabled = bParam;

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

        gvValPosibles.DataSource = null;
        gvValPosibles.DataBind();
        Funciones.LlenarGrillaVacia(gvValPosibles);

        //Inicializo la lista de atributos en blanco
        lstValPosibles = new List<TuCuento.Entidades.CondicionValPosible>();

        HfAccion.Value = "";

        ddlEntidad.SelectedValue = "0";
        ddlAtributo.Items.Clear();

        PanelDetalle.Visible = false;
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

    private void CargarComboOperadores(Funciones.TipoDato nCod_TipoAtributo)
    {
        ddlOperador.Items.Clear();

        if (nCod_TipoAtributo == Funciones.TipoDato.Fecha || nCod_TipoAtributo == Funciones.TipoDato.Número)
        {
            ddlOperador.Items.Add(new ListItem("==", "=="));
            ddlOperador.Items.Add(new ListItem("!=", "!="));
            ddlOperador.Items.Add(new ListItem(">=", ">="));
            ddlOperador.Items.Add(new ListItem("<=", "<="));
            ddlOperador.Items.Add(new ListItem("<", "<"));
            ddlOperador.Items.Add(new ListItem(">", ">"));
        }

        if (nCod_TipoAtributo == Funciones.TipoDato.Texto)
        {
            ddlOperador.Items.Add(new ListItem("==", "=="));
            ddlOperador.Items.Add(new ListItem("!=", "!="));
        }

        if (nCod_TipoAtributo == Funciones.TipoDato.Booleano)
        {
            ddlOperador.Items.Add(new ListItem("==", "=="));
        }

        if (nCod_TipoAtributo == Funciones.TipoDato.Entidad)
        {
            //TODO: A definir
        }

        ddlOperador.SelectedIndex = 0;

    }

    private void CargarComboBooleano()
    {
        ddlBooleano.Items.Add(new ListItem("Verdadero", "VERDADERO"));
        ddlBooleano.Items.Add(new ListItem("Falso", "FALSO"));
        ddlBooleano.SelectedIndex = 0;
    }

    private bool ValidarValPosible()
    {
        bool bResp = true;

        //Valido los datos ingresados
        if (PanelFecha.Visible)
        {
            if (txtDia.Text.Trim().Length == 0 || txtMes.Text.Trim().Length == 0 || txtAnio.Text.Trim().Length < 4)
            {
                Master.MensajeError = "Debe completar el campo fecha con el formato dd/mm/aaaa.";
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
                    Master.MensajeError = "Ingreso una fecha invalida, por favor, verifique la información ingresada.";
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
                Master.MensajeError = "Debe ingresar valores numéricos validos. Solo se aceptan enteros.";
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
                Master.MensajeError = "Debe ingresar un texto.";
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

        if (bResp)
        {
            //Valido que no este repetido
            foreach (TuCuento.Entidades.CondicionValPosible oValPosible in lstValPosibles)
            {
                if (ddlOperador.SelectedValue == oValPosible.sOperadorLogico && oValPosible.sValor == sValorIngresado)
                {
                    if (HfAccionValPosible.Value == "EDIT")
                    {
                        //Verifico si es distinto código
                        if (oValPosible.nCod_ValPosible != Convert.ToInt32(HfnCod_ValPosible.Value))
                        {
                            sMensajeError = "La combinación de operador lógico y valor posible ya se encuentra cargada.";
                            bResp = false;
                            break;
                        }
                    }
                    else
                    {
                        sMensajeError = "La combinación de operador lógico y valor posible ya se encuentra cargada.";
                        bResp = false;
                        break;
                    }
                }
            }
        }

        return bResp;

    }

    private void BlanquearControlesValPosible()
    {
        HfAccionValPosible.Value = "";
        HfnCod_ValPosible.Value = "";

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

        ddlOperador.Focus();
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

        if (ddlEntidad.SelectedValue == "0")
        {
            sMensajeError = "Debe seleccionar una entidad.";
            bResp = false;
        }

        if (lstValPosibles.Count == 0)
        {
            sMensajeError = "La condición debe tener al menos un valor posible.";
            bResp = false;
        }

        //Verifico que no este repetido el nombre
        DataTable oTabla = TuCuento.Negocio.Condicion_NEG.ValCondicion(Convert.ToInt32((HfnCod_Condicion.Value == "" ? "0" : HfnCod_Condicion.Value)), txtNombre.Text.Trim(), Convert.ToInt32(ddlDominioCond.SelectedValue));
        if (oTabla.Rows.Count > 0)
        {
            sMensajeError = "El nombre de la condicion que especifico ya se encuentra cargado.";
            bResp = false;
        }

        if (!bResp)
            Master.MensajeError = sMensajeError;

        return bResp;
    }

    #endregion

    
}
