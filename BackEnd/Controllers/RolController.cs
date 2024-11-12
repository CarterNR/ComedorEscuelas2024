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
    public class RolController : ControllerBase
    {
        IRolService rolService;

        public RolController(IRolService rolService)
        {
            this.rolService = rolService;
        }






        // GET: api/<RolController>
        [HttpGet]
        public IEnumerable<RolDTO> Get()
        {
            return rolService.Obtener();
        }

        // GET api/<RolController>/5
        [HttpGet("{id}")]
        public RolDTO Get(int id)
        {
            return rolService.Obtener(id);
        }

        // POST api/<RolController>
        [HttpPost]
        public IActionResult Post([FromBody] RolDTO rol)
        {
            rolService.Agregar(rol);
            return Ok(rol);
        }

        // PUT api/<RolController>/5
        [HttpPut]
        public IActionResult Put([FromBody] RolDTO rol)
        {
            rolService.Editar(rol);
            return Ok(rol);
        }

        // DELETE api/<RolController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            RolDTO rol = new RolDTO
            {
                IdRol = id
            };
            rolService.Eliminar(rol);
        }
    }
}
