using BusinessLayer.Util;
using DataAccessLayer.ModeloExterno;
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
using DAL = DataAccessLayer.Util;

namespace BusinessLayer.Menu
{
    public class MenuSidebar
    {
        public MenuSidebar() { }

        ///<summary>
        /// Método enlace para obtener elementos del menu sidebar desde la base de datos
        ///</summary>
        ///<remarks>
        ///Autor: by acolindres 20220317
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public  CustomJsonResult GetElementoSidebar(string usuario)
        {
            CustomJsonResult response = new CustomJsonResult();

            try
            {
                response = DataMenuSidebar.GetElementoSidebar(usuario);
            }
            catch (Exception e)
            {
                response.typeResult = DAL.UtilClass.codigoError; ;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }

            return response;
        }
    }
}
