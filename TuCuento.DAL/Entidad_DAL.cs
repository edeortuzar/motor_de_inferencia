using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System;

namespace TuCuento.DAL {

	public class Entidad_DAL {
 
        public static Boolean ListarEntidades(DataTable oEntidades)
        {
            try
            {
                string strCmd = "SPTCGETALL_tcEntidad";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref oEntidades);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean TraerEntidad(int nCod_Entidad, int nHab, DataTable dtEntidad)
        {
            try
            {
                string strCmd = "SPTCGET_tcEntidad";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Entidad;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtEntidad);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public static Boolean TraerAtributo(int nCod_Entidad, int nCod_Atributo, int nHab, DataTable dtAtributo)
        {
            try
            {
                string strCmd = "SPTCGET_tcEntidadAtributo";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Entidad;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nCod_Atributo", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Atributo;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtAtributo);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public Boolean AgregarEntidad(TuCuento.Entidades.Entidad oEntidad)
        {
            Boolean bResp = false;

            try
            {
                int nCod_Entidad = 0;

                if (Servicios.TraerParametro(1))
                    nCod_Entidad = Servicios.nValor1;
                else
                    throw new System.ArgumentException("No se pudo obtener el siguiente número de entidad", "nCod_Entidad");
                
                //Inserto la entidad
                string strCmd = "SPTCADD_tcEntidad";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Entidad;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oEntidad.sNombre;
                prm.Size = 50;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDescripcion", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oEntidad.sDescripcion;
                prm.Size = 100;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oEntidad.nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                foreach (TuCuento.Entidades.EntidadAtributo oAtributo in oEntidad.Atributos)
                {
                    //Inserto los atributos
                    strCmd = "SPTCADD_tcEntidadAtributo";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = nCod_Entidad;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAtributo.sNombre;
                    prm.Size = 50;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@sDescripcion", SqlDbType.VarChar);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAtributo.sDescripcion;
                    prm.Size = 100;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nHab", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAtributo.nHab;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nCod_TipoAtributo", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAtributo.nCod_TipoAtributo;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nCod_EntidadTipo", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oAtributo.TipoEntidad.nCod_Entidad;
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

        public Boolean ActualizarEntidad(TuCuento.Entidades.Entidad oEntidad)
        {
            Boolean bResp = false;

            try
            {
                string strCmd = "SPTCUPD_tcEntidad";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oEntidad.nCod_Entidad;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oEntidad.sNombre;
                prm.Size = 50;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDescripcion", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oEntidad.sDescripcion;
                prm.Size = 100;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oEntidad.nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                foreach (TuCuento.Entidades.EntidadAtributo oAtributo in oEntidad.Atributos)
                {
                    //Por cada atributo veo si esta o no, e inserto o actualizo según corresponda
                    DataTable dtAtributo = new DataTable();
                    
                    TraerAtributo(oEntidad.nCod_Entidad, oAtributo.nCod_Atributo, -1, dtAtributo);

                    if (dtAtributo.Rows.Count == 0)
                    {
                        //Inserto el atributo
                        strCmd = "SPTCADD_tcEntidadAtributo";
                        cmd = new SqlCommand(strCmd);
                        cmd.CommandType = CommandType.StoredProcedure;

                        prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oEntidad.nCod_Entidad;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oAtributo.sNombre;
                        prm.Size = 50;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@sDescripcion", SqlDbType.VarChar);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oAtributo.sDescripcion;
                        prm.Size = 100;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@nHab", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oAtributo.nHab;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@nCod_TipoAtributo", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oAtributo.nCod_TipoAtributo;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@nCod_EntidadTipo", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oAtributo.TipoEntidad.nCod_Entidad;
                        cmd.Parameters.Add(prm);
                    }
                    else
                    {
                        //Actualizo el atributo
                        strCmd = "SPTCUPD_tcEntidadAtributo";
                        cmd = new SqlCommand(strCmd);
                        cmd.CommandType = CommandType.StoredProcedure;

                        prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oEntidad.nCod_Entidad;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@nCod_Atributo", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oAtributo.nCod_Atributo;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oAtributo.sNombre;
                        prm.Size = 50;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@sDescripcion", SqlDbType.VarChar);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oAtributo.sDescripcion;
                        prm.Size = 100;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@nHab", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oAtributo.nHab;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@nCod_TipoAtributo", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oAtributo.nCod_TipoAtributo;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@nCod_EntidadTipo", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oAtributo.TipoEntidad.nCod_Entidad;
                        cmd.Parameters.Add(prm);
                    }

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

        public static Boolean TraerAtributos(int nCod_Entidad, int nCod_Atributo, int nHab, DataTable dtAtributos)
        {
            try
            {
                string strCmd = "SPTCGET_tcEntidadAtributo";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Entidad;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_Atributo", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Atributo;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtAtributos);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public static Boolean ValEntidad(int nCod_Entidad, string sNombre, DataTable dtEntidad)
        {
            try
            {
                string strCmd = "SPTCVAL_tcEntidad";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Entidad;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sNombre;
                prm.Size = 50;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtEntidad);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

	}
}
