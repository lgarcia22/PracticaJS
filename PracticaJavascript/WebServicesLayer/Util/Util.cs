using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebServicesLayer.Util
{
    public static class Util
    {
        public static string ObtenerDetalleException(Exception e)
        {
            return HttpUtility.JavaScriptStringEncode("Error: " + e.Message == null ? (e.InnerException != null ? e.InnerException.InnerException.Message : e.ToString()) : e.Message);
        }
    }

    public static class UtilWs
    {
        //listar web services 
        #region listar servicios 
        public static string nombreWebService = "nombreWebService";

        #endregion

    }
}