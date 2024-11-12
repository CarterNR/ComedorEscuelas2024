using BackEnd.DTO;
using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        IPedidoService pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            this.pedidoService = pedidoService;
        }



        // GET: api/<PedidoController>
        [HttpGet]
        public IEnumerable<PedidoDTO> Get()
        {
            return pedidoService.Obtener();
        }

        // GET api/<PedidoController>/5
        [HttpGet("{id}")]
        public PedidoDTO Get(int id)
        {
            return pedidoService.Obtener(id);
        }

        // POST api/<PedidoController>
        [HttpPost]
        public IActionResult Post([FromBody] PedidoDTO pedido)
        {
            pedidoService.Agregar(pedido);
            return Ok(pedido);
        }

        // PUT api/<PedidoController>/5
        [HttpPut]
        public IActionResult Put([FromBody] PedidoDTO pedido)
        {
            pedidoService.Editar(pedido);
            return Ok(pedido);
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PedidoDTO pedido = new PedidoDTO
            {
                IdPedido = id
            };
            pedidoService.Eliminar(pedido);
        }
    }
}
