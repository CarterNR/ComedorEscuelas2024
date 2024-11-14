using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class EscuelaController : Controller
    {
        IEscuelaHelper _escuelaHelper;

        public EscuelaController(IEscuelaHelper escuelaHelper)
        {
            _escuelaHelper = escuelaHelper;
        }
        // GET: EscuelaController
        public ActionResult Index()
        {
            var lista = _escuelaHelper.GetEscuelas();
            return View(lista);
        }

        // GET: EscuelaController/Details/5
        public ActionResult Details(int id)
        {
            var escuela = _escuelaHelper.GetEscuela(id);
            return View(escuela);
        }

        // GET: EscuelaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EscuelaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EscuelaViewModel escuela)
        {
            try
            {
                _escuelaHelper.Add(escuela);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EscuelaController/Edit/5
        public ActionResult Edit(int id)
        {
            var escuela = _escuelaHelper.GetEscuela(id);

            return View(escuela);
        }

        // POST: EscuelaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EscuelaViewModel escuela)
        {
            try
            {
                _escuelaHelper.Update(escuela
                    );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EscuelaController/Delete/5
        public ActionResult Delete(int id)
        {

            var escuela = _escuelaHelper.GetEscuela(id);

            return View(escuela);
        }

        // POST: EscuelaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EscuelaViewModel escuela)
        {
            try
            {
                _escuelaHelper.Delete(escuela.IdEscuela);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
