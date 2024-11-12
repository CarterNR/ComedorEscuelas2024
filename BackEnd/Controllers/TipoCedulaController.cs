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
    public class TipoCedulaController : ControllerBase
    {
        ITipoCedulaService tipoCedulaService;

        public TipoCedulaController(ITipoCedulaService tipoCedulaService)
        {
            this.tipoCedulaService = tipoCedulaService;
        }




        // GET: api/<TipoCedulaController>
        [HttpGet]
        public IEnumerable<TipoCedulaDTO> Get()
        {
            return tipoCedulaService.Obtener();
        }

        // GET api/<TipoCedulaController>/5
        [HttpGet("{id}")]
        public TipoCedulaDTO Get(int id)
        {
            return tipoCedulaService.Obtener(id);
        }

        // POST api/<TipoCedulaController>
        [HttpPost]
        public IActionResult Post([FromBody] TipoCedulaDTO tipoCedula)
        {
            tipoCedulaService.Agregar(tipoCedula);
            return Ok(tipoCedula);
        }

        // PUT api/<TipoCedulaController>/5
        [HttpPut]
        public IActionResult Put([FromBody] TipoCedulaDTO tipoCedula)
        {
            tipoCedulaService.Editar(tipoCedula);
            return Ok(tipoCedula);
        }

        // DELETE api/<TipoCedulaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            TipoCedulaDTO tiposCedula = new TipoCedulaDTO
            {
                IdTipoCedula = id
            };
            tipoCedulaService.Eliminar(tiposCedula);
        }
    }
}
