using KN_ProyectoClase.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_ProyectoClase.Controllers
{
    public class OfertaController : Controller
    {
        // GET: Oferta
        [HttpGet]
        public ActionResult ConsultarOfertas()
        {
            // EF usando LinkQ
            using (var context = new KN_DBEntities())
            {
                // EF usando SP
                var info = context.ConsultarOfertas().ToList();

                return View(info);
            }
        }

        [HttpGet]
        public ActionResult ConsultarOfertasDisponibles()
        {

            // EF usando LinkQ
            using (var context = new KN_DBEntities())
            {
                // EF usando SP
                var info = context.ConsultarOfertas().ToList();

                return View(info);
            }
        }

        [HttpGet]
        public ActionResult AgregarOferta()
        {

            // EF usando LinkQ
            using (var context = new KN_DBEntities())
            {
                ViewBag.PuestosCombo = context.ConsultarPuestos().ToList();

                return View();
            }
        }

    }
}