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
    public class ProductoDiaController : ControllerBase
    {
        IProductoDiaService productoDiaService;

        public ProductoDiaController(IProductoDiaService productoDiaService)
        {
            this.productoDiaService = productoDiaService;
        }


        // GET: api/<ProductoDiaController>
        [HttpGet]
        public IEnumerable<ProductoDiaDTO> Get()
        {
            return productoDiaService.Obtener();
        }

        // GET api/<ProductoDiaController>/5
        [HttpGet("{id}")]
        public ProductoDiaDTO Get(int id)
        {
            return productoDiaService.Obtener(id);
        }

        // POST api/<ProductoDiaController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductoDiaDTO productoDia)
        {
            productoDiaService.Agregar(productoDia);
            return Ok(productoDia);
        }

        // PUT api/<ProductoDiaController>/5
        [HttpPut]
        public IActionResult Put([FromBody] ProductoDiaDTO productoDia)
        {
            productoDiaService.Editar(productoDia);
            return Ok(productoDia);
        }

        // DELETE api/<ProductoDiaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ProductoDiaDTO productoDia = new ProductoDiaDTO
            {
                IdProductoDia = id
            };
            productoDiaService.Eliminar(productoDia);
        }
    }
}
