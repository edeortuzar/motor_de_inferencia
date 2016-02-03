using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descripción breve de MICServicio
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class MICServicio : System.Web.Services.WebService
{

    public MICServicio()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] EjecutarRegla(byte[] Regla, List<MIC.Entidades.Termino> oTerminos)
    {
        string sXMLTerminos = "";
        string sXMLSalida = "";
        string sRespuesta = "";
        string[] vRespuesta = new string[3];

        MIC.Negocio.Negocio oNegocio = new MIC.Negocio.Negocio();

        sRespuesta = oNegocio.EjecutarRegla(Regla, oTerminos.ToArray(), ref sXMLTerminos, ref sXMLSalida);

        //Una vez que ejecuta la regla si no dio error proceso los XMLs resultantes
        if (sRespuesta == "Ok")
        {
            vRespuesta[0] = sRespuesta;
            vRespuesta[1] = sXMLTerminos;
            vRespuesta[2] = sXMLSalida;
        }

        return vRespuesta;
    }

    

}

