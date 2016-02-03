using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TuCuento.DAL {

	public class Accion_DAL {
        
        public static Boolean ListarAcciones(DataTable oAcciones)
        {
            try
            {
                string strCmd = "SPTCGETALL_tcAccion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref oAcciones);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean TraerAccion(int nCod_Accion, int nHab, int nCod_Dominio, DataTable dtAccion)
        {
            try
            {
                string strCmd = "SPTCGET_tcAccion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Accion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Accion;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Dominio;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtAccion);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public Boolean AgregarAccion(TuCuento.Entidades.Accion oAccion)
        {
            Boolean bResp = false;

            try
            {
                int nCod_Accion = 0;

                if (Servicios.TraerParametro(3))
                    nCod_Accion = Servicios.nValor1;
                else
                    throw new System.ArgumentException("No se pudo obtener el siguiente número de Accion", "nCod_Accion");

                //Inserto la Accion
                string strCmd = "SPTCADD_tcAccion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Accion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Accion;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.sNombre;
                prm.Size = 50;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDescripcion", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.sDescripcion;
                prm.Size = 100;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.nHab;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_TipoAccion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.nCod_TipoAccion;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.nCod_Dominio;
                cmd.Parameters.Add(prm);
                
                cmds.Add(cmd);

                if (oAccion.Hecho != null)
                {
                    strCmd = "SPTCADD_tcAccionHecho";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    prm = new SqlParameter("@nCod_Accion", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = nCod_Accion;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAccion.Hecho.nCod_Entidad;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nCod_Atributo", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAccion.Hecho.nCod_Atributo;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nValor", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAccion.Hecho.nValor;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@sValor", SqlDbType.VarChar);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAccion.Hecho.sValor;
                    prm.Size = 20;
                    cmd.Parameters.Add(prm);
                    
                    cmds.Add(cmd);
                }

                if (oAccion.Historia != null)
                {
                    strCmd = "SPTCADD_tcAccionHistoria";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nCod_Accion", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = nCod_Accion;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAccion.Historia.nCod_Historia;
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

        public Boolean ActualizarAccion(TuCuento.Entidades.Accion oAccion)
        {
            Boolean bResp = false;

            try
            {
                string strCmd = "SPTCUPD_tcAccion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Accion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.nCod_Accion;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.sNombre;
                prm.Size = 50;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDescripcion", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.sDescripcion;
                prm.Size = 100;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.nHab;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_TipoAccion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.nCod_TipoAccion;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.nCod_Dominio;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                //Primero elimino los hijos
                strCmd = "SPTCDEL_tcAccionHistoria";
                cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                prm = new SqlParameter("@nCod_Accion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.nCod_Accion;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                strCmd = "SPTCDEL_tcAccionHecho";
                cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                prm = new SqlParameter("@nCod_Accion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oAccion.nCod_Accion;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                if (oAccion.Hecho != null)
                {
                    strCmd = "SPTCADD_tcAccionHecho";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nCod_Accion", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAccion.nCod_Accion;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAccion.Hecho.nCod_Entidad;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nCod_Atributo", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAccion.Hecho.nCod_Atributo;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nValor", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAccion.Hecho.nValor;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@sValor", SqlDbType.VarChar);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAccion.Hecho.sValor;
                    prm.Size = 20;
                    cmd.Parameters.Add(prm);

                    cmds.Add(cmd);
                }

                if (oAccion.Historia != null)
                {
                    strCmd = "SPTCADD_tcAccionHistoria";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nCod_Accion", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAccion.nCod_Accion;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAccion.Historia.nCod_Historia;
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
        
        public static Boolean ValAccion(int nCod_Accion, string sNombre, int nCod_Dominio, DataTable dtAccion)
        {
            try
            {
                string strCmd = "SPTCVAL_tcAccion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Accion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Accion;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sNombre;
                prm.Size = 50;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Dominio;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtAccion);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

	}
}
