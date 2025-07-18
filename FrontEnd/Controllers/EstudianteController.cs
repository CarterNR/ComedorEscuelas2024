using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FrontEnd.Controllers
{
    public class EstudianteController : Controller
    {
        IEstudianteHelper _estudianteHelper;
        IEscuelaHelper _escuelaHelper;

        public EstudianteController(IEstudianteHelper estudianteHelper, IEscuelaHelper escuelaHelper)
        {
            _estudianteHelper = estudianteHelper;
            _escuelaHelper = escuelaHelper;
        }

        // GET: UsuarioController
        public IActionResult Index()
        {
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            var estudiante = _estudianteHelper.GetEstudiantePorUsuario(idUsuario.Value);


            if (estudiante == null)
            {
                ViewBag.Error = "Estudiante no encontrado.";
                return RedirectToAction("Login", "Usuario");
            }

            return View(estudiante);
        }

        // GET: UsuarioController
        public ActionResult Listado()
        {
            var lista = _estudianteHelper.GetEstudiantes();
            return View(lista);
        }

        public IActionResult VerPorCedula(string cedula)
        {
            var estudiante = _estudianteHelper.GetEstudiantePorCedula(cedula);

            if (estudiante == null)
            {
                ViewBag.Error = "Estudiante no encontrado.";
                return View("Error");  // O como manejes los mensajes de error
            }

            return View("Index", estudiante);  // Vista para mostrar detalles
        }


        // GET: EstudianteController/Details/5
        public ActionResult Details(int id)
        {
            var estudiante = _estudianteHelper.GetEstudiante(id);

            var escuela = _escuelaHelper.GetEscuela(estudiante.IdEscuela);
            estudiante.NombreEscuela = escuela?.NombreEscuela;

            return View(estudiante);
        }


        // GET: EstudianteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstudianteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EstudianteViewModel estudiante)
        {
            try
            {
                _estudianteHelper.Add(estudiante);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: EstudianteController/Edit/5
        public ActionResult Edit(int id)
        {
            var estudiante = _estudianteHelper.GetEstudiante(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            var escuelas = _escuelaHelper.GetEscuelas();
            estudiante.ListaEscuelas = escuelas
                .Select(e => new SelectListItem
                {
                    Value = e.IdEscuela.ToString(),
                    Text = e.NombreEscuela
                }).ToList();

            return View(estudiante);
        }

        // POST: EstudianteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EstudianteViewModel estudiante)
        {
            try
            {
                _estudianteHelper.Update(estudiante
                    );
                return RedirectToAction(nameof(Listado));
            }
            catch
            {
                return View();
            }
        }



        // GET: EstudianteController/Delete/5
        public ActionResult Delete(int id)
        {
            var estudiante = _estudianteHelper.GetEstudiante(id);  // Obtener estudiante

            if (estudiante == null || estudiante.IdEstudiante == 0)
            {
                TempData["Script"] = "alert('Estudiante no encontrado o no válido.');";
                return RedirectToAction(nameof(Index));  // Redirigir si no se encuentra el estudiante
            }

            // Puedes agregar aquí la información adicional que desees mostrar
            return View(estudiante);  // Devolver la vista con la información del estudiante
        }


        // POST: EstudianteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EstudianteViewModel estudiante)
        {
            if (estudiante == null || estudiante.IdEstudiante == 0)
            {
                // Si el estudiante no es válido, redirigir o mostrar mensaje de error
                TempData["Error"] = "Estudiante no encontrado.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                // Llamamos al método de eliminación
                _estudianteHelper.Delete(estudiante.IdEstudiante);

                // Redirigir a la lista de estudiantes después de la eliminación
                TempData["Success"] = "Estudiante eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Si ocurre un error, muestra un mensaje detallado
                TempData["Error"] = $"Error al eliminar el estudiante: {ex.Message}";
                return View(estudiante);
            }
        }

    }
}
