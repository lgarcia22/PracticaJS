using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEntityLayer.Models;
using System.Web;
using System.Security.Cryptography;
using System.IO;


namespace DataAccessLayer.Util
{
    public static class UtilClass
    {
        

        public static string estado = "0";
        public static int codigoError = 2;
        public static int codigoExitoso = 0;
        
        public static int tipoOperacionCrear = 1;
        public static int tipoOperacionModificar = 2;
        public static int tipoOperacionEliminar = 3;
        

        

        #region Enrolamiento
        public static string codigoEstadoUsuarioExternoVerificarCodigo = "1";
        public static string codigoEstadoUsuarioExternoCambioClave = "2";
        public static string codigoEstadoUsuarioExternoActivo = "3";
        public static string codigoRespuestaNula = "0";


        #endregion
        

        public static UsuarioSesion GetUsuarioSesion()
        {
            return (UsuarioSesion)HttpContext.Current.Session["USUARIO_SESION"];
        }

        public static int maximoVarchar = 32500;
        public static bool isEstadoActivo = true;
        public static int maximoInt = 32;
        private static readonly Encoding encoding = Encoding.UTF8;

        ///<summary>
        /// Este método sirve encriptar las credenciales
        ///</summary>
        ///<remarks>
        ///Autor: by acolindres 20220317
        ///</remarks>
        ///<returns>
        /// string
        /// </returns>
        public static string EncriptarPassword(string str)
        {
            string Key = "@PlTyvh4536#567*{KLa123AA!]o)_?K";
            string encryptedText;
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.ECB;

                aes.Key = encoding.GetBytes(Key);
                aes.GenerateIV();

                ICryptoTransform AESEncrypt = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] buffer = encoding.GetBytes(str);

                encryptedText = Convert.ToBase64String(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}, Fuente: {e.Source}");
            }
            return encryptedText;
        }
    }
}
