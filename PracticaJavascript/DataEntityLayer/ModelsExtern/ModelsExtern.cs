using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataEntityLayer.ModelsExtern
{

    public class SeguridadContexto
    {
        public int codigoUsuario { get; set; }
        public string nombreVentana { get; set; }
        public string nombreControl { get; set; }
        public string contextoUno { get; set; }
        public string valorUno { get; set; }
        public string contextoDos { get; set; }
        public string valorDos { get; set; }
        public string contextoTres { get; set; }
        public string valorTres { get; set; }

        public SeguridadContexto() { }

        public SeguridadContexto(int intCodigoUsuario, string strNombreVentana, string strNombreControl, string strContextoUno, string strValorUno)
        {
            codigoUsuario = intCodigoUsuario;
            nombreVentana = strNombreVentana;
            nombreControl = strNombreControl;
            contextoUno = strContextoUno;
            valorUno = strValorUno;

        }
    }

    public class SliderModel
    {
        [Display(Name = "Dirección Imagen")]
        public IEnumerable<string> url { get; set; }
    }

    public class TipoArchivo
    {
        public string extension { get; set; }
        public string mediaType { get; set; }
        public string descripcion { get; set; }
        public string visualizacion { get; set; }
    }

    public class SubElemento
    {
        public int? idElementoMenu { get; set; }
        public string controladorElementoMenu { get; set; }
        public string accionElementoMenu { get; set; }
        public string tituloElementoMenu { get; set; }
        public string iconoElementoMenu { get; set; }
        public string color { get; set; }
        public int? tipoUrl { get; set; }
        public string url { get; set; }
    }

    public class ElementoMenuSidebar
    {
        public int idElementoMenu { get; set; }
        public string controladorElementoMenu { get; set; }
        public string accionElementoMenu { get; set; }
        public string tituloElementoMenu { get; set; }
        public string iconoElementoMenu { get; set; }
        public string color { get; set; }
        public int tipoUrl { get; set; }
        public string url { get; set; }
        public List<SubElemento> SubElemento { get; set; }
        public int orden { get; set; }
    }
    public class ControlUsuario
    {
        public string nombreControl { get; set; }
        public string nombreVentana { get; set; }
    }

    public class ControladorAccion
    {
        public string controlador { get; set; }
        public string accion { get; set; }
    }

    public class VentanaControlUsuario
    {
        public string nombreVentana { get; set; }
        public string nombreControl { get; set; }
        public List<ControladorAccion> controladorAccion { get; set; }
    }
}
