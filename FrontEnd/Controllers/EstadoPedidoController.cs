using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class EstadoPedidoController : Controller
    {
        IEstadoPedidoHelper _estadopedidoHelper;

        public EstadoPedidoController(IEstadoPedidoHelper estadopedidoHelper)
        {
            _estadopedidoHelper = estadopedidoHelper;
        }
        // GET: EstadoPedidoController
        public ActionResult Index()
        {
            var lista = _estadopedidoHelper.GetEstadoPedidos();
            return View(lista);
        }

        // GET: EstadoPedidoController/Details/5
        public ActionResult Details(int id)
        {
            var estadopedido = _estadopedidoHelper.GetEstadoPedido(id);
            return View(estadopedido);
        }

        // GET: EstadoPedidoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoPedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EstadoPedidoViewModel estadopedido)
        {
            try
            {
                _estadopedidoHelper.Add(estadopedido);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadoPedidoController/Edit/5
        public ActionResult Edit(int id)
        {
            var estadopedido = _estadopedidoHelper.GetEstadoPedido(id);

            return View(estadopedido);
        }

        // POST: EstadoPedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EstadoPedidoViewModel estadopedido)
        {
            try
            {
                _estadopedidoHelper.Update(estadopedido
                    );
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadoPedidoController/Delete/5
        public ActionResult Delete(int id)
        {

            var estadopedido = _estadopedidoHelper.GetEstadoPedido(id);

            return View(estadopedido);
        }

        // POST: EstadoPedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EstadoPedidoViewModel estadopedido)
        {
            try
            {
                _estadopedidoHelper.Delete(estadopedido.IdEstadoPedido);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
