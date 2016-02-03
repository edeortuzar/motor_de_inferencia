using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio
{
    public class Newsletter_NEG
    {

        public static DataTable ListarSuscripciones()
        {
            DataTable oSuscripciones = new DataTable();
            TuCuento.DAL.Newsletter_DAL.TraerSuscripcion("TODOS",-1,oSuscripciones);
            return oSuscripciones;
        }

        public static DataTable ListarSuscripciones(string sEmail, int nHab)
        {
            DataTable dtSuscripciones = new DataTable();
            TuCuento.DAL.Newsletter_DAL.TraerSuscripcion(sEmail,nHab,dtSuscripciones);
            return dtSuscripciones;
        }

        public static Boolean PersistirSuscripcion(string sEmail, int nHab)
        {
            Boolean bResp = false;
            DataTable dtSuscripcion = new DataTable();
            TuCuento.DAL.Newsletter_DAL oDAL = new TuCuento.DAL.Newsletter_DAL();

            TuCuento.DAL.Newsletter_DAL.TraerSuscripcion(sEmail,-1, dtSuscripcion);

            if (dtSuscripcion.Rows.Count == 0)
            {
                if (oDAL.AgregarSuscripcion(sEmail,nHab))
                    bResp = true;
            }
            else
            {
                if (oDAL.ActualizarSuscripcion(sEmail, nHab))
                    bResp = true;
            }

            return bResp;
        }

        public static Boolean PersistirNewsletter(string sTitulo, string sTexto, string sArchivo)
        {
            Boolean bResp = false;
            TuCuento.DAL.Newsletter_DAL oDAL = new TuCuento.DAL.Newsletter_DAL();

            if (oDAL.AgregarNewsletter(sTitulo, sTexto, sArchivo))
                bResp = true;
            
            return bResp;
        }

        public static DataTable ListarNewsletter()
        {
            DataTable oNewsletter = new DataTable();
            TuCuento.DAL.Newsletter_DAL.TraerNewsletter(oNewsletter);
            return oNewsletter;
        }

    }
}
