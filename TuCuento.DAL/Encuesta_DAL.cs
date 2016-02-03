using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TuCuento.DAL
{
    public class Encuesta_DAL
    {

        public static Boolean TraerEncuestasUsuario(string sCod_Usuario, DataTable dtEncuesta)
        {
            try
            {
                string strCmd = "SPTCGET_tcEncuestaUsuario";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sCod_Usuario;
                prm.Size = 15;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtEncuesta);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public static Boolean TraerEncuesta(int nId, int nHab, DataTable dtEncuesta)
        {
            try
            {
                string strCmd = "SPTCGET_tcEncuesta";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nId", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nId;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtEncuesta);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public Boolean AgregarEncuesta(TuCuento.Entidades.Encuesta oEncuesta)
        {
            Boolean bResp = false;

            try
            {
                int nId = 0;

                if (Servicios.TraerParametro(9))
                    nId = Servicios.nValor1;
                else
                    throw new System.ArgumentException("No se pudo obtener el siguiente número de Encuesta", "nId");

                //Inserto la Encuesta
                string strCmd = "SPTCADD_tcEncuesta";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nId", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nId;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@dFechaFin", SqlDbType.DateTime);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oEncuesta.dFechaFin;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oEncuesta.nHab;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sTitulo", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oEncuesta.sTitulo;
                prm.Size = 80;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sPregunta", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oEncuesta.sPregunta;
                prm.Size = 255;
                cmd.Parameters.Add(prm);
                
                cmds.Add(cmd);

                //Agrego las respuestas
                foreach (TuCuento.Entidades.EncuestaDetalle oDetalle in oEncuesta.lstDetalle)
                {
                    strCmd = "SPTCADD_tcEncuestaDetalle";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    prm = new SqlParameter("@nId", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = nId;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@nIdRespuesta", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oDetalle.nIdRespuesta;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@sRespuesta", SqlDbType.VarChar);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oDetalle.sRespuesta;
                    prm.Size = 255;
                    cmd.Parameters.Add(prm);

                    cmds.Add(cmd);
                }

                //Agrego los usuarios asignados
                foreach (TuCuento.Entidades.Usuario oUsuario in oEncuesta.lstUsuario)
                {
                    strCmd = "SPTCADD_tcEncuestaUsuario";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nId", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = nId;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oUsuario.sCod_Usuario;
                    prm.Size = 15;
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

        public Boolean AgregarResultado(int nId, int nIdRespuesta, string sCod_Usuario, string sIp)
        {
            Boolean bResp = false;

            try
            {
                //Inserto el resultado
                string strCmd = "SPTCADD_tcEncuestaResultado";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nIdEncuesta", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nId;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nIdRespuesta", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nIdRespuesta;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sCod_Usuario;
                prm.Size = 15;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sIp", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sIp;
                prm.Size = 30;
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

        public static Boolean TraerEncuestaDetalle(int nId, DataTable dtEncuesta)
        {
            try
            {
                string strCmd = "SPTCGET_tcEncuestaDetalle";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nId", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nId;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtEncuesta);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public static Boolean TraerEncuestaResultado(int nId, DataTable dtEncuesta)
        {
            try
            {
                string strCmd = "SPTCGET_tcEncuestaResultado";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nIdEncuesta", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nId;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtEncuesta);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

    }
}
