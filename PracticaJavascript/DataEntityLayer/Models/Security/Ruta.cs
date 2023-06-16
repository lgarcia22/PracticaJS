using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntityLayer.Models
{
    public class Ruta
    {
        public Ruta(String controlador, String accion)
        {
            nombreAccion = accion;
            nombreControlador = controlador;
        }

        public Ruta(String controlador, String accion, Permiso permiso)
        {
            nombreAccion = accion;
            nombreControlador = controlador;
            this.nombrePermiso = permiso;
        }

        private String nombreAccion;
        private String nombreControlador;
        public Permiso nombrePermiso { set; get; }

        public String NombreControlador
        {
            set { this.nombreControlador = value; }
            get { return this.nombreControlador; }
        }

        public String NombreAccion
        {
            set { this.nombreAccion = value; }
            get { return this.nombreAccion; }
        }

        public override bool Equals(object obj)
        {
            Ruta r = (Ruta)obj;

            return this.NombreControlador.Equals(r.NombreControlador) && this.NombreAccion.Equals(r.NombreAccion);
        }
    }
}
