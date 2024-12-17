using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido")]
        [Display(Name = "Nombre del Producto")]
        public string? NombreProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        public int? Cantidad { get; set; }

        public byte[]? Imagen { get; set; }
        [Display(Name = "Imagen del Producto")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)] // 5MB máximo
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
        public IFormFile? ImagenFile { get; set; }

        public bool? Estado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un proveedor")]
        [Display(Name = "Proveedor")]
        public int? IdProveedor { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una escuela")]
        [Display(Name = "Escuela")]
        public int? IdEscuela { get; set; }

        public string? NombreProveedor { get; set; }
        public string? NombreEscuela { get; set; }

        public IEnumerable<SelectListItem> ListaProveedores { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ListaEscuelas { get; set; } = new List<SelectListItem>();
    }
}
