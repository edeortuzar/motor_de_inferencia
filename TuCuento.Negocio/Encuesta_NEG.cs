using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio
{
    public class Encuesta_NEG
    {
        public static DataTable ListarEncuestas()
        {
            return ListarEncuestas(-1,-1);
        }

        public static DataTable ListarEncuestas(int nId, int nHab)
        {
            DataTable dtEncuestas = new DataTable();
            TuCuento.DAL.Encuesta_DAL.TraerEncuesta(nId, nHab, dtEncuestas);
            return dtEncuestas;
        }

        public static Boolean PersistirEncuesta(TuCuento.Entidades.Encuesta oEncuesta)
        {
            Boolean bResp = false;
            DataTable dtEncuesta = new DataTable();
            TuCuento.DAL.Encuesta_DAL oDAL = new TuCuento.DAL.Encuesta_DAL();

            if (oDAL.AgregarEncuesta(oEncuesta))
                bResp = true;
            
            return bResp;
        }

        public static Boolean PersistirResultado(int nId, int nIdRespuesta, string sCod_Usuario, string sIp)
        {
            Boolean bResp = false;
            DataTable dtEncuesta = new DataTable();
            TuCuento.DAL.Encuesta_DAL oDAL = new TuCuento.DAL.Encuesta_DAL();

            if (oDAL.AgregarResultado(nId, nIdRespuesta, sCod_Usuario, sIp))
                bResp = true;

            return bResp;
        }

        public static DataTable TraerEncuestasUsuario(string sCod_Usuario)
        {
            DataTable dtEncuestas = new DataTable();
            TuCuento.DAL.Encuesta_DAL.TraerEncuestasUsuario(sCod_Usuario, dtEncuestas);
            return dtEncuestas;
        }

        public static DataTable TraerEncuestaDetalle(int nId)
        {
            DataTable dtEncuestas = new DataTable();
            TuCuento.DAL.Encuesta_DAL.TraerEncuestaDetalle(nId, dtEncuestas);
            return dtEncuestas;
        }

        public static DataTable TraerEncuestaResultado(int nId)
        {
            DataTable dtEncuestas = new DataTable();
            TuCuento.DAL.Encuesta_DAL.TraerEncuestaResultado(nId, dtEncuestas);
            return dtEncuestas;
        }

    }
}
