using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web;
using DataEntityLayer.Models;
using DataEntityLayer.ModelsExtern;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;
using System.Security.Cryptography;
using System.IO;
using DAL= DataAccessLayer.Util;


namespace BusinessLayer.Util
{
    public static class UtilClass
    {
        
        public static String alertNotification = "AlertNotification";
        
        public static void SetVariableSesion(Object var, String nombre)
        {
            HttpContext.Current.Session[nombre] = var;
        }

        public static Boolean IsAuthenticated()
        {
            return HttpContext.Current.Session["USUARIO_SESION"] != null ? true : false;
        }

        public static String GetIp()
        {
            System.Web.HttpContext httpContext = System.Web.HttpContext.Current;

            string visitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                visitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                visitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return visitorsIPAddr;
        }

        public static List<TipoArchivo> ArchivoAdmitido = new List<TipoArchivo>()
         {
             new TipoArchivo{ extension = ".jpg", mediaType = "image/jpeg", descripcion = "Imagen", visualizacion = "S"},
             new TipoArchivo{ extension = ".png", mediaType = "image/png", descripcion = "Imagen", visualizacion = "S"},
             new TipoArchivo{ extension = ".jpeg", mediaType = "image/jpeg", descripcion = "Imagen", visualizacion = "S"},
             new TipoArchivo{ extension = ".doc", mediaType = "application/msword", descripcion = "MS Word", visualizacion = "N"},
             new TipoArchivo{ extension = ".docx", mediaType = "application/msword", descripcion = "MS Word", visualizacion = "N"},
             new TipoArchivo{ extension = ".xls", mediaType = "application/vnd.ms-excel", descripcion = "MS Excel", visualizacion = "N"},
             new TipoArchivo{ extension = ".txt", mediaType = "text/plain", descripcion = "Archivo Texto", visualizacion = "N"},
             new TipoArchivo{ extension = ".xlsx", mediaType = "application/vnd.ms-excel", descripcion = "MS Excel", visualizacion = "N"},
             new TipoArchivo{ extension = ".pdf", mediaType = "application/pdf", descripcion = "PDF", visualizacion = "S" },

             
             new TipoArchivo{ extension = ".JPG", mediaType = "image/jpeg", descripcion = "Imagen", visualizacion = "S"},
             new TipoArchivo{ extension = ".PNG", mediaType = "image/png", descripcion = "Imagen", visualizacion = "S"},
             new TipoArchivo{ extension = ".JPEG", mediaType = "image/jpeg", descripcion = "Imagen", visualizacion = "S"},
             new TipoArchivo{ extension = ".DOC", mediaType = "application/msword", descripcion = "MS Word", visualizacion = "N"},
             new TipoArchivo{ extension = ".DOCX", mediaType = "application/msword", descripcion = "MS Word", visualizacion = "N"},
             new TipoArchivo{ extension = ".XLS", mediaType = "application/vnd.ms-excel", descripcion = "MS Excel", visualizacion = "N"},
             new TipoArchivo{ extension = ".TXT", mediaType = "text/plain", descripcion = "Archivo Texto", visualizacion = "N"},
             new TipoArchivo{ extension = ".XLSX", mediaType = "application/vnd.ms-excel", descripcion = "MS Excel", visualizacion = "N"},
             new TipoArchivo{ extension = ".PDF", mediaType = "application/pdf", descripcion = "PDF", visualizacion = "S" },
             new TipoArchivo{ extension = ".MP4", mediaType = "video/mp4", descripcion = "Video", visualizacion = "S" }
         };

        public static string Encripta(string Cadena)
        {
            string result = string.Empty;
            byte[] encryted = Encoding.Unicode.GetBytes(Cadena);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public static string Desencripta(string Cadena)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(Cadena);
            result =Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
