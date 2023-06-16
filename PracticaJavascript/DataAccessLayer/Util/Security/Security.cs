using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataEntityLayer.Models;
using System.Data.SqlClient;
using System.Configuration;
using DataAccessLayer.Util.Database;
using DataEntityLayer.ModelsExtern;
using Newtonsoft.Json;

namespace DataAccessLayer.Util.Security
{
    public static class Security
    {
        ///<summary>
        /// Este método sirve para obtener los controles asociados al usuario y para el mapeo de rutas
        ///</summary>
        ///<remarks>
        ///Autor: by acolindres 20220317
        ///</remarks>
        ///<returns>
        /// void
        /// </returns>
        public static object ObtenerPermisoUsuario(String nombreUsuario)
        {
            CustomJsonResult response = new CustomJsonResult();
            UsuarioSesion usuario = new UsuarioSesion();
            List<Permiso> permisos = new List<Permiso>();
            SqlConnection conn = ScriptSql.DefaultSqlConnection();
            try
            {
                //using (SqlConnection conn = new SqlConnection(Database.Environment.GetConnectionString()))
                using (conn)
                {
                    SqlCommand cmd = ScriptSql.SqlSpCmd(ScriptSql.SP_SEG_OBTENER_CONTROLES_USUARIO, conn);

                    ScriptSql.InVarcharParam(cmd, "@pcUsuario", nombreUsuario, UtilClass.isEstadoActivo);
                    //Parámetros de salida por defecto
                    ScriptSql.DefaultOutParams(cmd);
                    // Data reader 
                    ScriptSql.SqlDtRd(cmd);
                    // Obtener valores de los parámetros de salida
                    response = ScriptSql.DefaultOutParamsValue(cmd);

                    List<VentanaControlUsuario> ventanaControlUsuario = JsonConvert.DeserializeObject<List<VentanaControlUsuario>>(response.result.ToString());


                    for (int i = 0; i < ventanaControlUsuario.Count(); i++)
                    {
                        Permiso permiso = new Permiso();
                        permiso.Pantalla = ventanaControlUsuario[i].nombreVentana;
                        permiso.Control = ventanaControlUsuario[i].nombreControl;

                        List<Ruta> ruta = new List<Ruta>();
                        foreach (ControladorAccion ca in ventanaControlUsuario[i].controladorAccion)
                        {
                            ruta.Add(new Ruta(ca.controlador, ca.accion));
                        }
                        permiso.Ruta = ruta;
                        permisos.Add(permiso);

                    }
                    conn.Close();
                }

                //MapeoRutasPermisos(permisos);

                return new { message = "", permiso = permisos };
            }
            catch (Exception e)
            {
                conn.Close();
                return new { message = "Error al obtener los permisos: " + e.Message, permiso = permisos };
            }
        }

        ///<summary>
        /// Este método sirve para obtener seguridad por contexto
        ///</summary>
        ///<remarks>
        ///Autor: by acolindres 20220329
        ///</remarks>
        ///<returns>
        /// void
        /// </returns>
        public static CustomJsonResult SeguridadContexto(SeguridadContexto values)
        {
            CustomJsonResult response = new CustomJsonResult();

            SqlConnection conn = ScriptSql.DefaultSqlConnection();
            try
            {
                //using (SqlConnection conn = new SqlConnection(Database.Environment.GetConnectionString()))
                using (conn)
                {
                    SqlCommand cmd = conn.CreateCommand();

                    cmd.CommandText = ScriptSql.FN_SEG_CONTEXTO;
                    cmd.Parameters.AddWithValue("@pnCodigoUsuario", values.codigoUsuario);
                    cmd.Parameters.AddWithValue("@pcNombreVentana", values.nombreVentana);
                    cmd.Parameters.AddWithValue("@pcNombreControl", values.nombreControl);
                    cmd.Parameters.AddWithValue("@pcContextoUno", values.contextoUno);
                    cmd.Parameters.AddWithValue("@pcValorUno", values.valorUno);
                    cmd.Parameters.AddWithValue("@pcContextoDos", String.IsNullOrEmpty(values.contextoDos) ? "" : values.contextoDos);
                    cmd.Parameters.AddWithValue("@pcValorDos", String.IsNullOrEmpty(values.valorDos) ? "" : values.valorDos);
                    cmd.Parameters.AddWithValue("@pcContextoTres", String.IsNullOrEmpty(values.contextoTres) ? "" : values.contextoTres);
                    cmd.Parameters.AddWithValue("@pcValorTres", String.IsNullOrEmpty(values.valorTres) ? "" : values.valorTres);

                    int? responseFn = cmd.ExecuteScalar() as int?;

                    conn.Close();

                    response.typeResult = UtilClass.codigoExitoso;
                    response.result = responseFn;
                    response.message = "Se ejecutó función de seguridad por contexto exitosamente";
                }
            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }

            return response;
        }
    }
}


