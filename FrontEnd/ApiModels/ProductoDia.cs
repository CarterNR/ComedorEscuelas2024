using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.ApiModels
{
    public class ProductoDia
    {
        public int IdProductoDia { get; set; }

        public int? IdProducto { get; set; }

        public int? Cantidad { get; set; }

        public DateTime? Fecha { get; set; }

        public bool? Estado { get; set; }

        public int? IdEscuela { get; set; }

        public string? NombreEscuela { get; set; }
        public string? NombreProducto { get; set; }




    }
}
