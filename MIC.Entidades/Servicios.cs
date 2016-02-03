using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace MIC.DAL
{
    class Servicios
    {
        private static SqlConnection _cnn = new SqlConnection();

        private static void AbrirConexion()
        {
            if ((_cnn.State == ConnectionState.Closed))
            {
                //Notebook- con usuario sa
                //_cnn.ConnectionString = @"Integrated Security=;Persist Security Info=False;User ID=sa;Password=tucuento;Initial Catalog=TuCuento;Data Source=LECHU-PC\TUCUENTO";

                //Notebook-con usuario tucuentoweb
                //_cnn.ConnectionString = @"Integrated Security=;Persist Security Info=False;User ID=tucuentoweb;Password=tfi2010;Initial Catalog=TuCuento;Data Source=LECHU-PC\TUCUENTO";

                //PC
                //cnn.ConnectionString = "Persist Security Info=False;User ID=sa;Password=tucuento2010;Initial Catalog=Navegador;Data Source=EQUIPO\\TUCUENTO;Trusted_Connection=FALSE";

                //Facultad
                //cnn.ConnectionString = "Persist Security Info=False;User ID=sa;Password=tucuento2010;Initial Catalog=Navegador;Data Source=EQUIPO\\TUCUENTO;Trusted_Connection=FALSE";

                _cnn.ConnectionString = @"Integrated Security=;Persist Security Info=False;User ID=tucuento2010;Password=tucuento2010;Initial Catalog=MIC;Data Source=.";
                _cnn.Open();
            }

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
    }
}
