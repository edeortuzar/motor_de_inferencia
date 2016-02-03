using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TuCuento.DAL
{
    public class Newsletter_DAL
    {

        public static Boolean TraerSuscripcion(string sEmail, int nHab, DataTable dtSuscripcion)
        {
            try
            {
                string strCmd = "SPTCGET_tcSuscripcion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sEmail", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sEmail;
                prm.Size = 50;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nEstado", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtSuscripcion);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public Boolean AgregarSuscripcion(string sEmail, int nHab)
        {
            Boolean bResp = false;

            try
            {
                //Inserto la Suscripcion
                string strCmd = "SPTCADD_tcSuscripcion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sEmail", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sEmail;
                prm.Size = 50;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nEstado", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
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

        public Boolean ActualizarSuscripcion(string sEmail, int nHab)
        {
            Boolean bResp = false;

            try
            {
                //Inserto la Suscripcion
                string strCmd = "SPTCUPD_tcSuscripcion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sEmail", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sEmail;
                prm.Size = 50;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nEstado", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
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

        public static Boolean TraerNewsletter(DataTable dtNewsletter)
        {
            try
            {
                string strCmd = "SPTCGET_tcNewsletter";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtNewsletter);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public Boolean AgregarNewsletter(string sTitulo, string sTexto, string sArchivo)
        {
            Boolean bResp = false;

            try
            {
                //Inserto el Newsletter
                string strCmd = "SPTCADD_tcNewsletter";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@sTitulo", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sTitulo;
                prm.Size = 80;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sTexto", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sTexto;
                prm.Size = 4000;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sArchivo", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = sArchivo;
                prm.Size = 250;
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
