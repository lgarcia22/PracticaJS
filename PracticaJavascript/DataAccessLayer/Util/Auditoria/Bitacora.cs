using System;
using System.Data.SqlClient;
using System.Data;
using DataEntityLayer.Models;
using DataAccessLayer.Util;
using DataAccessLayer.Util.Database;

namespace DataAccessLayer.Util.Auditoria
{
    public class BitacoraException : Exception
    {
        
        private String mensajeError = null;

        public BitacoraException(string error)
        : base(error)
        {
            mensajeError = base.Message;
            GuadarBitacora();
        }

        public CustomJsonResult GuadarBitacora()
        {
            CustomJsonResult result = new CustomJsonResult();
            SqlConnection conn = ScriptSql.DefaultSqlConnection();
            try
            {
                using (conn)
                {
                    SqlCommand cmd = ScriptSql.SqlSpCmd(ScriptSql.SP_BITACORA, conn);
                    //Parámetros Generales
                    ScriptSql.InVarcharParam(cmd, "@pvusuario", UtilClass.GetUsuarioSesion().Nombre, UtilClass.isEstadoActivo);        
                    ScriptSql.InVarcharParam(cmd, "@pvtexto", mensajeError, UtilClass.isEstadoActivo);

                    ScriptSql.SqlDtRd(cmd);
                    conn.Close();
                    return result;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                result.typeResult = 2;
                result.message = $"Error: {e.Message}, Fuente: {e.Source}";
                return result;
            }
        }
    }
}
