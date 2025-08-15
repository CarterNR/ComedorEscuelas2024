using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using FrontEnd.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Controllers
{
    [RoleAuth(1, 2)] // Rol Administrador y Escuela
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
       // [Authorize(Roles = "Admin")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 8)
        {
            var lista = _proveedorHelper.GetProveedores();
            var escuelas = _escuelaHelper.GetEscuelas();

            foreach (var item in lista)
            {
                item.NombreEscuela = escuelas.FirstOrDefault(e => e.IdEscuela == item.IdEscuela)?.NombreEscuela;
            }


            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                lista = lista.Where(p =>
                    (p.NombreProveedor != null && p.NombreProveedor.ToLower().Contains(searchString)) ||
                    (p.Telefono != null && p.Telefono.ToLower().Contains(searchString)) ||
                    (p.CorreoElectronico != null && p.CorreoElectronico.ToLower().Contains(searchString)) ||
                    (p.Direccion != null && p.Direccion.ToLower().Contains(searchString)) ||
                    (p.Estado.ToString().ToLower().Contains(searchString)) ||
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

        // GET: ProveedorController/Details/5
       // [Authorize(Roles = "Admin")]
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
      //  [Authorize(Roles = "Admin")]
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
      //  [Authorize(Roles = "Admin")]
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
     //   [Authorize(Roles = "Admin")]
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
