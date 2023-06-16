using System;
using DataEntityLayer.Models;
using System.Web;
using DataAccessLayer.ModeloExterno;
using DataAccessLayer.Util;

namespace BusinessLayer.Account
{
    public class Account
    {
        ///<summary>
        /// Método de enlace con la capa DataAccessLayer para validar un usuario
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

            try
            {
                response = DataAccount.ValidarUsuario(model, model2, ip, navegador);

            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError; 
                response.message= $"Error: {e.Message}, Fuente: {e.Source}";
            }

            return response;
        }


        ///<summary>
        /// Método de enlace con la capa DataAccessLayer para resetear contraseña del usuario.
        ///</summary>
        ///<remarks>
        ///Autor: by acolindres 20220317
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public static CustomJsonResult ResetUsuarioPass(Reset model, HttpRequestBase model2)
        {
            CustomJsonResult response = new CustomJsonResult();
            

            try
            {
                DataAccount.ResetUsuarioPass(model, model2);

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
