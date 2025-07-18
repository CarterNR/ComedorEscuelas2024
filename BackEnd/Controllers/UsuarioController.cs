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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            if (login == null || string.IsNullOrEmpty(login.NombreUsuario) || string.IsNullOrEmpty(login.Clave))
            {
                return BadRequest("Faltan datos de login.");
            }

            var usuario = usuarioService.ObtenerPorCredenciales(login.NombreUsuario, login.Clave);

            if (usuario == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            if (usuario.Estado == false)
            {
                return Unauthorized("Usuario inactivo.");
            }

            return Ok(usuario);
        }



        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<UsuarioDTO> Get()
        {
            return usuarioService.Obtener();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public UsuarioDTO Get(int id)
        {
            return usuarioService.Obtener(id);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioDTO usuario)
        {
            usuarioService.Agregar(usuario);
            return Ok(usuario);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut]
        public IActionResult Put([FromBody] UsuarioDTO usuario)
        {
            usuarioService.Editar(usuario);
            return Ok(usuario);
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            UsuarioDTO usuario = new UsuarioDTO
            {
                IdUsuario = id
            };
            usuarioService.Eliminar(usuario);
        }
    }
}
