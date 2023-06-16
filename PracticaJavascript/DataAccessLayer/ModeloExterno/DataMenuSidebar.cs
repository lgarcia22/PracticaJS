using DataAccessLayer.Util;
using DataAccessLayer.Util.Database;
using DataEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ModeloExterno
{
    public class DataMenuSidebar
    {
        ///<summary>
        /// Método para obtener elementos del menu sidebar
        ///</summary>
        ///<remarks>
        ///Autor: by acolindres 20220317
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public static CustomJsonResult GetElementoSidebar(string usuario)
        {
            CustomJsonResult response = new CustomJsonResult();
            SqlConnection conn = ScriptSql.DefaultSqlConnection();
            try
            {
                using (conn)
                {
                    SqlCommand cmd = ScriptSql.SqlSpCmd(ScriptSql.SP_SEGURIDAD_OBTENER_MENU_SIDEBAR, conn);

                    //parametros de entrada 
                    ScriptSql.InVarcharParam(cmd,"@pcUsuario", usuario, UtilClass.isEstadoActivo);
                    //Parámetros de salida por defecto
                    ScriptSql.DefaultOutParams(cmd);
                    // Data reader 
                    ScriptSql.SqlDtRd(cmd);
                    // Obtener valores de los parámetros de salida
                    response = ScriptSql.DefaultOutParamsValue(cmd);
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                conn.Close();
                response.typeResult = UtilClass.codigoError; ;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }

            return response;
        }
    }
}
