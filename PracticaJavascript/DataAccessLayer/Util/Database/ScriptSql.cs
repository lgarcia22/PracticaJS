using DataEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Util.Database
{
    public class ScriptSql
    {

        public static string SP_GESTION_PRODUCTOS = "dbo.sp_tsc_lgarcia_gestion_products";

        #region stored procedure security 
        public static string SP_SEG_OBTENER_CONTROLES_USUARIO = "[seg].[sp_seguridad_obtener_controles_usuario]";
        public static string SP_AUTENTICACION_VALIDAR_USUARIO = "[seg].[sp_autenticacion_validar_usuario]";
        public static string SP_CAMBIA_CONTRASENIA = "[seg].[SP_Cambia_Contrasenia]";
        public static string SP_SEG_OBTENER_RUTAS_CONTROLADOR = "[seg].[sp_seguridad_obtener_rutas_controlador]";
        public static string FN_SEG_CONTEXTO = @"Select [seg].[fn_seguridad_por_contexto](@pnCodigoUsuario, @pcNombreVentana, @pcNombreControl, @pcContextoUno, @pcValorUno, @pcContextoDos, @pcValorDos, @pcContextoTres,  @pcValorTres )";
        public static string FN_SEG_VALIDAR_URL_ENCRIPTADA = @"Select [seg].[fn_seguridad_validar_url_encriptada](@pcLlave, @pcTextoEncriptado, @pnCodigoUsuario, @pbValidarPertenencia )";
        public static string SP_SEG_VALIDAR_CODIGO_VERIFICACION_USUARIO = "[seg].[sp_seguridad_validar_codigo_verificacion_usuario]";
        public static string SP_SEG_CAMBIAR_CONTRASENA_USUARIO = "[seg].[sp_seguridad_cambiar_contrasena_usuario]";
        public static string SP_SEG_OBTENER_INFORMACION_USUARIO = "[seg].[sp_seguridad_obtener_informacion_usuario]";
        public static string SP_SEG_CAMBIAR_ESTADO_USUARIO_EXTERNO = "[seg].[sp_seguridad_cambiar_estado_usuario_externo]";
        #endregion

        #region stored procedure general 
        public static string SP_SEGURIDAD_OBTENER_MENU_SIDEBAR = "[seg].[sp_seguridad_obtener_menu_sidebar]";
        #endregion

        #region stored procedure bitacora y auditoria 
        public static string SP_AUDITORIA = "SP_AUDITORIA";
        public static string SP_BITACORA = "SP_BITACORA";
        public static string SP_TRACKING_AUDITORIA = "[seg].[sp_tracking_auditoria]";
        
        #endregion
        
        public static SqlConnection DefaultSqlConnection()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            return con;
        }

        public static SqlCommand SqlSpCmd(string StoredProcedure, SqlConnection connectionString)
        {
            SqlCommand cmd = new SqlCommand(StoredProcedure, connectionString);
            cmd.CommandType = CommandType.StoredProcedure;

            return cmd;
        }

        public static void SqlDtRd(SqlCommand cmd)
        {
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
        }

        public static SqlParameter InVarcharParam(SqlCommand cmd, string parameterName, string parameterValue, bool IsNullable)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.VarChar;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Input;
            param.IsNullable = IsNullable;
            param.Value = parameterValue;
            return cmd.Parameters.Add(param);
        }

        public static SqlParameter InIntParam(SqlCommand cmd, string parameterName, int? parameterValue, bool IsNullable)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.Int;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Input;
            param.IsNullable = IsNullable;
            param.Value = parameterValue;
            return cmd.Parameters.Add(param);
        }

        public static SqlParameter InTextParam(SqlCommand cmd, string parameterName, string parameterValue, bool IsNullable)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.Text;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Input;
            param.IsNullable = IsNullable;
            param.Value = parameterValue;
            return cmd.Parameters.Add(param);
        }



        public static SqlParameter InCharParam(SqlCommand cmd, string parameterName, string parameterValue, bool IsNullable)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.Char;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Input;
            param.IsNullable = IsNullable;
            param.Value = parameterValue;
            return cmd.Parameters.Add(param);
        }

        public static SqlParameter InDecimalParam(SqlCommand cmd, string parameterName, decimal? parameterValue, bool IsNullable)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.Decimal;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Input;
            param.IsNullable = IsNullable;
            param.Value = parameterValue;
            return cmd.Parameters.Add(param);
        }

        public static SqlParameter InTinyIntParam(SqlCommand cmd, string parameterName, int? parameterValue, bool IsNullable)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.TinyInt;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Input;
            param.IsNullable = IsNullable;
            param.Value = parameterValue;
            return cmd.Parameters.Add(param);
        }

        public static SqlParameter InNvarcharParam(SqlCommand cmd, string parameterName, string parameterValue, bool IsNullable)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.NVarChar;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Input;
            param.IsNullable = IsNullable;
            param.Value = parameterValue;
            return cmd.Parameters.Add(param);
        }

        public static SqlParameter InDateTimeParam(SqlCommand cmd, string parameterName, DateTime? parameterValue, bool IsNullable)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.DateTime;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Input;
            param.IsNullable = IsNullable;
            param.Value = parameterValue;
            return cmd.Parameters.Add(param);
        }

        public static SqlParameter InBitParam(SqlCommand cmd, string parameterName, bool? parameterValue, bool IsNullable)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.Bit;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Input;
            param.IsNullable = IsNullable;
            param.Value = parameterValue;
            return cmd.Parameters.Add(param);
        }

        public static SqlParameter OutVarcharParam(SqlCommand cmd, string parameterName)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.VarChar;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Output;
            return cmd.Parameters.Add(param);
        }

        public static SqlParameter OutBitParam(SqlCommand cmd, string parameterName)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.Bit;
            param.Direction = ParameterDirection.Output;
            return cmd.Parameters.Add(param);
        }

        public static SqlParameter OutNVarcharParam(SqlCommand cmd, string parameterName)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.NVarChar;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Output;
            return cmd.Parameters.Add(param);
        }

        public static SqlParameter OutIntParam(SqlCommand cmd, string parameterName)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.SqlDbType = SqlDbType.Int;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Output;
            return cmd.Parameters.Add(param);
        }

        public static void DefaultOutParams(SqlCommand cmd)
        {
            SqlParameter pnTypeResultParam = new SqlParameter();
            pnTypeResultParam.ParameterName = "@pnTypeResult";
            pnTypeResultParam.SqlDbType = SqlDbType.VarChar;
            pnTypeResultParam.Size = int.MaxValue;
            pnTypeResultParam.Direction = ParameterDirection.Output;

            SqlParameter pnCodeResultParam = new SqlParameter();
            pnCodeResultParam.ParameterName = "@pnCodeResult";
            pnCodeResultParam.SqlDbType = SqlDbType.VarChar;
            pnCodeResultParam.Size = int.MaxValue;
            pnCodeResultParam.Direction = ParameterDirection.Output;

            SqlParameter pcResultParam = new SqlParameter();
            pcResultParam.ParameterName = "@pcResult";
            pcResultParam.SqlDbType = SqlDbType.VarChar;
            pcResultParam.Size = int.MaxValue;
            pcResultParam.Direction = ParameterDirection.Output;

            SqlParameter pcMessageParam = new SqlParameter();
            pcMessageParam.ParameterName = "@pcMessage";
            pcMessageParam.SqlDbType = SqlDbType.VarChar;
            pcMessageParam.Size = int.MaxValue;
            pcMessageParam.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(pnTypeResultParam);
            cmd.Parameters.Add(pnCodeResultParam);
            cmd.Parameters.Add(pcResultParam);
            cmd.Parameters.Add(pcMessageParam);
        }

        public static CustomJsonResult DefaultOutParamsValue(SqlCommand cmd)
        {
            CustomJsonResult OutParams = new CustomJsonResult();
            string pnTypeResult = (cmd.Parameters["@pnTypeResult"].Value.ToString() == "null") ? null : cmd.Parameters["@pnTypeResult"].Value.ToString();
            string pnCodeResult = (cmd.Parameters["@pnCodeResult"].Value.ToString() == "null") ? null : cmd.Parameters["@pnCodeResult"].Value.ToString();

            OutParams.typeResult = Convert.ToInt32(pnTypeResult);
            OutParams.codeResult = Convert.ToInt32(pnCodeResult);
            OutParams.result = (cmd.Parameters["@pcResult"].Value.ToString() == "null") ? null : cmd.Parameters["@pcResult"].Value.ToString();
            OutParams.message = (cmd.Parameters["@pcMessage"].Value.ToString() == "null") ? null : cmd.Parameters["@pcMessage"].Value.ToString();

            return OutParams;
        }
    }
}
