using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace TuCuento.DAL {

	public class Backup_DAL {

        public static Boolean HacerBKBD(string sPath)
        {
            try
            {
                string strCmd = "SPTCBackUp";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sPath", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sPath;
                prm.Size = 2000;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutar(cmds);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public static Boolean HacerRestore(string sPath)
        {
            try
            {
                string strCmd = "SPTCRestoreBD";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sPath", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sPath;
                prm.Size = 2000;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarMaster(cmds);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

    }
}
