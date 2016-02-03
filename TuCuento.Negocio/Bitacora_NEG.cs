using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio {

	public class Bitacora_NEG {

        public static DataTable TraerBitacora(DateTime dFechaDesde, DateTime dFechaHasta, string sCod_Usuario, int nCod_Patente)
        {
            DataTable oBitacora = new DataTable();
            TuCuento.DAL.Bitacora_DAL.ListarBitacora(dFechaDesde, dFechaHasta, sCod_Usuario, nCod_Patente, oBitacora);
            return oBitacora;
        }
	}
}
