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
        public ActionResult Index()
        {
            var lista = _pedidoHelper.GetPedidos();

            var productos = _productoHelper.GetProductos();

            foreach (var item in lista)
            {
                var producto = productos.FirstOrDefault(p => p.IdProducto == item.IdProducto);
                if (producto != null)
                {
                    item.NombreProducto = producto.NombreProducto;
                }
            }

            var escuelas = _escuelaHelper.GetEscuelas();

            foreach (var item in lista)
            {
                var escuela = escuelas.FirstOrDefault(p => p.IdEscuela == item.IdEscuela);
                if (escuela != null)
                {
                    item.NombreEscuela = escuela.NombreEscuela;
                }
            }

            var usuarios = _usuarioHelper.GetUsuarios();

            foreach (var item in lista)
            {
                var usuario = usuarios.FirstOrDefault(p => p.IdUsuario == item.IdUsuario);
                if (usuario != null)
                {
                    item.NombreCompleto = usuario.NombreCompleto;
                }
            }


            var estadopedidos = _estadoPedidoHelper.GetEstadoPedidos();

            foreach (var item in lista)
            {
                var estadopedido = estadopedidos.FirstOrDefault(p => p.IdEstadoPedido == item.IdEstadoPedido);
                if (estadopedido != null)
                {
                    item.EstadoPedido1 = estadopedido.EstadoPedido1;
                }
            }

            return View(lista);
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

            var usuarios = _usuarioHelper.GetUsuarios();

            model.ListaUsuarios = usuarios.Select(e => new SelectListItem
            {
                Value = e.IdUsuario.ToString(),
                Text = e.NombreCompleto
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

                 

                    producto.Cantidad += pedido.Cantidad;
                    _productoHelper.Update(producto);

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

            var usuarios = _usuarioHelper.GetUsuarios();
            pedido.ListaUsuarios = usuarios.Select(e => new SelectListItem
            {
                Value = e.IdUsuario.ToString(),
                Text = e.NombreCompleto
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
                var originalPedido = _pedidoHelper.GetPedido(pedido.IdPedido);
                if (originalPedido == null)
                {
                    return NotFound();
                }

                int diferencia = (pedido.Cantidad ?? 0) - (originalPedido.Cantidad ?? 0);

                var producto = _productoHelper.GetProducto(pedido.IdProducto);
                if (producto == null)
                {
                    return NotFound();
                }

                if (diferencia < 0)
                {
                    int cantidadARestar = Math.Abs(diferencia); 
                    if (producto.Cantidad < cantidadARestar)
                    {
                        ModelState.AddModelError("Cantidad", "No hay suficiente inventario para realizar este cambio.");
                        return View(CargarListasParaVista(pedido)); 
                    }

                    producto.Cantidad -= cantidadARestar;
                }
                else if (diferencia > 0)
                {
                    producto.Cantidad += diferencia;
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
