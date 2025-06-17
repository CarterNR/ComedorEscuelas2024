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
    public class EstudianteController : ControllerBase
    {

        IEstudianteService estudianteService;
    
    public EstudianteController(IEstudianteService estudianteService)
    {
        this.estudianteService = estudianteService;
    }



    // GET: api/<EstudianteController>
    [HttpGet]
    public IEnumerable<EstudianteDTO> Get()
    {
        return estudianteService.Obtener();
    }

    // GET api/<EstudianteController>/5
    [HttpGet("{id}")]
    public EstudianteDTO Get(int id)
    {
        return estudianteService.Obtener(id);
    }

    // POST api/<EstudianteController>
    [HttpPost]
    public IActionResult Post([FromBody] EstudianteDTO estudiante)
    {
        estudianteService.Agregar(estudiante);
        return Ok(estudiante);
    }

    // PUT api/<EstudianteController>/5
    [HttpPut]
    public IActionResult Put([FromBody] EstudianteDTO estudiante)
    {
        estudianteService.Editar(estudiante);
        return Ok(estudiante);
    }

    // DELETE api/<EstudianteController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)


            EstudianteDTO estudiante = new EstudianteDTO
            {
                IdEstudiante = id
            };
    estudianteService.Eliminar(estudiante);
        }
    }
}
