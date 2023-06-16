using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntityLayer.Models.Productos
{
    public class ProveedoresModel
    {
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
    }
}
