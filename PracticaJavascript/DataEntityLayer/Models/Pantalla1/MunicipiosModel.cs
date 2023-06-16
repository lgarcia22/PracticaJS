using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntityLayer.Models.Pantalla1
{
    public class MunicipiosModel
    {
        [Display(Name = "Código Municipio")]
        public int CodigoMuni{ get; set; }
        [Display(Name = "Código Departamento")]
        public int CodigoDepto { get; set; }
        [Display(Name = "Nombre Municipio")]
        public string NombreMuni { get; set; }
    }
}
