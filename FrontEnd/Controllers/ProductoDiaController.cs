using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;

namespace FrontEnd.Controllers
{
    public class ProductoDiaController : Controller
    {
        IProductoDiaHelper _productodiaHelper;
        IProductoHelper _productoHelper;
        IEscuelaHelper _escuelaHelper;



        public ProductoDiaController(IProductoDiaHelper productodiaHelper, IProductoHelper productoHelper, IEscuelaHelper escuelaHelper)
        {
            _productodiaHelper = productodiaHelper; 
            _productoHelper = productoHelper;
            _escuelaHelper = escuelaHelper;
        }
        // GET: ProductoDiaController

        // [Authorize(Roles = "Admin, Producto")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 8)
        {
            // Obtener datos
            var listaProductoDia = _productodiaHelper.GetProductosDia();
            var productos = _productoHelper.GetProductos();
            var escuelas = _escuelaHelper.GetEscuelas(); // Si usás escuelas también

            // Enriquecer con nombre del producto y escuela
            foreach (var item in listaProductoDia)
            {
                var producto = productos.FirstOrDefault(p => p.IdProducto == item.IdProducto);
                if (producto != null)
                {
                    item.NombreProducto = producto.NombreProducto;
                }

                var escuela = escuelas.FirstOrDefault(e => e.IdEscuela == item.IdEscuela);
                if (escuela != null)
                {
                    item.NombreEscuela = escuela.NombreEscuela;
                }
            }

            // Filtrar por fecha de hoy
            var hoy = DateTime.Now.Date;
            var listaFiltrada = listaProductoDia
                .Where(p => p.Fecha.HasValue && p.Fecha.Value.Date == hoy)
                .ToList();

            // Búsqueda
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                listaFiltrada = listaFiltrada.Where(p =>
                    (p.NombreProducto != null && p.NombreProducto.ToLower().Contains(searchString)) ||
                    (p.Cantidad.HasValue  && p.Cantidad.Value.ToString().Contains(searchString)) ||
                    (p.Fecha.HasValue && p.Fecha.Value.ToString("yyyy-MM-dd").Contains(searchString)) ||
                    (p.NombreEscuela != null && p.NombreEscuela.ToLower().Contains(searchString)) ||
                    (p.Estado.ToString().ToLower().Contains(searchString))
                ).ToList();
            }

            var totalItems = listaFiltrada.Count();
            var items = listaFiltrada.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.SearchString = searchString;

            int TotalPages = ViewBag.TotalPages;


            int totalRegistros = listaFiltrada.Count();
            var datosPagina = listaFiltrada
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




        public ActionResult ListaProductosDiaCompletos(string searchString, int page = 1, int pageSize = 8)
        {
            int tamanioPagina = 8;

            // Obtener datos
            var listaProductoDia = _productodiaHelper.GetProductosDia();
            var productos = _productoHelper.GetProductos();
            var escuelas = _escuelaHelper.GetEscuelas(); // Si usás escuelas también

            // Enriquecer con nombre del producto y escuela
            foreach (var item in listaProductoDia)
            {
                var producto = productos.FirstOrDefault(p => p.IdProducto == item.IdProducto);
                if (producto != null)
                {
                    item.NombreProducto = producto.NombreProducto;
                }

                var escuela = escuelas.FirstOrDefault(e => e.IdEscuela == item.IdEscuela);
                if (escuela != null)
                {
                    item.NombreEscuela = escuela.NombreEscuela;
                }
            }

            // Filtrar solo los productos agregados hoy
            var hoy = DateTime.Now.Date; // Solo la fecha, sin hora
            var listaFiltrada = listaProductoDia
                .Where(p => p.Fecha.HasValue && p.Fecha.Value.Date != hoy)
                .ToList();

            // Búsqueda
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                listaFiltrada = listaFiltrada.Where(p =>
                    (p.NombreProducto != null && p.NombreProducto.ToLower().Contains(searchString)) ||
                    (p.Cantidad.HasValue && p.Cantidad.Value.ToString().Contains(searchString)) ||
                    (p.Fecha.HasValue && p.Fecha.Value.ToString("yyyy-MM-dd").Contains(searchString)) ||
                    (p.NombreEscuela != null && p.NombreEscuela.ToLower().Contains(searchString)) ||
                    (p.Estado.ToString().ToLower().Contains(searchString))
                ).ToList();
            }
            var totalItems = listaFiltrada.Count();
            var items = listaFiltrada.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.SearchString = searchString;

            int TotalPages = ViewBag.TotalPages;


            int totalRegistros = listaFiltrada.Count();
            var datosPagina = listaFiltrada
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

        //  [Authorize(Roles = "Admin, Producto")]
        // GET: ProductoDiaController/Details/5
        public ActionResult Details(int id)
        {
            var productodia = _productodiaHelper.GetProductoDia(id);

            if (productodia == null)
            {
                return NotFound(); 
            }

            var producto = _productoHelper.GetProducto(productodia.IdProducto);

            productodia.NombreProducto = producto?.NombreProducto;

            var escuela = _escuelaHelper.GetEscuela(productodia.IdEscuela);

            productodia.NombreEscuela = escuela?.NombreEscuela;

            return View(productodia);  
        }

