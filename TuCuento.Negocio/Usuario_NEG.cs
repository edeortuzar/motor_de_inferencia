using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace TuCuento.Negocio {

	public class Usuario_NEG {

        #region Encriptar
        /// <summary>
        /// Método para encriptar un texto plano usando el algoritmo.
        /// Este es el mas simple posible, muchos de los datos necesarios los
        /// definimos como constantes.
        /// </summary>
        /// <param name="textoQueEncriptaremos">texto a encriptar</param>
        /// <returns>Texto encriptado</returns>
        public static string Encriptar(string textoQueEncriptaremos)
        {
            return Encriptar(textoQueEncriptaremos,
              "pass75dc@avz10", "s@lAvz", "MD5", 1, "@1B2c3D4e5F6g7H8", 128);
        }
        /// <summary>
        /// Método para encriptar un texto plano usando el algoritmo
        /// </summary>
        /// <returns>Texto encriptado</returns>
        public static string Encriptar(string textoQueEncriptaremos,
          string passBase, string saltValue, string hashAlgorithm,
          int passwordIterations, string initVector, int keySize)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(textoQueEncriptaremos);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passBase,
              saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged()
            {
                Mode = CipherMode.CBC
            };
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes,
              initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor,
             CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }
        #endregion

        #region Desencriptar
        /// <summary>
        /// Método para desencriptar un texto encriptado.
        /// </summary>
        /// <returns>Texto desencriptado</returns>
        public static string Desencriptar(string textoEncriptado)
        {
            return Desencriptar(textoEncriptado, "pass75dc@avz10", "s@lAvz", "MD5",
              1, "@1B2c3D4e5F6g7H8", 128);
        }
        /// <summary>
        /// Método para desencriptar un texto encriptado
        /// </summary>
        /// <returns>Texto desencriptado</returns>
        public static string Desencriptar(string textoEncriptado, string passBase,
          string saltValue, string hashAlgorithm, int passwordIterations,
          string initVector, int keySize)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] cipherTextBytes = Convert.FromBase64String(textoEncriptado);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passBase,
              saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged()
            {
                Mode = CipherMode.CBC
            };
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes,
              initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor,
              CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0,
              plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0,
              decryptedByteCount);
            return plainText;
        }
        #endregion

        public static DataTable ListarUsuarios()
        {
            DataTable oUsuarios = new DataTable();
            TuCuento.DAL.Usuario_DAL.ListarUsuarios(oUsuarios);
            return oUsuarios;
        }

        public static DataTable TraerUsuariosHabilitados()
        {
            DataTable oUsuarios = new DataTable();
            TuCuento.DAL.Usuario_DAL.TraerUsuariosHabilitados(oUsuarios);
            return oUsuarios;
        }

        public static DataTable TraerUsuario(TuCuento.Entidades.Usuario oUsuario)
        {
            DataTable odtUsuario = new DataTable();
            TuCuento.DAL.Usuario_DAL.TraerUsuario(oUsuario, odtUsuario);
            return odtUsuario;
        }

        public static Boolean Persistir(TuCuento.Entidades.Usuario oUsuario)
        {
            Boolean bResp = false;
            DataTable dtUsuario = new DataTable();
            TuCuento.DAL.Usuario_DAL oDAL = new TuCuento.DAL.Usuario_DAL();

            TuCuento.DAL.Usuario_DAL.TraerUsuario(oUsuario, dtUsuario);

            if (dtUsuario.Rows.Count == 0)
            {
                if (oUsuario.sPSW == "" || oUsuario.sPSW == null)
                    oUsuario.sPSW = Encriptar("INIT");
                else
                    oUsuario.sPSW = Encriptar(oUsuario.sPSW);

                if (oDAL.AgregarUsuario(oUsuario))
                    bResp = true;
            }
            else
            {
                if (oDAL.ActualizarUsuario(oUsuario))
                    bResp = true;
            }

            return bResp;
        }
    
        public TuCuento.Entidades.Usuario ValidarUsuario(string sCod_Usuario, string sPSW)
        {
            TuCuento.Entidades.Usuario oUsuario = new TuCuento.Entidades.Usuario();
            DataTable oRespuesta = new DataTable();
            oUsuario.sCod_Usuario = sCod_Usuario;
            if (TuCuento.DAL.Usuario_DAL.TraerUsuario(oUsuario,oRespuesta))
            {
                if (Desencriptar(oRespuesta.Rows[0]["sPSW"].ToString()) == sPSW && oRespuesta.Rows[0]["nHab"].ToString() == "1")
                {
                    oUsuario.sCod_Usuario = oRespuesta.Rows[0]["sCod_Usuario"].ToString();
                    oUsuario.sNombre = oRespuesta.Rows[0]["sNombre"].ToString();
                    oUsuario.sApellido = oRespuesta.Rows[0]["sApellido"].ToString();
                    oUsuario.sEmail= oRespuesta.Rows[0]["sEmail"].ToString();
                    return oUsuario;
                }
                else
                {
                    oUsuario.sCod_Usuario = null;
                    return oUsuario;
                }

            }
            else
            {
                oUsuario.sCod_Usuario = null;
                return oUsuario;
            }
            
        }

        public static DataTable TraerMenuUsuario(TuCuento.Entidades.Usuario oUsuario)
        {
            DataTable oMenu = new DataTable();
            TuCuento.DAL.Usuario_DAL.TraerMenuUsuario(oUsuario, oMenu);
            return oMenu;
        }

        public static DataTable TraerPatentesUsuario(TuCuento.Entidades.Usuario oUsuario)
        {
            DataTable oPatentes = new DataTable();
            TuCuento.DAL.Usuario_DAL.TraerPatentesUsuario(oUsuario, oPatentes);
            return oPatentes;
        }

        public static DataTable TraerFamiliasUsuario(TuCuento.Entidades.Usuario oUsuario)
        {
            DataTable oFamilias = new DataTable();
            TuCuento.DAL.Usuario_DAL.TraerFamiliasUsuario(oUsuario, oFamilias);
            return oFamilias;
        }

        public static Boolean ActualizarEstado(TuCuento.Entidades.Usuario oUsuario)
        {
            Boolean bResp = false;

            TuCuento.DAL.Usuario_DAL oDAL = new TuCuento.DAL.Usuario_DAL();
            bResp = oDAL.ActualizarEstadoUsuario(oUsuario);
                    
            return bResp;
        }

        public static Boolean ActualizarPSW(TuCuento.Entidades.Usuario oUsuario)
        {
            Boolean bResp = false;

            TuCuento.DAL.Usuario_DAL oDAL = new TuCuento.DAL.Usuario_DAL();

            oUsuario.sPSW = Encriptar(oUsuario.sPSW);

            bResp = oDAL.ActualizarPSWUsuario(oUsuario);

            return bResp;
        }

    }
}
