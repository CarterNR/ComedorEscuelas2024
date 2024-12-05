using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ProductoDiaViewModel
    {

        public int IdProductoDia { get; set; }

        public int? IdProducto { get; set; }

        public int? Cantidad { get; set; }

        public DateTime? Fecha { get; set; }

        public bool? Estado { get; set; }

        public int? IdEscuela { get; set; }

        public string? NombreEscuela { get; set; }
        public string? NombreProducto { get; set; }


        public IEnumerable<SelectListItem> ListaProductos { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> ListaEscuelas { get; set; } = new List<SelectListItem>();



    }
}