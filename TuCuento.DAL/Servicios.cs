using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TuCuento.DAL
{
    class Servicios
    {

        public static int nValor1 { get; set; }
        public static int nValor2 { get; set; }
        public static int nValor3 { get; set; }
        public static string sValor1 { get; set; }
        public static string sValor2 { get; set; }
        public static string sValor3 { get; set; }

        private static SqlConnection _cnn = new SqlConnection();

        private static void AbrirConexion()
        {
            if ((_cnn.State == ConnectionState.Closed))
            {
                _cnn.ConnectionString = @"Integrated Security=;Persist Security Info=False;User ID=tucuentotfi;Password=tucuentotfi;Initial Catalog=TuCuento;Data Source=.";

                _cnn.Open();
            }

        }

        private static void AbrirConexionMaster()
        {
            _cnn.ConnectionString = @"Integrated Security=;Persist Security Info=False;User ID=tucuentotfi;Password=tucuentotfi;Initial Catalog=master;Data Source=.";

            _cnn.Open();

        }

        private static void CerrarConexion()
        {
            _cnn.Close();
            _cnn.Dispose();
        }

        public static bool ejecutarTransaccion(List<SqlCommand> commands)
        {
            SqlTransaction tr;
            AbrirConexion();

            tr = _cnn.BeginTransaction();

            try
            {
                foreach (SqlCommand cmd in commands)
                {
                    cmd.Connection = _cnn;
                    cmd.Transaction = tr;
                    cmd.ExecuteNonQuery();
                }
                tr.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                return false;
            }
            finally
            {
                _cnn.Close();
            }
        }

        public static bool ejecutar(List<SqlCommand> commands)
        {
            AbrirConexion();

            try
            {
                foreach (SqlCommand cmd in commands)
                {
                    cmd.Connection = _cnn;
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _cnn.Close();
            }
        }

        public static bool ejecutarMaster(List<SqlCommand> commands)
        {
            AbrirConexionMaster();

            try
            {
                foreach (SqlCommand cmd in commands)
                {
                    cmd.Connection = _cnn;
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _cnn.Close();
            }
        }

        public static bool ejecutarTransaccion(List<SqlCommand> commands, ref DataTable resultado)
        {
            SqlDataAdapter da = new SqlDataAdapter();

            AbrirConexion();

            try
            {
                foreach (SqlCommand cmd in commands)
                {
                    cmd.Connection = _cnn;
                    da.SelectCommand = cmd;
                    da.Fill(resultado);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }

        public static bool TraerParametro(int nCod_Parametro)
        {
            //Inserto la entidad
            string strCmd = "SPTCGET_tcParametros";
            SqlCommand cmd = new SqlCommand(strCmd);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter prm;
            
            prm = new SqlParameter("@nCod_Parametro", SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = nCod_Parametro;
            cmd.Parameters.Add(prm);
            
            prm = new SqlParameter("@nValor1", SqlDbType.Int);
            prm.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(prm);
            prm = new SqlParameter("@nValor2", SqlDbType.Int);
            prm.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(prm);
            prm = new SqlParameter("@nValor3", SqlDbType.Int);
            prm.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(prm);

            prm = new SqlParameter("@sValor1", SqlDbType.VarChar);
            prm.Direction = ParameterDirection.Output;
            prm.Size = 20;
            cmd.Parameters.Add(prm);
            prm = new SqlParameter("@sValor2", SqlDbType.VarChar);
            prm.Direction = ParameterDirection.Output;
            prm.Size = 20;
            cmd.Parameters.Add(prm);
            prm = new SqlParameter("@sValor3", SqlDbType.VarChar);
            prm.Direction = ParameterDirection.Output;
            prm.Size = 20;
            cmd.Parameters.Add(prm);

            SqlDataAdapter da = new SqlDataAdapter();

            AbrirConexion();

            try
            {
                cmd.Connection = _cnn;
                cmd.ExecuteNonQuery();

                nValor1 = Convert.ToInt32(cmd.Parameters["@nValor1"].Value);
                nValor2 = Convert.ToInt32(cmd.Parameters["@nValor2"].Value);
                nValor3 = Convert.ToInt32(cmd.Parameters["@nValor3"].Value);

                sValor1 = cmd.Parameters["@sValor1"].Value.ToString();
                sValor2 = cmd.Parameters["@sValor2"].Value.ToString();
                sValor3 = cmd.Parameters["@sValor3"].Value.ToString();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                CerrarConexion();
            }
        }

    }
}
