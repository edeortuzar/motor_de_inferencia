using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio {

	public class Condicion_NEG {

        public static DataTable ListarCondiciones()
        {
            DataTable oCondiciones = new DataTable();
            TuCuento.DAL.Condicion_DAL.ListarCondiciones(oCondiciones);
            return oCondiciones;
        }

        public static DataTable ListarCondiciones(int nCod_Condicion, int nHab, int nCod_Dominio)
        {
            DataTable dtCondiciones = new DataTable();
            TuCuento.DAL.Condicion_DAL.TraerCondicion(nCod_Condicion, nHab, nCod_Dominio, dtCondiciones);
            return dtCondiciones;
        }

        public static DataTable TraerValoresPosibles(int nCod_Condicion)
        {
            DataTable oValPosibles = new DataTable();
            TuCuento.DAL.Condicion_DAL.TraerValoresPosibles(nCod_Condicion, oValPosibles);
            return oValPosibles;
        }

        public static Boolean Persistir(TuCuento.Entidades.Condicion oCondicion)
        {
            Boolean bResp = false;
            DataTable dtCondicion = new DataTable();
            TuCuento.DAL.Condicion_DAL oDAL = new TuCuento.DAL.Condicion_DAL();

            TuCuento.DAL.Condicion_DAL.TraerCondicion(oCondicion.nCod_Condicion, -1, -1, dtCondicion);

            if (dtCondicion.Rows.Count == 0)
            {
                if (oDAL.AgregarCondicion(oCondicion))
                    bResp = true;
            }
            else
            {
                if (oDAL.ActualizarCondicion(oCondicion))
                    bResp = true;
            }

            return bResp;
        }

        public static DataTable ValCondicion(int nCod_Condicion, string sNombre, int nCod_Dominio)
        {
            DataTable dtCondicion = new DataTable();
            TuCuento.DAL.Condicion_DAL.ValCondicion(nCod_Condicion, sNombre, nCod_Dominio, dtCondicion);
            return dtCondicion;
        }

	}
}
