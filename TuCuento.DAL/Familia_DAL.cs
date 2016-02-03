using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace TuCuento.DAL {

	public class Familia_DAL {

        public static Boolean TraerFamilia(int nCod_Flia, int nHab, DataTable dtFamilia)
        {
            try
            {
                string strCmd = "SPTCGET_tcFamilia";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Flia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Flia;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtFamilia);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public Boolean AgregarFamilia(TuCuento.Entidades.Familia oFamilia)
        {
            Boolean bResp = false;

            try
            {
                int nCod_Flia = 0;

                if (Servicios.TraerParametro(8))
                    nCod_Flia = Servicios.nValor1;
                else
                    throw new System.ArgumentException("No se pudo obtener el siguiente número de Familia", "nCod_Flia");

                //Inserto la Familia
                string strCmd = "SPTCADD_tcFamilia";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Flia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Flia;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDesc_Flia", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oFamilia.sDesc_Flia;
                prm.Size = 50;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oFamilia.nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                //Inserto las patentes
                foreach (TuCuento.Entidades.Patente oPatente in oFamilia.lstPatentes)
                {
                    strCmd = "SPTCADD_tcFamilia_Patente";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    prm = new SqlParameter("@nCod_Flia", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = nCod_Flia;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nCod_Patente", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oPatente.nCod_Patente;
                    cmd.Parameters.Add(prm);
                    
                    cmds.Add(cmd);
                }

                bResp = Servicios.ejecutarTransaccion(cmds);

            }
            catch (Exception excp)
            {
                bResp = false;
            }

            return bResp;
        }

        public Boolean ActualizarFamilia(TuCuento.Entidades.Familia oFamilia)
        {
            Boolean bResp = false;

            try
            {
                string strCmd = "SPTCUPD_tcFamilia";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Flia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oFamilia.nCod_Flia;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDesc_Flia", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oFamilia.sDesc_Flia;
                prm.Size = 50;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oFamilia.nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                //Borro todas las patentes asociadas
                strCmd = "SPTCDEL_tcFamilia_Patente";
                cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                prm = new SqlParameter("@nCod_Flia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oFamilia.nCod_Flia;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                //Inserto las patentes
                foreach (TuCuento.Entidades.Patente oPatente in oFamilia.lstPatentes)
                {
                    strCmd = "SPTCADD_tcFamilia_Patente";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nCod_Flia", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oFamilia.nCod_Flia;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nCod_Patente", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oPatente.nCod_Patente;
                    cmd.Parameters.Add(prm);

                    cmds.Add(cmd);
                }


                bResp = Servicios.ejecutarTransaccion(cmds);


            }
            catch (Exception excp)
            {
                bResp = false;
            }

            return bResp;
        }

        public static Boolean TraerFamiliaPatente(int nCod_Flia, DataTable dtFamilia)
        {
            try
            {
                string strCmd = "SPTCGET_tcFamilia_Patente";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Flia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Flia;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtFamilia);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

	}
}
