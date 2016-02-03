using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;

public partial class Usuarios_ABMUsuario : System.Web.UI.Page
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
            Master.MensajeError = "";
            CargarGrilla();
            HabDesCtrl(false);

        }
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        if (!Validar())
            return;

        TuCuento.Entidades.Usuario oUsuario = new TuCuento.Entidades.Usuario();
        
        oUsuario.sCod_Usuario = txtCodigo.Text;
        oUsuario.sNombre = txtNombre.Text;
        oUsuario.sApellido = txtApellido.Text;
        oUsuario.sEmail = txtEMail.Text;
        oUsuario.bHab = chkHab.Checked;

        if (chkBlanPSW.Checked)
            oUsuario.sPSW = "INIT";

        //Cargo las patentes seleccionadas
        foreach (ListItem item in lvDerecha.Items)
        {
            TuCuento.Entidades.Patente oPatente = new TuCuento.Entidades.Patente();
            oPatente.nCod_Patente = Convert.ToInt32(item.Value);

            oUsuario.lstPatentes.Add(oPatente);
        }

        //Cargo las familias seleccionadas
        foreach (ListItem item in lstFliaSel.Items)
        {
            TuCuento.Entidades.Familia oFamilia = new TuCuento.Entidades.Familia();
            oFamilia.nCod_Flia = Convert.ToInt32(item.Value);

            oUsuario.lstFamilias.Add(oFamilia);
        }
        
        if (TuCuento.Negocio.Usuario_NEG.Persistir(oUsuario))
        {
            BlanquearControles();
            LbMensaje.Text = "Se actualizaron los datos";
            HabDesCtrl(false);
            CargarGrilla();
        }
        else
            LbMensaje.Text = "Ocurrio un error al actualizar los datos";
        
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(4000);
        HfAccion.Value = "NEW";
        BlanquearControles();
        HabDesCtrl(true);

        CargarFamilias();
        CargarPatentes();

        txtCodigo.Focus();
    }

    protected void gvDatos_SelectedIndexChanged(object sender, EventArgs e)
    {
        TuCuento.Entidades.Usuario oUsuario = new TuCuento.Entidades.Usuario();

        GridViewRow row = gvDatos.SelectedRow;
        HfAccion.Value = "";
        this.txtCodigo.Text = Server.HtmlDecode(row.Cells[1].Text.Trim());
        this.txtNombre.Text = Server.HtmlDecode(row.Cells[2].Text.Trim());
        this.txtApellido.Text = Server.HtmlDecode(row.Cells[3].Text.Trim());
        this.txtEMail.Text = Server.HtmlDecode(row.Cells[4].Text.Trim());
        this.chkHab.Checked = ((CheckBox)row.FindControl("chkHabGrilla")).Checked;

        //Cargo las patentes y las familias
        CargarPatentes();
        CargarFamilias();

        #region Muevo las listas según lo que tiene el usuario seleccionado

        oUsuario.sCod_Usuario = this.txtCodigo.Text;

        //Traigo las patentes asociadas
        DataTable oPatentes = TuCuento.Negocio.Usuario_NEG.TraerPatentesUsuario(oUsuario);

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

        //Traigo las familias asociadas
        DataTable oFamilias = TuCuento.Negocio.Usuario_NEG.TraerFamiliasUsuario(oUsuario);

        //Muevo las familias según corresponda
        elementos = new List<ListItem>();
        foreach (ListItem item in lstFlia.Items)
        {

            for (int nI = 0; nI < (oFamilias.Rows.Count); nI++)
            {
                if (item.Value == oFamilias.Rows[nI]["nCod_Flia"].ToString())
                {
                    elementos.Add(item);
                    lstFliaSel.Items.Add(item);
                    break;
                }
            }

        }

        lstFliaSel.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lstFlia.Items.Remove(item);
        }

        #endregion

        HabDesCtrl(true);
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

    protected void btnSelFlia_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();
        foreach (ListItem item in lstFlia.Items)
        {
            if (item.Selected)
            {
                elementos.Add(item);
                lstFliaSel.Items.Add(item);
            }
        }

        lstFliaSel.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lstFlia.Items.Remove(item);
        }

    }

    protected void btnSelAllFlia_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();

        foreach (ListItem item in lstFlia.Items)
        {
            elementos.Add(item);
            lstFliaSel.Items.Add(item);
        }

        lstFliaSel.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lstFlia.Items.Remove(item);
        }

    }

    protected void btnDesSelFlia_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();
        foreach (ListItem item in lstFliaSel.Items)
        {
            if (item.Selected)
            {
                elementos.Add(item);
                lstFlia.Items.Add(item);
            }
        }

        lstFlia.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lstFliaSel.Items.Remove(item);
        }
    }

    protected void btnDesSelAllFlia_Click(object sender, EventArgs e)
    {
        List<ListItem> elementos = new List<ListItem>();

        foreach (ListItem item in lstFliaSel.Items)
        {
            elementos.Add(item);
            lstFlia.Items.Add(item);
        }

        lstFlia.SelectedIndex = -1;

        foreach (ListItem item in elementos)
        {
            lstFliaSel.Items.Remove(item);
        }
    }
  
    #endregion

    #region Metodos y Funciones

    private Boolean Validar()
    {
        Boolean bResp = true;
        
        Master.MensajeError = "";

        if (txtCodigo.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo código.";
            bResp = false;
        }

        if (txtNombre.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo nombre.";
            
            bResp = false;
        }

        if (txtApellido.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo apellido.";
            
            bResp = false;
        }

        if (txtEMail.Text.Trim().Length == 0)
        {
            sMensajeError = "Debe completar el campo e-mail.";

            bResp = false;
        }
        else
        {
            string ExpresionReg = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
              + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
                [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
                [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
              + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

            if (!Regex.IsMatch(txtEMail.Text.Trim(), ExpresionReg))
            {
                sMensajeError = "Debe completar el campo e-mail con e-mail válido.";

                bResp = false;
            }

        }

        if (!bResp)
            Master.MensajeError = sMensajeError;

        return bResp;
    }

    private void CargarGrilla()
    {
        //TODO: :maxlenght de la columna del datatable
        gvDatos.DataSource = TuCuento.Negocio.Usuario_NEG.ListarUsuarios();
        gvDatos.DataBind();
    }

    private void HabDesCtrl(Boolean bParam)
    {
        btnGrabar.Enabled = bParam;
        txtCodigo.Enabled = bParam;
        txtNombre.Enabled = bParam;
        txtApellido.Enabled = bParam;
        txtEMail.Enabled = bParam;
        chkHab.Enabled = bParam;

        if (HfAccion.Value == "NEW")
            chkBlanPSW.Enabled = false;
        else
            chkBlanPSW.Enabled = bParam;

        lvIzquierda.Enabled = bParam;
        lvDerecha.Enabled = bParam;
        btnDesSel.Enabled = bParam;
        btnDesSelAll.Enabled = bParam;
        btnSel.Enabled = bParam;
        btnSelAll.Enabled = bParam;

        lstFlia.Enabled = bParam;
        lstFliaSel.Enabled = bParam;
        btnDesSelFlia.Enabled = bParam;
        btnDesSelAllFlia.Enabled = bParam;
        btnSelFlia.Enabled = bParam;
        btnSelAllFlia.Enabled = bParam;

    }

    private void BlanquearControles()
    {
        Master.MensajeError = "";
        LbMensaje.Text = "";
        txtCodigo.Text = "";
        txtNombre.Text = "";
        txtApellido.Text = "";
        txtEMail.Text = "";
        lvIzquierda.Items.Clear();
        lvDerecha.Items.Clear();
        lstFlia.Items.Clear();
        lstFliaSel.Items.Clear();

        chkHab.Checked = true;
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

    private void CargarFamilias()
    {
        //Borro las dos listas
        lstFlia.Items.Clear();
        lstFliaSel.Items.Clear();

        //Traigo los datos y lo cargo del lado izquierdo
        lstFlia.DataSource = TuCuento.Negocio.Familia_NEG.ListarFamilias(-1, 1);
        lstFlia.DataTextField = "sDesc_Flia";
        lstFlia.DataValueField = "nCod_Flia";
        lstFlia.DataBind();
    }

    #endregion
}
