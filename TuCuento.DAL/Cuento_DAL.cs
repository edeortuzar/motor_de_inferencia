using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TuCuento.DAL {

	public class Cuento_DAL {

        public static Boolean ListarCuentos(DataTable oCuentos)
        {
            try
            {
                string strCmd = "SPTCGETALL_tcCuento";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref oCuentos);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean TraerCuento(int nCod_Cuento, string sCod_Usuario, DataTable dtCuento)
        {
            try
            {
                string strCmd = "SPTCGET_tcCuento";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Cuento", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Cuento;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sCod_Usuario;
                prm.Size = 15;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtCuento);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public static Boolean TraerCuento(int nCod_Cuento, DataTable dtCuento)
        {
            try
            {
                string strCmd = "SPTCGET_tcCuento";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Cuento", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Cuento;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = DBNull.Value;
                //prm.Size = 15;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtCuento);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public Boolean AgregarCuento(TuCuento.Entidades.Cuento oCuento)
        {
            Boolean bResp = false;

            try
            {
                int nCod_Cuento = 0;

                if (Servicios.TraerParametro(6))
                    nCod_Cuento = Servicios.nValor1;
                else
                    throw new System.ArgumentException("No se pudo obtener el siguiente número de Cuento", "nCod_Cuento");

                //Inserto la Cuento
                string strCmd = "SPTCADD_tcCuento";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Cuento", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Cuento;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCuento.sCod_Usuario;
                prm.Size = 15;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sTexto", SqlDbType.NVarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCuento.sTexto;
                prm.Size = -1;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCuento.nCod_Dominio;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                bResp = Servicios.ejecutarTransaccion(cmds);

            }
            catch (Exception excp)
            {
                bResp = false;
            }

            return bResp;
        }

	}
}
