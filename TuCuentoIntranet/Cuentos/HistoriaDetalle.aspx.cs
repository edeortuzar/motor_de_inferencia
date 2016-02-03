using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Cuentos_HistoriaDetalle : System.Web.UI.Page
{
    #region Propiedades

    private string _sMensajeError;

    private List<TuCuento.Entidades.HistoriaDetalle> lstDetalleHistoria
    {
        get { return (List<TuCuento.Entidades.HistoriaDetalle>)Session["lstDetalleHistoria"]; }
        set
        {
            Session["lstDetalleHistoria"] = value;
        }
    }

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

    private List<TuCuento.Entidades.Condicion> lstCondiciones
    {
        get { return (List<TuCuento.Entidades.Condicion>)Session["lstCondiciones"]; }
        set { Session["lstCondiciones"] = value; }
    }

    private List<TuCuento.Entidades.Accion> lstAcciones
    {
        get { return (List<TuCuento.Entidades.Accion>)Session["lstAcciones"]; }
        set { Session["lstAcciones"] = value; }
    }

    #endregion

    #region Eventos
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarComboDominio();
            InicializarPagina();
            if (HfnCod_Historia.Value != "-1")
            {
                CargarDatosHistoria(Convert.ToInt32(HfnCod_Historia.Value));
                CargarListaDetalle(Convert.ToInt32(HfnCod_Historia.Value));
            }
            CargarGrillaDetalle();
            ddlDominio.Focus();
        }
    }

    protected void gvDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gvDatos.SelectedRow;
        int nOrden = Convert.ToInt32(Server.HtmlDecode(row.Cells[1].Text))-1; //Resto un porque el indice de la lista comineza en 0
        nOrden = row.RowIndex;
        HfnOrden.Value = nOrden.ToString();

        //Según el tipo de detalle muestro lo que corresponda
        if (Server.HtmlDecode(row.Cells[2].Text.Trim()) == Funciones.TipoHistoriaDetalle.Texto.ToString())
        {
            txtTexto.Text = lstDetalleHistoria[nOrden].Texto.sTexto;
            MostrarPanel(3);
        }
        else
        {
            CargarDetalleInferencia(lstDetalleHistoria[nOrden].Inferencia);
            MostrarPanel(5);
        }
    }

    protected void gvCondiciones_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Oculto las columnas de códigos
        if (e.Row.Cells.Count > 0)
            e.Row.Cells[0].Visible = false;

    }
    
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Button b = (Button)sender;

        switch (b.ID.Substring(b.ID.Length - 1, 1))
        {
            case "1":
            case "2":
                Response.Redirect("ABMHistorias.aspx", false);
                break;
            case "3":
                MostrarPanel(2);
                break;
            case "5":
            case "6":
            case "7":
                MostrarPanel(2);
                break;
        }
        
    }

    protected void btnSiguiente_Click(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        if (ValidarPanel(Convert.ToInt32(b.ID.Substring(b.ID.Length - 1, 1)),true))
            MostrarPanel(Convert.ToInt32(b.ID.Substring(b.ID.Length - 1, 1))+1);
    }

    protected void btnAnterior_Click(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        if (ValidarPanel(Convert.ToInt32(b.ID.Substring(b.ID.Length - 1, 1)),false))
            MostrarPanel(Convert.ToInt32(b.ID.Substring(b.ID.Length - 1, 1))-1);
    }

    protected void btnAgregarTexto_Click(object sender, EventArgs e)
    {
        MostrarPanel(3);
    }

    protected void btnAgregarInferencia_Click(object sender, EventArgs e)
    {
        MostrarPanel(5);
        CargarDetalleInferencia(null);
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        if (ValidarDatos())
        {
            TuCuento.Entidades.Historia oHistoria = new TuCuento.Entidades.Historia();

            if (HfAccion.Value == "EDIT")
                oHistoria.nCod_Historia = Convert.ToInt32(HfnCod_Historia.Value);

            oHistoria.nCod_Dominio = Convert.ToInt32(ddlDominio.SelectedValue);
            oHistoria.sDescripcion = txtDescripcion.Text.Trim();
            oHistoria.sNombre = txtNombre.Text.Trim();
            oHistoria.nHab = (chkHab.Checked ? 1 : 0);

            oHistoria.Detalle = lstDetalleHistoria;

            if (TuCuento.Negocio.Historia_NEG.Persistir(oHistoria))
            {
                Response.Redirect("ABMHistorias.aspx?nResultado=1", false);
            }
            else
            {
                Master.MensajeError = "Ocurrio un error al generar el cuento. Por favor, intente mas tarde.";
            }
        }
    }

    protected void btnSelCond_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();
        foreach (ListItem item in lvCondiciones.Items)
        {
            if (item.Selected)
            {
                elementos.Add(item);
                lvCondicionesSel.Items.Add(item);
            }
        }
        
        lvCondicionesSel.SelectedIndex = -1;
        
        foreach (ListItem item in elementos)
        {
            lvCondiciones.Items.Remove(item);
        }

    }

    protected void btnSelAllCond_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();

        foreach (ListItem item in lvCondiciones.Items)
        {
            elementos.Add(item);
            lvCondicionesSel.Items.Add(item);
        }

        lvCondicionesSel.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lvCondiciones.Items.Remove(item);
        }

    }

    protected void btnDesSelCond_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();
        foreach (ListItem item in lvCondicionesSel.Items)
        {
            if (item.Selected)
            {
                elementos.Add(item);
                lvCondiciones.Items.Add(item);
            }
        }

        lvCondiciones.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lvCondicionesSel.Items.Remove(item);
        }
    }

    protected void btnDesSelAllCond_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();

        foreach (ListItem item in lvCondicionesSel.Items)
        {
            elementos.Add(item);
            lvCondiciones.Items.Add(item);
        }

        lvCondiciones.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lvCondicionesSel.Items.Remove(item);
        }
    }

    protected void btnSelAcc_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();
        foreach (ListItem item in lvAcciones.Items)
        {
            if (item.Selected)
            {
                elementos.Add(item);
                lvAccionesSel.Items.Add(item);
            }
        }

        lvAccionesSel.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lvAcciones.Items.Remove(item);
        }
    }

    protected void btnSelAllAcc_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();

        foreach (ListItem item in lvAcciones.Items)
        {
            elementos.Add(item);
            lvAccionesSel.Items.Add(item);
        }

        lvAccionesSel.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lvAcciones.Items.Remove(item);
        }
    }

    protected void btnDesSelAcc_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();
        foreach (ListItem item in lvAccionesSel.Items)
        {
            if (item.Selected)
            {
                elementos.Add(item);
                lvAcciones.Items.Add(item);
            }
        }

        lvAcciones.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lvAccionesSel.Items.Remove(item);
        }
    }

    protected void btnDesSelAllAcc_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();

        foreach (ListItem item in lvAccionesSel.Items)
        {
            elementos.Add(item);
            lvAcciones.Items.Add(item);
        }

        lvAcciones.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lvAccionesSel.Items.Remove(item);
        }
    }

    #endregion

    #region Metodos

    private void MostrarPanel(int nPaso)
    {
        switch (nPaso)
        {
            case 1:
                PanelPrincipal.Visible = true;
                PanelCuerpo.Visible = false;
                PanelTexto.Visible = false;
                PanelInferencia.Visible = false;
                lstAcciones = null;
                lstCondiciones = null;
                break;
            case 2:
                lvAccionesSel.Items.Clear();
                lvCondicionesSel.Items.Clear();
                CargarListaAcciones();
                CargarListaCondiciones();
                HfnOrden.Value = "";
                PanelPrincipal.Visible = false;
                PanelCuerpo.Visible = true;
                PanelTexto.Visible = false;
                PanelInferencia.Visible = false;
                break;
            case 3:
                PanelPrincipal.Visible = false;
                PanelCuerpo.Visible = false;
                PanelTexto.Visible = true;
                PanelInferencia.Visible = false;
                txtTexto.Focus();
                break;          
            case 4:
                AgregarTexto();
                CargarGrillaDetalle();
                PanelPrincipal.Visible = false;
                PanelCuerpo.Visible = true;
                PanelTexto.Visible = false;
                PanelInferencia.Visible = false;
                break;
            case 5:
                PanelPrincipal.Visible = false;
                PanelCuerpo.Visible = false;
                PanelTexto.Visible = false;
                PanelInferencia.Visible = true;
                PanelCondiciones.Visible = true;
                PanelAcciones.Visible = false;
                PanelDetalleInferencia.Visible = false;
                break;
            case 6:
                PanelPrincipal.Visible = false;
                PanelCuerpo.Visible = false;
                PanelTexto.Visible = false;
                PanelInferencia.Visible = true;
                PanelCondiciones.Visible = false;
                PanelAcciones.Visible = true;
                PanelDetalleInferencia.Visible = false;
                break;
            case 7:
                CargarPanelPaso7();
                PanelPrincipal.Visible = false;
                PanelCuerpo.Visible = false;
                PanelTexto.Visible = false;
                PanelInferencia.Visible = true;
                PanelCondiciones.Visible = false;
                PanelAcciones.Visible = false;
                PanelDetalleInferencia.Visible = true;
                break;
            case 8:
                CargarInferencia();
                CargarGrillaDetalle();
                lvAccionesSel.Items.Clear();
                lvCondicionesSel.Items.Clear();
                HfnOrden.Value = "";
                PanelPrincipal.Visible = false;
                PanelCuerpo.Visible = true;
                PanelTexto.Visible = false;
                PanelInferencia.Visible = false;
                PanelCondiciones.Visible = false;
                PanelAcciones.Visible = false;
                PanelDetalleInferencia.Visible = false;
                break;
        }
        Master.MensajeError = "";
    }

    private void AgregarTexto()
    {
        TuCuento.Entidades.HistoriaDetalle oDetalle = new TuCuento.Entidades.HistoriaDetalle();
        oDetalle.nCod_Historia = Convert.ToInt32(HfnCod_Historia.Value);
        
        oDetalle.nCod_TipoHistoriaDetalle = Funciones.CodigoTipoHistoriaDetalle(Funciones.TipoHistoriaDetalle.Texto.ToString());
        oDetalle.Texto.sTexto = txtTexto.Text.Trim();

        if (HfnOrden.Value != "")
        {
            oDetalle.nOrden = Convert.ToInt32(HfnOrden.Value) + 1;
            lstDetalleHistoria[Convert.ToInt32(HfnOrden.Value)] = oDetalle;
        }
        else
        {
            oDetalle.nOrden = lstDetalleHistoria.Count + 1;
            lstDetalleHistoria.Add(oDetalle);
        }
        txtTexto.Text = "";
        HfnOrden.Value = "";
    }

    private void CargarGrillaDetalle()
    {
        gvDatos.DataSource = lstDetalleHistoria;
        gvDatos.DataBind();

        Funciones.LlenarGrillaVacia(gvDatos);
        BulletedList bl;

        //Recorro la grilla y lleno cada combo con los valores posibles de cada condicion
        foreach (GridViewRow grdRow in gvDatos.Rows)
        {
            //Obtengo el combo
            bl = (BulletedList)(gvDatos.Rows[grdRow.RowIndex].Cells[1].FindControl("bltDetalle"));

            if ((Funciones.TipoHistoriaDetalle)lstDetalleHistoria[grdRow.RowIndex].nCod_TipoHistoriaDetalle == Funciones.TipoHistoriaDetalle.Inferencia)
            {
                foreach (TuCuento.Entidades.Condicion oCondicion in lstDetalleHistoria[grdRow.RowIndex].Inferencia.Condiciones)
                {
                    bl.Items.Add(new ListItem(oCondicion.sNombre + " " + oCondicion.lstValPosible[0].sOperadorLogico + oCondicion.lstValPosible[0].sValor));
                }
            }
            else
            {
                bl.Items.Add(new ListItem(lstDetalleHistoria[grdRow.RowIndex].Texto.sTextoCorto));
            }
        }

    }

    private void InicializarPagina()
    {
        InicializarControles();
        MostrarPanel(1);
    }

    private void CargarListaCondiciones()
    {
        if (lstCondiciones == null)
        {
            DataTable oTabla = TuCuento.Negocio.Condicion_NEG.ListarCondiciones(-1, 1, Convert.ToInt32(ddlDominio.SelectedValue));

            if (oTabla.Rows.Count > 0)
                lstCondiciones = new List<TuCuento.Entidades.Condicion>();

            foreach (DataRow oRow in oTabla.Rows)
            {
                TuCuento.Entidades.Condicion oCondicion = new TuCuento.Entidades.Condicion();

                oCondicion.nCod_Condicion = Convert.ToInt32(oRow["nCod_COndicion"].ToString());
                oCondicion.sNombre = oRow["sNombre"].ToString();

                lstCondiciones.Add(oCondicion);

            }
        }
    }

    private void CargarListaAcciones()
    {
        if (lstAcciones == null)
        {
            DataTable oTabla = TuCuento.Negocio.Accion_NEG.ListarAcciones(-1, 1, Convert.ToInt32(ddlDominio.SelectedValue));

            if (oTabla.Rows.Count > 0)
                lstAcciones = new List<TuCuento.Entidades.Accion>();

            foreach (DataRow oRow in oTabla.Rows)
            {
                TuCuento.Entidades.Accion oAccion = new TuCuento.Entidades.Accion();

                oAccion.nCod_Accion = Convert.ToInt32(oRow["nCod_Accion"].ToString());
                oAccion.sNombre = oRow["sNombre"].ToString();

                lstAcciones.Add(oAccion);

            }
        }
    }

    private void CargarListaDetalle(int nCod_Historia)
    {
        DataTable oTabla = TuCuento.Negocio.HistoriaDetalle_NEG.ListarDetalleHistoria(nCod_Historia);

        foreach (DataRow oFila in oTabla.Rows)
        {
            TuCuento.Entidades.HistoriaDetalle oDetalle = new TuCuento.Entidades.HistoriaDetalle();

            oDetalle.nCod_Historia = Convert.ToInt32(oFila["nCod_Historia"].ToString());
            oDetalle.nCod_TipoHistoriaDetalle = Convert.ToInt32(oFila["nCod_TipoHistoriaDetalle"].ToString());
            oDetalle.nOrden = Convert.ToInt32(oFila["nOrden"].ToString());

            Funciones.TipoHistoriaDetalle oTipo = (Funciones.TipoHistoriaDetalle)oDetalle.nCod_TipoHistoriaDetalle;

            if (Funciones.TipoHistoriaDetalle.Texto == oTipo)
            {
                DataTable oTexto = TuCuento.Negocio.HistoriaDetalle_NEG.TraerTexto(oDetalle.nCod_Historia, oDetalle.nOrden);

                oDetalle.Texto.sTexto = oTexto.Rows[0]["sTexto"].ToString();

            }
            else
            {
                TuCuento.Entidades.HistoriaDetInf oInferencia = new TuCuento.Entidades.HistoriaDetInf();
                //Traigo las condiciones
                DataTable oTablaInferencia = TuCuento.Negocio.HistoriaDetalle_NEG.TraerCondiciones(nCod_Historia,oDetalle.nOrden);

                foreach (DataRow oFilaInf in oTablaInferencia.Rows)
                {
                    TuCuento.Entidades.Condicion oCondicion = new TuCuento.Entidades.Condicion();
                    TuCuento.Entidades.CondicionValPosible oValPosible = new TuCuento.Entidades.CondicionValPosible();
                    
                    oCondicion.nCod_Condicion = Convert.ToInt32(oFilaInf["nCod_Condicion"].ToString());
                    oCondicion.sNombre = oFilaInf["sNombreCondicion"].ToString();
                    
                    oValPosible.nCod_ValPosible = Convert.ToInt32(oFilaInf["nCod_ValPosible"].ToString());
                    oValPosible.sOperadorLogico = oFilaInf["sOperadorLogico"].ToString().PadRight(2,Convert.ToChar(" "));
                    oValPosible.sValor = oFilaInf["sValor"].ToString();

                    oCondicion.lstValPosible = new List<TuCuento.Entidades.CondicionValPosible>();
                    oCondicion.lstValPosible.Add(oValPosible);

                    oInferencia.Condiciones.Add(oCondicion);
                }

                //Traigo las acciones
                oTablaInferencia = TuCuento.Negocio.HistoriaDetalle_NEG.TraerAccion(nCod_Historia, oDetalle.nOrden);

                foreach (DataRow oFilaInf in oTablaInferencia.Rows)
                {
                    TuCuento.Entidades.Accion oAccion = new TuCuento.Entidades.Accion();
                    oAccion.nCod_Accion = Convert.ToInt32(oFilaInf["nCod_Accion"].ToString());
                    //oInferencia.Accion.nCod_Accion = Convert.ToInt32(oFilaInf["nCod_Accion"].ToString());
                    oInferencia.Accion.Add(oAccion);
                }

                oDetalle.Inferencia = oInferencia;

            }

            lstDetalleHistoria.Add(oDetalle);
        }

    }

    private void InicializarControles()
    {
        lstDetalleHistoria = new List<TuCuento.Entidades.HistoriaDetalle>();
        if (Request.QueryString["nCod_Historia"] == null)
        {
            HfnCod_Historia.Value = "-1";
            HfAccion.Value = "";
            chkHab.Checked = true;
        }
        else
        {
            HfnCod_Historia.Value = Request.QueryString["nCod_Historia"].ToString();
            HfAccion.Value = "EDIT";
        }
    }

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

    private bool ValidarPanel(int nPaso, bool bSiguiente)
    {
        bool bResp = true;

        switch (nPaso)
        {
            case 1:

                if (ddlDominio.SelectedValue == "0")
                {
                    sMensajeError = "Debe seleccionar un dominio."; 
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
                break;
            case 2:
                //NADA
                break;
            case 3:
                int nLen = txtTexto.Text.Trim().Length;
                if (nLen == 0)
                {
                    sMensajeError = "Debe completar el campo texto.";
                    bResp = false;
                }
                else
                {
                    if (nLen > 4000)
                    {
                        sMensajeError = "El campo texto tiene que tener un máximo de 4000 caracteres.";
                        bResp = false;
                    }
                }
                break;
            case 4:
                //NADA
                break;
            case 5:
                if (lvCondicionesSel.Items.Count == 0)
                {
                    sMensajeError = "Debe seleccionar al menos una condición.";
                    bResp = false;
                }
                break;
            case 6:
                if (lvAccionesSel.Items.Count == 0 && bSiguiente)
                {
                    sMensajeError = "Debe seleccionar al menos una acción.";
                    bResp = false;
                }
                break;
            case 7:
                if (bSiguiente)
                {
                    //Recorro los combos para ver que en todos haya seleccionado una opción
                    DropDownList ddlVPosible;
                    int nNoAplica = 0;

                    foreach (GridViewRow grdRow in gvCondiciones.Rows)
                    {
                        //Obtengo el combo
                        ddlVPosible = (DropDownList)(gvCondiciones.Rows[grdRow.RowIndex].Cells[1].FindControl("ddlValPosible"));

                        if (ddlVPosible.SelectedValue == "0")
                        {
                            sMensajeError = "Debe seleccionar un valor posible para cada condición.";
                            bResp = false;
                            break;
                        }
                        else
                        {
                            if (ddlVPosible.SelectedValue == "-1")
                            {
                                nNoAplica++;
                            }
                        }
                    }

                    //Verifico que no haya seleccionado N/A en todas las condiciones
                    if (nNoAplica == gvCondiciones.Rows.Count)
                    {
                        sMensajeError = "Todas las condiciones no pueden tener valor posible N/A.";
                        bResp = false;
                    }

                    //if (lvAccionesInf.SelectedIndex < 0)
                    //{
                    //    sMensajeError = "Debe seleccionar una acción.";
                    //    bResp = false;
                    //}
                }
                break;
        }

        if (!bResp)
            Master.MensajeError = sMensajeError;

        return bResp;
    }

    private bool ValidarDatos()
    {
        bool bResp = true;

        if (ddlDominio.SelectedValue == "0")
        {
            sMensajeError = "Debe seleccionar un dominio.";
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

        if (lstDetalleHistoria.Count == 0)
        {
            sMensajeError = "La historia debe tener un detalle.";
            bResp = false;
        }

        //Verifico que no este repetido el nombre
        DataTable oTabla = TuCuento.Negocio.Historia_NEG.ValHistoria(Convert.ToInt32((HfnCod_Historia.Value == "" ? "0" : HfnCod_Historia.Value)), txtNombre.Text.Trim(), Convert.ToInt32(ddlDominio.SelectedValue));
        if (oTabla.Rows.Count > 0)
        {
            sMensajeError = "El nombre de la historia que especifico ya se encuentra cargado.";
            bResp = false;
        }

        if (!bResp)
            Master.MensajeError = sMensajeError;

        return bResp;
    }

    private void CargarDatosHistoria(int nCod_Historia)
    {
        DataTable oTabla = TuCuento.Negocio.Historia_NEG.ListarHistorias(nCod_Historia, -1, -1);

        ddlDominio.SelectedValue = oTabla.Rows[0]["nCod_Dominio"].ToString();
        txtNombre.Text = oTabla.Rows[0]["sNombre"].ToString();
        txtDescripcion.Text = oTabla.Rows[0]["sDescripcion"].ToString();
        chkHab.Checked = (oTabla.Rows[0]["nHab"].ToString() == "1" ? true : false ) ;
    }

    private void CargarDetalleInferencia(TuCuento.Entidades.HistoriaDetInf oDetalle)
    {
        lvCondiciones.DataSource = lstCondiciones;
        lvCondiciones.DataTextField = "sNombre";
        lvCondiciones.DataValueField = "nCod_Condicion";
        lvCondiciones.DataBind();

        lvAcciones.DataSource = lstAcciones;
        lvAcciones.DataTextField = "sNombre";
        lvAcciones.DataValueField = "nCod_Accion";
        lvAcciones.DataBind();

        if (oDetalle != null)
        {
            //Muevo las condiciones
            List<ListItem> elementos = new List<ListItem>();
            foreach (ListItem item in lvCondiciones.Items)
            {
                foreach (TuCuento.Entidades.Condicion oCondicion in oDetalle.Condiciones)
                {
                    if (item.Value == oCondicion.nCod_Condicion.ToString())
                    {
                        elementos.Add(item);
                        lvCondicionesSel.Items.Add(item);
                    }
                }
            }

            lvCondicionesSel.SelectedIndex = -1;

            foreach (ListItem item in elementos)
            {
                lvCondiciones.Items.Remove(item);
            }

            //Muevo la accion
            elementos = new List<ListItem>();
            foreach (ListItem item in lvAcciones.Items)
            {
                foreach (TuCuento.Entidades.Accion oAccion in oDetalle.Accion)
                {
                    if (item.Value == oAccion.nCod_Accion.ToString())
                    {
                        elementos.Add(item);
                        lvAccionesSel.Items.Add(item);
                    }
                }
            }

            lvAccionesSel.SelectedIndex = -1;

            foreach (ListItem item in elementos)
            {
                lvAcciones.Items.Remove(item);
            }

        }
        
    }

    private void CargarPanelPaso7()
    {
        List<TuCuento.Entidades.Condicion> lstCondSel = new List<TuCuento.Entidades.Condicion>();
        DropDownList ddlVPosible;
        int nCod_Condicion;

        foreach (ListItem item in lvCondicionesSel.Items)
        {
            TuCuento.Entidades.Condicion oCondicion = new TuCuento.Entidades.Condicion();
            oCondicion.nCod_Condicion = Convert.ToInt32(item.Value);
            oCondicion.sNombre = item.Text;
            lstCondSel.Add(oCondicion);
        }

        gvCondiciones.DataSource = lstCondSel;
        gvCondiciones.DataBind();

        //Recorro la grilla y lleno cada combo con los valores posibles de cada condicion
        foreach (GridViewRow grdRow in gvCondiciones.Rows)
        {
            string sValorComboValPosible = "";
            //Obtengo el combo
            ddlVPosible = (DropDownList)(gvCondiciones.Rows[grdRow.RowIndex].Cells[1].FindControl("ddlValPosible"));

            //Obtengo el nCod_Condicion
            nCod_Condicion = Convert.ToInt32(Server.HtmlDecode(grdRow.Cells[0].Text.Trim()));

            //Busco los valores posibles en la base
            DataTable oTabla = TuCuento.Negocio.Condicion_NEG.TraerValoresPosibles(nCod_Condicion);

            //Cargo el combo
            ddlVPosible.DataSource = oTabla;
            ddlVPosible.DataTextField = "ValorPosible";
            ddlVPosible.DataValueField = "nCod_ValPosible";
            ddlVPosible.DataBind();

            ddlVPosible.Items.Add(new ListItem("Elija", "0"));
            ddlVPosible.Items.Add(new ListItem("N/A", "-1"));

            Funciones.OrdenarCombo(ddlVPosible);

            sValorComboValPosible = "0";
            if (HfnOrden.Value != "")
            {
                foreach (TuCuento.Entidades.Condicion oCondicion in lstDetalleHistoria[Convert.ToInt32(HfnOrden.Value)].Inferencia.Condiciones)
                {
                    if (oCondicion.nCod_Condicion == nCod_Condicion)
                    {
                        sValorComboValPosible = oCondicion.lstValPosible[0].nCod_ValPosible.ToString();
                        break;
                    }
                }
            }

            ddlVPosible.SelectedValue = sValorComboValPosible;

        }

        lvAccionesInf.Items.Clear();

        foreach (ListItem item in lvAccionesSel.Items)
        {
            lvAccionesInf.Items.Add(item);
        }

    }

    private void CargarInferencia()
    {
        TuCuento.Entidades.HistoriaDetalle oDetalle = new TuCuento.Entidades.HistoriaDetalle();
        List<TuCuento.Entidades.Condicion> oLstCond = new List<TuCuento.Entidades.Condicion>();
        TuCuento.Entidades.Condicion oCondicion;
        TuCuento.Entidades.CondicionValPosible oValPosible;
        DropDownList ddlVPosible;

        foreach (GridViewRow grdRow in gvCondiciones.Rows)
        {
            //Obtengo el combo
            ddlVPosible = (DropDownList)(gvCondiciones.Rows[grdRow.RowIndex].Cells[1].FindControl("ddlValPosible"));

            if (Convert.ToInt32(ddlVPosible.SelectedValue) > 0)
            {
                oCondicion = new TuCuento.Entidades.Condicion();
                oValPosible = new TuCuento.Entidades.CondicionValPosible();

                oValPosible.nCod_ValPosible = Convert.ToInt32(ddlVPosible.SelectedValue);
                oValPosible.sOperadorLogico = ddlVPosible.SelectedItem.Text.Substring(0, 2);
                oValPosible.sValor = ddlVPosible.SelectedItem.Text.Substring(2);

                //Obtengo el nCod_Condicion
                oCondicion.nCod_Condicion = Convert.ToInt32(Server.HtmlDecode(grdRow.Cells[0].Text.Trim()));
                oCondicion.sNombre = Server.HtmlDecode(grdRow.Cells[1].Text.Trim());
                oCondicion.lstValPosible = new List<TuCuento.Entidades.CondicionValPosible>();
                oCondicion.lstValPosible.Add(oValPosible);

                oLstCond.Add(oCondicion);
            }
        }

        foreach (ListItem item in lvAccionesInf.Items)
        {
            TuCuento.Entidades.Accion oAccion = new TuCuento.Entidades.Accion();
            oAccion.nCod_Accion = Convert.ToInt32(item.Value);
            oDetalle.Inferencia.Accion.Add(oAccion);
        }

        //oDetalle.Inferencia.Accion.nCod_Accion = Convert.ToInt32(lvAccionesInf.SelectedValue);
        oDetalle.Inferencia.Condiciones = oLstCond;
        oDetalle.nCod_Historia = Convert.ToInt32(HfnCod_Historia.Value);
        oDetalle.nCod_TipoHistoriaDetalle = Funciones.CodigoTipoHistoriaDetalle(Funciones.TipoHistoriaDetalle.Inferencia.ToString());

        if (HfnOrden.Value == "")
        {
            oDetalle.nOrden = lstDetalleHistoria.Count + 1;
            lstDetalleHistoria.Add(oDetalle);
        }
        else
        {
            oDetalle.nOrden = Convert.ToInt32(HfnOrden.Value)+1; //Porque el valor que tome en el hiden es el valor de la lista que comineza por 0
            lstDetalleHistoria[Convert.ToInt32(HfnOrden.Value)] = oDetalle;
        }

    }

    #endregion

}
