using System;
using System.Web;
using DataAccessLayer.Util;
using DataEntityLayer.Models;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Configuration;
using System.Web.Security;
using Newtonsoft.Json;

namespace ProyectoNcapas.Filter
{
    public class BaseActionFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            #region Implementación Local
            UsuarioSesion usuarioSesion = UtilClass.GetUsuarioSesion();
            var UrlAnterior = filterContext.HttpContext.Request.UrlReferrer;

            if (usuarioSesion == null || usuarioSesion.Permisos == null || usuarioSesion.Permisos.Count == 0)
            {
                if (filterContext.HttpContext.Request.HttpMethod != "POST")
                {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "Controller", "Account" }, { "Action", "Login" }, { "returnUrl", filterContext.HttpContext.Request.Url } });
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "Controller", "Account" }, { "Action", "Login" }, { "returnUrl", null } });
                }
            }


            //Se crean un objeto de tipo Permiso para luego crear una ruta con el controlador y la acción actual.
            //Ya que al permiso no se le proporciona los atributos CONTROL y PANTALLA
            //el método TienePermiso() interpreta que se debe de validar con la ruta, puesto que es una validación
            //a nivel de ActionFilter

            //Para conocer más de la lógica de Permisos y Rutas puede ir al archivo:
            //     DataAccessLayer.Util.Security

            // donde hay una inicialización o mapeo de permiso y rutas


            String nombreController = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            String nombreAccion = filterContext.ActionDescriptor.ActionName;

            Permiso permiso = new Permiso();
            permiso.Ruta.Add(new Ruta(nombreController, nombreAccion));



            //Las llamadas asíncronas que no tienen definido en sus parámetros, se asume 
            //que son hechas por librerias de terceros, como DevExpress. Un ejemplo, puede ser
            //hacer una búsqueda en un grid view de devexpress que genera un request asíncrono.
            //Este se debe de dejar pasar sin ninguna restricción

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (usuarioSesion != null && !usuarioSesion.TienePermiso(permiso))
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {{ "Controller", "Inicio" },
                                                    { "Action", "Ajax" }, {"url", UrlAnterior } });
                }
            }


            //Esta validación se hace en cada uno de los llamados a un método de un controlador.
            //El flag filterContext.IsChildAction determina si la acción viene de una vista parcial.
            //Las vistas parciales pueden ser tablas, listados, etc. Ya que las vistas parciales
            //están dentro de las vistas padres, los llamados a la obtención de datos de estas acciones
            //hijas no se toman en cuenta, pues la seguridad se maneja a un nivel de vista y no de vista parcial

            else if (!filterContext.IsChildAction && usuarioSesion != null && !usuarioSesion.TienePermiso(permiso))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {{ "Controller", "Account" },
                                        { "Action", "Index" } });
            }

            base.OnActionExecuting(filterContext);
            #endregion
            
        }
    }
}