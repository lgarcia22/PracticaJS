using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Util;
using DataEntityLayer.Models;
using System.Globalization;
using System.Threading;

namespace ProyectoNcapas.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected UsuarioSesion UsuarioSesion { set; get; }

        public static CustomJsonResult CJsonResult { set; get; }

        public BaseController()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");

            CJsonResult = new CustomJsonResult();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        public void Alert(string message, NotificationType notificationType, int displayTime = 4000)
        {
            string notificationColor = "default";
            switch (notificationType)
            {
                case NotificationType.error:
                    {
                        notificationColor = "error";
                        break;
                    }
                case NotificationType.success:
                    {
                        notificationColor = "success";
                        break;
                    }
                case NotificationType.warning:
                    {
                        notificationColor = "warning";
                        break;
                    }
                case NotificationType.info:
                    {
                        notificationColor = "info";
                        break;
                    }
            }

            TempData[UtilClass.alertNotification] = "Iniciar";
            TempData["Message"] = message;
            TempData["Type"] = notificationColor;
            TempData["displayTime"] = displayTime;
        }
    }

    public enum NotificationType
    {
        error,
        success,
        warning,
        info
    }
}
