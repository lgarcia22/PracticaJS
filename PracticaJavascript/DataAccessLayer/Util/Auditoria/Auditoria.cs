using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataAccessLayer.Util;
using System.Data.SqlClient;
using DataEntityLayer.Models;
using DataAccessLayer.Util.Database;
using System.Configuration;

namespace DataAccessLayer.Util.Auditoria
{
    public class Auditoria
    {
        private String valoresNuevos;
        private String valoresAnteriores;
        private String entidad;
        private String realizarAccion;
        private String llave;

        public void SetAuditoria(DbEntityEntry entity, List<string> llaves)
        {
            valoresNuevos = "";
            valoresAnteriores = "";
            var accion = entity.State;
            realizarAccion = accion.ToString().ToUpper(); //Acción ejecutada

            if (accion == System.Data.Entity.EntityState.Added)//Acción agregar
            {
                entidad = entity.Entity.GetType().Name; //Tabla
                llave = GetKeyCurrentValues(entity, llaves); //Llaves (Primary Key)
                valoresNuevos = "Agregó el registro con valores: " + GetCurrentValues(entity);

            }
            else if (accion == System.Data.Entity.EntityState.Modified) //Acción actualizar
            {
                entidad = entity.Entity.GetType().BaseType.Name; //Tabla
                llave = GetKeyCurrentValues(entity, llaves); //Llaves (Primary Key)
                valoresNuevos = "Actualizó el registro con valores: " + GetCurrentValues(entity);
                valoresAnteriores = "El registro tenía los valores: " + GetOriginalValues(entity);
            }
            else //Acción eliminar
            {
                entidad = entity.Entity.GetType().BaseType.Name; //Tabla
                llave = GetKeyOriginalValues(entity, llaves); //Llaves (Primary Key)
                valoresNuevos = "Eliminó el registro con valores: " + GetOriginalValues(entity);
            }

            GuadarAuditoria(); // Guarda la Auditoria 


        }

        // Obtiene las llaves primarias para una accion de agregar
        public String GetKeyCurrentValues(DbEntityEntry entity, List<string> keyNames)
        {
            String Valores = null;

            foreach (string propertyName in keyNames)
            {
                Valores += "[" + entity.CurrentValues.GetValue<object>(propertyName) + "]";
            }

            return Valores;
        }

        // Obtiene las llaves primarias para una accion de eliminar
        public String GetKeyOriginalValues(DbEntityEntry entity, List<string> keyNames)
        {
            String Valores = null;

            foreach (string propertyName in keyNames)
            {
                Valores += "[" + entity.OriginalValues.GetValue<object>(propertyName) + "]";
            }

            return Valores;
        }

        // Obtiene los valores iniciales antes de cambio en una entidad
        public String GetCurrentValues(DbEntityEntry entity)
        {
            String Valores = null;
            foreach (string propertyName in entity.CurrentValues.PropertyNames)
            {
                Valores += propertyName + ":" + entity.CurrentValues.GetValue<object>(propertyName) + " ";
            }

            return Valores;
        }

        // Obtiene los valores anteriores al cambio en una entidad
        public String GetOriginalValues(DbEntityEntry entity)
        {
            String Valores = null;
            foreach (string propertyName in entity.OriginalValues.PropertyNames)
            {
                Valores += propertyName + ":" + entity.OriginalValues.GetValue<object>(propertyName) + " ";
            }


            return Valores;
        }

        // Guarda la auditoria
        public CustomJsonResult GuadarAuditoria()
        {
            CustomJsonResult result = new CustomJsonResult();
            SqlConnection conn = ScriptSql.DefaultSqlConnection();
            try
            {
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand(ScriptSql.SP_AUDITORIA, conn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pventidad", entidad);
                    cmd.Parameters.AddWithValue("@pvllave", llave);
                    cmd.Parameters.AddWithValue("@pvaccion", realizarAccion);
                    cmd.Parameters.AddWithValue("@pvusuario", UtilClass.GetUsuarioSesion().Nombre);
                    cmd.Parameters.AddWithValue("@pvvaloresnuevos", valoresNuevos);
                    cmd.Parameters.AddWithValue("@pvvaloresanteriores", valoresAnteriores);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    return result;
                }
            }
            catch (Exception e)
            {

                result.typeResult = UtilClass.codigoError;
                result.message = $"Error: {e.Message}, Fuente: {e.Source}";
                return result;
            }
        }


        ///<summary>
        /// Método que inserta en la tabla de Bitácora cuando exista un evento de inserción, modificación o eliminación en una tabla
        ///</summary>
        ///<remarks>
        ///Autor: by acolindres 20220425
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public static CustomJsonResult TrakingAuditoria(string modulo, string entidad, string componente, string tipoSentencia, string llave, string valoresAnteriores, string valoresActuales)
        {

            CustomJsonResult response = new CustomJsonResult();
            SqlConnection conn = ScriptSql.DefaultSqlConnection();
            int codUusario = UtilClass.GetUsuarioSesion().CodigoUsuario;
            try
            {

                using (conn)
                {
                    SqlCommand cmd = ScriptSql.SqlSpCmd(ScriptSql.SP_TRACKING_AUDITORIA, conn);
                    //Parámetros de entrada 
                    ScriptSql.InIntParam(cmd,"@pnCodigoUsuario", codUusario, UtilClass.isEstadoActivo);
                    ScriptSql.InVarcharParam(cmd,"@pcModulo", modulo, UtilClass.isEstadoActivo);
                    ScriptSql.InVarcharParam(cmd,"@pcEntidad", entidad, UtilClass.isEstadoActivo);
                    ScriptSql.InVarcharParam(cmd,"@pcComponente", componente, UtilClass.isEstadoActivo);
                    ScriptSql.InVarcharParam(cmd,"@pcTipoSentencia", tipoSentencia, UtilClass.isEstadoActivo);
                    ScriptSql.InVarcharParam(cmd,"@pcLlave", llave, UtilClass.isEstadoActivo);
                    ScriptSql.InVarcharParam(cmd,"@pcValoresAnteriores", valoresAnteriores, UtilClass.isEstadoActivo);
                    ScriptSql.InVarcharParam(cmd,"@pcValoresActuales", valoresActuales, UtilClass.isEstadoActivo);

                    //Parámetros de salida por defecto
                    ScriptSql.DefaultOutParams(cmd);
                    // Data reader 
                    ScriptSql.SqlDtRd(cmd);
                    //cmd.ExecuteNonQuery();
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
    }
}
