using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System;

namespace TuCuento.DAL {

	public class Bitacora_DAL {
        
        public static Boolean ListarBitacora(DateTime dFechaDesde, DateTime dFechaHasta, string sCod_Usuario, 
            int nCod_Patente, DataTable oBitacora)
        {
            try
            {
                string strCmd = "SPTCGET_tcBitacora";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@dFechaDesde", SqlDbType.DateTime);
                prm.Direction = ParameterDirection.Input;
                prm.Value = dFechaDesde;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@dFechaHasta", SqlDbType.DateTime);
                prm.Direction = ParameterDirection.Input;
                prm.Value = dFechaHasta;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sCod_Usuario;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nCod_Patente", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Patente;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref oBitacora);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
	}
}
