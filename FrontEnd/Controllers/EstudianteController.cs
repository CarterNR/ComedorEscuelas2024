using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using FrontEnd.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;

namespace FrontEnd.Controllers
{
    public class EstudianteController : Controller
    {
        IEstudianteHelper _estudianteHelper;
        IEscuelaHelper _escuelaHelper;
        IUsuarioHelper _usuarioHelper;



        public EstudianteController(IEstudianteHelper estudianteHelper, IEscuelaHelper escuelaHelper, IUsuarioHelper usuarioHelper)
        {
            _estudianteHelper = estudianteHelper;
            _escuelaHelper = escuelaHelper;
            _usuarioHelper = usuarioHelper;
        }

        // GET: UsuarioController - Solo estudiantes pueden acceder
        [RoleAuth(3)] // Solo rol estudiante
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

        // GET: UsuarioController - Solo admin puede ver listado
        [RoleAuth(1)] // Solo rol Administrador
        public ActionResult Listado(string searchString, int page = 1, int pageSize = 8)
        {
            var lista = _estudianteHelper.GetEstudiantes();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                lista = lista.Where(p =>
                    (p.NombreUsuario != null && p.NombreUsuario.ToLower().Contains(searchString)) ||
                    (p.Nombre != null && p.Nombre.ToLower().Contains(searchString)) ||
                    (p.Cedula != null && p.Cedula.ToLower().Contains(searchString)) ||
                    (p.NombreEscuela != null && p.NombreEscuela.ToLower().Contains(searchString))
                ).ToList();
            }

            var totalItems = lista.Count();
            var items = lista.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.SearchString = searchString;

            int TotalPages = ViewBag.TotalPages;


            int totalRegistros = lista.Count();
            var datosPagina = lista
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();



            ViewBag.TotalRegistros = totalRegistros;
            ViewBag.Mostrando = datosPagina.Count();


            ViewBag.PaginaActual = page;
            ViewBag.TotalPaginas = TotalPages;
            ViewBag.TotalRegistros = totalRegistros;
            ViewBag.TerminoBusqueda = searchString;


            ViewBag.Mostrando = datosPagina.Count();



            return View(items);

        }

        [RoleAuth(3)] // Solo rol estudiante
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


        // GET: EstudianteController/Details/5 - Solo admin
        [RoleAuth(1)] // Solo rol Administrador
        public ActionResult Details(int id)
        {
            var estudiante = _estudianteHelper.GetEstudiante(id);

            var escuela = _escuelaHelper.GetEscuela(estudiante.IdEscuela);
            estudiante.NombreEscuela = escuela?.NombreEscuela;

            return View(estudiante);
        }


        // GET: EstudianteController/Create - Solo admin
        [RoleAuth(1)] // Solo rol Administrador
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstudianteController/Create - Solo admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleAuth(1)] // Solo rol Administrador
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


        // TU MÉTODO EDIT EXISTENTE (mantenerlo como está)
        // GET: EstudianteController/Edit/5 - Solo admin
        [RoleAuth(1)] // Solo rol Administrador
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

        // NUEVO MÉTODO: Editar estudiante por IdUsuario
        // GET: EstudianteController/EditByUsuario/5 - Solo admin
        [RoleAuth(1)] // Solo rol Administrador
        public ActionResult EditByUsuario(int idUsuario)
        {
            var estudiante = _estudianteHelper.GetEstudiantePorUsuario(idUsuario);
            if (estudiante == null)
            {
                return NotFound("No se encontró el estudiante asociado a este usuario.");
            }

            // Cargar las escuelas para el dropdown
            var escuelas = _escuelaHelper.GetEscuelas();
            estudiante.ListaEscuelas = escuelas
                .Select(e => new SelectListItem
                {
                    Value = e.IdEscuela.ToString(),
                    Text = e.NombreEscuela,
                    Selected = e.IdEscuela == estudiante.IdEscuela
                }).ToList();

            // Indicar que venimos desde el controlador de Usuario
            ViewBag.FromUsuario = true;
            ViewBag.IdUsuario = idUsuario;
            ViewBag.Title = "Editar Estudiante";

            return View("Edit", estudiante); // Reutiliza la vista Edit existente
        }

        // MODIFICAR TU MÉTODO POST EXISTENTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleAuth(1)] // Solo rol Administrador
        public ActionResult Edit(EstudianteViewModel estudiante)
        {
            try
            {
                // Verificar que el estudiante existe
                var estudianteOriginal = _estudianteHelper.GetEstudiantePorUsuario(estudiante.IdUsuario);
                if (estudianteOriginal == null)
                {
                    return NotFound("No se encontró el estudiante a actualizar.");
                }
                estudiante.Nombre.ToUpper();

                var usuario = _usuarioHelper.GetUsuario(estudiante.IdUsuario);

                if (usuario != null)
                {
                    usuario.Cedula = estudiante.Cedula;
                    usuario.NombreCompleto = estudiante.Nombre.ToUpper();
                    usuario.IdEscuela = estudiante.IdEscuela;
                    usuario.IdUsuario = estudiante.IdUsuario;
                    usuario.NombreUsuario = estudiante.NombreUsuario;
                    usuario.Clave = estudiante.Clave;


                    _usuarioHelper.Update(usuario);
                }


                // Actualizar el estudiante
                var estudianteActualizado = _estudianteHelper.Update(estudiante);

                // Verificar si venimos desde el controlador de Usuario
                if (Request.Form["FromUsuario"] == "true")
                {
                    TempData["SuccessMessage"] = "Estudiante actualizado correctamente.";
                    return RedirectToAction("Index", "Usuario");
                }

                TempData["SuccessMessage"] = "Estudiante actualizado correctamente.";
                return RedirectToAction(nameof(Listado));
            }
            catch (Exception ex)
            {
                // En caso de error, recargar las listas y mostrar la vista con error
                var escuelas = _escuelaHelper.GetEscuelas();
                estudiante.ListaEscuelas = escuelas
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEscuela.ToString(),
                        Text = e.NombreEscuela,
                        Selected = e.IdEscuela == estudiante.IdEscuela
                    }).ToList();

                ModelState.AddModelError("", "Ocurrió un error al actualizar el estudiante: " + ex.Message);
                return View(estudiante);
            }
        }














        // GET: EstudianteController/Delete/5 - Solo admin
        [RoleAuth(1)] // Solo rol Administrador
        public ActionResult Delete(int id)
        {
            var estudiante = _estudianteHelper.GetEstudiante(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            // Puedes agregar aquí la información adicional que desees mostrar
            return View(estudiante);  // Devolver la vista con la información del estudiante
        }


        // POST: EstudianteController/Delete/5 - Solo admin
        [HttpPost, ActionName("Delete")]
        [RoleAuth(1)] // Solo rol Administrador
        public IActionResult DeleteConfirmed(int id, int idusuario)
        {
            try
            {
                _estudianteHelper.Delete(id);
                _usuarioHelper.Desactivar(idusuario);
                return RedirectToAction(nameof(Listado));
            }
            catch (Exception ex)
            {
                return View("Delete", _estudianteHelper.GetEstudiante(id));
            }
        }

    }
}
