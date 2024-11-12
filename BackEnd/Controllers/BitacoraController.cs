using BackEnd.DTO;
using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitacoraController : ControllerBase
    {
        IBitacoraService bitacoraService;

        public BitacoraController(IBitacoraService bitacoraService)
        {
            this.bitacoraService = bitacoraService;
        }




        // GET: api/<BitacoraController>
        [HttpGet]
        public IEnumerable<BitacoraDTO> Get()
        {
            return bitacoraService.Obtener();
        }

        // GET api/<BitacoraController>/5
        [HttpGet("{id}")]
        public BitacoraDTO Get(int id)
        {
            return bitacoraService.Obtener(id);
        }

        // POST api/<BitacoraController>
        [HttpPost]
        public IActionResult Post([FromBody] BitacoraDTO bitacora)
        {
            bitacoraService.Agregar(bitacora);
            return Ok(bitacora);
        }

        // PUT api/<BitacoraController>/5
        [HttpPut]
        public IActionResult Put([FromBody] BitacoraDTO bitacora)
        {
            bitacoraService.Editar(bitacora);
            return Ok(bitacora);
        }

        // DELETE api/<BitacoraController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            BitacoraDTO bitacora = new BitacoraDTO
            {
                IdBitacora = id
            };
            bitacoraService.Eliminar(bitacora);
        }
    }
}
