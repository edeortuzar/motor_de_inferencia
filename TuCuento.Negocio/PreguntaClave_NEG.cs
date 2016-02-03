using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio {

	public class PreguntaClave_NEG {

        public static DataTable ListarPreguntaClave()
        {
            DataTable oPreguntaClave = new DataTable();
            TuCuento.DAL.PreguntaClave_DAL.TraerPreguntaClave(oPreguntaClave);
            return oPreguntaClave;
        }

        public static DataTable ListarPreguntaClave(int nCod_PreguntaClave, int nHab)
        {
            DataTable dtPreguntaClave = new DataTable();
            TuCuento.DAL.PreguntaClave_DAL.TraerPreguntaClave(nCod_PreguntaClave, nHab, dtPreguntaClave);
            return dtPreguntaClave;
        }

        public static Boolean Persistir(TuCuento.Entidades.PreguntaClave oPreguntaClave)
        {
            Boolean bResp = false;
            DataTable dtPreguntaClave = new DataTable();
            TuCuento.DAL.PreguntaClave_DAL oDAL = new TuCuento.DAL.PreguntaClave_DAL();

            TuCuento.DAL.PreguntaClave_DAL.TraerPreguntaClave(oPreguntaClave.nCod_Pregunta, -1, dtPreguntaClave);

            if (dtPreguntaClave.Rows.Count == 0)
            {
                if (oDAL.AgregarPreguntaClave(oPreguntaClave))
                    bResp = true;
            }
            else
            {
                if (oDAL.ActualizarPreguntaClave(oPreguntaClave))
                    bResp = true;
            }

            return bResp;
        }

	}
}
