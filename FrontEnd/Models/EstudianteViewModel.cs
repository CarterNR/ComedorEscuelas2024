using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class EstudianteViewModel
    {
            public int IdEstudiante { get; set; }
            public string Nombre { get; set; }
            public string Cedula { get; set; }
            public int IdEscuela { get; set; }
            public int TiquetesRestantes { get; set; }
            public int IdUsuario { get; set; }
            public byte[] RutaQR { get; set; }
            public string NombreUsuario { get; set; }
            public string Clave { get; set; }
            public DateTime? FechaUltimoRebajo { get; set; }

            public string NombreEscuela { get; set; }

        public IEnumerable<SelectListItem> ListaEscuelas { get; set; } = new List<SelectListItem>();





    }
}