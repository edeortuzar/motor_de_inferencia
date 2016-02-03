using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TuCuento.DAL {

	public class Patente_DAL {

        public static Boolean TraerPatente(int nCod_Patente, int nHab, DataTable dtPatente)
        {
            try
            {
                string strCmd = "SPTCGET_tcPatente";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Patente", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Patente;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtPatente);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public Boolean AgregarPatente(TuCuento.Entidades.Patente oPatente)
        {
            Boolean bResp = false;

            try
            {
                int nCod_Patente = 0;

                if (Servicios.TraerParametro(7))
                    nCod_Patente = Servicios.nValor1;
                else
                    throw new System.ArgumentException("No se pudo obtener el siguiente número de Patente", "nCod_Patente");

                //Inserto la Patente
                string strCmd = "SPTCADD_tcPatente";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Patente", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Patente;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDesc_Patente", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPatente.sDesc_Patente;
                prm.Size = 250;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPatente.nHab;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nNodo", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPatente.nNodo;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nNodo_Padre", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPatente.nNodo_Padre;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sUrl", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPatente.sUrl;
                prm.Size = 500;
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

        public Boolean ActualizarPatente(TuCuento.Entidades.Patente oPatente)
        {
            Boolean bResp = false;

            try
            {
                string strCmd = "SPTCUPD_tcPatente";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Patente", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPatente.nCod_Patente;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDesc_Patente", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPatente.sDesc_Patente;
                prm.Size = 250;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPatente.nHab;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nNodo", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPatente.nNodo;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nNodo_Padre", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPatente.nNodo_Padre;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sUrl", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPatente.sUrl;
                prm.Size = 500;
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
