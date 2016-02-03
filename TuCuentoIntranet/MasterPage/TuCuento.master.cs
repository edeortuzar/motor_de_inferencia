using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;

public partial class MasterPage_TuCuento : System.Web.UI.MasterPage
{
    #region Propiedades
    internal string _sCod_Usuario;
    internal String _mensajeError;

    public String MensajeError
    {
        get
        {
            return _mensajeError;
        }
        set
        {
            _mensajeError = value;
            lblDetalleErrores.Text = value;
            
            if (!String.IsNullOrEmpty(this.MensajeError))
            {
                pnlMasterErrores.Style.Clear();
                pnlMasterErrores.Style.Add("display", "block");
            }
            else
            {
                pnlMasterErrores.Style.Clear();
                pnlMasterErrores.Style.Add("display", "none");
            }
        }
    }

    public string sCod_Usuario { get { return _sCod_Usuario; } set { _sCod_Usuario = value; } }

    #endregion

    #region Eventos
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string _PaginaInicio = System.Configuration.ConfigurationSettings.AppSettings["URLLogin"].ToString();

        if (Session["oUsuario"] == null)
            Response.Redirect(_PaginaInicio);

        TuCuento.Entidades.Usuario oUnUsuario = new TuCuento.Entidades.Usuario();
        oUnUsuario = (TuCuento.Entidades.Usuario)Session["oUsuario"];

        sCod_Usuario = oUnUsuario.sCod_Usuario;

        if (Session["Menu"] == null)
        {
            CargarMenuaMemoria(oUnUsuario);
        }

        if (Session["bDigitoVerificador"] == null || Session["bDigitoVerificador"].ToString() == "MAL")
        {
            DataTable oDatos = TuCuento.Negocio.DigitoVerificador_NEG.ListarInformacion();

            if (oDatos.Rows.Count > 0)
            {
                Session["bDigitoVerificador"] = "MAL";
                this.pnlDigitoVerificador.Visible = true;
            }
            else
            {
                Session["bDigitoVerificador"] = "OK";
            }
        }
        
        ArmarMenu();

        lblUsuario.Text = "Usuario: " + oUnUsuario.sApellido + ", " + oUnUsuario.sNombre;

    }

    #endregion

    #region Metodos

    private void CargarMenuaMemoria(TuCuento.Entidades.Usuario oUsuario)
    {
        DataTable oDatos = TuCuento.Negocio.Usuario_NEG.TraerMenuUsuario(oUsuario);

        Session["Menu"] = oDatos.Rows[0][0].ToString();

    }

    private void ArmarMenu()
    {
        XmlDocument xmlDocMenu = new XmlDocument();
        string sMenu;
        System.Xml.XmlNodeList oLstMenus;
        System.Xml.XmlNodeList oLstMenusInternos;
        int nDiv = 0;
        string nNodo_Padre = "";

        #region MenuSuperior

        sMenu = @"<ul>";

        xmlDocMenu.LoadXml("<ROOT>" + Session["Menu"].ToString() + "</ROOT>");
        
        oLstMenus = xmlDocMenu.SelectNodes("./ROOT/Patente[@nNodo_Padre=\"0\"]");

        foreach (System.Xml.XmlNode unMenu in oLstMenus)
        {
            sMenu = sMenu + "<li><a href='" + GetApplicationPath() + unMenu.Attributes["sUrl"].InnerText + "' title=''>" + unMenu.Attributes["sDesc_Patente"].InnerText + "</a></li>";
        }

        sMenu = sMenu + "</ul>";

        ltMenuSuperior.Text = sMenu;

        #endregion

        #region MenuLateral

        sMenu = "";

        //Primero traigo los padres
        oLstMenus = xmlDocMenu.SelectNodes("./ROOT/Patente[@nNodo_Padre=\"0\"]");

        foreach (System.Xml.XmlNode unMenu in oLstMenus)
        {
            nNodo_Padre = unMenu.Attributes["nNodo"].InnerText;

            //Por cada padre busco los hijos
            oLstMenusInternos = xmlDocMenu.SelectNodes("./ROOT/Patente[@nNodo_Padre=\"" + nNodo_Padre + "\"]");

            if (oLstMenusInternos.Count > 0)
            {
                nDiv++;
                sMenu = sMenu + "<div id='div" + nDiv + "' class='boxed'><h2>" + unMenu.Attributes["sDesc_Patente"].InnerText + "</h2><div class='content'><ul>";

                foreach (System.Xml.XmlNode unSubMenu in oLstMenusInternos)
                {
                    sMenu = sMenu + "<li><a href='" + GetApplicationPath() + unSubMenu.Attributes["sUrl"].InnerText + "' title=''>" + unSubMenu.Attributes["sDesc_Patente"].InnerText + "</a></li>";
                }

                sMenu = sMenu + "</ul></div></div>";

            }

        }

        ltMenuLateral.Text = sMenu;

        #endregion
    }

    public string GetApplicationPath()
    {
        string applicationPath = "";

        if (this.Page.Request.Url != null)
            applicationPath = this.Page.Request.Url.AbsoluteUri.Substring(
             0, this.Request.Url.AbsoluteUri.ToLower().IndexOf(
              this.Request.ApplicationPath.ToLower(),
               this.Request.Url.AbsoluteUri.ToLower().IndexOf(
              this.Page.Request.Url.Authority.ToLower()) +
              this.Page.Request.Url.Authority.Length) +
             this.Request.ApplicationPath.Length);
        return applicationPath;
    }

    #endregion
}
