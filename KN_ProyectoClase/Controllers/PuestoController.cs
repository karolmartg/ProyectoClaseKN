using KN_ProyectoClase.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_ProyectoClase.Controllers
{
    public class PuestoController : Controller
    {
        // GET: Puesto
        [HttpGet]
        public ActionResult ConsultarPuestos()
        {
            // EF usando LinkQ
            using (var context = new KN_DBEntities())
            {
                // EF usando SP
                var info = context.ConsultarPuestos().ToList();

                return View(info);
            }
        }
    }
}