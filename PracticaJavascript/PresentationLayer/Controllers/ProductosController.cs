using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinessLayer.Productos;
using DataEntityLayer.Models.Productos;
using DataEntityLayer.Models;
using Newtonsoft.Json;
using System.Net;
using DataAccessLayer.Util;
using DevExtreme.AspNet.Mvc;
      

namespace ProyectoNcapas.Controllers
{
    public class ProductosController : BaseController
    {

        //Instancia de la clase de Pantalla1 de la capa de Negocios
        Productos BusinnesProductos = new Productos();
        Productos productos = new Productos();
        public ActionResult Index()
        {
            return View("~/Views/Productos/Index.cshtml");
        }

        public ActionResult GetProductos(DataSourceLoadOptions loadOptions)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                response = BusinnesProductos.GetProductos(loadOptions);
                if (response.typeResult.Equals(UtilClass.codigoError))
                {
                    HttpContext.Response.Write(response.message);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception e)
            {
                response.typeResult = 1;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return Content(JsonConvert.SerializeObject(response.result), "application/json");
        }

        public ActionResult AbrirPopupAgregarProductos()
        {
            return PartialView("~/Views/Productos/_Formulario.cshtml");
        }

        public ActionResult ObtenerProveedor(DataSourceLoadOptions loadOptions)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                response = BusinnesProductos.ObtenerProveedor(loadOptions);
                if (response.typeResult.Equals(UtilClass.codigoError))
                {
                    HttpContext.Response.Write(response.message);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return Content(JsonConvert.SerializeObject(response.result), "application/json");
        }

        public ActionResult ObtenerCategoria(DataSourceLoadOptions loadOptions)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                response = BusinnesProductos.ObtenerCategoria(loadOptions);
                if (response.typeResult.Equals(UtilClass.codigoError))
                {
                    HttpContext.Response.Write(response.message);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return Content(JsonConvert.SerializeObject(response.result), "application/json");
        }

        public ActionResult AgregarProductos(string values)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                response = BusinnesProductos.AgregarProductos(values);
                response.codeResult = 0;
                response.typeResult = 0;
                response.message = "Producto guardado con éxito.";
            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        public ActionResult ActualizarProductos(string key, string values)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                response = BusinnesProductos.ActualizarProductos(key, values);
                response.codeResult = 0;
                response.typeResult = 0;
                response.message = "Producto editado con éxito.";
            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        public ActionResult EliminarProductos(string key)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                response = BusinnesProductos.EliminarProductos(key);
                response.codeResult = 0;
                response.typeResult = 0;
                response.message = "Producto eliminado con éxito.";
            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

   
       
       public ActionResult AgregarProducto(ProductosModel model)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                response = productos.AgregarProducto(model, UtilClass.tipoOperacionCrear);
                if (response.typeResult.Equals(UtilClass.codigoError))
                {
                    HttpContext.Response.Write(response.message);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception e)
            {
                response.typeResult = 1;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }

            return Content(JsonConvert.SerializeObject(response), "application/json");
       }

        public ActionResult EditarProducto(ProductosModel model)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                response = productos.ActualizarProducto(model, UtilClass.tipoOperacionModificar);
                if (response.typeResult.Equals(UtilClass.codigoError))
                {
                    HttpContext.Response.Write(response.message);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception e)
            {
                response.typeResult = 1;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }

            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        public ActionResult AbrirPopupEditarProductos(ProductosModel model)
        {
            return PartialView("~/Views/Productos/_EditarProducto.cshtml", model);
        }

        public ActionResult EliminarProducto(int IdProducto)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                response = productos.EliminarProducto(IdProducto, UtilClass.tipoOperacionEliminar);
                if (response.typeResult.Equals(UtilClass.codigoError))
                {
                    HttpContext.Response.Write(response.message);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception e)
            {
                response.typeResult = 1;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }

            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        public ActionResult AbrirPopupEliminarProductos(int IdProducto)
        {
            ViewBag.IdProducto = IdProducto;
            return PartialView("~/Views/Productos/_EliminarProducto.cshtml");
        }


        public ActionResult DetallarProducto(int id_producto)
        {
            CustomJsonResult response = new CustomJsonResult();
            ProductosModel ModelProducto= new ProductosModel();
            response = BusinnesProductos.ObtenerDetalleProducto(id_producto);
            ModelProducto = (ProductosModel)response.result;
            ViewBag.Modelo = ModelProducto;
            ViewBag.CerrarDetalle = "CerrarDetalle";
            return PartialView("~/Views/Productos/_DetallarProducto.cshtml");
        }

        public ActionResult CerrarDetalle()
        {
            return PartialView("~/Views/Productos/Index.cshtml");
        }

        public ActionResult ActualizaProducto(ProductosModel model)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                response = productos.ActualizarProducto(model, UtilClass.tipoOperacionModificar);
                if (response.typeResult.Equals(UtilClass.codigoError))
                {
                    HttpContext.Response.Write(response.message);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception e)
            {
                response.typeResult = 1;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }

            return Content(JsonConvert.SerializeObject(response), "application/json");
        }
    }
}
