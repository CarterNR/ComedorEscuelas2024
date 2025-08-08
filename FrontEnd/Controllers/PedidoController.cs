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
    public class PedidoController : Controller
    {
        IPedidoHelper _pedidoHelper;
        IProductoHelper _productoHelper;
        IEscuelaHelper _escuelaHelper;
        IUsuarioHelper _usuarioHelper;
        IEstadoPedidoHelper _estadoPedidoHelper;

        public PedidoController(IPedidoHelper pedidoHelper, IProductoHelper productoHelper, IEscuelaHelper escuelaHelper, 
            IUsuarioHelper usuarioHelper, IEstadoPedidoHelper estadoPedidoHelper)
        {
            _pedidoHelper = pedidoHelper;
            _productoHelper = productoHelper;
            _escuelaHelper = escuelaHelper;
            _usuarioHelper = usuarioHelper; 
            _estadoPedidoHelper = estadoPedidoHelper;

        }
        // GET: PedidoController
        // [Authorize(Roles = "Admin")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 8)
        {
            var lista = _pedidoHelper.GetPedidos();
            var productos = _productoHelper.GetProductos();
            var escuelas = _escuelaHelper.GetEscuelas();
            var usuarios = _usuarioHelper.GetUsuarios();
            var estadopedidos = _estadoPedidoHelper.GetEstadoPedidos();

            foreach (var item in lista)
            {
                item.NombreProducto = productos.FirstOrDefault(p => p.IdProducto == item.IdProducto)?.NombreProducto;
                item.NombreEscuela = escuelas.FirstOrDefault(e => e.IdEscuela == item.IdEscuela)?.NombreEscuela;
                item.NombreCompleto = usuarios.FirstOrDefault(u => u.IdUsuario == item.IdUsuario)?.NombreCompleto;
                item.EstadoPedido1 = estadopedidos.FirstOrDefault(e => e.IdEstadoPedido == item.IdEstadoPedido)?.EstadoPedido1;
            }

            // Filtrar pedidos del mes actual
            var hoy = DateTime.Now;
            var primerDiaMes = new DateTime(hoy.Year, hoy.Month, 1).Date;
            var ultimoDiaMes = primerDiaMes.AddMonths(1).Date;
            var listaFiltrada = lista
                .Where(p => p.FechaHoraIngreso.HasValue &&
                            p.FechaHoraIngreso.Value.Date >= primerDiaMes &&
                            p.FechaHoraIngreso.Value.Date < ultimoDiaMes)
                .ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                listaFiltrada = listaFiltrada.Where(p =>
                    (p.NombreProducto != null && p.NombreProducto.ToLower().Contains(searchString)) ||
                    (p.NombreEscuela != null && p.NombreEscuela.ToLower().Contains(searchString)) ||
                    (p.NombreCompleto != null && p.NombreCompleto.ToLower().Contains(searchString)) ||
                    (p.EstadoPedido1 != null && p.EstadoPedido1.ToLower().Contains(searchString)) ||
                    (p.FechaHoraIngreso.HasValue && p.FechaHoraIngreso.Value.ToString("yyyy-MM-dd").Contains(searchString))
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


        public ActionResult ListaPedidosCompletos(string searchString, int page = 1, int pageSize = 8)
        {
            var lista = _pedidoHelper.GetPedidos();
            var productos = _productoHelper.GetProductos();
            var escuelas = _escuelaHelper.GetEscuelas();
            var usuarios = _usuarioHelper.GetUsuarios();
            var estadopedidos = _estadoPedidoHelper.GetEstadoPedidos();

            foreach (var item in lista)
            {
                item.NombreProducto = productos.FirstOrDefault(p => p.IdProducto == item.IdProducto)?.NombreProducto;
                item.NombreEscuela = escuelas.FirstOrDefault(e => e.IdEscuela == item.IdEscuela)?.NombreEscuela;
                item.NombreCompleto = usuarios.FirstOrDefault(u => u.IdUsuario == item.IdUsuario)?.NombreCompleto;
                item.EstadoPedido1 = estadopedidos.FirstOrDefault(e => e.IdEstadoPedido == item.IdEstadoPedido)?.EstadoPedido1;
            }

            var hoy = DateTime.Now;
            var primerDiaMes = new DateTime(hoy.Year, hoy.Month, 1).Date;
            var ultimoDiaMes = primerDiaMes.AddMonths(1).Date;

            var listaFiltrada = lista
                .Where(p => !p.FechaHoraIngreso.HasValue ||
                            p.FechaHoraIngreso.Value.Date < primerDiaMes ||
                            p.FechaHoraIngreso.Value.Date >= ultimoDiaMes)
                .ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                listaFiltrada = listaFiltrada.Where(p =>
                    (p.NombreProducto != null && p.NombreProducto.ToLower().Contains(searchString)) ||
                    (p.NombreEscuela != null && p.NombreEscuela.ToLower().Contains(searchString)) ||
                    (p.NombreCompleto != null && p.NombreCompleto.ToLower().Contains(searchString)) ||
                    (p.EstadoPedido1 != null && p.EstadoPedido1.ToLower().Contains(searchString)) ||
                    (p.FechaHoraIngreso.HasValue && p.FechaHoraIngreso.Value.ToString("yyyy-MM-dd").Contains(searchString))
                ).ToList();
            }

            var totalItems = listaFiltrada.Count();
            var items = listaFiltrada.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.SearchString = searchString;

            return View(items);
        }



        //  [Authorize(Roles = "Admin")]
        // GET: PedidoController/Details/5
        public ActionResult Details(int id)
        {
            var pedido = _pedidoHelper.GetPedido(id);

            if (pedido == null)
            {
                return NotFound();
            }

            var producto = _productoHelper.GetProducto(pedido.IdProducto);
            pedido.NombreProducto = producto?.NombreProducto;

            var escuela = _escuelaHelper.GetEscuela(pedido.IdEscuela);
            pedido.NombreEscuela = escuela?.NombreEscuela;

            var usuario = _usuarioHelper.GetUsuario(pedido.IdUsuario);
            pedido.NombreCompleto = usuario?.NombreCompleto;

            var estadopedido = _estadoPedidoHelper.GetEstadoPedido(pedido.IdEstadoPedido);
            pedido.EstadoPedido1 = estadopedido?.EstadoPedido1;

            return View(pedido);
        }
       // [Authorize(Roles = "Admin")]
        // GET: PedidoController/Create
        public IActionResult Create()
        {
            var model = new PedidoViewModel();

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

            var estadopedidos = _estadoPedidoHelper.GetEstadoPedidos();

            model.ListaEstadoPedidos = estadopedidos.Select(e => new SelectListItem
            {
                Value = e.IdEstadoPedido.ToString(),
                Text = e.EstadoPedido1
            }).ToList();

            return View(model);


           
        }

        // POST: PedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PedidoViewModel pedido)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _pedidoHelper.Add(pedido);

                    var producto = _productoHelper.GetProducto(pedido.IdProducto);

                    if (producto == null)
                    {
                        throw new Exception("El producto no existe.");
                    }

                    if(pedido.IdEstadoPedido == 3)
                    {
                        producto.Cantidad += pedido.Cantidad;
                        _productoHelper.Update(producto);
                    }


                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Hubo un problema al agregar el pedido: " + ex.Message);
                }
            }


            var productos = _productoHelper.GetProductos();
            pedido.ListaProductos = productos.Select(p => new SelectListItem
            {
                Value = p.IdProducto.ToString(),
                Text = p.NombreProducto
            }).ToList();

            var escuelas = _escuelaHelper.GetEscuelas();
            pedido.ListaEscuelas = escuelas.Select(e => new SelectListItem
            {
                Value = e.IdEscuela.ToString(),
                Text = e.NombreEscuela
            }).ToList();

            var estadopedidos = _estadoPedidoHelper.GetEstadoPedidos();
            pedido.ListaEstadoPedidos = estadopedidos.Select(e => new SelectListItem
            {
                Value = e.IdEstadoPedido.ToString(),
                Text = e.EstadoPedido1
            }).ToList();

            return View(pedido);
        }


       // [Authorize(Roles = "Admin")]
        // GET: PedidoController/Edit/5
        public ActionResult Edit(int id)
        {
            var pedido = _pedidoHelper.GetPedido(id);

            if (pedido == null)
            {
                return NotFound();
            }

            var productos = _productoHelper.GetProductos();
            pedido.ListaProductos = productos
                .Select(p => new SelectListItem
                {
                    Value = p.IdProducto.ToString(),
                    Text = p.NombreProducto
                }).ToList();

            var escuelas = _escuelaHelper.GetEscuelas();
            pedido.ListaEscuelas = escuelas
                .Select(e => new SelectListItem
                {
                    Value = e.IdEscuela.ToString(),
                    Text = e.NombreEscuela
                }).ToList();

            var usuarios = _usuarioHelper.GetUsuarios();
            pedido.ListaUsuarios = usuarios
                .Select(e => new SelectListItem
                {
                    Value = e.IdUsuario.ToString(),
                    Text = e.NombreCompleto
                }).ToList();

            var estadopedidos = _estadoPedidoHelper.GetEstadoPedidos();
            pedido.ListaEstadoPedidos = estadopedidos
                .Select(e => new SelectListItem
                {
                    Value = e.IdEstadoPedido.ToString(),
                    Text = e.EstadoPedido1
                }).ToList();

            return View(pedido);
        }

        // POST: PedidoController/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PedidoViewModel pedido)
        {
            try
            {
                var producto = _productoHelper.GetProducto(pedido.IdProducto);

                if (pedido.IdEstadoPedido == 3)
                {
                    producto.Cantidad += pedido.Cantidad;
                    _productoHelper.Update(producto);
                }

                var originalPedido = _pedidoHelper.GetPedido(pedido.IdPedido);
                if (originalPedido == null)
                {
                    return NotFound();
                }

                if (producto == null)
                {
                    return NotFound();
                }
                

                _productoHelper.Update(producto);

                _pedidoHelper.Update(pedido);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Hubo un error al editar el pedido: " + ex.Message);
                return View(CargarListasParaVista(pedido));
            }
        }



        private PedidoViewModel CargarListasParaVista(PedidoViewModel pedido)
        {
            var productos = _productoHelper.GetProductos();
            pedido.ListaProductos = productos
                .Select(p => new SelectListItem
                {
                    Value = p.IdProducto.ToString(),
                    Text = p.NombreProducto
                }).ToList();

            var escuelas = _escuelaHelper.GetEscuelas();
            pedido.ListaEscuelas = escuelas
                .Select(e => new SelectListItem
                {
                    Value = e.IdEscuela.ToString(),
                    Text = e.NombreEscuela
                }).ToList();

            var usuarios = _usuarioHelper.GetUsuarios();
            pedido.ListaUsuarios = usuarios
                .Select(u => new SelectListItem
                {
                    Value = u.IdUsuario.ToString(),
                    Text = u.NombreCompleto
                }).ToList();

            var estadopedidos = _estadoPedidoHelper.GetEstadoPedidos();
            pedido.ListaEstadoPedidos = estadopedidos
                .Select(ep => new SelectListItem
                {
                    Value = ep.IdEstadoPedido.ToString(),
                    Text = ep.EstadoPedido1
                }).ToList();

            return pedido;
        }



        //[Authorize(Roles = "Admin")]
        // GET: PedidoController/Delete/5
        public ActionResult Delete(int id)
        {
            var pedido = _pedidoHelper.GetPedido(id);

            if (pedido == null)
            {
                return NotFound();
            }

            var producto = _productoHelper.GetProducto(pedido.IdProducto);
            pedido.NombreProducto = producto?.NombreProducto;

            var escuela = _escuelaHelper.GetEscuela(pedido.IdEscuela);
            pedido.NombreEscuela = escuela?.NombreEscuela;

            var usuario = _usuarioHelper.GetUsuario(pedido.IdUsuario);
            pedido.NombreCompleto = usuario?.NombreCompleto;

            var estadopedido = _estadoPedidoHelper.GetEstadoPedido(pedido.IdEstadoPedido);
            pedido.EstadoPedido1 = estadopedido?.EstadoPedido1;

            return View(pedido);
        }

        // POST: PedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int IdPedido)
        {
            try
            {
                var pedido = _pedidoHelper.GetPedido(IdPedido);
                if (pedido == null)
                {
                    return NotFound();
                }

                var producto = _productoHelper.GetProducto(pedido.IdProducto);
                if (producto == null)
                {
                    throw new Exception("Producto relacionado no encontrado.");
                }

                producto.Cantidad -= pedido.Cantidad ?? 0;

                _productoHelper.Update(producto);

                _pedidoHelper.Delete(IdPedido);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Hubo un problema al eliminar el producto del día: " + ex.Message);

                var pedido = _pedidoHelper.GetPedido(IdPedido);
                return View("Delete", pedido);
            }
        }






    }
}
