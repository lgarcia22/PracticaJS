using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntityLayer.Models.Productos
{
    public class ProductosModel
    {

		public int productID { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre del Producto")]
        public string productName { get; set; }

        [Required(ErrorMessage = "Seleccione el Proveedor")]
        public int? supplier { get; set; }

        [Required(ErrorMessage = "Seleccione la Categoría")]
        public int? category { get; set; }

        [Required(ErrorMessage = "Ingrese la Cantidad por Unidad")]
        public string quantityPerUnit { get; set; }

        [Required(ErrorMessage = "Ingrese el Precio Unitario")]
        public decimal? unitPrice { get; set; }

        [Required(ErrorMessage = "Ingrese las Unidades en Existencia")]
        public int? unitsInStock { get; set; }

        [Required(ErrorMessage = "Ingrese las Unidades en Pedido")]
        public int? unitsOnOrder { get; set; }
		public int? reorderLevel { get; set; }
		public bool discontinued { get; set; }
		
    }
}
