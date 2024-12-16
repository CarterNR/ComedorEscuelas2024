using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Controllers
{
    public class ProveedorController : Controller
    {
        IProveedorHelper _proveedorHelper;
        IEscuelaHelper _escuelaHelper;

        public ProveedorController(IProveedorHelper proveedorHelper, IEscuelaHelper escuelaHelper)
        {
            _proveedorHelper = proveedorHelper;
            _escuelaHelper = escuelaHelper;
        }
        // GET: ProveedorController
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var lista = _proveedorHelper.GetProveedores();
            return View(lista);
        }

        // GET: ProveedorController/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            var proveedor = _proveedorHelper.GetProveedor(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            var escuela = _escuelaHelper.GetEscuela(proveedor.IdEscuela);
            proveedor.NombreEscuela = escuela?.NombreEscuela;

            return View(proveedor);
        }

        // GET: ProveedorController/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new ProveedorViewModel();
            var escuelas = _escuelaHelper.GetEscuelas();
            model.ListaEscuelas = escuelas.Select(e => new SelectListItem
            {
                Value = e.IdEscuela.ToString(),
                Text = e.NombreEscuela
            }).ToList();

            return View(model);
        }

        // POST: ProveedorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProveedorViewModel proveedor)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState es inválido. Detalles:");
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"Clave: {key}, Error: {error.ErrorMessage}");
                    }
                }
                CargarListasDeSeleccion(proveedor);
                return View(proveedor);
            }


            try
            {
                _proveedorHelper.Add(proveedor);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Hubo un error al agregar el proveedor: " + ex.Message);
                CargarListasDeSeleccion(proveedor);
                return View(proveedor);
            }

            return RedirectToAction("Index");
        }

        private void CargarListasDeSeleccion(ProveedorViewModel proveedor)
        {
            var escuelas = _escuelaHelper.GetEscuelas();
            proveedor.ListaEscuelas = escuelas.Select(e => new SelectListItem
            {
                Value = e.IdEscuela.ToString(),
                Text = e.NombreEscuela
            }).ToList();
        }



        // GET: ProveedorController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var proveedor = _proveedorHelper.GetProveedor(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            var escuelas = _escuelaHelper.GetEscuelas();
            proveedor.ListaEscuelas = escuelas
                .Select(e => new SelectListItem
                {
                    Value = e.IdEscuela.ToString(),
                    Text = e.NombreEscuela
                }).ToList();

            return View(proveedor);
        }

        // POST: ProveedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProveedorViewModel proveedor)
        {
            try
            {
                var productoOriginal = _proveedorHelper.GetProveedor(proveedor.IdProveedor);
                if (productoOriginal == null)
                {
                    return NotFound();
                }
                

                var proveedorActualizado = _proveedorHelper.Update(proveedor);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(proveedor);
            }
        }

        // GET: ProveedorController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var proveedor = _proveedorHelper.GetProveedor(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            var escuela = _escuelaHelper.GetEscuela(proveedor.IdEscuela);

            proveedor.NombreEscuela = escuela?.NombreEscuela;

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
            catch (Exception)
            {
                TempData["Script"] = "alert('No se puede eliminar el proveedor porque tiene productos asociados.');";
                return View(_proveedorHelper.GetProveedor(proveedor.IdProveedor));
            }
        }
    }
}
