using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class PedidoViewModel
    {

        public int IdPedido { get; set; }

        public int? IdProducto { get; set; }

        public DateTime? FechaHoraIngreso { get; set; }

        public int? Cantidad { get; set; }

        public int? IdUsuario { get; set; }

        public int IdEscuela { get; set; }

        public int? IdEstadoPedido { get; set; }

        public string? EstadoPedido1 { get; set; }
        public bool? Estado { get; set; }


        public string? NombreEscuela { get; set; }
        public string? NombreProducto { get; set; }

        public string? NombreCompleto { get; set; }

        public IEnumerable<SelectListItem> ListaProductos { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> ListaEscuelas { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> ListaUsuarios { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> ListaEstadoPedidos { get; set; } = new List<SelectListItem>();




    }
}