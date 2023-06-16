using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntityLayer.Models
{
    public class Permiso
    {
        public Permiso()
        {
            this.Ruta = new List<Ruta>();
        }

        public Permiso(String pantalla, String control)
        {
            Pantalla = pantalla;
            Control = control;

            this.Ruta = new List<Ruta>();
        }

        private String pantalla;
        private String control;

        private List<Ruta> ruta;


        public String Pantalla
        {
            set { this.pantalla = value; }
            get { return this.pantalla; }
        }

        public String Control
        {
            set { this.control = value; }
            get { return this.control; }
        }

        public List<Ruta> Ruta
        {
            set { this.ruta = value; }
            get { return this.ruta; }
        }

        public override bool Equals(object obj)
        {
            Permiso permiso = (Permiso)obj;

            if (permiso.control == null || permiso.pantalla == null)
            {
                foreach (Ruta r in permiso.Ruta)
                {
                    if (this.Ruta.Contains(r))
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return (this.Pantalla.Equals(permiso.Pantalla) && this.control.Equals(permiso.control));
            }
        }
    }

    public class ControlAccion
    {
        public string controlador { get; set; }
        public string accion { get; set; }
    }

    public class Control
    {
        public string nombreControl { get; set; }
        public List<ControlAccion> accion { get; set; }
    }

    public class VentanaControlAccion
    {
        public string nombreVentana { get; set; }
        public List<Control> control { get; set; }
    }
}
