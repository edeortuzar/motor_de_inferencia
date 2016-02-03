using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data;
using System.Xml;

public partial class Newsletter : System.Web.UI.Page
{
    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        TuCuento.Entidades.Usuario oUnUsuario = new TuCuento.Entidades.Usuario();
        oUnUsuario = (TuCuento.Entidades.Usuario)Session["oUsuario"];

        ArmarMenu();

        lblUsuario.Text = "Usuario: " + oUnUsuario.sApellido + ", " + oUnUsuario.sNombre;

        if (!IsPostBack)
        {
            CargarGrilla();
        }
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        LbMensaje.Text = "";

        if (flNewsLetter.FileName == "")
        {
            LbMensaje.Text = "Debe seleccionar un archivo para enviar";
            return;
        }

        try
        {
            string bodyHTML = string.Empty;
            string bodyPlain = string.Empty;

            MailMessage correo = new MailMessage();
            MailAddress mailAddress = new MailAddress("tucuento@tucuento.com.ar");
            SmtpClient client = new SmtpClient();

            //Armo el email a enviar
            correo.Subject = "TuCuento.com.ar - " + txtTitulo.Text.Trim();
            correo.From = mailAddress;
            //Genera un mail para todos los que esten suscriptos al newsletter
            DataTable oDatos = TuCuento.Negocio.Newsletter_NEG.ListarSuscripciones("TODOS", 1);

            for (int nI = 0; nI < oDatos.Rows.Count; nI++)
            {
                correo.To.Add(new MailAddress(oDatos.Rows[nI]["sEmail"].ToString()));
            }

            correo.IsBodyHtml = true;
            bodyHTML = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'
                        'http://www.w3.org/TR/html4/loose.dtd'>
                        <html lang='es'>
                        <head>
                        </head>
                        <body>
                        <table style='width:800px; border:1px solid #000;' cellpadding='10' cellspacing='0' align='center'>
                        <tr>
                        <td style='background-color:#3399FF; border-bottom:1px solid #000;'>
                        <h1 style='text-align:center; color:#ffffff;'>TuCuento.com.ar - ";
            bodyHTML = bodyHTML + txtTitulo.Text.Trim() + @"</h1></td>
                        </tr>
                        <tr>
                        <td style='border-right:1px solid #000; vertical-align:top; background-color:#FFFFFF;'>
	                        <h4 style='font-family:Arial, Helvetica, sans-serif;color:#000;'>";
            bodyHTML = bodyHTML + txtTexto.Text.Trim() + @"</h4>
                        </td>
                        </tr>
                        <tr>
                        <td colspan='2' style='background-color:#3399FF; color:#FFFFFF; font-family:Arial, Helvetica, sans-serif; font-size:.8em; border-top:1px solid #000;'>Copyright (c) 2010. TuCuento.com.ar</td>
                        </tr>
                        </body>
                        </html>";

            bodyPlain = @"// TuCuento.com.ar // //Copyright (c) 2010. TuCuento.com.ar //";
            correo.Body = bodyHTML + bodyPlain;

            correo.Attachments.Add(new Attachment(flNewsLetter.PostedFile.FileName));

            if (TuCuento.Negocio.Newsletter_NEG.PersistirNewsletter(txtTitulo.Text.Trim(), txtTexto.Text.Trim(), flNewsLetter.PostedFile.FileName))
            {
                client.Send(correo);
                LbMensaje.Text = "El newsletter fue enviado con éxito.";
                CargarGrilla();
            }
            else
            {
                LbMensaje.Text = "Ocurrio un error al generar el newsletter, por favor, intente más tarde.";
            }

        }
        catch (Exception ex)
        {
            LbMensaje.Text = "Ocurrio un error al generar el newsletter, por favor, intente más tarde.";
        }

    }

    #endregion

    #region Metodos

    private void CargarGrilla()
    {
        DataTable oTabla = TuCuento.Negocio.Newsletter_NEG.ListarNewsletter();
        gvDatos.DataSource = oTabla;
        gvDatos.DataBind();

        Funciones.LlenarGrillaVacia(gvDatos);
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
