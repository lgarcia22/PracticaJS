using BusinessLayer.Menu;
using DataAccessLayer.Util;
using DataAccessLayer.EntityModel;
using DataAccessLayer.Util.Security;
using DataEntityLayer.Models;
using DataEntityLayer.ModelsExtern;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoNcapas.Controllers
{
    public class MenuController : BaseController
    {
        MenuSidebar menuSidebar = new MenuSidebar();
        // GET: Menu
        public ActionResult Index()
        {
            UsuarioSesion sesion = UtilClass.GetUsuarioSesion();



            List<ElementoMenuSidebar> elementosSidebar = new List<ElementoMenuSidebar>();

            if (sesion != null)
            {
                
                CustomJsonResult response = menuSidebar.GetElementoSidebar(sesion.Nombre);
                if (response.typeResult != UtilClass.codigoError)
                {
                    elementosSidebar = JsonConvert.DeserializeObject<List<ElementoMenuSidebar>>(response.result.ToString());

                }
                
            }
            
            string result = JsonConvert.SerializeObject(elementosSidebar.OrderBy(or => or.tituloElementoMenu));
            return Content(result, "application/json");
        }
    }
}