using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TuCuento.DAL
{
    public class PreguntaClave_DAL
    {

        public static Boolean TraerPreguntaClave(DataTable dtPreguntaClave)
        {
            return TraerPreguntaClave(-1, -1, dtPreguntaClave);
        }

        public static Boolean TraerPreguntaClave(int nCod_Pregunta, int nHab, DataTable dtPreguntaClave)
        {
            try
            {
                string strCmd = "SPTCGET_tcPreguntaClave";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Pregunta", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Pregunta;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nHab;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                return Servicios.ejecutarTransaccion(cmds, ref dtPreguntaClave);

            }
            catch (Exception excp)
            {
                return false;
            }
        }

        public Boolean AgregarPreguntaClave(TuCuento.Entidades.PreguntaClave oPreguntaClave)
        {
            Boolean bResp = false;

            try
            {
                int nCod_Pregunta = 0;

                if (Servicios.TraerParametro(7))
                    nCod_Pregunta = Servicios.nValor1;
                else
                    throw new System.ArgumentException("No se pudo obtener el siguiente número de PreguntaClave", "nCod_Pregunta");

                //Inserto la PreguntaClave
                string strCmd = "SPTCADD_tcPreguntaClave";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Pregunta", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = nCod_Pregunta;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sPregunta", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPreguntaClave.sPregunta;
                prm.Size = 100;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPreguntaClave.nHab;
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

        public Boolean ActualizarPreguntaClave(TuCuento.Entidades.PreguntaClave oPreguntaClave)
        {
            Boolean bResp = false;

            try
            {
                string strCmd = "SPTCUPD_tcPreguntaClave";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@nCod_Pregunta", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPreguntaClave.nCod_Pregunta;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@sPregunta", SqlDbType.VarChar);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPreguntaClave.sPregunta;
                prm.Size = 100;
                cmd.Parameters.Add(prm);
                prm = new SqlParameter("@nHab", SqlDbType.Int);
                prm.Direction = ParameterDirection.Input;
                prm.Value = oPreguntaClave.nHab;
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
