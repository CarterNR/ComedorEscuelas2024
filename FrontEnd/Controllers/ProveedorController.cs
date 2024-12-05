using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class ProveedorController : Controller
    {
        IProveedorHelper _proveedorHelper;

        public ProveedorController(IProveedorHelper proveedorHelper)
        {
            _proveedorHelper = proveedorHelper;
        }
        // GET: ProveedorController
        public ActionResult Index()
        {
            var lista = _proveedorHelper.GetProveedores();
            return View(lista);
        }

        // GET: ProveedorController/Details/5
        public ActionResult Details(int id)
        {
            var proveedor = _proveedorHelper.GetProveedor(id);
            return View(proveedor);
        }

        // GET: ProveedorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProveedorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProveedorViewModel proveedor)
        {
            try
            {
                _proveedorHelper.Add(proveedor);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProveedorController/Edit/5
        public ActionResult Edit(int id)
        {
            var proveedor = _proveedorHelper.GetProveedor(id);

            return View(proveedor);
        }

        // POST: ProveedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProveedorViewModel proveedor)
        {
            try
            {
                _proveedorHelper.Update(proveedor
                    );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProveedorController/Delete/5
        public ActionResult Delete(int id)
        {

            var proveedor = _proveedorHelper.GetProveedor(id);

            return View(proveedor);
        }

        // POST: ProveedorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProveedorViewModel proveedor)
        {
            try
            {
                _proveedorHelper.Delete(proveedor.IdProveedor);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
