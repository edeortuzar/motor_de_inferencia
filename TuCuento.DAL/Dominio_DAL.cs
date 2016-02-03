using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TuCuento.DAL
{
    public class Dominio_DAL
    {

        public static Boolean ListarDominios(DataTable oDominios)
        {
            try
            {
                string strCmd = "SPTCGETALL_tcDominio";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref oDominios);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean TraerDominio(int nCod_Dominio, int nHab, DataTable dtDominio)
        {
            try
            {
                string strCmd = "SPTCGET_tcDominio";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Dominio;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtDominio);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public Boolean AgregarDominio(TuCuento.Entidades.Dominio oDominio)
        {
            Boolean bResp = false;

            try
            {
                int nCod_Dominio = 0;

                if (Servicios.TraerParametro(4))
                    nCod_Dominio = Servicios.nValor1;
                else
                    throw new System.ArgumentException("No se pudo obtener el siguiente número de Dominio", "nCod_Dominio");

                //Inserto la Dominio
                string strCmd = "SPTCADD_tcDominio";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Dominio;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDesc_Dominio", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oDominio.sDescripcion;
                prm.Size = 50;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oDominio.nHab;
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

        public Boolean ActualizarDominio(TuCuento.Entidades.Dominio oDominio)
        {
            Boolean bResp = false;

            try
            {
                string strCmd = "SPTCUPD_tcDominio";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oDominio.nCod_Dominio;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDesc_Dominio", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oDominio.sDescripcion;
                prm.Size = 50;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oDominio.nHab;
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

        public static Boolean ValDominio(int nCod_Dominio, string sDesc_Dominio, DataTable dtDominio)
        {
            try
            {
                string strCmd = "SPTCVAL_tcDominio";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Dominio;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sDesc_Dominio", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sDesc_Dominio;
                prm.Size = 50;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtDominio);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

    }
}
