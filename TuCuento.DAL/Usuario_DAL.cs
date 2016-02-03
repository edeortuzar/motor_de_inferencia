using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TuCuento.DAL {

	public class Usuario_DAL {

        public Boolean AgregarUsuario(TuCuento.Entidades.Usuario oUsuario)
        {
            Boolean bResp = false;

            try
            {
                string strCmd = "SPTCADD_tcUsuario";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sCod_Usuario;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sNombre;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sApellido", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sApellido;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sPSW", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sPSW;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.nHab;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sEmail", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sEmail;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nCod_Pregunta", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.Pregunta.nCod_Pregunta;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sRespuesta", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = (oUsuario.sRespuesta == null ? "" : oUsuario.sRespuesta);
                cmd.Parameters.Add(prm);
                
                cmds.Add(cmd);

                //Agrego las patentes
                foreach (TuCuento.Entidades.Patente oPatente in oUsuario.lstPatentes)
                {
                    strCmd = "SPTCADD_tcPatente_Usuario";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nCod_Patente", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oPatente.nCod_Patente;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oUsuario.sCod_Usuario;
                    cmd.Parameters.Add(prm);

                    cmds.Add(cmd);
                }

                //Agrego las familias
                foreach (TuCuento.Entidades.Familia oFamilia in oUsuario.lstFamilias)
                {
                    strCmd = "SPTCADD_tcFamilia_Usuario";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nCod_Flia", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oFamilia.nCod_Flia;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oUsuario.sCod_Usuario;
                    cmd.Parameters.Add(prm);

                    cmds.Add(cmd);
                }

                bResp = Servicios.ejecutarTransaccion(cmds);

            }
            catch (Exception ex)
            {
                bResp = false;
            }

            return bResp;
        }

        public static Boolean ListarUsuarios(DataTable oUsuarios)
        {
            try
            {
                string strCmd = "SPTCGETALL_tcUsuario";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref oUsuarios);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean TraerUsuario(TuCuento.Entidades.Usuario oUsuario, DataTable dtUsuario)
        {
            try
            {
                string strCmd = "SPTCGET_tcUsuario";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sCod_Usuario;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtUsuario);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean ActualizarUsuario(TuCuento.Entidades.Usuario oUsuario)
        {
            Boolean bResp = false;
            
            try
            {
                string strCmd = "SPTCUPD_tcUsuario";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sCod_Usuario;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sNombre", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sNombre;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sApellido", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sApellido;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sPSW", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sPSW;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.nHab;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sEmail", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sEmail;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                //Borro todas las patentes que tiene asociado
                strCmd = "SPTCDEL_tcPatente_Usuario";
                cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                
                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sCod_Usuario;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                //Borro todas las familias que tiene asociado
                strCmd = "SPTCDEL_tcFamilia_Usuario";
                cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sCod_Usuario;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                //Agrego las patentes
                foreach (TuCuento.Entidades.Patente oPatente in oUsuario.lstPatentes)
                {
                    strCmd = "SPTCADD_tcPatente_Usuario";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nCod_Patente", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oPatente.nCod_Patente;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oUsuario.sCod_Usuario;
                    cmd.Parameters.Add(prm);

                    cmds.Add(cmd);
                }

                //Agrego las familias
                foreach (TuCuento.Entidades.Familia oFamilia in oUsuario.lstFamilias)
                {
                    strCmd = "SPTCADD_tcFamilia_Usuario";
                    cmd = new SqlCommand(strCmd);
                    cmd.CommandType = CommandType.StoredProcedure;

                    prm = new SqlParameter("@nCod_Flia", SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oFamilia.nCod_Flia;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = oUsuario.sCod_Usuario;
                    cmd.Parameters.Add(prm);

                    cmds.Add(cmd);
                }

                bResp = Servicios.ejecutarTransaccion(cmds);

            }
            catch (Exception ex)
            {
                bResp = false;
            }

            return bResp;
        }

        public static Boolean TraerMenuUsuario(TuCuento.Entidades.Usuario oUsuario, DataTable dtUsuario)
        {
            try
            {
                string strCmd = "SPTCGET_MenuUsuario";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sCod_Usuario;
                prm.Size = 15;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtUsuario);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean TraerPatentesUsuario(TuCuento.Entidades.Usuario oUsuario, DataTable dtUsuario)
        {
            try
            {
                string strCmd = "SPTCGET_tcPatente_Usuario";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sCod_Usuario;
                prm.Size = 15;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtUsuario);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Boolean TraerFamiliasUsuario(TuCuento.Entidades.Usuario oUsuario, DataTable dtUsuario)
        {
            try
            {
                string strCmd = "SPTCGET_tcFamilia_Usuario";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sCod_Usuario;
                prm.Size = 15;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtUsuario);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean ActualizarEstadoUsuario(TuCuento.Entidades.Usuario oUsuario)
        {
            Boolean bResp = false;

            try
            {
                string strCmd = "SPTCUPD_tcUsuario_Hab";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sCod_Usuario;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);
                
                bResp = Servicios.ejecutarTransaccion(cmds);

            }
            catch (Exception ex)
            {
                bResp = false;
            }

            return bResp;
        }

        public Boolean ActualizarPSWUsuario(TuCuento.Entidades.Usuario oUsuario)
        {
            Boolean bResp = false;

            try
            {
                string strCmd = "SPTCUPD_tcUsuario_Psw";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sCod_Usuario", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sCod_Usuario;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sPSW", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oUsuario.sPSW;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                bResp = Servicios.ejecutarTransaccion(cmds);

            }
            catch (Exception ex)
            {
                bResp = false;
            }

            return bResp;
        }

        public static Boolean TraerUsuariosHabilitados(DataTable dtUsuario)
        {
            try
            {
                string strCmd = "SPTCGET_tcUsuarioHab";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtUsuario);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

	}
}
