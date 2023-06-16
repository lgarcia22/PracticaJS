using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataEntityLayer.Models;
using DataAccessLayer.Util;
using ProyectoNcapas.Filter;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProyectoNcapas.Controllers
{
    //[BaseActionFilter]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            UsuarioSesion usuario = new UsuarioSesion();
            usuario = UtilClass.GetUsuarioSesion();
            return View();
        }
    }
}