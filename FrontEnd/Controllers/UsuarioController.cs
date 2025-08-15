using FrontEnd.ApiModels;
using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using FrontEnd.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace FrontEnd.Controllers
{
    [RoleAuth(1)] // Solo rol Administrador
    public class UsuarioController : Controller
    {
        IUsuarioHelper _usuarioHelper;
        IRolHelper _rolHelper;
        ITipoCedulaHelper _tipocedulaHelper;
        IEscuelaHelper _escuelaHelper;
        IEstudianteHelper _estudianteHelper;

        public UsuarioController(IUsuarioHelper usuarioHelper, IEscuelaHelper escuelaHelper, IRolHelper rolHelper, ITipoCedulaHelper tipocedulaHelper, IEstudianteHelper estudianteHelper)
        {
            _usuarioHelper = usuarioHelper;
            _escuelaHelper = escuelaHelper;
            _rolHelper = rolHelper;
            _tipocedulaHelper = tipocedulaHelper;
            _estudianteHelper = estudianteHelper;

        }
        // GET: UsuarioController
        public IActionResult Index(string searchString, int page = 1, int pageSize = 8)
        {
            var lista = _usuarioHelper.GetUsuarios();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                lista = lista.Where(u =>
                    (u.NombreUsuario != null && u.NombreUsuario.ToLower().Contains(searchString)) ||
                    (u.NombreCompleto != null && u.NombreCompleto.ToLower().Contains(searchString)) ||
                    (u.Cedula != null && u.Cedula.ToLower().Contains(searchString)) ||
                    (u.CorreoElectronico != null && u.CorreoElectronico.ToLower().Contains(searchString)) ||
                    (u.NombreEscuela != null && u.NombreEscuela.ToLower().Contains(searchString))
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




        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            var usuario = _usuarioHelper.GetUsuario(id);

            var escuela = _escuelaHelper.GetEscuela(usuario.IdEscuela);
            usuario.NombreEscuela = escuela?.NombreEscuela;

            var tipocedula = _tipocedulaHelper.GetTipoCedula(usuario.IdTipoCedula);
            usuario.NombreTipoCedula = tipocedula?.NombreTipoCedula;

            var rol = _rolHelper.GetRol(usuario.IdRol);
            usuario.NombreRol = rol?.NombreRol;

            return View(usuario);
        }

        // GET: UsuarioController/Create
        public IActionResult Create()
        {


            var viewModel = new UsuarioViewModel
            {
                ListaRoles = _rolHelper.GetRoles()
                    .Select(r => new SelectListItem
                    {
                        Value = r.IdRol.ToString(),
                        Text = r.NombreRol
                    }),

                ListaEscuelas = _escuelaHelper.GetEscuelas()
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEscuela.ToString(),
                        Text = e.NombreEscuela
                    }),

                ListaTiposCedulas = _tipocedulaHelper.GetTiposCedulas()
                    .Select(t => new SelectListItem
                    {
                        Value = t.IdTipoCedula.ToString(),
                        Text = t.NombreTipoCedula
                    })


            };

            return View(viewModel);

        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    usuario.NombreCompleto = usuario.NombreCompleto.ToUpper();
                    usuario.NombreUsuario = usuario.NombreUsuario.ToLower();

                    usuario.NombreUsuario = GenerarNombreUsuarioUnico(usuario.NombreUsuario.ToLower());

                    // Verificar si ya existe un usuario con la misma cédula
                    var usuarioCedula = _usuarioHelper.GetUsuarios()
                        .FirstOrDefault(u => u.Cedula == usuario.Cedula);

                    if (usuarioCedula != null)
                    {
                        ModelState.AddModelError("Cedula", "Ya existe un usuario con esta cédula.");
                        RepoblarListas(usuario);
                        return View(usuario);
                    }

                    // Verificar si ya existe un usuario con el mismo correo
                    var usuarioCorreo = _usuarioHelper.GetUsuarios()
                        .FirstOrDefault(u => u.CorreoElectronico.ToLower() == usuario.CorreoElectronico.ToLower());

                    if (usuarioCorreo != null)
                    {
                        ModelState.AddModelError("CorreoElectronico", "Ya existe un usuario con este correo electrónico.");
                        RepoblarListas(usuario);
                        return View(usuario);
                    }

                    var usuarioCreado = _usuarioHelper.Add(usuario);
                    _usuarioHelper.Add(usuario);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            // Repoblar listas
            usuario.ListaRoles = _rolHelper.GetRoles()
                .Select(r => new SelectListItem { Value = r.IdRol.ToString(), Text = r.NombreRol });
            usuario.ListaEscuelas = _escuelaHelper.GetEscuelas()
                .Select(e => new SelectListItem { Value = e.IdEscuela.ToString(), Text = e.NombreEscuela });
            usuario.ListaTiposCedulas = _tipocedulaHelper.GetTiposCedulas()
                .Select(t => new SelectListItem { Value = t.IdTipoCedula.ToString(), Text = t.NombreTipoCedula });

            return View(usuario);
        }

        // Método auxiliar para repoblar las listas
        private void RepoblarListas(UsuarioViewModel usuario)
        {
            usuario.ListaRoles = _rolHelper.GetRoles()
                .Select(r => new SelectListItem { Value = r.IdRol.ToString(), Text = r.NombreRol });
            usuario.ListaEscuelas = _escuelaHelper.GetEscuelas()
                .Select(e => new SelectListItem { Value = e.IdEscuela.ToString(), Text = e.NombreEscuela });
            usuario.ListaTiposCedulas = _tipocedulaHelper.GetTiposCedulas()
                .Select(t => new SelectListItem { Value = t.IdTipoCedula.ToString(), Text = t.NombreTipoCedula });
        }

        private string GenerarNombreUsuarioUnico(string nombreUsuarioBase)
        {
            var usuarios = _usuarioHelper.GetUsuarios();
            string nombreUsuarioOriginal = nombreUsuarioBase.ToLower();
            string nombreUsuarioUnico = nombreUsuarioOriginal;
            int contador = 1;

            // Verificar si el nombre original ya existe
            while (usuarios.Any(u => u.NombreUsuario.ToLower() == nombreUsuarioUnico))
            {
                nombreUsuarioUnico = $"{nombreUsuarioOriginal}{contador}";
                contador++;
            }

            return nombreUsuarioUnico;
        }





        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            var usuario = _usuarioHelper.GetUsuario(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var escuelas = _escuelaHelper.GetEscuelas();
            usuario.ListaEscuelas = escuelas
                .Select(e => new SelectListItem
                {
                    Value = e.IdEscuela.ToString(),
                    Text = e.NombreEscuela
                }).ToList();


            var tiposcedulas = _tipocedulaHelper.GetTiposCedulas();
            usuario.ListaTiposCedulas = tiposcedulas
                .Select(t => new SelectListItem
                {
                    Value = t.IdTipoCedula.ToString(),
                    Text = t.NombreTipoCedula
                }).ToList();

            var roles = _rolHelper.GetRoles();
            usuario.ListaRoles = roles
                .Select(r => new SelectListItem
                {
                    Value = r.IdRol.ToString(),
                    Text = r.NombreRol
                }).ToList();

            return View(usuario);
        }

        // POST: UsuarioController/Edit/5
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioViewModel usuario)
        {
            try
            {
                var usuarioOriginal = _usuarioHelper.GetUsuario(usuario.IdUsuario);


                bool cambioARolEstudiante = usuarioOriginal.IdRol != usuario.IdRol && usuario.IdRol == 3;
                bool dejoDeSerEstudiante = usuarioOriginal.IdRol == 3 && usuario.IdRol != 3;
                bool noCambioClave = usuarioOriginal.Clave == usuario.Clave;
                bool noCambioUsuario = usuarioOriginal.NombreUsuario == usuario.NombreUsuario;

                var claveEncryptada = usuario.Clave;

                if (noCambioClave != true)
                {
                    claveEncryptada = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);
                }

                if(noCambioUsuario == false)
                {
                    usuario.NombreUsuario = GenerarNombreUsuarioUnico(usuario.NombreUsuario.ToLower());
                }
                
                


                _usuarioHelper.Update(usuario);


                // ✅ Aquí se llama a editar estudiante si el usuario tiene rol de estudiante
                if (cambioARolEstudiante == false && dejoDeSerEstudiante == false && usuario.IdRol == 3) // Suponiendo que 2 es el ID del rol "Estudiante"
                {
                    var estudiante = _estudianteHelper.GetEstudiantePorUsuario(usuario.IdUsuario);
                    if (estudiante != null)
                    {
                        // Actualizás los campos que se editaron desde el formulario usuario
                        estudiante.Cedula = usuario.Cedula;
                        estudiante.Nombre = usuario.NombreCompleto.ToUpper();
                        estudiante.IdEscuela = usuario.IdEscuela;
                        estudiante.IdUsuario = usuario.IdUsuario;
                        estudiante.NombreUsuario = usuario.NombreUsuario;
                        estudiante.Clave = claveEncryptada;
                        estudiante.TiquetesRestantes = estudiante.TiquetesRestantes;

                        _estudianteHelper.Update(estudiante);
                    }
                }

                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(usuario);
            }
        }

        






        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            var usuario = _usuarioHelper.GetUsuario(id);

            if (usuario == null || usuario.IdUsuario == 0)
            {
                TempData["Script"] = "alert('Usuario no encontrado o no válido.');";
                return RedirectToAction(nameof(Index));
            }

            var escuela = _escuelaHelper.GetEscuela(usuario.IdEscuela);
            var rol = _rolHelper.GetRol(usuario.IdRol);

            usuario.NombreEscuela = escuela?.NombreEscuela ?? "Sin escuela";
            usuario.NombreRol = rol?.NombreRol ?? "Sin rol";

            return View(usuario);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UsuarioViewModel usuario)
        {
            try
            {
                _usuarioHelper.Delete(usuario.IdUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Desactivar(int id)
        {
            var usuario = _usuarioHelper.GetUsuario(id);

            if (usuario == null || usuario.IdUsuario == 0)
            {
                TempData["Script"] = "alert('Usuario no encontrado o no válido.');";
                return RedirectToAction(nameof(Index));
            }

            if (usuario.Estado == false)
            {
                TempData["Script"] = "alert('El usuario ya está inactivo.');";
                return RedirectToAction(nameof(Index));
            }

            var escuela = _escuelaHelper.GetEscuela(usuario.IdEscuela);
            var rol = _rolHelper.GetRol(usuario.IdRol);
            var tipocedula = _tipocedulaHelper.GetTipoCedula(usuario.IdTipoCedula);

            usuario.NombreEscuela = escuela?.NombreEscuela ?? "Sin escuela";
            usuario.NombreRol = rol?.NombreRol ?? "Sin rol";
            usuario.NombreTipoCedula = tipocedula?.NombreTipoCedula ?? "Sin tipo";

            return View(usuario);
        }

        // POST: UsuarioController/Desactivar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Desactivar(UsuarioViewModel usuario)
        {
            try
            {
                _usuarioHelper.Desactivar(usuario.IdUsuario);
                TempData["Mensaje"] = "Usuario desactivado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al desactivar el usuario: " + ex.Message;
                return View(usuario);
            }
        }
    }
}