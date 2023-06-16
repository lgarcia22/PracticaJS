using System.Web;
using System.Web.Optimization;

namespace HIS
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scriptBundle = new ScriptBundle("~/Scripts/bundle");
            var styleBundle = new StyleBundle("~/Content/bundle");

            styleBundle.Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                       "~/Content/Iconos.css",
                      "~/Content/template.css",
                      "~/Content/themify-icons.css",
                      "~/Content/FontAwesome/fontawesome.css",
                      "~/Content/Confirm/jquery-confirm.min.css",
                      "~/Content/FontAwesome/fontawesome-all.css");

            scriptBundle.Include(
                        //Se importan los scripts de funciones reutilizables
                        "~/Scripts/funcionesReutilizables/Formularios/Formulario.js",
                        "~/Scripts/funcionesReutilizables/Grids/Grid.js",
                        "~/Scripts/funcionesReutilizables/Popups/Copia.js",

                        //Se importan los scripts cdns
                        "~/Scripts/cdns/exceljs.min.js",
                        "~/Scripts/cdns/FileSaver.min.js",
                        "~/Scripts/cdns/jspdf.plugin.autotable.min.js",
                        "~/Scripts/cdns/jspdf.umd.min.js",
                        "~/Scripts/cdns/polyfill.min.js",


                        //Se importan los scripts de funciones reutilizables
                        "~/Scripts/jquery-3.6.0.js",
                        "~/Scripts/jquery-confirm.min.js",
                        "~/Scripts/Site.js",
                        "~/Scripts/bootstrap.js");

                        

            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);
        }
    }
}
