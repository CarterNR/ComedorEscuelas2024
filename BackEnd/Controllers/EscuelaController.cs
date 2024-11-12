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
    public class EscuelaController : ControllerBase
    {
        IEscuelaService escuelaService;

        public EscuelaController(IEscuelaService escuelaService)
        {
            this.escuelaService = escuelaService;
        }



        // GET: api/<EscuelaController>
        [HttpGet]
        public IEnumerable<EscuelaDTO> Get()
        {
            return escuelaService.Obtener();
        }

        // GET api/<EscuelaController>/5
        [HttpGet("{id}")]
        public EscuelaDTO Get(int id)
        {
            return escuelaService.Obtener(id);
        }

        // POST api/<EscuelaController>
        [HttpPost]
        public IActionResult Post([FromBody] EscuelaDTO escuela)
        {
            escuelaService.Agregar(escuela);
            return Ok(escuela);
        }

        // PUT api/<EscuelaController>/5
        [HttpPut]
        public IActionResult Put([FromBody] EscuelaDTO escuela)
        {
            escuelaService.Editar(escuela);
            return Ok(escuela);
        }

        // DELETE api/<EscuelaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            EscuelaDTO escuela = new EscuelaDTO
            {
                IdEscuela = id
            };
            escuelaService.Eliminar(escuela);
        }
    }
}
