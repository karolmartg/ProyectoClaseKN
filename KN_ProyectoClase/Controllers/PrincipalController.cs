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
            // Esto es una forma de validar pero no es la más correcta
            /*
            if (string.IsNullOrEmpty(model.Identificacion))
            {
                return View();
            }
            */


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
                var result = context.RegistrarCuenta(model.Identificacion, model.Contrasenna, model.Nombre, model.Correo);

                // EF siempre nos devuelve la cantidad de filas afectadas
                // Si se afectan las filas, debemos tener un resultado >= 1
                if (result > 0) return RedirectToAction("IniciarSesion", "Principal");
                
                // Si no se afectan las filas por algún error, el resultado deberá ser <= 0
                else if (result <= 0)
                {
                    // Devolvemos un mensaje de error
                    ViewBag.Mensaje = "Su información no se ha podido registrar correctamente.";
                    return View();
                }
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
                if (info != null)
                {
                    // Variables de sesión
                    Session["NombreUsuario"] = info.NombreUsuario;
                    Session["NombrePerfilUsuario"] = info.NombrePerfil;
                    Session["IdPerfilUsuario"] = info.idPerfil;

                    return RedirectToAction("Inicio", "Principal");
                }
                else if (info == null)
                {
                    ViewBag.Mensaje = "Su información no se ha podido validar correctamente.";
                    return View();
                }
            }

        
            return View();
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("IniciarSesion", "Principal");
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