using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataEntityLayer.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Ingrese su Usuario")]
        [Display(Name = "Nombre de Usuario")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "Ingrese su Contraseña")]
        [Display(Name = "Contraseña")]
        public string contrasena { get; set; }

    }

    public class Reset
    {
        [Required(ErrorMessage = "Ingrese su Usuario")]
        [Display(Name = "Contraseña Actual")]
        public string contrasenaActual { get; set; }

        [Required(ErrorMessage = "Ingrese su Contraseña")]
        [Display(Name = "Nueva Contraseña")]
        public string nuevaContrasena { get; set; }

        [Required(ErrorMessage = "Ingrese su Contraseña")]
        [Display(Name = "Ingrese nuevamente su Contraseña")]
        [Compare("ContrasenaActual", ErrorMessage = "'Nueva Contraseña' y 'Confirmar Contraseña' no Coinciden.")]
        public string validaContrasena { get; set; }

    }


}
