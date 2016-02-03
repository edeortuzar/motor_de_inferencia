using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Cuentos_ABMEntidades : System.Web.UI.Page
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

    private List<TuCuento.Entidades.EntidadAtributo> lstAtributos
    {
        get { return (List<TuCuento.Entidades.EntidadAtributo>)Session["Atributos"] ;}
        set
        {
            Session["Atributos"] = value;
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
        HfnCod_Entidad.Value = Server.HtmlDecode(row.Cells[1].Text.Trim());
        this.txtNombre.Text = Server.HtmlDecode(row.Cells[2].Text.Trim());
        this.txtDescripcion.Text = Server.HtmlDecode(row.Cells[3].Text.Trim());
        this.chkHab.Checked = ((CheckBox)row.FindControl("chkHabGrilla")).Checked;

        //Paso los atributos a la lista
        DataTable oTabla = TuCuento.Negocio.Entidad_NEG.TraerAtributos(Convert.ToInt32(HfnCod_Entidad.Value), -1, -1);

        foreach (DataRow fila in oTabla.Rows)
        {
            TuCuento.Entidades.EntidadAtributo oAtributo = new TuCuento.Entidades.EntidadAtributo();
            oAtributo.TipoEntidad = new TuCuento.Entidades.Entidad();
            oAtributo.nCod_Entidad = Convert.ToInt32(fila["nCod_Entidad"]);
            oAtributo.nCod_Atributo = Convert.ToInt32(fila["nCod_Atributo"]);
            oAtributo.sNombre = fila["sNombre"].ToString();
            oAtributo.sDescripcion = fila["sDescripcion"].ToString();
            oAtributo.nHab = Convert.ToInt32(fila["nHab"]);
            oAtributo.nCod_TipoAtributo = Convert.ToInt32(fila["nCod_TipoAtributo"]);
            oAtributo.TipoEntidad.nCod_Entidad = Convert.ToInt32(fila["nCod_EntidadTipo"]);
            oAtributo.TipoEntidad.sNombre = fila["sNombreEntidad"].ToString();
            lstAtributos.Add(oAtributo);
        }

        gvAtributos.DataSource = lstAtributos;
        gvAtributos.DataBind();
        
        HabDesCtrl(true);
        txtNombre.Focus();
    }

    protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Oculto la columna con el código
        if (e.Row.Cells.Count > 1)
            e.Row.Cells[1].Visible = false;
    }

    protected void gvAtributos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 1)
            e.Row.Cells[1].Visible = false;

        if (e.Row.Cells.Count > 5)
            e.Row.Cells[5].Visible = false;

        if (e.Row.Cells.Count > 7)
            e.Row.Cells[7].Visible = false;

    }

    protected void gvAtributos_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gvAtributos.SelectedRow;
        HfAccionAttr.Value = "EDIT";
        HfnCod_Atributo.Value = Server.HtmlDecode(row.Cells[1].Text.Trim());
        this.txtAttrNombre.Text = Server.HtmlDecode(row.Cells[2].Text.Trim());
        this.txtAttrDescripcion.Text = Server.HtmlDecode(row.Cells[3].Text.Trim());

        this.ddlTipoAtributo.SelectedValue = Server.HtmlDecode(row.Cells[5].Text.Trim());

        //Si es de tipo entidad selecciona la entidad
        if ((Funciones.TipoDato)Convert.ToInt32(this.ddlTipoAtributo.SelectedValue) == Funciones.TipoDato.Entidad)
        {
            this.ddlEntidad.Enabled = true;
            this.ddlEntidad.SelectedValue = Server.HtmlDecode(row.Cells[7].Text.Trim());
        }

        this.chkAttrHab.Checked = ((CheckBox)row.FindControl("chkHabGrilla")).Checked;

        HabDesCtrl(true);
    }

    protected void ddlTipoAtributo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Funciones.TipoDato)Convert.ToInt32(this.ddlTipoAtributo.SelectedValue) == Funciones.TipoDato.Entidad)
        {
            ddlEntidad.Enabled = true;
            ddlEntidad.Focus();
        }
        else
        {
            ddlEntidad.Enabled = false;
            ddlEntidad.SelectedValue = "0";
            ddlTipoAtributo.Focus();
        }
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        HabDesCtrl(true);
        InicializarControles();
        txtNombre.Focus();
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        //Valido los datos ingresados
        if (ValidarDatosAtributo())
        {
            if (HfAccionAttr.Value == "EDIT")
            {
                //Lo busco en la lista y lo modifico
                foreach (TuCuento.Entidades.EntidadAtributo oAtributo in lstAtributos)
                {
                    if (oAtributo.nCod_Atributo == Convert.ToInt32(HfnCod_Atributo.Value))
                    {
                        oAtributo.sNombre = txtAttrNombre.Text.Trim();
                        oAtributo.sDescripcion = txtAttrDescripcion.Text.Trim();
                        oAtributo.nCod_TipoAtributo = Convert.ToInt32(ddlTipoAtributo.SelectedValue);

                        if ((Funciones.TipoDato)Convert.ToInt32(this.ddlTipoAtributo.SelectedValue) == Funciones.TipoDato.Entidad)
                        {
                            oAtributo.TipoEntidad.nCod_Entidad = Convert.ToInt32(ddlEntidad.SelectedValue);
                            oAtributo.TipoEntidad.sNombre = ddlEntidad.SelectedItem.Text;
                        }
                        else
                        {
                            oAtributo.TipoEntidad.nCod_Entidad = 0;
                            oAtributo.TipoEntidad.sNombre = null;
                        }

                        oAtributo.nHab = (chkAttrHab.Checked ? 1 : 0);

                        break;
                    }
                }
            }
            else
            {
                TuCuento.Entidades.EntidadAtributo oAtributo = new TuCuento.Entidades.EntidadAtributo();
                TuCuento.Entidades.Entidad oEntidad = new TuCuento.Entidades.Entidad();

                int nCod_Atributo = lstAtributos.Count;

                nCod_Atributo++;

                oAtributo.nCod_Atributo = nCod_Atributo;
                oAtributo.sNombre = txtAttrNombre.Text.Trim();
                oAtributo.sDescripcion = txtAttrDescripcion.Text.Trim();
                oAtributo.nCod_TipoAtributo = Convert.ToInt32(ddlTipoAtributo.SelectedValue);

                if (ddlTipoAtributo.SelectedValue == "1")
                {
                    oEntidad.nCod_Entidad = Convert.ToInt32(ddlEntidad.SelectedValue);
                    oEntidad.sNombre = ddlEntidad.SelectedItem.Text;
                }

                oAtributo.TipoEntidad = oEntidad;

                oAtributo.nHab = (chkAttrHab.Checked ? 1 : 0);

                //Lo agrego a la grilla
                lstAtributos.Add(oAtributo);

            }
            
            gvAtributos.DataSource = lstAtributos;
            gvAtributos.DataBind();

            BlanquearControlesAtributos();
            txtAttrNombre.Focus();
        }
    }

    protected void btnNuevoAttr_Click(object sender, EventArgs e)
    {
        BlanquearControlesAtributos();
        txtAttrNombre.Focus();
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        //Validar lo ingresado
        if (ValidarDatos())
        {
            //Cargo los datos en un objeto
            TuCuento.Entidades.Entidad oEntidad = new TuCuento.Entidades.Entidad();

            if (HfAccion.Value == "EDIT")
                oEntidad.nCod_Entidad = Convert.ToInt32(HfnCod_Entidad.Value);

            oEntidad.sNombre = txtNombre.Text.Trim();
            oEntidad.sDescripcion = txtDescripcion.Text.Trim();
            oEntidad.nHab = (chkHab.Checked ? 1 : 0);
            oEntidad.Atributos = lstAtributos;

            if (TuCuento.Negocio.Entidad_NEG.Persistir(oEntidad))
            {   
                InicializarPagina();
                LbMensaje.Text = "Se actualizaron los datos.";
            }
            else
            {
                Master.MensajeError = "Ocurrio un error al intentar grabar la entidad, por favor, vuelva a intentarlo mas tarde. Disculpe las molestias.";
            }

        }
    }

    #endregion

    #region Metodos

    private void InicializarPagina()
    {
        CargarGrilla();
        CargarTipodeDatos();
        CargarComboEntidades();
        HabDesCtrl(false);
        InicializarControles();
    }

    private void CargarGrilla()
    {
        DataTable oTabla = TuCuento.Negocio.Entidad_NEG.ListarEntidades();
        
        gvDatos.DataSource = oTabla;
        gvDatos.DataBind();

        Funciones.LlenarGrillaVacia(gvDatos);
    }

    private void HabDesCtrl(Boolean bParam)
    {
        btnGrabar.Enabled = bParam;
        txtNombre.Enabled = bParam;
        chkHab.Enabled = bParam;

        txtAttrDescripcion.Enabled = bParam;
        txtAttrNombre.Enabled = bParam;
        chkAttrHab.Enabled = bParam;
        ddlTipoAtributo.Enabled = bParam;

        btnAgregar.Enabled = bParam;
        btnNuevoAttr.Enabled = bParam;

    }

    private void BlanquearControlesAtributos()
    {
        txtAttrNombre.Text = "";
        txtAttrDescripcion.Text = "";
        chkAttrHab.Text = "";

        ddlTipoAtributo.SelectedValue = "0";
        ddlEntidad.SelectedValue = "0";

        chkAttrHab.Checked = true;
        ddlEntidad.Enabled = false;
        
        HfAccionAttr.Value = "";
    }

    private void InicializarControles()
    {
        Master.MensajeError = "";
        LbMensaje.Text = "";
        txtNombre.Text = "";
        txtDescripcion.Text = "";
        chkHab.Checked = true;

        BlanquearControlesAtributos();

        gvAtributos.DataSource = null;
        gvAtributos.DataBind();
        Funciones.LlenarGrillaVacia(gvAtributos);

        //Inicializo la lista de atributos en blanco
        lstAtributos = new List<TuCuento.Entidades.EntidadAtributo>();

        ddlEntidad.Enabled = false;

        HfAccion.Value = "";
        HfAccionAttr.Value = "";

    }

    private void CargarTipodeDatos()
    {
        Funciones.CargarComboTipoDato(ddlTipoAtributo);
    }

    private bool ValidarDatosAtributo()
    {
        Boolean bResp = true;

        Master.MensajeError = "";

        if (txtAttrNombre.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo nombre del atributo.";
            bResp = false;
        }
        
        if (txtAttrDescripcion.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo descripción del atributo.";
            bResp = false;
        }

        if (ddlTipoAtributo.SelectedValue == "0")
        {
            sMensajeError = "Debe seleccionar un tipo de atributo.";
            bResp = false;
        }
        else
        {
            if (ddlTipoAtributo.SelectedValue == "1" && ddlEntidad.SelectedValue == "0")
            {
                sMensajeError = "Debe seleccionar una entidad si selecciona el tipo de dato entidad.";
                bResp = false;
            }
        }

        if (bResp)
        {
            //Valido que el atributo no este en la lista
            foreach (TuCuento.Entidades.EntidadAtributo oAtributo in lstAtributos)
            {
                
                if (oAtributo.sNombre.Trim().ToUpper() == txtAttrNombre.Text.Trim().ToUpper())
                {
                    if (HfAccionAttr.Value == "EDIT")
                    {
                        //Verifico si es distinto código
                        if (oAtributo.nCod_Atributo != Convert.ToInt32(HfnCod_Atributo.Value))
                        {
                            sMensajeError = "Ya existe un atributo con el nombre especificado.";
                            bResp = false;
                            break;
                        }
                    }
                    else
                    {
                        sMensajeError = "Ya existe un atributo con el nombre especificado.";
                        bResp = false;
                        break;
                    }
                }
                
            }
        }

        if (!bResp)
            Master.MensajeError = sMensajeError;


        return bResp;
    }

    private void CargarComboEntidades()
    {
        DataTable oTabla = TuCuento.Negocio.Entidad_NEG.ListarEntidades(-1,1);

        ddlEntidad.DataSource = oTabla;
        ddlEntidad.DataTextField = "sNombre";
        ddlEntidad.DataValueField = "nCod_Entidad";
        ddlEntidad.DataBind();

        ddlEntidad.Items.Add(new ListItem("Elija", "0"));

    }

    private bool ValidarDatos()
    {
        bool bResp = true;

        if (txtNombre.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo nombre.";
            bResp = false;
        }

        if (txtDescripcion.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo descripcion.";
            bResp = false;
        }

        if (lstAtributos.Count == 0)
        {
            sMensajeError = "La entidad debe tener al menos un atributo.";
            bResp = false;
        }

        //Verifico que no este repetido el nombre
        DataTable oTabla = TuCuento.Negocio.Entidad_NEG.ValEntidad(Convert.ToInt32((HfnCod_Entidad.Value == "" ? "0" : HfnCod_Entidad.Value) ), txtNombre.Text.Trim());
        if (oTabla.Rows.Count > 0)
        {
            sMensajeError = "El nombre de la entidad que especifico ya se encuentra cargado.";
            bResp = false;
        }
        
        if (!bResp)
            Master.MensajeError = sMensajeError;

        return bResp;

    }

    #endregion
    
}