       // [Authorize(Roles = "Admin, Producto")]
        // GET: ProductoDiaController/Create
        public IActionResult Create()
        {
            var model = new ProductoDiaViewModel();

            var productos = _productoHelper.GetProductos(); 

            model.ListaProductos = productos.Select(p => new SelectListItem
            {
                Value = p.IdProducto.ToString(),
                Text = p.NombreProducto
            }).ToList();

            var escuelas = _escuelaHelper.GetEscuelas();

            model.ListaEscuelas = escuelas.Select(e => new SelectListItem
            {
                Value = e.IdEscuela.ToString(),
                Text = e.NombreEscuela
            }).ToList();

            return View(model);
        }



        // POST: ProductoDiaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductoDiaViewModel productodia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Obtener el producto original para verificar la cantidad
                    var producto = _productoHelper.GetProducto(productodia.IdProducto);

                    if (producto == null)
                    {
                        throw new Exception("El producto no existe.");
                    }

                    // Verificar si hay suficiente cantidad en el inventario
                    if (producto.Cantidad < productodia.Cantidad)
                    {
                        throw new Exception("No hay suficiente stock disponible para este producto.");
                    }

                    // Actualizar la cantidad del producto original antes de agregar el producto del día
                    producto.Cantidad -= productodia.Cantidad;
                    _productoHelper.Update(producto);

                    // Agregar el nuevo producto del día después de la actualización del inventario
                    _productodiaHelper.Add(productodia);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, agregar un mensaje de error al estado del modelo
                    ModelState.AddModelError("", "Hubo un problema al agregar el producto del día: " + ex.Message);
                }
            }

            // Si hay errores de validación, recargar las listas para el formulario
            var productos = _productoHelper.GetProductos();
            productodia.ListaProductos = productos.Select(p => new SelectListItem
            {
                Value = p.IdProducto.ToString(),
                Text = p.NombreProducto
            }).ToList();

            var escuelas = _escuelaHelper.GetEscuelas();
            productodia.ListaEscuelas = escuelas.Select(e => new SelectListItem
            {
                Value = e.IdEscuela.ToString(),
                Text = e.NombreEscuela
            }).ToList();

            return View(productodia);
        }



      //  [Authorize(Roles = "Admin, Producto")]
        // GET: ProductoDiaController/Edit/5
        public ActionResult Edit(int id)
        {
            var productodia = _productodiaHelper.GetProductoDia(id);

            if (productodia == null)
            {
                return NotFound();
            }

            var productos = _productoHelper.GetProductos();

            productodia.ListaProductos = productos
                .Select(p => new SelectListItem
                {
                    Value = p.IdProducto.ToString(),
                    Text = p.NombreProducto
                }).ToList();

            var escuelas = _escuelaHelper.GetEscuelas();

            productodia.ListaEscuelas = escuelas
                .Select(e => new SelectListItem
                {
                    Value = e.IdEscuela.ToString(),
                    Text = e.NombreEscuela
                }).ToList();

            return View(productodia);
        }

        // POST: ProductoDiaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductoDiaViewModel productodia)
        {
            try
            {
                // Obtén la cantidad original del Producto del Día antes de la actualización
                var originalProductoDia = _productodiaHelper.GetProductoDia(productodia.IdProductoDia);
                if (originalProductoDia == null)
                {
                    return NotFound();
                }

                // Calcula la diferencia de cantidades
                int diferencia = (productodia.Cantidad ?? 0) - (originalProductoDia.Cantidad ?? 0);

                // Ajusta la cantidad en el inventario del producto
                var producto = _productoHelper.GetProducto(productodia.IdProducto);
                if (producto == null)
                {
                    return NotFound();
                }

                producto.Cantidad -= diferencia; // Resta o suma según la diferencia calculada
                _productoHelper.Update(producto);

                // Actualiza el Producto del Día
                _productodiaHelper.Update(productodia);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Si hay un error, recarga las listas de productos y escuelas
                var productos = _productoHelper.GetProductos();
                productodia.ListaProductos = productos
                    .Select(p => new SelectListItem
                    {
                        Value = p.IdProducto.ToString(),
                        Text = p.NombreProducto
                    }).ToList();

                var escuelas = _escuelaHelper.GetEscuelas();
                productodia.ListaEscuelas = escuelas
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEscuela.ToString(),
                        Text = e.NombreEscuela
                    }).ToList();

                return View(productodia);
            }
        }


       // [Authorize(Roles = "Admin, Producto")]
        // GET: ProductoDiaController/Delete/5
        public ActionResult Delete(int id)
        {
            var productodia = _productodiaHelper.GetProductoDia(id);

            if (productodia == null)
            {
                return NotFound();  
            }

            var producto = _productoHelper.GetProducto(productodia.IdProducto);

            productodia.NombreProducto = producto?.NombreProducto;

            var escuela = _escuelaHelper.GetEscuela(productodia.IdEscuela);

            productodia.NombreEscuela = escuela?.NombreEscuela;

            return View(productodia);  
        }


        // POST: ProductoDiaController/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int IdProductoDia)
        {
            try
            {
                var productodia = _productodiaHelper.GetProductoDia(IdProductoDia);
                if (productodia == null)
                {
                    return NotFound();
                }

                var producto = _productoHelper.GetProducto(productodia.IdProducto);
                if (producto == null)
                {
                    throw new Exception("Producto relacionado no encontrado.");
                }

                producto.Cantidad += productodia.Cantidad ?? 0;

                _productoHelper.Update(producto);

                _productodiaHelper.Delete(IdProductoDia);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Hubo un problema al eliminar el producto del día: " + ex.Message);

                var productodia = _productodiaHelper.GetProductoDia(IdProductoDia);
                return View("Delete", productodia);
            }
        }


    }

}


