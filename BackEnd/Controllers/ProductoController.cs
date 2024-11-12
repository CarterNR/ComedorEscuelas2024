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



        // GET: api/<ProductoController>
        [HttpGet]
        public IEnumerable<ProductoDTO> Get()
        {
            return productoService.Obtener();
        }

        // GET api/<ProductoController>/5
        [HttpGet("{id}")]
        public ProductoDTO Get(int id)
        {
            return productoService.Obtener(id);
        }

        // POST api/<ProductoController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductoDTO producto)
        {
            productoService.Agregar(producto);
            return Ok(producto);
        }

        // PUT api/<ProductoController>/5
        [HttpPut]
        public IActionResult Put([FromBody] ProductoDTO producto)
        {
            productoService.Editar(producto);
            return Ok(producto);
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ProductoDTO producto = new ProductoDTO
            {
                IdProducto = id
            };
            productoService.Eliminar(producto);
        }
    }
}
