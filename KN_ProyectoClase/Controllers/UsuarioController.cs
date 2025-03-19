using KN_ProyectoClase.BD;
using KN_ProyectoClase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_ProyectoClase.Controllers
{
    public class UsuarioController : Controller
    {
        RegistroErrores error = new RegistroErrores();
        Utilitarios util = new Utilitarios();

        // GET: Usuario
        [HttpGet]
        public ActionResult CambiarAcceso()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get CambiarAcceso");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult CambiarAcceso(UsuarioModel model)
        {
            try
            {
                // La contraseña nueva y la de confirmar no coinciden
                if (model.Contrasenna != model.ConfirmarContrasenna)
                {
                    ViewBag.Mensaje = "Debe confirmar su contraseña correctamente.";
                    return View();
                }


                using (var context = new KN_DBEntities())
                {
                    long idSesion = long.Parse(Session["IdUsuario"].ToString());
                    var info = context.Usuario.Where(x => x.Id == idSesion).FirstOrDefault();

                    if (info != null)
                    {
                        // Si la contraseña anterior es diferente a la contraseña de la DB
                        if (model.ContrasennaAnterior != info.Contrasenna)
                        {
                            ViewBag.Mensaje = "Debe confirmar correctamente su contraseña actual.";
                            return View();
                        }


                        info.Contrasenna = model.Contrasenna;
                        context.SaveChanges();

                        string mensaje = $"Hola {info.Nombre}, se ha detectado el cambio de su contraseña.";

                        var notificacion = util.EnviarCorreo(info, mensaje, "Seguridad sistema KN");

                        if (notificacion)
                            return RedirectToAction("Inicio", "Principal");
                    }

                    ViewBag.Mensaje = "Su contraseña no se ha podido actualizar correctamente.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post CambiarAcceso");
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ActualizarDatos()
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    long idSesion = long.Parse(Session["IdUsuario"].ToString());
                    var info = context.Usuario.Where(x => x.Id == idSesion).FirstOrDefault();
                    return View(info);
                }

            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get ActualizarDatos");
                return View("Error");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ActualizarDatos(Usuario model)
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    long idSesion = long.Parse(Session["IdUsuario"].ToString());
                    var info = context.Usuario.Where(x => x.Id == idSesion).FirstOrDefault();

                    // Consultamos si el correo "Nuevo" existe en la base de datos
                    var infoCorreo = context.Usuario.Where((x) => x.Correo == model.Correo
                                                                  && x.Id != idSesion).FirstOrDefault();

                    // Si el correo "Nuevo" ya está registrado, NO debemos dejar actualizar
                    if (infoCorreo == null)
                    {
                        info.Identificacion = model.Identificacion;
                        info.Nombre = model.Nombre;
                        info.Correo = model.Correo;
                        var result = context.SaveChanges();


                        if (result > 0)
                        {
                            Session["NombreUsuario"] = model.Nombre;
                            return RedirectToAction("Inicio", "Principal");
                        }
                    }


                    ViewBag.Mensaje = "Su información no se ha podido actualizar correctamente";
                    return View();

                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post ActualizarDatos");
                return View("Error");

            }
        }

    }
}