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
    public class EstadoPedidoController : ControllerBase
    {
        IEstadoPedidoService estadoPedidoService;

        public EstadoPedidoController(IEstadoPedidoService estadoPedidoService)
        {
            this.estadoPedidoService = estadoPedidoService;
        }




        // GET: api/<EstadoPedidoController>
        [HttpGet]
        public IEnumerable<EstadoPedidoDTO> Get()
        {
            return estadoPedidoService.Obtener();
        }

        // GET api/<EstadoPedidoController>/5
        [HttpGet("{id}")]
        public EstadoPedidoDTO Get(int id)
        {
            return estadoPedidoService.Obtener(id);
        }

        // POST api/<EstadoPedidoController>
        [HttpPost]
        public IActionResult Post([FromBody] EstadoPedidoDTO estadoPedido)
        {
            estadoPedidoService.Agregar(estadoPedido);
            return Ok(estadoPedido);
        }

        // PUT api/<EstadoPedidoController>/5
        [HttpPut]
        public IActionResult Put([FromBody] EstadoPedidoDTO estadoPedido)
        {
            estadoPedidoService.Editar(estadoPedido);
            return Ok(estadoPedido);
        }

        // DELETE api/<EstadoPedidoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            EstadoPedidoDTO estadoPedido = new EstadoPedidoDTO
            {
                IdEstadoPedido = id
            };
            estadoPedidoService.Eliminar(estadoPedido);
        }
    }
}
