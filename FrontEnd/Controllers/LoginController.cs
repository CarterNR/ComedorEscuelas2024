using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using FrontEnd.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioHelper _usuarioHelper;
        private readonly IEstudianteHelper _estudianteHelper;

        public LoginController(IUsuarioHelper usuarioHelper, IEstudianteHelper estudianteHelper)
        {
            _usuarioHelper = usuarioHelper;
            _estudianteHelper = estudianteHelper;   
        }

        // Mostrar el formulario de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Procesar el login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = _usuarioHelper.Autenticar(model.NombreUsuario, model.Clave);

            if (usuario != null)
            {
                HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
                HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario);
                HttpContext.Session.SetInt32("IdRol", usuario.IdRol);

                if (usuario.IdRol == 1) // Administrador
                    return RedirectToAction("Index", "Home");

                if (usuario.IdRol == 2) // Escuela
                    return RedirectToAction("Index", "ProductoDia");

                if (usuario.IdRol == 3) // Estudiante
                {
                    var estudiante = _estudianteHelper.GetEstudiantePorUsuario(usuario.IdUsuario);

                    if (estudiante != null)
                    {
                        if (DateTime.Now.DayOfWeek == DayOfWeek.Monday && estudiante.TiquetesRestantes != 5)
                        {
                            estudiante.TiquetesRestantes = 5;
                            estudiante.FechaUltimoRebajo = DateTime.Now.Date;
                            _estudianteHelper.Update(estudiante);
                        }
                        else
                        {
                            DateTime ultimaFecha = estudiante.FechaUltimoRebajo ?? DateTime.Now.Date;
                            if (ultimaFecha.Date < DateTime.Now.Date && estudiante.TiquetesRestantes > 0)
                            {
                                estudiante.TiquetesRestantes -= 1;
                                estudiante.FechaUltimoRebajo = DateTime.Now.Date;
                                _estudianteHelper.Update(estudiante);
                            }
                        }

                        return RedirectToAction("Index", "Estudiante", new { id = estudiante.IdEstudiante });
                    }
                    ViewBag.Error = "No se pudo cargar el estudiante.";
                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Credenciales inválidas.";
            return View(model);
        }


        [HttpPost]
        [HttpGet]
        public IActionResult Logout()
        {
            // Limpiar toda la sesión
            HttpContext.Session.Clear();
            
            // Opcional: También limpiar cookies si se están usando
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            
            // Redirigir al login
            return RedirectToAction("Login", "Login");
        }
    }
}
