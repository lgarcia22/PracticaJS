using System;
using System.ComponentModel.DataAnnotations;

namespace DataEntityLayer.Models.Pantalla1
{
    public class Pantalla1Model
    {
        [Display(Name = "Código Empleado")]
        public int codigoEmpleado { get; set; }

        [Required(ErrorMessage = "Ingrese su Nombre")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese su Apellido")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese su Edad")]
        [Display(Name = "Edad")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "Ingrese su Identidad")]
        [Display(Name = "Identidad")]
        public string Identidad { get; set; }

        [Display(Name = "Cónyuge")]
        public string Conyuge { get; set; }

        [Required(ErrorMessage = "Ingrese la Fecha Inicial ")]
        [Display(Name = "Fecha Inicial")]
        public DateTime Fecha_Inicial { get; set; }

        [Required(ErrorMessage = "Ingrese la Fecha Final ")]
        [Display(Name = "Fecha Final")]
        public DateTime Fecha_Final { get; set; }

        [Required(ErrorMessage = "Ingrese jefe ")]
        [Display(Name = "Jefe")]
        public int? codigoJefe { get; set; }

        [Required(ErrorMessage = "Ingrese jefe 2 ")]
        [Display(Name = "Jefe 2")]
        public int? codigoJefe2 { get; set; }

        [Required(ErrorMessage = "Ingrese la Fecha Minima")]
        [Display(Name = "Fecha Minima")]
        public DateTime Fecha_Minima { get; set; }


        [Required(ErrorMessage = "Ingrese el Departamento")]
        [Display(Name = "Departamento")]
        public int CodigoDepto { get; set; }

        [Required(ErrorMessage = "Ingrese el Municipio")]
        [Display(Name = "Municipio")]
        public int CodigoMuni { get; set; }
    }
}
