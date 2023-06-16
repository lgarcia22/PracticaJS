using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntityLayer.Models
{
    public class UsuarioSesionAD
    {
        private String nombre;

        public String Nombre
        {
            set { this.nombre = value; }
            get { return this.nombre; }
        }
    }

    public class UsuarioSesion
    {
       
        private String nombre;
        private String contrasenia;
        private List<Permiso> permisos;
        private int codigoUsuario;
        private int codigoOrganigrama;

        public int CodigoUsuario
        {
            set { this.codigoUsuario = value; }
            get { return this.codigoUsuario; }
        }

        public int CodigoOrganigrama
        {
            set { this.codigoOrganigrama = value; }
            get { return this.codigoOrganigrama; }
        }

        public String Nombre
        {
            set { this.nombre = value; }
            get { return this.nombre; }
        }

        public List<Permiso> Permisos
        {
            set { this.permisos = value; }
            get { return this.permisos; }
        }

        public String Contrasenia
        {
            set { this.contrasenia = value; }
            get { return this.contrasenia; }
        }

        public Boolean TienePermiso(Permiso permiso)
        {
            try
            {
                if (permiso == null)
                {
                    return false;
                }
                else
                {
                    return this.permisos.Contains(permiso);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class Organigrama
    {
        public int codigoOrganigrama { get; set; }
        public int codigoUsuario { get; set; }
    }
}
