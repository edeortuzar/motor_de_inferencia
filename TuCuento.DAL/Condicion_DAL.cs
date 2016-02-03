using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;

namespace TuCuento.DAL {

	public class Condicion_DAL {

        public static Boolean ListarCondiciones(DataTable oCondiciones)
        {
            try
            {
                string strCmd = "SPTCGETALL_tcCondicion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref oCondiciones);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean TraerCondicion(int nCod_Condicion, int nHab, int nCod_Dominio, DataTable dtCondicion)
        {
            try
            {
                string strCmd = "SPTCGET_tcCondicion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Condicion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Condicion;
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

                return Servicios.ejecutarTransaccion(cmds, ref dtCondicion);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public Boolean AgregarCondicion(TuCuento.Entidades.Condicion oCondicion)
        {
            Boolean bResp = false;

            try
            {
                int nCod_Condicion = 0;

                if (Servicios.TraerParametro(2))
                    nCod_Condicion = Servicios.nValor1;
                else
                    throw new System.ArgumentException("No se pudo obtener el siguiente número de Condicion", "nCod_Condicion");

                //Inserto la Condicion
                string strCmd = "SPTCADD_tcCondicion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Condicion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Condicion;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.sNombre;
                prm.Size = 50;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDescripcion", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.sDescripcion;
                prm.Size = 100;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.nHab;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.nCod_Entidad;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_Atributo", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.nCod_Atributo;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.nCod_Dominio;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                foreach (TuCuento.Entidades.CondicionValPosible oValPosible in oCondicion.lstValPosible)
                {
                    //Inserto los valores posibles
                    strCmd = "SPTCADD_tcCondicionValPosible";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nCod_Condicion", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = nCod_Condicion;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@sValor", SqlDbType.VarChar);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oValPosible.sValor;
                    prm.Size = 255;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@sOperadorLogico", SqlDbType.VarChar);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oValPosible.sOperadorLogico;
                    prm.Size = 20;
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

        public Boolean ActualizarCondicion(TuCuento.Entidades.Condicion oCondicion)
        {
            Boolean bResp = false;

            try
            {
                string strCmd = "SPTCUPD_tcCondicion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Condicion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.nCod_Condicion;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.sNombre;
                prm.Size = 50;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDescripcion", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.sDescripcion;
                prm.Size = 100;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.nHab;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_Entidad", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.nCod_Entidad;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_Atributo", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.nCod_Atributo;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oCondicion.nCod_Dominio;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                //Primero elimino todos los valores posibles
                //strCmd = "SPTCDEL_tcCondicionValPosible";
                //cmd = new SqlCommand(strCmd);
                //cmd.CommandType = CommandType.StoredProcedure;

                //prm = new SqlParameter("@nCod_Condicion", SqlDbType.Int);
                //prm.Direction = ParameterDirection.Input;
                //prm.Value = oCondicion.nCod_Condicion;
                //cmd.Parameters.Add(prm);

                //cmds.Add(cmd);

                foreach (TuCuento.Entidades.CondicionValPosible oValPosible in oCondicion.lstValPosible)
                {
                    //Consulto si esta o no en la bd y según eso actualizo o inserto
                    DataTable oTabla = new DataTable();

                    TraerValoresPosibles(oCondicion.nCod_Condicion, oValPosible.nCod_ValPosible, oTabla);

                    if (oTabla.Rows.Count > 0)
                    {
                        //Actualizo los valores posibles
                        strCmd = "SPTCUPD_tcCondicionValPosible";
                        cmd = new SqlCommand(strCmd);
                        cmd.CommandType = CommandType.StoredProcedure;

                        prm = new SqlParameter("@nCod_Condicion", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oCondicion.nCod_Condicion;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@nCod_ValPosible", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oValPosible.nCod_ValPosible;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@sValor", SqlDbType.VarChar);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oValPosible.sValor;
                        prm.Size = 255;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@sOperadorLogico", SqlDbType.VarChar);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oValPosible.sOperadorLogico;
                        prm.Size = 20;
                        cmd.Parameters.Add(prm);

                        cmds.Add(cmd);
                    }
                    else
                    {
                        //Inserto los valores posibles
                        strCmd = "SPTCADD_tcCondicionValPosible";
                        cmd = new SqlCommand(strCmd);
                        cmd.CommandType = CommandType.StoredProcedure;

                        prm = new SqlParameter("@nCod_Condicion", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oCondicion.nCod_Condicion;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@sValor", SqlDbType.VarChar);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oValPosible.sValor;
                        prm.Size = 255;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@sOperadorLogico", SqlDbType.VarChar);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oValPosible.sOperadorLogico;
                        prm.Size = 20;
                        cmd.Parameters.Add(prm);

                        cmds.Add(cmd);
                    }
                }
                bResp = Servicios.ejecutarTransaccion(cmds);


            }
            catch (Exception excp)
            {
                bResp = false;
            }

            return bResp;
        }

        public static Boolean TraerValoresPosibles(int nCod_Condicion, DataTable dtValPosibles)
        {
            try
            {
                string strCmd = "SPTCGET_tcCondicionValPosible";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Condicion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Condicion;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nCod_ValPosible", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = -1;
                cmd.Parameters.Add(prm);
                
                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtValPosibles);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public static Boolean TraerValoresPosibles(int nCod_Condicion, int nCod_ValPosible, DataTable dtValPosibles)
        {
            try
            {
                string strCmd = "SPTCGET_tcCondicionValPosible";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Condicion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Condicion;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nCod_ValPosible", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_ValPosible;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtValPosibles);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public static Boolean ValCondicion(int nCod_Condicion, string sNombre, int nCod_Dominio,DataTable dtCondicion)
        {
            try
            {
                string strCmd = "SPTCVAL_tcCondicion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Condicion", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Condicion;
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

                return Servicios.ejecutarTransaccion(cmds, ref dtCondicion);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

	}
}
