using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TuCuento.Negocio {

	public class DigitoVerificador_NEG {

        public static DataTable ListarInformacion()
        {
            DataTable oInformacion = new DataTable();
            TuCuento.DAL.DigitoVerificador_DAL.ListarInformacion(oInformacion);
            return oInformacion;
        }

	}
}
