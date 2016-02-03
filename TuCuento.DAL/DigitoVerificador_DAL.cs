using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;

namespace TuCuento.DAL {

	public class DigitoVerificador_DAL {

        public static Boolean ListarInformacion(DataTable oInformacion)
        {
            try
            {
                string strCmd = "SPTCGET_Verificador";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref oInformacion);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

	}
}
