

using FrontEnd.Helpers.Interfaces;
using FrontEnd.Helpers.Implementations;
using FrontEnd.Models;
using FrontEnd.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace FrontEnd.Controllers
{
    [RoleAuth(1)] // Solo rol Administrador
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductoHelper _productoHelper;

        public HomeController(ILogger<HomeController> logger, IProductoHelper productoHelper)
        {
            _logger = logger;
            _productoHelper = productoHelper;
        }

        public IActionResult Index()
        {
            var productos = _productoHelper.GetProductos(); // Obt�n los productos
            return View(productos); // Pasa los productos al modelo de la vista
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
