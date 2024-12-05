using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class UsuarioViewModel
    {

        public int IdUsuario { get; set; }

        public string? NombreCompleto { get; set; }

        public int? IdTipoCedula { get; set; }

        public string? Cedula { get; set; }

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public string? CorreoElectronico { get; set; }

        public string? Clave { get; set; }

        public bool? Estado { get; set; }

        public int? IdEscuela { get; set; }

        public int? IdRol { get; set; }



        // Campo adicional para el nombre de la escuela y proveedor
        public string? NombreRol { get; set; }
        public string? NombreEscuela { get; set; }
        public string? TipoCedula1 { get; set; }



    }
}