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
            try
            {
                if (producto == null)
                {
                    return BadRequest("El producto no puede ser nulo.");
                }

                if (producto.Imagen == null || producto.Imagen.Length == 0)
                {
                    return BadRequest("La imagen es requerida y no puede estar vacía.");
                }

                productoService.Agregar(producto);

                return Ok(producto); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }


        // PUT api/<ProductoController>/5
        [HttpPut]
        public IActionResult Put([FromBody] ProductoDTO producto)
        {
            if (producto == null)
            {
                return BadRequest("Producto es nulo.");
            }

            var productoActualizado = productoService.Editar(producto);

            if (!productoActualizado)
            {
                return BadRequest("No se pudo actualizar el producto.");
            }

            return Ok(producto);  // Devuelve el producto actualizado
        }



        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var producto = new ProductoDTO { IdProducto = id };
            var resultado = productoService.Eliminar(producto);

            if (resultado)
            {
                return NoContent(); 
            }
            return BadRequest("No se pudo eliminar el producto. Verifica si existe o si tiene dependencias.");
        }






    }
}
