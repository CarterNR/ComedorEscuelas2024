using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string? NombreProducto { get; set; }

        public string? NombreEscuela { get; set; }

        public string? NombreProveedor { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int? Cantidad { get; set; }

        public byte[]? Imagen { get; set; }

        public string? ImagenBase64 { get; set; }


        [Display(Name = "Cargar Imagen")]
        public IFormFile? ImagenArchivo { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public bool? Estado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un proveedor.")]
        public int? IdProveedor { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una escuela.")]
        public int? IdEscuela { get; set; }

        public List<SelectListItem>? ListaProductos { get; set; }

        public IEnumerable<SelectListItem> ListaEscuelas { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> ListaProveedores { get; set; } = new List<SelectListItem>();
    }
}
