using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Comercial_RespuestaEncuesta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int nRespuesta = Convert.ToInt32(Request.QueryString["nRespuesta"].ToString());
        int nId = Convert.ToInt32(Request.QueryString["nId"].ToString());
        string sIp = Request.UserHostAddress;
        string sCod_Usuario = Request.QueryString["sCod_Usuario"].ToString();

        TuCuento.Negocio.Encuesta_NEG.PersistirResultado(nId, nRespuesta, sCod_Usuario, sIp);

        //Traigo los resultados y lo expongo en la grilla
        DataTable oDatos = TuCuento.Negocio.Encuesta_NEG.TraerEncuestaResultado(nId);

        lblTitulo.Text = "Resultado de la encuesta '" + oDatos.Rows[0]["sTitulo"].ToString() +"'";
        lblPregunta.Text = "Pregunta: '" + oDatos.Rows[0]["sPregunta"].ToString() + "'";

        gvDatos.DataSource = oDatos;
        gvDatos.DataBind();

    }
}
