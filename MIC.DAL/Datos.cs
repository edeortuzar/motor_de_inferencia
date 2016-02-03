using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Xml;

namespace MIC.DAL
{
    public class Datos
    {
        public Boolean EjecutarRegla(string sXML, ref string sXMLTerminos, ref string sXMLSalida)
        {
            Boolean bResp = false;

            try
            {
                string strCmd = "SPMIC_EjecutarRegla";
                
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@XMLRegla", SqlDbType.Xml);
                prm.Direction = ParameterDirection.Input;
                prm.Value = new SqlXml(new XmlTextReader(sXML, XmlNodeType.Document, null));
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@XMLTerminos", SqlDbType.Xml);
                prm.Direction = ParameterDirection.Output;
                prm.Size = 9000;
                cmd.Parameters.Add(prm);

                prm = new SqlParameter("@XMLSalida", SqlDbType.Xml);
                prm.Direction = ParameterDirection.Output;
                prm.Size = 9000;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                bResp = Servicios.ejecutarTransaccion(cmds);

                sXMLTerminos = cmd.Parameters["@XMLTerminos"].Value.ToString();
                sXMLSalida = cmd.Parameters["@XMLSalida"].Value.ToString();

            }
            catch (Exception ex)
            {
                bResp = false;
            }

            return bResp;
        }

        public int NumerodeSesion()
        {
            int nRespuesta = 0;
            bool bResp;

            try
            {
                string strCmd = "SPMIC_ObtenerSesion";
                List<SqlCommand> cmds = new List<SqlCommand>();
                SqlCommand cmd = new SqlCommand(strCmd);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm;

                prm = new SqlParameter("@IDSession", SqlDbType.Int);
                prm.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(prm);

                cmds.Add(cmd);

                bResp = Servicios.ejecutarTransaccion(cmds);

                nRespuesta = Convert.ToInt32(cmd.Parameters["@IDSession"].Value);

            }
            catch (Exception ex)
            {
                bResp = false;
            }

            return nRespuesta;
        }

    }
}
