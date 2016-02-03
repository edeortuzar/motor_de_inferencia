using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio {

	public class Accion_NEG {

        public static DataTable ListarAcciones()
        {
            DataTable oAcciones = new DataTable();
            TuCuento.DAL.Accion_DAL.ListarAcciones(oAcciones);
            return oAcciones;
        }

        public static DataTable ListarAcciones(int nCod_Accion, int nHab, int nCod_Dominio)
        {
            DataTable dtAcciones = new DataTable();
            TuCuento.DAL.Accion_DAL.TraerAccion(nCod_Accion, nHab, nCod_Dominio, dtAcciones);
            return dtAcciones;
        }

        public static Boolean Persistir(TuCuento.Entidades.Accion oAccion)
        {
            Boolean bResp = false;
            DataTable dtAccion = new DataTable();
            TuCuento.DAL.Accion_DAL oDAL = new TuCuento.DAL.Accion_DAL();

            TuCuento.DAL.Accion_DAL.TraerAccion(oAccion.nCod_Accion, -1, -1, dtAccion);

            if (dtAccion.Rows.Count == 0)
            {
                if (oDAL.AgregarAccion(oAccion))
                    bResp = true;
            }
            else
            {
                if (oDAL.ActualizarAccion(oAccion))
                    bResp = true;
            }

            return bResp;
        }

        public static DataTable ValAccion(int nCod_Accion, string sNombre, int nCod_Dominio)
        {
            DataTable dtAccion = new DataTable();
            TuCuento.DAL.Accion_DAL.ValAccion(nCod_Accion, sNombre, nCod_Dominio, dtAccion);
            return dtAccion;
        }

	}
}
