using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ProveedorViewModel
    {

        public int IdProveedor { get; set; }

        public string? NombreProveedor { get; set; }

        public string? Telefono { get; set; }

        public string? CorreoElectronico { get; set; }

        public string? Direccion { get; set; }

        public bool? Estado { get; set; }


        public string? NombreEscuela { get; set; }


        public int? IdEscuela { get; set; }
        

        public IEnumerable<SelectListItem> ListaEscuelas { get; set; } = new List<SelectListItem>();



    }
}