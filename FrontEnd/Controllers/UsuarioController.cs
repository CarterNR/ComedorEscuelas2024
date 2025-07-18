using FrontEnd.ApiModels;
using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Controllers
{
    public class UsuarioController : Controller
    {
        IUsuarioHelper _usuarioHelper;
        IRolHelper _rolHelper;
        ITipoCedulaHelper _tipocedulaHelper;
        IEscuelaHelper _escuelaHelper;

        public UsuarioController(IUsuarioHelper usuarioHelper, IEscuelaHelper escuelaHelper, IRolHelper rolHelper, ITipoCedulaHelper tipocedulaHelper)
        {
            _usuarioHelper = usuarioHelper;
            _escuelaHelper = escuelaHelper;
            _rolHelper = rolHelper;
            _tipocedulaHelper = tipocedulaHelper;

        }
        // GET: UsuarioController
        public ActionResult Index()
        {
            var lista = _usuarioHelper.GetUsuarios();
            return View(lista);
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
                _usuarioHelper.Update(usuario
                    );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
    }
}