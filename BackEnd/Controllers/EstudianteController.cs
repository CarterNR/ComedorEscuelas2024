using BackEnd.DTO;
using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EstudianteController : ControllerBase
{
    private readonly IEstudianteService estudianteService;
    private readonly SisComedorContext context; // Asegúrate de tener esta referencia al contexto

    public EstudianteController(IEstudianteService estudianteService, SisComedorContext context)
    {
        this.estudianteService = estudianteService;
        this.context = context;
    }

    [HttpGet("Escanear/{cedula}")]
    public IActionResult Escanear(string cedula)
    {
        var estudiante = context.Estudiantes.FirstOrDefault(e => e.Cedula == cedula);

        if (estudiante == null)
        {
            return NotFound(new { mensaje = "Estudiante no encontrado." });
        }

        if (estudiante.TiquetesRestantes > 0)
        {
            estudiante.TiquetesRestantes -= 1;
            estudiante.FechaUltimoRebajo = DateTime.Now.Date;
            context.SaveChanges();

            return Ok(new
            {
                mensaje = $"Tiquete descontado. Tiquetes restantes: {estudiante.TiquetesRestantes}"
            });
        }
        else
        {
            return Ok(new { mensaje = "El estudiante no tiene tiquetes disponibles." });
        }
    }

    // 3️⃣ Backend Controller para descontar tiquete por QR
    [HttpPost("DescontarTiquete/{idEstudiante}")]
    public IActionResult DescontarTiquete(int idEstudiante)
    {
        var estudiante = context.Estudiantes.FirstOrDefault(e => e.IdEstudiante == idEstudiante);

        if (estudiante == null)
            return NotFound("Estudiante no encontrado.");

        if (estudiante.TiquetesRestantes > 0)
        {
            estudiante.TiquetesRestantes -= 1;
            estudiante.FechaUltimoRebajo = DateTime.Now.Date;
            context.SaveChanges();
            return Ok(estudiante);
        }
        else
        {
            return BadRequest("El estudiante no tiene tiquetes restantes.");
        }
    }

    [HttpGet("PorCedula/{cedula}")]
    public IActionResult GetEstudiantePorCedula(string cedula)
    {
        var estudiante = context.Estudiantes.FirstOrDefault(e => e.Cedula == cedula);

        if (estudiante == null)
            return NotFound();

        return Ok(estudiante);
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
    public IActionResult Put([FromBody] EstudianteDtoo estudiante)
    {
        var actualizado = estudianteService.Editar(estudiante);

        if (!actualizado)
            return NotFound("Estudiante no encontrado para ese usuario.");

        return Ok("Estudiante actualizado correctamente.");
    }





    // DELETE api/<EstudianteController>/5
    [HttpDelete("{id}")]
    [AllowAnonymous]
    public IActionResult Delete(int id)
    {
        try
        {
            EstudianteDTO estudiante = new EstudianteDTO { IdEstudiante = id };
            estudianteService.Eliminar(estudiante);
            return NoContent(); // 204 Eliminado correctamente
        }
        catch (Exception ex)
        {
            return NotFound(); // o return StatusCode(500, ex.Message);
        }

    }

    [HttpGet("PorUsuario/{idUsuario}")]
    public ActionResult<EstudianteDTO> ObtenerPorUsuario(int idUsuario)
    {
        var estudiante = estudianteService.ObtenerPorIdUsuario(idUsuario);

        if (estudiante == null)
            return NoContent();

        return Ok(estudiante);
    }





}
