using DataAccessLayer.EntityModel;
using DataAccessLayer.ModeloExterno;
using DataAccessLayer.Util;
using DataEntityLayer.Models;
using DataEntityLayer.Models.Productos;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Productos
{
    public class Productos
    {

        

        NORTHWNDEntities db = new NORTHWNDEntities();


        ///<summary>
        /// Método para obtener productos
        ///</summary>
        ///<remarks>
        ///Autor: by lgarcia 13062023
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public CustomJsonResult GetProductos(DataSourceLoadOptions loadOptions)
        {
            CustomJsonResult response = new CustomJsonResult();
            List<ProductosModel> listaProductos = new List<ProductosModel>();
            try
            {
                    listaProductos = db.Products
                    .OrderByDescending(o => o.ProductID)
                    .Select(s => new ProductosModel
                    {
                        productID = s.ProductID,
                        productName = s.ProductName,
                        supplier = (int)s.SupplierID,
                        category = (int)s.CategoryID,
                        quantityPerUnit = s.QuantityPerUnit,
                        unitPrice = (decimal)s.UnitPrice,
                        unitsInStock = (int)s.UnitsInStock,
                        unitsOnOrder = (int)s.UnitsOnOrder,
                        reorderLevel = (int)s.ReorderLevel,
                        discontinued = s.Discontinued,
                        //proveedor = db.Suppliers
                        //.Where(ww => ww.SupplierID == s.SupplierID)
                        //.Select(ss => ss.CompanyName).FirstOrDefault(),
                        //categoria = db.Categories
                        //.Where(ww => ww.CategoryID == s.CategoryID)
                        //.Select(ss => ss.CategoryName).FirstOrDefault()
                    }).ToList();
                response.result = DataSourceLoader.Load(listaProductos, loadOptions);
            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return response;
        }
        
        ///<summary>
        /// Método para obtener proveedor
        ///</summary>
        ///<remarks>
        ///Autor: by lgarcia 15062023
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public CustomJsonResult ObtenerProveedor(DataSourceLoadOptions loadOptions)
        {
            CustomJsonResult response = new CustomJsonResult();
            List<ProveedoresModel> listaProveedor = new List<ProveedoresModel>();
            try
            {
                listaProveedor = db.Suppliers
                .OrderByDescending(o => o.SupplierID)
                .Select(s => new ProveedoresModel
                {
                    SupplierID = (int)s.SupplierID,
                    CompanyName = s.CompanyName,
                }).ToList();
                response.result = DataSourceLoader.Load(listaProveedor, loadOptions);
            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return response;
        }

        ///<summary>
        /// Método para obtener categoria
        ///</summary>
        ///<remarks>
        ///Autor: by lgarcia 15062023
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public CustomJsonResult ObtenerCategoria(DataSourceLoadOptions loadOptions)
        {
            CustomJsonResult response = new CustomJsonResult();
            List<CategoriasModel> listaCategoria = new List<CategoriasModel>();
            try
            {
                listaCategoria = db.Categories
                .OrderByDescending(o => o.CategoryID)
                .Select(s => new CategoriasModel
                {
                    CategoryID = (int)s.CategoryID,
                    CategoryName = s.CategoryName,
                }).ToList();
                response.result = DataSourceLoader.Load(listaCategoria, loadOptions);
            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return response;
        }

        ///<summary>
        /// Método para Crear productos
        ///</summary>
        ///<remarks>
        ///Autor: by lgarcia 13062023
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public CustomJsonResult AgregarProductos(string values)
            {
                CustomJsonResult response = new CustomJsonResult();
                ProductosModel productos = new ProductosModel();
                try
                {
                    Products product = new Products();
                    productos = JsonConvert.DeserializeObject<ProductosModel>(values);

                    product.ProductID = productos.productID;
                    product.ProductName = productos.productName;
                    product.SupplierID = (short?)productos.supplier;
                    product.CategoryID = (short?)productos.category;
                    product.QuantityPerUnit = productos.quantityPerUnit;
                    product.UnitPrice = (short?)productos.unitPrice;
                    product.UnitsInStock = (short?)productos.unitsInStock;
                    product.UnitsOnOrder = (short?)productos.unitsOnOrder;
                    product.ReorderLevel = (short?)productos.reorderLevel;
                    product.Discontinued = productos.discontinued;
                    //category.Picture = productos.Imagen;

                    db.Products.Add(product);
                    db.SaveChanges();//Guarda en la BD

                    response.result = "";
                }
                catch (Exception e)
                {
                    response.typeResult = UtilClass.codigoError;
                    response.message = $"Error: {e.Message}, Fuente: {e.Source}";
                }
                return response;
            }

        ///<summary>
        /// Método para Actualizar Productos
        ///</summary>
        ///<remarks>
        ///Autor: by lgarcia 13062023
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public CustomJsonResult ActualizarProductos(string key, string values)
        {
            CustomJsonResult response = new CustomJsonResult();
            ProductosModel productos = new ProductosModel();
            try
            {

                int codigoproducto = Convert.ToInt32(key);

                Products product = db.Products.Where(w => w.ProductID == codigoproducto).FirstOrDefault();
                productos = JsonConvert.DeserializeObject<ProductosModel>(values);


                if (!string.IsNullOrEmpty(productos.productName))
                {
                    product.ProductName = productos.productName;
                }

                if (productos.supplier != 0)
                {
                    product.SupplierID = productos.supplier;
                }

                if (productos.category != 0)
                {
                    product.CategoryID = productos.category;
                }

                if (!string.IsNullOrEmpty(productos.quantityPerUnit))
                {
                    product.QuantityPerUnit = productos.quantityPerUnit;
                }

                if (productos.unitPrice != 0.000m)
                {
                    product.UnitPrice = productos.unitPrice;
                }

                if (productos.unitsInStock != 0)
                {
                    product.UnitsInStock = (short?)productos.unitsInStock;
                }

                if (productos.unitsOnOrder != 0)
                {
                    product.UnitsOnOrder = (short?)productos.unitsOnOrder;
                }

                if (productos.reorderLevel != 0)
                {
                    product.ReorderLevel = (short?)productos.reorderLevel;
                }

                if (productos.discontinued)
                {
                    product.Discontinued = true;
                }
                else
                {
                    product.Discontinued = false;
               
                }



                db.SaveChanges();

 
                response.typeResult = UtilClass.codigoExitoso;
                response.result = "Ok";
                response.message = "Registro Editado Exitosamente";
            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return response;


        }

        ///<summary>
        /// Método para Eliminar Productos
        ///</summary>
        ///<remarks>
        ///Autor: by lgarcia 13062023
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public CustomJsonResult EliminarProductos(string key)
        {
            CustomJsonResult response = new CustomJsonResult();
            ProductosModel productos = new ProductosModel();
            try
            {
                //JObject keys = JObject.Parse(key);
                int codigoproducto = Convert.ToInt32(key);

                Products product = db.Products
                    .Where(w => w.ProductID == codigoproducto).FirstOrDefault();

                db.Products.Remove(product);
                db.SaveChanges();//Guarda en la BD

                response.result = "";
                response.typeResult = UtilClass.codigoExitoso;
                response.result = "Ok";
                response.message = "Registro eliminado exitosamente";

            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return response;
        }

        ///<summary>
        /// Método para Crear productos Modal
        ///</summary>
        ///<remarks>
        ///Autor: by lgarcia 13062023
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public CustomJsonResult AgregarProducto(ProductosModel model, int TipoOperacion)
        {
            CustomJsonResult response = new CustomJsonResult();

            try
            {
                response = DataProductos.GestionProductos(model, TipoOperacion);

            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return response;
        }

        ///<summary>
        /// Método para actualizar productos
        ///</summary>
        ///<remarks>
        ///Autor: by lgarcia 16062023
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public CustomJsonResult ActualizarProducto(ProductosModel model, int TipoOperacion)
        {
            CustomJsonResult response = new CustomJsonResult();

            try
            {
                response = DataProductos.GestionProductos(model, TipoOperacion);

            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return response;
        }

        ///<summary>
        /// Método para eliminar productos
        ///</summary>
        ///<remarks>
        ///Autor: by lgarcia 13062023
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public CustomJsonResult EliminarProducto(int IdProducto, int TipoOperacion)
        {
            CustomJsonResult response = new CustomJsonResult();

            try
            {
                ProductosModel model = new ProductosModel();
                model.productID = IdProducto;
                response = DataProductos.GestionProductos(model, TipoOperacion);

            }
            catch (Exception e)
            {
                response.typeResult = UtilClass.codigoError;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return response;
        }

        ///<summary>
        /// Método para obtener el detalle un producto
        ///</summary>
        ///<remarks>
        ///Autor: by lgarcia 15062023
        ///</remarks>
        ///<returns>
        /// CustomJsonResult
        /// </returns>
        public CustomJsonResult ObtenerDetalleProducto(int IdProducto)
        {
            CustomJsonResult response = new CustomJsonResult();
            ProductosModel productos = new ProductosModel();

            try
            {
                productos = db.Products
                    .Where(w => w.ProductID == IdProducto)
                    .Select(s => new ProductosModel
                    {
                        productID = s.ProductID,
                        productName = s.ProductName,
                        supplier = (int)s.SupplierID,
                        category = (int)s.CategoryID,
                        unitPrice = (decimal)s.UnitPrice,
                        quantityPerUnit = s.QuantityPerUnit,
                        discontinued = s.Discontinued,
                        unitsOnOrder = (short)s.UnitsOnOrder,
                        unitsInStock = (short)s.UnitsInStock,
                        reorderLevel = (short)s.ReorderLevel,
                    }).FirstOrDefault();
                response.result = productos;
                response.typeResult = UtilClass.codigoExitoso;
                response.message = "Detalle correctamente";
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
