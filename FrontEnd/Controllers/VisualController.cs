using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FrontEnd.Controllers
{
    public class VisualController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public VisualController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("Visual/Escanear/{cedula}")]
        public async Task<IActionResult> Escanear(string cedula)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"http://192.168.0.10:5273/api/Estudiante/Escanear/{cedula}");

            if (response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadFromJsonAsync<JsonElement>();

                if (resultado.TryGetProperty("mensaje", out var mensajeElement))
                {
                    ViewBag.Mensaje = mensajeElement.GetString();
                }
                else
                {
                    ViewBag.Mensaje = "Sin mensaje recibido.";
                }
            }
            else
            {
                ViewBag.Mensaje = "Error al conectar con el servidor.";
            }

            return View("ResultadoEscaneo");
        }
    }
}
