using DataAccessLayer.EntityModel;
using DataEntityLayer.Models;
using DataEntityLayer.Models.Pantalla1;
using DevExpress.DataAccess.Native.Web;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProyectoNcapas.Controllers
{
    public class Pantalla1Controller : BaseController
    {
        public ActionResult Index()
        {
            return View("~/Views/Pantalla1/Index.cshtml");
        }

        public ActionResult GetPersonas(DataSourceLoadOptions loadOptions)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                List<Pantalla1Model> ListaPersonas = new List<Pantalla1Model>
                {
                    new Pantalla1Model
                    {
                        codigoEmpleado = 1,
                        Nombre = "José",
                        Apellido = "Chavez",
                        Edad = 30,
                        Identidad = "0501199709267",
                        Conyuge = "Conyuge",
                        Fecha_Inicial = DateTime.Now, 
                        Fecha_Final = DateTime.Now.AddDays(1)
                    },
                    new Pantalla1Model
                    {
                        codigoEmpleado = 2,
                        Nombre = "Moisés",
                        Apellido = "Inestroza",
                        Edad = 31,
                        Identidad = "0501199709888",
                        Conyuge = "Conyuge",
                        Fecha_Inicial = DateTime.Now,
                        Fecha_Final = DateTime.Now.AddDays(4)
                    },
                    new Pantalla1Model
                    {
                        codigoEmpleado = 3,
                        Nombre = "Arnold",
                        Apellido = "Colindres",
                        Edad = 26,
                        Identidad = "0501199709999",
                        Conyuge = "Conyuge",
                        Fecha_Inicial = DateTime.Now,
                        Fecha_Final = DateTime.Now.AddDays(3)
                    },
                    new Pantalla1Model
                    {   
                        codigoEmpleado = 4,
                        Nombre = "Alejandro",
                        Apellido = "Contreras",
                        Edad = 21,
                        Identidad = "0501199709444",
                        Conyuge = "Conyuge",
                        Fecha_Inicial = DateTime.Now,
                        Fecha_Final = DateTime.Now.AddDays(2)
                    },
                    new Pantalla1Model
                    {
                        codigoEmpleado = 5,
                        Nombre = "Eduardo",
                        Apellido = "Castro",
                        Edad = 25,
                        Identidad = "0501199709222",
                        Conyuge = "Conyuge",
                        Fecha_Inicial = DateTime.Now,
                        Fecha_Final = DateTime.Now.AddDays(3)
                    },
                };

                response.result = DataSourceLoader.Load(ListaPersonas, loadOptions);                
            }
            catch (Exception e)
            {
                response.typeResult = 1;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return Content(JsonConvert.SerializeObject(response.result), "application/json");
        }

        public ActionResult FormAgregar()
        {
            return PartialView("~/Views/Pantalla1/_Formulario.cshtml");
        }

        public ActionResult GuardarPersona(Pantalla1Model modelo)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                response.codeResult = 0;
                response.typeResult = 0;
                response.message = "Persona guardada con éxito.";
            }
            catch (Exception e)
            {
                response.typeResult = 1;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return Content(JsonConvert.SerializeObject(response), "application/json");
        }

        public ActionResult ObtenerJefe(DataSourceLoadOptions loadOptions, int codJefe)
        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                List<Pantalla1Model> ListaPersonas = new List<Pantalla1Model>
                {
                    new Pantalla1Model
                    {
                        codigoEmpleado = 1,
                        Nombre = "José",
                        Apellido = "Chavez",
                        Edad = 30,
                        Identidad = "0501199709267",
                        Conyuge = "Conyuge",
                        Fecha_Inicial = DateTime.Now,
                        Fecha_Final = DateTime.Now.AddDays(1)
                    },
                    new Pantalla1Model
                    {
                        codigoEmpleado = 2,
                        Nombre = "Moisés",
                        Apellido = "Inestroza",
                        Edad = 31,
                        Identidad = "0501199709888",
                        Conyuge = "Conyuge",
                        Fecha_Inicial = DateTime.Now,
                        Fecha_Final = DateTime.Now.AddDays(4)
                    },
                    new Pantalla1Model
                    {
                        codigoEmpleado = 3,
                        Nombre = "Arnold",
                        Apellido = "Colindres",
                        Edad = 26,
                        Identidad = "0501199709999",
                        Conyuge = "Conyuge",
                        Fecha_Inicial = DateTime.Now,
                        Fecha_Final = DateTime.Now.AddDays(3)
                    },
                    new Pantalla1Model
                    {
                        codigoEmpleado = 4,
                        Nombre = "Alejandro",
                        Apellido = "Contreras",
                        Edad = 21,
                        Identidad = "0501199709444",
                        Conyuge = "Conyuge",
                        Fecha_Inicial = DateTime.Now,
                        Fecha_Final = DateTime.Now.AddDays(2)
                    },
                    new Pantalla1Model
                    {
                        codigoEmpleado = 5,
                        Nombre = "Eduardo",
                        Apellido = "Castro",
                        Edad = 25,
                        Identidad = "0501199709222",
                        Conyuge = "Conyuge",
                        Fecha_Inicial = DateTime.Now,
                        Fecha_Final = DateTime.Now.AddDays(3)
                    },
                    new Pantalla1Model
                    {
                        codigoEmpleado = 5,
                        Nombre = "asdaSDAS",
                        Apellido = "Castro",
                        Edad = 25,
                        Identidad = "0501199709222",
                        Conyuge = "Conyuge",
                        Fecha_Inicial = DateTime.Now,
                        Fecha_Final = DateTime.Now.AddDays(3)
                    },
                };
                ListaPersonas = ListaPersonas.Where(w => w.codigoEmpleado == codJefe).ToList();
                response.result = DataSourceLoader.Load(ListaPersonas, loadOptions);
            }
            catch (Exception e)
            {
                response.typeResult = 1;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return Content(JsonConvert.SerializeObject(response.result), "application/json");
        }

        public ActionResult GetDepartamento(DataSourceLoadOptions loadOptions)//Ejercicio #4

        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                List<DepartamentosModel> ListaDepartamentos = new List<DepartamentosModel>
                {
                    new DepartamentosModel
                    {
                        CodigoDepto = 1,
                        NombreDepto = " Francisco Morazán",

                    },
                    new DepartamentosModel
                    {
                        CodigoDepto = 2,
                        NombreDepto = "Cortés",
                    },
                    new DepartamentosModel
                    {
                        CodigoDepto = 3,
                        NombreDepto = " Comayagua",

                    },

                };

                response.result = DataSourceLoader.Load(ListaDepartamentos, loadOptions);
            }
            catch (Exception e)
            {
                response.typeResult = 1;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return Content(JsonConvert.SerializeObject(response.result), "application/json");
        }


        public ActionResult GetMunicipio(DataSourceLoadOptions loadOptions, int codigoDepto)//Ejercicio #5

        {
            CustomJsonResult response = new CustomJsonResult();
            try
            {
                List<MunicipiosModel> ListaMunicipios = new List<MunicipiosModel>
                {
                    new MunicipiosModel
                    {
                        CodigoMuni = 1,
                        NombreMuni= "Distrito Central",
                        CodigoDepto = 1,
                    },

                    new MunicipiosModel
                    {
                        CodigoMuni = 2,
                        NombreMuni= "Valle de Ángeles",
                        CodigoDepto = 1,
                    },

                    new MunicipiosModel
                    {
                        CodigoMuni = 3,
                        NombreMuni= "Santa Lucía",
                        CodigoDepto = 1,
                    },

                    new MunicipiosModel
                    {
                        CodigoMuni = 4,
                        NombreMuni= "Omoa",
                        CodigoDepto = 2,
                    },

                    new MunicipiosModel
                    {
                        CodigoMuni = 5,
                        NombreMuni= "Puerto Cortés",
                        CodigoDepto = 2,
                    },

                     new MunicipiosModel
                    {
                        CodigoMuni = 6,
                        NombreMuni= "San Pedro Sula",
                        CodigoDepto = 2,
                    },

                      new MunicipiosModel
                    {
                        CodigoMuni = 7,
                        NombreMuni= "Comayagua",
                        CodigoDepto = 3,
                    },

                    new MunicipiosModel
                    {
                        CodigoMuni = 8,
                        NombreMuni= "siguatepeque",
                        CodigoDepto = 3,
                    },

                     new MunicipiosModel
                    {
                        CodigoMuni = 9,
                        NombreMuni= "Taulabe",
                        CodigoDepto = 3,
                    },
                };

                ListaMunicipios = ListaMunicipios.Where(w => w.CodigoDepto == codigoDepto).ToList();
                response.result = DataSourceLoader.Load(ListaMunicipios, loadOptions);

            }
            catch (Exception e)
            {
                response.typeResult = 1;
                response.message = $"Error: {e.Message}, Fuente: {e.Source}";
            }
            return Content(JsonConvert.SerializeObject(response.result), "application/json");
        }
    }
}