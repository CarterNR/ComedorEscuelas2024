using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using FrontEnd.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    [RoleAuth(1)] // Solo rol Administrador
    public class RolController : Controller
    {
        IRolHelper _rolHelper;

        public RolController(IRolHelper rolHelper)
        {
            _rolHelper = rolHelper;
        }
        // GET: RolController
        public ActionResult Index()
        {
            var lista = _rolHelper.GetRoles();
            return View(lista);
        }

        // GET: RolController/Details/5
        public ActionResult Details(int id)
        {
            var rol = _rolHelper.GetRol(id);
            return View(rol);
        }

        // GET: RolController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RolViewModel rol)
        {
            try
            {
                _rolHelper.Add(rol);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolController/Edit/5
        public ActionResult Edit(int id)
        {
            var rol = _rolHelper.GetRol(id);

            return View(rol);
        }

        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RolViewModel rol)
        {
            try
            {
                _rolHelper.Update(rol
                    );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolController/Delete/5
        public ActionResult Delete(int id)
        {

            var rol = _rolHelper.GetRol(id);

            return View(rol);
        }

        // POST: RolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(RolViewModel rol)
        {
            try
            {
                _rolHelper.Delete(rol.IdRol);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
