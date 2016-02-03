using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;


namespace TuCuento.DAL {

	public class HistoriaDetalle_DAL {

        public static Boolean ListarDetalleHistoria(int nCod_Historia, DataTable oHistorias)
        {
            try
            {
                string strCmd = "SPTCGET_tcHistoriaDetalle";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Historia;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref oHistorias);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean TraerTexto(int nCod_Historia, int nOrden, DataTable dtTabla)
        {
            try
            {
                string strCmd = "SPTCGET_tcHistoriaDetTexto";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Historia;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nOrden", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nOrden;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtTabla);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean TraerCondiciones(int nCod_Historia, int nOrden, DataTable dtTabla)
        {
            try
            {
                string strCmd = "SPTCGET_tcHistoriaDetCondicion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Historia;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nOrden", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nOrden;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtTabla);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean TraerAccion(int nCod_Historia, int nOrden, DataTable dtTabla)
        {
            try
            {
                string strCmd = "SPTCGET_tcHistoriaDetAccion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Historia;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nOrden", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nOrden;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtTabla);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

	}
}
