using BL= BusinessLayer.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using reCAPTCHA.MVC;
using DataEntityLayer.Models;
using BusinessLayer.Account;
using DataAccessLayer.Util;
using DataAccessLayer.Util.Security;

namespace ProyectoNcapas.Controllers
{

    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        //Deploy DEV
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            TempData["ErrorLogin"] = "No data";
            if (!string.IsNullOrEmpty(returnUrl))
            {
                var returnPath = new Uri(returnUrl).AbsolutePath;

                if (Url.IsLocalUrl(returnPath))
                {

                    ViewBag.ReturnURL = returnUrl;
                }
            }
            return this.View();
        }

        
        [HttpPost]
        [CaptchaValidator]
        public ActionResult Login(Login model, string url, bool captchaValid)
        {
            try
            {
                //string Controllers = ConfigurationManager.AppSettings["IniUrl"];
                TempData["ErrorLogin"] = "No data";
                var responseLogin = new CustomJsonResult();
                
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                if (!captchaValid)
                {
                    TempData["ErrorLogin"] = $"Error: Captcha invalido ";
                    return this.View(model);
                }

                var browser = System.Web.HttpContext.Current.Request.Browser;

                return RedirectToAction("Index", "Productos");
                responseLogin = Account.ValidarUsuario(model, Request, BL.UtilClass.GetIp(), browser.ToString());


                if (responseLogin.typeResult!=UtilClass.codigoExitoso)
                {
                    System.Web.HttpContext.Current.Session["USUARIO_SESION"] = null;
                    Alert(responseLogin.message, NotificationType.error);
                    TempData["ErrorLogin"] = responseLogin.message;
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    return this.View(model);
                }
                
                
                UsuarioSesion usuario = new UsuarioSesion();
                usuario.Nombre = model.usuario;
                List<Organigrama> organigrama = JsonConvert.DeserializeObject<List<Organigrama>>(responseLogin.result.ToString());
                usuario.CodigoOrganigrama = organigrama[0].codigoOrganigrama;
                usuario.CodigoUsuario = organigrama[0].codigoUsuario;

                //Validar El Usuario y extraer la instancia
                var response = Security.ObtenerPermisoUsuario(usuario.Nombre.ToUpper());

                var mensajeObtenerPermisos = response.GetType().GetProperty("message").GetValue(response, null).ToString();
                if (!string.IsNullOrEmpty(mensajeObtenerPermisos))
                {
                    TempData["message"] = mensajeObtenerPermisos;
                    return RedirectToAction("ErrorUserPage");
                }
                usuario.Permisos = (List<Permiso>)response.GetType().GetProperty("permiso").GetValue(response, null);

                usuario.Contrasenia = UtilClass.EncriptarPassword(model.contrasena);
               


                BL.UtilClass.SetVariableSesion(usuario, "USUARIO_SESION");
                if (usuario.Permisos != null && usuario.Permisos.Count > 0)
                {
                    if (string.IsNullOrEmpty(url))
                    {
                        TempData["ErrorLogin"] = "No data";
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        TempData["ErrorLogin"] = "No data";
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                else
                {
                    System.Web.HttpContext.Current.Session["USUARIO_SESION"] = null;
                    Alert("Ocurrió un error al obtener los permisos.", NotificationType.error);
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["ErrorLogin"] = $"Error: {e.Message}, Fuente: {e.Source}";
                FormsAuthentication.SignOut();
                Session.Abandon();
                return this.View(model);
            }
        }

        public ActionResult ObtenerPermisos(string url)
        {
            UsuarioSesion usuario = new UsuarioSesion();
            usuario =UtilClass.GetUsuarioSesion();


            if (string.IsNullOrEmpty(usuario.Nombre))
            {
                TempData["message"] = "Problemas con el nombre de usuario";
                return RedirectToAction("ErrorUserPage");
            }
            //obtener la instancia 
            var response = Security.ObtenerPermisoUsuario(usuario.Nombre.ToUpper());
            var mensajeObtenerPermisos = response.GetType().GetProperty("message").GetValue(response, null).ToString();
            if (!string.IsNullOrEmpty(mensajeObtenerPermisos))
            {
                TempData["message"] = mensajeObtenerPermisos;
                return RedirectToAction("ErrorUserPage");
            }
            usuario.Permisos = (List<Permiso>)response.GetType().GetProperty("permiso").GetValue(response, null);
            BL.UtilClass.SetVariableSesion(usuario, "USUARIO_SESION");
            if (usuario.Permisos != null && usuario.Permisos.Count > 0)
            {
                if (string.IsNullOrEmpty(url))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return Redirect(url);
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                LogOff();
                TempData["message"] = "No tiene permisos para entrar al aplicativo";
                return RedirectToAction("ErrorUserPage");
            }
        }

        public ActionResult ErrorUserPage()
        {
            //Session.Abandon();
            BL.UtilClass.SetVariableSesion(new UsuarioSesion(), "USUARIO_SESSION");
            return View();
        }

        public ActionResult LogOff()
        {

            //Deploy DEV
            FormsAuthentication.SignOut();
            Session.Abandon();
            System.Web.HttpContext.Current.Session.Abandon();
            System.Web.HttpContext.Current.Session["USUARIO_SESION"] = null;
            //return this.RedirectToAction("Login", "Account");
            return View();
        }

        public ActionResult TimeOutSession()
        {
            object response;
            if (BL.UtilClass.IsAuthenticated())
            {
                response = new { isSessionActive = true };
            }
            else
            {
                response = new { isSessionActive = false };
            }
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        public ActionResult RenewSession()
        {
            HttpCookie authCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            object response;
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                FormsAuthentication.RenewTicketIfOld(authTicket);
                response = new { renewSessionTicketState = true };
            }
            else
            {
                response = new { renewSessionTicketState = true };
            }
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        [HttpPost]
        public ActionResult ResetUsuarioPass(Reset model)
        {
            UsuarioSesion usuario = new UsuarioSesion();
            usuario = UtilClass.GetUsuarioSesion();

            var responseLogin = new CustomJsonResult();
            try
            {
                if (usuario.Contrasenia == model.contrasenaActual)
                {
                    responseLogin = Account.ResetUsuarioPass(model, Request);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "La contraseña actual ingresada no coincide con la contraseña guardada");
                }

                if (!string.IsNullOrEmpty(responseLogin.message))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, responseLogin.message);
                }
                else
                {
                    usuario.Contrasenia = model.nuevaContrasena;
                    BL.UtilClass.SetVariableSesion(usuario, "USUARIO_SESION");
                }
            }
            catch (Exception e)
            {
                responseLogin.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return Content(JsonConvert.SerializeObject(responseLogin.result), "application/json");

        }
    }

}