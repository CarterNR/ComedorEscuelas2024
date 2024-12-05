using FrontEnd.ApiModels;
using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace FrontEnd.Controllers
{
    public class ProductoController : Controller
    {
        IProductoHelper _productoHelper;
        IEscuelaHelper _escuelaHelper;
        IProveedorHelper _proveedorHelper;



        public ProductoController(IProductoHelper productoHelper, IEscuelaHelper escuelaHelper, IProveedorHelper proveedorHelper)
        {
            _productoHelper = productoHelper;
            _escuelaHelper = escuelaHelper;
            _proveedorHelper = proveedorHelper;
        }
        // GET: ProductoController

        public ActionResult Index()


        {
            var lista = _productoHelper.GetProductos();

            var escuelas = _escuelaHelper.GetEscuelas();

            foreach (var item in lista)
            {
                var escuela = escuelas.FirstOrDefault(e => e.IdEscuela == item.IdEscuela);
                if (escuela != null)
                {
                    item.NombreEscuela = escuela.NombreEscuela;
                }
            }

            var proveedores = _proveedorHelper.GetProveedores();

            foreach (var item in lista)
            {
                var proveedor = proveedores.FirstOrDefault(p => p.IdProveedor == item.IdProveedor);
                if (proveedor != null)
                {
                    item.NombreProveedor = proveedor.NombreProveedor;
                }
            }

            foreach (var producto in lista)
            {
                if (producto.Imagen != null)
                {
                    string base64Image = Convert.ToBase64String(producto.Imagen);
                    producto.ImagenBase64 = $"data:image/jpeg;base64,{base64Image}";
                }
            }

            return View(lista);
        }


        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {
            var producto = _productoHelper.GetProducto(id);

            if (producto == null)
            {
                return NotFound();
            }

            var proveedor = _proveedorHelper.GetProveedor(producto.IdProveedor);
            producto.NombreProveedor = proveedor?.NombreProveedor;

            var escuela = _escuelaHelper.GetEscuela(producto.IdEscuela);
            producto.NombreEscuela = escuela?.NombreEscuela;

            if (producto.Imagen != null)
            {
                string base64Image = Convert.ToBase64String(producto.Imagen);
                producto.ImagenBase64 = $"data:image/jpeg;base64,{base64Image}";
            }

            return View(producto);
        }



        // GET: ProductoController/Create
        public IActionResult Create()
        {
            var model = new ProductoViewModel();

            var escuelas = _escuelaHelper.GetEscuelas();
            model.ListaEscuelas = escuelas.Select(e => new SelectListItem
            {
                Value = e.IdEscuela.ToString(),
                Text = e.NombreEscuela
            }).ToList();

            var proveedores = _proveedorHelper.GetProveedores();
            model.ListaProveedores = proveedores.Select(p => new SelectListItem
            {
                Value = p.IdProveedor.ToString(),
                Text = p.NombreProveedor
            }).ToList();

            return View(model);
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductoViewModel producto)
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
                CargarListasDeSeleccion(producto);
                return View(producto);
            }

            if (producto.ImagenArchivo != null && producto.ImagenArchivo.Length > 0)
            {
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        producto.ImagenArchivo.CopyTo(ms);
                        producto.Imagen = ms.ToArray(); 
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Hubo un error al procesar la imagen: " + ex.Message);
                    CargarListasDeSeleccion(producto);
                    return View(producto);
                }
            }

            try
            {
                _productoHelper.Add(producto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Hubo un error al agregar el producto: " + ex.Message);
                CargarListasDeSeleccion(producto);
                return View(producto);
            }

            return RedirectToAction("Index");
        }

        private void CargarListasDeSeleccion(ProductoViewModel producto)
        {
            var proveedores = _proveedorHelper.GetProveedores();
            producto.ListaProveedores = proveedores.Select(p => new SelectListItem
            {
                Value = p.IdProveedor.ToString(),
                Text = p.NombreProveedor
            }).ToList();

            var escuelas = _escuelaHelper.GetEscuelas();
            producto.ListaEscuelas = escuelas.Select(e => new SelectListItem
            {
                Value = e.IdEscuela.ToString(),
                Text = e.NombreEscuela
            }).ToList();
        }





        // GET: ProductoController/Edit/5

        public ActionResult Edit(int id)
        {
            var producto = _productoHelper.GetProducto(id);

            if (producto == null)
            {
                return NotFound();
            }

            if (producto.Imagen != null)
            {
                producto.ImagenBase64 = $"data:image/jpeg;base64,{Convert.ToBase64String(producto.Imagen)}";
            }

            var proveedores = _proveedorHelper.GetProveedores();
            producto.ListaProveedores = proveedores
                .Select(p => new SelectListItem
                {
                    Value = p.IdProveedor.ToString(),
                    Text = p.NombreProveedor
                }).ToList();

            var escuelas = _escuelaHelper.GetEscuelas();
            producto.ListaEscuelas = escuelas
                .Select(e => new SelectListItem
                {
                    Value = e.IdEscuela.ToString(),
                    Text = e.NombreEscuela
                }).ToList();

            return View(producto);
        }


        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductoViewModel producto)
        {
            try
            {
                var productoOriginal = _productoHelper.GetProducto(producto.IdProducto);
                if (productoOriginal == null)
                {
                    return NotFound();
                }

                if (producto.ImagenArchivo == null || producto.ImagenArchivo.Length == 0)
                {
                    producto.Imagen = productoOriginal.Imagen; 
                }
                else
                {
                    using (var ms = new MemoryStream())
                    {
                        producto.ImagenArchivo.CopyTo(ms);
                        producto.Imagen = ms.ToArray(); 
                    }
                }

                var productoActualizado = _productoHelper.Update(producto);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(producto);
            }
        }





        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            var producto = _productoHelper.GetProducto(id);

            if (producto == null)
            {
                return NotFound();
            }

            var proveedor = _proveedorHelper.GetProveedor(producto.IdProveedor);

            producto.NombreProveedor = proveedor?.NombreProveedor;

            var escuela = _escuelaHelper.GetEscuela(producto.IdEscuela);

            producto.NombreEscuela = escuela?.NombreEscuela;

            if (producto.Imagen != null)
            {
                string base64Image = Convert.ToBase64String(producto.Imagen);
                producto.ImagenBase64 = $"data:image/jpeg;base64,{base64Image}";
            }

            return View(producto);
        }



        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int IdProducto)
        {
            try
            {
                _productoHelper.Delete(IdProducto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Hubo un problema al eliminar el producto del día: " + ex.Message);

                var producto = _productoHelper.GetProducto(IdProducto);
                return View("Delete", producto);
            }
        }




       


    }
}
