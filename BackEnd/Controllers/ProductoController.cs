using BackEnd.DTO;
using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        IProductoService productoService;

        public ProductoController(IProductoService productoService)
        {
            this.productoService = productoService;
        }

        [HttpGet]
        public IEnumerable<ProductoDTO> Get()
        {
            return productoService.Obtener();
        }

        [HttpGet("{id}")]
        public ProductoDTO Get(int id)
        {
            return productoService.Obtener(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductoDTO producto)
        {
            productoService.Agregar(producto);
            return Ok(producto);
        }

        [HttpPut]
        public IActionResult Put([FromBody] ProductoDTO producto)
        {
            productoService.Editar(producto);
            return Ok(producto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ProductoDTO producto = new ProductoDTO { IdProducto = id };
            productoService.Eliminar(producto);
            return Ok();
        }






    }
}
