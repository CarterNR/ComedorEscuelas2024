using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class UsuarioController : Controller
    {
        IUsuarioHelper _usuarioHelper;

        public UsuarioController(IUsuarioHelper usuarioHelper)
        {
            _usuarioHelper = usuarioHelper;
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
            return View(usuario);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioViewModel usuario)
        {
            try
            {
                _usuarioHelper.Add(usuario);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            var usuario = _usuarioHelper.GetUsuario(id);

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
