using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;

namespace TuCuento.DAL {

	public class Historia_DAL {

        public static Boolean ListarHistorias(DataTable oHistorias)
        {
            try
            {
                string strCmd = "SPTCGETALL_tcHistoria";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref oHistorias);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean TraerHistoria(int nCod_Historia, int nHab, int nCod_Dominio, DataTable dtHistoria)
        {
            try
            {
                string strCmd = "SPTCGET_tcHistoria";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Historia;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Dominio;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Size = 50;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtHistoria);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public static Boolean TraerHistoria(int nCod_Historia, int nHab, int nCod_Dominio, string sNombre, DataTable dtHistoria)
        {
            try
            {
                string strCmd = "SPTCGET_tcHistoria";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Historia;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Dominio;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sNombre;
                prm.Size = 50;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtHistoria);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public Boolean AgregarHistoria(TuCuento.Entidades.Historia oHistoria)
        {
            Boolean bResp = false;

            try
            {
                int nCod_Historia = 0;

                if (Servicios.TraerParametro(5))
                    nCod_Historia = Servicios.nValor1;
                else
                    throw new System.ArgumentException("No se pudo obtener el siguiente número de Historia", "nCod_Historia");

                //Inserto la Historia
                string strCmd = "SPTCADD_tcHistoria";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Historia;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oHistoria.sNombre;
                prm.Size = 50;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDescripcion", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oHistoria.sDescripcion;
                prm.Size = 200;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oHistoria.nHab;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oHistoria.nCod_Dominio;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                foreach (TuCuento.Entidades.HistoriaDetalle oDetalle in oHistoria.Detalle)
                {
                    //Inserto el detalle
                    strCmd = "SPTCADD_tcHistoriaDetalle";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = nCod_Historia;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nOrden", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oDetalle.nOrden;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nCod_TipoHistoriaDetalle", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oDetalle.nCod_TipoHistoriaDetalle;
                    cmd.Parameters.Add(prm);

                    cmds.Add(cmd);

                    if (oDetalle.Texto.sTexto != null)
                    {
                        //Inserto el detalle del texto
                        strCmd = "SPTCADD_tcHistoriaDetTexto";
                        cmd = new SqlCommand(strCmd);
                        cmd.CommandType = CommandType.StoredProcedure;

                        prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = nCod_Historia;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@nOrden", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oDetalle.nOrden;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@sTexto", SqlDbType.VarChar);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oDetalle.Texto.sTexto;
                        prm.Size = 4000;
                        cmd.Parameters.Add(prm);

                        cmds.Add(cmd);
                    }

                    if (oDetalle.Inferencia.Condiciones.Count > 0)
                    {
                        foreach (TuCuento.Entidades.Accion oAccion in oDetalle.Inferencia.Accion)
                        {
                            //Inserto las acciones
                            strCmd = "SPTCADD_tcHistoriaDetAccion";
                            cmd = new SqlCommand(strCmd);
                            cmd.CommandType = CommandType.StoredProcedure;

                            prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = nCod_Historia;
                            cmd.Parameters.Add(prm);
                            prm = new SqlParameter("@nOrden", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = oDetalle.nOrden;
                            cmd.Parameters.Add(prm);
                            prm = new SqlParameter("@nCod_Accion", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = oAccion.nCod_Accion;
                            cmd.Parameters.Add(prm);

                            cmds.Add(cmd);
                        }

                        foreach (TuCuento.Entidades.Condicion oCondicion in oDetalle.Inferencia.Condiciones)
                        {
                            //Inserto la condición
                            strCmd = "SPTCADD_tcHistoriaDetCondicion";
                            cmd = new SqlCommand(strCmd);
                            cmd.CommandType = CommandType.StoredProcedure;

                            prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = nCod_Historia;
                            cmd.Parameters.Add(prm);
                            prm = new SqlParameter("@nOrden", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = oDetalle.nOrden;
                            cmd.Parameters.Add(prm);
                            prm = new SqlParameter("@nCod_Condicion", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = oCondicion.nCod_Condicion;
                            cmd.Parameters.Add(prm);
                            prm = new SqlParameter("@nCod_ValPosible", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = oCondicion.lstValPosible[0].nCod_ValPosible;
                            cmd.Parameters.Add(prm);

                            cmds.Add(cmd);
                        }
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

        public Boolean ActualizarHistoria(TuCuento.Entidades.Historia oHistoria)
        {
            Boolean bResp = false;

            try
            {
                
                //Actualizo la Historia
                string strCmd = "SPTCUPD_tcHistoria";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oHistoria.nCod_Historia;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oHistoria.sNombre;
                prm.Size = 50;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sDescripcion", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oHistoria.sDescripcion;
                prm.Size = 200;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oHistoria.nHab;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nCod_Dominio", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oHistoria.nCod_Dominio;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                //Borro todo el detalle
                strCmd = "SPTCDEL_tcHistoriaDetTexto";
                cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oHistoria.nCod_Historia;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                strCmd = "SPTCDEL_tcHistoriaDetInf";
                cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oHistoria.nCod_Historia;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                strCmd = "SPTCDEL_tcHistoriaDetalle";
                cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oHistoria.nCod_Historia;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);
                
                foreach (TuCuento.Entidades.HistoriaDetalle oDetalle in oHistoria.Detalle)
                {
                    //Inserto el detalle
                    strCmd = "SPTCADD_tcHistoriaDetalle";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oHistoria.nCod_Historia;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nOrden", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oDetalle.nOrden;
                    cmd.Parameters.Add(prm);
                    prm = new SqlParameter("@nCod_TipoHistoriaDetalle", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oDetalle.nCod_TipoHistoriaDetalle;
                    cmd.Parameters.Add(prm);

                    cmds.Add(cmd);

                    if (oDetalle.Texto.sTexto != null)
                    {
                        //Inserto el detalle del texto
                        strCmd = "SPTCADD_tcHistoriaDetTexto";
                        cmd = new SqlCommand(strCmd);
                        cmd.CommandType = CommandType.StoredProcedure;

                        prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oHistoria.nCod_Historia;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@nOrden", SqlDbType.Int);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oDetalle.nOrden;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@sTexto", SqlDbType.VarChar);
                        prm.Direction = ParameterDirection.Input;
                        prm.Value = oDetalle.Texto.sTexto;
                        prm.Size = 4000;
                        cmd.Parameters.Add(prm);

                        cmds.Add(cmd);
                    }

                    if (oDetalle.Inferencia.Condiciones.Count > 0)
                    {
                        foreach (TuCuento.Entidades.Accion oAccion in oDetalle.Inferencia.Accion)
                        {
                            //Inserto las acciones
                            strCmd = "SPTCADD_tcHistoriaDetAccion";
                            cmd = new SqlCommand(strCmd);
                            cmd.CommandType = CommandType.StoredProcedure;

                            prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = oHistoria.nCod_Historia;
                            cmd.Parameters.Add(prm);
                            prm = new SqlParameter("@nOrden", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = oDetalle.nOrden;
                            cmd.Parameters.Add(prm);
                            prm = new SqlParameter("@nCod_Accion", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = oAccion.nCod_Accion;
                            cmd.Parameters.Add(prm);

                            cmds.Add(cmd);
                        }

                        foreach (TuCuento.Entidades.Condicion oCondicion in oDetalle.Inferencia.Condiciones)
                        {
                            //Inserto la condición
                            strCmd = "SPTCADD_tcHistoriaDetCondicion";
                            cmd = new SqlCommand(strCmd);
                            cmd.CommandType = CommandType.StoredProcedure;

                            prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = oHistoria.nCod_Historia;
                            cmd.Parameters.Add(prm);
                            prm = new SqlParameter("@nOrden", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = oDetalle.nOrden;
                            cmd.Parameters.Add(prm);
                            prm = new SqlParameter("@nCod_Condicion", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = oCondicion.nCod_Condicion;
                            cmd.Parameters.Add(prm);
                            prm = new SqlParameter("@nCod_ValPosible", SqlDbType.Int);
                            prm.Direction = ParameterDirection.Input;
                            prm.Value = oCondicion.lstValPosible[0].nCod_ValPosible;
                            cmd.Parameters.Add(prm);

                            cmds.Add(cmd);
                        }
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

        public static Boolean ValHistoria(int nCod_Historia, string sNombre, int nCod_Dominio, DataTable dtHistoria)
        {
            try
            {
                string strCmd = "SPTCVAL_tcHistoria";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Historia", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Historia;
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

                return Servicios.ejecutarTransaccion(cmds, ref dtHistoria);

            }
            catch (Exception excp)
            {
                return false;
            }
        }
	}
}
