using BackEnd.DTO;
using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        IProveedorService proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            this.proveedorService = proveedorService;
        }



        // GET: api/<ProveedorController>
        [HttpGet]
        public IEnumerable<ProveedorDTO> Get()
        {
            return proveedorService.Obtener();
        }

        // GET api/<ProveedorController>/5
        [HttpGet("{id}")]
        public ProveedorDTO Get(int id)
        {
            return proveedorService.Obtener(id);
        }

        // POST api/<ProveedorController>
        [HttpPost]
        public IActionResult Post([FromBody] ProveedorDTO proveedor)
        {
            proveedorService.Agregar(proveedor);
            return Ok(proveedor);
        }

        // PUT api/<ProveedorController>/5
        [HttpPut]
        public IActionResult Put([FromBody] ProveedorDTO proveedor)
        {
            proveedorService.Editar(proveedor);
            return Ok(proveedor);
        }

        // DELETE api/<ProveedorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ProveedorDTO proveedor = new ProveedorDTO
                {
                    IdProveedor = id
                };
                proveedorService.Eliminar(proveedor);
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("No se puede eliminar el proveedor porque tiene productos asociados.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al eliminar el proveedor: " + ex.Message);
            }
        }

    }
}
