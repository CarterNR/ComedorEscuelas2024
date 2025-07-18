using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class TipoCedulaController : Controller
    {
        ITipoCedulaHelper _tipocedulaHelper;

        public TipoCedulaController(ITipoCedulaHelper tipocedulaHelper)
        {
            _tipocedulaHelper = tipocedulaHelper;
        }
        // GET: TipoCedulaController
        public ActionResult Index()
        {
            var lista = _tipocedulaHelper.GetTiposCedulas();
            return View(lista);
        }

        // GET: TipoCedulaController/Details/5
        public ActionResult Details(int id)
        {
            var tipocedula = _tipocedulaHelper.GetTipoCedula(id);
            return View(tipocedula);
        }

        // GET: TipoCedulaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoCedulaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoCedulaViewModel tipocedula)
        {
            try
            {
                _tipocedulaHelper.Add(tipocedula);

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
            var tipocedula = _tipocedulaHelper.GetTipoCedula(id);

            return View(tipocedula);
        }

        // POST: TipoCedulaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoCedulaViewModel tipocedula)
        {
            try
            {
                _tipocedulaHelper.Update(tipocedula
                    );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoCedulaController/Delete/5
        public ActionResult Delete(int id)
        {

            var tipocedula = _tipocedulaHelper.GetTipoCedula(id);

            return View(tipocedula);
        }

        // POST: TipoCedulaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TipoCedulaViewModel tipocedula)
        {
            try
            {
                _tipocedulaHelper.Delete(tipocedula.IdTipoCedula);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
