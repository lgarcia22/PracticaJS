using DataAccessLayer.Util;
using DataAccessLayer.Util.Database;
using DataEntityLayer.Models;
using DataEntityLayer.Models.Productos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ModeloExterno
{
    public class DataProductos : ScriptSql
    {
        public static CustomJsonResult GestionProductos(ProductosModel model, int TipoOperacion)
        {

            CustomJsonResult response = new CustomJsonResult();
            SqlConnection conn = DefaultSqlConnection();

            try
            {
                using (conn)
                {
                    SqlCommand cmd = SqlSpCmd(SP_GESTION_PRODUCTOS, conn);

                    //Paramarámetros de entrada 
                    InIntParam(cmd, "@pnTipoOperacion", TipoOperacion, UtilClass.isEstadoActivo);
                    InIntParam(cmd, "@pnProductID", model.productID, UtilClass.isEstadoActivo);
                    InNvarcharParam(cmd, "@pcProductName", model.productName, UtilClass.isEstadoActivo);
                    InIntParam(cmd, "@pnSupplierID", model.supplier, UtilClass.isEstadoActivo);
                    InIntParam(cmd, "@pnCategoryID", model.category, UtilClass.isEstadoActivo);
                    InNvarcharParam(cmd, "@pcQuantityPerUnit", model.quantityPerUnit, UtilClass.isEstadoActivo);
                    InDecimalParam(cmd, "@pnUnitPrice", model.unitPrice, UtilClass.isEstadoActivo);
                    InIntParam(cmd, "@pnUnitsInStock", model.unitsInStock, UtilClass.isEstadoActivo);
                    InIntParam(cmd, "@pnUnitsOnOrder", model.unitsOnOrder, UtilClass.isEstadoActivo);
                    InIntParam(cmd, "@pnReorderLevel", model.reorderLevel, UtilClass.isEstadoActivo);
                    InBitParam(cmd, "@pbDiscontinued", model.discontinued, UtilClass.isEstadoActivo);
 
                    //Parámetros de salida por defecto
                    DefaultOutParams(cmd);
                    SqlDtRd(cmd);
                    response = DefaultOutParamsValue(cmd);

                    conn.Close();
                }
            }
            catch (Exception e)
            {
                //Cerrar Conexión en caso de error
                conn.Close();
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return response;
        }
    }
}
