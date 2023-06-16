using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntityLayer.Models.Pantalla1
{
    public class DepartamentosModel
    {
        [Display(Name = "Código Departamento")]
        public int CodigoDepto { get; set; }
        [Display(Name = "Nombre Departamento")]
        public string NombreDepto{ get; set; }

    }
}
