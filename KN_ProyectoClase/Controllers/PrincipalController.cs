using KN_ProyectoClase.BD;
using KN_ProyectoClase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_ProyectoClase.Controllers
{
    public class PrincipalController : Controller
    {
        // GET entrar a una vista
        [HttpGet]
        public ActionResult RegistrarCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCuenta(UsuarioModel model)
        {
            using (var context = new KN_DBEntities())
            {
            // EF usando LinkQ
            /*
                Usuario tabla = new Usuario();
                tabla.Identificacion = model.Identificacion;
                tabla.Contrasenna = model.Contrasenna;
                tabla.Nombre = model.Correo;
                tabla.Estado = true;
                tabla.idPerfil = 2;

                context.Usuario.Add(tabla);
                context.SaveChanges();
            */
                // EF usando Procedimientos Almacenados
                context.RegistrarCuenta(model.Identificacion, model.Contrasenna, model.Nombre, model.Correo);

            }

            return View();
        }

        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(UsuarioModel model)
        {
            //EF usando LinkQ
            using (var context = new KN_DBEntities())
            {
                /*
                var info = context.Usuario.Where(x => x.Identificacion == model.Identificacion
                                                 && x.Contrasenna == model.Contrasenna
                                                 && x.Estado == true).FirstOrDefault();
                */

                // EF usando SP
                var info = context.IniciarSesion(model.Identificacion, model.Contrasenna).FirstOrDefault();

                // Si encuentra algo 
                if (info != null) return RedirectToAction("Inicio", "Principal"); 
            }

        
            return View();
        }


        [HttpGet]
        public ActionResult Inicio()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RecuperarContrasenna()
        {
            return View();
        }

    }
}