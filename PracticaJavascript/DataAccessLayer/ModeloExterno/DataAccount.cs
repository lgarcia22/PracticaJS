using DataAccessLayer.Util;
using DataAccessLayer.Util.Database;
using DataEntityLayer.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;


namespace DataAccessLayer.ModeloExterno
{
    public class DataAccount
    {
        ///<summary>
        /// Método para validar el usuario en la base de datos.
        ///</summary>
        ///<remarks>
        ///Autor: by acolindres 20220317
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public static CustomJsonResult ValidarUsuario(Login model, HttpRequestBase model2, string ip, string navegador)
        {
            CustomJsonResult response = new CustomJsonResult();
            SqlConnection conn = ScriptSql.DefaultSqlConnection();
            try
            {
                using (conn)
                {
                    SqlCommand cmd = ScriptSql.SqlSpCmd(ScriptSql.SP_AUTENTICACION_VALIDAR_USUARIO, conn);

                    //parametros de entrada 
                    ScriptSql.InVarcharParam(cmd, "@pcUsuario", model.usuario, UtilClass.isEstadoActivo);
                    ScriptSql.InVarcharParam(cmd, "@pcClave", UtilClass.EncriptarPassword(model.contrasena), UtilClass.isEstadoActivo);
                    ScriptSql.InVarcharParam(cmd, "@pcNavegador", navegador, UtilClass.isEstadoActivo);
                    ScriptSql.InVarcharParam(cmd, "@PcIp", ip.ToString(), UtilClass.isEstadoActivo);

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
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }

            return response;
        }

        ///<summary>
        /// Método para resetear contraseña del usuario.
        ///</summary>
        ///<remarks>
        ///Autor: by acolindres 20220317
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public static CustomJsonResult ResetUsuarioPass(Reset model, HttpRequestBase model2)
        {
            UsuarioSesion usuario = new UsuarioSesion();
            usuario = UtilClass.GetUsuarioSesion();

            CustomJsonResult response = new CustomJsonResult();
            CustomJsonResult responseVald = new CustomJsonResult();
            SqlConnection conn = ScriptSql.DefaultSqlConnection();
            try
            {
                using (conn)
                {
                    
                    using (SqlCommand cmd = new SqlCommand(ScriptSql.SP_CAMBIA_CONTRASENIA, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@pcUsuario", SqlDbType.VarChar).Value = usuario.Nombre;
                        cmd.Parameters.Add("@pcClave", SqlDbType.VarChar).Value = model.nuevaContrasena;

                        SqlParameter PcMensajeError = new SqlParameter();
                        PcMensajeError.SqlDbType = SqlDbType.VarChar;
                        PcMensajeError.Size = int.MaxValue;
                        PcMensajeError.ParameterName = "@PcMensajeError";
                        PcMensajeError.Direction = ParameterDirection.Output;
                        PcMensajeError.IsNullable = true;

                        cmd.Parameters.Add(PcMensajeError);

                        cmd.ExecuteNonQuery();

                        response.message = (cmd.Parameters["@PcMensajeError"].Value.ToString() == "null") ? null : cmd.Parameters["@PcMensajeError"].Value.ToString();
                        response.result = usuario.Nombre + ", Su contraseña fue actualizada exitosamente.";

                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {
                conn.Close();
                response.typeResult =UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }

            return response;
        }
    }
}
