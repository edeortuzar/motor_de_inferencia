using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Comercial_Encuesta : System.Web.UI.Page
{
    RadioButtonList rbRespuesta = new RadioButtonList();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sCod_Usuario = Request.QueryString["sCod_Usuario"].ToString();
            DataTable oDatos = TuCuento.Negocio.Encuesta_NEG.TraerEncuestasUsuario(sCod_Usuario);

            if (oDatos.Rows.Count > 0)
            {
                hsCod_Usuario.Value = sCod_Usuario;
                hId.Value = oDatos.Rows[0]["nId"].ToString();

                //Traigo la encuesta
                oDatos = TuCuento.Negocio.Encuesta_NEG.ListarEncuestas(Convert.ToInt32(hId.Value), 1);
                lblTituloEncuesta.Text = oDatos.Rows[0]["sTitulo"].ToString();
                lblPregunta.Text = oDatos.Rows[0]["sPregunta"].ToString();

                //Traigo las posibles respuestas
                oDatos = TuCuento.Negocio.Encuesta_NEG.TraerEncuestaDetalle(Convert.ToInt32(hId.Value));

                for (int nI = 0; nI <= oDatos.Rows.Count - 1; nI++)
                {
                    rbRespuesta.ID = "respuesta";
                    rbRespuesta.Items.Add(oDatos.Rows[nI]["sRespuesta"].ToString());
                    Page.Form.Controls.Add(rbRespuesta);
                }
                //rbRespuesta.AutoPostBack = true;
                Button btnsend = new Button();
                btnsend.Text = "Enviar";
                btnsend.OnClientClick = "btnSend_Click()";
                btnsend.CssClass = "boton";
                Page.Form.Controls.Add(btnsend);
            }
        }
                
    }
}
