using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataEntityLayer.ModelsExtern;

namespace ProyectoNcapas.Views.Account
{
    public class SliderController : Controller
    {
        public ActionResult _Slider()
        {
            int numeroimg;
            var numero = ConfigurationManager.AppSettings["CantImg"];

            if (!string.IsNullOrEmpty(numero))
            {
                int.TryParse(numero, out numeroimg);
            }
            else
            {
                numeroimg = 1;
            }

            var images = Enumerable.Range(1, numeroimg).Select(i => Url.Content(String.Format("~/Content/Image/imgsite/img{0}.jpg", i)));

            return PartialView("~/Views/Slider/_Slider.cshtml", new SliderModel { url = images });
        }
    }
}