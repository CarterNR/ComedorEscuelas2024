using FrontEnd.ApiModels;
using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace FrontEnd.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoHelper _productoHelper;
        private readonly IProveedorHelper _proveedorHelper;
        private readonly IEscuelaHelper _escuelaHelper;

        public ProductoController(
            IProductoHelper productoHelper,
            IProveedorHelper proveedorHelper,
            IEscuelaHelper escuelaHelper)
        {
            _productoHelper = productoHelper;
            _proveedorHelper = proveedorHelper;
            _escuelaHelper = escuelaHelper;
        }

        // GET: Producto
//        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var productos = _productoHelper.GetProductos();
            return View(productos);
        }

       // [Authorize(Roles = "Admin")]
        // GET: Producto/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = _productoHelper.GetProducto(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

      //  [Authorize(Roles = "Admin")]
        // GET: Producto/Create
        public IActionResult Create()
        {
            var viewModel = new ProductoViewModel
            {
                ListaProveedores = _proveedorHelper.GetProveedores()
                    .Select(p => new SelectListItem
                    {
                        Value = p.IdProveedor.ToString(),
                        Text = p.NombreProveedor
                    }),
                ListaEscuelas = _escuelaHelper.GetEscuelas()
                    .Select(e => new SelectListItem
                    {
                        Value = e.IdEscuela.ToString(),
                        Text = e.NombreEscuela
                    })
            };

            return View(viewModel);
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductoViewModel producto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productoHelper.Add(producto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            if (producto.ImagenFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    producto.ImagenFile.CopyTo(ms);
                    producto.Imagen = ms.ToArray();
                }
            }


            // Repoblar listas desplegables en caso de error
            producto.ListaProveedores = _proveedorHelper.GetProveedores()
                .Select(p => new SelectListItem
                {
                    Value = p.IdProveedor.ToString(),
                    Text = p.NombreProveedor
                });
            producto.ListaEscuelas = _escuelaHelper.GetEscuelas()
                .Select(e => new SelectListItem
                {
                    Value = e.IdEscuela.ToString(),
                    Text = e.NombreEscuela
                });

            return View(producto);
        }


     //   [Authorize(Roles = "Admin")]
        // GET: Producto/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = _productoHelper.GetProducto(id);
            if (producto == null)
            {
                return NotFound();
            }

            producto.ListaProveedores = _proveedorHelper.GetProveedores()
                .Select(p => new SelectListItem
                {
                    Value = p.IdProveedor.ToString(),
                    Text = p.NombreProveedor
                });
            producto.ListaEscuelas = _escuelaHelper.GetEscuelas()
                .Select(e => new SelectListItem
                {
                    Value = e.IdEscuela.ToString(),
                    Text = e.NombreEscuela
                });

            return View(producto);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductoViewModel producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Manejar la carga de imagen de manera similar al método Add
                    if (producto.ImagenFile != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            producto.ImagenFile.CopyTo(ms);
                            producto.Imagen = ms.ToArray();
                        }
                    }

                    _productoHelper.Update(producto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            // Repoblar listas desplegables en caso de error
            producto.ListaProveedores = _proveedorHelper.GetProveedores()
                .Select(p => new SelectListItem
                {
                    Value = p.IdProveedor.ToString(),
                    Text = p.NombreProveedor
                });
            producto.ListaEscuelas = _escuelaHelper.GetEscuelas()
                .Select(e => new SelectListItem
                {
                    Value = e.IdEscuela.ToString(),
                    Text = e.NombreEscuela
                });

            return View(producto);
        }


   //     [Authorize(Roles = "Admin")]
        // GET: Producto/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = _productoHelper.GetProducto(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _productoHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Delete", _productoHelper.GetProducto(id));
            }
        }

        public IActionResult MostrarImagen(int id)
        {
            var producto = _productoHelper.GetProducto(id);

            if (producto?.Imagen == null)
            {
                return NotFound();
            }

            // Detección de tipo MIME
            string mimeType = "application/octet-stream"; // Tipo MIME predeterminado
            if (producto.ImagenFile != null)
            {
                var extension = Path.GetExtension(producto.ImagenFile.FileName).ToLowerInvariant();
                mimeType = extension switch
                {
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".gif" => "image/gif",
                    _ => "application/octet-stream"
                };
            }

            return File(producto.Imagen, mimeType);
        }
    }
}
